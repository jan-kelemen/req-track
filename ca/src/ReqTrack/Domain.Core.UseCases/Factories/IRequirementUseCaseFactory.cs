using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement;

namespace ReqTrack.Domain.Core.UseCases.Factories
{
    public interface IRequirementUseCaseFactory
    {
        IUseCase<AddRequirementRequest, AddRequirementResponse> AddRequirement { get; }

        IUseCase<ChangeRequirementInitialRequest, ChangeRequirementRequest, ChangeRequirementResponse> ChangeRequirement { get; }

        IUseCase<DeleteRequirementRequest, DeleteRequirementResponse> DeleteRequirement { get; }

        IUseCase<ViewRequirementRequest, ViewRequirementResponse> ViewRequirement { get; set; }
    }
}
