﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.Factories;
using ReqTrack.Application.Web.MVC.Factories.Default;
using ReqTrack.Application.Web.MVC.ViewModels.Projects;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.Projects.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Projects.CreateProject;
using ReqTrack.Domain.Core.UseCases.Projects.DeleteProject;
using ReqTrack.Domain.Core.UseCases.Projects.ViewProject;
using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectUseCaseFactory _projectUseCaseFactory;

        private readonly IProjectPresenterFactory _projectPresenterFactory;

        public ProjectsController()
        {
            _projectUseCaseFactory = RegistryProxy.Get.GetFactory<IProjectUseCaseFactory>();
            _projectPresenterFactory = new ProjectPresenterFactory();
        }

        [HttpGet]
        public IActionResult Index(string id, bool showRequirements = true, bool showUseCases = true)
        {
            var uc = _projectUseCaseFactory.ViewProject;
            var request = new ViewProjectRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = id,
                ShowRequirements = showRequirements,
                ShowUseCases = showUseCases,
            };

            var presenter = _projectPresenterFactory.ViewProject(HttpContext.Session, TempData, ModelState);
            if(!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ChangeInformation(string id)
        {
            var uc = _projectUseCaseFactory.ChangeInformation;
            var request = new ChangeInformationInitialRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = id,
            };

            var presenter = _projectPresenterFactory.ChangeInformation(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ChangeInformation(ChangeInformationViewModel vm)
        {
            var uc = _projectUseCaseFactory.ChangeInformation;
            var request = new ChangeInformationRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = vm.ProjectId,
                Name = vm.Name,
                Description = vm.Description,
            };

            var presenter = _projectPresenterFactory.ChangeInformation(HttpContext.Session, TempData, ModelState);
            if(!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new {id = vm.ProjectId});
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            return View(new CreateProjectViewModel(
                HttpContext.Session.GetString("UserId"),
                HttpContext.Session.GetString("UserName")
                )
            );
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateProject(CreateProjectViewModel vm)
        {
            var uc = _projectUseCaseFactory.CreateProject;
            var request = new CreateProjectRequest(HttpContext.Session.GetString("UserId"))
            {
                Name = vm.Name,
                Description = vm.Description,
            };

            var presenter = _projectPresenterFactory.Default<CreateProjectResponse>(HttpContext.Session, TempData, ModelState);
            if(!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new {id = presenter.Response.GivenId});
        }

        [HttpGet]
        public IActionResult DeleteProject(string id)
        {
            var uc = _projectUseCaseFactory.DeleteProject;
            var request = new DeleteProjectRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = id,
            };

            var presenter = _projectPresenterFactory.Default<DeleteProjectResponse>(HttpContext.Session, TempData, ModelState);
            if(!uc.Execute(presenter, request)) { return NotFound(); }

            return RedirectToAction(nameof(Index), "Users", new {id = HttpContext.Session.GetString("UserId")});
        }
    }
}