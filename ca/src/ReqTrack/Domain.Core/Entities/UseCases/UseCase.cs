using System;
using System.Collections.Generic;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Users;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;
using ReqTrack.Domain.Core.Exceptions;

namespace ReqTrack.Domain.Core.Entities.UseCases
{
    public class UseCase : BasicUseCase
    {
        public class UseCaseStep : IComparable<UseCaseStep>
        {
            public int OrderMarker { get; set; }

            public string Content { get; set; }

            public int CompareTo(UseCaseStep other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return OrderMarker.CompareTo(other.OrderMarker);
            }
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
                    throw new ValidationException("Project is invalid")
                    {
                        PropertyKey = nameof(Project),
                    };
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
                    throw new ValidationException("Author is invalid.")
                    {
                        PropertyKey = nameof(Author),
                    };
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
                    throw new ValidationException("Note is invalid.")
                    {
                        PropertyKey = nameof(Note),
                    };
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
                        throw new ValidationException("Use case step isn't valid")
                        {
                            PropertyKey = nameof(Steps),
                        };
                    }
                }

                given.Sort();
                _steps = given;
            }
        }
    }
}
