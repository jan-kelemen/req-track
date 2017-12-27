using System;
using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.Entities.Requirements;

namespace ReqTrack.Domain.Core.Entities.Projects
{
    public class Project : Entity<Project>
    {
        public static class Helper
        {
            public static readonly int MaximumNameLength = 50;

            public static readonly int MaximumDescriptionLength = 1000;

            public static bool IsAuthorValid(User author)
            {
                return !ReferenceEquals(author, null);
            }

            public static bool IsNameValid(string name)
            {
                return !string.IsNullOrWhiteSpace(name) && name.Length <= MaximumNameLength;
            }

            public static bool IsDescriptionValid(string description)
            {
                return description == null || description.Length <= MaximumDescriptionLength;
            }
        }

        public class User
        {
            public Identity Id { get; set; }

            public string Name { get; set; }
        }

        public class Requirement
        {
            public Identity Id { get; set; }

            public RequirementType Type { get; set; }

            public string Title { get; set; }
        }

        public class UseCase
        {
            public Identity Id { get; set; }

            public string Title { get; set; }
        }

        private User _author;

        private string _name;

        private string _description;

        private readonly IDictionary<Identity, Requirement> _requirementsByIdentity = 
            new Dictionary<Identity, Requirement>();

        private readonly IDictionary<RequirementType, List<Requirement>> _requirementsByType =
            new SortedDictionary<RequirementType, List<Requirement>>
            {
                {RequirementType.Bussiness, new List<Requirement>()},
                {RequirementType.User, new List<Requirement>()},
                {RequirementType.Functional, new List<Requirement>()},
                {RequirementType.Nonfunctional, new List<Requirement>()},
            };

        private readonly IDictionary<Identity, UseCase> _useCasesById = new Dictionary<Identity, UseCase>();
        private readonly IDictionary<string, UseCase> _useCasesByTitle = new SortedDictionary<string, UseCase>();

        public Project(
            Identity id, 
            User author, 
            string name, 
            string description, 
            IEnumerable<Requirement> requirements, 
            IEnumerable<UseCase> useCases) 
            : base(id)
        {
            Author = author;
            Name = name;
            Description = description;
            AddRequirements(requirements);
            AddUseCases(useCases);
        }

        public User Author
        {
            get => _author;
            set
            {
                if (!Helper.IsAuthorValid(value))
                {
                    throw new ArgumentException("Author is null");
                }

                _author = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (!Helper.IsNameValid(value))
                {
                    throw new ArgumentException("Name is invalid");
                }

                _name = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (!Helper.IsDescriptionValid(value))
                {
                    throw new ArgumentException("Description is invalid");
                }

                _description = value;
            }
        }

        public IEnumerable<Requirement> Requirements => _requirementsByIdentity.Values;

        public IEnumerable<UseCase> UseCases => _useCasesByTitle.Values;

        public bool HasRequirement(Identity id) => _requirementsByIdentity.ContainsKey(id);

        public bool HasUseCase(Identity id) => _useCasesById.ContainsKey(id);

        public bool HasUseCase(string title) => _useCasesByTitle.ContainsKey(title);

        public IReadOnlyList<Requirement> GetRequirementsOfType(RequirementType type) => _requirementsByType[type];

        public IEnumerable<Tuple<Requirement, string>> CheckIfAllRequirementsCanBeAdded(IEnumerable<Requirement> requirements)
        {
            var errorList = new List<Tuple<Requirement, string>>();
            foreach (var requirement in requirements)
            {
                if (HasRequirement(requirement.Id) && _requirementsByIdentity[requirement.Id].Type != requirement.Type)
                {
                    errorList.Add(
                        new Tuple<Requirement, string>(
                            requirement, 
                            "Can't change type of the requirement.")
                        );
                }
            }

            return errorList;
        }

        public void AddRequirements(IEnumerable<Requirement> requirements)
        {
            var reqs = requirements as Requirement[] ?? requirements.ToArray();

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

        public IEnumerable<Tuple<Requirement, string>> CheckIfAllRequirementsCanBeDeleted(IEnumerable<Requirement> requirements)
        {
            var errorList = new List<Tuple<Requirement, string>>();
            foreach (var requirement in requirements)
            {
                if (!HasRequirement(requirement.Id))
                {
                    errorList.Add(
                        new Tuple<Requirement, string>(
                            requirement,
                            "Project doesn't have this requirement.")
                        );
                }
            }

            return errorList;
        }

        public void DeleteRequirements(IEnumerable<Requirement> requirements)
        {
            var reqs = requirements as Requirement[] ?? requirements.ToArray();

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

        public IEnumerable<Tuple<UseCase, string>> CheckIfAllUseCasesCanBeAdded(IEnumerable<UseCase> useCases)
        {
            var errorList = new List<Tuple<UseCase, string>>();
            foreach (var useCase in useCases)
            {
                if (HasUseCase(useCase.Title) && !HasUseCase(useCase.Id))
                {
                    errorList.Add(
                        new Tuple<UseCase, string>(
                            useCase,
                            "Project already has a usecase of the same name")
                        );
                }
            }

            return errorList;
        }

        public void AddUseCases(IEnumerable<UseCase> useCases)
        {
            var cases = useCases as UseCase[] ?? useCases.ToArray();

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

        public IEnumerable<Tuple<UseCase, string>> CheckIfAllUseCasesCanBeDeleted(IEnumerable<UseCase> useCases)
        {
            var errorList = new List<Tuple<UseCase, string>>();
            foreach (var useCase in useCases)
            {
                if (!HasRequirement(useCase.Id))
                {
                    errorList.Add(
                        new Tuple<UseCase, string>(
                            useCase,
                            "Project doesn't have this use case.")
                    );
                }
            }

            return errorList;
        }

        public void DeleteUseCases(IEnumerable<UseCase> useCases)
        {
            var ucs = useCases as UseCase[] ?? useCases.ToArray();

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
