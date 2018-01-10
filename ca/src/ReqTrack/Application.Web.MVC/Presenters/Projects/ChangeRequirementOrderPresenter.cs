using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRequirementOrder;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class ChangeRequirementOrderPresenter : Presenter<ChangeRequirementOrderResponse, ChangeRequirementOrderViewModel>
    {
        public ChangeRequirementOrderPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(ChangeRequirementOrderResponse success)
        {
            base.Accept(success);
            ViewModel = new ChangeRequirementOrderViewModel
            {
                ProjectId = success.ProjectId,
                ProjectName = success.Name,
                Type = success.Type,

            };

            if (success.Requirements != null)
            {
                ViewModel.RequirementIds = (from r in success.Requirements select r.Id).ToArray();
                ViewModel.RequirementTitles = (from r in success.Requirements select r.Title).ToArray();
            }
            return true;
        }
    }
}
