using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.UseCases.Core.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReqTrack.Application.Web.MVC.Presenters.Factories
{
    public interface IProjectPresenterFactory
    {
        IPresenter<CreateProjectResponse, CreateProjectViewModel> CreateProject();

        IPresenter<DeleteProjectResponse, DeleteProjectViewModel> DeleteProject();

        IPresenter<GetAllProjectsResponse, GetAllProjectsViewModel> GetAllProjects();

        IPresenter<GetProjectResponse, GetProjectViewModel> GetProject();

        IPresenter<GetProjectResponse, UpdateProjectViewModel> UpdateProjectInitial();

        IPresenter<UpdateProjectResponse, UpdateProjectViewModel> UpdateProject();
    }
}
