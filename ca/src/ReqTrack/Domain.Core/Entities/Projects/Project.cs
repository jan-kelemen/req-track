using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Entities.UseCases;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class Project : BasicProject
    {
        public class Requirement : BasicRequirement, IComparable<Requirement>
        {
            public Requirement(Identity id, RequirementType type, string title, int orderMarker)
                : base(id, type, title)
            {
                OrderMarker = orderMarker;
            }

            public int OrderMarker { get; set; }

            public int CompareTo(Requirement other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;

                var typeComparison = Type.CompareTo(other.Type);

                if (typeComparison == 0)
                {
                    return OrderMarker.CompareTo(other.OrderMarker);
                }

                return typeComparison;
            }
        }

        public class UseCase : BasicUseCase, IComparable<UseCase>
        {
            public UseCase(Identity id, string title, int orderMarker) : base(id, title)
            {
                OrderMarker = orderMarker;
            }

            public int OrderMarker { get; set; }

            public int CompareTo(UseCase other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return OrderMarker.CompareTo(other.OrderMarker);
            }
        }

        private BasicUser _author;

        private string _description;

        private readonly ProjectRequirements _requirements;

        private readonly ProjectUseCases _useCases;

        public Project(
            Identity id, 
            BasicUser author, 
            string name, 
            string description = null,
            IEnumerable<Requirement> requirements = null, 
            IEnumerable<UseCase> useCases = null) 
            : base(id, name)
        {
            Author = author;
            Description = description;

            _requirements = requirements == null ? null : new ProjectRequirements(requirements);
            _useCases = useCases == null ? null : new ProjectUseCases(useCases);
        }

        public BasicUser Author
        {
            get => _author;
            set
            {
                if (!ProjectValidationHelper.IsAuthorValid(value))
                {
                    throw new ValidationException("Author is invalid.")
                    {
                        PropertyKey = nameof(Author),
                    };
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
                    throw new ValidationException("Description is invalid")
                    {
                        PropertyKey = nameof(Description),
                    };
                }

                _description = value;
            }
        }

        public IEnumerable<Requirement> Requirements => _requirements;

        public IEnumerable<UseCase> UseCases => _useCases;

        public bool HasRequirement(Identity id)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements.HasRequirement(id);
        }

        public Requirement GetRequirement(Identity id)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements[id];
        }

        public IEnumerable<Requirement> GetRequirementsOfType(RequirementType type)
        {
            if (_requirements == null)
            {
                throw new ApplicationException("Project requirements weren't loaded");
            }

            return _requirements[type];
        }

        public IEnumerable<Tuple<Requirement, string>> CanChangeRequirements(
            IEnumerable<Requirement> requirementsToAdd,
            IEnumerable<Requirement> requirementsToUpdate,
            IEnumerable<Requirement> requirementsToDelete)
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
            IEnumerable<Requirement> requirementsToAdd,
            IEnumerable<Requirement> requirementsToUpdate,
            IEnumerable<Requirement> requirementsToDelete)
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

        public UseCase GetUseCase(Identity id)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases[id];
        }

        public UseCase GetUseCase(string title)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            return _useCases[title];
        }

        public IEnumerable<Tuple<UseCase, string>> CanChangeUseCases(
            IEnumerable<UseCase> useCasesToAdd,
            IEnumerable<UseCase> useCasesToUpdate,
            IEnumerable<UseCase> useCasesToDelete)
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
            IEnumerable<UseCase> useCasesToAdd,
            IEnumerable<UseCase> useCasesToUpdate,
            IEnumerable<UseCase> useCasesToDelete)
        {
            if (_useCases == null)
            {
                throw new ApplicationException("Project use cases weren't loaded");
            }

            _useCases.ChangeUseCases(useCasesToAdd, useCasesToUpdate, useCasesToDelete);
        }
    }
}
