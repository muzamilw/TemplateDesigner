﻿function StartLoader(msg) {
    loaderLoading = true;
    var3 = 1;
    if (msg == null || msg == "") {
        msg = "You can add different layouts to your design, you can also add images, text content,  background colour and image to your design.";
    }
    $("#paraLoaderMsg").text(msg);
    $("#MainLoader").css("display","block");
    $(".progressValue").css("width","1%");
    $(".dialog").css("top", ($(window).height() - $(".dialog").height()) / 2 + "px");
    var2 = setInterval((function () { 
        var3 += 1;
        if (var3 <= 95) {
            $(".progressValue").css("width", var3 + "%");
        }
      
    }), 25);
}
function StopLoader() {
    var3 = 99;
    loaderLoading = false;
    $(".progressValue").css("width", 100 + "%");
    $(".progressValue").one('webkitTransitionEnd otransitionend oTransitionEnd msTransitionEnd transitionend',   
    function (e) {
       if (!loaderLoading) {
            $("#MainLoader").css("display", "none");
            clearInterval(var2);
        }
    });
   
    
}
function startInlineLoader(divID) {
    if (divID == 1) {
        $(".searchLoaderHolder").appendTo((".resultLayoutsScroller"));
    } else if (divID == 21) {
        $(".searchLoaderHolder").appendTo((".templateImagesContainer .inlineFolderGroup"));
    } else if (divID == 22) {
        $(".searchLoaderHolder").appendTo((".tempBackgroundImages .inlineFolderGroup"));
    } else if (divID == 23) {
        $(".searchLoaderHolder").appendTo((".freeImgsContainer .inlineFolderGroup"));
    } else if (divID == 24) {
        $(".searchLoaderHolder").appendTo((".freeBkImgsContainer .inlineFolderGroup"));
    } else if (divID == 25) {
        $(".searchLoaderHolder").appendTo((".shapesContainer .inlineFolderGroup"));
    } else if (divID == 26) {
        $(".searchLoaderHolder").appendTo((".logosContainer .inlineFolderGroup"));
    } else if (divID == 27) {
        $(".searchLoaderHolder").appendTo((".yourLogosContainer .inlineFolderGroup"));
    } else if (divID == 28) {
        $(".searchLoaderHolder").appendTo((".illustrationsContainer .inlineFolderGroup"));
    } else if (divID == 29) {
        $(".searchLoaderHolder").appendTo((".framesContainer .inlineFolderGroup"));
    } else if (divID == 30) {
        $(".searchLoaderHolder").appendTo((".bannersContainer .inlineFolderGroup"));
    } else if (divID == 31) {
        $(".searchLoaderHolder").appendTo((".myBkImgsContainer .inlineFolderGroup"));
    } else if (divID == 32) {
        $(".searchLoaderHolder").appendTo((".yourImagesContainer .inlineFolderGroup"));
    }
    $(".searchLoaderHolder").css("display", "block");
    var1 = setInterval((function () {
        $('.searchLoaderHolder  span').each(function (i) {
            $(this).toggleClass("on");
        });
    }), 500);
}
function stopInlineLoader() {
    $(".searchLoaderHolder").css("display", "none"); 
    $(".searchLoaderHolder").appendTo((".mainContainer"));
    clearInterval(var1);
}

function downloadJSAtOnload(name) {
    var element = document.createElement("script");
    element.src = name;
    document.body.appendChild(element);
}

function a0(fontName, fontFileName) {
    var path = "";
    path = "/DesignEngine/";
    var html = "";
    if (jQuery.browser.msie) {
        html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".woff" + ') format("woff");  font-weight: normal; font-style: normal;}</style>';
    } else if (jQuery.browser.Chrome) {
        html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".woff" + ') format("woff");  font-weight: normal; font-style: normal;}</style>';
    } else if (jQuery.browser.Safari || jQuery.browser.opera || jQuery.browser.mozilla) {
        html = '<style> @font-face { font-family: ' + fontName + '; src:  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
    } else {
        html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".eot" + '); src: url(' + path + fontFileName + ".eot?#iefix" + ') format(" embedded-opentype"), url(' + path + fontFileName + ".woff" + ') format("woff"),  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
    }
    $('head').append(html);
}

function b1(selectId, value, text, id) {
    var html = '<option  id = ' + id + ' value="' + value + '" >' + text + '</option>';
    $('#' + selectId).append(html);
}

function b4(imgSrc) {
    IW = 150;
    IH = 150;
    var he = Template.PDFTemplateHeight;
    var wd = Template.PDFTemplateWidth;
    $.each(LiImgs, function (i, IT) {
        if (imgSrc.indexOf(IT.BackgroundImageRelativePath) != -1) {
            IW = IT.ImageWidth;
            IH = IT.ImageHeight;
            if (parseInt(IW) < 50) {
                IW = 50;
            }
            if (parseInt(IH) < 50) {
                IH = 50;
            }
            if (IW > wd) {
                wd = wd / 2;
                ratio = wd / IW;
                IH = IH * ratio;
                IW = IW * ratio;
            }
            if (IH > he) {
                he = he / 2;
                ratio = he / IH;
                IW = IW * ratio;
            }
            return;
        }
    });
}

function b8(imageID, productID) {

    if (confirm("Delete this image from all instances on canvas on all pages! Do you still wish to delete this image now?")) {
        StartLoader("Deleting the image from your design, please wait....");
        b8_svc(imageID, productID);
    }
}
function b8_svc_callBack(DT) {
    if (DT != "false") {
        $("#" + imageID).parent().parent().remove();
        i2(DT);
        StopLoader();
    }
}
function c0(cCanvas, TOC) {
    var hAlign = "";
    if (TOC.Allignment == 1)
        hAlign = "left";
    else if (TOC.Allignment == 2)
        hAlign = "center";
    else if (TOC.Allignment == 3)
        hAlign = "right";
    var hStyle = "";
    if (TOC.IsItalic)
        hStyle = "italic";
    var hWeight = "";
    if (TOC.IsBold)
        hWeight = "bold";
    var textStyles = [];
    
    if (TOC.textStyles != null && TOC.textStyles != undefined && TOC.textStyles != "") {
        var textStylesTemp = JSON.parse(TOC.textStyles);
        $.each(textStylesTemp, function (i, IT) {
            if (!textStyles[IT.characterIndex]) {
                textStyles[IT.characterIndex] = {};
            }
            var style = {};
            var styleName = "";
            var value = "";
            if (IT.textColor) {
                styleName = 'color';
                value = IT.textColor;
                style[styleName] = value;
            }
            if (IT.fontName) {
                styleName = 'font-family';
                value = IT.fontName;
                style[styleName] = value;
            }
            if (IT.fontSize) {
                styleName = 'font-Size';
                value = IT.fontSize;
                style[styleName] = value;
            }
            if (IT.fontWeight) {
                styleName = 'font-Weight';
                value = IT.fontWeight;
                style[styleName] = value;
            }
            if (IT.fontStyle) {
                styleName = 'font-Style';
                value = IT.fontStyle;
                style[styleName] = value;
            }
            
            fabric.util.object.extend(textStyles[IT.characterIndex], style);
        });
    }
    var TOL = new fabric.IText(TOC.ContentString, {
        left: (TOC.PositionX + TOC.MaxWidth / 2) * dfZ1l,
        top: (TOC.PositionY + TOC.MaxHeight / 2) * dfZ1l,
        fontFamily: TOC.FontName,
        fontStyle: hStyle,
        fontWeight: hWeight,
        lineHeight: (TOC.LineSpacing == 0 ? 1 : TOC.LineSpacing),
        fontSize: TOC.FontSize,
        angle: TOC.RotationAngle,
        fill: TOC.ColorHex,
        scaleX: dfZ1l,  // to add an object on current zoom level
        scaleY: dfZ1l,    // to add an object on current zoom level
        maxWidth: TOC.MaxWidth,
        maxHeight: TOC.MaxHeight,
        textAlign: hAlign
    });
    TOL.ObjectID = TOC.ObjectID;
    if (textStyles != []) {
        TOL.customStyles = (textStyles);
    }
    TOL.C = TOC.ColorC;
    TOL.M = TOC.ColorM;
    TOL.Y = TOC.ColorY;
    TOL.K = TOC.ColorK;
    if (TOC.CharSpacing == "" || TOC.CharSpacing == null) {
        TOC.CharSpacing = 0;
    }
    TOL.charSpacing = TOC.CharSpacing;
    TOL.IsPositionLocked = TOC.IsPositionLocked;
    TOL.IsOverlayObject = TOC.IsOverlayObject;
    TOL.IsHidden = TOC.IsHidden;
    TOL.IsEditable = TOC.IsEditable;
    TOL.IsTextEditable = TOC.IsTextEditable;
    TOL.AutoShrinkText = TOC.AutoShrinkText;
    TOL.setAngle(TOC.RotationAngle);
    TOL.textCase = TOC.textCase;
    if (TOC.IsPositionLocked) {
        TOL.lockMovementX = true;
        TOL.lockMovementY = true;
        TOL.lockScalingX = true;
        TOL.lockScalingY = true;
        TOL.lockRotation = true;
    }
    else {
        TOL.lockMovementX = false;
        TOL.lockMovementY = false;
        TOL.lockScalingX = false;
        TOL.lockScalingY = false;
        TOL.lockRotation = false;
    }
    TOL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });
    //if(TOC.Name == "Name" || TOC.Name == "Title" || TOC.Name == "CompanyName" || TOC.Name == "CompanyMessage" || TOC.Name == "AddressLine1" || TOC.Name == "Phone" || TOC.Name == "Fax" || TOC.Name == "Email" || TOC.Name == "Website" )
    if (TOC.IsQuickText == true) {
        TOL.set({
            borderColor: 'green',
            cornerColor: 'green',
            cornersize: 10
        });
    }
    cCanvas.insertAt(TOL, TOC.DisplayOrderPdf);
    return TOL;

}
function c2_v2() {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    var objs = canvas.getObjects();
    $.each(objs, function (j, Obj) {
        c2_01(Obj);
    });
}
function c2_01(OPT) {
    $.each(TO, function (i, IT) {
        if (IT.ObjectID == OPT.ObjectID) {
            var orgLeft = OPT.left / dfZ1l;
            var orgTop = OPT.top / dfZ1l;
            var orgSx = OPT.scaleX / dfZ1l, orgSy = OPT.scaleY / dfZ1l;
            IT.PositionX = orgLeft - IT.MaxWidth / 2;
            IT.PositionY = orgTop - IT.MaxHeight / 2;
            if (OPT.type == "text" || OPT.type == "i-text") {
                IT.ContentString = OPT.text;
                var CustomStylesList = [];
                for (var prop in OPT.customStyles) {
                    var objStyle = OPT.customStyles[prop];
                    if (objStyle != undefined) {
                        var obj = {
                            textColor: objStyle['color'],
                            fontName: objStyle['font-family'],
                            fontSize: objStyle['font-Size'],
                            fontWeight: objStyle['font-Weight'],
                            fontStyle: objStyle['font-Style'],
                            characterIndex: prop
                        }
                        CustomStylesList.push(obj);
                    }
                }
                if (CustomStylesList.length != 0) {
                    IT.textStyles = JSON.stringify(CustomStylesList, null, 2);
                }
            }
            IT.RotationAngle = OPT.getAngle();
            if (OPT.type != "text" && OPT.type != "i-text") {
                IT.MaxWidth = OPT.width * orgSx;
                IT.MaxHeight = OPT.height * orgSy;
                OPT.maxWidth = OPT.width * OPT.scaleX;
                OPT.maxHeight = OPT.height * OPT.scaleY;
                if (OPT.type == "ellipse") {
                    IT.CircleRadiusX = OPT.get('rx') * orgSx;
                    IT.CircleRadiusY = OPT.get('ry') * orgSy;
                    IT.PositionX = orgLeft - (OPT.width * orgSx) / 2;
                    IT.PositionY = orgTop - (OPT.height * orgSy)/ 2;
                }
                if (OPT.type == "image") {
                    IT.ClippedInfo = OPT.ImageClippedInfo;
                }
                //IT.Tint =parseInt( OPT.getOpacity() * 100);
            }
            else {
                IT.MaxWidth = OPT.maxWidth;
                IT.MaxHeight = OPT.maxHeight;
                IT.LineSpacing = OPT.lineHeight;

            }
            if (OPT.type == "path-group") {
                //IT.textStyles = OPT.toDataURL();
            }
            if (OPT.textAlign == "left")
                IT.Allignment = 1;
            else if (OPT.textAlign == "center")
                IT.Allignment = 2;
            else if (OPT.textAlign == "right")
                IT.Allignment = 3;

            if (OPT.fontFamily != undefined)
                IT.FontName = OPT.fontFamily;
            else
                IT.FontName = "";

            if (OPT.fontSize != undefined)
                IT.FontSize = OPT.fontSize;
            else
                IT.FontSize = 0;

            if (OPT.fontWeight == "bold")
                IT.IsBold = true;
            else
                IT.IsBold = false;

            if (OPT.fontStyle == "italic")
                IT.IsItalic = true;
            else
                IT.IsItalic = false;

            if (OPT.type != "image") {
                IT.ColorHex = OPT.fill;
            }
            if (OPT.type == "path") {
                IT.ExField1 = OPT.strokeWidth;
            }
            IT.Opacity = OPT.opacity;
            IT.ColorC = OPT.C;
            IT.ColorM = OPT.M;
            IT.ColorY = OPT.Y;
            IT.ColorK = OPT.K;
            IT.IsPositionLocked = OPT.IsPositionLocked;
            IT.IsOverlayObject = OPT.IsOverlayObject;
            IT.IsTextEditable = OPT.IsTextEditable;
            IT.AutoShrinkText = OPT.AutoShrinkText;
            IT.IsHidden = OPT.IsHidden;
            IT.IsEditable = OPT.IsEditable;
            return;
        }
    });
}
function c2_del(obj) {
    $.each(TO, function (i, IT) {
        if (IT.ObjectID == obj.ObjectID) {
            fabric.util.removeFromArray(TO, IT);
            return false;
        }
    });
}
function c7(PageID) {
    $.each(TO, function (i, IT) {
        if (IT.ProductPageId == PageID) {
            if (IT.ObjectType == 2) {
                c0(canvas, IT);
            }
            else if (IT.ObjectType == 3) {
                $("#loadingMsg").html("Loading Design Images, please wait..");
                d1(canvas, IT);
            }
            else if (IT.ObjectType == 6) {
                c9(canvas, IT);
            }
            else if (IT.ObjectType == 7) {
                c8(canvas, IT);
            }
            else if (IT.ObjectType == 9) {
                d1SvgOl(canvas, IT);
            }
            else if (IT.ObjectType == 8) {
                k31(canvas, IT);
            }
            else if (IT.ObjectType == 12) {
                k31(canvas, IT);
            }
        }
    });

    d2();
}
function c8(cCanvas, CO) {
    var COL = new fabric.Ellipse({
        left: (CO.PositionX + CO.MaxWidth / 2)* dfZ1l,
        top: (CO.PositionY + CO.MaxHeight / 2)* dfZ1l,
        fill: CO.ColorHex,
        rx: (CO.CircleRadiusX)* dfZ1l,
        ry: (CO.CircleRadiusY)* dfZ1l,
        opacity: CO.Opacity

    })
    COL.C = CO.ColorC;
    COL.M = CO.ColorM;
    COL.Y = CO.ColorY;
    COL.K = CO.ColorK;
    COL.IsPositionLocked = CO.IsPositionLocked;
    COL.IsOverlayObject = CO.IsOverlayObject;
    COL.IsTextEditable = CO.IsTextEditable;
    COL.AutoShrinkText = CO.AutoShrinkText;
    COL.IsHidden = CO.IsHidden;
    COL.IsEditable = CO.IsEditable;
    if (CO.IsPositionLocked == true) {
        COL.lockMovementX = true;
        COL.lockMovementY = true;
        COL.lockScalingX = true;
        COL.lockScalingY = true;
        COL.lockRotation = true;
    }
    COL.setAngle(CO.RotationAngle);
    COL.ObjectID = CO.ObjectID;
    COL.maxWidth = CO.MaxWidth;
    COL.maxHeight = CO.MaxHeight;
    COL.setOpacity(CO.Opacity);
    COL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });
    canvas.insertAt(COL, CO.DisplayOrderPdf);
    canvas.renderAll();
}
function c9(cCanvas, RO) {
    var ROL = new fabric.Rect({
        left: (RO.PositionX + RO.MaxWidth / 2)* dfZ1l,
        top: (RO.PositionY + RO.MaxHeight / 2)* dfZ1l,
        fill: RO.ColorHex,
        width: (RO.MaxWidth)* dfZ1l,
        height: (RO.MaxHeight)* dfZ1l,
        opacity: 1
    });
    ROL.setAngle(RO.RotationAngle);
    ROL.C = RO.ColorC;
    ROL.M = RO.ColorM;
    ROL.Y = RO.ColorY;
    ROL.K = RO.ColorK;
    ROL.maxWidth = RO.MaxWidth;
    ROL.maxHeight = RO.MaxHeight;
    ROL.IsPositionLocked = RO.IsPositionLocked;
    ROL.IsOverlayObject = RO.IsOverlayObject;
    ROL.IsTextEditable = RO.IsTextEditable;
    ROL.AutoShrinkText = RO.AutoShrinkText;
    ROL.IsHidden = RO.IsHidden;
    ROL.IsEditable = RO.IsEditable;
    ROL.setOpacity(RO.Opacity);
    if (RO.IsPositionLocked == true) {
        ROL.lockMovementX = true;
        ROL.lockMovementY = true;
        ROL.lockScalingX = true;
        ROL.lockScalingY = true;
        ROL.lockRotation = true;
    }
    ROL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });
    ROL.ObjectID = RO.ObjectID;
    canvas.insertAt(ROL, RO.DisplayOrderPdf);
    canvas.renderAll();
}
function d1SvgOl(cCanvas, IO) {
    TIC += 1;
    fabric.loadSVGFromURL(IO.ContentString, function (objects, options) {

        var loadedObject = fabric.util.groupSVGElements(objects, options);
        loadedObject.set({
            left: (IO.PositionX + IO.MaxWidth / 2)* dfZ1l,
            top: (IO.PositionY + IO.MaxHeight / 2)* dfZ1l,
            angle: IO.RotationAngle
        });
        loadedObject.maxWidth = IO.MaxWidth;
        loadedObject.maxHeight = IO.MaxHeight;
        loadedObject.ObjectID = IO.ObjectID;
        loadedObject.fill = IO.ColorHex;
        loadedObject.scaleX = (loadedObject.maxWidth / loadedObject.width)* dfZ1l;
        loadedObject.scaleY = (loadedObject.maxHeight / loadedObject.height)* dfZ1l;
        loadedObject.setAngle(IO.RotationAngle);
        loadedObject.IsPositionLocked = IO.IsPositionLocked;
        loadedObject.IsOverlayObject = IO.IsOverlayObject;
        loadedObject.C = IO.ColorC;
        loadedObject.M = IO.ColorM;
        loadedObject.Y = IO.ColorY;
        loadedObject.K = IO.ColorK;
        loadedObject.IsHidden = IO.IsHidden;
        loadedObject.IsEditable = IO.IsEditable;
        loadedObject.IsTextEditable = IO.IsTextEditable;
        loadedObject.AutoShrinkText = IO.AutoShrinkText;
        loadedObject.setOpacity(IO.Opacity);
        if (IO.IsPositionLocked == true) {
            loadedObject.lockMovementX = true;
            loadedObject.lockMovementY = true;
            loadedObject.lockScalingX = true;
            loadedObject.lockScalingY = true;
            loadedObject.lockRotation = true;
        }

        loadedObject.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });
        if (IO.IsQuickText == true) {
            loadedObject.IsQuickText = true;
        }
        cCanvas.insertAt(loadedObject, IO.DisplayOrderPdf);

        TotalImgLoaded += 1;
        d2();
    });
}
function d1Svg(cCanvas, IO, isCenter) {
    TIC += 1;
    if (IO.MaxWidth == 0) {
        IO.MaxWidth = 50;
    }
    if (IO.MaxHeight == 0) {
        IO.MaxHeight = 50;
    }
    fabric.loadSVGFromURL(IO.ContentString, function (objects, options) {

        var loadedObject = fabric.util.groupSVGElements(objects, options);
        loadedObject.set({
            left: IO.PositionX + IO.MaxWidth / 2,
            top: IO.PositionY + IO.MaxHeight / 2,
            angle: IO.RotationAngle
        });
        loadedObject.maxWidth = IO.MaxWidth;
        loadedObject.maxHeight = IO.MaxHeight;
        loadedObject.ObjectID = IO.ObjectID;
        loadedObject.scaleX = loadedObject.maxWidth / loadedObject.width;
        loadedObject.scaleY = loadedObject.maxHeight / loadedObject.height;
        loadedObject.setAngle(IO.RotationAngle);
        loadedObject.IsPositionLocked = IO.IsPositionLocked;
        loadedObject.IsOverlayObject = IO.IsOverlayObject;
        loadedObject.IsHidden = IO.IsHidden;
        loadedObject.C = IO.ColorC;
        loadedObject.M = IO.ColorM;
        loadedObject.Y = IO.ColorY;
        loadedObject.K = IO.ColorK;
        loadedObject.fill = IO.ColorHex;
        loadedObject.IsEditable = IO.IsEditable;
        loadedObject.IsTextEditable = IO.IsTextEditable;
        loadedObject.AutoShrinkText = IO.AutoShrinkText;
        if (IO.IsPositionLocked == true) {
            loadedObject.lockMovementX = true;
            loadedObject.lockMovementY = true;
            loadedObject.lockScalingX = true;
            loadedObject.lockScalingY = true;
            loadedObject.lockRotation = true;
        }

        loadedObject.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });
        if (IO.IsQuickText == true) {
            loadedObject.IsQuickText = true;
        }
        if (isCenter == true) {
            cCanvas.centerObject(loadedObject);
        }
        cCanvas.insertAt(loadedObject, IO.DisplayOrderPdf);
        if (isCenter == true) {
            cCanvas.centerObject(loadedObject);
        }

        TotalImgLoaded += 1;
        d2();
    });
}
function d1(cCanvas, IO, isCenter) {
    TIC += 1;
    if (IO.MaxWidth == 0) {
        IO.MaxWidth = 50;
    }
    if (IO.MaxHeight == 0) {
        IO.MaxHeight = 50;
    }
    fabric.Image.fromURL(IO.ContentString, function (IOL) {
        IOL.set({
            left: (IO.PositionX + IO.MaxWidth / 2)* dfZ1l,
            top: (IO.PositionY + IO.MaxHeight / 2)* dfZ1l,
            angle: IO.RotationAngle
        });
        IOL.maxWidth = IO.MaxWidth;
        IOL.maxHeight = IO.MaxHeight;
        IOL.ObjectID = IO.ObjectID;
        IOL.scaleX = (IOL.maxWidth / IOL.width)* dfZ1l;
        IOL.scaleY = (IOL.maxHeight / IOL.height)* dfZ1l;
        IOL.setAngle(IO.RotationAngle);
        IOL.IsPositionLocked = IO.IsPositionLocked;
        IOL.IsOverlayObject = IO.IsOverlayObject;
        IOL.IsHidden = IO.IsHidden;
        IOL.IsEditable = IO.IsEditable;
        IOL.IsTextEditable = IO.IsTextEditable;
        IOL.AutoShrinkText = IO.AutoShrinkText;
        IOL.ImageClippedInfo = IO.ClippedInfo;
        IOL.setOpacity(IO.Opacity);
        if (IO.IsPositionLocked == true) {
            IOL.lockMovementX = true;
            IOL.lockMovementY = true;
            IOL.lockScalingX = true;
            IOL.lockScalingY = true;
            IOL.lockRotation = true;
        }
        IOL.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });
        if (IO.IsQuickText == true) {
            IOL.IsQuickText = true;
        }
        if (isCenter == true) {
            cCanvas.centerObject(IOL);
        }
        cCanvas.insertAt(IOL, IO.DisplayOrderPdf);
        if (isCenter == true) {
            cCanvas.centerObject(IOL);
        }
        TotalImgLoaded += 1;
        d2();
    });
}

