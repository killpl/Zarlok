using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZarlokMvc.ViewModels
{
    public class EatenViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [DisplayName("Wybierz produkt")]
        public Nullable<int> foodId { get; set; }
        public Nullable<int> profileId { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    }
}