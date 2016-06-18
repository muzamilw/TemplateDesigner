

namespace TemplateDesignerModelV2
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    [DataContract]
    public partial class TemplatesModel
    {
        public TemplatesModel()
        {
            
        }
         [DataMember]
        public int ProductID { get; set; }
         [DataMember]
        public string Code { get; set; }
         [DataMember]
        public string ProductName { get; set; }
         [DataMember]
        public string Description { get; set; }
         [DataMember]
        public Nullable<int> ProductCategoryID { get; set; }
         [DataMember]
        public string LowResPDFTemplates { get; set; }
         [DataMember]
        public string BackgroundArtwork { get; set; }
         [DataMember]
         public string Side2LowResPDFTemplates { get; set; }
         [DataMember]
         public string Side2BackgroundArtwork { get; set; }
         [DataMember]
         public string Thumbnail { get; set; }
         [DataMember]
         public string Image { get; set; }
         [DataMember]
         public Nullable<bool> IsDisabled { get; set; }
         [DataMember]
         public Nullable<int> PTempId { get; set; }
         [DataMember]
         public bool IsDoubleSide { get; set; }
         [DataMember]
         public bool IsUsePDFFile { get; set; }
         [DataMember]
         public Nullable<double> PDFTemplateWidth { get; set; }
         [DataMember]
         public Nullable<double> PDFTemplateHeight { get; set; }
         [DataMember]
         public Nullable<bool> IsUseBackGroundColor { get; set; }
         [DataMember]
         public Nullable<int> BgR { get; set; }
         [DataMember]
         public Nullable<int> BgG { get; set; }
         [DataMember]
         public Nullable<int> BgB { get; set; }
         [DataMember]
         public Nullable<bool> IsUseSide2BackGroundColor { get; set; }
         [DataMember]
         public Nullable<int> Side2BgR { get; set; }
         [DataMember]
         public Nullable<int> Side2BgG { get; set; }
         [DataMember]
         public Nullable<int> Side2BgB { get; set; }
         [DataMember]
         public Nullable<double> CuttingMargin { get; set; }
         [DataMember]
         public Nullable<int> MultiPageCount { get; set; }
         [DataMember]
         public Nullable<int> Orientation { get; set; }
         [DataMember]
         public string MatchingSetTheme { get; set; }
         [DataMember]
         public Nullable<int> BaseColorID { get; set; }
         [DataMember]
         public Nullable<int> SubmittedBy { get; set; }
         [DataMember]
         public string SubmittedByName { get; set; }
         [DataMember]
         public Nullable<System.DateTime> SubmitDate { get; set; }
         [DataMember]
         public Nullable<int> Status { get; set; }
         [DataMember]
         public Nullable<int> ApprovedBy { get; set; }
         [DataMember]
         public string ApprovedByName { get; set; }
         [DataMember]
         public Nullable<int> UserRating { get; set; }
         [DataMember]
         public Nullable<int> UsedCount { get; set; }
         [DataMember]
         public Nullable<int> MPCRating { get; set; }
         [DataMember]
         public string RejectionReason { get; set; }
         [DataMember]
         public Nullable<System.DateTime> ApprovalDate { get; set; }
         [DataMember]
         public string TempString { get; set; }
         [DataMember]
         public Nullable<int> MatchingSetID { get; set; }
         [DataMember]
         public string SLThumbnail { get; set; }
         [DataMember]
         public string FullView { get; set; }
         [DataMember]
         public string SuperView { get; set; }
         [DataMember]
         public string ColorHex { get; set; }
         [DataMember]
         public Nullable<int> TemplateOwner { get; set; }
         [DataMember]
         public string TemplateOwnerName { get; set; }
         [DataMember]
         public Nullable<bool> IsPrivate { get; set; }
         [DataMember]
         public Nullable<System.DateTime> ApprovedDate { get; set; }
         [DataMember]
         public Nullable<bool> IsCorporateEditable { get; set; }
         [DataMember]
         public Nullable<int> TemplateType { get; set; }
         [DataMember]
         public Nullable<bool> isWatermarkText { get; set; }
         [DataMember]
         public Nullable<bool> isSpotTemplate { get; set; }
         [DataMember]
         public Nullable<bool> isCreatedManual { get; set; }
         [DataMember]
         public Nullable<bool> isEditorChoice { get; set; }
         [DataMember]
        public Nullable<decimal> ScaleFactor { get; set; }


        private byte[] _SLThumbnailByte;

        [DataMember]
        public byte[] SLThumbnaillByte
        {
            get
            {
                return _SLThumbnailByte;
            }
            set
            {
                _SLThumbnailByte = value;
            }
        }


        private byte[] _FullViewByte;

        [DataMember]
        public byte[] FullViewByte
        {
            get
            {
                return _FullViewByte;
            }
            set
            {
                _FullViewByte = value;
            }
        }


        private byte[] _SuperViewByte;

        [DataMember]
        public byte[] SuperViewByte
        {
            get
            {
                return _SuperViewByte;
            }
            set
            {
                _SuperViewByte = value;
            }
        } 
    }
}
