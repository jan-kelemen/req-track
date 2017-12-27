using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class Project : BasicProject
    {
        private BasicUser _author;

        private string _description;

        private readonly ProjectRequirements _requirements;

        private readonly ProjectUseCases _useCases;

        public Project(
            Identity id, 
            BasicUser author, 
            string name, 
            string description = null,
            ProjectRequirements requirements = null, 
            ProjectUseCases useCases = null) 
            : base(id, name)
        {
            Author = author;
            Description = description;

            _requirements = requirements;
            _useCases = useCases;
        }

        public BasicUser Author
        {
            get => _author;
            set
            {
                if (!ProjectValidationHelper.IsAuthorValid(value))
                {
                    throw new ArgumentException("Author is null");
                }

                _author = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (!ProjectValidationHelper.IsDescriptionValid(value))
                {
                    throw new ArgumentException("Description is invalid");
                }

                _description = value;
            }
        }

        public IEnumerable<ProjectRequirement> Requirements => _requirements;

        public IEnumerable<ProjectUseCase> UseCases => _useCases;

        public bool HasRequirement(Identity id)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements.HasRequirement(id);
        }

        public ProjectRequirement GetRequirement(Identity id)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements[id];
        }

        public IEnumerable<ProjectRequirement> GetRequirementsOfType(RequirementType type)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements[type];
        }

        public IEnumerable<Tuple<ProjectRequirement, string>> CanChangeRequirements(
            IEnumerable<ProjectRequirement> requirementsToAdd,
            IEnumerable<ProjectRequirement> requirementsToUpdate,
            IEnumerable<ProjectRequirement> requirementsToDelete)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements.CanAddRequirements(requirementsToAdd)
                .Union(_requirements.CanUpdateRequirements(requirementsToUpdate))
                .Union(_requirements.CanDeleteRequirements(requirementsToDelete));
        }

        public void ChangeRequirements(
            IEnumerable<ProjectRequirement> requirementsToAdd,
            IEnumerable<ProjectRequirement> requirementsToUpdate,
            IEnumerable<ProjectRequirement> requirementsToDelete)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            _requirements.ChangeRequirements(requirementsToAdd, requirementsToUpdate, requirementsToDelete);
        }

        public bool HasUseCase(Identity id)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases.HasUseCase(id);
        }

        public bool HasUseCase(string title)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases.HasUseCase(title);
        }

        public ProjectUseCase GetUseCase(Identity id)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases[id];
        }

        public ProjectUseCase GetUseCase(string title)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases[title];
        }

        public IEnumerable<Tuple<ProjectUseCase, string>> CanChangeUseCases(
            IEnumerable<ProjectUseCase> useCasesToAdd,
            IEnumerable<ProjectUseCase> useCasesToUpdate,
            IEnumerable<ProjectUseCase> useCasesToDelete)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases.CanAddUseCases(useCasesToAdd)
                .Union(_useCases.CanUpdateUseCases(useCasesToUpdate))
                .Union(_useCases.CanDeleteUseCases(useCasesToDelete));
        }

        public void ChangeUseCases(
            IEnumerable<ProjectUseCase> useCasesToAdd,
            IEnumerable<ProjectUseCase> useCasesToUpdate,
            IEnumerable<ProjectUseCase> useCasesToDelete)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            _useCases.ChangeUseCases(useCasesToAdd, useCasesToUpdate, useCasesToDelete);
        }
    }
}
