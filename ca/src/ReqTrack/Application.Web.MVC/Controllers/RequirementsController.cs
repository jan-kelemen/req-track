using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.Factories;
using ReqTrack.Application.Web.MVC.Factories.Default;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.Requirements.AddRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ChangeRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.DeleteRequirement;
using ReqTrack.Domain.Core.UseCases.Requirements.ViewRequirement;
using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class RequirementsController : Controller
    {
        private readonly IRequirementUseCaseFactory _requirementUseCaseFactory;

        private readonly IRequirementPresenterFactory _requirementPresenterFactory;

        public RequirementsController()
        {
            _requirementUseCaseFactory = RegistryProxy.Get.GetFactory<IRequirementUseCaseFactory>();
            _requirementPresenterFactory = new RequirementPresenterFactory();
        }

        [HttpGet]
        public IActionResult Index(string projectId, string id)
        {
            var uc = _requirementUseCaseFactory.ViewRequirement;
            var request = new ViewRequirementRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = projectId,
                RequirementId = id,
            };

            var presenter = _requirementPresenterFactory.ViewRequirement(HttpContext.Session, TempData, ModelState);
            if(!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var uc = _requirementUseCaseFactory.AddRequirement;
            var request = new AddRequirementInitialRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = id,
            };

            var presenter = _requirementPresenterFactory.AddRequirement(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult Create(AddRequirementViewModel vm)
        {
            var uc = _requirementUseCaseFactory.AddRequirement;
            var request = new AddRequirementRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = vm.ProjectId,
                Type = vm.Type,
                Title = vm.Title,
                Note = vm.Note,
            };

            var presenter = _requirementPresenterFactory.AddRequirement(HttpContext.Session, TempData, ModelState);
            if(!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new
            {
                projectId = presenter.Response.ProjectId,
                id = presenter.Response.GivenId
            });
        }

        [HttpGet]
        public IActionResult Change(string projectId, string id)
        {
            var uc = _requirementUseCaseFactory.ChangeRequirement;
            var request = new ChangeRequirementInitialRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = projectId,
                RequirementId = id,
            };

            var presenter = _requirementPresenterFactory.ChangeRequirement(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult Change(ChangeRequirementViewModel vm)
        {
            var uc = _requirementUseCaseFactory.ChangeRequirement;
            var request = new ChangeRequirementRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = vm.ProjectId,
                RequirementId = vm.RequirementId,
                Type = vm.Type,
                Title = vm.Title,
                Note = vm.Note,
            };

            var presenter = _requirementPresenterFactory.ChangeRequirement(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new
            {
                projectId = presenter.Response.ProjectId,
                id = presenter.Response.RequirementId,
            });
        }

        [HttpGet]
        public IActionResult Delete(string projectId, string id)
        {
            var uc = _requirementUseCaseFactory.DeleteRequirement;
            var request = new DeleteRequirementRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = projectId,
                RequirementId = id,
            };

            var presenter = _requirementPresenterFactory.Default<DeleteRequirementResponse>(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return RedirectToAction(nameof(Index), "Projects",
                new
                {
                    id = projectId,
                    showRequirements = true,
                    showUseCases = false
                }
            );
        }
    }
}