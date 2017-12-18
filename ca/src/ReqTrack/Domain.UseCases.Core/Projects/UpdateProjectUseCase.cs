using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Projects.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class UpdateProjectUseCase : IUpdateProjectUseCase
    {
        private IProjectRepository _projectRepository;

        public UpdateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<UpdateProjectResponse> outputBoundary, UpdateProjectRequest requestModel)
        {
            var projectToUpdate = requestModel.ProjectInfo.ToDomainEntity();
            var result = _projectRepository.UpdateProject(projectToUpdate);

            if(!result)
            {
                //TODO: handle update error
            }

            outputBoundary.ResponseModel = new UpdateProjectResponse
            {
                ProjectInfo = result.Updated.ToBoundaryObject(),
            };
        }
    }
}
