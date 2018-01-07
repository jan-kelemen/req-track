using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement;

namespace ReqTrack.Application.Web.MVC.Presenters.Requirements
{
    public class ChangeRequirementPresenter : Presenter<ChangeRequirementResponse, ChangeRequirementViewModel>
    {
        public ChangeRequirementPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(ChangeRequirementResponse success)
        {
            base.Accept(success);
            ViewModel = new ChangeRequirementViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                Type = success.Type,
                Title = success.Title,
                Note = success.Note,
                RequirementId = success.RequirementId,
                ProjectName = success.ProjectName,
                Types = success.Types?.Select(x => new SelectListItem { Value = x, Text = x, }).ToList(),
            };

            return true;
        }
    }
}
