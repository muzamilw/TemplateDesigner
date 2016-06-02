/**
* Jquery UndoAble Plugin For Designer Module of MPC Systems
* First Draft By: Arslan Pervaiz
* Dated: 22-03-12
* Dependencies: jQuery/1.3.2+
*/

/**  Undo Funtion Starts Here **/

(function () {
    var jsk = {
        ///// PROPERTIES
        dids: [],
        undids: [],
        // Can undo
        canUndo: function () {
            return this.dids.length > 0;
        },
        // Can Redo
        canRedo: function () {
            return this.undids.length > 0;
        },
        ///// FUNCTIONS
        // deprecated : Push an undo function
        push: function (undoFunction) {
            this.execute(null, undoFunction);
        },
        // Do something that can be undone
        execute: function execute(doFunction, undoFunction, options) {
            // If in async, it will not execute the do when calling redo
            // (see redoFunction)
            var data;

            if (options === undefined || options === null) { options = {}; }

            if (this.isFct(doFunction) && options.async !== true) {
                data = doFunction();
            }

            // This causes me problem on async
            // TODO : This is not thread safe but I didn't find yet how to do it
            if (jsk.isInAsyncRedo !== true) {
                this.undids = [];
            }
            jsk.isInAsyncRedo = false;

            // If there's data in options, use them
            if (options.data) { data = options.data; }

            // Create a new undo and pass what the do returned
            var wrappedUndo = function wrappedUndo() {
                undoFunction(data);
            };

            this.dids.push({ redo: doFunction, undo: undoFunction,
                wrappedUndo: wrappedUndo, options: options
            });

            this.fireEvents();

            return data;
        },
        // Undo
        undo: function undo() {
            var fct = this.dids && this.dids.length > 0 ? this.dids.pop() : null;
            if (this.isFct(fct.wrappedUndo)) {
                fct.wrappedUndo();

                // There can be no "do" so don't push a redo
                if (this.isFct(fct.redo)) {
                    this.undids.push({ redo: fct.redo, undo: fct.undo,
                        options: fct.options
                    });
                }
            }

            this.fireEvents();
        },
        // Redo
        redo: function redo() {
            var fct = this.undids && this.undids.length > 0 ? this.undids.pop() : null;
            if (this.isFct(fct.redo)) {
                jsk.isInAsyncRedo = fct.options.async;
                var data = fct.redo();

                // If there's data in options, use them
                if (fct.options.data) { data = fct.options.data; }

                var wrappedUndo = function wrappedUndo() {
                    fct.undo(data);
                };

                // Put the redo in dids (if in async, skip this)
                if (fct.options.async !== true) {
                    this.dids.push({ redo: fct.redo, undo: fct.undo,
                        wrappedUndo: wrappedUndo, options: fct.options
                    });
                }
            }

            this.fireEvents();
        },
        ///// EVENTS
        // When there's a change
        onChange: function () {
            return false;
        },
        // deprecated : when all the do/undo are empty
        onEmpty: function () {
            return false;
        },
        ///// PRIVATE
        // fired when something changes
        fireEvents: function () {
            if (this.onChange) { this.onChange(); }
            if (this.dids.length === 0 && this.undids.length === 0) { this.onEmpty(); }
        },
        // is Function
        isFct: function (fct) {
            return fct && typeof fct == "function";
        }
    };

    // Creates the base namespaces
    if (window.javascriptKataDotCom === undefined) { window.javascriptKataDotCom = {}; }
    if (window.jsKata === undefined) { window.jsKata = window.javascriptKataDotCom; }
    if (window.jsk === undefined) { window.jsk = window.javascriptKataDotCom; }
    if (window._ === undefined) { window._ = window.javascriptKataDotCom; }

    window.javascriptKataDotCom.undo = jsk;
    window.javascriptKataDotCom.u = jsk;

    // Shortcut for backward compatibility
    window.jskataUndo = window.javascriptKataDotCom.undo;
})();



    /** Undo Funtion Ends Here **/



/** Class Defination And Helper Function **/


// Show Alert Function
function ShowAlert(user) {
    alert(user + ' Added');
}

// Discard Alert Function
function DisCardAlert(user) {
    alert(user + ' Removed');
}

