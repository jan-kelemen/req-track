using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Boundary.Objects.Requirements;

namespace ReqTrack.Application.Web.MVC.ViewModels.Extensions
{
    public static class RequirementBoundaryObjectExtensions
    {
        public static Requirement ToBoundaryObject(this RequirementViewModel vm) => new Requirement
        {
            Id = vm.Id,
            Title = vm.Title,
            Type = vm.Type,
            Details = vm.Details,
            ProjectId = vm.ProjectId,
        };

        public static RequirementViewModel ToViewModel(this Requirement requirement) => new RequirementViewModel
        {
            Id = requirement.Id,
            Title = requirement.Title,
            Type = requirement.Type,
            Details = requirement.Details,
            ProjectId = requirement.ProjectId,
        };
    }
}
