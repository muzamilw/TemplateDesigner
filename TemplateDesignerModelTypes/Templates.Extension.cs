﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace TemplateDesignerModelTypes
{

    public partial class Templates : IObjectWithChangeTracker, INotifyPropertyChanged
    {
        private byte[] _SLThumbnailByte;

         [DataMember]
        public byte[] SLThumbnaillByte
        {
            get
            {
                return _SLThumbnailByte;
            }
            protected set {
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
             protected set
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
             protected set
             {
                 _SuperViewByte = value;
             }
         } 

    }
}