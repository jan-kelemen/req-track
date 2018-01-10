using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
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

            var rights = success.Rights.ToArray();

            ViewModel = new ChangeRightsViewModel(UserId, UserName)
            {
                ProjectId = success.ProjectId,
                ProjectName = success.Name,
                UserNames = (from r in rights select r.UserName).ToArray(),
                CanView = (from r in rights select r.CanViewProject).ToArray(),
                CanChangeRequirements = (from r in rights select r.CanChangeProjectRights).ToArray(),
                CanChangeUseCases = (from r in rights select r.CanChangeUseCases).ToArray(),
                CanChangeProjectRights = (from r in rights select r.CanChangeProjectRights).ToArray(),
                IsAdministrator = (from r in rights select r.IsAdministrator).ToArray(),
            };

            return true;
        }
    }
}
