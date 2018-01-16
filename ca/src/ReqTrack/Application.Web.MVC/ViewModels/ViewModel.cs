using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels
{
    public class ViewModel
    {
        public ViewModel(string userId, string userName)
        {
            ApplicationUserId = userId;
            ApplicationUserName = userName;
        }

        [HiddenInput]
        public string ApplicationUserId { get; set; }

        [HiddenInput]
        public string ApplicationUserName { get; set; }
    }
}
