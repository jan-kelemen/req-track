using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Interfaces;

namespace ReqTrack.Domain.UseCases.Core.Project
{
    public class UpdateProjectRequest
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

    public class UpdateProjectResponse
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

    internal class UpdateProjectUseCase : IUseCaseInputBoundary<UpdateProjectRequest, UpdateProjectResponse>
    {
        private IProjectRepository _projectRepository;

        public UpdateProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Execute(IUseCaseOutputBoundary<UpdateProjectResponse> outputBoundary, UpdateProjectRequest requestModel)
        {
            var readResult = _projectRepository.ReadProject(Identity.FromString(requestModel.Id));
            
            if(!readResult)
            {
                //TODO: handle read error
            }

            var readProject = readResult.Read;
            readProject.Name = requestModel.Name;

            var updateResult = _projectRepository.UpdateProject(readProject);

            if(!updateResult)
            {
                //TODO: handle update error
            }

            var updatedProject = updateResult.Updated;

            outputBoundary.ResponseModel = new UpdateProjectResponse
            {
                Id = updatedProject.Id.ToString(),
                Name = updatedProject.Name,
            };
        }
    }
}
