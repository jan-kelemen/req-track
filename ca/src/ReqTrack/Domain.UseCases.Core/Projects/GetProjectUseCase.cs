using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class GetProjectRequest
    {
        /// <summary>
        /// <see cref="Entity{T}.Id"/>
        /// </summary>
        public string Id { get; set; }
    }

    public class GetProjectResponse
    {
        /// <summary>
        /// Read project.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }

    public class GetProjectUseCase : IUseCaseInputBoundary<GetProjectRequest, GetProjectResponse>
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
