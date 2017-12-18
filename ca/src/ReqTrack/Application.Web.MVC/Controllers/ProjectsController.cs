using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Runtime.Core.Registry;
using ReqTrack.Domain.UseCases.Core.Factories;
using ReqTrack.Application.Web.MVC.Presenters.Factories;
using ReqTrack.Application.Web.MVC.ViewModels.Extensions.Projects;
using ReqTrack.Domain.UseCases.Core.Projects.RequestModels;

namespace Application.Web.MVC.Controllers
{
    public class ProjectsController : Controller
    {
        private IProjectPresenterFactory _presenterFactory => RegistryProxy.Get.GetFactory<IProjectPresenterFactory>();

        private IProjectUseCaseFactory _useCaseFactory => RegistryProxy.Get.GetFactory<IProjectUseCaseFactory>();

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectViewModel vm)
        {
            var request = vm.ToCreateRequestModel();
            var uc = _useCaseFactory.CreateProject();
            var presenter = _presenterFactory.CreateProject();
            uc.Execute(presenter, request);
            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ViewAllProjects()
        {
            var uc = _useCaseFactory.GetAllProjects();
            var presenter = _presenterFactory.GetAllProjects();
            uc.Execute(presenter, null);
            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ViewProject(string id)
        {
            var uc = _useCaseFactory.GetProject();
            var presenter = _presenterFactory.GetProject();
            uc.Execute(presenter, new GetProjectRequest { Id = id });
            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult EditProject(string id)
        {
            var uc = _useCaseFactory.GetProject();
            var presenter = _presenterFactory.UpdateProjectInitial();
            uc.Execute(presenter, new GetProjectRequest { Id = id });
            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult EditProject(ProjectViewModel vm)
        {
            var request = vm.ToUpdateRequestModel();
            var uc = _useCaseFactory.UpdateProject();
            var presenter = _presenterFactory.UpdateProject();
            uc.Execute(presenter, request);
            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult DeleteProject(string id)
        {
            var uc = _useCaseFactory.DeleteProject();
            var presenter = _presenterFactory.DeleteProject();
            uc.Execute(presenter, new DeleteProjectRequest { Id = id });
            return View(presenter.ViewModel);
        }
    }
}