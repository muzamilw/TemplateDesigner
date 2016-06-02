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
    
    public partial class sp_GetMatchingSetTemplateView_Result : INotifyComplexPropertyChanging, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public Nullable<long> RN
        {
            get { return _rN; }
            set
            {
                if (_rN != value)
                {
                    OnComplexPropertyChanging();
                    _rN = value;
                    OnPropertyChanged("RN");
                }
            }
        }
        private Nullable<long> _rN;
    
        [DataMember]
        public string ProductName
        {
            get { return _productName; }
            set
            {
                if (_productName != value)
                {
                    OnComplexPropertyChanging();
                    _productName = value;
                    OnPropertyChanged("ProductName");
                }
            }
        }
        private string _productName;
    
        [DataMember]
        public Nullable<int> matchingSetID
        {
            get { return _matchingSetID; }
            set
            {
                if (_matchingSetID != value)
                {
                    OnComplexPropertyChanging();
                    _matchingSetID = value;
                    OnPropertyChanged("matchingSetID");
                }
            }
        }
        private Nullable<int> _matchingSetID;
    
        [DataMember]
        public Nullable<int> TemplateCount
        {
            get { return _templateCount; }
            set
            {
                if (_templateCount != value)
                {
                    OnComplexPropertyChanging();
                    _templateCount = value;
                    OnPropertyChanged("TemplateCount");
                }
            }
        }
        private Nullable<int> _templateCount;
    
        [DataMember]
        public Nullable<int> Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    OnComplexPropertyChanging();
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        private Nullable<int> _status;
    
        [DataMember]
        public string Completion
        {
            get { return _completion; }
            set
            {
                if (_completion != value)
                {
                    OnComplexPropertyChanging();
                    _completion = value;
                    OnPropertyChanged("Completion");
                }
            }
        }
        private string _completion;
    
        [DataMember]
        public string Thumbnail
        {
            get { return _thumbnail; }
            set
            {
                if (_thumbnail != value)
                {
                    OnComplexPropertyChanging();
                    _thumbnail = value;
                    OnPropertyChanged("Thumbnail");
                }
            }
        }
        private string _thumbnail;
    
        [DataMember]
        public Nullable<int> ProductID
        {
            get { return _productID; }
            set
            {
                if (_productID != value)
                {
                    OnComplexPropertyChanging();
                    _productID = value;
                    OnPropertyChanged("ProductID");
                }
            }
        }
        private Nullable<int> _productID;

        #endregion
        #region ChangeTracking
    
        private void OnComplexPropertyChanging()
        {
            if (_complexPropertyChanging != null)
            {
                _complexPropertyChanging(this, new EventArgs());
            }
        }
    
        event EventHandler INotifyComplexPropertyChanging.ComplexPropertyChanging { add { _complexPropertyChanging += value; } remove { _complexPropertyChanging -= value; } }
        private event EventHandler _complexPropertyChanging;
    
        private void OnPropertyChanged(String propertyName)
        {
            if (_propertyChanged != null)
            {
                _propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged { add { _propertyChanged += value; } remove { _propertyChanged -= value; } }
        private event PropertyChangedEventHandler _propertyChanged;
    
        public static void RecordComplexOriginalValues(String parentPropertyName, sp_GetMatchingSetTemplateView_Result complexObject, ObjectChangeTracker changeTracker)
        {
            if (String.IsNullOrEmpty(parentPropertyName))
            {
                throw new ArgumentException("String parameter cannot be null or empty.", "parentPropertyName");
            }
    
            if (changeTracker == null)
            {
                throw new ArgumentNullException("changeTracker");
            }
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.RN", parentPropertyName), complexObject == null ? null : (object)complexObject.RN);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.ProductName", parentPropertyName), complexObject == null ? null : (object)complexObject.ProductName);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.matchingSetID", parentPropertyName), complexObject == null ? null : (object)complexObject.matchingSetID);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.TemplateCount", parentPropertyName), complexObject == null ? null : (object)complexObject.TemplateCount);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Status", parentPropertyName), complexObject == null ? null : (object)complexObject.Status);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Completion", parentPropertyName), complexObject == null ? null : (object)complexObject.Completion);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Thumbnail", parentPropertyName), complexObject == null ? null : (object)complexObject.Thumbnail);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.ProductID", parentPropertyName), complexObject == null ? null : (object)complexObject.ProductID);
        }

        #endregion
    }
}
