namespace ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels
{
    public class DeleteRequirementResponse
    {
        /// <summary>
        /// Identifier of the deleted requirement, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }
}
