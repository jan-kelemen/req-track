using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Domain.UseCases.Core.Factories
{
    /// <summary>
    /// Factory for use cases related to projects.
    /// </summary>
    public interface IProjectUseCaseFactory
    {
        /// <summary>
        /// Creates a use case for creating a new project.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<CreateProjectRequest, CreateProjectResponse> CreateProject();

        /// <summary>
        /// Creates a use case for deleting a project.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<DeleteProjectRequest, DeleteProjectResponse> DeleteProject();

        /// <summary>
        /// Creates a use case for reading all projects.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<GetAllProjectsRequest, GetAllProjectsResponse> GetAllProjects();

        /// <summary>
        /// Creates a use case for reading a project.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<GetProjectRequest, GetProjectResponse> GetProject();

        /// <summary>
        /// Creates a use case for updating a project.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<UpdateProjectRequest, UpdateProjectResponse> UpdateProject();
    }
}
