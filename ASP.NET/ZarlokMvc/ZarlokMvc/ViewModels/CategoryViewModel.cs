using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZarlokMvc.ViewModels
{
    public class CategoryViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Wpisz nazwę"), MaxLength(100)]
        public string name { get; set; }
    }
}