﻿using Microsoft.AspNetCore.Mvc;

namespace ReqTrack.Application.Web.MVC.Controllers
{
    public class UseCasesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}