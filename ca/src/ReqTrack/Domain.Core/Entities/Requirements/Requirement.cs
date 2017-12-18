using System;

namespace ReqTrack.Domain.Core.Entities.Requirements
{
    public class Requirement : Entity<Requirement>
    {
        public Requirement(Identity id, string title, RequirementType type, string details, Identity projectId) 
            : base(id)
        {
            Title = title;
            Type = type;
            Details = details;
            ProjectId = projectId;
        }

        /// <summary>
        /// Title of the requirement.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Type of the requirement.
        /// </summary>
        public RequirementType Type { get; set; }

        /// <summary>
        /// Details of the requirement.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Id of the project to which the requirement belongs.
        /// </summary>
        public Identity ProjectId { get; set; }

        public override bool HasSameValueAs(Requirement other)
        {
            //yagni for now.
            throw new NotImplementedException();
        }
    }
}
