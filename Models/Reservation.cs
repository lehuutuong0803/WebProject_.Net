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
    
    public partial class Reservation
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public Nullable<int> AmoutPerson { get; set; }
        public string NameTheBooker { get; set; }
        public string EmailTheBooker { get; set; }
        public string NumberTheBooker { get; set; }
        public int Id { get; set; }
    }
}