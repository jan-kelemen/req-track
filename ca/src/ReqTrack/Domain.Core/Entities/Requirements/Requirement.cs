using System;

namespace ReqTrack.Domain.Core.Entities.Requirements
{
    public class Requirement : Entity<Requirement>
    {
        public static class Helper
        {
            public static readonly int MaximumTitleLength = 200;

            public static readonly int MaximumNoteLength = 1000;

            public static bool IsProjectValid(Project project)
            {
                return !ReferenceEquals(project, null);
            }

            public static bool IsAuthorValid(User author)
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
        }

        public class User
        {
            public Identity Id { get; set; }

            public string Name { get; set; }
        }

        public class Project
        {
            public Identity Id { get; set; }

            public string Name { get; set; }
        }

        private Project _project;

        private User _author;

        private RequirementType _type;

        private string _title;

        private string _note;

        public Requirement(
            Identity id, 
            Project project, 
            User author, 
            RequirementType type, 
            string title, 
            string note) 
            : base(id)
        {
            ContainingProject = project;
            Author = author;
            Type = type;
            Title = title;
            Note = note;
        }

        public Project ContainingProject
        {
            get => _project;
            set
            {
                if(!Helper.IsProjectValid(value))
                {
                    throw new ArgumentException("Project is invalid");
                }

                _project = value;
            }
        }

        public User Author
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

        public RequirementType Type
        {
            get => _type;
            set => _type = value;
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
    }
}
