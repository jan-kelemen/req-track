using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements.RequestModels
{
    public class UpdateRequirementRequest
    {
        /// <summary>
        /// Requirement to be updated.
        /// </summary>
        public Requirement Requirement { get; set; }
    }
}
