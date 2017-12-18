using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects
{
    public static class RequestModelConversions
    {
        public static CreateProjectRequest ToCreateRequestModel(this ProjectInfoViewModel vm)
        {
            return new CreateProjectRequest
            {
                ProjectInfo = new ProjectInfo
                {
                    Id = null,
                    Name = vm.Name,
                },
            };
        }

        public static UpdateProjectRequest ToUpdateRequestModel(this ProjectInfoViewModel vm)
        {
            return new UpdateProjectRequest
            {
                ProjectInfo = new ProjectInfo
                {
                    Id = vm.Id,
                    Name = vm.Name,
                },
            };
        }
    }
}
