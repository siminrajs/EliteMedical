//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EliteHospital.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class EventImagesList
    {
        public int Id { get; set; }
        public Nullable<int> EventId { get; set; }
        public byte[] Image { get; set; }
        public string ImagePath { get; set; }
        public string Event_Image { get; set; }
    }
}
