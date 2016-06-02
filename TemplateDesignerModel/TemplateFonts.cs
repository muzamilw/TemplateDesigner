//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;

namespace TemplateDesignerModelTypes
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(Templates))]
    public partial class TemplateFonts: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int ProductFontId
        {
            get { return _productFontId; }
            set
            {
                if (_productFontId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ProductFontId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _productFontId = value;
                    OnPropertyChanged("ProductFontId");
                }
            }
        }
        private int _productFontId;
    
        [DataMember]
        public Nullable<int> ProductId
        {
            get { return _productId; }
            set
            {
                if (_productId != value)
                {
                    ChangeTracker.RecordOriginalValue("ProductId", _productId);
                    if (!IsDeserializing)
                    {
                        if (Templates != null && Templates.ProductID != value)
                        {
                            Templates = null;
                        }
                    }
                    _productId = value;
                    OnPropertyChanged("ProductId");
                }
            }
        }
        private Nullable<int> _productId;
    
        [DataMember]
        public string FontName
        {
            get { return _fontName; }
            set
            {
                if (_fontName != value)
                {
                    _fontName = value;
                    OnPropertyChanged("FontName");
                }
            }
        }
        private string _fontName;
    
        [DataMember]
        public string FontDisplayName
        {
            get { return _fontDisplayName; }
            set
            {
                if (_fontDisplayName != value)
                {
                    _fontDisplayName = value;
                    OnPropertyChanged("FontDisplayName");
                }
            }
        }
        private string _fontDisplayName;
    
        [DataMember]
        public string FontFile
        {
            get { return _fontFile; }
            set
            {
                if (_fontFile != value)
                {
                    _fontFile = value;
                    OnPropertyChanged("FontFile");
                }
            }
        }
        private string _fontFile;
    
        [DataMember]
        public Nullable<int> DisplayIndex
        {
            get { return _displayIndex; }
            set
            {
                if (_displayIndex != value)
                {
                    _displayIndex = value;
                    OnPropertyChanged("DisplayIndex");
                }
            }
        }
        private Nullable<int> _displayIndex;
    
        [DataMember]
        public bool IsPrivateFont
        {
            get { return _isPrivateFont; }
            set
            {
                if (_isPrivateFont != value)
                {
                    _isPrivateFont = value;
                    OnPropertyChanged("IsPrivateFont");
                }
            }
        }
        private bool _isPrivateFont;
    
        [DataMember]
        public Nullable<bool> IsEnable
        {
            get { return _isEnable; }
            set
            {
                if (_isEnable != value)
                {
                    _isEnable = value;
                    OnPropertyChanged("IsEnable");
                }
            }
        }
        private Nullable<bool> _isEnable;
    
        [DataMember]
        public byte[] FontBytes
        {
            get { return _fontBytes; }
            set
            {
                if (_fontBytes != value)
                {
                    _fontBytes = value;
                    OnPropertyChanged("FontBytes");
                }
            }
        }
        private byte[] _fontBytes;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public Templates Templates
        {
            get { return _templates; }
            set
            {
                if (!ReferenceEquals(_templates, value))
                {
                    var previousValue = _templates;
                    _templates = value;
                    FixupTemplates(previousValue);
                    OnNavigationPropertyChanged("Templates");
                }
            }
        }
        private Templates _templates;

        #endregion
        #region ChangeTracking
    
        protected virtual void OnPropertyChanged(String propertyName)
        {
            if (ChangeTracker.State != ObjectState.Added && ChangeTracker.State != ObjectState.Deleted)
            {
                ChangeTracker.State = ObjectState.Modified;
            }
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        protected virtual void OnNavigationPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged{ add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
        private ObjectChangeTracker _changeTracker;
    
        [DataMember]
        public ObjectChangeTracker ChangeTracker
        {
            get
            {
                if (_changeTracker == null)
                {
                    _changeTracker = new ObjectChangeTracker();
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
                return _changeTracker;
            }
            set
            {
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging -= HandleObjectStateChanging;
                }
                _changeTracker = value;
                if(_changeTracker != null)
                {
                    _changeTracker.ObjectStateChanging += HandleObjectStateChanging;
                }
            }
        }
    
        private void HandleObjectStateChanging(object sender, ObjectStateChangingEventArgs e)
        {
            if (e.NewState == ObjectState.Deleted)
            {
                ClearNavigationProperties();
            }
        }
    
        protected bool IsDeserializing { get; private set; }
    
        [OnDeserializing]
        public void OnDeserializingMethod(StreamingContext context)
        {
            IsDeserializing = true;
        }
    
        [OnDeserialized]
        public void OnDeserializedMethod(StreamingContext context)
        {
            IsDeserializing = false;
            ChangeTracker.ChangeTrackingEnabled = true;
        }
    
        protected virtual void ClearNavigationProperties()
        {
            Templates = null;
        }

        #endregion
        #region Association Fixup
    
        private void FixupTemplates(Templates previousValue, bool skipKeys = false)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TemplateFonts.Contains(this))
            {
                previousValue.TemplateFonts.Remove(this);
            }
    
            if (Templates != null)
            {
                if (!Templates.TemplateFonts.Contains(this))
                {
                    Templates.TemplateFonts.Add(this);
                }
    
                ProductId = Templates.ProductID;
            }
            else if (!skipKeys)
            {
                ProductId = null;
            }
    
            if (ChangeTracker.ChangeTrackingEnabled)
            {
                if (ChangeTracker.OriginalValues.ContainsKey("Templates")
                    && (ChangeTracker.OriginalValues["Templates"] == Templates))
                {
                    ChangeTracker.OriginalValues.Remove("Templates");
                }
                else
                {
                    ChangeTracker.RecordOriginalValue("Templates", previousValue);
                }
                if (Templates != null && !Templates.ChangeTracker.ChangeTrackingEnabled)
                {
                    Templates.StartTracking();
                }
            }
        }

        #endregion
    }
}
