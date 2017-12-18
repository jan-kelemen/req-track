using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
using System.Collections.Generic;

namespace ReqTrack.Application.Web.MVC.Presenters.Projects
{
    public class GetAllProjectsPresenter : Presenter<GetAllProjectsResponse, GetAllProjectsViewModel>
    {
        protected override GetAllProjectsViewModel CreateViewModel(GetAllProjectsResponse response)
        {
            var projects = new List<GetAllProjectsViewModel.Project>();
            foreach(var p in response.Projects)
            {
                projects.Add(new GetAllProjectsViewModel.Project
                {
                    Id = p.Id,
                    Name = p.Name,
                });
            }

            var vm = new GetAllProjectsViewModel
            {
                Projects = projects,
            };

            return vm;
        }
    }
}
