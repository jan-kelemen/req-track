using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Projects.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Projects
{

    public class GetProjectUseCase : IGetProjectUseCase
    {
        private IProjectRepository _projectRepository;

        public GetProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<GetProjectResponse> outputBoundary, GetProjectRequest requestModel)
        {
            var result = _projectRepository.ReadProject(Identity.FromString(requestModel.Id));

            if (!result)
            {
                //TODO: handle error
            }

            outputBoundary.ResponseModel = new GetProjectResponse
            {
                ProjectInfo = result.Read.ToBoundaryObject(),
            };
        }
    }
}
