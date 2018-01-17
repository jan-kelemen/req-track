using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Projects;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeRights;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class ChangeRightsPresenter : Presenter<ChangeRightsResponse, ChangeRightsViewModel>
    {
        public ChangeRightsPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m)
        {
        }

        public override bool Accept(ChangeRightsResponse success)
        {
            base.Accept(success);

            var rights = success.Rights?.ToArray() ?? new ProjectRights[0];

            ViewModel = new ChangeRightsViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                ProjectName = success.Name,
                UserNames = (from r in rights select r.UserName).ToArray(),
                CanView = (from r in rights select r.CanViewProject ? "y" : "n").ToArray(),
                CanChangeRequirements = (from r in rights select r.CanChangeProjectRights ? "y" : "n").ToArray(),
                CanChangeUseCases = (from r in rights select r.CanChangeUseCases ? "y" : "n").ToArray(),
                CanChangeProjectRights = (from r in rights select r.CanChangeProjectRights ? "y" : "n").ToArray(),
                IsAdministrator = (from r in rights select r.IsAdministrator ? "y" : "n").ToArray(),
            };

            return true;
        }
    }
}
