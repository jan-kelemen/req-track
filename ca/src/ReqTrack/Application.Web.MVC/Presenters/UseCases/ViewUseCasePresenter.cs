using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.UseCases;
using ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase;

namespace ReqTrack.Application.Web.MVC.Presenters.UseCases
{
    public class ViewUseCasePresenter : Presenter<ViewUseCaseResponse, ViewUseCaseViewModel>
    {
        public ViewUseCasePresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(ViewUseCaseResponse success)
        {
            base.Accept(success);
            ViewModel = new ViewUseCaseViewModel(UserId, UserName)
            {
                ProjectId = success.Project.Id,
                UserId = success.Author.Id,
                Title = success.Title,
                Note = success.Note,
                Steps = success.Steps?.ToArray() ?? new string[] { },
                ProjectName = success.Project.Name,
                CanChange = success.CanChange,
                UseCaseId = success.UseCaseId,
                UserDisplayName = success.Author.Name
            };

            return true;
        }
    }
}
