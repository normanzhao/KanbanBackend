//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KanbanBackend.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class item
    {
        public int id { get; set; }
        public int p_id { get; set; }
        public string type { get; set; }
        public int priority { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    
        public virtual project project { get; set; }
    }
}
