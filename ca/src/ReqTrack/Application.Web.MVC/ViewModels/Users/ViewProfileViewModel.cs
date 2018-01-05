using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReqTrack.Application.Web.MVC.ViewModels.Users
{
    public class ViewProfileViewModel : ViewModel
    {
        public ViewProfileViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [DisplayName("User name")]
        public string UserName { get; set; }

        [DisplayName("Display name")]
        public string DisplayName { get; set; }

        [DisplayName("Projects")]
        public IEnumerable<Tuple<string, string>> Projects { get; set; }
    }
}
