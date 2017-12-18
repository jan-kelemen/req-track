using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ProjectsViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
