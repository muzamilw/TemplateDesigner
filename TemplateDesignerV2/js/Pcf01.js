function pcL01(mode) {
    if (mode == 1) {
        if ($(".toolbarText").css("opacity") == "0") {
            $(".toolbarText").css("display", "block");
            $(".toolbarText").css("opacity", "1");
        } else {
            $(".toolbarText").css("display", "none");
            $(".toolbarText").css("opacity", "0");
        }
    } else if (mode == 2) {
        $(".toolbarImageTransparency").css("display", "none");
        $(".toolbarImageTransparency").css("opacity", "0");
        if ($(".toolbarImage").css("opacity") == "0") {
            $(".toolbarImage").css("display", "block");
            $(".toolbarImage").css("opacity", "1");
        } else {
            $(".toolbarImage").css("display", "none");
            $(".toolbarImage").css("opacity", "0");
        }
    } else if (mode == 3) {
        $(".toolbarImage").css("display", "none");
        $(".toolbarImage").css("opacity", "0");
        if ($(".toolbarImageTransparency").css("opacity") == "0") {
            $(".toolbarImageTransparency").css("display", "block");
            $(".toolbarImageTransparency").css("opacity", "1");
        } else {
            $(".toolbarImageTransparency").css("display", "none");
            $(".toolbarImageTransparency").css("opacity", "0");
        }
    }
}
function pcL02() {
    pcL36('toggle', '#DivColorPallet');
}
function pcL03() {
    if (confirm("Are you sure you want to Remove this Object from the canvas.")) {
        var D1AO = canvas.getActiveObject(),
        D1AG = canvas.getActiveGroup();
        if (D1AO) {
            //  c2(D1AO, 'delete');
            c2_del(D1AO);
            canvas.remove(D1AO);
        }
        else if (D1AG) {
            var objectsInGroup = D1AG.getObjects();
            canvas.discardActiveGroup();
            objectsInGroup.forEach(function (OPT) {
                //  c2(OPT, 'delete');
                c2_del(OPT);
                canvas.remove(OPT);
            });
        }
        pcL36('hide', '#textPropertPanel');
    }
}
function pcL04(isCaller) {
    var fontFamily = $('#BtnSelectFonts').val();
    if (isCaller == 2) {
        fontFamily = $('#BtnSelectFontsRetail').val();
    }
    
    fontFamily = $('.fonts .selected').css('font-family');
    if (fontFamily.indexOf( "(select)") != -1) {
        fontFamily = "";
    }
    while (fontFamily.indexOf("'") != -1) {
        fontFamily = fontFamily.replace("'", "");
    }
    while (fontFamily.indexOf('"') != -1) {
        fontFamily = fontFamily.replace('"', '');
    }
    if (fontFamily != "") {
        var selectedObject = canvas.getActiveObject();
        if (selectedObject && selectedObject.isEditing == false) {
            if (selectedObject && (selectedObject.type === 'text' || selectedObject.type === 'i-text')) {
                selectedObject.fontFamily = fontFamily;
                $("#txtAreaUpdateTxt").css("font-family", fontFamily);
                //c2(selectedObject);
                canvas.renderAll();
            }
        } else {
            setActiveStyle("font-family", fontFamily);
        }
    }
    var selName = "select#BtnSelectFonts";
    if (IsCalledFrom == 3) {
        selName = "select#BtnSelectFontsRetail";
    }
    $(selName).fontSelector('option', 'close', 'Arial Black');
}
function pcL05() {
    var selectedObject = canvas.getActiveObject();
    if (selectedObject) {
        setActiveStyle('font-Weight', 'bold');
      //  c2(selectedObject);
        pcL22_Sub(selectedObject);
        canvas.renderAll();
    }
}
function pcL06() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {

        setActiveStyle('font-Style', 'italic');
        pcL22_Sub(D1AO);
       // c2(D1AO);
        canvas.renderAll();
    }
}
function pcL07() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        D1AO.set('textAlign', 'left');
        $("#txtAreaUpdateTxt").css("text-align", 'left');
     //   c2(D1AO);
        pcL22_Sub(D1AO);
        canvas.renderAll();
    }
}
function pcL08() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        D1AO.set('textAlign', 'center');
       // c2(D1AO);
        pcL22_Sub(D1AO);
        $("#txtAreaUpdateTxt").css("text-align", 'center');
        canvas.renderAll();
    }
}
function pcL09() {
    var D1AO = canvas.getActiveObject();
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        D1AO.set('textAlign', 'right');
      //  c2(D1AO);
        pcL22_Sub(D1AO);
        $("#txtAreaUpdateTxt").css("text-align", 'right');
        canvas.renderAll();
    }
}
function pcL10() {
    pcL36('hide', '#DivLayersPanel');
    var D1AG = canvas.getActiveGroup();
    var D1AO = canvas.getActiveObject();
    D1CO = [];
    if (D1AG) {
        var objectsInGroup = D1AG.getObjects();
        $.each(objectsInGroup, function (j, Obj) {
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == Obj.ObjectID) {
                    c2_01(Obj);
                    D1CO.push(IT);
                    return false;
                }
            });
        });
    } else if (D1AO) {
        $.each(TO, function (i, IT) {
            if (IT.ObjectID == D1AO.ObjectID) {
                c2_01(D1AO);
                D1CO.push(IT);
                return false;
            }
        });
    }
}
function pcL11() {
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var angle = D1AO.getAngle();
    angle = angle - 5;
    D1AO.setAngle(angle);
   // c2(D1AO);
    canvas.renderAll();
}
function pcL12() {
    var D1AO = canvas.getActiveObject();
    if (!D1AO) return;
    var angle = D1AO.getAngle();
    angle = angle + 5;
    D1AO.setAngle(angle);
  //  c2(D1AO);
    canvas.renderAll();
}
function pcL13() {
    //close all panels  

    $(".retailPropPanels").css("display", "none");
    $(".retailPropPanelsSubMenu").css("display", "none");
    $(".retailPropPanelsSubMenu").css("opacity", "0");
}
function pcL14() {
    var D1AO = canvas.getActiveObject();
    var oldW = D1AO.getWidth();
    var oldH = D1AO.getHeight();
    var scale = D1AO.get('scaleX') + 0.05;
    D1AO.set('scaleX', scale);
    scale = D1AO.get('scaleY') + 0.05;
    D1AO.set('scaleY', scale);
    var dif = D1AO.getWidth() - oldW;
    dif = dif / 2
    D1AO.left = D1AO.left + dif;
    dif = D1AO.getHeight() - oldH;
    dif = dif / 2
    D1AO.top = D1AO.top + dif;
   // c2(D1AO);
    canvas.renderAll();
}
function pcL15() {
    var D1AO = canvas.getActiveObject();
    var oldW = D1AO.getWidth();
    var oldH = D1AO.getHeight();
    var scale = D1AO.get('scaleX') - 0.05;
    if (scale > 0.10) {
        D1AO.set('scaleX', scale);
    }
    scale = D1AO.get('scaleY') - 0.05;
    if (scale > 0.10) {
        D1AO.set('scaleY', scale);
    }
    var dif = D1AO.getWidth() - oldW;
    dif = dif / 2
    D1AO.left = D1AO.left + dif;
    dif = D1AO.getHeight() - oldH;
    dif = dif / 2
    D1AO.top = D1AO.top + dif;
  //  c2(D1AO);
    canvas.renderAll();
}
function pcL16() {
    var D1AO = canvas.getActiveObject();
    var angle = D1AO.getAngle();
    angle = angle - 5;
    D1AO.setAngle(angle);
  //  c2(D1AO);
    canvas.renderAll();
}
function pcL17() {
    var D1AO = canvas.getActiveObject();
    var angle = D1AO.getAngle();
    angle = angle + 5;
    D1AO.setAngle(angle);
   // c2(D1AO);
    canvas.renderAll();
}
function pcL18() {
    var D1AO = canvas.getActiveObject();
    canvas.bringForward(D1AO);
    canvas.renderAll();
  //  c2(D1AO);
    g7();
}
function pcL19() {
    var D1AO = canvas.getActiveObject();
    canvas.sendBackwards(D1AO);
    canvas.renderAll();
  //  c2(D1AO);
    g7();
}
function pcL20() {
    var D1AO = canvas.getActiveObject();
    var src;
    if (D1AO && D1AO.type === 'image' && D1AO) {
        src = D1AO.getSrc();
        if (src.indexOf("Imageplaceholder.png") == -1) {
            $("#CropImgContainer").html('<img id="CropTarget" src="' + src + "?r=" + fabric.util.getRandomInt(1, 100) + '" style="visibility:hidden;" />');
            $(function () {
                $('#CropTarget').Jcrop(
                {
                    boxWidth: 210,
                    boxHeight: 250,
                    onChange: f8,
                    onSelect: f8
                });
            });
            pcL36('hide', '#divPositioningPanel');
            pcL36('toggle', '#DivCropToolContainer');
        } else {
            if (IsCalledFrom != 4) {
                alert("Please add an image to crop it!");
            }
        }
    }
}
var crX = 0;
var crY = 0;
var crWd = 0;
var crHe = 0;
var crv1 = 0;
var crv2 = 0;
var crv3 = 0;
var crv4 = 0;
var crv5 = 0;

