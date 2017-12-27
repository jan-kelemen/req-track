using System;
using ReqTrack.Domain.Core.Entities.Users;

namespace ReqTrack.Domain.Core.Entities.ValidationHelpers
{
    public static class ProjectValidationHelper
    {
        public static readonly int MaximumNameLength = 50;

        public static readonly int MaximumDescriptionLength = 1000;

        public static bool IsAuthorValid(BasicUser author)
        {
            return !ReferenceEquals(author, null);
        }

        public static bool IsNameValid(string name)
        {
            return !String.IsNullOrWhiteSpace(name) && name.Length <= MaximumNameLength;
        }

        public static bool IsDescriptionValid(string description)
        {
            return description == null || description.Length <= MaximumDescriptionLength;
        }
    }
}