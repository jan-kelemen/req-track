using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Domain.Core.Entities.UseCases
{
    public class UseCase : BasicUseCase
    {
        public class UseCaseStep
        {
            public int OrderMarker { get; set; }

            public string Content { get; set; }
        }

        private BasicProject _project;

        private BasicUser _author;

        private string _note;

        private IList<UseCaseStep> _steps;

        public UseCase(Identity id, BasicProject project, BasicUser author, string title, string note, IEnumerable<UseCaseStep> steps)
            : base(id, title)
        {
            Project = project;
            Author = author;
            Note = note;
            Steps = steps;
        }

        public BasicProject Project
        {
            get => _project;
            set
            {
                if (!UseCaseValidationHelper.IsProjectValid(value))
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
                if (!UseCaseValidationHelper.IsAuthorValid(value))
                {
                    throw new ArgumentException("Author is invalid");
                }

                _author = value;
            }
        }

        public string Note
        {
            get => _note;
            set
            {
                if (!UseCaseValidationHelper.IsNoteValid(value))
                {
                    throw new ArgumentException("Note is invalid");
                }

                _note = value;
            }
        }

        public IEnumerable<UseCaseStep> Steps
        {
            get => _steps;
            set
            {
                var given = new List<UseCaseStep>(value);
                foreach (var step in given)
                {
                    if (!UseCaseValidationHelper.IsStepContentValid(step.Content))
                    {
                        throw new ArgumentException("Use case step isn't valid");
                    }
                }

                _steps = given;
            }
        }
    }
}
