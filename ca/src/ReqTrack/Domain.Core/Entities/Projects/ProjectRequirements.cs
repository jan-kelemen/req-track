using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class ProjectRequirements : IEnumerable<Project.Requirement>
    {
        private IDictionary<Identity, Project.Requirement> _requirementsById;

        private List<Project.Requirement> _requirements;

        public ProjectRequirements(IEnumerable<Project.Requirement> requirements)
        {
            var dict = new Dictionary<Identity, Project.Requirement>();
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

            var list = new List<Project.Requirement>(dict.Values);
            list.Sort();

            _requirementsById = dict;
            _requirements = list;
        }

        public bool HasRequirement(Identity id) => _requirementsById.ContainsKey(id);

        public IEnumerable<Tuple<Project.Requirement, string>> CanAddRequirements(
            IEnumerable<Project.Requirement> requirements)
        {
            return CanAddRequirements(requirements, _requirementsById);
        }

        public IEnumerable<Tuple<Project.Requirement, string>> CanUpdateRequirements(
            IEnumerable<Project.Requirement> requirements)
        {
            return CanUpdateRequirements(requirements, _requirementsById);
        }

        public IEnumerable<Tuple<Project.Requirement, string>> CanDeleteRequirements(
            IEnumerable<Project.Requirement> requirements)
        {
            return CanDeleteRequirements(requirements, _requirementsById);
        }

        public void ChangeRequirements(
            IEnumerable<Project.Requirement> requirementsToAdd,
            IEnumerable<Project.Requirement> requirementsToUpdate,
            IEnumerable<Project.Requirement> requirementsToDelete)
        {
            var dict = new Dictionary<Identity, Project.Requirement>(_requirementsById);

            var toDelete = requirementsToDelete as Project.Requirement[] ?? requirementsToDelete.ToArray();
            DeleteRequirements(toDelete, dict);

            var toUpdate = requirementsToUpdate as Project.Requirement[] ?? requirementsToUpdate.ToArray();
            UpdateRequirements(toUpdate, dict);

            var toAdd = requirementsToAdd as Project.Requirement[] ?? requirementsToAdd.ToArray();
            AddRequirements(toAdd, dict);

            _requirementsById = dict;

            _requirements = new List<Project.Requirement>(_requirementsById.Values);
            _requirements.Sort();
        }

        public IEnumerator<Project.Requirement> GetEnumerator() => _requirements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Project.Requirement this[Identity id]
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

        public IEnumerable<Project.Requirement> this[RequirementType type] => _requirements.Where(r => r.Type == type);

        private IEnumerable<Tuple<Project.Requirement, string>> CanAddRequirements(
            IEnumerable<Project.Requirement> requirements
            , IDictionary<Identity, Project.Requirement> checkIn)
        {
            return requirements
                .Where(r => checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<Project.Requirement, string>(r, "Requirement already exists"));
        }

        private IEnumerable<Tuple<Project.Requirement, string>> CanUpdateRequirements(
            IEnumerable<Project.Requirement> requirements
            , IDictionary<Identity, Project.Requirement> checkIn)
        {
            var projectRequirements = requirements as Project.Requirement[] ?? requirements.ToArray();

            return projectRequirements
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<Project.Requirement, string>(r, "Requirement doesn't exist in project."))
                .Union(
                    projectRequirements
                        .Where(r => checkIn.ContainsKey(r.Id) && r.Type != checkIn[r.Id].Type)
                        .Select(r => new Tuple<Project.Requirement, string>(r, "Can't change type of the requirement"))
                );
        }

        private IEnumerable<Tuple<Project.Requirement, string>> CanDeleteRequirements(
            IEnumerable<Project.Requirement> requirements
            , IDictionary<Identity, Project.Requirement> checkIn)
        {
            return requirements
                .Where(r => !checkIn.ContainsKey(r.Id))
                .Select(r => new Tuple<Project.Requirement, string>(r, "Requirement doesn't exist in project."));
        }

        private void DeleteRequirements(Project.Requirement[] toDelete, IDictionary<Identity, Project.Requirement> dict)
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

        private void UpdateRequirements(Project.Requirement[] toUpdate, IDictionary<Identity, Project.Requirement> dict)
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

        private void AddRequirements(Project.Requirement[] toAdd, IDictionary<Identity, Project.Requirement> dict)
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
