using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class ChangePasswordPresenter : Presenter<ChangePasswordResponse, ChangePasswordViewModel>
    {
        public ChangePasswordPresenter(ISession session, ViewDataDictionary viewData, ModelStateDictionary modelState)
            : base(session, viewData, modelState)
        {
        }

        public override bool Accept(ChangePasswordResponse success)
        {
            Accept(success as ResponseModel);
            ViewModel = new ChangePasswordViewModel(UserId, UserName);
            return true;
        }
    }
}
