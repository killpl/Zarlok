using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZarlokMvc.ViewModels
{
    public class AchievementViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [DisplayName("Osiągnięcie")]
        [Required(ErrorMessage = "Wpisz osiągnięcie"), MaxLength(150)]
        public string name { get; set; }
    }
}