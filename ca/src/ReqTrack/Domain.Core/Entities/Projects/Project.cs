using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Entities.Users;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class Project : BasicProject
    {
        private BasicUser _author;

        private string _description;

        private readonly IDictionary<Identity, ProjectRequirement> _requirementsByIdentity = 
            new Dictionary<Identity, ProjectRequirement>();

        private readonly IDictionary<RequirementType, List<ProjectRequirement>> _requirementsByType =
            new SortedDictionary<RequirementType, List<ProjectRequirement>>
            {
                {RequirementType.Bussiness, new List<ProjectRequirement>()},
                {RequirementType.User, new List<ProjectRequirement>()},
                {RequirementType.Functional, new List<ProjectRequirement>()},
                {RequirementType.Nonfunctional, new List<ProjectRequirement>()},
            };

        private readonly IDictionary<Identity, ProjectUseCase> _useCasesById = new Dictionary<Identity, ProjectUseCase>();
        private readonly IDictionary<string, ProjectUseCase> _useCasesByTitle = new SortedDictionary<string, ProjectUseCase>();

        public Project(
            Identity id, 
            BasicUser author, 
            string name, 
            string description, 
            IEnumerable<ProjectRequirement> requirements, 
            IEnumerable<ProjectUseCase> useCases) 
            : base(id, name)
        {
            Author = author;
            Description = description;
            AddRequirements(requirements);
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

        public IEnumerable<ProjectRequirement> Requirements => _requirementsByIdentity.Values;

        public IEnumerable<ProjectUseCase> UseCases => _useCasesByTitle.Values;

        public bool HasRequirement(Identity id) => _requirementsByIdentity.ContainsKey(id);

        public bool HasUseCase(Identity id) => _useCasesById.ContainsKey(id);

        public bool HasUseCase(string title) => _useCasesByTitle.ContainsKey(title);

        public IReadOnlyList<ProjectRequirement> GetRequirementsOfType(RequirementType type) => _requirementsByType[type];

        public IEnumerable<Tuple<ProjectRequirement, string>> CheckIfAllRequirementsCanBeAdded(IEnumerable<ProjectRequirement> requirements)
        {
            var errorList = new List<Tuple<ProjectRequirement, string>>();
            foreach (var requirement in requirements)
            {
                if (HasRequirement(requirement.Id) && _requirementsByIdentity[requirement.Id].Type != requirement.Type)
                {
                    errorList.Add(
                        new Tuple<ProjectRequirement, string>(
                            requirement, 
                            "Can't change type of the requirement.")
                        );
                }
            }

            return errorList;
        }

        public void AddRequirements(IEnumerable<ProjectRequirement> requirements)
        {
            var reqs = requirements as ProjectRequirement[] ?? requirements.ToArray();

            var errors = CheckIfAllRequirementsCanBeAdded(reqs);
            if (errors.Any())
            {
                throw new ArgumentException("Can't add some of the requirements to the user.");
            }

            foreach (var requirement in reqs)
            {
                if (HasRequirement(requirement.Id))
                {
                    var oldEntity = _requirementsByIdentity[requirement.Id];

                    _requirementsByIdentity[requirement.Id] = requirement;
                    var index = _requirementsByType[requirement.Type].IndexOf(oldEntity);
                    _requirementsByType[requirement.Type][index] = requirement;
                }
                else
                {
                    _requirementsByIdentity.Add(requirement.Id, requirement);
                    _requirementsByType[requirement.Type].Add(requirement);
                }
            }
        }

        public IEnumerable<Tuple<ProjectRequirement, string>> CheckIfAllRequirementsCanBeDeleted(IEnumerable<ProjectRequirement> requirements)
        {
            var errorList = new List<Tuple<ProjectRequirement, string>>();
            foreach (var requirement in requirements)
            {
                if (!HasRequirement(requirement.Id))
                {
                    errorList.Add(
                        new Tuple<ProjectRequirement, string>(
                            requirement,
                            "Project doesn't have this requirement.")
                        );
                }
            }

            return errorList;
        }

        public void DeleteRequirements(IEnumerable<ProjectRequirement> requirements)
        {
            var reqs = requirements as ProjectRequirement[] ?? requirements.ToArray();

            var errors = CheckIfAllRequirementsCanBeDeleted(reqs);
            if (errors.Any())
            {
                throw new ArgumentException("Can't delete some of the requirements");
            }

            foreach (var requirement in reqs)
            {
                var oldEntity = _requirementsByIdentity[requirement.Id];
                _requirementsByIdentity.Remove(requirement.Id);
                _requirementsByType[oldEntity.Type].Remove(oldEntity);
            }
        }

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
