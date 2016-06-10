using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace TemplateDesignerModelV2
{

    public partial class Templates 
    {
        private byte[] _SLThumbnailByte;

         [DataMember]
        public byte[] SLThumbnaillByte
        {
            get
            {
                return _SLThumbnailByte;
            }
            set {
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