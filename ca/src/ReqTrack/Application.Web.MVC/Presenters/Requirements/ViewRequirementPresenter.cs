using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement;

namespace ReqTrack.Application.Web.MVC.Presenters.Requirements
{
    public class ViewRequirementPresenter : Presenter<ViewRequirementResponse, ViewRequirementViewModel>
    {
        public ViewRequirementPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m) { }

        public override bool Accept(ViewRequirementResponse success)
        {
            Accept(success as ResponseModel);
            ViewModel = new ViewRequirementViewModel(UserId, UserName)
            {
                ProjectId = success.Project.Id,
                UserId = success.Author.Id,
                Type = success.Type,
                Title = success.Title,
                RequirementId = success.RequirementId,
                Note = success.Note,
                ProjectName = success.Project.Name,
                UserDisplayName = success.Author.Name,
                CanChange = success.CanChange,
            };
            return true;
        }
    }
}
