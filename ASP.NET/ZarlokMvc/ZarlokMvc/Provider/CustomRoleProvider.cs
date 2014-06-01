using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ZarlokMvc.Models;

namespace ZarlokMvc.Provider
{
    public class CustomRoleProvider : RoleProvider
    {
        private string[] roles = new string[] { "admin", "user" };

        public override bool IsUserInRole(string username, string roleName)
        {
            using (var usersContext = new UsersContext())
            {
                var user = usersContext.GetUser(username);
                if (user == null)
                    return false;
                return user.type != null && user.type == roleName;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            using (var usersContext = new UsersContext())
            {
                var user = usersContext.GetUser(username);
                if (user == null)
                    return new string[] { };
                return user.type == null ? new string[] { } : new string[] { user.type };
            }
        }

        public override string[] GetAllRoles()
        {
            return roles;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            if (roleNames.Length > 1)
            {
                throw new NotSupportedException();
            }
            using (var usersContext = new UsersContext())
            {
                foreach (var user in usernames)
                {
                    var uzytkownik = usersContext.GetUser(user);
                    var role = roleNames.FirstOrDefault();
                    if (role != null)
                    {
                        uzytkownik.type = role;
                    }
                    else
                    {
                        uzytkownik.type = string.Empty;
                    }
                    usersContext.SaveUser(uzytkownik);
                }
            }
        }

        public override string ApplicationName
        {
            get
            {
                return this.ApplicationName;
            }
            set
            {
                this.ApplicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            using (var usersContext = new UsersContext())
            {
                return usersContext.GetUsers().Where(m => m.type == roleName && m.login.Contains(usernameToMatch)).Select(m => m.login).ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            using (var usersContext = new UsersContext())
            {
                return usersContext.GetUsers().Where(m => m.type == roleName).Select(m => m.login).ToArray();
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            using (var usersContext = new UsersContext())
            {
                foreach (var user in usernames)
                {
                    var uzytkownik = usersContext.GetUser(user);
                    if (roleNames.Contains(uzytkownik.type))
                    {
                        uzytkownik.type = string.Empty;
                    }
                    usersContext.SaveUser(uzytkownik);
                }
            }
        }

        public override bool RoleExists(string roleName)
        {
            return this.roles.Contains(roleName);
        }
    }
}