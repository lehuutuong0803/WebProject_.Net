//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EntityProfileItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public bool Visible { get; set; }
        public bool VisibleInTheGrid { get; set; }
        public bool Required { get; set; }
        public bool Advanced { get; set; }
        public bool Searchable { get; set; }
        public int Deleted { get; set; }
        public bool Active { get; set; }
        public bool Default { get; set; }
        public Nullable<int> EntityProfile_ID { get; set; }
    
        public virtual EntityProfile EntityProfile { get; set; }
    }
}
