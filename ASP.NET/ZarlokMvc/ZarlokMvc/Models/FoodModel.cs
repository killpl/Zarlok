using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Models
{
    public class FoodModel : BaseModel
    {
        public ICollection<Food> Foods()
        {
            return db.Food.OrderBy(x => x.categoryId).ThenBy(x => x.name).ToList();
        }

        public void AddFood(FoodViewModel food)
        {
            Food newFood = new Food()
            {
                name = food.name,
                calories = food.calories,
                categoryId = food.categoryId
            };

            db.Food.Add(newFood);
            db.SaveChanges();
        }

        public void EditFood(FoodViewModel food)
        {
            var query = db.Food.Where(x => x.id == food.id).First();

            query.name = food.name;
            query.calories = food.calories;
            query.categoryId = food.categoryId;
            db.SaveChanges();
        }

        public FoodViewModel GetFood(int id)
        {
            var query = db.Food.Find(id);
            if (query == null)
                return null;
            return new FoodViewModel
            {
                name = query.name,
                calories = query.calories,
                categoryId = query.categoryId
            };
        }

        public void DeleteFood(int id)
        {
            var query = db.Food.ToList().First(x => x.id == id);
            if (query != null)
            {
                db.Food.Remove(query);
                db.SaveChanges();
            }
        }
    }
}