function d2() {
    if (LIFT && TIC === TotalImgLoaded) {
        LIFT = false;
        isloadingNew = false;
        StopLoader();
        m0();
        $.each(TP, function (i, ite) {
            if (ite.ProductPageID == SP) {
                if (ite.Orientation == 1) {
                    d6(Template.PDFTemplateWidth * dfZ1l, Template.PDFTemplateHeight * dfZ1l, ISG1);
                }
                else {
                    d6(Template.PDFTemplateHeight * dfZ1l, Template.PDFTemplateWidth * dfZ1l, ISG1);
                }
            }
        });
    } else {
        if (TIC == TotalImgLoaded) {
            isloadingNew = false;
            StopLoader();
            m0();
        }
        $.each(TP, function (i, ite) {
            if (ite.ProductPageID == SP) {
                if (ite.Orientation == 1) {
                    d6(Template.PDFTemplateWidth * dfZ1l, Template.PDFTemplateHeight * dfZ1l, ISG1);
                }
                else {
                    d6(Template.PDFTemplateHeight * dfZ1l, Template.PDFTemplateWidth * dfZ1l, ISG1);
                }
            }
        });
    }
}
function d5(pageID, isloading) {
    undoArry = [];
    redoArry = [];
    firstLoad = false;
    bleedPrinted = false;
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    //e0("d5");
    canvas.renderAll();
    c2_v2();
    c2_v2();
    SP = pageID;
    $(".menuItemContainer").removeClass("selectedItem");
    $("." + pageID).addClass("selectedItem");
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == SP) {
            canvas.clear();
            canvas.setBackgroundImage(null, function (IOL) {
                canvas.renderAll(); //StopLoader();
            });
            canvas.backgroundColor = "#ffffff";
            
            if (IT.Orientation == 1) {
                canvas.setHeight(Template.PDFTemplateHeight * dfZ1l);
                canvas.setWidth(Template.PDFTemplateWidth * dfZ1l);
            }
            else {
                canvas.setHeight(Template.PDFTemplateWidth * dfZ1l);
                canvas.setWidth(Template.PDFTemplateHeight * dfZ1l);
            }
            $(".page").css("height", ((Template.PDFTemplateHeight * dfZ1l) + 20) + "px");
            $(".page").css("width", ((Template.PDFTemplateWidth * dfZ1l) + 0) + "px");
            var val = $("#canvaDocument").width() - $(".page").width();
            val = val / 2;
            if (val < 0) val = 20;
            $(".page").css("left", val + "px");
            //$(".page").css("left", (($("#canvaDocument").width() - $(".page").width()) / 2) + "px");
            //$("#addNewPage").css("top", (Template.PDFTemplateHeight + 150) + "px");
            //$("#addNewPage ").css("left", (($("#canvaDocument").width() - $("#addNewPage").width()) / 2) + "px");
            if (IT.BackgroundFileName != "") {
               
                if (IT.BackGroundType == 3) {
                    if (IT.BackgroundFileName.indexOf('Designer/Products/') == -1) {
                        IT.BackgroundFileName = "./Designer/Products/" + IT.BackgroundFileName;
                    }
                }
                if (IT.BackGroundType == 1) {
                    if (IT.BackgroundFileName.indexOf('Designer/Products/') == -1) {
                        IT.BackgroundFileName = "./Designer/Products/" + IT.BackgroundFileName;
                    }
                }
                var bk = IT.BackgroundFileName + "?r=" + CzRnd;
                if (IT.BackgroundFileName != "") {
                    if (!isloading) {
                        StartLoader("Loading background files for your design, please wait....");
                    }
                    canvas.setBackgroundImage(bk, canvas.renderAll.bind(canvas), {
                        left: 0,
                        top: 0,
                        height: canvas.getHeight(),
                        width: canvas.getWidth(),
                        maxWidth: canvas.getWidth(),
                        maxHeight: canvas.getHeight(),
                        originX: 'left',
                        originY: 'top'
                    });
                    //StopLoader();
                    canvas.renderAll();
                } else {
                    canvas.backgroundColor = "#ffffff";
                    canvas.setBackgroundImage(null, function (IOL) {
                        canvas.renderAll();
                        //StopLoader();
                    });
                }
            }
            if (IT.BackGroundType == 2) {
                canvas.setBackgroundImage(null, function (IOL) {
                    canvas.renderAll();
                    //StopLoader();
                });
                var colorHex = getColorHex(IT.ColorC, IT.ColorM, IT.ColorY, IT.ColorK);
                canvas.backgroundColor = colorHex;
                canvas.renderAll();
            }
            c7(pageID);
            canvas.calcOffset();
        }
    });
}
function d6(width, height, showguides) {

    if (showguides && !bleedPrinted) {
        bleedPrinted = true;
        var cutmargin = Template.CuttingMargin * dfZ1l;
        if (udCutMar != 0) {
            cutmargin = udCutMar * dfZ1l;
        }
        var leftline = i4([0, 0, 0, cutmargin + height - cutmargin], -980, 'white', cutmargin * 2);
        var topline = i4([cutmargin + 0.39, 0, cutmargin + width - 0.39 - cutmargin * 2, 0], -981, 'white', cutmargin * 2);
        var rightline = i4([width - 1, 0, width - 1, cutmargin + height - cutmargin], -982, 'white', cutmargin * 2);
        var bottomline = i4([cutmargin + 0.39, height, cutmargin + width - 0.39 - cutmargin * 2, height], -983, 'white', cutmargin * 2);

        var topCutMarginTxt = i5((14 * dfZ1l), width / 2, 17, 100, 10, 'Bleed Area', -975, 0, 'gray');
        var leftCutMarginTxt = i5(height / 2, width - (12 * dfZ1l), 17, 100, 10, 'Bleed Area', -974, 90, 'gray');
        var rightCutMarginTxt = i5(height / 2, (13 * dfZ1l), 17, 100, 10, 'Bleed Area', -973, -90, 'gray');
        var bottomCutMarginTxt = i5(height - 6, width / 2, 17, 100, 10, 'Bleed Area', -972, 0, 'gray');
        var zafeZoneMargin = cutmargin; // + 8.49;
        var sleftline = d7([zafeZoneMargin, zafeZoneMargin, zafeZoneMargin, zafeZoneMargin + height - zafeZoneMargin * 2], -984, 'gray');
        var stopline = d7([zafeZoneMargin, zafeZoneMargin, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin], -985, 'gray');
        var srightline = d7([zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin + height - zafeZoneMargin * 2], -986, 'gray');
        var sbottomline = d7([zafeZoneMargin, zafeZoneMargin + height - zafeZoneMargin * 2, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin + height - zafeZoneMargin * 2], -987, 'gray');
        canvas.add(leftline, topline, rightline, bottomline, sleftline, stopline, srightline, sbottomline, topCutMarginTxt, bottomCutMarginTxt, leftCutMarginTxt, rightCutMarginTxt);
    }

    var iCounter = 1;
    while (iCounter < width) {
        SXP.push(iCounter);
        iCounter += 5;
    }

    iCounter = 1;
    while (iCounter < height) {
        SYP.push(iCounter);
        iCounter += 5;
    }

}
function d7(coords, ObjectID, color) {
    var line = new fabric.Line(coords,
        {
            fill: color, strokeWidth: 1, selectable: false
        });

    line.ObjectID = ObjectID;
    return line;
}
function e0(caller) {
    canvas.setHeight(canvas.getHeight() * (1 / D1CS));
    canvas.setWidth(canvas.getWidth() * (1 / D1CS));
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        var scaleX = IT.scaleX;
        var scaleY = IT.scaleY;
        var left = IT.left;
        var top = IT.top;
        var tempScaleX = scaleX * (1 / D1CS);
        var tempScaleY = scaleY * (1 / D1CS);
        var tempLeft = left * (1 / D1CS);
        var tempTop = top * (1 / D1CS);
        IT.scaleX = tempScaleX;
        IT.scaleY = tempScaleY;
        IT.left = tempLeft;
        IT.top = tempTop;
        IT.setCoords();
        if (caller != "d5") {
            //  e9(IT);
        }

    });
    D1CS = 1;
    D1CS = 1;
    canvas.setHeight(canvas.getHeight() * (1 / D1CS));
    canvas.setWidth(canvas.getWidth() * (1 / D1CS));
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        var scaleX = IT.scaleX;
        var scaleY = IT.scaleY;
        var left = IT.left;
        var top = IT.top;
        var tempScaleX = scaleX * (1 / D1CS);
        var tempScaleY = scaleY * (1 / D1CS);
        var tempLeft = left * (1 / D1CS);
        var tempTop = top * (1 / D1CS);
        IT.scaleX = tempScaleX;
        IT.scaleY = tempScaleY;
        IT.left = tempLeft;
        IT.top = tempTop;
        IT.setCoords();
        if (caller != "d5") {
            //   e9(IT);
        } else {
            //  e8(IT);
        }
    });
    if (canvas.backgroundImage) {
        canvas.backgroundImage.left = 0;
        canvas.backgroundImage.top = 0;
        canvas.backgroundImage.height = canvas.getHeight();
        canvas.backgroundImage.width = canvas.getWidth();
        canvas.backgroundImage.maxWidth = canvas.getWidth();
        canvas.backgroundImage.maxHeight = canvas.getHeight();
        canvas.backgroundImage.originX = 'left';
        canvas.backgroundImage.originY = 'top';
    }
    D1CS = 1;
    D1CS = 1;
    D1CZL = 0;
}
function e6() {
    pcL36('hide', '#PreviewerContainer');
    $('.opaqueLayer').css("display", "none");
    $('.opaqueLayer').css("background-color", "transparent");

    if (IsCalledFrom == 3 || IsCalledFrom == 4) {
        //parent.ShowTopBars();
    }
}
function fu01(a) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var b = sURLVariables[i].split('=');
        if (b[0] == a) {
            return b[1];
        }
    }
}
function fu02UI() {
    
    CzRnd = fabric.util.getRandomInt(1, 100);
    $("#documentMenu li").hover(function () {
        $el = $(this);
        leftPos = $el.position().left;
        newWidth = $el.width();
        $(".marker").stop().animate({
            left: leftPos,
            width: newWidth
        })
    }, function () {
        $el = $('.selectedItem');
        leftPos = $el.position().left;
        newWidth = $el.width();
        $(".marker").stop().animate({
            left: leftPos,
            width: newWidth
        });
    });
    var height = $(window).height();
    $('.scrollPane').slimscroll({
        height: height
    });
    $('.resultLayoutsScroller').slimscroll({
        height: height
    }).bind('slimscroll', function (e, pos) {
        if (pos == "bottom") {
            fu09();
        }
    });
    $('.scrollPaneImgDam').slimscroll({
        height: height + 320
    }).bind('slimscrolling', function (e, pos) {
        var he = $(".scrollPaneImgDam").parent().find(".slimScrollBar").height();
        // var val = pos + he;
        var val = parseInt(he + ($(".scrollPaneImgDam").parent().find(".slimScrollBar").position().top));
        var winHeight = $("#resultsSearch").height();
        if (isImgPaCl) {
            if (selCat == "11") {
                val +=265;
                if (val > winHeight) { 
                    k21();
                }
            } else if (selCat == "12") {
                val += 265;
                if (val > winHeight) {
                 //   k24ilus();
                }
            } else if (selCat == "13") {
                val += 265;
                if (val > winHeight) {
                   // k24frames();
                }
            } else if (selCat == "21") {
                val += 135;
                if (val > winHeight) {
                  //  k24banners();
                }
            } else if (selCat == "22") {
                val += 135;
                if (val > winHeight) {
                 //   k21Sh();
                }
            } else if (selCat == "23") {
                val += 135;
                if (val > winHeight) {
                 //   k21Log();
                }
            } else if (selCat = "31") {
                val += 15;
                if (val > winHeight) {
                    k17();
                }
            }
        }
     
       
    });
    $('.bkDamScroller').slimscroll({
        height: height
    }).bind('slimscroll', function (e, pos) {
        if (pos == "bottom") {
            if (isBkPaCl) {
                if (SelBkCat == "11") {
                    k21Bk();
                } else if (SelBkCat == "12") {
                    k24Bk();
                } else if (SelBkCat == "13") {
                    k17Bk(); 
                }
            } 
        }
    });
    $('#divTempBkImgContainer').css("height", "540px !important");
    $('.upDamScroller').slimscroll({
        height: height
    }).bind('slimscroll', function (e, pos) {
        if (pos == "bottom") {
            if (isUpPaCl) {
                if (SelUpCat == "11") {
                    k24();
                } else if (SelUpCat == "12") {
                    k21PLog();
                }
            } 
        }
    });
    $("#BtnFontSizeRetail").spinner({
        step: 0.50,
        numberFormat: "n",
        change: function (event, ui) {
            var fz = $('#BtnFontSizeRetail').val();
            k12(fz);
        },
        stop: function (event, ui) {
            var fz = $('#BtnFontSizeRetail').val();
            k12(fz);
        }
    });
    $("#DivColorC,#DivColorM,#DivColorY, #DivColorK").slider({
        orientation: "horizontal",
        range: "min",
        max: 100,
        change: f4,
        slide: f4
    });
    $(".transparencySlider").slider({
        range: "min",
        value: 1,
        min: 1,
        max: 100,
        slide: function (event, ui) {
            k7_trans_retail(ui.value);

        }
    });
    $(".inputObjectAlphaSlider").slider({
        range: "min",
        value: 1,
        min: 1,
        max: 100,
        slide: function (event, ui) {
            k7_trans_retail(ui.value);

        }
    });
    $(".CaseModeSlider").slider({
        range: "min",
        value: 1,
        step:33,
        min: 1,
        max: 100,
        slide: function (event, ui) {
             k7_Case_Force(ui.value);

        }
    });

    $(".rotateSlider").slider({
        range: "min",
        value: 0,
        min: 0,
        max: 360,
        step:5,
        slide: function (event, ui) {
            k7_rotate_retail(ui.value);

        }
    });
    $(".rotateSliderTxt").slider({
        range: "min",
        value: 0,
        min: 0,
        max: 360,
        step: 5,
        slide: function (event, ui) {
            k7_rotate_retail(ui.value);

        }
    });
    $("#LargePreviewer").dialog({
        autoOpen: false,
        height: $(window).height() - 40,
        width: $(window).width() - 40,
        show: {
            effect: "blind",
            duration: 300
        },
        hide: {

        },
        close: function (event, ui) {
            $("#DivShadow").css("z-Index", "100000");
            $("#DivShadow").css("display", "block");
        }
    });
    var draggable = $(".LargePreviewer").dialog("option", "draggable");
    $(".LargePreviewer").dialog("option", "draggable", false);
    $(".LargePreviewerIframe").css("width", $(window).width() - 70);
    $(".LargePreviewerIframe").css("height", $(window).height() - 80);
    $("#canvaDocument").scroll(function () {
        canvas.calcOffset();
    });
    $(".add").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move', cancel: false
    });
    $("#canvas").droppable({
        activeClass: "custom-state-active",
        accept: function (dropElem) {
            var draggable = dropElem.attr('id');
            if (draggable == "BtnAddNewText") {
                //if ($("#IsQuickTxtCHK").is(':checked')) {
                //    var val1 = $("#txtQTitleChk").val();
                //    if (val1 == "") {
                //        return false;
                //    } else {
                //        return true;
                //    }
                //} else {
                //    return true;
                //}

            } else if (dropElem.attr('src')) {
                var D1AO = canvas.getActiveObject();
                if (D1AO) {
                    return false;
                } else {
                    return true;
                }
            } else {
                return true;
            }
        },
        drop: function (event, ui) {
            if (ui.draggable.attr('src')) {
                if (ui.draggable.attr('class') == "btnEditImg") {
                } else {
                    if (ui.draggable.attr('class').indexOf("bkImg") != -1) {
                        var src = "" + ui.draggable.attr('src');
                        var id = ui.draggable.attr('id');
                        k32(id, tID, src);
                    } else if (ui.draggable.attr('class').indexOf("CustomRectangleObj") != -1) {
                        var pos = canvas.getPointer(event); h1(pos.x, pos.y);
                    } else if (ui.draggable.attr('class').indexOf("CustomCircleObj") != -1) {
                        var pos = canvas.getPointer(event); h2(pos.x, pos.y);
                    } else {
                        var pos = canvas.getPointer(event);
                        var currentPos = ui.helper.position();
                        var draggable = ui.draggable;
                        var url = "";
                        if (draggable.attr('src').indexOf('.svg') == -1) {
                            var p = draggable.attr('src').split('_thumb.');
                            url += p[0] + "." + p[1];

                        } else {
                            url = draggable.attr('src');
                        }
                        b4(url);
                        if (url.indexOf(".svg") == -1) {
                            d1ToCanvas(url, pos.x, pos.y, IW, IH);
                        } else {
                            d1SvgToCCC(url, IW, IH);
                        }
                    }
                }
            }
            else if (ui.draggable.attr('class') == "ui-state-default ui-sortable-helper" || ui.draggable.attr('id') == "sortableLayers" || ui.draggable.attr('id') == "DivLayersPanel" || ui.draggable.attr('id') == "divLayersPanelRetail" || ui.draggable.attr('id') == "ImagePropertyPanel" || ui.draggable.attr('id') == "DivColorPickerDraggable" || ui.draggable.attr('id') == "quickTextFormPanel" || ui.draggable.attr('id') == "AddTextDragable" || ui.draggable.attr('id') == "addImage" || ui.draggable.attr('id') == "divImageDAM" || ui.draggable.attr('id') == "divImageEditScreen" || ui.draggable.attr('id') == "DivControlPanelDraggable" || ui.draggable.attr('id') == "DivAlignObjs" || ui.draggable.attr('id') == "divPositioningPanel" || ui.draggable.attr('id') == "divVariableContainer" || ui.draggable.attr('id') == "LayerObjectsContainerRetail") {
                //l4
            } else {
                var pos = canvas.getPointer(event);
                var draggable = ui.draggable.attr('id');
                //if (draggable == "QuickTxtAllFields") {
                  //  e0(); // l3
                  //  g9(draggable, pos.x, pos.y);
                //} else
                if (draggable == "addTxtHeading") {
                    g0(pos.x, pos.y, false, "", "", "", "Add text", 26.67, true);
                } else if (draggable == "addTxtSubtitle") {
                    g0(pos.x, pos.y, false, "", "", "", "Add subtitle text", 21.33, false);
                } else if (draggable == "addTxtBody") {
                    g0(pos.x, pos.y, false, "", "", "", "Add a little bit of body text", 13.33, false);
                }
              
            }
        }
    });
    $(".PreviewerDownloadPDF").css("display", "none");
    $("#divLayersPanelRetail").draggable({
        appendTo: "body",
        cursor: 'move',
        cancel: "div #LayerObjectsContainerRetail"
    });
    $("#getCopied").bind('paste', function (e) {
        var elem = $(this);

        setTimeout(function () {
            var text = elem.val();
            elem.val('').blur();
            canvas.getActiveObject().insertChars(text);
            e.preventDefault();
            e.stopPropagation();
            this.canvas && this.canvas.renderAll();
        }, 100);
    });
    if (panelMode == 1) {
        $("#inputcharSpacing").spinner({
            step: 0.1,
            numberFormat: "n",
            change: k8,
            stop: k8
        });
        $("#BtnFontSize").spinner({
            step: 0.50,
            numberFormat: "n",
            change: function (event, ui) {
                var fz = $('#BtnFontSize').val();
                k12(fz);
            },
            stop: function (event, ui) {
                var fz = $('#BtnFontSize').val();
                k12(fz);
            }
        });
        $("#txtLineHeight").spinner({
            step: 0.01,
            numberFormat: "n",
            change: k15,
            stop: k15
        });
        $("#inputObjectWidthTxt").spinner({
            change: k7,
            stop: k7
        });
        $("#inputObjectHeightTxt").spinner({
            change: k6,
            stop: k6
        });
        $("#inputPositionXTxt").spinner({
            change: k5,
            stop: k5
        });
        $("#inputPositionYTxt").spinner({
            change: k5,
            stop: k5
        });
        $("#inputObjectWidth").spinner({
            change: k7,
            stop: k7
        });
        $("#inputObjectAlpha").spinner({
            change: k7_trans,
            stop: k7_trans
        });
        $("#inputObjectHeight").spinner({
            change: k6,
            stop: k6
        });
        $("#inputPositionX").spinner({
            change: k5,
            stop: k5
        });
        $("#inputPositionY").spinner({
            change: k5,
            stop: k5
        });
    }
}
function fu02() {
    cID = parseInt(fu01('c'));
    cIDv2 = parseInt(fu01('cv2'));
    tID = parseInt(fu01('t'));
    printCM = (fu01('cm'));
    printWM = (fu01('wm'));
    CustomerID = parseInt(fu01('CustomerID'));
    ContactID = parseInt(fu01('ContactID'));
    ItemId = parseInt(fu01('ItemId'));
    fu09();// called for retail store only
    if (tID == 0) {
        fu03();
    } else {
        fu04();
    }
    fu05_Cl();
    $('input:checkbox').focus(function () {
        var D1A0 = canvas.getActiveObject();
        if (!D1A0) return;
        if (D1A0.isEditing) {
            D1A0.exitEditing(); canvas.renderAll();
        }
    });
    canvas = new fabric.Canvas('canvas', {
    });
    canvas.findTarget = (function (originalFn) {
        return function () {
            var TG = originalFn.apply(this, arguments);
            if (TG) {
                if (this._hoveredTarget !== TG) {
                    canvas.fire('object:over', { TG: TG });
                    if (this._hoveredTarget) {
                        canvas.fire('object:out', { TG: this._hoveredTarget });
                    }
                    this._hoveredTarget = TG;
                }
            }
            else if (this._hoveredTarget) {
                canvas.fire('object:out', { TG: this._hoveredTarget });
                this._hoveredTarget = null;
            } return TG;
        };
    })(canvas.findTarget);

    canvas.on('object:over', function (e) {
        if (e.TG.IsQuickText == true && e.TG.type == 'image' && e.TG.getWidth() > 112 && e.TG.getHeight() > 64) {
            $("#placeHolderTxt").css("visibility", "visible")
            var width = 51;//$("#placeHolderTxt").width() / 2;
            var height = 23;// $("#placeHolderTxt").height() / 2;
            $("#placeHolderTxt").css("left", ($(window).width() / 2 - canvas.getWidth() / 2 + 212+ e.TG.left - width) + "px");
            $("#placeHolderTxt").css("top", ( e.TG.top +103 - height/2) + "px");
        } else {
            $("#placeHolderTxt").css("visibility", "hidden");
        }
    });

    canvas.on('object:out', function (e) {
        if (e.TG.IsQuickText == true && e.TG.type == 'image') {
            $("#placeHolderTxt").css("visibility", "hidden");
        }
    });

    //    canvas.observe('mouse:down', onMouseDown);
    //    function onMouseDown(e) {
    //        //animatedcollapse.hide(['addText', 'DivAdvanceColorPanel','DivColorPallet','addImage', 'quickText','UploadImage', 'ImagePropertyPanel', 'ShapePropertyPanel','textPropertPanel']);
    //    }


    canvas.observe('object:modified', g3);
    canvas.observe('text:changed', g4);




    canvas.observe('object:selected', g5);
    //	canvas.observe('group:selected', g4);
    fabric.util.addListener(fabric.document, 'dblclick', j4);
    // fabric.util.removeListener(canvas.upperCanvasEl, 'dblclick', j4);
    canvas.observe('object:moving', g6);
    canvas.observe('selection:cleared', function (e) {
        pcL36('hide', '#divImgPropPanelRetail , #divTxtPropPanelRetail ,#DivColorPickerDraggable ');
        $("#sortableLayers li").removeClass("selectedItemLayers");

    });
}

