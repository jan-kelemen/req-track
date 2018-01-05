﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class AuthorizeUserPresenter : Presenter<AuthorizeUserResponse, LogInViewModel>
    {
        public AuthorizeUserPresenter(ISession session, ViewDataDictionary viewData, ModelStateDictionary modelState)
            : base(session, viewData, modelState)
        {
        }

        public override bool Accept(AuthorizeUserResponse success) => Accept(success as ResponseModel);
    }
}
