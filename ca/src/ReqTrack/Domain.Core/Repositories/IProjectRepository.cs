﻿using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Project;
using ReqTrack.Domain.Core.Repositories.Results;
using System.Collections.Generic;

namespace ReqTrack.Domain.Core.Repositories
{
    /// <summary>
    /// Repository for persisting project entities.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Creates the given project.
        /// </summary>
        /// <param name="projectInfo">Project to be created.</param>
        /// <returns>Result of the operation, <see cref="CreateResult{T}"/>.</returns>
        CreateResult<ProjectInfo> CreateProject(ProjectInfo projectInfo);

        /// <summary>
        /// Reads all projects.
        /// </summary>
        /// <returns>Result of the operation, <see cref="ReadResult{T}"/>.</returns>
        ReadResult<IEnumerable<ProjectInfo>> ReadAllProjects();

        /// <summary>
        /// Reads the project with specified identity.
        /// </summary>
        /// <param name="id">Identifier of the project.</param>
        /// <returns>Result of the operation, <see cref="ReadResult{T}"/>.</returns>
        ReadResult<ProjectInfo> ReadProject(Identity id);

        /// <summary>
        /// Updates the project with specified project information.
        /// </summary>
        /// <param name="projectInfo">New project information.</param>
        /// <returns>Result of the operation, <see cref="UpdateResult{T}"/>.</returns>
        UpdateResult<ProjectInfo> UpdateProject(ProjectInfo projectInfo);

        /// <summary>
        /// Deletes the project with specified identifier.
        /// </summary>
        /// <param name="id">Identifier of the project.</param>
        /// <returns>Result of the operation, <see cref="DeleteResult{T}"/>.</returns>
        DeleteResult<Identity> DeleteProject(Identity id);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="id">Identifier of the project.</param>
        /// <returns>Result of the operation, <see cref="DeleteResult{T}"/>.</returns>
        DeleteResult<Identity> DeleteProject(ProjectInfo projectInfo);
    }
}