using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class UpdateProjectRequest
    {
        /// <summary>
        /// Project to be updated.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }

    public class UpdateProjectResponse
    {
        /// <summary>
        /// Updated project.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }

    internal class UpdateProjectUseCase : IUseCaseInputBoundary<UpdateProjectRequest, UpdateProjectResponse>
    {
        private IProjectRepository _projectRepository;

        public UpdateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<UpdateProjectResponse> outputBoundary, UpdateProjectRequest requestModel)
        {
            var projectToUpdate = requestModel.ProjectInfo.ConvertToDomainEntity();
            var result = _projectRepository.UpdateProject(projectToUpdate);

            if(!result)
            {
                //TODO: handle update error
            }

            outputBoundary.ResponseModel = new UpdateProjectResponse
            {
                ProjectInfo = result.Updated.ConvertToBoundaryEntity(),
            };
        }
    }
}
