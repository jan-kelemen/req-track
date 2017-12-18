using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects
{
    public static class ResponseModelConversions
    {
        public static ProjectInfoViewModel ToViewModel(this ProjectInfo projectInfo)
        {
            return new ProjectInfoViewModel
            {
                Id = projectInfo.Id,
                Name = projectInfo.Name,
            };
        }
    }
}
