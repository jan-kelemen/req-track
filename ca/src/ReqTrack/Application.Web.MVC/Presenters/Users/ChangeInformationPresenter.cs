using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class ChangeInformationPresenter : Presenter<ChangeInformationResponse, ChangeInformationViewModel>
    {
        public ChangeInformationPresenter(ISession session, ViewDataDictionary viewData, ModelStateDictionary modelState)
            : base(session, viewData, modelState)
        {
        }

        public override bool Accept(ChangeInformationResponse success)
        {
            Accept(success as ResponseModel);
            ViewModel = new ChangeInformationViewModel(UserId, UserName)
            {
                DisplayName = success.DisplayName,
                UserName = success.UserName,
            };
            return true;
        }


    }
}