function fu04_callBack(DT) {
    Template = DT;
    tID = Template.ProductID;
    $("#txtTemplateTitle").val(Template.ProductName);
    $.each(Template.TemplatePages, function (i, IT) {
        TP.push(IT);
    });
    if (Template.TemplateType == 1 || Template.TemplateType == 2) {
        IsBC = true
    } else {
        IsBC = false;

    }
    Template.TemplatePages = [];
    fu04_01();
    fu14();
    b3_1();
}

function fu05_svcCall(DT) {
    $.each(DT, function (i, IT) {
        fu05_ClHtml(IT.ColorC, IT.ColorM, IT.ColorY, IT.ColorK, IT.SpotColor, IT.IsColorActive, IT.PelleteID);
    });
    var html = '<li class="picker" id="BtnAdvanceColorPicker" style="display: list-item;" onclick="return f6_1(); "><a>Add a color</a></li>';
    $('.ColorOptionContainer').append(html);
}
function fu05_ClHtml(c, m, y, k, Sname, IsACT, PID) {
    var Color = getColorHex(c, m, y, k);
    var html = "";
    html = '<li class="colorOption ColorPallet" style="background-color:' + Color + '" onclick="f2(' + c + ',' + m + ',' + y + ',' + k + ',&quot;' + Color + '&quot;' + ',&quot;' + Sname + '&quot;);"' + '"><a href="#">' + Color + '</a></li>';
    $('.ColorOptionContainer').append(html);

}
function fu05_SvcCallback(xdata) {
    QTD = xdata;

    if (QTD.Name == "" || QTD.Name == null) {
        QTD.Name = "Your Name"
    }
    if (QTD.Title == "" || QTD.Title == null) {
        QTD.Title = "Your Title"
    }
    if (QTD.Company == "" || QTD.Company == null) {
        QTD.Company = "Your Company Name"
    }
    if (QTD.CompanyMessage == "" || QTD.CompanyMessage == null) {
        QTD.CompanyMessage = "Your Company Message"
    }
    if (QTD.Address1 == "" || QTD.Address1 == null) {
        QTD.Address1 = "Address Line 1"
    }
    if (QTD.Telephone == "" || QTD.Telephone == null) {
        QTD.Telephone = "Telephone / Other"
    }
    if (QTD.Fax == "" || QTD.Fax == null) {
        QTD.Fax = "Fax / Other"
    }
    if (QTD.Email == "" || QTD.Email == null) {
        QTD.Email = "Email address / Other"
    }
    if (QTD.Website == "" || QTD.Website == null) {
        QTD.Website = "Website address"
    }

    if (QTD.MobileNumber == "" || QTD.MobileNumber == null) {
        QTD.MobileNumber = "Mobile number"
    }
    if (QTD.FacebookID == "" || QTD.FacebookID == null) {
        QTD.FacebookID = "Facebook ID"
    }
    if (QTD.TwitterID == "" || QTD.TwitterID == null) {
        QTD.TwitterID = "Twitter ID"
    }
    if (QTD.LinkedInID == "" || QTD.LinkedInID == null) {
        QTD.LinkedInID = "LinkedIn ID"
    }
    if (QTD.OtherId == "" || QTD.OtherId == null) {
        QTD.OtherId = "Other ID"
    }

    var AQTD = [];
    var NameArr = [];
    var HM = "";
    var hQText = false;
    $.each(TO, function (i, IT) {
        if (IT.IsQuickText == true && IT.ObjectType != 3 && IT.ObjectType != 8 && IT.ObjectType != 12) {
            if (IT.watermarkText == null || IT.watermarkText == "null" || IT.watermarkText == "") {
                IT.watermarkText = IT.ContentString;
            }
            var obj = {
                Order: IT.QuickTextOrder,
                Name: IT.Name,
                ContentString: IT.ContentString,
                watermarkText: IT.watermarkText
            }
            if ($.inArray(IT.Name, NameArr) == -1) {
                if (IT.IsEditable != false) {   // show only editable text
                    NameArr.push(IT.Name);
                    AQTD.push(obj);
                }
            }
        }
    });
    AQTD.sort(function (obj1, obj2) {
        return obj1.Order - obj2.Order;
    });
    if (AQTD.length >= 1) {
        TOFZ = AQTD[AQTD.length - 1].Order + 1;
        //alert(TOFZ);
    }
    $.each(AQTD, function (i, ITOD) {
        var id = ITOD.Name.split(' ').join('');
        id = id.replace(/\W/g, '');
        HM += '<div class="QtextData"><label class="lblQData" id ="lblQ' + id + '" >' + ITOD.Name + '</label><br/><input id="txtQ' + id + '" maxlength="500" class="qTextInput" style=""></div>';

    });
    HM += '<div class="clear"></div><div><a id="BtnQuickTextSave" title="Save" style=" width: 299px;margin:auto;" class="SampleBtn"><span class="onText">Save</span> </a> </div>'
    $(".QuickTextFields").append(HM);
    $.each(AQTD, function (i, ITOD) {
        var id = ITOD.Name.split(' ').join('');
        id = id.replace(/\W/g, '');
        $("#txtQ" + id).attr("placeholder", ITOD.watermarkText);
        $("#txtQ" + id).val(ITOD.ContentString);
        //  $("#lblQ" + id).val(ITOD.Name);
        var tn = "txtQ" + id;
        var addEvent = function (elem, type, fn) {
            if (elem.addEventListener) elem.addEventListener(type, fn, false);
            else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
        },
        textField = document.getElementById(tn),
        text = ITOD.ContentString,
        placeholder = ITOD.watermarkText;
        addEvent(textField, 'focus', function () {
            if (this.value === placeholder) this.value = '';
        });
        addEvent(textField, 'blur', function () {
            if (this.value === '') this.value = placeholder;
        });

    });
    $("#txtQName").val(QTD.Name);
    $("#txtQTitle").val(QTD.Title);
    $("#txtQCompanyName").val(QTD.Company);
    $("#txtQCompanyMessage").val(QTD.CompanyMessage);
    $("#txtQAddressLine1").val(QTD.Address1);
    $("#txtQPhone").val(QTD.Telephone);
    $("#txtQFax").val(QTD.Fax);
    $("#txtQEmail").val(QTD.Email);
    $("#txtQWebsite").val(QTD.Website);
    $("#txtQOtherID").val(QTD.OtherId);
    $("#txtQLinkedIn").val(QTD.LinkedInID);
    $("#txtQFacebook").val(QTD.FacebookID);
    $("#txtQTwitter").val(QTD.TwitterID);
    $("#txtQMobile").val(QTD.MobileNumber);

    $("#BtnQuickTextSave").click(function (event) {
        fu11();
    });
}
function fu06_SvcCallback(DT, fname) {
    $.each(DT, function (i, IT) {
        b1(fname, IT.FontName, IT.FontName);
        a0(IT.FontName, IT.FontFile, IT.FontPath);
        h8(IT.FontName, IT.FontFile, IT.FontPath);
    });
    h9();
    var selName = "#" + fname;
    $(selName).fontSelector({

        fontChange: function (e, ui) {
            // Update page title according to the font that's set in the widget options:
            //pcL04(1);
        },
        styleChange: function (e, ui) {
            // Update page title according to what's set in the widget options:
            // pcL04(1);
        }
    });
    $('div.fontSelector h4:nth-child(3)').css("display", "none");
    $('div.fontSelector h4:nth-child(2)').css("display", "none");
    //var he = $('#canvasSection').height() + 100 ;
    //$(".menusContainer").css("height", he + "px");
    //  $(selName).fontSelector('option', 'font', 'Arial Black');
    $('.scrollPane2').slimscroll({
        height: $(window).height()

    }).bind('slimscrolling', function (e, pos) {
        canvas.calcOffset();
    });
    $("#canvaDocument").css("width", $(window).width() - 430);
    d5(TP[0].ProductPageID, true);
}
function fu07() {
    var dm = '<span class="marker" style="left: 0px; width: 72px;"></span>';
    $("#documentMenu").html(dm);
    var pHtml = "";
    $.each(TP, function (i, ite) {
        var classes = "menuItemContainer " + ite.ProductPageID + " ";
        if (i == 0) {
            classes = "menuItemContainer selectedItem " + ite.ProductPageID + " ";
        }
        pHtml += '  <li  class="' + classes + '"><a class="plain" onClick="d5(' + ite.ProductPageID + ')">Page ' + (i + 1) + '</a></li>';
    });
    $("#documentMenu").append(pHtml);
    $("#documentMenu li").hover(function () {
        $el = $(this);
        leftPos = $el.position().left;
        newWidth = $el.width();
        $(".marker").stop().animate({
            left: leftPos,
            width: newWidth
        })
    }, function () {
        $el = $('.selectedItem');
        leftPos = $el.position().left;
        newWidth = $el.width();
        $(".marker").stop().animate({
            left: leftPos,
            width: newWidth
        });
    });
}

