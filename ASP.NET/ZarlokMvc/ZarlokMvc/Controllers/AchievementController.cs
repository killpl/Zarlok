using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    [Authorize(Roles = "admin")]
    public class AchievementController : Controller
    {
        private AchievementModel achievementModel = new AchievementModel();

        //
        // GET: /Achievement
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ICollection<Achievement> achievements = achievementModel.Achievements();
            if (achievementModel.Achievements().Count == 0)
            {
                ViewBag.Error = "Nie dodałeś jeszcze żadngo osiągnięcia";
            }
            return View(achievements);
        }

        //
        // GET: /Achievement/CreateAchievement
        [HttpGet]
        public ActionResult CreateAchievement()
        {
            ViewBag.Message = "";
            return View();
        }

        //
        // POST: /Achievement/CreateAchievement
        [HttpPost]
        public ActionResult CreateAchievement(AchievementViewModel achievement)
        {
            if (ModelState.IsValid)
            {
                achievementModel.AddAchievement(achievement);
                return RedirectToAction("Index", "Achievement");
            }
            return View(achievement);
        }

        //
        // GET: /Achievement/EditAchievement
        [HttpGet]
        public ActionResult EditAchievement(int id)
        {
            ViewBag.Message = "";
            var achievement = achievementModel.GetAchievement(id);
            if (achievementModel.GetAchievement(id) == null)
            {
                ViewBag.Error = "Takie osiągnięcie nie istnieje";
                return View("Index");
            }
            return View(achievement);
        }

        //
        // POST: /Achievement/EditAchievement
        [HttpPost]
        public ActionResult EditAchievement(AchievementViewModel achievement)
        {
            if (ModelState.IsValid)
            {
                achievementModel.EditAchievement(achievement);
            }
            return RedirectToAction("Index", "Achievement");
        }

        [HttpGet]
        public ActionResult DeleteAchievement(int id)
        {
            ViewBag.Message = "";
            achievementModel.DeleteAchievement(id);
            return RedirectToAction("Index", "Achievement");
        }
    }
}