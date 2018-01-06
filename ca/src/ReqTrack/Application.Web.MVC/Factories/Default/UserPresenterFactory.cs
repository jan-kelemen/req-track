using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.Presenters.Users;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;

namespace ReqTrack.Application.Web.MVC.Factories.Default
{
    public class UserPresenterFactory : PresenterFactory, IUserPresenterFactory
    {
        public IPresenter<ViewProfileResponse, ViewProfileViewModel> ViewProfile(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ViewProfilePresenter(s, t, m);
        }

        public IPresenter<ChangeInformationResponse, ChangeInformationViewModel> ChangeInformation(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangeInformationPresenter(s, t, m);
        }

        public IPresenter<ChangePasswordResponse, ChangePasswordViewModel> ChangePassword(ISession s, ITempDataDictionary t, ModelStateDictionary m)
        {
            return new ChangePasswordPresenter(s, t, m);
        }
    }
}