function fu09_SvcCallBack(DT) {
    if (DT != "") {
        tcListCc++;
        // load image size 
        if (tcListCc == 1) {
            for (var line in DT[0]) {
                var html = '<span id="demotestcsss" class="templateGallerylist"><a  >' +
                              '<img src="' + V2Url + '/designer/products/' + line + '/TemplateThumbnail1.jpg' + '" class="imgsdtsss"> </a></span>'

                $(".templateListUL").append(html);
                $("img.imgsdtsss").load(function () {
                    var height = $(this).height();
                    tcImHh = height + 10;
                    //  tcImThh = tcImHh;
                    tcRowCount = 0;
                    fu09_1(DT); $("#demotestcsss").remove();
                }).error(function () {
                    tcRowCount = 0;
                    fu09_1(DT); $("#demotestcsss").remove();
                });

            }
        } else {
            fu09_1(DT);
        }
    } else {
        tcAllcc = true;
        stopInlineLoader();
    }
}
function fu09_1(DT) {

    $.each(DT, function (key, val) {
        for (var line in val) {
            //tcRowCount++;
            //if (tcRowCount % 2 !=0 &&   tcRowCount != 1) {
            //    tcImThh += tcImHh;
            //}
            //var top = tcImThh;
            //var left = tcLltemp * 200
          
            var html = '<span class="templateGallerylist"><a title="' + val[line] + '" onClick="fu10(this,' + line + ')">' +
                  '<img src="' + V2Url + '/designer/products/' + line + '/TemplateThumbnail1.jpg' + '" class="imgs' + line + '"> </a></span>'

         $(".templateListUL").append(html);
           // tcLltemp++;
            //tcRowCount = tcRowCount + 0.50;
            //var csHe = tcImThh + tcImHh + 10;
            //$(".resultLayoutsScroller .inner").css("height", csHe + "px");
        }

    });
    stopInlineLoader(); tcAllcc = false;
}
function fu10(ca,gtID) {
     $(".templateListUL .on").removeClass("on");
     $(ca).parent().addClass("on");
     StartLoader("Downloading images and text objects for your design., please wait....");
     TP = [];
     TO = [];
     isloadingNew = true;
     svcCall1(ca, gtID);
}
function fu14() {
    k16(1, TeImC, "Loader");
    k16(12, TeImCBk, "Loader");
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(2, GlImC, "Loader");
        k16(3, GlImCBk, "Loader");
        k16(17, GlLogCn, "Loader");
      //  k16(16, GlShpCn, "Loader");
        if (IsCalledFrom == 4) {
            k16(4, UsImC, "Loader");
            k16(5, UsImCBk, "Loader");
        }

    }

    if (IsCalledFrom == 1 || IsCalledFrom == 3) {

        if (IsCalledFrom == 3) {
            k16(8, UsImC, "Loader");
            k16(6, GlImC, "Loader");
            k16(9, UsImCBk, "Loader");
            k16(7, GlImCBk, "Loader");
            k16(15, GlLogCnP, "Loader");
         //   k16(14, GlLogCn, "Loader");
          //  k16(13, GlShpCn, "Loader");

          //  k16(18, GlLogCnP, "Loader");
         //   k16(19, GlLogCn, "Loader");
         //   k16(20, GlShpCn, "Loader");
         }
        if (IsCalledFrom == 1) {
            if (CustomerID != -999) {
             k16(10, GlImC, "Loader");
                k16(11, GlImCBk, "Loader");
            } else {
               k16(6, GlImC, "Loader");
                k16(7, GlImCBk, "Loader");
              //  k16(14, GlLogCn, "Loader");
             //   k16(13, GlShpCn, "Loader");
            //    k16(18, GlLogCnP, "Loader");
            //    k16(19, GlLogCn, "Loader");
            //    k16(20, GlShpCn, "Loader");
              }
        }
    }
}
function fu15() {
    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "rectangle";
    D1NTO.ContentString = "rectangle";
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ObjectType = 6; //rectangle
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 200;
    D1NTO.MaxHeight = 200;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    var ROL = new fabric.Rect({
        left: 0,
        top: 0,
        fill: '#000000',
        width: 100 * dfZ1l,
        height: 100 * dfZ1l,
        opacity: 1
    })

    ROL.maxWidth = 200;
    ROL.maxHeight = 200;
    ROL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });

    ROL.ObjectID = D1NTO.ObjectID;
    canvas.add(ROL);

    canvas.centerObject(ROL);
    // getting object index
    var index;
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        if (IT.ObjectID == ROL.ObjectID) {
            index = i;
        }
    });
    D1NTO.DisplayOrderPdf = index;

    D1NTO.PositionX = ROL.left - ROL.maxWidth / 2;
    D1NTO.PositionY = ROL.top - ROL.maxHeight / 2;
    ROL.setCoords();

    ROL.C = "0";
    ROL.M = "0";
    ROL.Y = "0";
    ROL.K = "100";
    canvas.renderAll();
    TO.push(D1NTO);
}
function fu16() {
    var NewCircleObejct = {};
    NewCircleObejct = fabric.util.object.clone(TO[0]);
    NewCircleObejct.Name = "Ellipse";
    NewCircleObejct.ContentString = "Ellipse";
    NewCircleObejct.ObjectID = --NCI;
    NewCircleObejct.ColorHex = "#000000";
    NewCircleObejct.ColorC = 0;
    NewCircleObejct.ColorM = 0;
    NewCircleObejct.ColorY = 0;
    NewCircleObejct.ColorK = 100;

    NewCircleObejct.IsItalic = false;
    NewCircleObejct.ObjectType = 7; //ellipse/circle
    NewCircleObejct.ProductPageId = SP;
    NewCircleObejct.MaxWidth = 100;
    NewCircleObejct.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    NewCircleObejct.CircleRadiusX = 50;
    NewCircleObejct.CircleRadiusY = 50;
    NewCircleObejct.Opacity = 1;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        NewCircleObejct.IsSpotColor = true;
        NewCircleObejct.SpotColorName = 'Black';
    }
    var COL = new fabric.Ellipse({
        left: 0,
        top: 0,
        fill: '#000000',
        rx: 50 * dfZ1l,
        ry: 50 * dfZ1l,
        opacity: 1
    })

    COL.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });

    COL.ObjectID = NewCircleObejct.ObjectID;
    canvas.add(COL);
    canvas.centerObject(COL);


    // getting object index
    var index;
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        if (IT.ObjectID == COL.ObjectID) {
            index = i;
        }
    });
    NewCircleObejct.DisplayOrderPdf = index;

    NewCircleObejct.PositionX = COL.left - COL.width / 2;
    NewCircleObejct.PositionY = COL.top - COL.width / 2;
    COL.setCoords();
    COL.C = "0";
    COL.M = "0";
    COL.Y = "0";
    COL.K = "100";
    canvas.renderAll();
    TO.push(NewCircleObejct);
}
function h8(FN, FF, FP) {
    var p = "";
    p = "/DesignEngine/";
    if (jQuery.browser.msie) {
        T0FN.push(FN);
        n = p + FF + ".woff";
        T0FU.push(n);
    } else if (jQuery.browser.Chrome) {
        T0FN.push(FN);
        n = p + FF + ".woff";
        T0FU.push(n);
    } else if (jQuery.browser.Safari || jQuery.browser.opera || jQuery.browser.mozilla) {
        T0FN.push(FN);
        n = p + FF + ".ttf";
        T0FU.push(n);
    } else {
        T0FN.push(FN);
        n = p + FF + ".eot";
        T0FU.push(n);

        T0FN.push(FN);
        n = p + FF + ".woff";
        T0FU.push(n);

        T0FN.push(FN);
        n = p + FF + ".ttf";
        T0FU.push(n);
    }

}
function h9() {
    WebFontConfig = {
        custom: {
            families: T0FN,
            urls: T0FU
        },
        active: function () {
            // stop loading and  load page
        },
        inactive: function () {
            alert("error while loading fonts");
        }
    };
    var wf = document.createElement('script');
    // wf.src = "js/webfont.js"
    wf.src = ('https:' == document.location.protocol ? 'https' : 'http') +
        '://ajax.googleapis.com/ajax/libs/webfont/1/webfont.js';
    wf.type = 'text/javascript';
    wf.async = 'true';
    var s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(wf, s);
}
function i2(cs) {
    var length = TO.length;
    var TempTo = TO;
    var TempIdo = [];
    for (i = 0; i < length ; i++) {
        if (TempTo[i] != null || TempTo[i] != undefined) {
            if (TempTo[i].IsQuickText != true) {
                if (TempTo[i].ContentString == cs || TempTo[i].ContentString == "./" + cs) {

                    TempIdo.push(TempTo[i].ObjectID);
                }
            } else {
                $.each(TO, function (j, ite) {
                    if (ite.ObjectID == TempTo[i].ObjectID) {
                        if (TempTo[i].ContentString == cs || TempTo[i].ContentString == "./" + cs) {
                            ite.ContentString = "./assets/Imageplaceholder.png";
                        }
                    }
                });
            }
        }
    }
    $.each(TempIdo, function (i, IT) {
        i3(IT);
    });
    d5(SP);
}
function i4(coords, ObjectID, color, cutMargin) {
    var line = new fabric.Line(coords,
        {
            fill: color, strokeWidth: cutMargin, selectable: false, opacity: 0.5, border: 'none'
        });

    line.ObjectID = ObjectID;
    return line;
}
function i5(top, left, maxHeight, maxWidth, fontsize, text, ObjectID, rotationangle, Color) {
    var hAlign = "";
    hAlign = "center";
    var hStyle = "";
    var hWeight = "";
    var TOL = new fabric.Text(text, {
        left: left,
        top: top,
        fontFamily: 'Arial',
        fontStyle: hStyle,
        fontWeight: hWeight,
        fontSize: fontsize,
        angle: rotationangle,
        fill: Color,
        scaleX: dfZ1l,
        scaleY: dfZ1l,
        maxWidth: maxWidth,
        maxHeight: maxHeight,
        textAlign: hAlign,
        selectable: false

    });
    TOL.ObjectID = ObjectID;

    return TOL;

}
function i3(ia) {
    for (i = 0; i < TO.length ; i++) {
        if (TO[i].ObjectID == ia) {
            fabric.util.removeFromArray(TO, TO[i]);
            return false;
        }
    }
}
function j1(oI) {
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, ite) {
        if (ite.ObjectID == oI) {
            canvas.setActiveObject(ite);

            return false;
        }
    });
}
function j8(src) {
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'image') {
        $.each(TO, function (i, IT) {
            if (IT.ObjectID == D1AO.ObjectID) {
                IT.ContentString = src;
                D1AO.ImageClippedInfo = null;
                d5(SP);
                return;
            }
        });
    }

}
function j9(e, url1, id) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        if (D1AO.type === 'image') {
            if (e != undefined || e != null) {
                var src = "";
                var parts = "";
                if (url1 != undefined) {
                    src = url1;
                } else {

                    if ($.browser.mozilla) {
                        src = e.target.src;
                    } else {
                        src = e.srcElement.src;
                    }
                    var url = "";
                    if (src.indexOf('.svg') == -1) {
                        if (src.indexOf('_thumb.') != -1) {
                            var p = src.split('_thumb.');
                            url += p[0] + "." + p[1];
                        } else {
                            url = src;
                        }
                    } else {
                        url = src;
                    }
                    src = url;
                }
                if (src.indexOf('UserImgs') != -1) {
                    var n = src;
                    while (n.indexOf('/') != -1)
                        n = n.replace("/", "___");
                    while (n.indexOf(':') != -1)
                        n = n.replace(":", "@@");
                    while (n.indexOf('%20') != -1)
                        n = n.replace("%20", " ");
                    while (n.indexOf('./') != -1)
                        n = n.replace("./", "");
                    StartLoader("Downloading image to your design, please wait....");
                    var imgtype = 2;
                    if (isBKpnl) {
                        imgtype = 4;
                    }
                    svcCall2(n, tID, imgtype);
                } else {
                    parts = src.split("Designer/Products/");
                    var imgName = parts[parts.length - 1];
                    while (imgName.indexOf('%20') != -1)
                        imgName = imgName.replace("%20", " ");

                    var path = "./Designer/Products/" + imgName;
                    j8(path);
                }
            }
        }
    } else {
        var src = "";
        var srcElement = "";
        if (url1 != undefined) {
            src = url1;
            srcElement = "#" + id;
        } else {
            if (e != undefined || e != null) {
                if ($.browser.mozilla) {
                    src = e.target.src;
                    srcElement = e.target;
                } else {
                    src = e.srcElement.src;
                    srcElement = e.srcElement;
                }
            }
            var url = "";
            if (src.indexOf('.svg') == -1) {
                if (src.indexOf('_thumb.') != -1) {
                    var p = src.split('_thumb.');
                    url += p[0] + "." + p[1];
                } else {
                    url = src;
                }
            } else {
                url = src;
            }
            src = url;
        }
        if ($(srcElement).attr('class').indexOf("bkImg") != -1 ) {
            var id = $(srcElement).attr('id');
            k32(id, tID, src);
        } else {
            if (src.indexOf(".svg") == -1) {
                b4(src);
                d1ToCanvasCC(src, IW, IH);
            } else {
                d1SvgToCCC(src, IW, IH);
            }
        }
    }
}
function j9_21(DT) {
    StopLoader();
    k27();
    parts = DT.split("Designer/Products/");
    //$("#ImgCarouselDiv").tabs("option", "active", 1); open template  images section
    var imgName = parts[parts.length - 1];
    while (imgName.indexOf('%20') != -1)
        imgName = imgName.replace("%20", " ");

    var path = "./Designer/Products/" + imgName;
    j8(path);
}
function k0() {
    $("#sliderFrame").html('<p class="sliderframeMsg">Click on image below to see higher resolution preview.</p><div id="slider">  </div> <div id="thumbs"></div> <div style="clear:both;height:0;"></div>');
    if (IsCalledFrom == 1 || IsCalledFrom == 2) {
        $(".sliderframeMsg").css("display", "none");
    }
    if (IsBC) {
        $('#PreviewerContainer').css("width", "800px");
        $('#Previewer').css("width", "776px");
        $('#sliderFrame').css("width", "740px");
        $('#slider').css("width", "542px");
        $('#previewProofing').css("width", "760px");
        $('#PreviewerContainer').css("height", "562px");
        $('#PreviewerContainer').css("left", (($(window).width() - $('#PreviewerContainer').width()) / 2) + "px");
        $('#PreviewerContainer').css("top", (($(window).height() - $('#PreviewerContainer').height()) / 2) + "px");
        $('.sliderLine').css("width", "744px");
        $('#Previewer').css("height", ((500 - 46)) + "px");
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            $('#sliderFrame').css("height", $('#Previewer').height() - 50 - 40 + "px");
            $('#slider').css("height", $('#Previewer').height() - 50 - 40 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 50 - 40 + "px");
        } else {
            $('#sliderFrame').css("height", $('#Previewer').height() - 33 + "px");
            $('#slider').css("height", $('#Previewer').height() - 33 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 33 + "px");
        }
        $('.divTxtProofing').css("width", "624px");
        $('.btnBlueProofing').css("width", "108px");
        $('.previewerTitle').css("padding-left", "7px");
        $('.previewerTitle').css("padding-top", "7px");
        $('.previewerTitle').css("padding-bottom", "7px");
    } else {
        if ($(window).width() > 1200 && (IsCalledFrom == 1 || IsCalledFrom == 3)) {
            $('#PreviewerContainer').css("width", "1200px");
            $('#Previewer').css("width", "1176px");
            $('#sliderFrame').css("width", "1140px");
            $('#slider').css("width", "942px");
            $('.sliderLine').css("width", "1144px");
            $('#previewProofing').css("width", "1160px");
            $('.divTxtProofing').css("margin-left", "208px");
        }
        $('#PreviewerContainer').css("left", (($(window).width() - $('#PreviewerContainer').width()) / 2) + "px");
        $('#PreviewerContainer').css("height", (($(window).height() - 28)) + "px");
        $('#Previewer').css("height", (($(window).height() - 131)) + "px");
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            $('#sliderFrame').css("height", $('#Previewer').height() - 50 - 40 + "px");
            $('#slider').css("height", $('#Previewer').height() - 50 - 40 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 50 - 40 + "px");
        } else {
            $('#sliderFrame').css("height", $('#Previewer').height() - 33 + "px");
            $('#slider').css("height", $('#Previewer').height() - 33 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 33 + "px");
        }
    }
    $.each(TP, function (i, IT) {
        $("#slider").append('<img src="designer/products/' + tID + '/p' + IT.PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '"  alt="' + IT.PageName + '" />');
        $("#thumbs").append(' <div id="thumbPage' + IT.ProductPageID + '" class="thumb"><div class="frame"><img src="designer/products/' + tID + '/p' + IT.PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '" class="thumbNailFrame" /></div><div class="thumb-content"><p>' + IT.PageName + '</p></div><div style="clear:both;"></div></div>');

    });
    $.each(TP, function (i, IT) {
        $("#slider").append('<img class="overlayLayer' + IT.ProductPageID + '" style="visibility:hidden;" src="designer/products/' + tID + '/p' + IT.PageNo + 'overlay.png?r=' + fabric.util.getRandomInt(1, 100) + '"  alt="' + IT.PageName + '" />');
        $("#thumbs").append(' <div id="overlayLayer' + IT.ProductPageID + '" style="visibility:hidden;" class="thumb"><div class="frame"><img src="designer/products/' + tID + '/p' + IT.PageNo + 'overlay.png?r=' + fabric.util.getRandomInt(1, 100) + '" class="thumbNailFrame" /></div><div class="thumb-content"><p>' + IT.PageName + ' - Overlay Layer</p></div><div style="clear:both;"></div></div>');
    });
    if (IsCalledFrom == 1 || IsCalledFrom == 2) {
        $('#previewProofing').css("display", "none");
    }
    if (IsCalledFrom == 2) {
        $("#slider").css("visibility", "hidden");
        $(".PreviewerDownloadPDF").removeClass("PreviewerDownloadPDF").addClass("PreviewerDownloadPDFCorp");

        $(".PreviewerDownloadPDFCorp").css("top", "200px");
        $(".PreviewerDownloadPDFCorp").text("Click here to download high resolution PDF file.");
        $(".PreviewerDownloadPDFCorp").css("right", $("#PreviewerContainer").width() / 2 - 319 + "px");
    }
    if (IsCalledFrom == 3 || IsCalledFrom == 4) {
        $("#slider").css("cursor", "pointer");
        $("#slider").click(function () {
            var s = $('#slider').css('background-image');
            if (s != undefined) {
                var p = s.split("/");
                var i = p[p.length - 1];
                var im = i.split("?");
                var img = new Image();
                StartLoader("Loading content please wait..");
                img.onload = function () {
                    StopLoader();
                    var src = "Previewer.aspx?tId=" + tID + "&pID=" + im[0];
                    $("#LargePreviewerIframe").attr("src", src);
                    var width = this.width + 30;
                    var height = this.height + 50;
                    $(".LargePreviewerIframe").css("width", width - 30);
                    $(".LargePreviewerIframe").css("height", height - 40);
                    if (this.width > $(window).width()) {
                        width = $(window).width() - 50;
                    }
                    if (this.height > $(window).height()) {
                        height = $(window).height() - 80;
                        $(".LargePreviewerIframe").css("height", height - 40);
                        $(".LargePreviewerIframe").css("width", width - 10);
                    }
                    $(".LargePreviewer").dialog("option", "height", height);
                    $(".LargePreviewer").dialog("option", "width", width);

                    $("#DivShadow").css("z-Index", "100002");
                    $("#DivShadow").css("display", "block");

                    $("#LargePreviewer").dialog("open");
                }
                img.src = "designer/products/" + tID + "/" + im[0];
            }
        });
    }
}
function k4() {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();

    if (D1AG) {
    } else if (D1AO && D1AO.IsPositionLocked == false) {
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        var w;
        var h;
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        if (D1AO.type === 'text' || D1AO.type === 'i-text') {
            w = Math.round(D1AO.maxWidth);
            h = Math.round(D1AO.maxHeight);
            $("#inputObjectWidthTxt").val(w);
            $("#inputObjectHeightTxt").val(h);
            $("#inputPositionXTxt").val(l);
            $("#inputPositionYTxt").val(t);
        } else {
            // animatedcollapse.show('divPositioningPanel');
            w = Math.round(D1AO.getWidth());
            h = Math.round(D1AO.getHeight());
            o = D1AO.getOpacity() * 100;
            $("#inputObjectWidth").val(w);
            $("#inputObjectHeight").val(h);
            $("#inputObjectAlpha").val(o);
            $(".transparencySlider").slider("option", "value", o);

        }
        $("#inputPositionXTxt").spinner("option", "disabled", false);
        $("#inputPositionYTxt").spinner("option", "disabled", false);
        $("#inputObjectWidthTxt").spinner("option", "disabled", false);
        $("#inputObjectHeightTxt").spinner("option", "disabled", false);
        $("#inputPositionX").spinner("option", "disabled", false);
        $("#inputPositionY").spinner("option", "disabled", false);
        $("#inputObjectWidth").spinner("option", "disabled", false);
        $("#inputObjectHeight").spinner("option", "disabled", false);
    }

}
function k5() {
    if (!$.isNumeric($("#inputPositionX").val())) {
        $("#inputPositionX").val(0);
    }
    if (!$.isNumeric($("#inputPositionY").val())) {
        $("#inputPositionY").val(0);
    }
    if (!$.isNumeric($("#inputPositionXTxt").val())) {
        $("#inputPositionXTxt").val(0);
    }
    if (!$.isNumeric($("#inputPositionYTxt").val())) {
        $("#inputPositionYTxt").val(0);
    }
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var l = D1AO.left - D1AO.getWidth() / 2;
    var t = D1AO.top - D1AO.getHeight() / 2;
    l = Math.round(l);
    t = Math.round(t);
    var dL = $("#inputPositionX").val() - l;
    var dT = $("#inputPositionY").val() - t;
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        dL = $("#inputPositionXTxt").val() - l;
        dT = $("#inputPositionYTxt").val() - t;
    }
    D1AO.left += dL;
    D1AO.top += dT;
    // c2(D1AO);
    canvas.renderAll(); D1AO.setCoords();
}
function k6() {
    if (!$.isNumeric($("#inputObjectHeightTxt").val())) {
        $("#inputObjectHeightTxt").val(5);
    }
    if (!$.isNumeric($("#inputObjectHeight").val())) {
        $("#inputObjectHeight").val(5);
    }
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var oldH1 = D1AO.getHeight();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {
        var h = $("#inputObjectHeightTxt").val();
        var oldH = D1AO.getHeight();
        D1AO.maxHeight = h;
        var newScaleY = D1AO.maxHeight / D1AO.height;
        var height = newScaleY * D1AO.height;
        D1AO.set('height', height);
        D1AO.set('maxHeight', height);
        dif = D1AO.getHeight() - oldH;
        dif = dif / 2
        D1AO.top = D1AO.top + dif;

    } else {
        var h = $("#inputObjectHeight").val();
        var oldH = D1AO.getHeight();
        D1AO.maxHeight = h;
        D1AO.scaleY = D1AO.maxHeight / D1AO.height;
        var dif = D1AO.getHeight() - oldH;
        dif = dif / 2
        D1AO.top = D1AO.top + dif;
    }
    //  c2(D1AO);
    canvas.renderAll(); D1AO.setCoords();
    k4();
}
function k7() {
    if (!$.isNumeric($("#inputObjectWidthTxt").val())) {
        $("#inputObjectWidthTxt").val(5);
    }
    if (!$.isNumeric($("#inputObjectWidth").val())) {
        $("#inputObjectWidth").val(5);
    }
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var oldW1 = D1AO.getWidth();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {
        var w = $("#inputObjectWidthTxt").val();
        var oldW = D1AO.getWidth();
        D1AO.maxWidth = w;
        var scaleX = D1AO.maxWidth / D1AO.width;
        var width = D1AO.width * scaleX;
        D1AO.set('width', width);
        D1AO.set('maxWidth', width);
        var dif = D1AO.getWidth() - oldW;
        dif = dif / 2
        D1AO.left = D1AO.left + dif;
    } else {
        var w = $("#inputObjectWidth").val();
        var oldW = D1AO.getWidth();
        D1AO.maxWidth = w;
        D1AO.scaleX = D1AO.maxWidth / D1AO.width;
        var dif = D1AO.getWidth() - oldW;
        dif = dif / 2
        D1AO.left = D1AO.left + dif;

    }
    //  c2(D1AO);
    canvas.renderAll(); D1AO.setCoords();
    k4();
}
function k7_trans() {
    if (!$.isNumeric($("#inputObjectAlpha").val())) {
        $("#inputObjectAlpha").val(100);
    }
    if ($("#inputObjectAlpha").val() < 0) {
        $("#inputObjectAlpha").val(0);
    }
    if ($("#inputObjectAlpha").val() > 100) {
        $("#inputObjectAlpha").val(100);
    }
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var oldW1 = D1AO.getWidth();
    if (D1AO.type !== 'text' && D1AO.type !== 'i-text') {
        var o = $("#inputObjectAlpha").val();
        o = o / 100;
        D1AO.setOpacity(o);
    }
    //c2(D1AO);
    canvas.renderAll(); D1AO.setCoords();
    k4();
}
function k7_trans_retail(val) {

    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    if (D1AO.type !== 'text' && D1AO.type !== 'i-text') {
        var o = val;
        o = o / 100;
        D1AO.setOpacity(o);
        o = D1AO.getOpacity() * 100;
        $(".transparencySlider").slider("option", "value", o);

    }
    //  c2(D1AO);
    canvas.renderAll();

}
function k7_Case_Force(val) {
    var selectedObject = canvas.getActiveObject();
    if (selectedObject) {
        if (val == '1') {
            selectedObject.textCase = 0;
        } else if (val == '34') {
            selectedObject.textCase = 2;
        } else if (val == '67') {
            selectedObject.textCase = 1;
        } else {
            selectedObject.textCase = 3;
        }
        
       
        //  pcL22_Sub(selectedObject);
        changeCase();
        canvas.renderAll();
    }
    
    //  c2(D1AO);
   // canvas.renderAll();

}
function changeCase() {
    var selectedObject = canvas.getActiveObject();
    var text = selectedObject.text;
    if (selectedObject.textCase == 1) {
       text = text.toLowerCase();
    } else if (selectedObject.textCase == 2) {
        text = text.toUpperCase();
    } else if (selectedObject.textCase == 3) {

        text = text.toLowerCase();
        var sntncForSentncCase = text.split(".");
        var TextTemp = '';
        for (var sen = 0; sen < sntncForSentncCase.length; sen++) {
            if (sntncForSentncCase.length == 1) {
                TextTemp = TextTemp + sntncForSentncCase[sen].substr(0, 1).toUpperCase() + sntncForSentncCase[sen].substr(1);
            } else {
                sntncForSentncCase[sen] = sntncForSentncCase[sen].trim();
                TextTemp = TextTemp + sntncForSentncCase[sen].substr(0, 1).toUpperCase() + sntncForSentncCase[sen].substr(1) + '. ';
            }

        }

        text = TextTemp;
    }
    selectedObject.text=text;
}
function k7_rotate_retail(val) {
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    D1AO.setAngle(val);
    $(".rotateSlider").slider("option", "value", val);
    $(".rotateSliderTxt").slider("option", "value", val);
    canvas.renderAll();

}
function k8() {
    if ($("#inputcharSpacing").val() < -7) {
        $("#inputcharSpacing").val(-7);
    } else if ($("#inputcharSpacing").val() > 10) {
        $("#inputcharSpacing").val(10);
    }
    var Cs = k14($("#inputcharSpacing").val());
    var DIAO = canvas.getActiveObject();
    if (DIAO) {
        DIAO.charSpacing = Cs;
        $.each(TO, function (i, IT) {
            if (IT.ObjectID == DIAO.ObjectID) {
                IT.CharSpacing = DIAO.charSpacing;
            }
        });
        canvas.renderAll();
    }
}
function k9() {
    if ($('#slider') != undefined) {
        var s = $('#slider').css('background-image');
        if (s != undefined) {
            var p = s.split("?");
            if (s.indexOf("asset") == -1) {
                var temp = p[0].split("http://");
                var t2 = temp[1].split(".png");
                var i = 'http://' + t2[0] + '.pdf'; //+= '?r=' + ra ;
                if (IsCalledFrom == 2) {
                    $(".PreviewerDownloadPDFCorp").attr("href", i);
                } else {
                    $(".PreviewerDownloadPDF").attr("href", i);
                }
            }
        }
    }
}
function k12(fz) {
    var pt = k14(fz);
    var D1AO = canvas.getActiveObject();
    if (parseFloat(pt)) {
        if (pt <= 400) {
            var fontSize = parseFloat(pt);
            fontSize = fontSize.toFixed(2);
            fontSize = parseFloat(fontSize);

            if (D1AO) {
                if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
                    setActiveStyle("font-Size", fontSize)
                    canvas.renderAll();
                }
            }
        } else {
            alert("Please enter valid font size!");
            $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
        }
    } else {
        alert("Please enter valid font size!");
        $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
    }
}

