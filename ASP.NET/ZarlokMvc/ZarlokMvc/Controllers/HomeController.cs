using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;

namespace ZarlokMvc.Controllers
{
    public class HomeController : Controller
    {
        private ProfileAchievementModel profileAchievementModel = new ProfileAchievementModel();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            ViewBag.Ranking = profileAchievementModel.Ranking().Take(10).ToList();

            return View();
        }
    }
}
