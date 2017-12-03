using ReqTrack.Application.Web.MVC.ViewModels.Project;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Project;
using ReqTrack.Domain.UseCases.Core.Project;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions
{
    public static class RequestModelConversions
    {
        public static CreateProjectRequest ToRequestModel(this CreateProjectViewModel vm)
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

        public static UpdateProjectRequest ToRequestModel(this UpdateProjectViewModel vm)
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