function k13(e) {
    if (parseFloat(e)) {
        var ez = parseFloat(e);
        ez = ez.toFixed(2);
        ez = ez / 96 * 72;
        ez = ez.toFixed(2);
        return ez;
    }
    return e;

}
function k14(e) {
    if (parseFloat(e)) {
        var ez = parseFloat(e);
        ez = ez.toFixed(2);
        ez = ez * 96 / 72;
        ez = ez.toFixed(2);
        return ez;
    }
    return e;
}
function k15() {
    if ($("#txtLineHeight").val() < -1.50) {
        $("#txtLineHeight").val(1);
    } else if ($("#txtLineHeight").val() > 2.0) {
        $("#txtLineHeight").val(2.0);
    }
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        D1AO.lineHeight = $("#txtLineHeight").val();
        //   $("#txtAreaUpdateTxt").css("line-height", $("#txtLineHeight").val());
    }
    // c2(D1AO);
    canvas.renderAll();

}
function k16(TempImgType, ImC, Caller) {
    var loaderType = 1;
    var isBackground = false;
    var oldHtml = "";
    var strName = "";
    var jsonPath = "";
    var ImIsEditable = true;
    var searchTerm = "___notFound";
    if (IsCalledFrom == 1) {
        if (CustomerID == undefined) {
            CustomerID = -999;
        }
    } else if (IsCalledFrom == 2) {
    } else if (IsCalledFrom == 3) {
        if (TempImgType == 6 || TempImgType == 7 || TempImgType == 13 || TempImgType == 14 || TempImgType == 18 || TempImgType == 19 || TempImgType == 20) {
            jsonPath += V2Url;
        }
    } else if (IsCalledFrom == 4) {
        // change terrritory
    }
    if (ContactID == undefined) {
        ContactID = 0;
    }
    if (TempImgType == 1) {
        strName = "divTempImgContainer";
        //ImIsEditable = false;
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
    } if (TempImgType == 12) {
        strName = "divTempBkImgContainer";
        //ImIsEditable = false;
        //if ($('#inputSearchTBkg').val() != "") {
        //    searchTerm = $('#inputSearchTBkg').val();
        //}
        isBackground = true;
    } else if (TempImgType == 2) {
        strName = "divGlobImgContainer";
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
    }
    else if (TempImgType == 3) {
        strName = "divGlobBkImgContainer";
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
        if ($('#inputSearchTBkg').val() != "") {
            searchTerm = $('#inputSearchTBkg').val();
        }
        isBackground = true;
    }
    else if (TempImgType == 4) {
        strName = "divPersImgContainer";
        if ($('#inputSearchPImg').val() != "") {
            searchTerm = $('#inputSearchPImg').val();
        } loaderType = 3;
    } else if (TempImgType == 5) {
        strName = "divPersBkImgContainer";
        if ($('#inputSearchTBkg').val() != "") {
            searchTerm = $('#inputSearchTBkg').val();
        } loaderType = 2;
        isBackground = true;
    } else if (TempImgType == 6) {
        strName = "divGlobImgContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    } else if (TempImgType == 7) {
        strName = "divGlobBkImgContainer";
        //if ($('#inputSearchTBkg').val() != "") {
        //    searchTerm = $('#inputSearchTBkg').val();
        //}
        loaderType = 2;
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
        isBackground = true;
    } else if (TempImgType == 8) {
        strName = "divPersImgContainer";
        //if ($('#inputSearchPImg').val() != "") {
        //    searchTerm = $('#inputSearchPImg').val();
        //}
        loaderType = 3;
    }
    else if (TempImgType == 9) {
        strName = "divPersBkImgContainer";
        //if ($('#inputSearchTBkg').val() != "") {
        //    searchTerm = $('#inputSearchTBkg').val();
        //}
        loaderType = 2;
        isBackground = true;
    } else if (TempImgType == 10) {
        strName = "divPersImgContainer";
        if ($('#inputSearchPImg').val() != "") {
            searchTerm = $('#inputSearchPImg').val();
        } loaderType = 3;
    } else if (TempImgType == 11) {
        strName = "divPersBkImgContainer";
        if ($('#inputSearchTBkg').val() != "") {
            searchTerm = $('#inputSearchTBkg').val();
        }
        isBackground = true;
    }
    else if (TempImgType == 13) {
        strName = "divShapesContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 1) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 14) {
        strName = "divLogosContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 1) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 15) {
        strName = "divPLogosContainer";
        //if ($('#inputSearchPImg').val() != "") {
        //    searchTerm = $('#inputSearchPImg').val();
        //}
        loaderType = 3;
        if (IsCalledFrom == 3) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 16) {
        strName = "divShapesContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 1 || IsCalledFrom == 2) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 17) {
        strName = "divLogosContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 1 || IsCalledFrom == 2) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 18) {
        strName = "divIllustrationContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    }
    else if (TempImgType == 19) {
        strName = "divFramesContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    }
    else if (TempImgType == 20) {
        strName = "divBannersContainer";
        if ($('#inputSearchTImg').val() != "") {
            searchTerm = $('#inputSearchTImg').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    }
    jsonPath += "Services/imageSvcDam/" + IsCalledFrom + "," + TempImgType + "," + tID + "," + CustomerID + "," + ContactID + "," + Territory + "," + ImC + "," + searchTerm
    oldHtml = $("." + strName).html() + "";
    $.getJSON(jsonPath,
            function (DT) {
                // alert(DT);
                if (Caller != "Loader") {
                   stopInlineLoader();
                }
                if (DT.objsBackground == "") {
                    if (oldHtml.indexOf("allImgsLoadedMessage") == -1) {
                        $("." + strName).append("<p class='allImgsLoadedMessage'>No more images matches your search criteria. </p>");
                        $(".btn" + strName).css("display", "none");
                    } else {
                        if (TempImgType == 1) {
                            TeImC -= 1;
                            TeImCEx = false;
                        } else if (TempImgType == 2) {
                            GlImC -= 1;
                            GlImEx = false;
                        } else if (TempImgType == 3) {
                            GlImExBk = false;
                        } else if (TempImgType == 4) {
                            UsImC -= 1;
                            UsImEx = false;
                        } else if (TempImgType == 5) {
                            UsImCBkEx = false;
                        } else if (TempImgType == 6) {
                            GlImC -= 1;
                            GlImEx = false;
                        } else if (TempImgType == 7) {
                            GlImExBk = false;
                        } else if (TempImgType == 8) {
                            UsImC -= 1; UsImEx = false;
                        } else if (TempImgType == 9) {
                            UsImCBkEx = false;
                        } else if (TempImgType == 10) {
                            GlImEx = false;
                        }
                        else if (TempImgType == 11) {
                            GlImExBk = false;
                        }
                        else if (TempImgType == 12) {
                            TeImExBk = false;
                        }
                        else if (TempImgType == 13) {
                            GlShpCn -= 1;
                            GlShpCnEx = false;
                        }
                        else if (TempImgType == 14) {
                            GlLogCn -= 1;
                            GlLogCnEx = false;
                        }
                        else if (TempImgType == 15) {
                            GlLogCnP -= 1;
                            GlLogCnExP = false;
                        }
                        else if (TempImgType == 16) {
                            GlShpCn -= 1;
                            GlShpCnEx = false;
                        }
                        else if (TempImgType == 17) {
                            GlLogCn -= 1;
                            GlLogCnEx = false;
                        }
                        else if (TempImgType == 18) {
                            GlIlsC -= 1;
                            GlIllsEx = false;
                        }
                        else if (TempImgType == 19) {
                            GlframC -= 1;
                            GlFramesEx = false;
                        }
                        else if (TempImgType == 20) {
                            GlBanC -= 1;
                            GlBannerEx = false;
                        }
                        $("." + strName + " .allImgsLoadedMessage").remove();
                        $("." + strName).append("<p class='allImgsLoadedMessage'>No more images matches your search criteria. </p>");
                      //  $(".btn" + strName).css("display", "none"); if button added for load more images

                    }
                }
                else {
                    // $(".imCount" + strName).html(DT.ImageCount + " Images found.");
                    //    $(".imCount" + strName).html("Drag an image to canvas.");
                    
                    $.each(DT.objsBackground, function (j, IT) {
                        LiImgs.push(IT); 
                        var url = "./" + IT.BackgroundImageRelativePath;
                        if (IsCalledFrom == 3) {
                            if (TempImgType == 6 || TempImgType == 7 || TempImgType == 13 || TempImgType == 14 || TempImgType == 18 || TempImgType == 19 || TempImgType == 20) {
                                url = "http://designerv2.myprintcloud.com/" + IT.BackgroundImageRelativePath;
                            }
                        }
                        var title = IT.ID;
                        var index = tID;
                        var draggable = 'draggable2';
                        var bkContainer = '';
                        if (isBackground) {
                            draggable = "draggable2 bkImg";
                            bkContainer = '<span class="price free btnImgSetBk" onclick=k32(' + title + "," + index + ',"' + url + '")>Set as Background</span> ';
                            loaderType = 2;
                        }
                        var urlThumbnail = "";
                        if (url.indexOf('.svg') == -1) {
                            var p = url.split('.');
                            for (var z = 0; z <= p.length - 2; z++) {
                                if (p[z] != "") {
                                    if (z == 0 && IsCalledFrom == 3) {
                                        urlThumbnail += p[z];
                                    } else {
                                        urlThumbnail += "." + p[z];
                                    }
                                }
                            }
                            urlThumbnail += "_thumb." + p[p.length - 1];
                        } else {
                            urlThumbnail = url;
                        }
                        if (ImIsEditable) {
                            
                            var ahtml = '<li class="DivCarouselImgContainerStyle2"><a href="#">' + '<img  src="' + urlThumbnail +
                              '" class="svg imgCarouselDiv ' + draggable + '" style="z-index:1000;" id = "' + title + '" alt="' + url + '">'// + '<span class="info btnRemoveImg"><span class=" moreInfo ">✖</span></span>'
                              + bkContainer + '<span class="info">' + '<span class="moreInfo" title="Show more info" onclick=k26(' + title + "," + index + "," + loaderType + ')>i</span>' +
		                       '</span></a></li>';
                            $("." + strName).append(ahtml);
                        } else {
                            var ahtml = '<li class="DivCarouselImgContainerStyle2"><a href="#">' + '<img  src="' + urlThumbnail +
                              '" class="svg imgCarouselDiv ' + draggable + '" style="z-index:1000;" id = "' + title + '" alt="' + url + '">' + bkContainer +'</a></li>';

                            $("." + strName).append(ahtml);

                        }
                        $("#" + title).click(function (event) {
                            j9(event, url, title);
                        });
                    });
                    var he21 = $("." + strName + " li").length;
                    he21 = (he21 / 4) * ($("." + strName + " li").height() + 2);
                    if (isBackground)
                        he21 +=10;
                    $("." + strName).css("height", he21 + "px");
                    var clss = $(".searchLoaderHolder").parent().attr("class");
                    if (clss.indexOf("templateImagesContainer") != -1 || clss.indexOf("tempBackgroundImages") != -1 || clss.indexOf("freeImgsContainer") != -1 || clss.indexOf("freeBkImgsContainer") != -1 || clss.indexOf("shapesContainer") != -1 || clss.indexOf("logosContainer") != -1 || clss.indexOf("yourLogosContainer") != -1 || clss.indexOf("illustrationsContainer") != -1 || clss.indexOf("framesContainer") != -1 || clss.indexOf("bannersContainer") != -1 || clss.indexOf("myBkImgsContainer") != -1 || clss.indexOf("yourImagesContainer") != -1) {
                        $(".searchLoaderHolder").parent().css("height", (he21 + $(".searchLoaderHolder").height()) + "px");
                    }
                    $(".imgOrignalCrop").draggable({});
                    $(".draggable2").draggable({
                        snap: '#dropzone',
                        snapMode: 'inner',
                        revert: 'invalid',
                        helper: 'clone',
                        appendTo: "body",
                        cursor: 'move',
                        helper: function () {
                            var helper = $(this).clone(); // Untested - I create my helper using other means...
                            // jquery.ui.sortable will override width of class unless we set the style explicitly.
                            helper.css({ 'width': 'auto', 'height': '98px' });
                            return helper;
                        }

                    });
                    jQuery('.DivCarouselImgContainerStyle2').hover(function () {
                        jQuery(this).find('.btnImgSetBk').fadeIn(50);
                        jQuery(this).find('.DelImgAnchor').fadeIn(50);
                        jQuery(this).find('.EditImgBtn').fadeIn(50);
                    }, function () {
                        jQuery(this).find('.btnImgSetBk').fadeOut(50);
                        jQuery(this).find('.DelImgAnchor').fadeOut(50);
                        jQuery(this).find('.EditImgBtn').fadeOut(50);
                    });
                    if (isImgUpl) {
                        $('.DamImgContainer  div').each(function (i) {
                            if (i == 0) {
                                var id = $(this).find('img').attr("id");
                                $("#" + id).load(function () {
                                    Arc(720, id);
                                });
                            }
                        });
                        isImgUpl = false;
                    }
                }
            });
}

function k17() {
    startInlineLoader(21);
    TeImC += 1;
    k16(1, TeImC, "fun");
}
function k17Bk() {
    startInlineLoader(22);
    TeImCBk += 1;
    k16(12, TeImCBk, "fun");
}
function k19() {
    //StartLoader();
    TeImC = 1;
    $(".divTempImgContainer").html("");
    $(".btndivTempImgContainer").css("display", "block");
    k16(1, TeImC, "fun");
}
function k19Bk() {
    //StartLoader();
    TeImCBk = 1;
    $(".divTempBkImgContainer").html("");
    $(".btndivTempBkImgContainer").css("display", "block");
    k16(12, TeImCBk, "fun");
}
function k21() {
    startInlineLoader(23);
    GlImC += 1;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(2, GlImC, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(6, GlImC, "fun");
    }
}
function k21Bk() {
    startInlineLoader(24);
    GlImCBk += 1;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(3, GlImCBk, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(7, GlImCBk, "fun");
    }
}
function k21Sh() {
    startInlineLoader(25);
    GlShpCn += 1;
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
      //  k16(13, GlShpCn, "fun");
    } else {
      //  k16(16, GlShpCn, "fun");
    }
}
function k21Log() {
    startInlineLoader(26);
    GlLogCn += 1;
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
       // k16(14, GlLogCn, "fun");
    } else {
      //  k16(17, GlLogCn, "fun");
    }
}
function k21PLog() {
    startInlineLoader(27);
    GlLogCnP += 1;
    k16(15, GlLogCnP, "fun");
}
function k22() {
   // StartLoader();
    GlImC = 1;
    $(".divGlobImgContainer").html("");
    $(".btndivGlobImgContainer").css("display", "block");
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(2, GlImC, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(6, GlImC, "fun");
    }
}
function k22Log() {
    //StartLoader();
    GlLogCn = 1;
    $(".divLogosContainer").html("");
    $(".btndivLogosContainer").css("display", "block");
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
    //    k16(14, GlLogCn, "fun");
    } else {
      //  k16(17, GlLogCn, "fun");
    }//
}
function k22Sh() {
    //StartLoader();
    GlShpCn = 1;
    $(".divShapesContainer").html("");
    $(".btndivShapesContainer").css("display", "block");
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
      //  k16(13, GlShpCn, "fun");
    } else {
      //  k16(16, GlShpCn, "fun");
    }

}
function k22LogP() {
   // StartLoader();
    GlLogCnP = 1;
    $(".divPLogosContainer").html("");
    $(".btndivPLogosContainer").css("display", "block");
    k16(15, GlLogCnP, "fun");

}
function k24ilus() {
    startInlineLoader(28);
    GlIlsC += 1;
    k16(18, GlIlsC, "fun");
}
function k22Bk() {
   // StartLoader();
    GlImCBk = 1;
    $(".divGlobBkImgContainer").html("");
    $(".btndivGlobBkImgContainer").css("display", "block");
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(3, GlImCBk, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(7, GlImCBk, "fun");
    }
}
function k24frames() {
    startInlineLoader(29);
    GlframC += 1;
    k16(19, GlframC, "fun");
}
function k24banners() {
    startInlineLoader(30);
    GlBanC += 1;
    k16(20, GlBanC, "fun");
}

