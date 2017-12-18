using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements.RequestModels
{
    public class CreateRequirementRequest
    {
        /// <summary>
        /// Requirement to create. Identifier field is ignored.
        /// </summary>
        public Requirement Requirement { get; set; }
    }
}
