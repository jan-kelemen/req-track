using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Boundary.Responses;
using ReqTrack.Domain.Core.UseCases.Projects.ViewProject;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class ViewProjectPresenter : Presenter<ViewProjectResponse, ViewProjectViewModel>
    {
        public ViewProjectPresenter(ISession s, ITempDataDictionary t, ModelStateDictionary m) : base(s, t, m) { }

        public override bool Accept(ViewProjectResponse success)
        {
            var requirements = new Dictionary<string, IList<Tuple<string, string>>>();
            if (success.ShowRequirements)
            {
                foreach (var r in success.Requirements)
                {
                    var req = new Tuple<string, string>(r.Id, r.Title);
                    if (requirements.ContainsKey(r.Type))
                    {
                        requirements[r.Type].Add(req);
                    }
                    else
                    {
                        requirements.Add(r.Type, new List<Tuple<string, string>> { req });
                    }
                }
            }

            Accept(success as ResponseModel);
            ViewModel = new ViewProjectViewModel(UserId, UserName)
            {
                UserId = success.Author.Id,
                UserDisplayName = success.Author.Name,
                ProjectId = success.ProjectId,
                Name = success.Name,
                Description = success.Description,
                ShowRequirements = success.ShowRequirements,
                ShowUseCases = success.ShowUseCases,
                Requirements = requirements,
                UseCases = success.UseCases?.Select(x => new Tuple<string, string>(x.Id, x.Title)),
                CanChangeRequirements = success.Rights.CanChangeRequirements,
                CanChangeProjectRights = success.Rights.CanChangeProjectRights,
                CanChangeUseCases = success.Rights.CanChangeUseCases,
                IsAdministrator = success.Rights.IsAdministrator,
            };
            return true;
        }
    }
}
