using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
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
