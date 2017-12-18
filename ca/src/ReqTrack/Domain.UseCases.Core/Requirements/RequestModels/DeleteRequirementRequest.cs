namespace ReqTrack.Domain.UseCases.Core.Requirements.RequestModels
{
    public class DeleteRequirementRequest
    {
        /// <summary>
        /// Identifier of the requirement to be deleted, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }
}
