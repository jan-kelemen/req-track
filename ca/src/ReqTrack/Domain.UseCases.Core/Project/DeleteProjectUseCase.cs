using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Interfaces;

namespace ReqTrack.Domain.UseCases.Core.Project
{
    public class DeleteProjectRequest
    {
        /// <summary>
        /// <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteProjectResponse
    {
        /// <summary>
        /// <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Indicates if the deletion was successfull.
        /// </summary>
        public bool Success { get; set; }
    }

    internal class DeleteProjectUseCase : IUseCaseInputBoundary<DeleteProjectRequest, DeleteProjectResponse>
    {
        private IProjectRepository _projectRepository;

        public DeleteProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<DeleteProjectResponse> outputBoundary, DeleteProjectRequest requestModel)
        {
            var result = _projectRepository.DeleteProject(Identity.FromString(requestModel.Id));

            outputBoundary.ResponseModel = new DeleteProjectResponse
            {
                Id = result.Deleted.ToString(),
                Success = result.IsSuccessfull,
            };
        }
    }
}
