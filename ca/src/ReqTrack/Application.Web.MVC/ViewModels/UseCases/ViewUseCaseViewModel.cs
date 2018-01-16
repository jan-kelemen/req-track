using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels.UseCases
{
    public class ViewUseCaseViewModel : ViewModel
    {
        public ViewUseCaseViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string UseCaseId { get; set; }

        [HiddenInput]
        public string UserId { get; set; }

        [ReadOnly(true)]
        [DisplayName("Project name")]
        public string ProjectName { get; set; }

        [ReadOnly(true)]
        [DisplayName("Last changed by")]
        public string UserDisplayName { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Steps")]
        public string[] Steps { get; set; }

        [DisplayName("Note")]
        public string Note { get; set; }

        [HiddenInput]
        public bool CanChange { get; set; }
    }
}
