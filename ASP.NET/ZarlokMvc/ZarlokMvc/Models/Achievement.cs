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
    
    public partial class Achievement
    {
        public Achievement()
        {
            this.ProfileAchievement = new HashSet<ProfileAchievement>();
        }
    
        public int id { get; set; }

        [DisplayName("Nazwa")]
        public string name { get; set; }
        public string icon { get; set; }
    
        public virtual ICollection<ProfileAchievement> ProfileAchievement { get; set; }
    }
}
