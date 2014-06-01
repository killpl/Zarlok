using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZarlokMvc.ViewModels
{
    public class FriendshipViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }
        public int sender { get; set; }
        public int receiver { get; set; }
    }
}