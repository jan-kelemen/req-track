using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.UseCases;
using ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase;

namespace ReqTrack.Application.Web.MVC.Presenters.UseCases
{
    public class AddUseCasePresenter : Presenter<AddUseCaseResponse, AddUseCaseViewModel>
    {
        public AddUseCasePresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(AddUseCaseResponse success)
        {
            base.Accept(success);
            ViewModel = new AddUseCaseViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                ProjectName = success.ProjectName,
                Steps = new string[] {},
            };
            return true;
        }
    }
}
