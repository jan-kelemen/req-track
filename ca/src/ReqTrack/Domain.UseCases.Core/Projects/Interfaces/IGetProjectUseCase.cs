using ReqTrack.Domain.UseCases.Core.Boundary.Interfaces;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;
using ReqTrack.Domain.UseCases.Core.Projects.ResponseModels;

namespace ReqTrack.Domain.UseCases.Core.Projects.Interfaces
{
    public interface IGetProjectUseCase : IUseCaseInputBoundary<GetProjectRequest, GetProjectResponse>
    {
    }
}
