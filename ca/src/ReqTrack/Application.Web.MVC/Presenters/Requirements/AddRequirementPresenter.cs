using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement;

namespace ReqTrack.Application.Web.MVC.Presenters.Requirements
{
    public class AddRequirementPresenter : Presenter<AddRequirementResponse, AddRequirementViewModel>
    {
        public AddRequirementPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(AddRequirementResponse success)
        {
            base.Accept(success);
            ViewModel = new AddRequirementViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                ProjectName = success.ProjectName,
                Types = success.Types?.Select(x => new SelectListItem { Value = x, Text = x, })?.ToArray(),
            };
            return true;
        }
    }
}
