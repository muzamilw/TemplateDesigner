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
    [Serializable]
    [KnownType(typeof(MatchingSets))]
    [KnownType(typeof(tbl_ProductCategory))]
    public partial class MatchingSetCategories
    {
        public int ID { get; set; }
        public Nullable<int> MatchingSetID { get; set; }
        public Nullable<int> ProductCategoryID { get; set; }
    
        public virtual MatchingSets MatchingSets { get; set; }
        public virtual tbl_ProductCategory tbl_ProductCategory { get; set; }
    }
}
