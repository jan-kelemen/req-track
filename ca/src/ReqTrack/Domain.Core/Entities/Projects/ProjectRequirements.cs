using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectRequirements : IEnumerable<ProjectRequirement>
    {
        private IDictionary<Identity, ProjectRequirement> _requirementsById;

        private List<ProjectRequirement> _requirements;

        public ProjectRequirements(IEnumerable<ProjectRequirement> requirements)
        {
            var dict = new Dictionary<Identity, ProjectRequirement>();
            foreach (var requirement in requirements)
            {
                try
                {
                    dict.Add(requirement.Id, requirement);
                }
                catch
                {
                    throw new ArgumentException("Duplicate requirement found");
                }
            }

            var list = new List<ProjectRequirement>(dict.Values);
            list.Sort();

            _requirementsById = dict;
            _requirements = list;
        }

        public bool HasRequirement(Identity id) => _requirementsById.ContainsKey(id);

        public IEnumerable<Tuple<ProjectRequirement, string>> CanAddRequirements(
            IEnumerable<ProjectRequirement> requirements)
        {
            return CanAddRequirements(requirements, _requirementsById);
        }

        public IEnumerable<Tuple<ProjectRequirement, string>> CanUpdateRequirements(
            IEnumerable<ProjectRequirement> requirements)
        {
            return CanUpdateRequirements(requirements, _requirementsById);
        }

        public IEnumerable<Tuple<ProjectRequirement, string>> CanDeleteRequirements(
            IEnumerable<ProjectRequirement> requirements)
        {
            return CanDeleteRequirements(requirements, _requirementsById);
        }

        public void ChangeRequirements(
            IEnumerable<ProjectRequirement> requirementsToAdd,
            IEnumerable<ProjectRequirement> requirementsToUpdate,
            IEnumerable<ProjectRequirement> requirementsToDelete)
        {
            var dict = new Dictionary<Identity, ProjectRequirement>(_requirementsById);

            var toDelete = requirementsToDelete as ProjectRequirement[] ?? requirementsToDelete.ToArray();
            DeleteRequirements(toDelete, dict);

            var toUpdate = requirementsToUpdate as ProjectRequirement[] ?? requirementsToUpdate.ToArray();
            UpdateRequirements(toUpdate, dict);

            var toAdd = requirementsToAdd as ProjectRequirement[] ?? requirementsToAdd.ToArray();
            AddRequirements(toAdd, dict);

            _requirementsById = dict;

            _requirements = new List<ProjectRequirement>(_requirementsById.Values);
            _requirements.Sort();
        }

        public IEnumerator<ProjectRequirement> GetEnumerator() => _requirements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ProjectRequirement this[Identity id]
        {
            get
            {
                if (!HasRequirement(id))
                {
                    throw new ArgumentException("Requirement doesn't exist.");
                }

                return _requirementsById[id];
            }
        }

        public IEnumerable<ProjectRequirement> this[RequirementType type] => _requirements.Where(r => r.Type == type);

        private IEnumerable<Tuple<ProjectRequirement, string>> CanAddRequirements(
            IEnumerable<ProjectRequirement> requirements
            , IDictionary<Identity, ProjectRequirement> checkIn)
        {
            return requirements
                .Where(r => checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<ProjectRequirement, string>(r, "Requirement already exists"));
        }

        private IEnumerable<Tuple<ProjectRequirement, string>> CanUpdateRequirements(
            IEnumerable<ProjectRequirement> requirements
            , IDictionary<Identity, ProjectRequirement> checkIn)
        {
            var projectRequirements = requirements as ProjectRequirement[] ?? requirements.ToArray();

            return projectRequirements
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<ProjectRequirement, string>(r, "Requirement doesn't exist in project."))
                .Union(
                    projectRequirements
                        .Where(r => checkIn.ContainsKey(r.Id) && r.Type != checkIn[r.Id].Type)
                        .Select(r => new Tuple<ProjectRequirement, string>(r, "Can't change type of the requirement"))
                );
        }

        private IEnumerable<Tuple<ProjectRequirement, string>> CanDeleteRequirements(
            IEnumerable<ProjectRequirement> requirements
            , IDictionary<Identity, ProjectRequirement> checkIn)
        {
            return requirements
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<ProjectRequirement, string>(r, "Requirement doesn't exist in project."));
        }

        private void DeleteRequirements(ProjectRequirement[] toDelete, IDictionary<Identity, ProjectRequirement> dict)
        {
            var deleteErrors = CanDeleteRequirements(toDelete);
            if (deleteErrors.Any())
            {
                throw new ArgumentException("Can't delete some of the requirements");
            }

            foreach (var requirement in toDelete)
            {
                dict.Remove(requirement.Id);
            }
        }

        private void UpdateRequirements(ProjectRequirement[] toUpdate, IDictionary<Identity, ProjectRequirement> dict)
        {
            var updateErrors = CanUpdateRequirements(toUpdate, dict);
            if (updateErrors.Any())
            {
                throw new ArgumentException("Can't update some of the requirements");
            }

            foreach (var requirement in toUpdate)
            {
                dict[requirement.Id].OrderMarker = requirement.OrderMarker;
            }
        }

        private void AddRequirements(ProjectRequirement[] toAdd, IDictionary<Identity, ProjectRequirement> dict)
        {
            var addErrors = CanAddRequirements(toAdd, dict);
            if (addErrors.Any())
            {
                throw new ArgumentException("Can't add some of the requirements");
            }

            foreach (var requirement in toAdd)
            {
                dict.Add(requirement.Id, requirement);
            }
        }
    }
}
