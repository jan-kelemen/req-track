using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Requirements;

namespace ReqTrack.Domain.UseCases.Core.Factories
{
    /// <summary>
    /// Factory for use cases related to requirements.
    /// </summary>
    public interface IRequirementUseCaseFactory
    {
        /// <summary>
        /// Creates a use case for creating a new requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<CreateRequirementRequest, CreateRequirementResponse> CreateRequirement();

        /// <summary>
        /// Creates a use case for deleting a requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<DeleteRequirementRequest, DeleteRequirementResponse> DeleteRequirement();


        /// <summary>
        /// Creates a use case for reading a requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<GetRequirementRequest, GetRequirementResponse> GetRequirement();

        /// <summary>
        /// Creates a use case for updating a requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IUseCaseInputBoundary<UpdateRequirementRequest, UpdateRequirementResponse> UpdateRequirement();
    }
}
