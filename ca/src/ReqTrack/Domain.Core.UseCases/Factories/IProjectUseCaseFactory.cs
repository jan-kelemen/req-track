using ReqTrack.Domain.Core.UseCases.Boundary.Interfaces;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRights;
using ReqTrack.Domain.Core.UseCases.Projects.CreateProject;
using ReqTrack.Domain.Core.UseCases.Projects.DeleteProject;
using ReqTrack.Domain.Core.UseCases.Projects.ViewProject;

namespace ReqTrack.Domain.Core.UseCases.Factories
{
    public interface IProjectUseCaseFactory
    {
        IUseCase<CreateProjectRequest, CreateProjectResponse> CreateProject { get; }

        IUseCase<ViewProjectRequest, ViewProjectResponse> ViewProject { get; }

        IUseCase<ChangeInformationInitialRequest, ChangeInformationRequest, ChangeInformationResponse> ChangeInformation { get; }

        IUseCase<ChangeRightsInitialRequest, ChangeRightsRequest, ChangeRightsResponse> ChangeRights { get; }

        IUseCase<DeleteProjectRequest, DeleteProjectResponse> DeleteProject { get; }
    }
}
