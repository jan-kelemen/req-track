using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class ViewProfilePresenter : Presenter<ViewProfileResponse, ViewProfileViewModel>
    {
        public ViewProfilePresenter(ISession session, ViewDataDictionary viewData, ModelStateDictionary modelState)
            : base(session, viewData, modelState)
        {
        }

        public override bool Accept(ViewProfileResponse success)
        {
            Accept(success as ResponseModel);
            ViewModel = new ViewProfileViewModel(UserId, UserName)
            {
                UserId = success.UserId,
                UserName = success.UserName,
                DisplayName = success.DisplayName,
                Projects = success.Projects.Select(x => new Tuple<string, string>(x.Identifier, x.Name)),
            };

            return true;
        }
    }
}
