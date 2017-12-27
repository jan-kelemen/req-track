using System;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Users;

namespace ReqTrack.Domain.Core.Entities.Requirements
{
    public class Requirement : BasicRequirement
    {
        private BasicProject _project;

        private BasicUser _author;

        private string _note;

        public Requirement(
            Identity id, 
            BasicProject project, 
            BasicUser author, 
            RequirementType type, 
            string title, 
            string note) 
            : base(id, type, title)
        {
            Project = project;
            Author = author;
            Note = note;
        }

        public BasicProject Project
        {
            get => _project;
            set
            {
                if(!RequirementValidationHelper.IsProjectValid(value))
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
                if (!RequirementValidationHelper.IsAuthorValid(value))
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
                if (!RequirementValidationHelper.IsNoteValid(value))
                {
                    throw new ArgumentException("Note is invalid");
                }

                _note = value;
            }
        }
    }
}
