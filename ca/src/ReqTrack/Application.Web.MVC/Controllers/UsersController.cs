using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.Factories;
using ReqTrack.Application.Web.MVC.Presenters.Users;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
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

            var presenter = new AuthorizeUserPresenter(HttpContext.Session, ViewData, ModelState);
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

            return Redirect("/");
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.SetString("UserId", null);
            HttpContext.Session.SetString("UserName", null);

            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
    }
}