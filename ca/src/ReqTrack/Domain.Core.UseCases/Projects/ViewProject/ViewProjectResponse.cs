using System.Collections.Generic;
using ReqTrack.Domain.Core.UseCases.Boundary;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Projects.ViewProject
{
    public class ViewProjectResponse : ResponseModel
    {
        internal ViewProjectResponse(ExecutionStatus status) : base(status)
        {
        }

        public string ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public User Author { get; set; }

        public ProjectRights Rights { get; set; }

        public IEnumerable<Requirement> Requirements { get; set; }

        public IEnumerable<UseCase> UseCases { get; set; }
    }
}
