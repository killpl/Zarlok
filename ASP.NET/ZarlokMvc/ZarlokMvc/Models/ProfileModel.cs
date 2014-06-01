using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Models
{
    public class ProfileModel : BaseModel
    {
       public ICollection<ProfileViewModel> Profiles(int id)
        {
           return db.Profile.Where(x => x.type != "admin" && x.id != id).Select(user => new ProfileViewModel
            {
                firstname = user.firstname,
                surname = user.surname,
                name = user.firstname + " " + user.surname,
                id = user.id,
                login = user.login,
                isFriend = db.Friendship.Where(u => (u.sender == id && u.receiver == user.id) || (u.sender == user.id && u.receiver == id)).Count() > 0
            }).OrderBy(x => x.login).ToList();
        }

       public ICollection<ProfileViewModel> ProfilesFind(string query, int id)
       {
           return db.Profile.Where(x => (x.firstname.Contains(query) || x.login.Contains(query) || x.surname.Contains(query)) && x.type != "admin" && x.id != id).Select(user => new ProfileViewModel
           {
               firstname = user.firstname,
               surname = user.surname,
               name = user.firstname + " " + user.surname,
               id = user.id,
               login = user.login,
               isFriend = db.Friendship.Where(u => (u.sender == id && u.receiver == user.id) || (u.sender == user.id && u.receiver == id)).Count() > 0
           }).OrderBy(x => x.login).ToList();
       }

        public void EditProfile(ProfileViewModel profile)
        {
            var query = db.Profile.Where(
                x => x.id == profile.id).First();

            query.firstname = profile.firstname;
            query.surname = profile.surname;

            db.SaveChanges();
        }

        public ProfileViewModel GetProfile(int id)
        {
            var query = db.Profile.Find(id);

            if (query == null)
                return null;

            return new ProfileViewModel
            {
                firstname = query.firstname,
                surname = query.surname
            };
        }

        public Profile GetProfile(string login)
        {
            return db.Profile.SingleOrDefault(x => x.login == login);
        }

         public ICollection<DetailsViewModel> Details()
        {
            return (from pa in db.ProfileAchievement
                    join a in db.Achievement on pa.achievementId equals a.id
                    join p in db.Profile on pa.profileId equals p.id
                    select new DetailsViewModel()
                    {
                        date = (from x in db.ProfileAchievement where pa.profileId == p.id select pa.date).FirstOrDefault(),
                        name = (from x in db.Achievement where pa.achievementId == a.id select a.name).FirstOrDefault()
                    }).ToList();
        }
    }
}