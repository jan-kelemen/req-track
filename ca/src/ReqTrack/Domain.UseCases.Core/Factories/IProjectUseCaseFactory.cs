using ReqTrack.Domain.UseCases.Core.Projects.Interfaces;

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
        ICreateProjectUseCase CreateProject();

        /// <summary>
        /// Creates a use case for deleting a project.
        /// </summary>
        /// <returns>The use case.</returns>
        IDeleteProjectUseCase DeleteProject();

        /// <summary>
        /// Creates a use case for reading all projects.
        /// </summary>
        /// <returns>The use case.</returns>
        IGetAllProjectsUseCase GetAllProjects();

        /// <summary>
        /// Creates a use case for reading a project.
        /// </summary>
        /// <returns>The use case.</returns>
        IGetProjectUseCase GetProject();

        /// <summary>
        /// Creates a use case for updating a project.
        /// </summary>
        /// <returns>The use case.</returns>
        IUpdateProjectUseCase UpdateProject();

        IGetProjectRequirementsUseCase GetProjectRequirements();
    }
}
