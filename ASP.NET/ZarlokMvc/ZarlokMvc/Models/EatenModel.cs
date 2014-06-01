using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Models
{
    public class EatenModel : BaseModel
    {
        public ICollection<Eaten> Eaten(int id)
        {
            return db.Eaten.Where(x => x.profileId == id).OrderByDescending(x => x.date).ToList();
        }

        public void AddEaten(EatenViewModel eaten)
        {
            DateTime date = DateTime.Now;
            Eaten newEaten = new Eaten()
            {
                foodId = eaten.foodId,
                profileId = eaten.profileId,
                date = date
            };

            db.Eaten.Add(newEaten);
            db.SaveChanges();
        }

        public void DeleteEaten(int id, int userId)
        {
            var query = db.Eaten.ToList().First(x => x.id == id && x.profileId == userId);
            if (query != null)
            {
                db.Eaten.Remove(query);
                db.SaveChanges();
            }
        }
    }
}