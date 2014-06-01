using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryModel categoryModel = new CategoryModel();

        //
        // GET: /Category
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ICollection<Category> categories = categoryModel.Categories();
            if (categoryModel.Categories().Count == 0)
            {
                ViewBag.Error = "Brak kategorii do wyświetlenia";
            }
            return View(categories);
        }

        //
        // GET: /Category/CreateCategory
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateCategory()
        {
            ViewBag.Message = "";
            return View();
        }

        //
        // POST: /Category/CreateCategory
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateCategory(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                categoryModel.AddCategory(category);
                return RedirectToAction("Index", "Category");
            }
            return View(category);
        }

        //
        // GET: /Category/EditCategory
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            ViewBag.Message = "";
            var category = categoryModel.GetCategory(id);
            if (categoryModel.GetCategory(id) == null)
            {
                ViewBag.Error = "Taka kategoria nie istnieje";
                return View("Index");
            }
            return View(category);
        }

        //
        // POST: /Category/EditCategory
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                categoryModel.EditCategory(category);
            }
            return RedirectToAction("Index", "Category");
        }

       [Authorize(Roles = "admin")]
       [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            ViewBag.Message = "";
            categoryModel.DeleteCategory(id);
            return RedirectToAction("Index", "Category");
        }
    }
}