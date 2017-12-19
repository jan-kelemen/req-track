using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ProjectWithRequirementsViewModel : ProjectViewModel
    {
        public class Requirement
        {
            public string Id { get; set; }

            public string Type { get; set; }

            public string Title { get; set; }
        }

        public IEnumerable<Requirement> Requirements { get; set; }
    }
}
