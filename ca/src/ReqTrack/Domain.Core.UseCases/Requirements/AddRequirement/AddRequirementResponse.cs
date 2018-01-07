using ReqTrack.Domain.Core.UseCases.Boundary.Responses;

namespace ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement
{
    public class AddRequirementResponse : ResponseModel
    {
        public string GivenId { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string[] Types { get; set; }
    }
}
