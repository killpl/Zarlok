using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZarlokMvc.ViewModels
{
    public class FriendsListViewModel
    {
        [Required]
        public int userId { get; set; }

        [DisplayName("Imię:")]
        public string firstName { get; set; }

        [DisplayName("Nazwisko:")]
        public string lastName { get; set; }

        [DisplayName("Imię i nazwisko")]
        public string name { get; set; }

        [DisplayName("Nick")]
        public string login { get; set; }

        public bool isFriend { get; set; }
    }
}