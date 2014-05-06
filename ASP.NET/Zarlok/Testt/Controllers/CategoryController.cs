using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testt.Models;
using Testt.ViewModels;

namespace Zarlok.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryModel categoryModel = new CategoryModel();

        //
        // GET: /Category
        public ActionResult Index()
        {
            ViewBag.Message = "";

            ICollection<Category> categories = categoryModel.Categories();
            if (categoryModel.Categories().Count == 0)
            {
                ViewBag.Error = "Nie dodałeś jeszcze żadnej kategorii";
            }
            return View(categories);
        }

        //
        // GET: /Category/CreateCategory
        [HttpGet]
        public ActionResult CreateCategory()
        {
            ViewBag.Message = "";
            return View();
        }

        //
        // POST: /Category/CreateCategory
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
        [HttpPost]
        public ActionResult EditCategory(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                categoryModel.EditCategory(category);
            }
            return RedirectToAction("Index", "Category");
        }

       [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            ViewBag.Message = "";
            categoryModel.DeleteCategory(id);
            return RedirectToAction("Index", "Category");
        }
    }
}