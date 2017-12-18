using System.Collections.Generic;

namespace ReqTrack.Application.Web.MVC.ViewModels.Requirements
{
    public class RequirementViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public string Details { get; set; }

        public string ProjectId { get; set; }

        public IEnumerable<string> RequirementTypes { get; set; }
    }
}
