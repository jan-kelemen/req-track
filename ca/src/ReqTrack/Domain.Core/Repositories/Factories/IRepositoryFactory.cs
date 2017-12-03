using System;
using System.Collections.Generic;
using System.Text;

namespace ReqTrack.Domain.Core.Repositories.Factories
{
    /// <summary>
    /// Factory for all repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Provides the project repository.
        /// </summary>
        IProjectRepository ProjectRepository { get; }
    }
}
