using ReqTrack.Domain.UseCases.Core.Requirements.Interfaces;

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
        ICreateRequirementUseCase CreateRequirement();

        /// <summary>
        /// Creates a use case for deleting a requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IDeleteRequirementUseCase DeleteRequirement();


        /// <summary>
        /// Creates a use case for reading a requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IGetRequirementUseCase GetRequirement();

        /// <summary>
        /// Creates a use case for updating a requirement.
        /// </summary>
        /// <returns>The use case.</returns>
        IUpdateRequirementUseCase UpdateRequirement();
    }
}
