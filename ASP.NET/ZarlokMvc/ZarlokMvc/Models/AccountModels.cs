using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Linq;
using ZarlokMvc.ViewModels;
using ZarlokMvc.Models;

namespace ZarlokMvc.Models
{
    public class UsersContext : BaseModel
    {
        public Profile GetUser(string username)
        {
            return db.Profile.FirstOrDefault(m => m.login == username);
        }

        public void SaveUser(Profile user)
        {
            db.SaveChanges();
        }

        public void AddUser(Profile user)
        {
            db.Profile.Add(user);
            db.SaveChanges();
        }

        public void DeleteUser(Profile user)
        {
            db.Profile.Remove(user);
            db.SaveChanges();
        }

        public List<Profile> GetUsers()
        {
            return db.Profile.ToList();
        }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Stare hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi być o długości minimum {2} znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Podane hasła są różne")]
        public string ConfirmPassword { get; set; }
    }

    public class UserModel
    {
        public LocalPasswordModel PasswordModel { get; set; }
        public ProfileViewModel ProfileModel { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Imię")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} musi być o długości minimum {2} znaków", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Podane hasła są różne")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
