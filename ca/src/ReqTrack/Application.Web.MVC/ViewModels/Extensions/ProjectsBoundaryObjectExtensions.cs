using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions
{
    public static class ProjectBoundaryObjectExtensions
    {
        public static ProjectInfo ToBoundaryObject(this ProjectViewModel vm)
        {
            return new ProjectInfo
            {
                Id = vm.Id,
                Name = vm.Name,
            };
        }

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
