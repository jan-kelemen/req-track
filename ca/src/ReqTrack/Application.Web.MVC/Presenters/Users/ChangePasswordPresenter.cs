using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class ChangePasswordPresenter : Presenter<ChangePasswordResponse, ChangePasswordViewModel>
    {
        public ChangePasswordPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m) { }

        public override bool Accept(ChangePasswordResponse success)
        {
            base.Accept(success);
            ViewModel = new ChangePasswordViewModel(UserId, UserName)
            {
                UserName = success.UserName,
            };
            return true;
        }
    }
}
