using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ZarlokMvc.ViewModels
{
    public class ProfileViewModel
    {
        public int id { get; set; }

        [DisplayName("Nick")]
        public string login { get; set; }
        public string password { get; set; }

        [DisplayName("Imię")]
        public string firstname { get; set; }

        [DisplayName("Nazwisko")]
        public string surname { get; set; }
        public string type { get; set; }

        [DisplayName("Imię i nazwisko")]
        public string name { get; set; }
        public bool isFriend { get; set; }
    }
}