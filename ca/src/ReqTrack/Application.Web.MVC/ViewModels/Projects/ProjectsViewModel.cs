using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class GetAllProjectsViewModel
    {
        public IEnumerable<ProjectInfoViewModel> Projects { get; set; }
    }
}
