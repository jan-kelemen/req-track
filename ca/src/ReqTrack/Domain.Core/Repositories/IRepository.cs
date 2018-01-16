using ReqTrack.Domain.Core.Entities;

namespace ReqTrack.Domain.Core.Repositories
{
    /// <summary>
    /// Base interface for all repositories.
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Generates a new identity for the entity.
        /// </summary>
        /// <returns>New identity.</returns>
        Identity GenerateNewIdentity();
    }
}
