using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.ViewModels.Project;
using ReqTrack.Runtime.Core.Registry;
using ReqTrack.Domain.UseCases.Core.Factories;
using ReqTrack.Application.Web.MVC.Presenters.Project;
using ReqTrack.Application.Web.MVC.ViewModels.Extensions;

namespace Application.Web.MVC.Controllers
{
    public class ProjectsController : Controller
    {

        private IProjectUseCaseFactory _projectFactory => RegistryProxy.Get.GetFactory<IProjectUseCaseFactory>();

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel vm)
        {
            var request = vm.ToRequestModel();
            var uc = _projectFactory.CreateProject();
            var presenter = new CreateProjectPresenter();

            uc.Execute(presenter, request);

            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ViewAllProjects()
        {
            var uc = _projectFactory.GetAllProjects();
            var presenter = new GetAllProjectsPresenter();
            uc.Execute(presenter, null);
            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ViewProject(string id)
        {
            var uc = _projectFactory.GetProject();
            var presenter = new GetProjectPresenter();
            uc.Execute(presenter, new ReqTrack.Domain.UseCases.Core.Project.GetProjectRequest { Id = id });
            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult EditProject(UpdateProjectViewModel vm)
        {
            var request = vm.ToRequestModel();
            var uc = _projectFactory.UpdateProject();
            var presenter = new UpdateProjectPresenter();
            uc.Execute(presenter, request);
            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult DeleteProject(string id)
        {
            var uc = _projectFactory.DeleteProject();
            var presenter = new DeleteProjectPresenter();
            uc.Execute(presenter, new ReqTrack.Domain.UseCases.Core.Project.DeleteProjectRequest { Id = id });
            return View(presenter.ViewModel);
        }
    }
}