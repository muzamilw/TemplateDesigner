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
    public partial class TemplateColorStyles: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int PelleteID
        {
            get { return _pelleteID; }
            set
            {
                if (_pelleteID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'PelleteID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _pelleteID = value;
                    OnPropertyChanged("PelleteID");
                }
            }
        }
        private int _pelleteID;
    
        [DataMember]
        public Nullable<int> ProductID
        {
            get { return _productID; }
            set
            {
                if (_productID != value)
                {
                    ChangeTracker.RecordOriginalValue("ProductID", _productID);
                    if (!IsDeserializing)
                    {
                        if (Templates != null && Templates.ProductID != value)
                        {
                            Templates = null;
                        }
                    }
                    _productID = value;
                    OnPropertyChanged("ProductID");
                }
            }
        }
        private Nullable<int> _productID;
    
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public Nullable<int> ColorC
        {
            get { return _colorC; }
            set
            {
                if (_colorC != value)
                {
                    _colorC = value;
                    OnPropertyChanged("ColorC");
                }
            }
        }
        private Nullable<int> _colorC;
    
        [DataMember]
        public Nullable<int> ColorM
        {
            get { return _colorM; }
            set
            {
                if (_colorM != value)
                {
                    _colorM = value;
                    OnPropertyChanged("ColorM");
                }
            }
        }
        private Nullable<int> _colorM;
    
        [DataMember]
        public Nullable<int> ColorY
        {
            get { return _colorY; }
            set
            {
                if (_colorY != value)
                {
                    _colorY = value;
                    OnPropertyChanged("ColorY");
                }
            }
        }
        private Nullable<int> _colorY;
    
        [DataMember]
        public Nullable<int> ColorK
        {
            get { return _colorK; }
            set
            {
                if (_colorK != value)
                {
                    _colorK = value;
                    OnPropertyChanged("ColorK");
                }
            }
        }
        private Nullable<int> _colorK;
    
        [DataMember]
        public string SpotColor
        {
            get { return _spotColor; }
            set
            {
                if (_spotColor != value)
                {
                    _spotColor = value;
                    OnPropertyChanged("SpotColor");
                }
            }
        }
        private string _spotColor;
    
        [DataMember]
        public Nullable<bool> IsSpotColor
        {
            get { return _isSpotColor; }
            set
            {
                if (_isSpotColor != value)
                {
                    _isSpotColor = value;
                    OnPropertyChanged("IsSpotColor");
                }
            }
        }
        private Nullable<bool> _isSpotColor;
    
        [DataMember]
        public Nullable<int> Field1
        {
            get { return _field1; }
            set
            {
                if (_field1 != value)
                {
                    _field1 = value;
                    OnPropertyChanged("Field1");
                }
            }
        }
        private Nullable<int> _field1;
    
        [DataMember]
        public string ColorHex
        {
            get { return _colorHex; }
            set
            {
                if (_colorHex != value)
                {
                    _colorHex = value;
                    OnPropertyChanged("ColorHex");
                }
            }
        }
        private string _colorHex;

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
    
            if (previousValue != null && previousValue.TemplateColorStyles.Contains(this))
            {
                previousValue.TemplateColorStyles.Remove(this);
            }
    
            if (Templates != null)
            {
                if (!Templates.TemplateColorStyles.Contains(this))
                {
                    Templates.TemplateColorStyles.Add(this);
                }
    
                ProductID = Templates.ProductID;
            }
            else if (!skipKeys)
            {
                ProductID = null;
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
