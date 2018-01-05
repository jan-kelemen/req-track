using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}