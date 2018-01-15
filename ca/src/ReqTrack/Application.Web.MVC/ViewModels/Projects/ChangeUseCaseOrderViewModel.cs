using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.ViewModels.Projects
{
    public class ChangeUseCaseOrderViewModel : ViewModel
    {
        public ChangeUseCaseOrderViewModel() : this(null, null) { }

        public ChangeUseCaseOrderViewModel(string userId, string userName) : base(userId, userName)
        {
        }

        [HiddenInput]
        public string ProjectId { get; set; }

        [HiddenInput]
        public string ProjectName { get; set; }

        public string[] UseCaseIds { get; set; } = { };

        public string[] UseCaseTitles { get; set; } = { };
    }
}
