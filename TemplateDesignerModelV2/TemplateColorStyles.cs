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
    [KnownType(typeof(Templates))]
    public partial class TemplateColorStyles
    {
        public int PelleteID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ColorC { get; set; }
        public Nullable<int> ColorM { get; set; }
        public Nullable<int> ColorY { get; set; }
        public Nullable<int> ColorK { get; set; }
        public string SpotColor { get; set; }
        public Nullable<bool> IsSpotColor { get; set; }
        public Nullable<int> Field1 { get; set; }
        public string ColorHex { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<bool> IsColorActive { get; set; }
    
        public virtual Templates Templates { get; set; }
    }
}
