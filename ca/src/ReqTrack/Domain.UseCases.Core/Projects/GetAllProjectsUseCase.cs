using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Extensions;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using System.Collections.Generic;

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

    internal class GetAllProjectsUseCase : IUseCaseInputBoundary<GetAllProjectsRequest, GetAllProjectsResponse>
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
                Projects = convertEntityToResponseModel(result.Read),
            };
        }

        private IEnumerable<ProjectInfo> convertEntityToResponseModel(IEnumerable<Domain.Core.Entities.Projects.ProjectInfo> entities)
        {
            var rv = new List<ProjectInfo>();
            foreach(var e in entities)
            {
                rv.Add(e.ConvertToBoundaryEntity());
            }
            return rv;
        }
    }
}
