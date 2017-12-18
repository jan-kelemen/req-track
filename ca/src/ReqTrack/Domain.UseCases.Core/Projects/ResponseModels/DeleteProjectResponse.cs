namespace ReqTrack.Domain.UseCases.Core.Projects.ResponseModels
{
    public class DeleteProjectResponse
    {
        /// <summary>
        /// Identifier of the deleted project, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }
}
