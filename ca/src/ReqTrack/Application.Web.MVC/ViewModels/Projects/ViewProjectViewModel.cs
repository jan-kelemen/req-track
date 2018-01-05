using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ViewProjectViewModel : ViewModel
    {
        public ViewProjectViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string UserId { get; set; }

        [HiddenInput]
        public bool ShowRequirements { get; set; }

        [HiddenInput]
        public bool ShowUseCases { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Last changed by")]
        public string UserDisplayName { get; set; }

        public IDictionary<string, IEnumerable<Tuple<string, string>>> Requirements { get; set; }

        public IEnumerable<Tuple<string, string>> UseCases { get; set; }
    }
}
