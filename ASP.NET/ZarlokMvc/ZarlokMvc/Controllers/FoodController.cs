using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    public class FoodController : Controller
    {
        private FoodModel foodModel = new FoodModel();
        private CategoryModel categoryModel = new CategoryModel();

        //
        // GET: /Food
        [Authorize(Roles = "admin, user")]
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ICollection<Food> foods = foodModel.Foods();
            if (foodModel.Foods().Count == 0)
            {
                ViewBag.Error = "Nie dodałeś jeszcze żadnego jedzenia";
            }
            return View(foods);
        }

        //
        // GET: /Food/CreateFood
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateFood()
        {
            ViewBag.Message = "";
            ViewBag.Categories = categoryModel.Categories();
            return View();
        }

        //
        // POST: /Food/CreateFood
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateFood(FoodViewModel food)
        {
            if (ModelState.IsValid)
            {
                foodModel.AddFood(food);
                return RedirectToAction("Index", "Food");
            }
            ViewBag.Categories = categoryModel.Categories();
            return View(food);
        }

        //
        // GET: /Food/EditFood
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditFood(int id)
        {
            ViewBag.Message = "";
            var food = foodModel.GetFood(id);
            if (foodModel.GetFood(id) == null)
            {
                ViewBag.Error = "Takie jedzenie nie istnieje";
                return View("Index");
            }
            ViewBag.Categories = categoryModel.Categories();
            return View(food);
        }

        //
        // POST: /Food/EditFood
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditFood(FoodViewModel food)
        {
            if (ModelState.IsValid)
            {
                foodModel.EditFood(food);
            }
            ViewBag.Categories = categoryModel.Categories();
            return RedirectToAction("Index", "Food");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteFood(int id)
        {
            ViewBag.Message = "";
            foodModel.DeleteFood(id);
            return RedirectToAction("Index", "Food");
        }
    }
}