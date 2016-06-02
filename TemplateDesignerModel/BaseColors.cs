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
    public partial class BaseColors: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int BaseColorID
        {
            get { return _baseColorID; }
            set
            {
                if (_baseColorID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'BaseColorID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _baseColorID = value;
                    OnPropertyChanged("BaseColorID");
                }
            }
        }
        private int _baseColorID;
    
        [DataMember]
        public string Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged("Color");
                }
            }
        }
        private string _color;
    
        [DataMember]
        public Nullable<int> type
        {
            get { return _type; }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged("type");
                }
            }
        }
        private Nullable<int> _type;
    
        [DataMember]
        public string RGB
        {
            get { return _rGB; }
            set
            {
                if (_rGB != value)
                {
                    _rGB = value;
                    OnPropertyChanged("RGB");
                }
            }
        }
        private string _rGB;
    
        [DataMember]
        public string HEX
        {
            get { return _hEX; }
            set
            {
                if (_hEX != value)
                {
                    _hEX = value;
                    OnPropertyChanged("HEX");
                }
            }
        }
        private string _hEX;
    
        [DataMember]
        public string CMYK
        {
            get { return _cMYK; }
            set
            {
                if (_cMYK != value)
                {
                    _cMYK = value;
                    OnPropertyChanged("CMYK");
                }
            }
        }
        private string _cMYK;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<Templates> Templates
        {
            get
            {
                if (_templates == null)
                {
                    _templates = new TrackableCollection<Templates>();
                    _templates.CollectionChanged += FixupTemplates;
                }
                return _templates;
            }
            set
            {
                if (!ReferenceEquals(_templates, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_templates != null)
                    {
                        _templates.CollectionChanged -= FixupTemplates;
                    }
                    _templates = value;
                    if (_templates != null)
                    {
                        _templates.CollectionChanged += FixupTemplates;
                    }
                    OnNavigationPropertyChanged("Templates");
                }
            }
        }
        private TrackableCollection<Templates> _templates;

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
            Templates.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupTemplates(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (Templates item in e.NewItems)
                {
                    item.BaseColors = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("Templates", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Templates item in e.OldItems)
                {
                    if (ReferenceEquals(item.BaseColors, this))
                    {
                        item.BaseColors = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("Templates", item);
                    }
                }
            }
        }

        #endregion
    }
}
