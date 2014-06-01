using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZarlokMvc.ViewModels
{
    public class InvitationListViewModel
    {
        [Required]
        public Nullable<int> senderId { get; set; }

        [DisplayName("Nick")]
        public string login { get; set; }

        [DisplayName("Imię")]
        public string firstName { get; set; }

        [DisplayName("Nazwisko")]
        public string lastName { get; set; }

        [DisplayName("Imię i nazwisko")]
        public string name { get; set; }

        [DisplayName("Data wysłania zaproszenia")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy, HH:mm }")]
        [DataType(DataType.Date)]
        public DateTime? invitationDate { get; set; }

    }
}