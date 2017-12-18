using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels
{
    public class CreateRequirementResponse
    {
        /// <summary>
        /// Created requierment.
        /// </summary>
        public Requirement Requirement { get; set; }
    }
}