function pcL20_new() {
    var D1AO = canvas.getActiveObject();
    var src;
    if (D1AO && D1AO.type === 'image' && D1AO) {
        src = D1AO.getSrc();
        if (src.indexOf("Imageplaceholder.png") == -1) {
            $(".cropimage").attr('src', src + "?r=" + fabric.util.getRandomInt(1, 100));
            $(function () {
                $('.cropimage').each(function () {
                    var image = $(this);
                    $(".closePanelButtonCropTool").css("left", (D1AO.getWidth() - 35) + "px");
                    $(".CropControls").css("height", (D1AO.getHeight() +5) + "px");
                    $(".CropControls").css("width", (D1AO.getWidth() + 5) + "px");
                    $(".NewCropToolCotainer").css("height", $(document).height() + "px");
                    var width = $(".CropControls").width() / 2;
                    var height = $(".CropControls").height()/2 ;
                    $(".CropControls").css("left", ($(window).width() / 2 - canvas.getWidth() / 2 + D1AO.left - width + 4) + "px");
                    $(".CropControls").css("top", (164 + D1AO.top - D1AO.getHeight()/2) + "px");
                    image.cropbox({ width: D1AO.getWidth(), height: D1AO.getHeight(), showControls: 'auto', xml: D1AO.ImageClippedInfo })
                      .on('cropbox', function (event, results, img) {
                          crX = (results.cropX);
                          crY = (results.cropY);
                          crWd = (results.cropW);
                          crHe = (results.cropH);
                          crv1 = results.crv1;
                          crv2 = results.crv2;
                          crv3 = results.crv3;
                          crv4 = results.crv4;
                          crv5 = results.crv5;
                          pcL20_new_MoveImg(src, results.crv1, results.crv6, results.crv7);
                      });
                    $(".cropButton").click(function (event) {
                        //pcL20();
                        pcL20_newCrop(src);
                    });
                });

             

            }); 
            pcL36('hide', '#divPositioningPanel');
            $("#divBkCropTool").css("display", "block");
           // pcL36('toggle', '#divBkCropTool');
            pcL36('toggle', '#NewCropToolCotainer');

        } else {
            if (IsCalledFrom != 4) {
                alert("Please add an image to crop it!");
            }
        }
    }
}
function pcL20_new_MoveImg(src, percent, AcHei, AcWid) {
    
    $(".imgOrignalCrop").attr("src", src);
    $(".imgOrignalCrop").attr("width", Math.round(AcWid * percent));
    $(".imgOrignalCrop").attr("height", Math.round(AcHei * percent));
    $(".imgOrignalCrop").css("left", $(".cropImage").css("left"));
    $(".imgOrignalCrop").css("top", $(".cropImage").css("top"));
  //  var position = $(".cropImage").offset();
 //   $('.overlayHoverbox').css(position);
}
function pcL20_newCrop() {
    //alert(crX + " " + crY + " " + crHe + " " + crWd);
    var XML = new XMLWriter();
    XML.BeginNode("Cropped");

    XML.Node("sx", crX.toString());
    XML.Node("sy", crY.toString());
    XML.Node("swidth", crWd.toString());
    XML.Node("sheight", crHe.toString());


    XML.Node("crv1", crv1.toString());
    XML.Node("crv2", crv2.toString());
    XML.Node("crv3", crv3.toString());
    XML.Node("crv4", crv4.toString());
    XML.Node("crv5", crv5.toString());
    XML.EndNode();
    XML.Close();
    var D1AO = canvas.getActiveObject();
    if (D1AO && D1AO.type == 'image') {
        D1AO.ImageClippedInfo = XML.ToString().replace(/</g, "\n<");
        canvas.renderAll();
    }
    pcl20_newCropCls();
}
function pcl20_newCropCls() {
    if (croppedInstance != null) {
        croppedInstance.remove();
        croppedInstance = null;
    }
    $("#divBkCropTool").css("display", "none");
    pcL36('hide', '#NewCropToolCotainer');
}


