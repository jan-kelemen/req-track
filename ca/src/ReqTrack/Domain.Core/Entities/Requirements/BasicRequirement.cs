using System;
using ReqTrack.Domain.Core.Entities.ValidationHelpers;

namespace ReqTrack.Domain.Core.Entities.Requirements
{
    public class BasicRequirement : Entity<BasicRequirement>
    {
        private RequirementType _type;

        private string _title;

        public BasicRequirement(Identity id, RequirementType type, string title) : base(id)
        {
            Type = type;
            Title = title;
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
                if (!RequirementValidationHelper.IsTitleValid(value))
                {
                    throw new ArgumentException("Title is invalid");
                }

                _title = value;
            }
        }
    }
}
