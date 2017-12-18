using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels
{
    public class UpdateRequirementResponse
    {
        /// <summary>
        /// Updated requirement.
        /// </summary>
        public Requirement Requirement { get; set; }
    }
}
