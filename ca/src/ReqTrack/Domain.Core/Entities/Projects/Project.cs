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

        private readonly IDictionary<Identity, ProjectUseCase> _useCasesById = new Dictionary<Identity, ProjectUseCase>();
        private readonly IDictionary<string, ProjectUseCase> _useCasesByTitle = new SortedDictionary<string, ProjectUseCase>();

        public Project(
            Identity id, 
            BasicUser author, 
            string name, 
            string description,
            ProjectRequirements requirements, 
            IEnumerable<ProjectUseCase> useCases) 
            : base(id, name)
        {
            Author = author;
            Description = description;

            _requirements = requirements;

            AddUseCases(useCases);
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

        public IEnumerable<ProjectUseCase> UseCases => _useCasesByTitle.Values;

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

        public bool HasUseCase(Identity id) => _useCasesById.ContainsKey(id);

        public bool HasUseCase(string title) => _useCasesByTitle.ContainsKey(title);

        public IEnumerable<Tuple<ProjectUseCase, string>> CheckIfAllUseCasesCanBeAdded(IEnumerable<ProjectUseCase> useCases)
        {
            var errorList = new List<Tuple<ProjectUseCase, string>>();
            foreach (var useCase in useCases)
            {
                if (HasUseCase(useCase.Title) && !HasUseCase(useCase.Id))
                {
                    errorList.Add(
                        new Tuple<ProjectUseCase, string>(
                            useCase,
                            "Project already has a usecase of the same name")
                        );
                }
            }

            return errorList;
        }

        public void AddUseCases(IEnumerable<ProjectUseCase> useCases)
        {
            var cases = useCases as ProjectUseCase[] ?? useCases.ToArray();

            var errors = CheckIfAllUseCasesCanBeAdded(cases);
            if (errors.Any())
            {
                throw new ArgumentException("Use cases with same name already exits for this project");
            }

            foreach (var useCase in cases)
            {
                if (HasUseCase(useCase.Id))
                {
                    var oldEntity = _useCasesById[useCase.Id];

                    _useCasesById[useCase.Id] = useCase;
                    _useCasesByTitle.Remove(oldEntity.Title);
                    _useCasesByTitle.Add(useCase.Title, useCase);
                }
                else
                {
                    _useCasesById.Add(useCase.Id, useCase);
                    _useCasesByTitle.Add(useCase.Title, useCase);
                }
            }
        }

        public IEnumerable<Tuple<ProjectUseCase, string>> CheckIfAllUseCasesCanBeDeleted(IEnumerable<ProjectUseCase> useCases)
        {
            var errorList = new List<Tuple<ProjectUseCase, string>>();
            foreach (var useCase in useCases)
            {
                if (!HasRequirement(useCase.Id))
                {
                    errorList.Add(
                        new Tuple<ProjectUseCase, string>(
                            useCase,
                            "Project doesn't have this use case.")
                    );
                }
            }

            return errorList;
        }

        public void DeleteUseCases(IEnumerable<ProjectUseCase> useCases)
        {
            var ucs = useCases as ProjectUseCase[] ?? useCases.ToArray();

            var errors = CheckIfAllUseCasesCanBeDeleted(ucs);
            if (errors.Any())
            {
                throw new ArgumentException("Project doesn't contain one of the use cases.");
            }

            foreach (var useCase in ucs)
            {
                _useCasesByTitle.Remove(_useCasesById[useCase.Id].Title);
                _useCasesById.Remove(useCase.Id);
            }
        }
    }
}
