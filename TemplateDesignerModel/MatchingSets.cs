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
    [KnownType(typeof(MatchingSetCategories))]
    [KnownType(typeof(Templates))]
    public partial class MatchingSets: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int MatchingSetID
        {
            get { return _matchingSetID; }
            set
            {
                if (_matchingSetID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'MatchingSetID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _matchingSetID = value;
                    OnPropertyChanged("MatchingSetID");
                }
            }
        }
        private int _matchingSetID;
    
        [DataMember]
        public string MatchingSetName
        {
            get { return _matchingSetName; }
            set
            {
                if (_matchingSetName != value)
                {
                    _matchingSetName = value;
                    OnPropertyChanged("MatchingSetName");
                }
            }
        }
        private string _matchingSetName;
    
        [DataMember]
        public Nullable<System.DateTime> CreationDate
        {
            get { return _creationDate; }
            set
            {
                if (_creationDate != value)
                {
                    _creationDate = value;
                    OnPropertyChanged("CreationDate");
                }
            }
        }
        private Nullable<System.DateTime> _creationDate;
    
        [DataMember]
        public Nullable<System.DateTime> LastUpdatedDt
        {
            get { return _lastUpdatedDt; }
            set
            {
                if (_lastUpdatedDt != value)
                {
                    _lastUpdatedDt = value;
                    OnPropertyChanged("LastUpdatedDt");
                }
            }
        }
        private Nullable<System.DateTime> _lastUpdatedDt;

        #endregion
        #region Navigation Properties
    
        [DataMember]
        public TrackableCollection<MatchingSetCategories> MatchingSetCategories
        {
            get
            {
                if (_matchingSetCategories == null)
                {
                    _matchingSetCategories = new TrackableCollection<MatchingSetCategories>();
                    _matchingSetCategories.CollectionChanged += FixupMatchingSetCategories;
                }
                return _matchingSetCategories;
            }
            set
            {
                if (!ReferenceEquals(_matchingSetCategories, value))
                {
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        throw new InvalidOperationException("Cannot set the FixupChangeTrackingCollection when ChangeTracking is enabled");
                    }
                    if (_matchingSetCategories != null)
                    {
                        _matchingSetCategories.CollectionChanged -= FixupMatchingSetCategories;
                    }
                    _matchingSetCategories = value;
                    if (_matchingSetCategories != null)
                    {
                        _matchingSetCategories.CollectionChanged += FixupMatchingSetCategories;
                    }
                    OnNavigationPropertyChanged("MatchingSetCategories");
                }
            }
        }
        private TrackableCollection<MatchingSetCategories> _matchingSetCategories;
    
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
            MatchingSetCategories.Clear();
            Templates.Clear();
        }

        #endregion
        #region Association Fixup
    
        private void FixupMatchingSetCategories(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (e.NewItems != null)
            {
                foreach (MatchingSetCategories item in e.NewItems)
                {
                    item.MatchingSets = this;
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        if (!item.ChangeTracker.ChangeTrackingEnabled)
                        {
                            item.StartTracking();
                        }
                        ChangeTracker.RecordAdditionToCollectionProperties("MatchingSetCategories", item);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (MatchingSetCategories item in e.OldItems)
                {
                    if (ReferenceEquals(item.MatchingSets, this))
                    {
                        item.MatchingSets = null;
                    }
                    if (ChangeTracker.ChangeTrackingEnabled)
                    {
                        ChangeTracker.RecordRemovalFromCollectionProperties("MatchingSetCategories", item);
                    }
                }
            }
        }
    
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
                    item.MatchingSets = this;
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
                    if (ReferenceEquals(item.MatchingSets, this))
                    {
                        item.MatchingSets = null;
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