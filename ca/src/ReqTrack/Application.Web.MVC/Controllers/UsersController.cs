using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.Factories;
using ReqTrack.Application.Web.MVC.Factories.Default;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
using ReqTrack.Domain.Core.UseCases.Users.ChangeInformation;
using ReqTrack.Domain.Core.UseCases.Users.RegisterUser;
using ReqTrack.Domain.Core.UseCases.Users.ViewProfile;
using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserUseCaseFactory _userUseCaseFactory;

        private readonly IUserPresenterFactory _userPresenterFactory;

        public UsersController()
        {
            _userUseCaseFactory = RegistryProxy.Get.GetFactory<IUserUseCaseFactory>();
            _userPresenterFactory = new UserPresenterFactory();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View(new LogInViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public IActionResult LogIn(LogInViewModel vm)
        {
            var uc = _userUseCaseFactory.AuthorizeUser;
            var request = new AuthorizeUserRequest(null)
            {
                UserName = vm.UserName,
                Password = vm.Password,
            };

            var presenter = _userPresenterFactory.AuthorizeUser(HttpContext.Session, ViewData, ModelState);
            if (!uc.Execute(presenter, request)) { return View(vm); }

            HttpContext.Session.SetString("UserId", presenter.Response.UserId);
            HttpContext.Session.SetString("UserName", presenter.Response.DisplayName);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, vm.UserName),
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            var principal = new ClaimsPrincipal(userIdentity);
            HttpContext.SignInAsync(principal).Wait();

            return RedirectToAction(nameof(Index), new {id = presenter.Response.UserId});
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.SetString("UserId", null);
            HttpContext.Session.SetString("UserName", null);

            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterUser()
        {
            return View(new RegisterUserViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public IActionResult RegisterUser(RegisterUserViewModel vm)
        {
            var uc = _userUseCaseFactory.RegisterUser;
            var request = new RegisterUserRequest
            {
                UserName = vm.UserName,
                DisplayName = vm.DisplayName,
                Password = vm.Password,
                ConfirmedPassword = vm.ConfirmPassword,
            };

            var presenter = _userPresenterFactory.RegisterUser(HttpContext.Session, ViewData, ModelState);
            if(!uc.Execute(presenter, request)) { return View(vm); }

            return Redirect(nameof(LogIn));
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            var uc = _userUseCaseFactory.ViewProfile;
            var request = new ViewProfileRequest(HttpContext.Session.GetString("UserId"))
            {
                UserId = id,
            };

            var presenter = _userPresenterFactory.ViewProfile(HttpContext.Session, ViewData, ModelState);
            if(!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpGet]
        public IActionResult ChangeInformation()
        {
            var uc = _userUseCaseFactory.ChangeInformation;
            var request = new ChangeInformationInitialRequest(HttpContext.Session.GetString("UserId"))
            {
                UserId = HttpContext.Session.GetString("UserId"),
            };

            var presenter = _userPresenterFactory.ChangeInformation(HttpContext.Session, ViewData, ModelState);
            if(!uc.Execute(presenter, request)) { return NotFound(); }

            return View(presenter.ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangeInformation(ChangeInformationViewModel vm)
        {
            var uc = _userUseCaseFactory.ChangeInformation;
            var request = new ChangeInformationRequest(HttpContext.Session.GetString("UserId"))
            {
                UserId = vm.ApplicationUserId,
                DisplayName = vm.DisplayName,
            };

            var presenter = _userPresenterFactory.ChangeInformation(HttpContext.Session, ViewData, ModelState);
            if(!uc.Execute(presenter, request)) { return View(vm); }

            return RedirectToAction(nameof(Index), new { id = presenter.Response.UserId });
        }
    }
}