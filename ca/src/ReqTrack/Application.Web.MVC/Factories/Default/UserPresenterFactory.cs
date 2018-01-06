using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.Presenters.Users;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    internal class UserPresenterFactory : IUserPresenterFactory
    {

        public IPresenter<AuthorizeUserResponse, LogInViewModel> AuthorizeUser(ISession s, ViewDataDictionary v, ModelStateDictionary m)
        {
            return new AuthorizeUserPresenter(s, v, m);
        }

        public IPresenter<RegisterUserResponse, RegisterUserViewModel> RegisterUser(ISession s, ViewDataDictionary v, ModelStateDictionary m)
        {
            return new RegisterUserPresenter(s, v, m);
        }

        public IPresenter<ViewProfileResponse, ViewProfileViewModel> ViewProfile(ISession s, ViewDataDictionary v, ModelStateDictionary m)
        {
            return new ViewProfilePresenter(s, v, m);
        }
    }
}
