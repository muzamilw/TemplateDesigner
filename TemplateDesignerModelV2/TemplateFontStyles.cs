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
    public partial class TemplateFontStyles
    {
        public int FontStyleID { get; set; }
        public string Name { get; set; }
        public string FontName { get; set; }
        public double Size { get; set; }
        public double TheadingValue { get; set; }
        public int TheadingUnit { get; set; }
        public string TrackingDirective { get; set; }
        public int Allignment { get; set; }
        public double Indent { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsAllCapital { get; set; }
        public int ColorPelleteID { get; set; }
        public int ProductID { get; set; }
        public bool IsPrivateFontName { get; set; }
    
        public virtual Templates Templates { get; set; }
    }
}
