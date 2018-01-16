using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class LogInPresenter : Presenter<AuthorizeUserResponse, LogInViewModel>
    {
        public LogInPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m) { }

        public override bool Accept(AuthorizeUserResponse success) => Accept(success as ResponseModel);
    }
}
