using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Interfaces;


namespace ReqTrack.Domain.UseCases.Core.Project
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
        /// <see cref="Entity{T}.Id"/>
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <see cref="ProjectInfo.Name"/>
        /// </summary>
        public string Name { get; set; }
    }

    internal class GetProjectUseCase : IUseCaseInputBoundary<GetProjectRequest, GetProjectResponse>
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

            var project = result.Read;

            outputBoundary.ResponseModel = new GetProjectResponse
            {
                Id = project.Id.ToString(),
                Name = project.Name,
            };
        }
    }
}
