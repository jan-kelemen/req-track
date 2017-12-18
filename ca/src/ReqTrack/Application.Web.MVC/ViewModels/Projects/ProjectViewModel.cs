using System.ComponentModel.DataAnnotations;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ProjectInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Project name")]
        public string Name { get; set; }
    }
}