function k24Bk() {
    startInlineLoader(31);
    UsImCBk += 1;
    if (IsCalledFrom == 4) {
        k16(5, UsImCBk, "fun");
    }
    if (IsCalledFrom == 3) {
        k16(9, UsImCBk, "fun");
    }
    if (IsCalledFrom == 1) {
        k16(11, UsImCBk, "fun");
    }
}
function k24() {
    startInlineLoader(32);
    UsImC += 1;
    if (IsCalledFrom == 4) {
        k16(4, UsImC, "fun");
    }
    if (IsCalledFrom == 3) {
        k16(8, UsImC, "fun");
    }
    if (IsCalledFrom == 1) {
        k16(10, UsImC, "fun");
    }
}
function k25() {
   // StartLoader();
    UsImC = 1;
    $(".divPersImgContainer").html("");
    $(".btndivPersImgContainer").css("display", "block");
    if (IsCalledFrom == 4) {
        k16(4, UsImC, "fun");
    }
    if (IsCalledFrom == 3) {
        k16(8, UsImC, "fun");
    }
    if (IsCalledFrom == 1) {
        k16(10, UsImC, "fun");
    }
}
function k25Bk() {
   // StartLoader();
    UsImCBk = 1;
    $(".divPersBkImgContainer").html("");
    $(".divPersBkImgContainer").css("display", "block");
    if (IsCalledFrom == 4) {
        k16(5, UsImCBk, "fun");
    }
    if (IsCalledFrom == 3) {
        k16(9, UsImC, "fun");
    }
    if (IsCalledFrom == 1) {
        k16(11, UsImC, "fun");
    }
}
function k25Ills() {
   // StartLoader();
    GlIllsEx = 1;
    $(".divIllustrationContainer").html("");
    $(".btndivIllustrationContainer").css("display", "block");
    k16(18, GlIlsC, "fun");
}
function k25Frames() {
   // StartLoader();
    GlframC = 1;
    $(".divFramesContainer").html("");
    $(".btndivFramesContainer").css("display", "block");
    k16(19, GlframC, "fun");
}
function k25Banners() {
 //   StartLoader();
    GlBanC = 1;
    $(".divBannersContainer").html("");
    $(".btndivBannersContainer").css("display", "block");
    k16(20, GlBanC, "fun");
}
function k26(id, n,m) {
    //StartLoader("Loading image please wait..");
    imgSelected = id;
    imgLoaderSection = m;
    var imToLoad = parseInt(id);
    var tp = $("#selectedTab").css("top");
    $("#objectPanel").removeClass("stage1").removeClass("stage2").removeClass("stage3").removeClass("stage4").removeClass("stage5").removeClass("stage6").removeClass("stage8").removeClass("stage7").addClass("stage7");
   
 //   $(".stage7 #selectedTab").css("top", tp);
    $(".ImageContainer").css("display", "none");
    $("#progressbar").css("display", "none");
    svcCall3(imToLoad);
}
function k26_Dt(DT) {
    // StopLoader();
    $(".divImageTypes").css("display", "none");
    $("#InputImgTitle").val(DT.ImageTitle);
    $("#InputImgDescription").val(DT.ImageDescription);
    $("#InputImgKeywords").val(DT.ImageKeywords);
    $("#ImgDAMDetail").attr("src", "./" + DT.BackgroundImageRelativePath);
    // image set type 12 = global logos
    // image set type 13 = global shapes/icons
    $("#radioImagePicture").prop('checked', true);
    if (DT.ImageType == 14) {
        $("#radioImageLogo").prop('checked', true); $(".divImageTypes").css("display", "block");
    } else if (DT.ImageType == 15) {
        $("#radioImageLogo").prop('checked', true); $(".divImageTypes").css("display", "block");
    } else if (DT.ImageType == 13) {
        $("#radioImageShape").prop('checked', true);
    } else if (DT.ImageType == 17) {
        $("#radioImageLogo").prop('checked', true); $(".divImageTypes").css("display", "block");
    } else if (DT.ImageType == 16) {
        $("#radioImageShape").prop('checked', true);
    } else if (DT.ImageType == 18) {
        $("#radioBtnIllustration").prop('checked', true);
    } else if (DT.ImageType == 19) {
        $("#radioBtnFrames").prop('checked', true);
    } else if (DT.ImageType == 20) {
        $("#radioBtnBanners").prop('checked', true);
    } else if (DT.ImageType == 1) {
        $(".divImageTypes").css("display", "block");
    } else {
        $("#radioImagePicture").prop('checked', true);
        // $(".divImageTypes").css("display", "none");
    }
    $(".ImageContainer").css("display", "block");
}
function k27() {
    k25();
    k22();
    k19();
    k25Bk();
    k22Bk();
    k19Bk();
  //  k22Log();
  //  k22Sh();
    if (IsCalledFrom == 3) {
        k22LogP();
    }
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
      //  k25Ills();
      //  k25Frames();
      //  k25Banners();
    }
}
function k31(cCanvas, IO) {
    TIC += 1;
    if (IO.MaxWidth == 0) {
        IO.MaxWidth = 50;
    }
    if (IO.MaxHeight == 0) {
        IO.MaxHeight = 50;
    }
    fabric.Image.fromURL(IO.ContentString, function (IOL) {
        IOL.set({
            left: (IO.PositionX + IO.MaxWidth / 2)* dfZ1l,
            top: (IO.PositionY + IO.MaxHeight / 2)* dfZ1l,
            angle: IO.RotationAngle
        });
        IOL.ImageClippedInfo = IO.ClippedInfo;
        IOL.maxWidth = IO.MaxWidth;
        IOL.maxHeight = IO.MaxHeight;
        IOL.ObjectID = IO.ObjectID;
        IOL.scaleX = (IOL.maxWidth / IOL.width)* dfZ1l;
        IOL.scaleY = (IOL.maxHeight / IOL.height)* dfZ1l;
        IOL.setAngle(IO.RotationAngle);
        IOL.setOpacity(IO.Opacity);
        if (IsCalledFrom == 1 || IsCalledFrom == 2) {
            IOL.lockMovementX = false;
            IOL.lockMovementY = false;
            IOL.lockScalingX = false;
            IOL.lockScalingY = false;
            IOL.lockRotation = false;
            IOL.IsPositionLocked = false;
            IOL.IsHidden = false;
            IOL.IsEditable = false;
            IOL.IsTextEditable = true;
        } else {
            IOL.lockMovementX = true;
            IOL.lockMovementY = true;
            IOL.lockScalingX = true;
            IOL.lockScalingY = true;
            IOL.lockRotation = true;
            IOL.IsPositionLocked = true;
            IOL.IsHidden = true;
            IOL.IsEditable = true;
            IOL.IsTextEditable = false;
            IOL.selectable = true;
        }

        IOL.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });
        if (IO.IsQuickText == true) {
            IOL.IsQuickText = true;
        }
        cCanvas.insertAt(IOL, IO.DisplayOrderPdf);
        TotalImgLoaded += 1;
        d2();
    });
}
function k32(imID, Tid, eleID) {
    var url = "";
    if (eleID.indexOf('.svg') == -1) {
        if (eleID.indexOf('_thumb.') != -1) {
            var p = eleID.split('_thumb.');
            url += p[0] + "." + p[1];
        } else {
            url = eleID;
        }
    } else {
        url = eleID;
    }
    eleID = url;
    var n = url;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");
    
    if (eleID.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        }
        StartLoader("Downloading image to your design, please wait....");
        svcCall4(n, tID, imgtype);
    } else {
        var bkImgURL = eleID.split("./Designer/Products/");;
        //StopLoader();
        canvas.backgroundColor = "#ffffff";
        canvas.setBackgroundImage(eleID, canvas.renderAll.bind(canvas), {
            left: 0,
            top: 0,
            height: canvas.getHeight(),
            width: canvas.getWidth(),
            maxWidth: canvas.getWidth(),
            maxHeight: canvas.getHeight(),
            originX: 'left',
            originY: 'top'
        }); StopLoader();
        canvas.renderAll();
        $.each(TP, function (i, IT) {
            if (IT.ProductPageID == SP) {

                IT.BackgroundFileName = bkImgURL[bkImgURL.length - 1];
                IT.BackGroundType = 3;
                return;
            }
        });
    }

}
function k32_load(DT) {
    var p = DT.split(Tid + "/");
    var i = p[p.length - 1];
    var bkImgURL = p;
    StopLoader();
    canvas.backgroundColor = "#ffffff";
    canvas.setBackgroundImage(DT, canvas.renderAll.bind(canvas), {
        left: 0,
        top: 0,
        height: canvas.getHeight(),
        width: canvas.getWidth(),
        maxWidth: canvas.getWidth(),
        maxHeight: canvas.getHeight(),
        originX: 'left',
        originY: 'top'
    }); StopLoader();
    canvas.renderAll();
    k27();
    $.each(TP, function (op, IT) {
        if (IT.ProductPageID == SP) {
            // $("#ImgCarouselDiv").tabs("option", "active", 1); //open template background images tab
            IT.BackgroundFileName = Tid + "/" + i;
            IT.BackGroundType = 3;
            return;
        }
    });
}
function l4(caller) {
    if (llData.length > 0 || IsCalledFrom == 1) {
        $(".layoutsPanel").css("display", 'list-item');
        var html = "";
        var ClName = "";
        var PortCount = 0;
        var BtnCount = 0;
        if (caller != undefined && caller == 1) {
            $("#dropDownPresets").html(' <option value="0">(select)</option>');
            StopLoader();
        }
        $.each(llData, function (i, IT) {
            if (IT.Orientation == 1) {
                ClName = "BtnBCPresetOptionsLand";
            } else {
                ClName = "BtnBCPresetOptionsPort";
                PortCount++;
            }
            BtnCount++;
            if (PortCount == 1 ||BtnCount == 6) {
                html += "<br /><br />";

                BtnCount = 0;
            }
            if (IsCalledFrom == 1) {
                b1("dropDownPresets", IT.LayoutID, IT.Title, "itemPre" + IT.LayoutID);
            }
            var imURL = "";
            var mode = IT.ImageLogoType;
            if (mode == 1) {
                imURL = "assets/presets/preset5_2.png";
            } else if (mode == 2) {
                imURL = "assets/presets/preset5_1.png";
            } else if (mode == 3) {
                imURL =  "assets/presets/preset5.png";
            } else if (mode == 4) {
                imURL =  "assets/presets/preset4.png";
            } else if (mode == 5) {
                imURL =  "assets/presets/preset3.png";
            } else if (mode == 6) {
                imURL =  "assets/presets/preset2.png";
            } else if (mode == 7) {
                imURL =  "assets/presets/preset1.png";
            } else if (mode == 8) {
                imURL =  "assets/presets/preset6.png";
            } else if (mode == 9) {
                imURL =  "assets/presets/preset7.png";
            } else if (mode == 10) {
                imURL =  "assets/presets/preset8.png";
            } else if (mode == 11) {
                imURL =  "assets/presets/preset9.png";
            } else if (mode == 12) {
                imURL = "assets/presets/preset10.png";
            } else if (mode == 13) {
                imURL = "assets/presets/preset10_1.png";
            } else if (mode == 14) {
                imURL = "assets/presets/preset10_2.png";
            } else if (mode == 15) {
                imURL = "assets/presets/presets14.png";
            } else if (mode == 16) {
                imURL = "assets/presets/presets-15.png";
            } else if (mode == 17) {
                imURL = "assets/presets/presets16.png";
            } else if (mode == 18) {
                imURL = "assets/presets/presets11.png";
            } else if (mode == 19) {
                imURL = "assets/presets/presets12.png";
            } else if (mode == 20) {
                imURL = "assets/presets/presets-13.png";
            }
            html += '<button id="btnPreset' + IT.LayoutID + '" class="' + ClName + '" title="Left Presets" onClick="l5(' + IT.LayoutID + ')" style="background-image:url(' + imURL + ')  " ></button>';
            var id = "#btnPreset" + IT.LayoutID;
            $(id).css("background-image", '../assets/sprite.png');
        });
        $(".divLayoutBtnContainer").html(html);
        //if (IsCalledFrom == 1) {
        //    animatedcollapse.show('divPresetEditor');
        //}
    }
}

