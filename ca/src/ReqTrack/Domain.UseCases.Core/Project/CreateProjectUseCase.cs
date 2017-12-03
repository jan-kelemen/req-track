﻿using ReqTrack.Domain.Core.Repositories;
using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Project;

namespace ReqTrack.Domain.UseCases.Core.Project
{
    public class CreateProjectRequest
    {
        /// <summary>
        /// Project to be created. Identifier field is ignored.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
    }

    public class CreateProjectResponse
    {
        /// <summary>
        /// Created project.
        /// </summary>
        public ProjectInfo ProjectInfo { get; set; }
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
            var project = requestModel.ProjectInfo.ConvertToDomainEntity();

            var result = _projectRepository.CreateProject(project);

            if (!result)
            {
                //TODO: handle error
            }

            var createdProject = result.Created;

            outputBoundary.ResponseModel = new CreateProjectResponse
            {
                ProjectInfo = result.Created.ConvertToBoundaryEntity(),
            };
        }
    }
}
