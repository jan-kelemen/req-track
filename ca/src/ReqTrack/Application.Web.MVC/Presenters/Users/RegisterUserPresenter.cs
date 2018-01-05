using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class RegisterUserPresenter : Presenter<RegisterUserResponse, RegisterUserViewModel>
    {
        public RegisterUserPresenter(ISession session, ViewDataDictionary viewData, ModelStateDictionary modelState)
            : base(session, viewData, modelState)
        {
        }

        public override bool Accept(RegisterUserResponse success) => Accept(success as ResponseModel);
    }
}
