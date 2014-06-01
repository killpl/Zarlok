using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using ZarlokMvc.Filters;
using ZarlokMvc.Models;
using ZarlokMvc.ViewModels;

namespace ZarlokMvc.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        private ProfileModel profileModel = new ProfileModel();
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();
        }
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // GET: /Account/EditUser
        [HttpGet]
        public ActionResult EditUser(int id)
        {
            ViewBag.Message = "";
            var user = profileModel.GetProfile(id);
            if (user == null)
            {
                ViewBag.Error = "Taki użytkownik nie istnieje";
                return View("Index");
            }
            return PartialView(user);
        }

        //
        // POST: /Account/EditUser
        [HttpPost]
        public ActionResult EditUser(ProfileViewModel user)
        {
            if (ModelState.IsValid)
            {
                using (UsersContext context = new UsersContext())
                {
                    var user2 = context.GetUser(User.Identity.Name);
                    if (context.GetUsers().Any(m => m.login == user.login && m.id != user.id))
                    {
                        ViewBag.ErrorMessage = "Taki login już istnieje";
                    }
                    else
                    {
                        user.id = user2.id;
                        profileModel.EditProfile(user);
                    }
                }
            }
            UserModel model = new UserModel()
            {
                PasswordModel = new LocalPasswordModel(),
                ProfileModel = user
            };
            return View("Manage", model);
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (UsersContext context = new UsersContext())
                {
                    var user = context.GetUsers().SingleOrDefault(m => m.login == model.Login && m.password == model.Password);
                    if (user != null)
                    {
                        // FormsAuthentication.SetAuthCookie(user.UzytkownikID, model.RememberMe);
                        FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                        return RedirectToLocal(returnUrl);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "Login lub hasło jest niepoprawne!");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    using (UsersContext context = new UsersContext())
                    {
                        if (!context.GetUsers().Any(m => m.login == model.Login))
                        {
                            context.AddUser(new Profile()
                            {
                                firstname = model.Firstname,
                                surname = model.Surname,
                                password = model.Password,
                                type = "user",
                                login = model.Login
                            });
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Istnieje już użytkownik o takim loginie!");
                        }
                    }
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            UserModel model = new UserModel()
            {
                PasswordModel = new LocalPasswordModel(),
                ProfileModel = CreateProfileModel()
            };

            var uzytkownik = profileModel.GetProfile(User.Identity.Name);
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View(model);
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                using (UsersContext context = new UsersContext())
                {
                    var user = context.GetUser(User.Identity.Name);
                    if (user.password == model.OldPassword)
                    {
                        if (model.NewPassword == model.ConfirmPassword)
                        {
                            user.password = model.NewPassword;
                            context.SaveUser(user);
                            ViewBag.StatusMessage = "Zmiana hasła powiodła się!";
                            return RedirectToAction("Manage");

                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Nowe hasło i powtórzone nie są identyczne!";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Stare hasło jest niepoprawne";
                    }
                }
            }

            UserModel uzytkownikModel = new UserModel()
            {
                PasswordModel = model,
                ProfileModel = CreateProfileModel()
            };

            var uzytkownik = this.profileModel.GetProfile(User.Identity.Name);
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View(uzytkownikModel);
        }

        private ProfileViewModel CreateProfileModel()
        {
            using (UsersContext context = new UsersContext())
            {
                var user = context.GetUser(User.Identity.Name);
                var uzytkownik = profileModel.GetProfile(user.id);
                return uzytkownik;
            }
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
