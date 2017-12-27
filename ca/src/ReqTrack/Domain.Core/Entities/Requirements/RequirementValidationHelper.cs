﻿using System;
using ReqTrack.Domain.Core.Entities.Projects;
using ReqTrack.Domain.Core.Entities.Users;

namespace ReqTrack.Domain.Core.Entities.Requirements
{
    public static class RequirementValidationHelper
    {
        public static readonly int MaximumTitleLength = 200;

        public static readonly int MaximumNoteLength = 1000;

        public static bool IsProjectValid(BasicProject project)
        {
            return !ReferenceEquals(project, null);
        }

        public static bool IsAuthorValid(BasicUser author)
        {
            return !ReferenceEquals(author, null);
        }

        public static bool IsTitleValid(string title)
        {
            return !String.IsNullOrWhiteSpace(title) && title.Length <= MaximumTitleLength;
        }

        public static bool IsNoteValid(string note)
        {
            return note == null || note.Length <= MaximumNoteLength;
        }
    }
}