function pcL21() {
    if (confirm("Are you sure you want to Remove this Object from the canvas.")) {
        var D1AO = canvas.getActiveObject(),
        D1AG = canvas.getActiveGroup();
        if (D1AO) {
            //   c2(D1AO, 'delete');
            c2_del(D1AO);
            canvas.remove(D1AO);
        }
        else if (D1AG) {
            var objectsInGroup = D1AG.getObjects();
            canvas.discardActiveGroup();
            objectsInGroup.forEach(function (OPT) {
                //  c2(OPT, 'delete');
                c2_del(OPT);
                canvas.remove(OPT);
            });
        }
        pcL36('hide', '#ImagePropertyPanel');
    }
}

function pcL22(D1AO) {
    pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer');
    //show text prop retail
    //var l = $("#canvas").offset().left - $(".txtMenuDiv").width() + D1AO.left + D1AO.width / 2;
    //var h = $("#canvas").offset().top - $(".txtMenuDiv").height() + D1AO.top - D1AO.height / 2 - 20;
    //if (!IsBC) {
    //    h -= 148;
    //}
    $("#divTxtPropPanelRetail").css("display", "block");
    pcL22_Sub(D1AO);
    //$("#divTxtPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
    $(".toolbarText").css("display", "none");
    $(".toolbarText").css("opacity", "0");
}
function pcL22_Sub(D1AO) {
    $(".spanRectColour").css("background-color", D1AO.fill);
    $(".spanTxtcolour").css("background-color", D1AO.fill);
    if (D1AO.fontWeight == "bold") {
        $(".textToolbarBold").addClass("propOn");
    } else {
        $(".textToolbarBold").removeClass("propOn");
    }
    if (D1AO.fontStyle == "italic")
        $(".textToolbarItalic").addClass("propOn");
    else
        $(".textToolbarItalic").removeClass("propOn");

    $(".textToolbarLeft").removeClass("propOn");
    $(".textToolbarCenter").removeClass("propOn");
    $(".textToolbarRight").removeClass("propOn");
    if (D1AO.textAlign == "left")
        $(".textToolbarLeft").addClass("propOn");
    else if (D1AO.textAlign == "center")
        $(".textToolbarCenter").addClass("propOn");
    else if (D1AO.textAlign == "right")
        $(".textToolbarRight").addClass("propOn");
}
function pcL23(mg) {
    var size = $(".toast-item-wrapper .toast-type-error").size();
    if (size == 0) {
        $().toastmessage('showToast', {
            text: mg,
            sticky: false,
            position: 'top-right',
            type: 'error',
            closeText: ''
        });
    }
}
function pcL24(mg) {
    var size = $(".toast-item-wrapper .toast-type-warning").size();
    if (size == 0) {
        $().toastmessage('showToast', {
            text: mg,
            sticky: false,
            position: 'top-right',
            type: 'warning',
            closeText: ''
        });
    }
}
function pcL24_n(mg) {
    var size = $(".toast-item-wrapper .toast-type-notice").size();
    if (size == 0) {
        $().toastmessage('showToast', {
            text: mg,
            sticky: false,
            position: 'top-right',
            type: 'notice',
            closeText: ''
        });
    }
}
function pcL25() {
    var D1AO = canvas.getActiveObject();
    canvas.sendToBack(D1AO);
    canvas.renderAll();
   // c2(D1AO);
    g7();
}
function pcL26() {
    var D1AO = canvas.getActiveObject();
    canvas.bringToFront(D1AO);
 //   c2(D1AO);
    canvas.renderAll();
    g7();
}
function pcL27() {
    var D1AO = canvas.getActiveObject();
    canvas.bringForward(D1AO);
    canvas.renderAll();
  //  c2(D1AO);
    g7();
}
function pcL28() {
    var D1AO = canvas.getActiveObject();
    canvas.sendBackwards(D1AO);
    canvas.renderAll();
  //  c2(D1AO);
    g7();
}
function pcL27_find(id) {
    if (canvas.getActiveGroup()) {
        canvas.discardActiveGroup();
    }
    if (canvas.getActiveObject()) {
        canvas.discardActiveObject();
    } 
    var OBS = canvas.getObjects();
    $.each(OBS, function (it, ite) {
        if (ite.ObjectID === parseInt( id)) {
            var D1AO = ite;
            D1AO.bringForward();
            canvas.renderAll();
            g7(); console.log(id);
            return false;
        }
    });
}
function pcL28_find(id) {
    if (canvas.getActiveGroup()) {
        canvas.discardActiveGroup();
    }
    if (canvas.getActiveObject()) {
        canvas.discardActiveObject();
    }
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
function pcL29(fontSize,isBold) {
   // e0(); // l3
   
    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "New Text";
    D1NTO.ContentString = $('#txtAddNewText').val();
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = isBold;
    D1NTO.IsItalic = false;
    D1NTO.LineSpacing = 1.4;
    D1NTO.CharSpacing = 0;
    D1NTO.ProductPageId = SP;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    var text = $('#txtAddNewText').val();
    //alert(text.length);
    var textLength = text.length;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 80;

    if (textLength < 30) {
        var diff = textLength / 10;
        D1NTO.MaxWidth = 100 * diff;
    } else {
        D1NTO.MaxWidth = 190;
        var diff = textLength / 30;
        D1NTO.MaxHeight = 15 * diff;
    }
    D1NTO.IsQuickText = false;

    D1NTO.FontSize = fontSize;


    var uiTextObject = c0(canvas, D1NTO);
    canvas.centerObject(uiTextObject);
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    canvas.renderAll();
    uiTextObject.setCoords();
    $('#txtAddNewText').val("");
    TO.push(D1NTO);
}
function pcL30() {
    k21Bk();
}
function pcL31() {
    k17Bk();
}
function pcL32() {
    k24Bk();
}
function pcL33() {
    k21();
}
function pcL34() {
    k17();
}
function pcL35() {
    k24();
}
function pcL36(mode, arrayControls) {
    var notInPanel = " #quickText , #DivPersonalizeTemplate , #DivToolTip , #DivAdvanceColorPanel ,  #divPositioningPanel , #DivControlPanel1 , #divBCMenu , #btnShowMoreOptions , #divPopupUpdateTxt , #divVariableContainer , #PreviewerContainer , #divPresetEditor ";
    var controls = "";
    controls += '#addText , #addImage , #divImageDAM , #divImageEditScreen , #DivLayersPanel , #UploadImage , #ImagePropertyPanel , #ShapePropertyPanel '; 
    controls += ' , #textPropertPanel , #quickTextFormPanel , #DivUploadFont , #DivColorPallet ';
    controls += ', #DivCropToolContainer , #DivAlignObjs ';
    controls += ', #divBCMenuPresets ';
    var closeControls = false;
    var p = arrayControls.split(" , ");
    $.each(p, function (i, item) {
        if (controls.indexOf(item + " " ) != -1) {
            closeControls = true;
        }
    });
    if (closeControls && mode != "hide") {
        $(controls).css("display", "none");
        $(controls).css("opacity", "0");
    }
    if (mode == "show") {
        $(arrayControls).css("display", "block");
        $(arrayControls).css("opacity", "1");
    } else if (mode == "hide") {
        $(arrayControls).css("display", "none");
        $(arrayControls).css("opacity", "0");
    } else if (mode == "toggle") {
        if ($(arrayControls).css("opacity") == "0") {
            $(arrayControls).css("display", "block");
            $(arrayControls).css("opacity", "1");
        } else {
            $(arrayControls).css("display", "none");
            $(arrayControls).css("opacity", "0");
        }
    }

}

function pcl37() {
    $("#ImgCarouselDiv").tabs("option", "active", 0);
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    isBKpnl = false;
    $(".placeHolderControls").css("display", "block");
    $("#BkImgContainer, #ShapesContainer, #LogosContainer").css("display", "none");
    if (IsCalledFrom == 1 || IsCalledFrom == 3 || IsCalledFrom == 2) {
        $(".divImageTypes").css("display", "block");
    }
    $("#ImgCarouselDiv").css("display", "block");
    $(".bkPanel").css("display", "none");
    $(".imgPanel2").css("display", "none");
    $(".imgPanel").css("display", "");
    $('#uploader_browse').text("Upload Image");
    $('#uploader_browse').css("padding-left", "15px");
    $('#uploader_browse').css("width", "92px"); $('.RsizeDiv').css("margin-left", "7px");
    $('#uploader_browse').css("margin-left", "-10px"); $('.RsizeDiv').css("width", "253px");
    if (IsEmbedded) {
        $("#btnAddImagePlaceHolder").css("display", "none");
        $(".spanImgPlaceHolder").css("display", "none");
        $("#btnCompanyPlaceHolder").css("display", "none");
        $("#btnContactPersonPlaceHolder").css("display", "none"); $(".placeHolderControls").css("display", "none");
    } else {
        $('.DamImgContainer').css("height", "311px");
    } pcL13();
    pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate ,#DivAdvanceColorPanel ,#quickText');
    pcL36('show', '#divImageDAM');
}

function pcL38() {
    $("#ShapesContainer").tabs("option", "active", 0);
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    isBKpnl = false;
    $("#ShapesContainer").css("display", "block");
    if (IsCalledFrom == 1 || IsCalledFrom == 3 || IsCalledFrom == 2) {
        $(".divImageTypes").css("display", "block");
    }
    $("#ImgCarouselDiv, #BkImgContainer, #LogosContainer").css("display", "none");
    $(".placeHolderControls").css("display", "none");
    $(".bkPanel").css("display", "none");
    $(".imgPanel").css("display", "none");
    $('#uploader_browse').text("Upload Image");
    $('#uploader_browse').css("padding-left", "15px");
    $('#uploader_browse').css("width", "92px"); $('.RsizeDiv').css("margin-left", "29px");
    $('#uploader_browse').css("margin-left", "15px"); $('.RsizeDiv').css("width", "253px");
    $(".imgPanel2").css("display", "");
    if (IsEmbedded) {
        $("#btnAddImagePlaceHolder").css("display", "none");
        $(".spanImgPlaceHolder").css("display", "none");
        $("#btnCompanyPlaceHolder").css("display", "none");
        $("#btnContactPersonPlaceHolder").css("display", "none"); $(".placeHolderControls").css("display", "none");
    } else {
        $('.DamImgContainer').css("height", "311px");
    } pcL13();
    pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate ,#DivAdvanceColorPanel ,#quickText');
    pcL36('show', '#divImageDAM');
}

function pcl39() {
    $("#LogosContainer").tabs("option", "active", 0);
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    isBKpnl = false;
    $("#LogosContainer").css("display", "block");
    if (IsCalledFrom == 1 || IsCalledFrom == 3 || IsCalledFrom == 2) {
        $(".divImageTypes").css("display", "block");
    }
    $("#ImgCarouselDiv, #BkImgContainer, #ShapesContainer").css("display", "none");
    $(".placeHolderControls").css("display", "none");
    $(".bkPanel").css("display", "none");
    $(".imgPanel").css("display", "none");
    $('#uploader_browse').text("Upload Image");
    $(".imgPanel2").css("display", "none");
    $('#uploader_browse').css("padding-left", "5px");
    $('#uploader_browse').css("width", "92px"); $('.RsizeDiv').css("margin-left", "8px");
    $('#uploader_browse').css("margin-left", "0px"); $('.RsizeDiv').css("width", "253px");
    if (IsEmbedded) {
        $("#btnAddImagePlaceHolder").css("display", "none");
        $(".spanImgPlaceHolder").css("display", "none");
        $("#btnCompanyPlaceHolder").css("display", "none");
        $("#btnContactPersonPlaceHolder").css("display", "none"); $(".placeHolderControls").css("display", "none");
    } else {
        $('.DamImgContainer').css("height", "311px");
    } pcL13();
    pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate ,#DivAdvanceColorPanel ,#quickText');
    pcL36('show', '#divImageDAM');
}