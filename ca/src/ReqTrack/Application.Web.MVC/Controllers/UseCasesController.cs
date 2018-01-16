using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.Factories;
using ReqTrack.Application.Web.MVC.Factories.Default;
using ReqTrack.Application.Web.MVC.ViewModels.UseCases;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.UseCases.AddUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ChangeUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.DeleteUseCase;
using ReqTrack.Domain.Core.UseCases.UseCases.ViewUseCase;
using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class UseCasesController : Controller
    {
        private readonly IUseCaseUseCaseFactory _useCaseUseCaseFactory;

        private readonly IUseCasePresenterFactory _useCasePresenterFactory;

        public UseCasesController()
        {
            _useCaseUseCaseFactory = RegistryProxy.Get.GetFactory<IUseCaseUseCaseFactory>();
            _useCasePresenterFactory = new UseCasePresenterFactory();
        }

        [HttpGet]
        public IActionResult Index(string projectId, string id)
        {
            var uc = _useCaseUseCaseFactory.ViewUseCase;
            var request = new ViewUseCaseRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = projectId,
                UseCaseId = id,
            };

            var presenter = _useCasePresenterFactory.ViewUseCase(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult Create(string id)
        {
            var uc = _useCaseUseCaseFactory.AddUseCase;
            var request = new AddUseCaseInitialRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = id,
            };

            var presenter = _useCasePresenterFactory.AddUseCase(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult Create(AddUseCaseViewModel vm)
        {
            var uc = _useCaseUseCaseFactory.AddUseCase;
            var request = new AddUseCaseRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = vm.ProjectId,
                Title = vm.Title,
                Note = vm.Note,
                Steps = vm.Steps,
            };

            var presenter = _useCasePresenterFactory.AddUseCase(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new
            {
                projectId = presenter.Response.ProjectId,
                id = presenter.Response.GivenId
            });
        }

        [HttpGet]
        public IActionResult Change(string projectId, string id)
        {
            var uc = _useCaseUseCaseFactory.ChangeUseCase;
            var request = new ChangeUseCaseInitialRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = projectId,
                UseCaseId = id,
            };

            var presenter = _useCasePresenterFactory.ChangeUseCase(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult Change(ChangeUseCaseViewModel vm)
        {
            var uc = _useCaseUseCaseFactory.ChangeUseCase;
            var request = new ChangeUseCaseRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = vm.ProjectId,
                UseCaseId = vm.UseCaseId,
                Title = vm.Title,
                Note = vm.Note,
                Steps = vm.Steps,
            };

            var presenter = _useCasePresenterFactory.ChangeUseCase(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new
            {
                projectId = presenter.Response.ProjectId,
                id = presenter.Response.UseCaseId,
            });
        }

        [HttpGet]
        public IActionResult Delete(string projectId, string id)
        {
            var uc = _useCaseUseCaseFactory.DeleteUseCase;
            var request = new DeleteUseCaseRequest(HttpContext.Session.GetString("UserId"))
            {
                ProjectId = projectId,
                UseCaseId = id,
            };

            var presenter = _useCasePresenterFactory.Default<DeleteUseCaseResponse>(HttpContext.Session, TempData, ModelState);
            if (!uc.Execute(presenter, request)) { return NotFound(); }

            return RedirectToAction(nameof(Index), "Projects",
                new
                {
                    id = projectId,
                    showRequirements = false,
                    showTrue = false
                }
            );
        }
    }
}