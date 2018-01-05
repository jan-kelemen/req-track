﻿using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
namespace ReqTrack.Domain.Core.UseCases.Projects.ViewProjectRequirements
{
    public class ViewProjectRequirementsResponse : ResponseModel
    {
        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
