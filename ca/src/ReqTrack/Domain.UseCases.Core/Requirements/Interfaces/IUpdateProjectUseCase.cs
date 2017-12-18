using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Requirements.RequestModels;
using ReqTrack.Domain.UseCases.Core.Requirements.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Requirements.Interfaces
{
    public interface IUpdateRequirementUseCase : IUseCaseInputBoundary<UpdateRequirementRequest, UpdateRequirementResponse>
    {
    }
}