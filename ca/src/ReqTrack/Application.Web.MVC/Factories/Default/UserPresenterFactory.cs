using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.Presenters.Users;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    internal class UserPresenterFactory : IUserPresenterFactory
    {

        public IPresenter<AuthorizeUserResponse, LogInViewModel> AuthorizeUser(ISession s, ViewDataDictionary v, ModelStateDictionary m)
        {
            return new AuthorizeUserPresenter(s, v, m);
        }
    }
}
