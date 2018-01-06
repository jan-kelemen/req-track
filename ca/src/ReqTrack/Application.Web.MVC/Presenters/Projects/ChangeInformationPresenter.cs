using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class ChangeInformationPresenter : Presenter<ChangeInformationResponse, ChangeInformationViewModel>
    {
        public ChangeInformationPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m) { }

        public override bool Accept(ChangeInformationResponse success)
        {
            Accept(success as ResponseModel);
            ViewModel = new ChangeInformationViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                Name = success.Name,
                ProjectName = success.Name,
                Description = success.Description,
            };
            return true;
        }
    }
}
