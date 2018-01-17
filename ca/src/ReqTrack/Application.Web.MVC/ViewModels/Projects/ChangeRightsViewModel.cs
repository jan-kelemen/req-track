using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ChangeRightsViewModel : ViewModel
    {
        public ChangeRightsViewModel() : this(null, null) { }

        public ChangeRightsViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string ProjectName { get; set; }

        public string[] UserNames { get; set; } = { };

        public string[] CanView { get; set; } = { };

        public string[] CanChangeRequirements { get; set; } = { };

        public string[] CanChangeUseCases { get; set; } = { };

        public string[] CanChangeProjectRights { get; set; } = { };

        public string[] IsAdministrator { get; set; } = { };
    }
}
