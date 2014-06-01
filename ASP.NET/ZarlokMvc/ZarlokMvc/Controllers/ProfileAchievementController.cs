using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;

namespace ZarlokMvc.Controllers
{
    [Authorize(Roles = "user")]
    public class ProfileAchievementController : Controller
    {
        private ProfileAchievementModel profileAchievementModel = new ProfileAchievementModel();
        //
        // GET: /ProfileAchievement/

        public ActionResult Index()
        {
            ViewBag.Message = "";

            ICollection<ProfileAchievement> profileAchievements = profileAchievementModel.ProfileAchievements(GetUserId());
            if (profileAchievementModel.ProfileAchievements(GetUserId()).Count == 0)
            {
                ViewBag.Error = "Nie masz jeszcze żadnych osiągnięć";
            }
            return View(profileAchievements);
        }

        public int GetUserId()
        {
            var userName = User.Identity.Name;
            using (UsersContext context = new UsersContext())
            {
                var user = context.GetUsers().First(m => m.login == userName);
                return user.id;
            }
        }
    }
}
