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

        public string[] UserNames { get; set; }

        public bool[] CanView { get; set; }

        public bool[] CanChangeRequirements { get; set; }

        public bool[] CanChangeUseCases { get; set; }

        public bool[] CanChangeProjectRights { get; set; }

        public bool[] IsAdministrator { get; set; }
    }
}
