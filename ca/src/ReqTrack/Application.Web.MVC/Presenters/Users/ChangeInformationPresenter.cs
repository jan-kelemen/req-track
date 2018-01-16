using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;

namespace ReqTrack.Application.Web.MVC.Presenters.Users
{
    public class ChangeInformationPresenter : Presenter<ChangeInformationResponse, ChangeInformationViewModel>
    {
        public ChangeInformationPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s,t,m) { }

        public override bool Accept(ChangeInformationResponse success)
        {
            base.Accept(success);
            ViewModel = new ChangeInformationViewModel(UserId, UserName)
            {
                DisplayName = success.DisplayName,
                UserName = success.UserName,
            };
            return true;
        }


    }
}
