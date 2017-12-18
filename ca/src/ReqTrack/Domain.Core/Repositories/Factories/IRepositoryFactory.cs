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

        /// <summary>
        /// Provides th requirement repository.
        /// </summary>
        IRequirementRepository RequirementRepository { get; }
    }
}
