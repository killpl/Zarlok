//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZarlokMvc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Eaten
    {
        public int id { get; set; }

        [DisplayName("Jedzenie")]
        public Nullable<int> foodId { get; set; }
        public Nullable<int> profileId { get; set; }

        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy, HH:mm }")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual Food Food { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
