using ReqTrack.Domain.Core.Entities;
using ReqTrack.Domain.Core.Entities.Requirements;
using ReqTrack.Domain.Core.Repositories.Results;

namespace ReqTrack.Domain.Core.Repositories
{
    /// <summary>
    /// Repository for persisting requirement entities.
    /// </summary>
    public interface IRequirementRepository : IRepository
    {
        /// <summary>
        /// Creates the given requirement.
        /// </summary>
        /// <param name="requirement">Requirement to be created.</param>
        /// <returns>Result of the operation, <see cref="CreateResult{T}"/>.</returns>
        CreateResult<Requirement> CreateRequirement(Requirement requirement);

        /// <summary>
        /// Reads the requirement with specified identity.
        /// </summary>
        /// <param name="id">Identifier of the requirement.</param>
        /// <returns>Result of the operation, <see cref="ReadResult{T}"/>.</returns>
        ReadResult<Requirement> ReadRequirement(Identity id);

        /// <summary>
        /// Updates the requirement with specified requirement.
        /// </summary>
        /// <param name="requirement">New requirement information.</param>
        /// <returns>Result of the operation, <see cref="UpdateResult{T}"/>.</returns>
        UpdateResult<Requirement> UpdateRequirement(Requirement requirement);

        /// <summary>
        /// Deletes the requirement with specified identifier.
        /// </summary>
        /// <param name="id">Identifier of the requirement.</param>
        /// <returns>Result of the operation, <see cref="DeleteResult{T}"/>.</returns>
        DeleteResult<Identity> DeleteRequirement(Identity id);

        /// <summary>
        /// Deletes the requirement.
        /// </summary>
        /// <param name="requirement">Requierment to be deleted.</param>
        /// <returns>Result of the operation, <see cref="DeleteResult{T}"/>.</returns>
        DeleteResult<Identity> DeleteRequirement(Requirement requirement);
    }
}
