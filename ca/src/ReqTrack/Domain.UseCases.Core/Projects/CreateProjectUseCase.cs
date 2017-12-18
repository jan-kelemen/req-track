using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Projects.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class CreateProjectUseCase : ICreateProjectUseCase
    {
        private IProjectRepository _projectRepository;

        public CreateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<CreateProjectResponse> outputBoundary, CreateProjectRequest requestModel)
        {
            requestModel.ProjectInfo.Id = _projectRepository.GenerateNewIdentity().ToBoundaryIdentity();
            var project = requestModel.ProjectInfo.ToDomainEntity();

            var result = _projectRepository.CreateProject(project);

            if (!result)
            {
                //TODO: handle error
            }

            var createdProject = result.Created;

            outputBoundary.ResponseModel = new CreateProjectResponse
            {
                ProjectInfo = result.Created.ToBoundaryObject(),
            };
        }
    }
}
