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
    
    public partial class QuickText : INotifyComplexPropertyChanging, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    OnComplexPropertyChanging();
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        private string _name;
    
        [DataMember]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    OnComplexPropertyChanging();
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        private string _title;
    
        [DataMember]
        public string Company
        {
            get { return _company; }
            set
            {
                if (_company != value)
                {
                    OnComplexPropertyChanging();
                    _company = value;
                    OnPropertyChanged("Company");
                }
            }
        }
        private string _company;
    
        [DataMember]
        public string Address1
        {
            get { return _address1; }
            set
            {
                if (_address1 != value)
                {
                    OnComplexPropertyChanging();
                    _address1 = value;
                    OnPropertyChanged("Address1");
                }
            }
        }
        private string _address1;
    
        [DataMember]
        public string Address2
        {
            get { return _address2; }
            set
            {
                if (_address2 != value)
                {
                    OnComplexPropertyChanging();
                    _address2 = value;
                    OnPropertyChanged("Address2");
                }
            }
        }
        private string _address2;
    
        [DataMember]
        public string Address3
        {
            get { return _address3; }
            set
            {
                if (_address3 != value)
                {
                    OnComplexPropertyChanging();
                    _address3 = value;
                    OnPropertyChanged("Address3");
                }
            }
        }
        private string _address3;
    
        [DataMember]
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                if (_telephone != value)
                {
                    OnComplexPropertyChanging();
                    _telephone = value;
                    OnPropertyChanged("Telephone");
                }
            }
        }
        private string _telephone;
    
        [DataMember]
        public string Fax
        {
            get { return _fax; }
            set
            {
                if (_fax != value)
                {
                    OnComplexPropertyChanging();
                    _fax = value;
                    OnPropertyChanged("Fax");
                }
            }
        }
        private string _fax;
    
        [DataMember]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    OnComplexPropertyChanging();
                    _email = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string _email;
    
        [DataMember]
        public string Website
        {
            get { return _website; }
            set
            {
                if (_website != value)
                {
                    OnComplexPropertyChanging();
                    _website = value;
                    OnPropertyChanged("Website");
                }
            }
        }
        private string _website;
    
        [DataMember]
        public string CompanyMessage
        {
            get { return _companyMessage; }
            set
            {
                if (_companyMessage != value)
                {
                    OnComplexPropertyChanging();
                    _companyMessage = value;
                    OnPropertyChanged("CompanyMessage");
                }
            }
        }
        private string _companyMessage;
    
        [DataMember]
        public string LogoPath
        {
            get { return _logoPath; }
            set
            {
                if (_logoPath != value)
                {
                    OnComplexPropertyChanging();
                    _logoPath = value;
                    OnPropertyChanged("LogoPath");
                }
            }
        }
        private string _logoPath;

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
    
        public static void RecordComplexOriginalValues(String parentPropertyName, QuickText complexObject, ObjectChangeTracker changeTracker)
        {
            if (String.IsNullOrEmpty(parentPropertyName))
            {
                throw new ArgumentException("String parameter cannot be null or empty.", "parentPropertyName");
            }
    
            if (changeTracker == null)
            {
                throw new ArgumentNullException("changeTracker");
            }
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Name", parentPropertyName), complexObject == null ? null : (object)complexObject.Name);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Title", parentPropertyName), complexObject == null ? null : (object)complexObject.Title);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Company", parentPropertyName), complexObject == null ? null : (object)complexObject.Company);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Address1", parentPropertyName), complexObject == null ? null : (object)complexObject.Address1);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Address2", parentPropertyName), complexObject == null ? null : (object)complexObject.Address2);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Address3", parentPropertyName), complexObject == null ? null : (object)complexObject.Address3);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Telephone", parentPropertyName), complexObject == null ? null : (object)complexObject.Telephone);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Fax", parentPropertyName), complexObject == null ? null : (object)complexObject.Fax);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Email", parentPropertyName), complexObject == null ? null : (object)complexObject.Email);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.Website", parentPropertyName), complexObject == null ? null : (object)complexObject.Website);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.CompanyMessage", parentPropertyName), complexObject == null ? null : (object)complexObject.CompanyMessage);
            changeTracker.RecordOriginalValue(String.Format(CultureInfo.InvariantCulture, "{0}.LogoPath", parentPropertyName), complexObject == null ? null : (object)complexObject.LogoPath);
        }

        #endregion
    }
}
