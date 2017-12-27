using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using ReqTrack.Domain.Core.Entities.Users;

namespace ReqTrack.Domain.Core.Entities.UseCases
{
    public class UseCase : Entity<UseCase>
    {
        public static class Helper
        {
            public static readonly int MaximumTitleLength = 200;

            public static readonly int MaximumNoteLength = 1000;

            public static readonly int MaximumStepContentLength = 200;

            public static bool IsProjectValid(Project project)
            {
                return !ReferenceEquals(project, null);
            }

            public static bool IsAuthorValid(BasicUser author)
            {
                return !ReferenceEquals(author, null);
            }

            public static bool IsTitleValid(string title)
            {
                return !string.IsNullOrWhiteSpace(title) && title.Length <= MaximumTitleLength;
            }

            public static bool IsNoteValid(string note)
            {
                return note == null || note.Length <= MaximumNoteLength;
            }

            public static bool IsStepContentValid(string content)
            {
                return !string.IsNullOrWhiteSpace(content) && content.Length <= MaximumStepContentLength;
            }
        }

        public class Project
        {
            public Identity Id { get; set; }

            public string Name { get; set; }
        }

        public class Step
        {
            public Identity Id { get; set; }

            public Identity UseCaseId { get; set; }

            public string Content { get; set; }
        }

        private Project _project;

        private BasicUser _author;

        private string _title;

        private string _note;

        private readonly IDictionary<Identity, Step> _stepsById = new Dictionary<Identity, Step>();

        public UseCase(Identity id, Project project, BasicUser author, string title, string note, IEnumerable<Step> steps)
            : base(id)
        {
        }

        public Project ContainingProject
        {
            get => _project;
            set
            {
                if (!Helper.IsProjectValid(value))
                {
                    throw new ArgumentException("Project is invalid");
                }

                _project = value;
            }
        }

        public BasicUser Author
        {
            get => _author;
            set
            {
                if (!Helper.IsAuthorValid(value))
                {
                    throw new ArgumentException("Author is invalid");
                }

                _author = value;
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (!Helper.IsTitleValid(value))
                {
                    throw new ArgumentException("Title is invalid");
                }

                _title = value;
            }
        }

        public string Note
        {
            get => _note;
            set
            {
                if (!Helper.IsNoteValid(value))
                {
                    throw new ArgumentException("Note is invalid");
                }

                _note = value;
            }
        }

        public IEnumerable<Step> Steps => _stepsById.Values;

        public bool HasStep(Identity id) => _stepsById.ContainsKey(id);

        public IEnumerable<Tuple<Step, string>> CheckIfAllStepsCanBeAdded(IEnumerable<Step> steps)
        {
            return new List<Tuple<Step, string>>();
        }

        public void AddSteps(IEnumerable<Step> steps)
        {
            var s = steps as Step[] ?? steps.ToArray();

            var errors = CheckIfAllStepsCanBeAdded(s);
            if (errors.Any())
            {
                throw new ArgumentException("Can't add some of the steps to the use case.");
            }

            foreach (var step in s)
            {
                if (HasStep(step.Id))
                {
                    _stepsById[step.Id] = step;
                }
                else
                {
                    _stepsById.Add(step.Id, step);
                }
            }
        }

        public IEnumerable<Tuple<Step, string>> CheckIfAllStepsCanBeDeleted(IEnumerable<Step> steps)
        {
            var errorList = new List<Tuple<Step, string>>();
            foreach (var step in steps)
            {
                if (!HasStep(step.Id))
                {
                    errorList.Add(
                        new Tuple<Step, string>(
                            step,
                            "Use case doesn't have this step.")
                    );
                }
            }

            return errorList;
        }

        public void DeleteSteps(IEnumerable<Step> steps)
        {
            var s = steps as Step[] ?? steps.ToArray();

            var errors = CheckIfAllStepsCanBeDeleted(s);
            if (errors.Any())
            {
                throw new ArgumentException("Use case doesn't contain one of those use cases.");
            }

            foreach (var step in s)
            {
                _stepsById.Remove(step.Id);
            }
        }
    }
}