function m0() {
    m0_prePop();

}
function m0_prePop() {
    var OBS = canvas.getObjects();
    var html = '';
    var index1 = 0;
    for (var i = OBS.length - 1; i >= 0; i--) {
        var ite = OBS[i];
        $.each(TO, function (ij, IT) {
            if (ite.ObjectID == IT.ObjectID && ite.IsEditable != false) {
                if (i == 0) {
                    index1 = -1;
                }
                if (ite.type == "image") {
                    html += m0_i9(ite.ObjectID, 'Image Object', ite.type, ite.getSrc(), index1);
                } else if (ite.type == "text" || ite.type == "i-text") {
                    html += m0_i9(ite.ObjectID, ite.text, ite.type, "./assets/txtObject.png", index1);
                } else if (ite.type == "ellipse") {
                    html += m0_i9(ite.ObjectID, 'Ellipse Object', ite.type, "./assets/circleObject.png", index1);
                } else {
                    html += m0_i9(ite.ObjectID, 'Shape Object', ite.type, "./assets/rectObject.png", index1);
                }
                index1 += 1;
               
            }
        });

    }
    $("#sortableLayers").html(html);
    $("#sortableLayers").sortable({
        placeholder: "ui-state-highlight",
        update: function (event, ui) {
            i8($(ui.item).children(".selectedObjectID").text(), ui.item.index());
        },
        start: function (e, ui) {
            N111a = ui.item.index();
        }
    });
    $("#sortable").disableSelection();

    $(".btnMoveLayerUp").click(function () {
        var id = $(this).parent().children(".selectedObjectID").text();
        pcL27_find(id);
        m0_prePop();
        $("#sortableLayers li").removeClass("selectedItemLayers");
        $("#selobj_" + id).addClass("selectedItemLayers"); 
    });
    $(".btnMoveLayerDown").click(function () {
        var id = $(this).parent().children(".selectedObjectID").text();
        pcL28_find(id);
        m0_prePop();
        $("#sortableLayers li").removeClass("selectedItemLayers");
        $("#selobj_" + id).addClass("selectedItemLayers"); 
    });
    $(".editTxtBtn").click(function () {
        var id = $(this).parent().children(".selectedObjectID").text();
        var obj = canvas.getActiveObject();
        if (!obj) {
            j1(id);
        }
        g5_Sel();
    });
}
function m0_i9(oId, oName, OType, iURL, index1) {
    var html = "";
    var sObj = canvas.getActiveObject();
    var cid = 0;
    if (sObj) {
        cid = sObj.ObjectID;
    }
    var btnHtml = ' <button class="btnMoveLayerUp" ></button><button class="btnMoveLayerDown" ></button>';
    if (index1 == 0) {
        btnHtml = '<button class="btnMoveLayerDown" ></button>';
    } else if (index1 == -1) {
        btnHtml = ' <button class="btnMoveLayerUp" ></button>';
    }
    btnHtml += ' <button class="button editTxtBtn" >Edit</button>'
    if (cid == oId) {
        var innerHtml = "";
        html = '<li id="selobj_' + oId + '" class="ui-state-default uiOldSmothness" style="padding:5px;"><span class="selectedObjectID">' + oId + '</span>  <img class="layerImg" src="' + iURL + '" alt="Image" onclick="j1(' + oId + ')" /> <span class="spanLyrObjTxtContainer" onclick="j1(' + oId + ')">' + oName + '</span>' + btnHtml + ' <br /></li>';;//'<li id="selobj_' + oId + '" class="ui-state-default"></li>';
    } else {
        html = '<li id="selobj_' + oId + '" class="ui-state-default uiOldSmothness" style="padding:5px;"><span class="selectedObjectID">' + oId + '</span>  <img class="layerImg" src="' + iURL + '" alt="Image" onclick="j1(' + oId + ')" /> <span class="spanLyrObjTxtContainer" onclick="j1(' + oId + ')">' + oName + '</span>' + btnHtml + '</li>';

    }
    return html;
}
function pcL27_find(id) {
    //var Obj = null;
    //if (canvas.getActiveGroup()) {
    //    canvas.discardActiveGroup();
    //}
    //if (canvas.getActiveObject()) {
    //    Obj= canvas.discardActiveObject();
    //}
    var OBS = canvas.getObjects();
    $.each(OBS, function (it, ite) {
        if (ite.ObjectID === parseInt(id)) {
            var D1AO = ite;
            D1AO.bringForward();
            canvas.renderAll();
            g7();
            return false;
        }
    });
}
function pcL28_find(id) {
    //if (canvas.getActiveGroup()) {
    //    canvas.discardActiveGroup();
    //}
    //if (canvas.getActiveObject()) {
    //    canvas.discardActiveObject();
    //}
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, ite) {
        if (ite.ObjectID == id) {
            var D1AO = ite;
            D1AO.sendBackwards();
            canvas.renderAll();
            g7();
        }
    });

}
function pcL36(mode, arrayControls) {  // panels logic do here 
    //var notInPanel = " #quickText , #DivPersonalizeTemplate , #DivToolTip , #DivAdvanceColorPanel ,  #divPositioningPanel , #DivControlPanel1 , #divBCMenu , #btnShowMoreOptions , #divPopupUpdateTxt , #divVariableContainer , #PreviewerContainer , #divPresetEditor ";
    var controls = "";
    controls += ' #DivAlignObjs ,#divTxtPropPanelRetail ,#divImgPropPanelRetail ,#DivColorPickerDraggable ,#DivAdvanceColorPanel';
    //controls += '#addText , #addImage , #divImageDAM , #divImageEditScreen , #DivLayersPanel , #UploadImage , #ImagePropertyPanel , #ShapePropertyPanel ';
    //controls += ' , #textPropertPanel , #quickTextFormPanel , #DivUploadFont , #DivColorPallet ';
   // arrayControls += ', #divEditObj ';
    var closeControls = true;
    //var p = arrayControls.split(" , ");
    //$.each(p, function (i, item) {
    //    if (controls.indexOf(item + " ") != -1) {
    //        closeControls = true;
    //    }
    //});
   // if (closeControls && mode != "hide") {
      
  //  }
    if (mode == "show") {
        $(controls).css("display", "none");
        $(controls).css("opacity", "0");
        $(arrayControls).css("display", "block");
        $(arrayControls).css("opacity", "1");
    } else if (mode == "hide") {
        $(controls).css("display", "none");
        $(controls).css("opacity", "0");
        $(arrayControls).css("display", "none");
        $(arrayControls).css("opacity", "0");
    } else if (mode == "toggle") {
        if ($(arrayControls).css("display") == "none") {
            $(arrayControls).css("display", "block");
            $(arrayControls).css("opacity", "1");
        } else {
            $(arrayControls).css("display", "none");
            $(arrayControls).css("opacity", "0");
        }
    }

}