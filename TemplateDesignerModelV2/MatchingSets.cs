//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TemplateDesignerModelV2
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    [KnownType(typeof(MatchingSetCategories))]
    public partial class MatchingSets
    {
        public MatchingSets()
        {
            this.MatchingSetCategories = new HashSet<MatchingSetCategories>();
        }
    
        public int MatchingSetID { get; set; }
        public string MatchingSetName { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDt { get; set; }
    
        public virtual ICollection<MatchingSetCategories> MatchingSetCategories { get; set; }
    }
}
