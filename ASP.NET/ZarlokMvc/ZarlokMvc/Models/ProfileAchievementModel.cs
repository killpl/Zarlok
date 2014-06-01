using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Models
{
    public class ProfileAchievementModel : BaseModel
    {
        public ICollection<ProfileAchievement> ProfileAchievements(int id)
        {
            return db.ProfileAchievement.Where(x => x.profileId == id).OrderByDescending(x => x.date).ToList();
        }

         public ICollection<RankingViewModel> Ranking()
         {
             return (from o in db.ProfileAchievement
                     group o by o.profileId into g
                     select new RankingViewModel()
                     {
                         ile = g.Count(),
                         nazwa = (from u in db.Profile where (int)g.Key == u.id select u.login).FirstOrDefault()
                     }).OrderByDescending(x => x.ile).Take(10).ToList();
         }
    }
}