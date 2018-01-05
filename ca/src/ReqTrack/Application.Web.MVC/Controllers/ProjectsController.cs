using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}