using Microsoft.AspNetCore.Http;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;

namespace ReqTrack.Application.Web.MVC.Factories
{
    public interface IUserPresenterFactory
    {
        IPresenter<AuthorizeUserResponse, LogInViewModel> AuthorizeUser(ISession session);
    }
}
