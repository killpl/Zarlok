using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Models
{
	public class FriendshipModel : BaseModel
	{
        public void SendInvitation(int senderId, int receiverId)
        {
            if(!db.Friendship.Any(f => (f.sender == senderId && f.receiver == receiverId) || (f.sender==receiverId && f.receiver==senderId)))
            {
                var request = db.Request.FirstOrDefault(r => r.sender == senderId && r.receiver == receiverId);

                if(request==null)
                {
                    var response = db.Request.FirstOrDefault(r => r.sender == receiverId && r.receiver == senderId);

                    if(response!=null)
                    {
                        var friend = new Friendship();
                        friend.sender = senderId;
                        friend.receiver = receiverId;

                        db.Friendship.Add(friend);
                        db.Request.Remove(response);
                        
                        db.SaveChanges();
                    }
                    else 
                    {
                        request = new Request();
                        request.receiver = receiverId;
                        request.sender = senderId;
                        request.sent = DateTime.Now;
                        db.Request.Add(request);

                        db.SaveChanges();
                    }
                }
            }
        }

        public void AcceptInvitation(int userId, int invitedId)
        {
            var request = db.Request.FirstOrDefault(r => r.sender == invitedId && r.receiver == userId);

            if (request != null)
            {
                var friend = new Friendship();
                friend.sender = invitedId;
                friend.receiver = userId;

                db.Friendship.Add(friend);
                db.Request.Remove(request);

                db.SaveChanges();
            }
        }

        public void RejectInvitation(int userId, int invitedId) 
        {
            var request = db.Request.FirstOrDefault(r => (r.sender == invitedId && r.receiver == userId) || (r.receiver == invitedId && r.sender == userId));

            if (request != null)
            {
                db.Request.Remove(request);

                db.SaveChanges();
            }        
        }

        public void DeleteFriend(int userId, int friendId) {
            var friend = db.Friendship.FirstOrDefault(f => (f.sender == userId && f.receiver == friendId) || (f.receiver == userId && f.sender == friendId));
            if (friend != null)
            {
                db.Friendship.Remove(friend);
                db.SaveChanges();
            }
        }

        public IEnumerable<FriendsListViewModel> FriendsList(int userId, int currentUserId) {
            List<int> keys = db.Friendship.Where(f => f.sender == userId).Select(f => f.receiver).ToList();
             keys = keys.Concat(db.Friendship.Where(f => f.receiver == userId).Select(f => f.sender).ToList()).ToList();
             return db.Profile.Where(f => keys.Contains(f.id)).Select(f =>
                 new FriendsListViewModel
                 {
                     firstName = f.firstname,
                     lastName = f.surname,
                     login = f.login,
                     name = f.firstname + " " + f.surname,
                     userId = f.id,
                     isFriend = db.Friendship.Where(u => (u.sender == currentUserId && u.receiver == f.id) || (u.sender == f.id && u.receiver == currentUserId)).Count() > 0,
                 }
             );
        }

        public IEnumerable<InvitationListViewModel> ReceivedInvitations(int userID)
        {
            return db.Request.Where(r => r.receiver == userID).Select(r => new InvitationListViewModel
            {
                senderId = r.sender,
                firstName = r.Profile1.firstname,
                lastName = r.Profile1.surname,
                login = r.Profile1.login,
                name = r.Profile1.firstname + " " + r.Profile1.surname,
                invitationDate = r.sent
            });
        }

        public IEnumerable<InvitationListViewModel> SentInvitations(int userID)
        {
            return db.Request.Where(r => r.sender == userID).Select(r => new InvitationListViewModel
            {
                senderId = r.receiver,
                firstName = r.Profile.firstname,
                lastName = r.Profile.surname,
                login = r.Profile.login,
                name = r.Profile.firstname + " " + r.Profile.surname,
                invitationDate = r.sent
            });
        }
    }
}