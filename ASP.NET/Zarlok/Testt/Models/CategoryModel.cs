using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Testt.ViewModels;

namespace Testt.Models
{
    public class CategoryModel : BaseModel
    {
        public ICollection<Category> Categories()
        {
            return db.Category.ToList();
        }

        public void AddCategory(CategoryViewModel category)
        {
            Category newCategory = new Category()
            {
                name = category.name
            };

            db.Category.Add(newCategory);
            db.SaveChanges();
        }

        public void EditCategory(CategoryViewModel category)
        {
            var query = db.Category.Where(x => x.id == category.id).First();

            query.name = category.name;
            db.SaveChanges();
        }

        public CategoryViewModel GetCategory(int id)
        {
            var query = db.Category.Find(id);
            if (query == null)
                return null;
            return new CategoryViewModel
            {
                name = query.name
            };
        }

        public void DeleteCategory(int id)
        {
            var query = db.Category.ToList().First(x => x.id == id);
            if (query != null)
            {
                db.Category.Remove(query);
                db.SaveChanges();
            }
        }
    }
}