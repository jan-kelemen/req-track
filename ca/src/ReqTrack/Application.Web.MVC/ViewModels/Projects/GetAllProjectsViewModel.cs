using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class GetAllProjectsViewModel
    {
        public class Project
        {
            public string Id { get; set; }

            [Display(Name = "Project name")]
            public string Name { get; set; }
        }

        public IEnumerable<Project> Projects { get; set; }
    }
}
