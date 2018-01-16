using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeUseCaseOrder;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class ChangeUseCaseOrderPresenter : Presenter<ChangeUseCaseOrderResponse, ChangeUseCaseOrderViewModel>
    {
        public ChangeUseCaseOrderPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(ChangeUseCaseOrderResponse success)
        {
            base.Accept(success);
            ViewModel = new ChangeUseCaseOrderViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                ProjectName = success.Name,
            };

            if (success.UseCases != null)
            {
                ViewModel.UseCaseIds = (from u in success.UseCases select u.Id).ToArray();
                ViewModel.UseCaseTitles = (from u in success.UseCases select u.Title).ToArray();
            }

            return true;
        }
    }
}
