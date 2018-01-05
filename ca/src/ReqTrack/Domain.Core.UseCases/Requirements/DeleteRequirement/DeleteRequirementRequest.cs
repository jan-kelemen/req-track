﻿using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement
{
    public class DeleteRequirementRequest : RequestModel
    {
        public DeleteRequirementRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

        public string RequirementId { get; set; }

        public override bool Validate(out Dictionary<string, string> errors)
        {
            base.Validate(out errors);

            if (string.IsNullOrWhiteSpace(ProjectId))
            {
                errors.Add(nameof(ProjectId), "Project identifier is invalid");
            }

            return !errors.Any();
        }
    }
}