using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class AuthorizeUserPresenter : Presenter<AuthorizeUserResponse, LogInViewModel>
    {
        public override bool Accept(AuthorizeUserResponse success)
        {
            Response = success;
            return true;
        }
    }
}
