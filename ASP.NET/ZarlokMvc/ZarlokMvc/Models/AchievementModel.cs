using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Models
{
    public class AchievementModel : BaseModel
    {
        public ICollection<Achievement> Achievements()
        {
            return db.Achievement.OrderBy(x => x.name).ToList();
        }

        public void AddAchievement(AchievementViewModel achievement)
        {
            Achievement newAchievement = new Achievement()
            {
                name = achievement.name
            };

            db.Achievement.Add(newAchievement);
            db.SaveChanges();
        }

        public void EditAchievement(AchievementViewModel achievement)
        {
            var query = db.Achievement.Where(x => x.id == achievement.id).First();

            query.name = achievement.name;
            db.SaveChanges();
        }

        public AchievementViewModel GetAchievement(int id)
        {
            var query = db.Achievement.Find(id);
            if (query == null)
                return null;
            return new AchievementViewModel
            {
                name = query.name
            };
        }

        public void DeleteAchievement(int id)
        {
            var query = db.Achievement.ToList().First(x => x.id == id);
            if (query != null)
            {
                db.Achievement.Remove(query);
                db.SaveChanges();
            }
        }
    }
}