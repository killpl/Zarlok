using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ZarlokMvc.ViewModels
{
    public class SendInvitationViewModel
    {
        [Required]
        public int idSender { get; set; }

        [Required]
        public int IdReceiver { get; set; }

        [Required]
        public DateTime invitationDate { get; set; }
    }
}