using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    [Authorize(Roles = "user")]
    public class EatenController : Controller
    {
        private EatenModel eatenModel = new EatenModel();
        private FoodModel foodModel = new FoodModel();

         //
        // GET: /Eaten
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ICollection<Eaten> eaten = eatenModel.Eaten(GetUserId());
            if (eatenModel.Eaten(GetUserId()).Count == 0)
            {
                ViewBag.Error = "Nie dodałeś jeszcze żadnego zjedzonego posiłku";
            }
            return View(eaten);
        }

        //
        // GET: /Eaten/CreateEaten
        [HttpGet]
        public ActionResult CreateEaten()
        {
            ViewBag.Message = "";
            ViewBag.Foods = foodModel.Foods();
            return View();
        }

        //
        // POST: /Eaten/CreateEaten
        [HttpPost]
        public ActionResult CreateEaten(EatenViewModel eaten)
        {
            if (ModelState.IsValid)
            {
                eaten.profileId = GetUserId();
                eatenModel.AddEaten(eaten);
                return RedirectToAction("Index", "Eaten");
            }
            ViewBag.Foods = foodModel.Foods();
            return View(eaten);
        }

        [HttpGet]
        public ActionResult DeleteEaten(int id)
        {
            ViewBag.Message = "";
            eatenModel.DeleteEaten(id, GetUserId());
            return RedirectToAction("Index", "Eaten");
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