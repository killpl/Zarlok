using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZarlokMvc.ViewModels
{
    public class DetailsViewModel
    {
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> date { get; set; }
        public string name { get; set; }
    }
}