﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.Presenters;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Users.ChangePassword;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;

namespace ReqTrack.Application.Web.MVC.Factories
{
    public interface IUserPresenterFactory
    {
        IPresenter<AuthorizeUserResponse, LogInViewModel> AuthorizeUser(ISession s, ViewDataDictionary v, ModelStateDictionary m);

        IPresenter<RegisterUserResponse, RegisterUserViewModel> RegisterUser(ISession s, ViewDataDictionary v, ModelStateDictionary m);

        IPresenter<ViewProfileResponse, ViewProfileViewModel> ViewProfile(ISession s, ViewDataDictionary v, ModelStateDictionary m);

        IPresenter<ChangeInformationResponse, ChangeInformationViewModel> ChangeInformation(ISession s, ViewDataDictionary v, ModelStateDictionary m);

        IPresenter<ChangePasswordResponse, ChangePasswordViewModel> ChangePassword(ISession s, ViewDataDictionary v, ModelStateDictionary m);
    }
}
