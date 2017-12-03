using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Interfaces;
using System.Collections.Generic;

namespace ReqTrack.Domain.UseCases.Core.Project
{
    public class GetAllProjectsRequest
    {

    }

    public class GetAllProjectsResponse
    {
        public class ProjectInfo
        {
            /// <summary>
            /// <see cref="Entity{T}.Id"/>
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// <see cref="Domain.Core.Entities.Project.ProjectInfo.Name"/>.
            /// </summary>
            public string Name { get; set; }
        }

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

        private IEnumerable<GetAllProjectsResponse.ProjectInfo> convertEntityToResponseModel(IEnumerable<ProjectInfo> entities)
        {
            var rv = new List<GetAllProjectsResponse.ProjectInfo>();
            foreach(var e in entities)
            {
                rv.Add(new GetAllProjectsResponse.ProjectInfo
                {
                    Id = e.Id.ToString(),
                    Name = e.Name,
                });
            }
            return rv;
        }
    }
}
