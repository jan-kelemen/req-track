using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class RequirementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}