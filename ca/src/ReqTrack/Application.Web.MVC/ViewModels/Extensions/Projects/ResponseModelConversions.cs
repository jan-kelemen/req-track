using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects
{
    public static class ResponseModelConversions
    {
        public static ProjectViewModel ToViewModel(this ProjectInfo projectInfo)
        {
            return new ProjectViewModel
            {
                Id = projectInfo.Id,
                Name = projectInfo.Name,
            };
        }
    }
}
