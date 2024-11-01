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
    public partial class TemplatePages: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int ProductPageId
        {
            get { return _productPageId; }
            set
            {
                if (_productPageId != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ProductPageId' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _productPageId = value;
                    OnPropertyChanged("ProductPageId");
                }
            }
        }
        private int _productPageId;
    
        [DataMember]
        public Nullable<int> ProductId
        {
            get { return _productId; }
            set
            {
                if (_productId != value)
                {
                    _productId = value;
                    OnPropertyChanged("ProductId");
                }
            }
        }
        private Nullable<int> _productId;
    
        [DataMember]
        public Nullable<int> PageNo
        {
            get { return _pageNo; }
            set
            {
                if (_pageNo != value)
                {
                    _pageNo = value;
                    OnPropertyChanged("PageNo");
                }
            }
        }
        private Nullable<int> _pageNo;
    
        [DataMember]
        public Nullable<int> ProductBackgroundImagesId
        {
            get { return _productBackgroundImagesId; }
            set
            {
                if (_productBackgroundImagesId != value)
                {
                    _productBackgroundImagesId = value;
                    OnPropertyChanged("ProductBackgroundImagesId");
                }
            }
        }
        private Nullable<int> _productBackgroundImagesId;
    
        [DataMember]
        public string BackgroundImg
        {
            get { return _backgroundImg; }
            set
            {
                if (_backgroundImg != value)
                {
                    _backgroundImg = value;
                    OnPropertyChanged("BackgroundImg");
                }
            }
        }
        private string _backgroundImg;
    
        [DataMember]
        public Nullable<bool> IsUseBackGroundColor
        {
            get { return _isUseBackGroundColor; }
            set
            {
                if (_isUseBackGroundColor != value)
                {
                    _isUseBackGroundColor = value;
                    OnPropertyChanged("IsUseBackGroundColor");
                }
            }
        }
        private Nullable<bool> _isUseBackGroundColor;
    
        [DataMember]
        public Nullable<double> BgC
        {
            get { return _bgC; }
            set
            {
                if (_bgC != value)
                {
                    _bgC = value;
                    OnPropertyChanged("BgC");
                }
            }
        }
        private Nullable<double> _bgC;
    
        [DataMember]
        public Nullable<double> BgM
        {
            get { return _bgM; }
            set
            {
                if (_bgM != value)
                {
                    _bgM = value;
                    OnPropertyChanged("BgM");
                }
            }
        }
        private Nullable<double> _bgM;
    
        [DataMember]
        public Nullable<double> BgY
        {
            get { return _bgY; }
            set
            {
                if (_bgY != value)
                {
                    _bgY = value;
                    OnPropertyChanged("BgY");
                }
            }
        }
        private Nullable<double> _bgY;
    
        [DataMember]
        public Nullable<double> BgK
        {
            get { return _bgK; }
            set
            {
                if (_bgK != value)
                {
                    _bgK = value;
                    OnPropertyChanged("BgK");
                }
            }
        }
        private Nullable<double> _bgK;
    
        [DataMember]
        public Nullable<bool> IsSide2
        {
            get { return _isSide2; }
            set
            {
                if (_isSide2 != value)
                {
                    _isSide2 = value;
                    OnPropertyChanged("IsSide2");
                }
            }
        }
        private Nullable<bool> _isSide2;
    
        [DataMember]
        public Nullable<int> PageType
        {
            get { return _pageType; }
            set
            {
                if (_pageType != value)
                {
                    _pageType = value;
                    OnPropertyChanged("PageType");
                }
            }
        }
        private Nullable<int> _pageType;
    
        [DataMember]
        public Nullable<int> PageTemplateId
        {
            get { return _pageTemplateId; }
            set
            {
                if (_pageTemplateId != value)
                {
                    _pageTemplateId = value;
                    OnPropertyChanged("PageTemplateId");
                }
            }
        }
        private Nullable<int> _pageTemplateId;
    
        [DataMember]
        public string PageName
        {
            get { return _pageName; }
            set
            {
                if (_pageName != value)
                {
                    _pageName = value;
                    OnPropertyChanged("PageName");
                }
            }
        }
        private string _pageName;

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
        }

        #endregion
    }
}
