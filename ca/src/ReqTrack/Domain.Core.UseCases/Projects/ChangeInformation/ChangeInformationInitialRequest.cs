﻿using System.Collections.Generic;
using System.Linq;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Boundary.Requests;

namespace ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation
{
    public class ChangeInformationInitialRequest : RequestModel
    {
        public ChangeInformationInitialRequest(string requestedBy) : base(requestedBy)
        {
        }

        public string ProjectId { get; set; }

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
