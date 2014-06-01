using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private ProfileModel profileModel = new ProfileModel();
        private FriendshipModel friendshipModel = new FriendshipModel();
       
        //
        // GET: /Profile/
        public ActionResult Index(string query)
        {
            ICollection<ProfileViewModel> profiles;
            
            if (query == null)
            {
                profiles = profileModel.Profiles(GetUserId());
                if (profileModel.Profiles(GetUserId()).Count == 0)
                {
                    ViewBag.Error = "Brak użytkowników do wyświetlenia";
                }

            }
            else
            {
                profiles = profileModel.ProfilesFind(query, GetUserId());
                if(profiles.Count == 0){
                    ViewBag.Error = "Brak użytkowników pasujących do wyszukiwania: "+query;
                }
            }
            return View(profiles);
        }

      
        public ActionResult AddFriend(int id)
        {
            if (id != GetUserId())
                friendshipModel.SendInvitation(GetUserId(), id);

            return RedirectToAction("Index", "Profile", new { id = id });
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