// Defination Of Class Draw
function ClassDraw() {
    // Private variable
    var id;
    var ObjectID;
    var ObjectType
    var Name;
    var IsEditable;
    var IsHidden;
    var IsMandatory;
    var PageNo;
    var PositionX;
    var PositionY;
    var MaxHeight;
    var MaxWidth;
    var MaxCharacters;
    var RotationAngle;
    var IsFontCustom;
    var IsFontNamePrivate;
    var FontName;
    var FontSize;
    var FontStyleID;
    var IsBold;
    var IsItalic;
    var Allignment;
    var VAllignment;
    var Indent;
    var IsUnderlinedText;
    var ColorType;
    var ColorStyleID;
    var PalleteID;
    var ColorName;
    var ColorC;
    var ColorM;
    var ColorY;
    var ColorK;
    var Tint;
    var IsSpotColor;
    var SpotColorName;
    var ContentString;
    var ContentCaseType;
    var ProductID;
    var DisplayOrderPdf;
    var DisplayOrderTxtControl;
    var IsRequireNumericValue;
    var RColor;
    var GColor;
    var BColor;
    var isSide2Object;
    var LineSpacing;
    var ProductPageId;
    var ParentId;
    var OffsetX;
    var OffsetY;
    var IsNewLine;
    var TCtlName;
    var ExField1;
    var ExField2;
    var IsPositionLocked;
    var Templates;
    var ChangeTracker;
    var State;
    var ObjectsRemovedFromCollectionProperties;
    var OriginalValues;
    var ExtendedProperties;
    var ObjectsAddedToCollectionProperties


    return {
        // Public methods
        setName: function (newName) {
            Name = newName;
        },
        getName: function () {
            return Name;
        },
        setID: function (newid) {
            id = newid;
        },
        getID: function () {
            return id;
        },
        setObjectID: function (objectid) {
            ObjectID = objectid;
        },
        getObjectID: function () {
            return ObjectID;
        },
        setObjectType: function (objecttype) {
            ObjectType = objecttype;
        },
        getObjectType: function () {
            return ObjectType;
        },
        setIsEditable: function (isEditable) {
            IsEditable = isEditable;
        },
        getIsEditable: function () {
            return IsEditable;
        },
        setIsHidden: function (isHidden) {
            IsHidden = isHidden;
        },
        getIsEditable: function () {
            return IsHidden;
        },
        setIsMandatory: function (isMandatory) {
            IsMandatory = isMandatory;
        },
        getIsMandatory: function () {
            return IsMandatory;
        },
        setPageNo: function (pageNo) {
            PageNo = pageNo;
        },
        getPageNo: function () {
            return PageNo;
        },
        setPositionX: function (positionX) {
            PositionX = positionX;
        },
        getPositionX: function () {
            return PositionX;
        },
        setPositionY: function (positionY) {
            PositionY = positionY;
        },
        getPositionY: function () {
            return PositionY;
        },
        setMaxHeight: function (maxHeight) {
            MaxHeight = maxHeight;
        },
        getMaxHeight: function () {
            return MaxHeight;
        },
        setMaxWidth: function (maxWidth) {
            MaxWidth = maxWidth;
        },
        getMaxWidth: function () {
            return MaxWidth;
        },
        setMaxCharacters: function (maxCharacters) {
            MaxCharacters = maxCharacters;
        },
        getMaxCharacters: function () {
            return MaxCharacters;
        },
        setRotationAngle: function (rotationAngle) {
            RotationAngle = rotationAngle;
        },
        getRotationAngle: function () {
            return RotationAngle;
        },
        setIsFontCustom: function (isFontCustom) {
            IsFontCustom = isFontCustom;
        },
        getIsFontCustom: function () {
            return IsFontCustom;
        },
        setIsFontNamePrivate: function (isFontNamePrivate) {
            IsFontNamePrivate = isFontNamePrivate;
        },
        getIsFontNamePrivate: function () {
            return IsFontNamePrivate;
        },
        setFontName: function (fontName) {
            FontName = fontName;
        },
        getFontName: function () {
            return FontName;
        },
        setFontSize: function (fontSize) {
            FontSize = fontSize;
        },
        getFontSize: function () {
            return FontSize;
        },
        setFontStyleID: function (fontStyleID) {
            FontStyleID = fontStyleID;
        },
        getFontStyleID: function () {
            return FontStyleID;
        },
        setIsBold: function (isBold) {
            IsBold = isBold;
        },
        getIsBold: function () {
            return IsBold;
        },
        setIsItalic: function (isItalic) {
            IsItalic = isItalic;
        },
        getIsItalic: function () {
            return IsItalic;
        },
        setAllignment: function (allignment) {
            Allignment = allignment;
        },
        getAllignment: function () {
            return Allignment;
        },
        setVAllignment: function (vAllignment) {
            VAllignment = vAllignment;
        },
        getVAllignment: function () {
            return VAllignment;
        },
        setIndent: function (indent) {
            Indent = indent;
        },
        getIndent: function () {
            return Indent;
        },
        setIsUnderlinedText: function (isUnderlinedText) {
            IsUnderlinedText = isUnderlinedText;
        },
        getIsUnderlinedText: function () {
            return IsUnderlinedText;
        },
        setColorType: function (colorType) {
            ColorType = colorType;
        },
        getColorType: function () {
            return ColorType;
        },
        setColorStyleID: function (colorStyleID) {
            ColorStyleID = colorStyleID;
        },
        getColorStyleID: function () {
            return ColorStyleID;
        },
        setPalleteID: function (palleteID) {
            PalleteID = palleteID;
        },
        getPalleteID: function () {
            return PalleteID;
        },
        setColorName: function (colorName) {
            ColorName = colorName;
        },
        getColorName: function () {
            return ColorName;
        },
        setColorC: function (colorC) {
            ColorName = colorC;
        },
        getColorC: function () {
            return ColorC;
        },
        setColorM: function (colorM) {
            ColorM = colorM;
        },
        getColorM: function () {
            return ColorM;
        },
        setColorY: function (colorY) {
            ColorY = colorY;
        },
        getColorY: function () {
            return ColorY;
        },
        setColorK: function (colorK) {
            ColorK = colorK;
        },
        getColorK: function () {
            return ColorK;
        },
        setTint: function (tint) {
            Tint = tint;
        },
        getTint: function () {
            return Tint;
        },
        setIsSpotColor: function (isSpotColor) {
            IsSpotColor = isSpotColor;
        },
        getIsSpotColor: function () {
            return IsSpotColor;
        },
        setSpotColorName: function (spotColorName) {
            SpotColorName = spotColorName;
        },
        getSpotColorName: function () {
            return SpotColorName;
        },
        setContentString: function (contentString) {
            ContentString = contentString;
        },
        getContentString: function () {
            return ContentString;
        },

        setContentCaseType: function (contentCaseType) {
            ContentCaseType = contentCaseType;
        },
        getContentCaseType: function () {
            return ContentCaseType;
        },
        setProductID: function (productID) {
            ProductID = productID;
        },
        getProductID: function () {
            return ProductID;
        },
        setDisplayOrderPdf: function (displayOrderPdf) {
            DisplayOrderPdf = displayOrderPdf;
        },
        getDisplayOrderPdf: function () {
            return DisplayOrderPdf;
        },
        setDisplayOrderTxtControl: function (displayOrderTxtControl) {
            DisplayOrderTxtControl = displayOrderTxtControl;
        },
        getDisplayOrderTxtControl: function () {
            return DisplayOrderTxtControl;
        },
        setIsRequireNumericValue: function (isRequireNumericValue) {
            IsRequireNumericValue = isRequireNumericValue;
        },
        getIsRequireNumericValue: function () {
            return IsRequireNumericValue;
        },
        setRColor: function (rColor) {
            RColor = rColor;
        },
        getRColor: function () {
            return RColor;
        },
        setGColor: function (gColor) {
            GColor = gColor;
        },
        getGColor: function () {
            return GColor;
        },
        setBColor: function (bColor) {
            BColor = bColor;
        },
        getBColor: function () {
            return BColor;
        },
        setisSide2Object: function (IsSide2Object) {
            isSide2Object = IsSide2Object;
        },
        getisSide2Object: function () {
            return isSide2Object;
        },
        setLineSpacing: function (lineSpacing) {
            LineSpacing = lineSpacing;
        },
        getLineSpacing: function () {
            return LineSpacing;
        },
        setProductPageId: function (productPageId) {
            ProductPageId = productPageId;
        },
        getProductPageId: function () {
            return ProductPageId;
        },
        setParentId: function (parentId) {
            ParentId = parentId;
        },
        getParentId: function () {
            return ParentId;
        },
        setOffsetX: function (offsetX) {
            OffsetX = offsetX;
        },
        getOffsetX: function () {
            return OffsetX;
        },
        setOffsetY: function (Offsety) {
            OffsetY = Offsety;
        },
        getOffsetY: function () {
            return OffsetY;
        },
        setIsNewLine: function (isNewLine) {
            IsNewLine = isNewLine;
        },
        getIsNewLine: function () {
            return IsNewLine;
        },
        setTCtlName: function (tCtlName) {
            TCtlName = tCtlName;
        },
        getTCtlName: function () {
            return TCtlName;
        },
        setExField1: function (exField1) {
            ExField1 = exField1;
        },
        getExField1: function () {
            return ExField1;
        },
        setExField2: function (exField2) {
            ExField2 = exField2;
        },
        getExField2: function () {
            return ExField2;
        },
        setIsPositionLocked: function (isPositionLocked) {
            IsPositionLocked = isPositionLocked;
        },
        getIsPositionLocked: function () {
            return IsPositionLocked;
        },
        setTemplates: function (templates) {
            Templates = templates;
        },
        getTemplates: function () {
            return Templates;
        },
        setChangeTracker: function (changeTracker) {
            ChangeTracker = changeTracker;
        },
        getChangeTracker: function () {
            return ChangeTracker;
        },
        setState: function (state) {
            State = state;
        },
        getState: function () {
            return State;
        },
        setObjectsRemovedFromCollectionProperties: function (objectsRemovedFromCollectionProperties) {
            ObjectsRemovedFromCollectionProperties = objectsRemovedFromCollectionProperties;
        },
        getObjectsRemovedFromCollectionProperties: function () {
            return ObjectsRemovedFromCollectionProperties;
        },
        setOriginalValues: function (originalValues) {
            OriginalValues = originalValues;
        },
        getOriginalValues: function () {
            return OriginalValues;
        },
        setExtendedProperties: function (extendedProperties) {
            ExtendedProperties = extendedProperties;
        },
        getExtendedProperties: function () {
            return ExtendedProperties;
        },
        setObjectsAddedToCollectionProperties: function (objectsAddedToCollectionProperties) {
            ObjectsAddedToCollectionProperties = objectsAddedToCollectionProperties;
        },
        getObjectsAddedToCollectionProperties: function () {
            return ObjectsAddedToCollectionProperties;
        }

    };
}


/**  End Class Defination And Helper Functions **/



