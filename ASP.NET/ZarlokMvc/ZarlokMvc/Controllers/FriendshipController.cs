using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    [Authorize]
    public class FriendshipController : Controller
    {
        private FriendshipModel friendshipModel = new FriendshipModel();
        private ProfileModel profileModel = new ProfileModel();
        
        //
        // GET: /Friendship
        public ActionResult Index()
        {
            return RedirectToAction("FriendsList", "Friendship", new { id = GetUserId() });
        }

        public ActionResult FriendsList(int? id)
        {
            if (id == null)
                id = GetUserId();
            if (friendshipModel.FriendsList((int)id, GetUserId()).Count() == 0)
            {
                ViewBag.Error = "Nie masz jeszcze żadnych znajomych";
            }
            return View(friendshipModel.FriendsList((int)id, GetUserId()));
        }

       [HttpGet]
        public ActionResult DeleteFriendship(int id)
        {
            ViewBag.Message = "";
            friendshipModel.DeleteFriend(id, GetUserId());
            return RedirectToAction("Index", "Friendship");
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

        [HttpGet]
        public ActionResult Details(int id)
        {
            ViewBag.Details = profileModel.Details().ToList();
            return View();
        }

        public ActionResult AcceptInvitation(int id)
        {
            if (id != GetUserId())
                friendshipModel.AcceptInvitation(GetUserId(), id);

            return RedirectToAction("Index", "Friendship", new { id = id });
        }

        public ActionResult RejectInvitation(int id)
        {
            if (id != GetUserId())
                friendshipModel.RejectInvitation(GetUserId(), id);

            return RedirectToAction("Index", "Friendship", new { id = id });
        }

        public ActionResult ReceivedInvitationsList()
        {
            return View(friendshipModel.ReceivedInvitations(GetUserId()));
        }

        public ActionResult SentInvitationsList()
        {
            return View(friendshipModel.SentInvitations(GetUserId()));
        }

        public ActionResult Invitations()
        {
            return View();
        }


    }
}