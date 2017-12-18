using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using System.Collections.Generic;
using System.Linq;

namespace ReqTrack.Domain.UseCases.Core.Projects
{
    public class GetAllProjectsRequest
    {

    }

    public class GetAllProjectsResponse
    {
        /// <summary>
        /// List of all projects.
        /// </summary>
        public IEnumerable<ProjectInfo> Projects { get; set; }
    }

    public class GetAllProjectsUseCase : IUseCaseInputBoundary<GetAllProjectsRequest, GetAllProjectsResponse>
    {
        private IProjectRepository _projectRepository;

        public GetAllProjectsUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<GetAllProjectsResponse> outputBoundary, GetAllProjectsRequest requestModel)
        {
            var result = _projectRepository.ReadAllProjects();

            if(!result)
            {
                //TODO: handle error
            }

            outputBoundary.ResponseModel = new GetAllProjectsResponse
            {
                Projects = result.Read.Select(e => e.ToBoundaryObject()),
            };
        }
    }
}
