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
    
    public partial class TemplateFonts
    {
        public int ProductFontId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string FontName { get; set; }
        public string FontDisplayName { get; set; }
        public string FontFile { get; set; }
        public Nullable<int> DisplayIndex { get; set; }
        public bool IsPrivateFont { get; set; }
        public Nullable<bool> IsEnable { get; set; }
        public byte[] FontBytes { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string FontPath { get; set; }
    
        public virtual Templates Templates { get; set; }
    }
}