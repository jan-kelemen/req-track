using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.UseCases;
using ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase;

namespace ReqTrack.Application.Web.MVC.Presenters.UseCases
{
    public class ChangeUseCasePresenter : Presenter<ChangeUseCaseResponse, ChangeUseCaseViewModel>
    {
        public ChangeUseCasePresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(ChangeUseCaseResponse success)
        {
            base.Accept(success);
            ViewModel = new ChangeUseCaseViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                ProjectName = success.ProjectName,
                Title = success.Title,
                Note = success.Note,
                UseCaseId = success.UseCaseId,
                Steps = success.Steps?.ToArray() ?? new string[] {},
            };
            return true;
        }
    }
}
