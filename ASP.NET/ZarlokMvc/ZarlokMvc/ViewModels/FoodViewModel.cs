using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZarlokMvc.ViewModels
{
    public class FoodViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Wpisz nazwę"), MaxLength(150)]
        public string name { get; set; }

        [DisplayName("Kalorie")]
        public Nullable<int> calories { get; set; }

        [DisplayName("Kategoria")]
        public Nullable<int> categoryId { get; set; }
    }
}