using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels
{
    public class GetRequirementResponse
    {
        /// <summary>
        /// Read requirement.
        /// </summary>
        public Requirement Requirement { get; set; }
    }
}
