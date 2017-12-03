using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Interfaces;

namespace ReqTrack.Domain.UseCases.Core.Project
{
    public class CreateProjectRequest
    {
        /// <summary>
        /// <see cref="ProjectInfo.Name"/>.
        /// </summary>
        public string Name { get; set; }
    }

    public class CreateProjectResponse
    {
        /// <summary>
        /// <see cref="Entity{T}.Id"/>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <see cref="ProjectInfo.Name"/>.
        /// </summary>
        public string Name { get; set; }
    }

    internal class CreateProjectUseCase : IUseCaseInputBoundary<CreateProjectRequest, CreateProjectResponse>
    {
        private IProjectRepository _projectRepository;

        public CreateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<CreateProjectResponse> outputBoundary, CreateProjectRequest requestModel)
        {
            var project = new ProjectInfo(Identity.BlankIdentity, requestModel.Name);

            var result = _projectRepository.CreateProject(project);

            if (!result)
            {
                //TODO: handle error
            }

            var createdProject = result.Created;

            outputBoundary.ResponseModel = new CreateProjectResponse
            {
                Id = createdProject.Id.ToString(),
                Name = createdProject.Name,
            };
        }
    }
}
