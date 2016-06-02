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
    public partial class TemplateObjects: IObjectWithChangeTracker, INotifyPropertyChanged
    {
        #region Primitive Properties
    
        [DataMember]
        public int ObjectID
        {
            get { return _objectID; }
            set
            {
                if (_objectID != value)
                {
                    if (ChangeTracker.ChangeTrackingEnabled && ChangeTracker.State != ObjectState.Added)
                    {
                        throw new InvalidOperationException("The property 'ObjectID' is part of the object's key and cannot be changed. Changes to key properties can only be made when the object is not being tracked or is in the Added state.");
                    }
                    _objectID = value;
                    OnPropertyChanged("ObjectID");
                }
            }
        }
        private int _objectID;
    
        [DataMember]
        public int ObjectType
        {
            get { return _objectType; }
            set
            {
                if (_objectType != value)
                {
                    _objectType = value;
                    OnPropertyChanged("ObjectType");
                }
            }
        }
        private int _objectType;
    
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
        public bool IsEditable
        {
            get { return _isEditable; }
            set
            {
                if (_isEditable != value)
                {
                    _isEditable = value;
                    OnPropertyChanged("IsEditable");
                }
            }
        }
        private bool _isEditable;
    
        [DataMember]
        public bool IsHidden
        {
            get { return _isHidden; }
            set
            {
                if (_isHidden != value)
                {
                    _isHidden = value;
                    OnPropertyChanged("IsHidden");
                }
            }
        }
        private bool _isHidden;
    
        [DataMember]
        public bool IsMandatory
        {
            get { return _isMandatory; }
            set
            {
                if (_isMandatory != value)
                {
                    _isMandatory = value;
                    OnPropertyChanged("IsMandatory");
                }
            }
        }
        private bool _isMandatory;
    
        [DataMember]
        public int PageNo
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
        private int _pageNo;
    
        [DataMember]
        public double PositionX
        {
            get { return _positionX; }
            set
            {
                if (_positionX != value)
                {
                    _positionX = value;
                    OnPropertyChanged("PositionX");
                }
            }
        }
        private double _positionX;
    
        [DataMember]
        public double PositionY
        {
            get { return _positionY; }
            set
            {
                if (_positionY != value)
                {
                    _positionY = value;
                    OnPropertyChanged("PositionY");
                }
            }
        }
        private double _positionY;
    
        [DataMember]
        public double MaxHeight
        {
            get { return _maxHeight; }
            set
            {
                if (_maxHeight != value)
                {
                    _maxHeight = value;
                    OnPropertyChanged("MaxHeight");
                }
            }
        }
        private double _maxHeight;
    
        [DataMember]
        public double MaxWidth
        {
            get { return _maxWidth; }
            set
            {
                if (_maxWidth != value)
                {
                    _maxWidth = value;
                    OnPropertyChanged("MaxWidth");
                }
            }
        }
        private double _maxWidth;
    
        [DataMember]
        public double MaxCharacters
        {
            get { return _maxCharacters; }
            set
            {
                if (_maxCharacters != value)
                {
                    _maxCharacters = value;
                    OnPropertyChanged("MaxCharacters");
                }
            }
        }
        private double _maxCharacters;
    
        [DataMember]
        public double RotationAngle
        {
            get { return _rotationAngle; }
            set
            {
                if (_rotationAngle != value)
                {
                    _rotationAngle = value;
                    OnPropertyChanged("RotationAngle");
                }
            }
        }
        private double _rotationAngle;
    
        [DataMember]
        public bool IsFontCustom
        {
            get { return _isFontCustom; }
            set
            {
                if (_isFontCustom != value)
                {
                    _isFontCustom = value;
                    OnPropertyChanged("IsFontCustom");
                }
            }
        }
        private bool _isFontCustom;
    
        [DataMember]
        public bool IsFontNamePrivate
        {
            get { return _isFontNamePrivate; }
            set
            {
                if (_isFontNamePrivate != value)
                {
                    _isFontNamePrivate = value;
                    OnPropertyChanged("IsFontNamePrivate");
                }
            }
        }
        private bool _isFontNamePrivate;
    
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
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (_fontSize != value)
                {
                    _fontSize = value;
                    OnPropertyChanged("FontSize");
                }
            }
        }
        private double _fontSize;
    
        [DataMember]
        public int FontStyleID
        {
            get { return _fontStyleID; }
            set
            {
                if (_fontStyleID != value)
                {
                    _fontStyleID = value;
                    OnPropertyChanged("FontStyleID");
                }
            }
        }
        private int _fontStyleID;
    
        [DataMember]
        public bool IsBold
        {
            get { return _isBold; }
            set
            {
                if (_isBold != value)
                {
                    _isBold = value;
                    OnPropertyChanged("IsBold");
                }
            }
        }
        private bool _isBold;
    
        [DataMember]
        public bool IsItalic
        {
            get { return _isItalic; }
            set
            {
                if (_isItalic != value)
                {
                    _isItalic = value;
                    OnPropertyChanged("IsItalic");
                }
            }
        }
        private bool _isItalic;
    
        [DataMember]
        public int Allignment
        {
            get { return _allignment; }
            set
            {
                if (_allignment != value)
                {
                    _allignment = value;
                    OnPropertyChanged("Allignment");
                }
            }
        }
        private int _allignment;
    
        [DataMember]
        public int VAllignment
        {
            get { return _vAllignment; }
            set
            {
                if (_vAllignment != value)
                {
                    _vAllignment = value;
                    OnPropertyChanged("VAllignment");
                }
            }
        }
        private int _vAllignment;
    
        [DataMember]
        public double Indent
        {
            get { return _indent; }
            set
            {
                if (_indent != value)
                {
                    _indent = value;
                    OnPropertyChanged("Indent");
                }
            }
        }
        private double _indent;
    
        [DataMember]
        public bool IsUnderlinedText
        {
            get { return _isUnderlinedText; }
            set
            {
                if (_isUnderlinedText != value)
                {
                    _isUnderlinedText = value;
                    OnPropertyChanged("IsUnderlinedText");
                }
            }
        }
        private bool _isUnderlinedText;
    
        [DataMember]
        public int ColorType
        {
            get { return _colorType; }
            set
            {
                if (_colorType != value)
                {
                    _colorType = value;
                    OnPropertyChanged("ColorType");
                }
            }
        }
        private int _colorType;
    
        [DataMember]
        public int ColorStyleID
        {
            get { return _colorStyleID; }
            set
            {
                if (_colorStyleID != value)
                {
                    _colorStyleID = value;
                    OnPropertyChanged("ColorStyleID");
                }
            }
        }
        private int _colorStyleID;
    
        [DataMember]
        public int PalleteID
        {
            get { return _palleteID; }
            set
            {
                if (_palleteID != value)
                {
                    _palleteID = value;
                    OnPropertyChanged("PalleteID");
                }
            }
        }
        private int _palleteID;
    
        [DataMember]
        public string ColorName
        {
            get { return _colorName; }
            set
            {
                if (_colorName != value)
                {
                    _colorName = value;
                    OnPropertyChanged("ColorName");
                }
            }
        }
        private string _colorName;
    
        [DataMember]
        public int ColorC
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
        private int _colorC;
    
        [DataMember]
        public int ColorM
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
        private int _colorM;
    
        [DataMember]
        public int ColorY
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
        private int _colorY;
    
        [DataMember]
        public int ColorK
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
        private int _colorK;
    
        [DataMember]
        public int Tint
        {
            get { return _tint; }
            set
            {
                if (_tint != value)
                {
                    _tint = value;
                    OnPropertyChanged("Tint");
                }
            }
        }
        private int _tint;
    
        [DataMember]
        public bool IsSpotColor
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
        private bool _isSpotColor;
    
        [DataMember]
        public string SpotColorName
        {
            get { return _spotColorName; }
            set
            {
                if (_spotColorName != value)
                {
                    _spotColorName = value;
                    OnPropertyChanged("SpotColorName");
                }
            }
        }
        private string _spotColorName;
    
        [DataMember]
        public string ContentString
        {
            get { return _contentString; }
            set
            {
                if (_contentString != value)
                {
                    _contentString = value;
                    OnPropertyChanged("ContentString");
                }
            }
        }
        private string _contentString;
    
        [DataMember]
        public int ContentCaseType
        {
            get { return _contentCaseType; }
            set
            {
                if (_contentCaseType != value)
                {
                    _contentCaseType = value;
                    OnPropertyChanged("ContentCaseType");
                }
            }
        }
        private int _contentCaseType;
    
        [DataMember]
        public int ProductID
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
        private int _productID;
    
        [DataMember]
        public int DisplayOrderPdf
        {
            get { return _displayOrderPdf; }
            set
            {
                if (_displayOrderPdf != value)
                {
                    _displayOrderPdf = value;
                    OnPropertyChanged("DisplayOrderPdf");
                }
            }
        }
        private int _displayOrderPdf;
    
        [DataMember]
        public int DisplayOrderTxtControl
        {
            get { return _displayOrderTxtControl; }
            set
            {
                if (_displayOrderTxtControl != value)
                {
                    _displayOrderTxtControl = value;
                    OnPropertyChanged("DisplayOrderTxtControl");
                }
            }
        }
        private int _displayOrderTxtControl;
    
        [DataMember]
        public bool IsRequireNumericValue
        {
            get { return _isRequireNumericValue; }
            set
            {
                if (_isRequireNumericValue != value)
                {
                    _isRequireNumericValue = value;
                    OnPropertyChanged("IsRequireNumericValue");
                }
            }
        }
        private bool _isRequireNumericValue;
    
        [DataMember]
        public int RColor
        {
            get { return _rColor; }
            set
            {
                if (_rColor != value)
                {
                    _rColor = value;
                    OnPropertyChanged("RColor");
                }
            }
        }
        private int _rColor;
    
        [DataMember]
        public int GColor
        {
            get { return _gColor; }
            set
            {
                if (_gColor != value)
                {
                    _gColor = value;
                    OnPropertyChanged("GColor");
                }
            }
        }
        private int _gColor;
    
        [DataMember]
        public int BColor
        {
            get { return _bColor; }
            set
            {
                if (_bColor != value)
                {
                    _bColor = value;
                    OnPropertyChanged("BColor");
                }
            }
        }
        private int _bColor;
    
        [DataMember]
        public bool isSide2Object
        {
            get { return _isSide2Object; }
            set
            {
                if (_isSide2Object != value)
                {
                    _isSide2Object = value;
                    OnPropertyChanged("isSide2Object");
                }
            }
        }
        private bool _isSide2Object;
    
        [DataMember]
        public double LineSpacing
        {
            get { return _lineSpacing; }
            set
            {
                if (_lineSpacing != value)
                {
                    _lineSpacing = value;
                    OnPropertyChanged("LineSpacing");
                }
            }
        }
        private double _lineSpacing;
    
        [DataMember]
        public int ProductPageId
        {
            get { return _productPageId; }
            set
            {
                if (_productPageId != value)
                {
                    _productPageId = value;
                    OnPropertyChanged("ProductPageId");
                }
            }
        }
        private int _productPageId;
    
        [DataMember]
        public int ParentId
        {
            get { return _parentId; }
            set
            {
                if (_parentId != value)
                {
                    _parentId = value;
                    OnPropertyChanged("ParentId");
                }
            }
        }
        private int _parentId;
    
        [DataMember]
        public double OffsetX
        {
            get { return _offsetX; }
            set
            {
                if (_offsetX != value)
                {
                    _offsetX = value;
                    OnPropertyChanged("OffsetX");
                }
            }
        }
        private double _offsetX;
    
        [DataMember]
        public double OffsetY
        {
            get { return _offsetY; }
            set
            {
                if (_offsetY != value)
                {
                    _offsetY = value;
                    OnPropertyChanged("OffsetY");
                }
            }
        }
        private double _offsetY;
    
        [DataMember]
        public bool IsNewLine
        {
            get { return _isNewLine; }
            set
            {
                if (_isNewLine != value)
                {
                    _isNewLine = value;
                    OnPropertyChanged("IsNewLine");
                }
            }
        }
        private bool _isNewLine;
    
        [DataMember]
        public string TCtlName
        {
            get { return _tCtlName; }
            set
            {
                if (_tCtlName != value)
                {
                    _tCtlName = value;
                    OnPropertyChanged("TCtlName");
                }
            }
        }
        private string _tCtlName;
    
        [DataMember]
        public string ExField1
        {
            get { return _exField1; }
            set
            {
                if (_exField1 != value)
                {
                    _exField1 = value;
                    OnPropertyChanged("ExField1");
                }
            }
        }
        private string _exField1;
    
        [DataMember]
        public string ExField2
        {
            get { return _exField2; }
            set
            {
                if (_exField2 != value)
                {
                    _exField2 = value;
                    OnPropertyChanged("ExField2");
                }
            }
        }
        private string _exField2;
    
        [DataMember]
        public Nullable<bool> IsPositionLocked
        {
            get { return _isPositionLocked; }
            set
            {
                if (_isPositionLocked != value)
                {
                    _isPositionLocked = value;
                    OnPropertyChanged("IsPositionLocked");
                }
            }
        }
        private Nullable<bool> _isPositionLocked;
    
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
    
        private void FixupTemplates(Templates previousValue)
        {
            if (IsDeserializing)
            {
                return;
            }
    
            if (previousValue != null && previousValue.TemplateObjects.Contains(this))
            {
                previousValue.TemplateObjects.Remove(this);
            }
    
            if (Templates != null)
            {
                if (!Templates.TemplateObjects.Contains(this))
                {
                    Templates.TemplateObjects.Add(this);
                }
    
                ProductID = Templates.ProductID;
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
