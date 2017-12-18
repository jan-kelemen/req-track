namespace ReqTrack.Domain.UseCases.Core.Projects.RequestModels
{
    public class DeleteProjectRequest
    {
        /// <summary>
        /// Identifier of the project to be deleted, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }
}
