using Microsoft.AspNetCore.Mvc;
using ReqTrack.Application.Web.MVC.ViewModels.Users;
using ReqTrack.Domain.Core.UseCases.Factories;
using ReqTrack.Domain.Core.UseCases.Users.AuthorizeUser;
using ReqTrack.Runtime.Core.Registry;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserUseCaseFactory _userUseCaseFactory;

        public UsersController() : this(RegistryProxy.Get.GetFactory<IUserUseCaseFactory>())
        {
        }

        public UsersController(IUserUseCaseFactory userUseCaseFactory)
        {
            _userUseCaseFactory = userUseCaseFactory;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View(new LogInViewModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult LogIn(LogInViewModel vm)
        {
            var uc = _userUseCaseFactory.AuthorizeUser;
            var request = new AuthorizeUserRequest(null)
            {
                UserName = vm.UserName,
                Password = vm.Password,
            };

            uc.Execute(null, request);

            return View(vm);
        }
    }
}