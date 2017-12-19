using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.Presenters.Factories;
using ReqTrack.Application.Web.MVC.ViewModels.Extensions;
using ReqTrack.Application.Web.MVC.ViewModels.Requirements;
using ReqTrack.Domain.UseCases.Core.Factories;
using ReqTrack.Domain.UseCases.Core.Requirements.RequestModels;
using ReqTrack.Runtime.Core.Registry;

namespace Application.Web.MVC.Controllers
{
    public class RequirementsController : Controller
    {
        private IRequirementPresenterFactory _presenterFactory => RegistryProxy.Get.GetFactory<IRequirementPresenterFactory>();

        private IRequirementUseCaseFactory _useCaseFactory => RegistryProxy.Get.GetFactory<IRequirementUseCaseFactory>();

        [HttpGet]
        public IActionResult CreateRequirement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRequirement(RequirementViewModel vm)
        {
            var request = new CreateRequirementRequest { Requirement = vm.ToBoundaryObject(), };
            var uc = _useCaseFactory.CreateRequirement();
            var presenter = _presenterFactory.CreateRequirement();
            uc.Execute(presenter, request);
            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ViewRequirement(string id)
        {
            var uc = _useCaseFactory.GetRequirement();
            var presenter = _presenterFactory.GetRequirement();
            uc.Execute(presenter, new GetRequirementRequest { Id = id });
            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult EditRequirement(string id)
        {
            var uc = _useCaseFactory.GetRequirement();
            var presenter = _presenterFactory.UpdateRequirementInitial();
            uc.Execute(presenter, new GetRequirementRequest { Id = id });
            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult EditRequirement(RequirementViewModel vm)
        {
            var request = new UpdateRequirementRequest { Requirement = vm.ToBoundaryObject(), };
            var uc = _useCaseFactory.UpdateRequirement();
            var presenter = _presenterFactory.UpdateRequirement();
            uc.Execute(presenter, request);
            return View(presenter.ViewModel);
        }

        [HttpPost]
        public IActionResult DeleteRequirement(string id)
        {
            var uc = _useCaseFactory.DeleteRequirement();
            var presenter = _presenterFactory.DeleteRequirement();
            uc.Execute(presenter, new DeleteRequirementRequest { Id = id });
            return View(presenter.ViewModel);
        }
    }
}