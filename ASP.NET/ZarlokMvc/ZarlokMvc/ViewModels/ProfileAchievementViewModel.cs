using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZarlokMvc.ViewModels
{
    public class ProfileAchievementViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [DisplayName("Użytkownik")]
        public Nullable<int> profileId { get; set; }

        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> date { get; set; }

        [DisplayName("Osiągnięcie")]
        public Nullable<int> achievementId { get; set; }
    }
}