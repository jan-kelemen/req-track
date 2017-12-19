using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects
{
    public class ProjectWithRequirements : ProjectInfo
    {
        public class Requirement
        {
            public string Id { get; set; }

            public string Type { get; set; }

            public string Title { get; set; }
        }

        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
