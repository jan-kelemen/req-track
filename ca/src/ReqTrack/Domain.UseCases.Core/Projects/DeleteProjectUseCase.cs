using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class DeleteProjectRequest
    {
        /// <summary>
        /// Identifier of the project to be deleted, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteProjectResponse
    {
        /// <summary>
        /// Identifier of the deleted project, <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteProjectUseCase : IUseCaseInputBoundary<DeleteProjectRequest, DeleteProjectResponse>
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
            };
        }
    }
}
