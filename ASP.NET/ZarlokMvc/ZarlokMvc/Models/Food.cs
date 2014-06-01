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
    
    public partial class Food
    {
        public Food()
        {
            this.Eaten = new HashSet<Eaten>();
        }
    
        public int id { get; set; }

        [DisplayName("Nazwa posi�ku")]
        public string name { get; set; }

        [DisplayName("Kalorie")]
        public Nullable<int> calories { get; set; }

        [DisplayName("Nazwa kategorii")]
        public Nullable<int> categoryId { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<Eaten> Eaten { get; set; }
    }
}
