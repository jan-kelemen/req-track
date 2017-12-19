using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Projects.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class GetProjectRequirementsUseCase : IGetProjectRequirementsUseCase
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectRequirementsUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<GetProjectRequirementsResponse> outputBoundary, GetProjectRequirementsRequest requestModel)
        {
            var result = _projectRepository.ReadProjectRequirements(requestModel.Id.ToDomainIdentity());

            if (!result)
            {
                //TODO: handle error
            }

            outputBoundary.ResponseModel = new GetProjectRequirementsResponse
            {
                Project = result.Read.ToBoundaryObject(),
            };
        }
    }
}
