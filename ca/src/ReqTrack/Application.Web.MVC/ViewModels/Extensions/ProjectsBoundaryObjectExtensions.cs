using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Projects;
using System.Linq;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions
{
    public static class ProjectBoundaryObjectExtensions
    {
        public static ProjectInfo ToBoundaryObject(this ProjectViewModel vm) => new ProjectInfo
        {
            Id = vm.Id,
            Name = vm.Name,
        };

        public static ProjectViewModel ToViewModel(this ProjectInfo projectInfo) => new ProjectViewModel
        {
            Id = projectInfo.Id,
            Name = projectInfo.Name,
        };

        public static ProjectWithRequirements ToBoundaryObject(this ProjectWithRequirementsViewModel vm) => new ProjectWithRequirements
        {
            Id = vm.Id,
            Name = vm.Name,
            Requirements = vm.Requirements.Select(r => r.ToBundaryObject()),
        };

        public static ProjectWithRequirementsViewModel ToViewModel(this ProjectWithRequirements projectWithRequirements) => new ProjectWithRequirementsViewModel
        {
            Id = projectWithRequirements.Id,
            Name = projectWithRequirements.Name,
            Requirements = projectWithRequirements.Requirements.Select(r => r.ToViewModel()),
        }; 

        private static ProjectWithRequirements.Requirement ToBundaryObject(this ProjectWithRequirementsViewModel.Requirement requirement) => new ProjectWithRequirements.Requirement
        {
            Id = requirement.Id,
            Title = requirement.Title,
            Type = requirement.Type,
        };

        private static ProjectWithRequirementsViewModel.Requirement ToViewModel(this ProjectWithRequirements.Requirement requirement) => new ProjectWithRequirementsViewModel.Requirement
        {
            Id = requirement.Id,
            Title = requirement.Title,
            Type = requirement.Type,
        };
    }
}
