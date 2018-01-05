using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels.Requirements
{
    public class ViewRequirementViewModel : ViewModel
    {
        public ViewRequirementViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string RequirementId { get; set; }

        [HiddenInput]
        public string UserId { get; set; }

        [DisplayName("Project name")]
        public string ProjectName { get; set; }

        [DisplayName("Last changed by")]
        public string UserDisplayName { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }
    }
}
