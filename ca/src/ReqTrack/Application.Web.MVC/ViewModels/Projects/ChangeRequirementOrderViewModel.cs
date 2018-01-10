using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ChangeRequirementOrderViewModel : ViewModel
    {
        public ChangeRequirementOrderViewModel() : this(null, null) { }

        public ChangeRequirementOrderViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string ProjectName { get; set; }

        [HiddenInput]
        public string Type { get; set; }

        public string[] RequirementIds { get; set; } = { };

        public string[] RequirementTitles { get; set; } = { };
    }
}
