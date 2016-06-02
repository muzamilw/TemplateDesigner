
var ctrlDown = false;
var shftlDown = false; 
var shiftKey = 16, ctrlKey = 17, vKey = 86, cKey = 67;
var copiedObject = [];
var IsFontLegal;
var IsDesignModified = false;   //flag used for detecting if there are unsaved changes
var SCALE_FACTOR = 1.2;
var canvasScale = 1;
// used in case of redo and undo 
var currentZoomLevel = 0;
var BoolDisplayFirstTip = 1;
var IsCoorporate = 0;
var IsTipEnabled = 0; 
var undoManager = new UndoManager();
var lastPanel = "";
// variables used to set height, width , margin left and margin right in crop tool

var CropedHeight;
var CropedWidth;
var CropedMarginLeft;
var CropedMarginTop;
var CropedObjID;
var CopiedCanvasObjects;


function performCoorporate() {
    IsCoorporate = 1;
    $("#BtnQuickTextForm").css("visibility", "hidden");
   // animatedcollapse.hide('BtnQuickTextForm');
}
//////var isCanvasCopied;
$("#BtnSave").click(function (event) {

    save("save");
//	var jsonObjects = JSON.stringify(TemplateObjects, null, 2);

//	// var jsonObjects = JSON.parse(TemplateObjects, null);

//	var to = "services/TemplateSvc/update/";
//	var options = {
//		type: "POST",
//		url: to,
//		data: jsonObjects,
//		contentType: "application/json;",
//		dataType: "json",
//		async: false,
//		success: function (response) {
//			//alert("success: " + response);
//		},
//		error: function (msg) { alert("Error : " + msg); }
//	};

//	var returnText = $.ajax(options).responseText;
//	IsDesignModified = false;
//	alert("saved");

});

$("#BtnPreview").click(function (event) {

    ResetZoom();
    save("preview");

});


$("#BtnPreviewNew").click(function (event) {


    save("preview");

});



$("#BtnContinue").click(function (event) {


    save("continue");

});

function save(mode) {
    ResetZoom();
    var dheight = $(window).height();
    dheight = dheight - 50;
    $("#loadingMsg").html("Saving and Generating Preview..");
    StartLoader();
    
    //saving the objects first
    var jsonObjects = JSON.stringify(TemplateObjects, null, 2);
    var to;
    if (mode == "save")
        to = "services/TemplateSvc/update/";
    else if (mode == "preview")
        to = "services/TemplateSvc/preview/";
    else if (mode == "continue")
        to = "services/TemplateSvc/savecontinue/";


var options = {
    type: "POST",
    url: to,
    data: jsonObjects,
    contentType: "application/json;",
    dataType: "json",
    async: true,
    complete: function (httpresp, returnstatus) {
        if (returnstatus == "success") {
           
            if (httpresp.responseText == '"true"') {
                PreviewLogic(mode, dheight);
            }
            else {
                StopLoader();
                alert(returnstatus);
            }

        }

    }
};


    var returnText =  $.ajax(options).responseText;
    //alert("success: " + returnText);
    //PreviewLogic(returnText,mode,dheight);
}

function PreviewLogic( mode, dheight) {

    
        IsDesignModified = false;
    


    if (mode == "preview") {


        var previewer = $("#Previewer");
        var shadow = document.getElementById("previewershadow");
        var bws = getBrowserHeight();
        
        



        //$("#Previewer").style.left = parseInt((bws.width - 350) / 2);
        //$("#Previewer").style.top = parseInt((bws.height - 200) / 2);

        var P1Path = "";


        $.each(TemplatePages, function (i, item) {
            $("#Previewer").append("<DIV class='previewerpage'><img id='pimg" + item.PageNo + "' class='previmg' src='designer/products/" + TemplateID + "/p" + item.PageNo + ".png?r=" + fabric.util.getRandomInt(1, 100) + "' ><div class='PreviewerPGLbl'> " + item.PageName + " </div></DIV>");
            if (i == 0) {

                P1Path = "designer/products/" + TemplateID + "/p" + item.PageNo + ".png?r=" + fabric.util.getRandomInt(1, 100);


            }
        });


        var pic_real_width, pic_real_height;
        $("<img/>") // Make in memory copy of image to avoid css issues 
        .attr("src", P1Path)
        .load(function () {
            pic_real_width = this.width;   // Note: $(this).width() will not 
            pic_real_height = this.height; // work for in memory images. 


            $("#PreviewerContainer").css("height", dheight + "px");
            $("#PreviewerContainer").center();
            $("#PreviewerContainer").show("clip", null, 500);

            $("#Previewer").css("height", dheight - 20 + "px");
            $("#Previewer").css("width", "920px");
            $("#Previewer").css("margin", "auto");

            //display: inline-block;

            if (pic_real_width > pic_real_height) {
                $(".previmg").css("width", "460px");
                $(".previmg").css("height", "auto");
            }
            else if (pic_real_width > 460) {
                $(".previmg").css("width", "460px");
                $(".previmg").css("height", "auto");
            }
            else {
                $(".previmg").css("width", "auto");
                $(".previmg").css("height", dheight - 40 + "px");
            }


            $("#Previewer").turn();

            shadow.style.display = "block";
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            shadow.style.background = "gray";
            
            StopLoader();
            $("#loadingMsg").html("Saving Content, Please wait..");
        });

       



    }
    else if (mode == "continue") {
        parent.SaveAttachments();
        //alert('save and continue after successful save');
        StopLoader();
        $("#loadingMsg").html("Saving Content, Please wait..");
    }
    else if (returnText != '"true"') {
        alert("error z : " + returnText);
        StopLoader();
        $("#loadingMsg").html("Saving Content, Please wait..");
    }




}

// function to set and get  cookies using javascript copied from w3 school by saqib 
    function setCookie(c_name,value,exdays)
    {
        var exdate=new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
        document.cookie=c_name + "=" + c_value;
    }
    function getCookie(c_name)
    {
        var i,x,y,ARRcookies=document.cookie.split(";");
        for (i=0;i<ARRcookies.length;i++)
        {
            x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
            y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
            x=x.replace(/^\s+|\s+$/g,"");
            if (x==c_name) {
                return unescape(y);
            }
        }
    }
 // function to set the current position of panel according to position of the active object 

    function setPanelPosition(currentpanelname, lastpanelname) 
    {
        if(lastpanelname === 'text')
        {
            lastpanelname = "textPropertPanel";
        } else if ( lastpanelname  === 'image' || lastpanelname  === 'path'  || lastpanelname  === 'rect' || lastpanelname  === 'ellipse') 
        {
            lastpanelname = "ImagePropertyPanel";
        } else if( lastpanelname  === 'group')
        {
            lastpanelname = "DivAlignObjs";
        }

        var lastPanelLeft = $("#" + lastpanelname).css("left");
        var lastPanelTop =  $("#" + lastpanelname).css("top");
        if(lastpanelname != undefined)
        {
            $("#" + currentpanelname).css("left", lastPanelLeft);
            $("#" + currentpanelname).css("top", lastPanelTop);

            //console.log(lastpanelname + " " + currentpanelname + " " +  lastPanelLeft + " " + lastPanelTop +  " " + ($("#" + currentpanelname).css("left")));

        }
        
    }

//checking if there are any unsaved changes before unloading and prompting user
window.onbeforeunload = function () {
    if (IsDesignModified) {
        //window.confirm("You have unsaved changes. Do you want to leave without saving changes ?");
        return "You have unsaved changes. Do you want to leave without saving changes ?";
    }
};



function EnableTips() {
    if (getCookie(  "IsTipEnabled") == "0") {
        $("#ShowTips").click();
    }
}
function ZoomIn()
{       
        if (canvasScale < 2.9) {

            canvasScale = canvasScale * SCALE_FACTOR;

            canvas.setHeight(canvas.getHeight() * SCALE_FACTOR);
            canvas.setWidth(canvas.getWidth() * SCALE_FACTOR);

            var objects = canvas.getObjects();
            for (var i in objects) {
                var scaleX = objects[i].scaleX;
                var scaleY = objects[i].scaleY;
                var left = objects[i].left;
                var top = objects[i].top;

                var tempScaleX = scaleX * SCALE_FACTOR;
                var tempScaleY = scaleY * SCALE_FACTOR;
                var tempLeft = left * SCALE_FACTOR;
                var tempTop = top * SCALE_FACTOR;

                objects[i].scaleX = tempScaleX;
                objects[i].scaleY = tempScaleY;
                objects[i].left = tempLeft;
                objects[i].top = tempTop;

                objects[i].setCoords();
                updateTemplateObjsZoom(objects[i]);
            }
        }

}
function ActionZoom()
{
        canvas.setHeight(canvas.getHeight() * (1 / canvasScale));
        canvas.setWidth(canvas.getWidth() * (1 / canvasScale));

        var objects = canvas.getObjects();
        $.each(objects, function (i, item) {
            var scaleX = item.scaleX;
            var scaleY = item.scaleY;
            var left = item.left;
            var top = item.top;

            var tempScaleX = scaleX * (1 / canvasScale);
            var tempScaleY = scaleY * (1 / canvasScale);
            var tempLeft = left * (1 / canvasScale);
            var tempTop = top * (1 / canvasScale);

            item.scaleX = tempScaleX;
            item.scaleY = tempScaleY;
            item.left = tempLeft;
            item.top = tempTop;

            item.setCoords();
            updateTemplateObjsZoom(item);
        });

        
        //canvas.renderAll();
    
        canvasScale = 1;
        canvasScale = 1;
        canvas.setHeight(canvas.getHeight() * (1 / canvasScale));
        canvas.setWidth(canvas.getWidth() * (1 / canvasScale));

        var objects = canvas.getObjects();
        $.each(objects, function (i, item) {
            var scaleX = item.scaleX;
            var scaleY = item.scaleY;
            var left = item.left;
            var top = item.top;

            var tempScaleX = scaleX * (1 / canvasScale);
            var tempScaleY = scaleY * (1 / canvasScale);
            var tempLeft = left * (1 / canvasScale);
            var tempTop = top * (1 / canvasScale);

            item.scaleX = tempScaleX;
            item.scaleY = tempScaleY;
            item.left = tempLeft;
            item.top = tempTop;

            item.setCoords();
            updateTemplateObjsZoom(item);
        });

        
        //canvas.renderAll();
    
        canvasScale = 1;
        canvasScale = 1;
}
function zoomOut()
{
        if (canvasScale > 0.61) {
            canvasScale = canvasScale / SCALE_FACTOR;
            canvas.setHeight(canvas.getHeight() * (1 / SCALE_FACTOR));
            canvas.setWidth(canvas.getWidth() * (1 / SCALE_FACTOR));
            var objects = canvas.getObjects();
            for (var i in objects) {
                var scaleX = objects[i].scaleX;
                var scaleY = objects[i].scaleY;
                var left = objects[i].left;
                var top = objects[i].top;
                var tempScaleX = scaleX * (1 / SCALE_FACTOR);
                var tempScaleY = scaleY * (1 / SCALE_FACTOR);
                var tempLeft = left * (1 / SCALE_FACTOR);
                var tempTop = top * (1 / SCALE_FACTOR);
                objects[i].scaleX = tempScaleX;
                objects[i].scaleY = tempScaleY;
                objects[i].left = tempLeft;
                objects[i].top = tempTop;
                objects[i].setCoords();
                updateTemplateObjsZoom(objects[i]);
            }
        }
        
}
function ClosePreview() {
    $("#Previewer").turn("destroy");
    $("#PreviewerContainer").hide("clip",null,500);
    var shadow = document.getElementById("previewershadow");
    shadow.style.display = "none";
}


jQuery.fn.center = function () {
    this.css("position", "absolute");
    this.css("top", (($(window).height() - this.outerHeight()) / 2) +
                                                $(window).scrollTop() + "px");
    this.css("left", (($(window).width() - this.outerWidth()) / 2) +
                                                $(window).scrollLeft() + "px");
    return this;
}

$("#ShowTips").click(function (event) {

    if (getCookie("IsTipEnabled") == "1") {
        IsTipEnabled = 0;
        setCookie("IsTipEnabled","0",7);
        animatedcollapse.hide('DivToolTip');

        $("#SpanTips").html("Show");
        //SwitchPositions(0);
    } else {
        setCookie("IsTipEnabled","1",7);
        IsTipEnabled = 1;
        ToolTipHandler(6);
        //SwitchPositions(1);
        $("#SpanTips").html("Hide");
    }
});


// function to switch position when tips are enabled and disabled
function SwitchPositions( status) {
    // status 1 tips are enabled
    if(status == 1) {
        $('.DivToolTipStyle').css("top", "8px");
        $('#textPropertPanel').css("top", "125px");
        $('.panelBasics').css("top", "245px");
        $('#DivColorPickerDraggable').css("top", "116px");
        $('#ImagePropertyPanel').css('top', '245px');
        $('#DivCropToolContainer').css("top", "245px");
        $('#addImage').css("top", "245px");
        $('#quickTextFormPanel').css("top", "126px");
        $('#addText').css("top", "245px");
        $('.popUpQuickTextPanel').css("top", "245px");
        $('#DivAdvanceColorPanel').css("top", "0px");
    } else {
        $('#textPropertPanel').css("top", "55px");
        $('.panelBasics').css("top", "175px");
        $('#DivColorPickerDraggable').css("top", "50px");
        $('#ImagePropertyPanel').css('top', '175px');
        $('#DivCropToolContainer').css("top", "175px");
        $('#addImage').css("top", "175px");
        $('#quickTextFormPanel').css("top", "55px");
        $('#addText').css("top", "176px");
        $('.popUpQuickTextPanel').css("top", "175px");
        
        $('#DivAdvanceColorPanel').css("top", "0px");
    }
}
// function to sync template obects with zoom function when page change is called it updates only positions not otherContent
function updateTemplateObjsZoomPageChange(object) {
    $.each(TemplateObjects, function (i, item) {
        if (item.ObjectID == object.ObjectID) {

            item.PositionX = object.left - item.MaxWidth / 2;
            item.PositionY = object.top - item.MaxHeight / 2;
            item.RotationAngle = object.getAngle();

            if (object.type != "text") {  // all other objects like image, vectors and svg
                item.MaxWidth = object.width * object.scaleX;
                item.MaxHeight = object.height * object.scaleY;


                if (object.type == "ellipse") {

                    item.CircleRadiusX = object.getRadiusX();
                    item.CircleRadiusY = object.getRadiusY();
                    //alert(item.CircleRadiusX);
                    item.PositionX = object.left - object.width / 2;
                    item.PositionY = object.top - object.height / 2;
                }
            }

            else { //object is text
                item.MaxWidth = object.maxWidth;
                item.MaxHeight = object.maxHeight;
                item.LineSpacing = object.lineHeight;
            }

            if (object.textAlign == "left")
                item.Allignment = 1;
            else if (object.textAlign == "center")
                item.Allignment = 2;
            else if (object.textAlign == "right")
                item.Allignment = 3;

            if (object.fontFamily != undefined)
                item.FontName = object.fontFamily;
            else
                item.FontName = "";

            if (object.fontSize != undefined)
                item.FontSize = object.fontSize;
            else
                item.FontSize = 0;

            if (object.fontWeight == "bold")
                item.IsBold = true;
            else
                item.IsBold = false;

            if (object.fontStyle == "italic")
                item.IsItalic = true;
            else
                item.IsItalic = false;

            if (object.type != "image") {
                item.ColorHex = object.fill;

            }

            if (object.type == "path") {
                item.ExField1 = object.strokeWidth;
            }


            item.Opacity = object.opacity;

            item.ColorC = object.C;
            item.ColorM = object.M;
            item.ColorY = object.Y;
            item.ColorK = object.K;

            item.IsPositionLocked = object.IsPositionLocked;
            item.IsHidden = object.IsHidden;
            item.IsEditable = object.IsEditable;





        }

    });

}

// function to sync template obects with zoom function 
function updateTemplateObjsZoom(object) {
    $.each(TemplateObjects, function (i, item) {
        if (item.ObjectID == object.ObjectID) {

            item.PositionX = object.left - item.MaxWidth / 2;
            item.PositionY = object.top - item.MaxHeight / 2;

            if (object.type == "text") {
                item.ContentString = object.text;
            }
            item.RotationAngle = object.getAngle();
            if (object.type != "text") {  // all other objects like image, vectors and svg
                item.MaxWidth = object.width * object.scaleX;
                item.MaxHeight = object.height * object.scaleY;


                if (object.type == "ellipse") {

                    item.CircleRadiusX = object.getRadiusX();
                    item.CircleRadiusY = object.getRadiusY();
                    //alert(item.CircleRadiusX);
                    item.PositionX = object.left - object.width / 2;
                    item.PositionY = object.top - object.height / 2;
                }
            }

            else { //object is text
                item.MaxWidth = object.maxWidth;
                item.MaxHeight = object.maxHeight;
                item.LineSpacing = object.lineHeight;
            }

            if (object.textAlign == "left")
                item.Allignment = 1;
            else if (object.textAlign == "center")
                item.Allignment = 2;
            else if (object.textAlign == "right")
                item.Allignment = 3;

            if (object.fontFamily != undefined)
                item.FontName = object.fontFamily;
            else
                item.FontName = "";

            if (object.fontSize != undefined)
                item.FontSize = object.fontSize;
            else
                item.FontSize = 0;

            if (object.fontWeight == "bold")
                item.IsBold = true;
            else
                item.IsBold = false;

            if (object.fontStyle == "italic")
                item.IsItalic = true;
            else
                item.IsItalic = false;

            if (object.type != "image") {
                item.ColorHex = object.fill;

            }

            if (object.type == "path") {
                item.ExField1 = object.strokeWidth;
            }


            item.Opacity = object.opacity;

            item.ColorC = object.C;
            item.ColorM = object.M;
            item.ColorY = object.Y;
            item.ColorK = object.K;

            item.IsPositionLocked = object.IsPositionLocked;
            item.IsHidden = object.IsHidden;
            item.IsEditable = object.IsEditable;





        }

    });
    
}
// functio to reseet object settings to orignal ones zoom = 1x
function ResetZoom(caller)
{

        canvas.setHeight(canvas.getHeight() * (1 / canvasScale));
        canvas.setWidth(canvas.getWidth() * (1 / canvasScale));

        var objects = canvas.getObjects();
        $.each(objects, function (i, item) {
            var scaleX = item.scaleX;
            var scaleY = item.scaleY;
            var left = item.left;
            var top = item.top;

            var tempScaleX = scaleX * (1 / canvasScale);
            var tempScaleY = scaleY * (1 / canvasScale);
            var tempLeft = left * (1 / canvasScale);
            var tempTop = top * (1 / canvasScale);

            item.scaleX = tempScaleX;
            item.scaleY = tempScaleY;
            item.left = tempLeft;
            item.top = tempTop;

            item.setCoords();
            if (caller != "pageChange") {
                updateTemplateObjsZoom(item);
            }
            
        });

        
        //canvas.renderAll();
    
        canvasScale = 1;
        canvasScale = 1;

        canvas.setHeight(canvas.getHeight() * (1 / canvasScale));
        canvas.setWidth(canvas.getWidth() * (1 / canvasScale));

        var objects = canvas.getObjects();
        $.each(objects, function (i, item) {
            var scaleX = item.scaleX;
            var scaleY = item.scaleY;
            var left = item.left;
            var top = item.top;

            var tempScaleX = scaleX * (1 / canvasScale);
            var tempScaleY = scaleY * (1 / canvasScale);
            var tempLeft = left * (1 / canvasScale);
            var tempTop = top * (1 / canvasScale);

            item.scaleX = tempScaleX;
            item.scaleY = tempScaleY;
            item.left = tempLeft;
            item.top = tempTop;

            item.setCoords();
            if (caller != "pageChange") {

                updateTemplateObjsZoom(item);
            } else {
                updateTemplateObjsZoomPageChange(item);
            }
        });

        
        canvas.renderAll();
    
        canvasScale = 1;
        canvasScale = 1;
        currentZoomLevel = 0;
}

//copy paste objects logice
$("#BtnCopyObj").click(function (event) {
    var activeGroup = canvas.getActiveGroup();

    var activeObject = canvas.getActiveObject();
    copiedObject = [];
    // for group copy
    if (activeGroup) {
        var objectsInGroup = activeGroup.getObjects();
        $.each(objectsInGroup, function (j, Obj) {
            $.each(TemplateObjects, function (i, item) {
                if (item.ObjectID == Obj.ObjectID) {
                    copiedObject.push(item);
                    return false;
                }
            });
        });

    } else if (activeObject) {

        $.each(TemplateObjects, function (i, item) {
            if (item.ObjectID == activeObject.ObjectID) {
                copiedObject.push(item);
                return false;
            }
        });


    }

});
$("#BtnPasteObj").click(function (event) {
    if (copiedObject.length != 0) {
        for (var i = 0; i < copiedObject.length; i++) {
            var target = fabric.util.object.clone(copiedObject[i]);
            target.ObjectID = --NewControlID;
            target.ProductPageId = SelectedPageID;
            target.$id = (parseInt(TemplateObjects[TemplateObjects.length-1].$id) + 4);
            // for showing the copied object below the orignal object
            target.PositionX -= 15;
            target.PositionY += 15;
            if (target.EntityKey != null) {
                delete target.EntityKey;
            }


            TemplateObjects.push(target);
            if (target.ObjectType == 2) {
                AddTextObject(canvas, target);
            }
            else if (target.ObjectType == 3) {
                AddImageObject(canvas, target);
            }
            else if (target.ObjectType == 6) {
                AddRectangleObject(canvas, target);
            }
            else if (target.ObjectType == 7) {
                AddCircleObject(canvas, target);
            }
            else if (target.ObjectType == 9) {
                AddPathObject(canvas, target);
            }
            canvas.renderAll();
        }
    }
});
// function to open tooltip
function ToolTipHandler(tipType) {
    var tipCookie=getCookie("IsTipEnabled");
    if (tipCookie!=null && tipCookie!="")
    {
        IsTipEnabled = tipCookie;
    }  

    //alert(tipCookie + IsTipEnabled);
    if (IsTipEnabled == "1") {
        var tip = new Array();
        tip[0] = "Get started… Double Click on ANY object to modify its properties.Or Select an object and move it by dragging on nudging with the scroll keys.";
        tip[1] = "Scaling Up degrades the quality of the original image.Crop large images and scale down for better results.";
        tip[2] = "Draw Squares and Circles by holding down the SHIFT key when resizing a rectangle or ellipse.";
        tip[3] = "Upload an individual image, logo or complete design and DRAG it onto the canvas area.For best results Upload images at 300 dpi, 100% in size and CMYK.";
        tip[4] = "Upload images in PNG format with transparent background if you want to overlay one image on top of another and then DRAG onto the canvas area.";
        tip[5] = "Add opacity to your image or logo by using the opacity control.";
        tip[6] = "To Add a New Image, click on the Second Icon in the Tool bar above and DRAG the picture onto the canvas area.Simply type in the text and then format the text using the text property panel.";
        tip[7] = "Quick Text allows you to quickly and accurately merge your credentials on this and other templates.You can DRAG a Quick Text field from Add Text>> Quick Text  or Edit your saved Text anytime from the tool bar “ Quick Text”";
        tip[8] = "Double Click on a Text or Image object to Edit and View it’s property box.";
        tip[9] = "To Add a New text field, click on the First Icon in the Tool bar above.Simply type in the text and then format the text using the text property panel.";
        tip[10] = "Hold down the SHIFT key to select multiple objects.You can move Object(s) with the Scroll keys ";
        tip[11] = "Hold down the SHIFT key with scroll keys  to nudge objects into position.Otherwise objects will Snap to grid, every 5 pixels.";
        tip[12] = "Coming soon – eBook Coming soon – Variable data merging from your csv files.";
        tip[13] = "Delete Objects with the DEL key.Undo & Redo using the ARROW icons in the top right,";
        tip[14] = "Retain Objects aspect by holding down the SHIFT key.Rotate, Stretch or Scale images by clicking on object nodes.";
        tip[15] = "Click on a object to change its ordering priority in the property panel.Bring to front, Send to back move Up or Down a layer";
        tip[16] = "Give your template a name and SAVE it for future editing or ordering. ";
        tip[17] = "Preview this product in the Page Turner view to check the correct page ordering.";
        tip[18] = "Aligning Objects ! You can selected multiple object by first selecting more than one object (by holding down the shift key) and then clicking on the Align Icons in the tool bar above.";
        tip[19] = "Flip between pages and sides by clicking on the Page Icons below the Tool bar";
        tip[20] = "Guides show any fold lines and Safe cutting margins.  Unless you deliberately want text or images to bleed to the edge after trimmed, then do not place text or images out side the Cyan lines (i.e. inside the Safe zone).";
        tip[21] = "Check ALL sides ?Check all spellings, details and the quality of images uploaded on all sides. ";


        // first tip 
        if (tipType == 1) {
            $("#DivTootTipTitle").text("TIP");
            $("#DivToolTipText").text(tip[0]);
            $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');

        } else if (tipType == 2) {
            // image tip
            if (BoolDisplayFirstTip == 1) {
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[0]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
                BoolDisplayFirstTip = 0;
            } else {

                var rand = Math.floor((Math.random() * 6) + 1);
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[rand]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
            }

        } else if (tipType == 3) {
            // 
            if (BoolDisplayFirstTip == 1) {
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[0]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
                BoolDisplayFirstTip = 0;
            } else {

                var rand = Math.floor((Math.random() * 9) + 7);
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[rand]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
            }

        } else if (tipType == 4) {
            //
            if (BoolDisplayFirstTip == 1) {
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[0]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
                BoolDisplayFirstTip = 0;
            } else {

                $("#DivTootTipTitle").text("TIP");
                var rand = Math.floor((Math.random() * 20) + 10);
                $("#DivToolTipText").text(tip[rand]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
            }
        } else if (tipType == 5) {
            // saving tip 
            $("#DivTootTipTitle").text("TIP");
            $("#DivToolTipText").text(tip[21]);
            $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
        } else if (tipType == 6) {
            // radom tip 
            $("#DivTootTipTitle").text("TIP");
            var rand = Math.floor((Math.random() * 21) + 0);
            $("#DivToolTipText").text(tip[rand]);
            $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "ToolTipHandler(6);">Next tip >></div>');
        }


        animatedcollapse.show('DivToolTip');
    }
}

// change colr handler 
function ChangeColorHandler(c,m,y,k,ColorHex) {

    var activeObject = canvas.getActiveObject();
    if (activeObject) {
        if (activeObject.type == 'text') {
            activeObject.setColor( ColorHex);
            activeObject.C = c;
            activeObject.M = m;
            activeObject.Y = y;
            activeObject.K = k;
        } else if (activeObject.type == 'ellipse' || activeObject.type == 'rect') {
            activeObject.set('fill',ColorHex);
            activeObject.C = c;
            activeObject.M = m;
            activeObject.Y = y;
            activeObject.K = k;
        } else if (activeObject.type == 'path') {
            activeObject.set('stroke', ColorHex);
            activeObject.C = c;
            activeObject.M = m;
            activeObject.Y = y;
            activeObject.K = k;
        }
        //activeObject.saveState();
        onObjModified(activeObject);
        canvas.renderAll();
        //  $('#BtnChngeClr').click();
    }

}

function AddImageObjectToCanvas(src, x, y, imWidth, imHeight) {
    //AddImageObjectNew(canvas, src, x, y, 0, imWidth, imHeight, 0);
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);

    var NewImageObject = {};
    NewImageObject = fabric.util.object.clone(TemplateObjects[0]);
    NewImageObject.ObjectID = --NewControlID;
    NewImageObject.ColorHex = "#000000";
    NewImageObject.IsBold = false;
    NewImageObject.IsItalic = false;
    NewImageObject.ProductPageId = SelectedPageID;
    NewImageObject.MaxWidth = 100;
    NewImageObject.$id = (parseInt(TemplateObjects[TemplateObjects.length-1].$id) + 4);
    NewImageObject.PositionX = x;
    NewImageObject.PositionY = y;
    NewImageObject.ObjectType = 3;

//    // to check height and width if greater than canvas 
//    if (imHeight < canvasHeight) {
//        NewImageObject.MaxHeight = imHeight;
//        NewImageObject.Height = imHeight;
//    } else {
//        NewImageObject.MaxHeight = canvasHeight-40;
//        NewImageObject.Height = canvasHeight-40;
//        NewImageObject.PositionY = 20;
//    }
//    if (imWidth < canvasWidth) {
//        NewImageObject.MaxWidth = imWidth;
//        NewImageObject.Width = imWidth;
//    } else {
//        // avoiding margins
//        NewImageObject.MaxWidth = canvasWidth-40;
//        NewImageObject.Width = canvasWidth-40;
//        NewImageObject.PositionX = 20;
//    }
    NewImageObject.MaxHeight = imHeight;
    NewImageObject.Height = imHeight;
    NewImageObject.MaxWidth = imWidth;
    NewImageObject.Width = imWidth;

    if (imHeight == 0) {
        NewImageObject.MaxHeight = 50;
        NewImageObject.Height = 50;
    }
    else if (imWidth == 0) {
        NewImageObject.MaxWidth = 50;
        NewImageObject.Width = 50;
    }
    NewImageObject.ContentString = src;
    NewImageObject.DisplayOrder = TemplateObjects.length + 1;
    AddImageObject(canvas, NewImageObject);

    

        // getting object index
    var objects = canvas.getObjects();

    NewImageObject.DisplayOrderPdf = objects.length;
    canvas.renderAll();
   // canvas.centerObject(imObj);
    TemplateObjects.push(NewImageObject);
    animatedcollapse.toggle('addImage');
}
function AddQuickText(DivID, left, top) {
   
    var NewTextObejct = {};
    NewTextObejct = fabric.util.object.clone(TemplateObjects[0]);
    if (DivID == "QuickTxtName") {
        NewTextObejct.Name = "Name";
        NewTextObejct.ContentString = (QuickTextData.Name == null ? "Your Name" : QuickTextData.Name);
    }
    if (DivID == "QuickTxtTitle") {
        NewTextObejct.Name = "Title";
        NewTextObejct.ContentString = QuickTextData.Title == null ? "Your Title" : QuickTextData.Title;
    }
    if (DivID == "QuickTxtCompanyName") {
        NewTextObejct.Name = "CompanyName";
        NewTextObejct.ContentString = (QuickTextData.Company == null ? "Your Company Name" : QuickTextData.Company);
    }
    if (DivID == "QuickTxtCompanyMsg") {
        NewTextObejct.Name = "CompanyMessage";
        NewTextObejct.ContentString = QuickTextData.CompanyMessage == null ? "Your Company Message" : QuickTextData.CompanyMessage;
    }
    if (DivID == "QuickTxtAddress1") {
        NewTextObejct.Name = "AddressLine1";
        NewTextObejct.ContentString = QuickTextData.Address1 == null ? "Address Line 1" : QuickTextData.Address1;
    }
    if (DivID == "QuickTxtTel") {
        NewTextObejct.Name = "Phone";
        NewTextObejct.ContentString = QuickTextData.Telephone == null ? "Telephone / Other" : QuickTextData.Telephone;
    }
    if (DivID == "QuickTxtFax") {
        NewTextObejct.Name = "Fax";
        NewTextObejct.ContentString = QuickTextData.Fax == null ? "Fax / Other" : QuickTextData.Fax;
    }
    if (DivID == "QuickTxtEmail") {
        NewTextObejct.Name = "Email";
        NewTextObejct.ContentString = QuickTextData.Email == null ? "Email address / Other" : QuickTextData.Email;
    }
    if (DivID == "QuickTxtWebsite") {
        NewTextObejct.Name = "Website";
        NewTextObejct.ContentString = QuickTextData.Website == null ? "Website address" : QuickTextData.Website;
    }

    NewTextObejct.ObjectID = --NewControlID;
    NewTextObejct.ColorHex = "#000000";
    NewTextObejct.ColorC = 0;
    NewTextObejct.ColorM = 0;
    NewTextObejct.ColorY = 0;
    NewTextObejct.ColorK = 100;
    NewTextObejct.IsBold = false;
    NewTextObejct.IsItalic = false;
    NewTextObejct.ProductPageId = SelectedPageID;
    NewTextObejct.MaxWidth = 100;
    NewTextObejct.MaxHeight = 30;
    NewTextObejct.FontSize = 14;
    NewTextObejct.$id =(parseInt(TemplateObjects[TemplateObjects.length-1].$id) + 4);
    TemplateObjects.push(NewTextObejct);

    var uiTextObject = AddTextObject(canvas, NewTextObejct);
    uiTextObject.left = left;
    uiTextObject.top = top;
    NewTextObejct.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    NewTextObejct.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    canvas.renderAll();

    //animatedcollapse.toggle('quickText');
    //animatedcollapse.toggle('addText');
}

/// function to handle on change event for multiple objects
function groupChange(activeGroup) {
    activeGroup.forEach(function (object) {  			
        onObjModified(object);
    });
}
// function to update color picker
function sliderUpdate() {
    var c = $("#DivColorC").slider("value");
    var m = $("#DivColorM").slider("value");
    var y = $("#DivColorY").slider("value");
    var k = $("#DivColorK").slider("value");
    var hex = getColorHex(c,m,y,k);
    //colorPicker.setColor("#" + hex);
    //$("#swatch").css("background-color",  hex);
    UpdateColorPallet(c,m,y,k);
}
    // function to add color pallets
function UpdateColorPallet(c, m,y,k) {
    //var path = "Designer/PrivateFonts/FontFace/"

    var Color = getColorHex(c, m, y, k);
    //alert(Color);
    var html = "<label for='ColorPalle' id ='LblCollarPalet'>Click on the pallet to apply Color</label><div class ='ColorPallet' style='background-color:" + Color + "' onclick='ChangeColorHandler(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);UpdateRecentColorDiv(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
    $('#LblDivColorC').html(c + "%");
    $('#LblDivColorM').html(m + "%");
    $('#LblDivColorY').html(y + "%");
    $('#LblDivColorK').html(k + "%");
    $('#ColorPickerPalletContainer').html(html);
}
function UpdateRecentColorDiv(c, m, y, k, Color) {
    var size = $("#DivRecentColors > div").size();
    
    var html = "<div class ='ColorPallet' style='background-color:" + Color + "' onclick='ChangeColorHandler(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
    if (size < 8) {

        $('#DivRecentColors').append(html);
    } else {

        var list = document.getElementById('DivRecentColors');
        list.removeChild(list.childNodes[0]);
        $('#DivRecentColors').append(html);
      
    }
    
}
function togglePanels(PanelToShow) {
    switch (PanelToShow) {  //addTxtJqueryButton
        case "addText": 
            {
                animatedcollapse.show('addText');
                animatedcollapse.hide('UploadImage');
                animatedcollapse.show('quickText');
                break;
            }
        case "addImage": 
            {
                animatedcollapse.toggle('addImage');
                animatedcollapse.hide('UploadImage');
                animatedcollapse.hide('quickText');
                animatedcollapse.hide('addText');
                break;
            }
        case "quickTextFormPanel":
            {
                animatedcollapse.toggle('quickTextFormPanel');
                animatedcollapse.hide('addImage');
                animatedcollapse.hide('UploadImage');
                animatedcollapse.hide('quickText');
                animatedcollapse.hide('addText');

                break;
            }
    }
}


// funtion to update image coOrdinates 
function UpdateCropImgCoords(coords) {
   // var rx = 100 / coords.w;
   // var ry = 100 / coords.h;
    //CropedWidth = Math.round(rx * 500) + 'px';
    //CropedHeight = Math.round(ry * 370) + 'px';
    //CropedMarginLeft = '-' + Math.round(rx * coords.x) + 'px';
    //CropedMarginTop = '-' + Math.round(ry * coords.y) + 'px';
    CropedWidth = Math.round(coords.w);
    CropedHeight = Math.round(coords.h);
    CropedMarginLeft = Math.round(coords.x);
    CropedMarginTop = Math.round(coords.y);
    var activeObj = canvas.getActiveObject();
    CropedObjID = activeObj.ObjectID;
    
}

(function (global) {

    "use strict";

    function pad(str, length) {
        while (str.length < length) {
            str = '0' + str;
        }
        return str;
    };

    var getRandomInt = fabric.util.getRandomInt;
    function getRandomColor() {
        return (
            pad(getRandomInt(0, 255).toString(16), 2) +
            pad(getRandomInt(0, 255).toString(16), 2) +
            pad(getRandomInt(0, 255).toString(16), 2)
        );
    }

    function getRandomNum(min, max) {
        return Math.random() * (max - min) + min;
    }

    var canvas = global.canvas = new fabric.Canvas('canvas', {
        /*
        clipTo: function(canvas) {
        canvas.arc(this.width / 2, this.height / 2, 200, 0, Math.PI * 2, true);
        }
        */
    });


    //	jskataUndo.onChange = function () {
    //		if (jsk.undo.canUndo()) $("#BtnUndo").attr("disabled", false)
    //		else $("#BtnUndo").attr("disabled", true);
    //		if (jsk.undo.canRedo()) $("#BtnRedo").attr("disabled", false)
    //		else $("#BtnRedo").attr("disabled", true);
    //	};


    // function to toggle the guides 

    function toggleGuides() {
        if (IsShowGuides) {
            IsShowGuides = false;
            $("#GuidesSpan").html("Show Guides");
        } else {
            IsShowGuides = true;
            $("#GuidesSpan").html(" Hide  Guides");
        }
        PageChange(SelectedPageID);

    }
    $("#BtnGuides").click(function (event) {
        toggleGuides();
    });
    // btn for croping an image 
    $("#BtnCropImg").click(function (event) {
        var activeObject = canvas.getActiveObject();
        var src
        if (activeObject && activeObject.type === 'image') {
            src = activeObject.getSrc();
            $("#CropImgContainer").html('<img id="CropTarget" src="' + src + "?r=" + fabric.util.getRandomInt(1, 100) + '" style="visibility:hidden;" />');
            $(function () {
                $('#CropTarget').Jcrop(
                {
                    //            minSize:[100,100],
                    //            maxSize: [150,150]
                    boxWidth: 210,
                    boxHeight: 250,
                    onChange: UpdateCropImgCoords,
                    onSelect: UpdateCropImgCoords
                });
            });
            animatedcollapse.toggle('DivCropToolContainer');
        }


    });
    // for scale in and scale out 
    $("#BtnImgScaleIN").click(function (event) {
        var activeObject = canvas.getActiveObject();
        // to scale an object by 5 percent 
        var newHeight = activeObject.getHeight() + (activeObject.getHeight() * 0.05);
        var newWidth = activeObject.getWidth() + (activeObject.getWidth() * 0.05);

        activeObject.scaleToHeight(newHeight);
        activeObject.scaleToWidth(newWidth);
        //        activeObject.set("maxWidth",newWidth);
        //        activeObject.set("maxHeight",newHeight);
        //        activeObject.set("width",newWidth);
        //        activeObject.set("height",newHeight);
        //alert(activeObject.get("MaxHeight"));
        onObjModified(activeObject);
        canvas.renderAll();
    });

    $("#BtnImgScaleOut").click(function (event) {
        var activeObject = canvas.getActiveObject();
        // to scale an object out  by 5 percent 
        var newHeight = activeObject.getHeight() - (activeObject.getHeight() * 0.05);
        var newWidth = activeObject.getWidth() - (activeObject.getWidth() * 0.05);

        activeObject.scaleToHeight(newHeight);
        activeObject.scaleToWidth(newWidth);
        onObjModified(activeObject);
        canvas.renderAll();
    });

    // btn to update image croping 
    $("#BtnApplyCropImg").click(function (event) {
        var activeObject = canvas.getActiveObject();
        var contentString;
        var DisplayOrderObj;
        if (activeObject.ObjectID == CropedObjID) {
            // getting imagae src 
            $.each(TemplateObjects, function (i, item) {
                if (item.ObjectID == activeObject.ObjectID) {
                    contentString = item.ContentString;
                    DisplayOrderObj = item.DisplayOrderPdf;
                }
            });

            // to replace all the three / in connection string with 3 _ each 
            var n = contentString.replace("/", "___");
            n = n.replace("/", "___");
            n = n.replace("/", "___");

            $.getJSON("services/imageSvc/CropImg/" + n + "," + CropedMarginLeft + "," + CropedMarginTop + "," + CropedWidth + "," + CropedHeight + "," + TemplateID,
            function (data) {
                $.each(TemplateObjects, function (i, item) {
                    if (item.ContentString == contentString) {
                        item.ContentString = data;
                        //DisplayOrderObj = item.DisplayOrderPdf;
                    }
                });

                // alert(data);
                //				if(data == true)
                //				{
                //                    
                //					var src = activeObject.getSrc() + "?r=" + fabric.util.getRandomInt(1, 100);
                //					//alert(DisplayOrderObj);
                //					var left = activeObject.get("left");
                //					var top = activeObject.get("top");
                //					var rotationAngle = activeObject.getAngle();
                //					var width = activeObject.get("width");
                //					var height = activeObject.get("height");
                //					var maxWidth = activeObject.get("maxWidth");
                //					var maxHeight = activeObject.get("maxHeight");
                //					var ObjectID = activeObject.ObjectID;
                //					//onObjModified(activeObject);
                //					
                //					// remove old image
                //					canvas.remove(activeObject); 
                //					
                //					 
                //					// add new cropped image
                //					fabric.Image.fromURL(src + "?r=" + fabric.util.getRandomInt(1, 100), function (imgObject) {
                //						imgObject.set({
                //							left: left,
                //							top: top,
                //							angle: rotationAngle
                //						});

                //						imgObject.width = CropedWidth;
                //						imgObject.height = CropedHeight;

                //						imgObject.maxWidth = CropedWidth;
                //						imgObject.maxHeight = CropedHeight;
                //						imgObject.ObjectID = ObjectID;
                //						//imgObject.scaleToWidth(width);

                //						imgObject.setAngle(rotationAngle);

                //						imgObject.set({
                //							borderColor: 'red',
                //							cornerColor: 'orange',
                //							cornersize: 8
                //						});

                //						canvas.insertAt(imgObject, DisplayOrderObj);
                //					});

                //					animatedcollapse.toggle('DivCropToolContainer');
                //					// reload all the page so that same images on that page should be refreshed
                //					//PageChange(SelectedPageID);
                //					canvas.renderAll();
                //						
                //				}
                animatedcollapse.toggle('DivCropToolContainer');
                //					// reload all the page so that same images on that page should be refreshed
                PageChange(SelectedPageID);
                canvas.renderAll();

            });
        }

    });
    
    //controls Initialize
    $("#BtnZoomIn").click(function (event) {
        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();
        if (activeGroup) {
            canvas.discardActiveGroup();
        } else if (activeObject) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        currentZoomLevel += 1;
        ZoomIn();
        canvas.renderAll();
//            // updating the undo and redo objects 
//            $.each(commandStack, function (i, item) { // way to access command stack elements array 
//                //commandStack[i].redo.p[0].ObjectID;
//                //commandStack[i].undo.p[0].ObjectID;
//                // for redo object 
//                var left = commandStack[i].redo.p[0].PositionX + commandStack[i].redo.p[0].MaxWidth / 2;
//                var top =  commandStack[i].redo.p[0].PositionY + commandStack[i].redo.p[0].MaxHeight / 2;


//            });
            
    });
    $("#BtnOrignalZoom").click(function (event) {
        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();
        if (activeGroup) {
            canvas.discardActiveGroup();
        } else if (activeObject) {
            canvas.discardActiveObject();
        }
        currentZoomLevel = 0;
        ResetZoom();
        canvas.renderAll();

    });



    $('#BtnZoomOut').click(function (event) {
        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();
        if (activeGroup) {
            canvas.discardActiveGroup();
        } else if (activeObject) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        currentZoomLevel-=1;
        zoomOut();
        canvas.renderAll();
    });

    $('#BtnUploadFont').click(function (event) {
        animatedcollapse.toggle("DivUploadFont");
        if (getCookie("IsTipEnabled") == "1") {
            $("#ShowTips").click();
        }
    });

    $('#BtnAdvanceColorPicker').click(function (event) {
        animatedcollapse.toggle("DivAdvanceColorPanel");
    });

    $('#BtnChngeClr').click(function (event) {
        animatedcollapse.toggle("DivColorPallet");
    });

    $('#AddColorShape').click(function (event) {
        animatedcollapse.toggle("DivColorPallet");
    });

    //top panel/tool bar event handlers
    // function that is called on undo 

    // for undo and redo buttons enabling disbaling
    function updateUIRedoUndo() {
        var btnUndo = document.getElementById('BtnUndo');
        var btnRedo = document.getElementById('BtnRedo');
        btnUndo.disabled = !undoManager.hasUndo();
        btnRedo.disabled = !undoManager.hasRedo();
    }
    $('#BtnUndo').click(function (event) {
        canvas.discardActiveGroup();
        canvas.renderAll();
        undoManager.undo();

        //        $.each(commandStack, function (i, item) { // way to access command stack elements array 
        //            alert(commandStack[i].redo.p[0].ObjectID);
        //            alert(commandStack[i].undo.p[0].ObjectID);
        //        });


    });
    // function that is called on redo 
    $('#BtnRedo').click(function (event) {
        canvas.discardActiveGroup();
        canvas.renderAll();
        undoManager.redo();

    });

    $('#BtnQuickTextForm').click(function (event) {

        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();
        if (activeGroup) {
            canvas.discardActiveGroup();
        } else if (activeObject) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        animatedcollapse.hide('DivAdvanceColorPanel');
        togglePanels("quickTextFormPanel");
    });

    $('#BtnQuickTextSave').click(function (event) {
        SaveQuickText();
        togglePanels("quickTextFormPanel");
    });


    $('#btnNewTxtPanel').click(function (event) {
        animatedcollapse.hide('DivAdvanceColorPanel');
        togglePanels('addText');
        togglePanels('addText');
        $("#txtAddNewText").focus();
    });


    $('#btnImgPanel').click(function (event) {
        animatedcollapse.hide('DivAdvanceColorPanel');
        togglePanels('addImage');
    });

    //add rectangle button event handler
    $('#btnAddRectangle').click(function (event) {
        var NewTextObejct = {};
        NewTextObejct = fabric.util.object.clone(TemplateObjects[0]);
        NewTextObejct.Name = "rectangle";
        NewTextObejct.ContentString = "rectangle";
        NewTextObejct.ObjectID = --NewControlID;
        NewTextObejct.ColorHex = "#000000";
        NewTextObejct.ColorC = 0;
        NewTextObejct.ColorM = 0;
        NewTextObejct.ColorY = 0;
        NewTextObejct.ColorK = 100;
        NewTextObejct.IsBold = false;
        NewTextObejct.IsItalic = false;
        NewTextObejct.ObjectType = 6; //rectangle
        NewTextObejct.ProductPageId = SelectedPageID;
        NewTextObejct.MaxWidth = 200;
        NewTextObejct.MaxHeight = 200;
        NewTextObejct.$id = (parseInt(TemplateObjects[TemplateObjects.length - 1].$id) + 4);

        var rectObject = new fabric.Rect({
            left: 0,
            top: 0,
            fill: '#000000',
            width: 100,
            height: 100,
            opacity: 1
        })

        rectObject.maxWidth = 200;
        rectObject.maxHeight = 200;
        rectObject.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });

        rectObject.ObjectID = NewTextObejct.ObjectID;
        canvas.add(rectObject);

        canvas.centerObject(rectObject);
        // getting object index
        var index; 
        var objects = canvas.getObjects();
        $.each(objects, function (i, item) {
            if(item.ObjectID == rectObject.ObjectID)
            {
                index = i;
            }
        });
        NewTextObejct.DisplayOrderPdf = index;

        rectObject.top = 130;
        NewTextObejct.PositionX = rectObject.left - rectObject.maxWidth / 2;
        NewTextObejct.PositionY = rectObject.top - rectObject.maxHeight / 2;
        rectObject.setCoords();

        rectObject.C = "0";
        rectObject.M = "0";
        rectObject.Y = "0";
        rectObject.K = "100";
        canvas.renderAll();
        TemplateObjects.push(NewTextObejct);
        ToolTipHandler(6);

    });

    //add circle / ellipse button event handler
    $('#btnAddCircle').click(function (event) {
        var NewCircleObejct = {};
        NewCircleObejct = fabric.util.object.clone(TemplateObjects[0]);
        NewCircleObejct.Name = "Ellipse";
        NewCircleObejct.ContentString = "Ellipse";
        NewCircleObejct.ObjectID = --NewControlID;
        NewCircleObejct.ColorHex = "#000000";
        NewCircleObejct.ColorC = 0;
        NewCircleObejct.ColorM = 0;
        NewCircleObejct.ColorY = 0;
        NewCircleObejct.ColorK = 100;

        NewCircleObejct.IsItalic = false;
        NewCircleObejct.ObjectType = 7; //ellipse/circle
        NewCircleObejct.ProductPageId = SelectedPageID;
        NewCircleObejct.MaxWidth = 100;
        NewCircleObejct.$id = (parseInt(TemplateObjects[TemplateObjects.length - 1].$id) + 4);
        NewCircleObejct.CircleRadiusX = 50;
        NewCircleObejct.CircleRadiusY = 50;
        NewCircleObejct.Opacity = 1;

        var circleObject = new fabric.Ellipse({
            left: 0,
            top: 0,
            fill: '#000000',
            rx: 50,
            ry: 50,
            opacity: 1
        })

        circleObject.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });

        circleObject.ObjectID = NewCircleObejct.ObjectID;
        canvas.add(circleObject);
        canvas.centerObject(circleObject);
        circleObject.top = 130;

         // getting object index
        var index; 
        var objects = canvas.getObjects();
        $.each(objects, function (i, item) {
            if(item.ObjectID == circleObject.ObjectID)
            {
                index = i;
            }
        });
        NewCircleObejct.DisplayOrderPdf = index;
        
        NewCircleObejct.PositionX = circleObject.left - circleObject.width / 2;
        NewCircleObejct.PositionY = circleObject.top - circleObject.width / 2;
        circleObject.setCoords();
        circleObject.C = "0";
        circleObject.M = "0";
        circleObject.Y = "0";
        circleObject.K = "100";
        canvas.renderAll();
        TemplateObjects.push(NewCircleObejct);
        //circleObject.setFill("black");
        //onObjModified(circleObject,undefined,"abc");


        ToolTipHandler(6);
    });




    //object hover effects
    canvas.findTarget = (function (originalFn) {
        return function () {
            var target = originalFn.apply(this, arguments);
            if (target) {
                if (this._hoveredTarget !== target) {
                    canvas.fire('object:over', { target: target });
                    if (this._hoveredTarget) {
                        canvas.fire('object:out', { target: this._hoveredTarget });
                    }
                    this._hoveredTarget = target;
                }
            }
            else if (this._hoveredTarget) {
                canvas.fire('object:out', { target: this._hoveredTarget });
                this._hoveredTarget = null;
            } return target;
        };
    })(canvas.findTarget);

    // event to close panels when clicked outside the canvas 
    $("body").click(function (event) {
        //alert(event.target.id);
        if (event.target.id == "") {
            //  animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        } else if (event.target.id == "btnNewTxtPanel") {
            // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        } else if (event.target.id == "btnImgPanel") {
            // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        } else if (event.target.id == "bd-wrapper" || event.target.id == "designer" || event.target.id == "CanvasContainer") {
            //canvas.selectionClear();
            var activeObject = canvas.getActiveObject();
            var activeGroup = canvas.getActiveGroup();
            if (activeGroup) {
                canvas.discardActiveGroup();
            } else if (activeObject) {
                canvas.discardActiveObject();
            }
            canvas.renderAll();
            animatedcollapse.hide(['textPropertPanel', 'DivAdvanceColorPanel', 'DivColorPallet', 'ShapePropertyPanel', 'ImagePropertyPanel', 'UploadImage', 'quickText', 'addImage', 'addText', 'DivToolTip', 'DivAlignObjs']);
        }

    });
    var IsInputSelected = false;
    // check if an input is focused or not 
    $('input, textarea, select').focus(function() {
        IsInputSelected = true;
    }).blur(function(){
        IsInputSelected = false;
    });
    //copy paste logic

    $('body').keydown(function (e) {

        if (e.keyCode == ctrlKey) ctrlDown = true;
        if (e.keyCode == shiftKey) shftlDown = true;

        // disabling browser scroll on arrow keys 
        if (e.keyCode >= 37 && e.keyCode <= 40 && IsInputSelected ==  false) {
            return false
            
        }

        // if backspace is presseid 

        if (e.keyCode == 8 && IsInputSelected == false) {
            if (IsDesignModified) {
                if (!confirm("You have unsaved changes. Do you want to leave without saving changes ?")) {
                    return false;
                }
            }
        }

        if (shftlDown && (e.keyCode == shiftKey)) {
            var activeGroup = canvas.getActiveGroup();
            if (activeGroup) {
                //alert();
                var lockedObjectFound = false;
                var objectsInGroup = activeGroup.getObjects();
                $.each(objectsInGroup, function (j, Obj) {
                    if(Obj.IsPositionLocked == true)
                    {
                        lockedObjectFound = true;   
                    }
                });
                if(!lockedObjectFound)
                {
                    setPanelPosition("DivAlignObjs",lastPanel);
                    lastPanel = "DivAlignObjs";
                    animatedcollapse.show("DivAlignObjs");
                } else 
                {
                    alert("The Group contains a locked object.");
                    canvas.discardActiveGroup();
                    canvas.renderAll();
                }
            }
        }
        //copy
        if (ctrlDown && (e.keyCode == cKey)) {
            var activeGroup = canvas.getActiveGroup();
            var activeObject = canvas.getActiveObject();
            copiedObject = [];
            // for group copy
            if (activeGroup) {
                var objectsInGroup = activeGroup.getObjects();
                $.each(objectsInGroup, function (j, Obj) {
                    $.each(TemplateObjects, function (i, item) {
                        if (item.ObjectID == Obj.ObjectID) {
                            copiedObject.push(item);
                            return false;
                        }
                    });
                });

            } else if (activeObject) {

                $.each(TemplateObjects, function (i, item) {
                    if (item.ObjectID == activeObject.ObjectID) {
                        copiedObject.push(item);
                        return false;
                    }
                });


            }
        }
        else if (ctrlDown && (e.keyCode == vKey)  && IsInputSelected ==  false) //paste
        {
            if (copiedObject.length != 0) {
                for (var i = 0; i < copiedObject.length; i++) {
                    var target = fabric.util.object.clone(copiedObject[i]);
                    target.ObjectID = --NewControlID;
                    target.ProductPageId = SelectedPageID;
                    target.$id = (parseInt(TemplateObjects[TemplateObjects.length - 1].$id) + 4);
                    // for showing the copied object below the orignal object
                    target.PositionX -= 15;
                    target.PositionY += 15;
                    if (target.EntityKey != null) {
                        delete target.EntityKey;
                    }


                    TemplateObjects.push(target);
                    if (target.ObjectType == 2) {
                        AddTextObject(canvas, target);
                    }
                    else if (target.ObjectType == 3) {
                        AddImageObject(canvas, target);
                    }
                    else if (target.ObjectType == 6) {
                        AddRectangleObject(canvas, target);
                    }
                    else if (target.ObjectType == 7) {
                        AddCircleObject(canvas, target);
                    }
                    else if (target.ObjectType == 9) {
                        AddPathObject(canvas, target);
                    }
                    canvas.renderAll();
                }
            }
        }


    });


    // body event to track keyboard keypress 
    $('body').keyup(function (event) {

        if (event.keyCode == ctrlKey) ctrlDown = false;

        if (event.keyCode == shiftKey) shftlDown = false


        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();

        if (activeGroup) {

            // handle cursor keys  
            if (event.keyCode == 38) {
                // slide up      
                if (shftlDown)
                    activeGroup.top -= 1;
                else
                activeGroup.top -= 5;
                //	activeGroup.top -= 10;
            }
            else if (event.keyCode == 37) {
                // slide left      
                //activeGroup.left -= 10;
                if (shftlDown)
                    activeGroup.left -= 1;
                else
                    activeGroup.left -= 5;
            } else if (event.keyCode == 39) {
                // slide right      
                //activeGroup.left += 10;
                if (shftlDown)
                    activeGroup.left += 1;
                else
                    activeGroup.left += 5;
            }
            else if (event.keyCode == 40) {
                // slide bottom      
                //activeGroup.top += 10;
                if (shftlDown)
                    activeGroup.top += 1;
                else
                    activeGroup.top += 5;
            }
            canvas.renderAll();
            if (shftlDown == false) {
                if (event.keyCode == 38 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 40) {
                    var objectsInGroup = activeGroup.getObjects();
                    objectsInGroup.forEach(function (object) {
                        var clonedItem = fabric.util.object.clone(object);
                        clonedItem.left += activeGroup.left;
                        clonedItem.top += activeGroup.top;
                        onObjModified(clonedItem);
                        //alert(clonedItem.top);
                    });
                }
            }
        }
        //else if (activeObject  ) { //changed by saqib to check if object has position locked 
        else if (activeObject  && activeObject.IsPositionLocked == false) {
            // handle cursor keys  

            if (event.keyCode == 38) {
                // prevent browser scrool
                //event.preventDefault();

                // slide up   

                if (shftlDown)
                    activeObject.top -= 1;
                else
                    activeObject.top -= 5;

                onObjModified(activeObject);

                canvas.renderAll();
            }
            else if (event.keyCode == 37) {
                // slide left  
                if (shftlDown)
                    activeObject.left -= 1;
                else
                    activeObject.left -= 5;
                onObjModified(activeObject);

                canvas.renderAll();
            } else if (event.keyCode == 39) {
                // slide right   
                if (shftlDown)
                    activeObject.left += 1;
                else
                    activeObject.left += 5;

                onObjModified(activeObject);

                canvas.renderAll();
            }
            else if (event.keyCode == 40) {
                // slide bottom   
                if (shftlDown)
                    activeObject.top += 1;
                else
                    activeObject.top += 5;

                onObjModified(activeObject);

                canvas.renderAll();
            }



           
        }

        if (event.keyCode == 46 || event.keyCode == 8) {
            var activeObject = canvas.getActiveObject();
            var activeGroup = canvas.getActiveGroup();
            if(activeGroup) 
            {
                if (confirm("Are you sure you want to delete group")) {
                    var objectsInGroup = activeGroup.getObjects();
                    canvas.discardActiveGroup();
                    objectsInGroup.forEach(function (object) {
                        onObjModified(object, 'delete');
                        canvas.remove(object);
                    });
                    
                }
            } else 
            {  
                if (confirm("Are you sure you want to delete")) {
                    onObjModified(activeObject, 'delete');
                    canvas.remove(activeObject);
                }
            }
        }

    });
    // function to update image properties 
    function updateImgProperties(activeObject) {
        if(IsEmbedded && IsCalledFrom == 4)
        {
        			
        	$("#LockPositionImg").css("visibility", "hidden");
        	$("#BtnPrintImage").css("visibility", "hidden");
              //$("#BtnLockEditing").attr("disabled", "disabled");
            // hiding labels 
            $("#lblLockPositionImg").css("visibility", "hidden");
        	$("#lblDoNotPrintImg").css("visibility", "hidden");
        }
        $.each(TemplateObjects, function (i, item) {

            if (item.ObjectID == activeObject.ObjectID) {
                if (item.IsPositionLocked) {
                    $("#LockPositionImg").attr('checked', true);
                }
                else {
                    $("#LockPositionImg").attr('checked', false);
                }
                if (item.IsHidden) {
                    $("#BtnPrintImage").attr('checked', true);
                }
                else {
                    $("#BtnPrintImage").attr('checked', false);
                }
                // in embedded mode diable editing and lock position check boxeds
                if (IsEmbedded && item.IsPositionLocked) {

                    $("#LockPositionImg").attr("disabled", "disabled");
                    //$("#BtnLockEditing").attr("disabled", "disabled");
                }
                return false;
            }
        });


    }
    // function to update  multiple text properties 
    function updateTextProperties(activeObject) {
        $("#EditTXtArea").val(activeObject.text);
        $("#EditTXtArea").removeAttr("disabled");
        $("#BtnSearchTxt").removeAttr("disabled");
        $("#BtnUpdateText").removeAttr("disabled");
        $("#BtnSelectFonts").val(activeObject.get('fontFamily'));
        $("#BtnFontSize").val(Math.floor(activeObject.get('fontSize')));
        $("#BtnSelectFontSize").val(Math.floor(activeObject.get('fontSize')));
        $("#txtLineHeight").val(activeObject.get('lineHeight'));


        $.each(TemplateObjects, function (i, item) {

            if (item.ObjectID == activeObject.ObjectID) {
                if (item.IsPositionLocked) {
                    $("#BtnLockTxtPosition").attr('checked', true);
                }
                else {
                    $("#BtnLockTxtPosition").attr('checked', false);
                }
                if (item.IsHidden) {
                    $("#BtnPrintObj").attr('checked', true);
                }
                else {
                    $("#BtnPrintObj").attr('checked', false);
                }
                if (item.IsEditable) {
                    $("#BtnLockEditing").attr('checked', false);
                    $("#BtnJustifyTxt1").removeAttr("disabled");
                    $("#BtnJustifyTxt2").removeAttr("disabled");
                    $("#BtnJustifyTxt3").removeAttr("disabled");
                    $("#BtnTxtarrangeOrder1").removeAttr("disabled");
                    $("#BtnTxtarrangeOrder2").removeAttr("disabled");
                    $("#BtnTxtarrangeOrder3").removeAttr("disabled");
                    $("#BtnTxtarrangeOrder4").removeAttr("disabled");
                    $("#EditTXtArea").removeAttr("disabled");
                    $("#BtnSearchTxt").removeAttr("disabled");
                    $("#BtnUpdateText").removeAttr("disabled");
                    $("#BtnSelectFonts").removeAttr("disabled");
                    $("#BtnFontSize").removeAttr("disabled");
                    $("#BtnBoldTxt").removeAttr("disabled");
                    $("#BtnItalicTxt").removeAttr("disabled");
                    $("#txtLineHeight").removeAttr("disabled");
                    $("#BtnChngeClr").removeAttr("disabled");
                    $("#BtnDeleteTxtObj").removeAttr("disabled");
                    $("#BtnRotateTxtLft").removeAttr("disabled");
                    $("#BtnRotateTxtRight").removeAttr("disabled");
                    $("#BtnLockTxtPosition").removeAttr("disabled");
                    $("#BtnPrintObj").removeAttr("disabled");
                    // in embedded mode diable editing and lock position check boxeds
                    if (IsEmbedded && !item.IsEditable) {

                        //$("#BtnLockTxtPosition").attr("disabled", "disabled");
                        $("#BtnLockEditing").attr("disabled", "disabled");
                    }
                    // in embedded mode diable editing and lock position check boxeds
                    if (IsEmbedded && item.IsPositionLocked) {

                        $("#BtnLockTxtPosition").attr("disabled", "disabled");
                        //$("#BtnLockEditing").attr("disabled", "disabled");
                    }
                }
                else {
                    $("#BtnLockEditing").attr('checked', true);
                    $("#BtnJustifyTxt1").attr("disabled", "disabled");
                    $("#BtnJustifyTxt2").attr("disabled", "disabled");
                    $("#BtnJustifyTxt3").attr("disabled", "disabled");
                    $("#BtnTxtarrangeOrder1").attr("disabled", "disabled");
                    $("#BtnTxtarrangeOrder2").attr("disabled", "disabled");
                    $("#BtnTxtarrangeOrder3").attr("disabled", "disabled");
                    $("#BtnTxtarrangeOrder4").attr("disabled", "disabled");
                    $("#EditTXtArea").attr("disabled", "disabled");
                    $("#BtnSearchTxt").attr("disabled", "disabled");
                    $("#BtnUpdateText").attr("disabled", "disabled");
                    $("#BtnSelectFonts").attr("disabled", "disabled");
                    $("#BtnFontSize").attr("disabled", "disabled");
                    $("#BtnBoldTxt").attr("disabled", "disabled");
                    $("#BtnItalicTxt").attr("disabled", "disabled");
                    $("#txtLineHeight").attr("disabled", "disabled");
                    $("#BtnChngeClr").attr("disabled", "disabled");
                    $("#BtnDeleteTxtObj").attr("disabled", "disabled");
                    $("#BtnRotateTxtLft").attr("disabled", "disabled");
                    $("#BtnRotateTxtRight").attr("disabled", "disabled");
                    $("#BtnLockTxtPosition").attr("disabled", "disabled");
                    $("#BtnPrintObj").attr("disabled", "disabled");

                }
            }
        });

        //		// in embedded mode diable editing and lock position check boxeds
        		if(IsEmbedded && IsCalledFrom == 4)
        		{
        			
        			$("#BtnLockTxtPosition").css("visibility", "hidden");
        			$("#BtnLockEditing").css("visibility", "hidden");
                    $("#BtnPrintObj").css("visibility", "hidden");

                    // hiding labels 
                    $("#lblLockPositionTxt").css("visibility", "hidden");
        			$("#lblDoNotPrintTxt").css("visibility", "hidden");
                    $("#lblLockEditingTxt").css("visibility", "hidden");
        		}
    }

   

    // adding a handler to listen to double click call on fabric document 
    //fabric.util.addListener(fabric.document, 'dblclick', dblClickHandler);
    // the function call on double click
    
    function dblClickHandler(e) {
        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();

        // for setting position equal to last panel 
        var lastPanelLocal = lastPanel;
        animatedcollapse.hide('DivAdvanceColorPanel');
        if (activeObject && lastPanel != activeObject.type) {
            // hide all open panels
            animatedcollapse.hide(['textPropertPanel', 'DivAdvanceColorPanel', 'DivColorPallet', 'ShapePropertyPanel', 'ImagePropertyPanel', 'UploadImage', 'quickText', 'addImage', 'addText', 'DivAlignObjs']);

        }
        lastPanel = activeObject.type;
        if (activeGroup) {
            animatedcollapse.hide(['textPropertPanel', 'DivAdvanceColorPanel', 'DivColorPallet', 'ShapePropertyPanel', 'ImagePropertyPanel', 'UploadImage', 'quickText', 'addImage', 'addText', 'DivToolTip']);
            setPanelPosition("DivAlignObjs",lastPanelLocal);
            animatedcollapse.show("DivAlignObjs");
        }

        // check if the object is not editable or editable commented the check of editable for designers 
        //		if (activeObject  && activeObject.IsPositionLocked && !activeObject.IsEditable && IsCoorporate) {
        //		        // not editable 
        //				canvas.discardActiveObject();   
        //				animatedcollapse.hide(['textPropertPanel', 'DivAdvanceColorPanel', 'DivColorPallet','ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        //		
        //		}
        //		else {
        //editable object
        if (activeObject && activeObject.type === 'text') {
            setPanelPosition("textPropertPanel",lastPanelLocal);
            animatedcollapse.show('textPropertPanel');
            updateTextProperties(activeObject);

        }
        else if (activeObject && activeObject.type === 'image') {
            updateImgProperties(activeObject);
            setPanelPosition("ImagePropertyPanel",lastPanelLocal);
            animatedcollapse.show('ImagePropertyPanel');
            DisplayDiv('1');
        } else if (activeObject && activeObject.type === 'path') {
            setPanelPosition("ImagePropertyPanel",lastPanelLocal);
            updateImgProperties(activeObject);
            animatedcollapse.show('ImagePropertyPanel'); DisplayDiv('2');
        } else if (activeObject && activeObject.type === 'rect') {
            updateImgProperties(activeObject);
            setPanelPosition("ImagePropertyPanel",lastPanelLocal);
            animatedcollapse.show('ImagePropertyPanel'); DisplayDiv('2');
        } else if (activeObject && activeObject.type === 'ellipse') {
            updateImgProperties(activeObject);
            setPanelPosition("ImagePropertyPanel",lastPanelLocal);
            animatedcollapse.show('ImagePropertyPanel'); DisplayDiv('2');
        }
        //}
        if (activeGroup && activeGroup.type === 'group') {
            lastPanel = activeGroup.type;
            //animatedcollapse.hide(['textPropertPanel', 'DivAdvanceColorPanel', 'DivColorPallet','ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
            // animatedcollapse.show('textPropertPanel');
            $("#EditTXtArea").attr("disabled", "disabled");
            $("#BtnSearchTxt").attr("disabled", "disabled");
            $("#BtnUpdateText").attr("disabled", "disabled");

        }
        else if (activeGroup && activeGroup.type === 'image') {
            animatedcollapse.show('ImagePropertyPanel');
            DisplayDiv('1');
        }
    }

    // add new text button Clicked
    $('#BtnAddNewText').click(function () {
        var NewTextObejct = {};
        NewTextObejct = fabric.util.object.clone(TemplateObjects[0]);
        NewTextObejct.Name = "New Text";
        NewTextObejct.ContentString = $('#txtAddNewText').val();
        NewTextObejct.ObjectID = --NewControlID;
        NewTextObejct.ColorHex = "#000000";
        NewTextObejct.ColorC = 0;
        NewTextObejct.ColorM = 0;
        NewTextObejct.ColorY = 0;
        NewTextObejct.ColorK = 100;
        NewTextObejct.IsBold = false;
        NewTextObejct.IsItalic = false;
        NewTextObejct.LineSpacing = 1;
        NewTextObejct.ProductPageId = SelectedPageID;
        NewTextObejct.$id = (parseInt(TemplateObjects[TemplateObjects.length - 1].$id) + 4);
        var text = $('#txtAddNewText').val();
        //alert(text.length);
        var textLength = text.length;
        NewTextObejct.MaxWidth = 100;
        NewTextObejct.MaxHeight = 30;

        if(textLength < 30)
        {
            var diff = textLength /10;
            NewTextObejct.MaxWidth = 100* diff;
        } else 
        {
            NewTextObejct.MaxWidth = 300;
            var diff = textLength /30;
            NewTextObejct.MaxHeight = 15*diff;
        }
        
        
        NewTextObejct.FontSize = 14;


        var uiTextObject = AddTextObject(canvas, NewTextObejct);
        canvas.centerObject(uiTextObject);
        NewTextObejct.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
        NewTextObejct.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
        canvas.renderAll();
        $('#txtAddNewText').val("");
        animatedcollapse.toggle('addText');
        animatedcollapse.toggle('quickText');
        TemplateObjects.push(NewTextObejct);
    });

    // function that is called when update text is button is clicked   
    document.getElementById('BtnUpdateText').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        if (activeObject) {
             
            // changing text box height according to the updated text 
            var text = $('#EditTXtArea').val();
            var textLength = text.length;
            // checking for empty text
            if(textLength > 0 )
            {

                activeObject.text = $("#EditTXtArea").val();
                var oldHeight =  activeObject.maxHeight;
                var oldWidth = activeObject.maxWidth;
                var diff = textLength /30;
                var widthDiff = 0; 
                var maxLength = 0;
                var fontSize =   activeObject.fontSize;
                var sentence = text.split(/\r\n|\r|\n/);
                for (var i = 0; i < sentence.length; i++)
                {
                    var length = sentence[i].length;
                    if(length > maxLength) 
                    {
                        maxLength = length;
                    }
                }  
                
                widthDiff = maxLength  * 2/3;
                widthDiff = widthDiff * fontSize;
                diff = (sentence.length  * fontSize)
                diff += 25;
                var canvasHeight = Math.floor(canvas.height);
                var canvasWidth = Math.floor(canvas.width);
                if (widthDiff < canvasWidth)
                {
                    activeObject.maxWidth = widthDiff;
                } else 
                {
                    activeObject.maxWidth = canvasWidth - 50;
                }
                if(diff < canvasHeight )
                {
                    activeObject.maxHeight =  diff;
                } else {
                     activeObject.maxHeight = canvasHeight - 50;
                }
                var heightDiff = Math.floor(diff/2) - Math.floor(oldHeight/2); 
                var widthdifference = Math.floor(widthDiff/2) - Math.floor(oldWidth/2);
                activeObject.top = activeObject.top + heightDiff;
                activeObject.left = activeObject.left + widthdifference;
                activeObject.saveState();
                onObjModified(activeObject);
                canvas.renderAll();
                // $("#EditTXtArea").val("");
                animatedcollapse.toggle('textPropertPanel');
            } 
            else 
            {
                alert("if you want to empty the text in this field, kindly delete it!");
            }
        }

    }
    // function that is called On mouse Click 
    canvas.observe('mouse:down', onMouseDown);


    function onMouseDown(e) {

        //animatedcollapse.hide(['addText', 'DivAdvanceColorPanel','DivColorPallet','addImage', 'quickText','UploadImage', 'ImagePropertyPanel', 'ShapePropertyPanel','textPropertPanel']);
    }



    // function that is called when an object Is modified in scale, position etc ... it is used to save state for undo / redo actions
    //canvas.observe('object:modified', onObjModified); 
    canvas.observe('object:modified', OnModify);
    function OnModify(event) {
        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();
        if (activeGroup) {
            if (shftlDown == false) {

                var objectsInGroup = activeGroup.getObjects();
                objectsInGroup.forEach(function (object) {
                    var clonedItem = fabric.util.object.clone(object);
                    clonedItem.left += activeGroup.left;
                    clonedItem.top += activeGroup.top;
                    onObjModified(clonedItem);
                    //alert(clonedItem.top);
                });
            }
        } else if (activeObject) {
            onObjModified(activeObject);
        }
    }

    // now we can observe "object:over" and "object:out" events// in this example, object is set to red color on hover over and green color on hover out
    canvas.observe('object:over', function (e) {
        //e.memo.target.setFill('red');
        //var ctx = canvas.getContext();
        //e.memo.target.drawCorners(ctx);
        //canvas.renderAll();
    });
    canvas.observe('object:out', function (e) {
        //e.memo.target.setFill('green');
        //canvas.renderAll();
    });

    canvas.observe('selection:cleared', function (e) {
        //        for (var i = activeObjectButtons.length; i--; ) {
        //            activeObjectButtons[i].disabled = true;
        //        }
    });


    //select events
    canvas.observe('object:selected', onObjectSelected);
    canvas.observe('group:selected', onGroupSelected);


    //moving event to do object snapping
    canvas.observe('object:moving', onObjMoving);
    // function for on object selected 
    function onGroupSelected(e) {
        var activeGroup = canvas.getActiveGroup();
        dblClickHandler(e);
    }

    function onObjectSelected(e) {
        //alert();

        var activeObject = canvas.getActiveObject();
        var activeGroup = canvas.getActiveGroup();

        if (activeGroup && activeGroup.type === 'group') {

            ToolTipHandler(1);
        }
        else if (activeGroup && activeGroup.type === 'image') {
            ToolTipHandler(2);
        }

        if (activeObject && activeObject.type === 'text') {
            ToolTipHandler(3);
        }
        else if (activeObject && activeObject.type === 'image') {

            ToolTipHandler(2);
        } else if (activeObject && activeObject.type === 'path') {

            ToolTipHandler(2);
        } else if (activeObject && activeObject.type === 'rect') {
            ToolTipHandler(2);
        } else if (activeObject && activeObject.type === 'circle') {
            ToolTipHandler(2);
        }


        dblClickHandler(e);
    }

    //object moving event handler to snap objects to grid if flag on
    function onObjMoving(e) {

        var X = e.memo.target.left;
        var Y = e.memo.target.top;
        //logAction(e.memo.target.ObjectID + " pos x " +  e.memo.target.left);



        if (IsSnapToGrid) {
            //new code
            var line1 = 0;
            var line2 = 0;

            line1 = SnapXPoints[0];
            line2 = SnapXPoints[1];

            var iCounter = 1;

            while (iCounter < SnapXPoints.length - 1) {

                if (X > line1 && X < line2) {
                    X = line1;
                    break;
                }

                iCounter++;
                line1 = SnapXPoints[iCounter - 1];
                line2 = SnapXPoints[iCounter];
            }

            line1 = 0;
            line2 = 0;

            line1 = SnapYPoints[0];
            line2 = SnapYPoints[1];

            iCounter = 1;

            while (iCounter < SnapYPoints.length - 1) {

                if (Y > line1 && Y < line2) {
                    Y = line1;
                    break;
                }

                iCounter++;
                line1 = SnapYPoints[iCounter - 1];
                line2 = SnapYPoints[iCounter];
            }

            e.memo.target.left = X;
            e.memo.target.top = Y;

        }

    }


    //handling this event to stop bubbling of keyup event which reacts with body main keyup trap
    $('#EditTXtArea').keyup(function (event) {

        if (event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            event.cancelBubble = true;
        }

    });


    //handling this event to stop bubbling of keyup event which reacts with body main keyup trap
    $('#txtAddNewText').keyup(function (event) {

        if (event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            event.cancelBubble = true;
        }

    });


    //text edit toolbar events
    // LockPosition2 event
    $("#BtnLockTxtPosition").click(function () {

        var thisCheck = $(this);
        var activeObject = canvas.getActiveObject();
        if (activeObject.type === 'text') {
            if (thisCheck.is(':checked')) {
                activeObject.IsPositionLocked = true;
                activeObject.lockMovementX = true;
                activeObject.lockMovementY = true;
                activeObject.lockScalingX = true;
                activeObject.lockScalingY = true;
                activeObject.lockRotation = true;
            }
            else {
                activeObject.IsPositionLocked = false;
                activeObject.lockMovementX = false;
                activeObject.lockMovementY = false;
                activeObject.lockScalingX = false;
                activeObject.lockScalingY = false;
                activeObject.lockRotation = false;
            }
            onObjModified(activeObject);

        }
        else if (activeObject.type === 'group') {
            var objectsInGroup = activeObject.getObjects();
            objectsInGroup.forEach(function (object) {
                if (thisCheck.is(':checked')) {
                    object.IsPositionLocked = true;
                    object.lockMovementX = true;
                    object.lockMovementY = true;
                    object.lockScalingX = true;
                    object.lockScalingY = true;
                    object.lockRotation = true;
                }
                else {
                    object.IsPositionLocked = false;
                    object.lockMovementX = false;
                    object.lockMovementY = false;
                    object.lockScalingX = false;
                    object.lockScalingY = false;
                    object.lockRotation = false;
                }
                onObjModified(object);
            });

        }
    });

    // do not print  the object click  event
    $("#BtnPrintObj").click(function () {
        var thisCheck = $(this);
        var activeObject = canvas.getActiveObject();
        if (activeObject.type === 'text') {
            if (thisCheck.is(':checked')) {
                activeObject.IsHidden = true;
            }
            else {
                activeObject.IsHidden = false;
            }
            onObjModified(activeObject);
        }
        else if (activeObject.type === 'group') {
            var objectsInGroup = activeObject.getObjects();
            objectsInGroup.forEach(function (object) {
                if (thisCheck.is(':checked')) {
                    activeObject.IsHidden = true;
                }
                else {
                    activeObject.IsHidden = false;
                }
            });
        }
    });


    // function to lock editing of a text object
    $("#BtnLockEditing").click(function () {
        var thisCheck = $(this);
        var activeObject = canvas.getActiveObject();
        if (activeObject.type === 'text') {
            if (activeObject.get('IsEditable')) {
                activeObject.IsEditable = false;
            }
            else {
                activeObject.IsEditable = true;
            }
            onObjModified(activeObject);
        }
        else if (activeObject.type === 'group') {
            var objectsInGroup = activeObject.getObjects();
            objectsInGroup.forEach(function (object) {
                if (activeObject.get('IsEditable')) {
                    activeObject.IsEditable = false;
                }
                else {
                    activeObject.IsEditable = true;
                }
            });
        }
        animatedcollapse.toggle('textPropertPanel');
    });

    // function that is called when the selected index of the font Select option is changed 
    document.getElementById('BtnSelectFonts').onchange = function (ev) {
        var fontFamily = $('#BtnSelectFonts').val();

        if (fontFamily != "") {
            var selectedObject = canvas.getActiveObject(),
            activeGroup = canvas.getActiveGroup();
            if (selectedObject) {
                if (selectedObject && selectedObject.type === 'text') {
                    selectedObject.fontFamily = fontFamily;
                    onObjModified(selectedObject);
                    canvas.renderAll();
                }
            }
            else if (activeGroup) {
                if (activeGroup && activeGroup.type === 'text') {
                    activeGroup.set("fontFamily", fontFamily);
                    canvas.renderAll();
                    onObjModified(selectedObject);
                }

            }
        }

    }


    // function that is called when the selected index of the font size Select option is changed where input type range is supported
    document.getElementById('BtnFontSize').onchange = function (ev) {
        var fz = $('#BtnFontSize').val();
        var fontSize = parseInt(fz);
        if (fontSize != "") {
            var selectedObject = canvas.getActiveObject(),
            activeGroup = canvas.getActiveGroup();
            if (selectedObject) {
                if (selectedObject && selectedObject.type === 'text') {
                    selectedObject.fontSize = fontSize;
                    onObjModified(selectedObject);
                    canvas.renderAll();
                }
            }
            else if (activeGroup) {
                if (activeGroup && activeGroup.type === 'text') {
                    activeGroup.set("fontSize", fontSize);
                    canvas.renderAll();
                    onObjModified(selectedObject);
                }
            }
        }
    }

    // function to change font size using select control where input element is not supported
    document.getElementById('BtnSelectFontSize').onchange = function (ev) {
        var fz = $('#BtnSelectFontSize').val();

        var fontSize = parseInt(fz);
        if (fontSize != "") {
            var selectedObject = canvas.getActiveObject(),
            activeGroup = canvas.getActiveGroup();
            if (selectedObject) {
                if (selectedObject && selectedObject.type === 'text') {
                    selectedObject.fontSize = fontSize;
                    onObjModified(selectedObject);
                    canvas.renderAll();
                }
            }
            else if (activeGroup) {
                if (activeGroup && activeGroup.type === 'text') {
                    activeGroup.set("fontSize", fontSize);
                    canvas.renderAll();
                    onObjModified(selectedObject);
                }
            }
        }
    }

    // function to add bold text
    document.getElementById('BtnBoldTxt').onclick = function (ev) {
        var selectedObject = canvas.getActiveObject();
        if (selectedObject) {
            if (selectedObject.fontWeight == 'bold') {
                selectedObject.set('fontWeight', '');
            }
            else {
                selectedObject.set('fontWeight', 'bold');
            }
            onObjModified(selectedObject);
            canvas.renderAll();
        }
    }

    //handling Textbox Italic Logic
    var cmdItalicBtn = document.getElementById('BtnItalicTxt');
    if (cmdItalicBtn) {
        //activeObjectButtons.push(cmdItalicBtn);
        //cmdItalicBtn.disabled = true;
        cmdItalicBtn.onclick = function () {
            var activeObject = canvas.getActiveObject();
            if (activeObject && activeObject.type === 'text') {
                if (activeObject.fontStyle == 'italic') {
                    activeObject.set('fontStyle', '');
                }
                else {
                    activeObject.set('fontStyle', 'italic'); ;
                }
                onObjModified(activeObject);
                canvas.renderAll();
            }
        };
    }

    //handling object Line height object
    $("#txtLineHeight").change(function (event) {
        var activeObject = canvas.getActiveObject();
        if (activeObject && activeObject.type === 'text') {
            activeObject.lineHeight = $("#txtLineHeight").val();
        }
        onObjModified(activeObject);
        canvas.renderAll();
        if (event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            event.cancelBubble = true;
        }
    });

    $("#txtLineHeight").keyup(function (event) {
        if (event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            event.cancelBubble = true;
        }
    });

    // function to justify text left
    document.getElementById('BtnJustifyTxt1').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        if (activeObject && activeObject.type === 'text') {
            activeObject.set('textAlign', 'left');
            onObjModified(activeObject);
            canvas.renderAll();
        }
    }

    // function to justify text left
    document.getElementById('BtnJustifyTxt2').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        if (activeObject && activeObject.type === 'text') {
            activeObject.set('textAlign', 'center');
            onObjModified(activeObject);
            canvas.renderAll();
        }

    }

    // function to justify text left
    document.getElementById('BtnJustifyTxt3').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        if (activeObject && activeObject.type === 'text') {
            activeObject.set('textAlign', 'right');
            onObjModified(activeObject);
            canvas.renderAll();
        }

    }


    //functions to bring text to front and send to back  
    // function to update objects with canvas 
    function UpdateCanvasObjects() {
        var objects = canvas.getObjects();
        for (i = 0; i < TemplateObjects.length; i++) {
            objects.filter(function (obj) {
                if (obj.get('ObjectID') == TemplateObjects[i].ObjectID) {
                    TemplateObjects[i].DisplayOrderPdf = objects.indexOf(obj);
                }
            });
        }
    }

    // fuction to bring text to front 
    document.getElementById('BtnTxtarrangeOrder1').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.bringToFront(activeObject);
        onObjModified(activeObject);
        canvas.renderAll();
        UpdateCanvasObjects();
    }


    // bring one step forward
    document.getElementById('BtnTxtarrangeOrder2').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.bringForward(activeObject);
        canvas.renderAll();
        onObjModified(activeObject);
        UpdateCanvasObjects();
    }
    // send on step backward
    document.getElementById('BtnTxtarrangeOrder3').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.sendBackwards(activeObject);
        canvas.renderAll();
        onObjModified(activeObject);
        UpdateCanvasObjects();
    }
    // send to back 
    document.getElementById('BtnTxtarrangeOrder4').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.sendToBack(activeObject);
        canvas.renderAll();
        onObjModified(activeObject);
        UpdateCanvasObjects();
    }




    // function to rotate left by 90 degree
    document.getElementById('BtnRotateTxtLft').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        var angle = activeObject.getAngle();
        angle = angle - 5;
        activeObject.setAngle(angle);
        onObjModified(activeObject);
        canvas.renderAll();
    }
    // function to rotate right by 90 degree
    document.getElementById('BtnRotateTxtRight').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        var angle = activeObject.getAngle();
        angle = angle + 5;
        activeObject.setAngle(angle);
        onObjModified(activeObject);
        canvas.renderAll();
    }

    function CompareObjSize(obj1, obj2) {
        if (obj1.width > obj2.width && obj1.height > obj2.height) {
            return true;
        }
        else {
            return false;
        }
    }
    // function to align objects to center 
    document.getElementById('BtnAlignObjCenter').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var activeGroup = canvas.getActiveGroup().getObjects();
            if (activeGroup) {
                //find the top most object by finding 
                var minID = 0;
                var mintop = 0;
                var left = 0;
                //var ModifiedObjectIds = [];
                mintop = activeGroup[0].top;
                minID = activeGroup[0].ObjectID;
                left = activeGroup[0].left;
                // ModifiedObjectIds.push(activeGroup[0].ObjectID);
                if (activeGroup) {
                    for (i = 0; i < activeGroup.length; i++) {
                        if (activeGroup[i].ObjectID != minID) {
                            activeGroup[i].left = left;
                            //onObjModified(activeGroup[i]);
                            //ModifiedObjectIds.push(activeGroup[i].ObjectID);

                        }

                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < activeGroup.length; i++) {
                        onObjModified(activeGroup[i]);
                    }
                    //                // store objects positions  
                    //                var canvasObjects = canvas.getObjects();
                    //                for(var j = 0; j < canvasObjects.length; j++)
                    //                {
                    //                    //alert(ModifiedObjectIds[j]);
                    //                    for(var i =0; i<ModifiedObjectIds.length;i++)
                    //                    {
                    //                        if(canvasObjects[j].ObjectID == ModifiedObjectIds[i])
                    //                        {
                    //                            onObjModified(canvasObjects[j]);
                    //                            //alert();
                    //                        }
                    //                    }
                    //                }

                }
                canvas.renderAll();
            }
        }
    }

    // align object horizontally left
    document.getElementById('BtnAlignObjLeft').onclick = function (ev) {

        if (canvas.getActiveGroup()) {
            var activeGroup = canvas.getActiveGroup().getObjects();
            if (activeGroup) {
                //find the top most object by finding 

                var minID = 0;
                var mintop = 0;
                var left = 0
                mintop = activeGroup[0].top;
                minID = activeGroup[0].ObjectID;
                left = activeGroup[0].left - activeGroup[0].width / 2;
               
                if (activeGroup) {
                    for (i = 0; i < activeGroup.length; i++) {
                        if (activeGroup[i].ObjectID != minID) {
                            activeGroup[i].left = left + activeGroup[i].width / 2;
                            //onObjModified(activeGroup[i]);
                            //console.log(left + " " +  activeGroup[i].left);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < activeGroup.length; i++) {
                        onObjModified(activeGroup[i]);
                    }
                }
                canvas.renderAll();
            }
        }

    }


    // align object horizontally right
    document.getElementById('BtnAlignObjRight').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var activeGroup = canvas.getActiveGroup().getObjects();
            if (activeGroup) {
                //find the top most object by finding 

                var minID = 0;
                var mintop = 0;
                var left = 0

                mintop = activeGroup[0].top;
                minID = activeGroup[0].ObjectID;
                left = activeGroup[0].left + activeGroup[0].width / 2;

                if (activeGroup) {
                    for (i = 0; i < activeGroup.length; i++) {
                        if (activeGroup[i].ObjectID != minID) {
                            activeGroup[i].left = left - activeGroup[i].width / 2;
                            //onObjModified(activeGroup[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < activeGroup.length; i++) {
                        onObjModified(activeGroup[i]);
                    }
                }
                canvas.renderAll();
            }
        }

        //		var diff =  0;
        //		var activeGroup = canvas.getActiveGroup().getObjects();
        //		if(activeGroup) {     
        //			for (i=1;i<activeGroup.length;i++) {
        //				if (CompareObjSize(activeGroup[0],activeGroup[i])) {
        //					activeGroup[i].left     = (activeGroup[0].width) /2   - (activeGroup[i].width) /2; 
        //				}
        //				else {                
        //					diff = activeGroup[i].left  - activeGroup[0].left  ;
        //					activeGroup[i].left     = (activeGroup[0].width) /2   - (activeGroup[i].width) /2 - diff;   
        //				}
        //			}
        //			//onObjModified(activeGroup);
        //			canvas.renderAll();
        //		}
    }

    // align object vertically top 
    document.getElementById('BtnAlignObjTop').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var activeGroup = canvas.getActiveGroup().getObjects();
            if (activeGroup) {
                //find the left most object by finding 

                var minID = 0;
                var minLeft = 99999;
                var top = 0
                //            var len = activeGroup.length;
                //            for (var i = 0; i < len; i++) 
                //            {
                //                if (activeGroup[i].left < minLeft) 
                //                {
                //                    minLeft = activeGroup[i].left;
                //                    minID = activeGroup[i].ObjectID;
                //                    top = activeGroup[i].top - activeGroup[i].height / 2;
                //                }
                //            }

                minLeft = activeGroup[0].left;
                minID = activeGroup[0].ObjectID;
                top = activeGroup[0].top - activeGroup[0].height / 2;

                if (activeGroup) {
                    for (i = 0; i < activeGroup.length; i++) {
                        if (activeGroup[i].ObjectID != minID) {
                            activeGroup[i].top = top + activeGroup[i].height / 2;

                            //onObjModified(activeGroup[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < activeGroup.length; i++) {
                        onObjModified(activeGroup[i]);
                    }
                }
                canvas.renderAll();
            }
        }

        //		var activeGroup = canvas.getActiveGroup().getObjects();
        //		if(activeGroup) {
        //			for (i=1;i<activeGroup.length;i++) {
        //				if (CompareObjSize(activeGroup[0],activeGroup[i])) {
        //					activeGroup[i].top     =  activeGroup[0].top/2  -   (activeGroup[i].top/2)  ;//((activeGroup[0].height/2) * (-1)) +  (activeGroup[i].height) /2
        //				}
        //				else {
        //					var diff = activeGroup[0].top - activeGroup[i].top;
        //					activeGroup[i].top     =  activeGroup[0].top  -   (activeGroup[i].height) /2  - diff ;//((activeGroup[0].height/2) * (-1)) +  (activeGroup[i].height) /2
        //				}
        //				//alert(activeGroup[i].height);
        //			 }
        //			//onObjModified(activeGroup);
        //			canvas.renderAll();
        //		}
    }

    // align object vertically middle 
    document.getElementById('BtnAlignObjMiddle').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var activeGroup = canvas.getActiveGroup().getObjects();
            if (activeGroup) {
                //find the left most object by finding 

                var minID = 0;
                var minLeft = 99999;
                var top = 0
                //            var len = activeGroup.length;
                //            for (var i = 0; i < len; i++) 
                //            {
                //                if (activeGroup[i].left < minLeft) 
                //                {
                //                    minLeft = activeGroup[i].left;
                //                    minID = activeGroup[i].ObjectID;
                //                    top = activeGroup[i].top;
                //                }
                //            }

                minLeft = activeGroup[0].left;
                minID = activeGroup[0].ObjectID;
                top = activeGroup[0].top;

                if (activeGroup) {
                    for (i = 0; i < activeGroup.length; i++) {
                        if (activeGroup[i].ObjectID != minID) {
                            activeGroup[i].top = top;
                            //onObjModified(activeGroup[i]);
                        }
                    }

                    canvas.discardActiveGroup();
                    for (var i = 0; i < activeGroup.length; i++) {
                        onObjModified(activeGroup[i]);
                    }
                }
                canvas.renderAll();
            }
        }
    }
    // align object vertically bottom 
    document.getElementById('BtnAlignObjBottom').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var activeGroup = canvas.getActiveGroup().getObjects();
            if (activeGroup) {
                //find the left most object by finding 

                var minID = 0;
                var minLeft = 99999;
                var top = 0
                //            var len = activeGroup.length;
                //            for (var i = 0; i < len; i++) 
                //            {
                //                if (activeGroup[i].left < minLeft) 
                //                {
                //                    minLeft = activeGroup[i].left;
                //                    minID = activeGroup[i].ObjectID;
                //                    top = activeGroup[i].top + activeGroup[i].height / 2;
                //                }
                //            }

                minLeft = activeGroup[0].left;
                minID = activeGroup[0].ObjectID;
                top = activeGroup[0].top + activeGroup[0].height / 2;

                if (activeGroup) {
                    for (i = 0; i < activeGroup.length; i++) {
                        if (activeGroup[i].ObjectID != minID) {
                            activeGroup[i].top = top - activeGroup[i].height / 2;
                            //onObjModified(activeGroup[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < activeGroup.length; i++) {
                        onObjModified(activeGroup[i]);
                    }
                }
                canvas.renderAll();
            }
        }
    }
    var removeSelectedEl = document.getElementById('BtnDeleteTxtObj');
    removeSelectedEl.onclick = function () {
        if (confirm("Are you sure you want to delete")) {
            var activeObject = canvas.getActiveObject(),
            activeGroup = canvas.getActiveGroup();
            if (activeObject) {
                onObjModified(activeObject, 'delete');
                canvas.remove(activeObject);
            }
            else if (activeGroup) {
                var objectsInGroup = activeGroup.getObjects();
                canvas.discardActiveGroup();
                objectsInGroup.forEach(function (object) {
                    onObjModified(object, 'delete');
                    canvas.remove(object);
                });
            }
        }
    };


    // images and shapes 
    // function to arrange images  BtnImageArrangeOrdr1
    // fuction to bring image to front 
    document.getElementById('BtnImageArrangeOrdr1').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.bringToFront(activeObject);
        onObjModified(activeObject);
        canvas.renderAll();
        UpdateCanvasObjects();
    }
    // bring one step forward
    document.getElementById('BtnImageArrangeOrdr2').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.bringForward(activeObject);
        canvas.renderAll();
        onObjModified(activeObject);
        UpdateCanvasObjects();
    }
    // send on step backward
    document.getElementById('BtnImageArrangeOrdr3').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.sendBackwards(activeObject);
        canvas.renderAll();
        onObjModified(activeObject);
        UpdateCanvasObjects();
    }
    // send to back 
    document.getElementById('BtnImageArrangeOrdr4').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        canvas.sendToBack(activeObject);
        canvas.renderAll();
        onObjModified(activeObject);
        UpdateCanvasObjects();
    }
    // function to rotate left by 90 degree
    document.getElementById('BtnImgRotateLeft').onclick = function (ev) {
        //alert();
        var activeObject = canvas.getActiveObject();
        var angle = activeObject.getAngle();
        angle = angle - 5;
        activeObject.setAngle(angle);
        onObjModified(activeObject);
        canvas.renderAll();
    }
    // function to rotate right by 90 degree
    document.getElementById('BtnImgRotateRight').onclick = function (ev) {
        //alert();
        var activeObject = canvas.getActiveObject();
        var angle = activeObject.getAngle();
        angle = angle + 5;
        activeObject.setAngle(angle);
        onObjModified(activeObject);
        canvas.renderAll();
    }

    $("#LockPositionImg").click(function () {
        var thisCheck = $(this);
        var activeObject = canvas.getActiveObject();

        if (thisCheck.is(':checked')) {
            activeObject.IsPositionLocked = true;
            activeObject.lockMovementX = true;
            activeObject.lockMovementY = true;
            activeObject.lockScalingX = true;
            activeObject.lockScalingY = true;
            activeObject.lockRotation = true;
        }
        else {
            activeObject.IsPositionLocked = false;
            activeObject.lockMovementX = false;
            activeObject.lockMovementY = false;
            activeObject.lockScalingX = false;
            activeObject.lockScalingY = false;
            activeObject.lockRotation = false;
        }
        onObjModified(activeObject);

        //        }
        //        else if (activeObject.type ==='group') {
        //            var objectsInGroup = activeObject.getObjects();
        //            objectsInGroup.forEach(function (object) {  
        //                if (thisCheck.is (':checked')) {
        //                    object.IsPositionLocked = true;       
        //                    object.lockMovementX = true;
        //                    object.lockMovementY = true;
        //                    object.lockScalingX = true;
        //                    object.lockScalingY = true;
        //                    object.lockRotation = true;
        //                }
        //                else {
        //                    object.IsPositionLocked = false;
        //                    object.lockMovementX = false;
        //                    object.lockMovementY = false;
        //                    object.lockScalingX = false;
        //                    object.lockScalingY = false;
        //                    object.lockRotation = false;
        //                }
        //                onObjModified(object);
        //            });

        //        }
    });

    $("#ChkBoxFontAllowed").click(function () {
        var thisCheck = $(this);

        if (thisCheck.is(':checked')) {
            IsFontLegal = true;
        }
        else {
            IsFontLegal = false;
        }


    });
    // do not print  the object click  event
    $("#BtnPrintImage").click(function () {
        var thisCheck = $(this);
        var activeObject = canvas.getActiveObject();
        //        if (activeObject.type === 'text') {
        if (thisCheck.is(':checked')) {
            activeObject.IsHidden = true;
        }
        else {
            activeObject.IsHidden = false;
        }
        onObjModified(activeObject);
        //        }
        //        else if (activeObject.type ==='group') {
        //            var objectsInGroup = activeObject.getObjects();
        //            objectsInGroup.forEach(function (object) {  
        //                if (thisCheck.is (':checked')){
        //                    activeObject.IsHidden = true; 
        //                }
        //                else {
        //                    activeObject.IsHidden = false; 
        //                }
        //            });
        //        }        
    });
    // function to flip x

    document.getElementById('BtnFlipImg1').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        if (activeObject.get('flipX')) {
            activeObject.set('flipX', false);
        } else {
            activeObject.set('flipX', true);
        }
        onObjModified(activeObject);
        canvas.renderAll();
    }
    // function to flip y
    document.getElementById('BtnFlipImg2').onclick = function (ev) {
        var activeObject = canvas.getActiveObject();
        if (activeObject.get('flipY')) {
            activeObject.set('flipY', false);
        } else {
            activeObject.set('flipY', true);
        }
        onObjModified(activeObject);
        canvas.renderAll();
    }
    // delete image button 
    var removeSelectedEl = document.getElementById('btnDeleteImage');
    removeSelectedEl.onclick = function () {
        if (confirm("Are you sure you want to delete")) {
            var activeObject = canvas.getActiveObject(),
            activeGroup = canvas.getActiveGroup();
            if (activeObject) {
                onObjModified(activeObject, 'delete');
                canvas.remove(activeObject);
            }
            else if (activeGroup) {
                var objectsInGroup = activeGroup.getObjects();
                canvas.discardActiveGroup();
                objectsInGroup.forEach(function (object) {
                    onObjModified(object, 'delete');
                    canvas.remove(object);
                });
            }
        }
    };

    ///////////////////////////

    var supportsInputOfType = function (type) {
        return function () {
            var el = document.createElement('input');
            try {
                el.type = type;
            }
            catch (err) { }
            return el.type === type;
        };
    };

    // check if the number Range input value of html 5 is supported
    var supportsRange = supportsInputOfType('number');
    if (supportsRange()) {
        (function () {
           // $('#BtnFontSize').css({ 'display': 'inline' });
           $('#BtnSelectFontSize').css({ 'display': 'inline' });
        })();
    }
    else {
        (function () {
            $('#BtnSelectFontSize').css({ 'display': 'inline' });
        })();
    }

    var supportsSlider = supportsInputOfType('range'),
    supportsColorpicker = supportsInputOfType('color');

    if (supportsSlider()) {
        (function () {
            //			var controls = document.getElementById('controls');
            //			var controls2 = document.getElementById('controls2');
            //			var sliderLabel = document.createElement('label');
            //			sliderLabel.htmlFor = 'opacity';
            //			sliderLabel.innerHTML = 'Opacity: ';
            //			var slider = document.createElement('input');
            //			var sliderLabel2 = document.createElement('label');
            //			sliderLabel2.htmlFor = 'opacity2';
            //			sliderLabel2.innerHTML = 'Opacity: ';
            //			var slider2 = document.createElement('input');
            //			try { slider.type = 'range'; } catch (err) { }
            //			try { slider2.type = 'range'; } catch (err) { }
            //			slider.id = 'opacity';
            //			slider.value = 100;
            //			slider2.id = 'opacity';
            //			slider2.value = 100;
            //			//controls.appendChild(sliderLabel);
            //			controls.appendChild(slider);
            //			controls2.appendChild(sliderLabel2);
            //			controls2.appendChild(slider2);
            //			canvas.calcOffset();
            //			slider.onchange = function () {
            //				var activeObject = canvas.getActiveObject(),
            //				activeGroup = canvas.getActiveGroup();
            //				if (activeObject || activeGroup) {
            //					(activeObject || activeGroup).setOpacity(parseInt(this.value, 10) / 100);
            //					canvas.renderAll();
            //				}
            //			};
            //			slider2.onchange = function () {
            //				var activeObject = canvas.getActiveObject(),
            //				activeGroup = canvas.getActiveGroup();
            //				if (activeObject || activeGroup) {
            //					(activeObject || activeGroup).setOpacity(parseInt(this.value, 10) / 100);
            //					canvas.renderAll();
            //				}
            //			};
        })();
    }

    if (supportsColorpicker()) {
        (function () {
            var controls = document.getElementById('controls');

            var label = document.createElement('label');
            label.htmlFor = 'color';
            label.innerHTML = 'Color: ';
            label.style.marginLeft = '10px';

            var colorpicker = document.createElement('input');
            colorpicker.type = 'color';
            colorpicker.id = 'color';
            colorpicker.style.width = '40px';

            controls.appendChild(label);
            controls.appendChild(colorpicker);

            canvas.calcOffset();

            colorpicker.onchange = function () {
                var activeObject = canvas.getActiveObject(),
                activeGroup = canvas.getActiveGroup();
                if (activeObject || activeGroup) {
                    (activeObject || activeGroup).setFill(this.value);
                    canvas.renderAll();
                }
            };
        })();
    }

    var gradientifyBtn = document.getElementById('gradientify');
    var activeObjectButtons = [
        gradientifyBtn
    ];
    var opacityEl = document.getElementById('opacity');
    if (opacityEl) {
        activeObjectButtons.push(opacityEl);
    }
    var colorEl = document.getElementById('color');
    if (colorEl) {
        activeObjectButtons.push(colorEl);
    }
    for (var i = activeObjectButtons.length; i--; ) {
        // commented for testing
        //  activeObjectButtons[i].disabled = true;
    }


    var drawingModeEl = document.getElementById('drawing-mode'),
    drawingOptionsEl = document.getElementById('drawing-mode-options'),
    drawingColorEl = document.getElementById('drawing-color'),
    drawingLineWidthEl = document.getElementById('drawing-line-width');

    drawingModeEl.onclick = function () {
        canvas.isDrawingMode = !canvas.isDrawingMode;
        if (canvas.isDrawingMode) {
            drawingModeEl.text = 'Cancel drawing mode';
            drawingModeEl.className = 'is-drawing';
            drawingOptionsEl.style.display = 'none';
        }
        else {
            drawingModeEl.innerHTML = '';
            drawingModeEl.className = '';
            drawingOptionsEl.style.display = 'none';

        }
    };

    //capture the newly created drawing SVG object
    canvas.observe('path:created', function (newPath) {

        var objPath = newPath.memo.path;
        var NewSVGObject = {};
        NewSVGObject = fabric.util.object.clone(TemplateObjects[0]);

        NewSVGObject.Name = "Path";
        NewSVGObject.ObjectID = --NewControlID;
        NewSVGObject.ColorHex = "#000000";
        NewSVGObject.ColorC = 0;
        NewSVGObject.ColorM = 0;
        NewSVGObject.ColorY = 0;
        NewSVGObject.ColorK = 100;
        NewSVGObject.IsBold = false;
        NewSVGObject.IsItalic = false;
        NewSVGObject.ObjectType = 9; //path
        NewSVGObject.ProductPageId = SelectedPageID;
        NewSVGObject.MaxWidth = objPath.width;
        NewSVGObject.MaxHeight = objPath.height;
        NewSVGObject.ExField1 = objPath.strokeWidth;

        NewSVGObject.$id = (parseInt(TemplateObjects[TemplateObjects.length - 1].$id) + 4);
        NewSVGObject.ContentString = objPath.toSVG();

        objPath.set({
            borderColor: 'red',
            cornerColor: 'green',
            cornersize: 6
        });
        objPath.ObjectID = NewSVGObject.ObjectID;
        NewSVGObject.PositionX = objPath.left - objPath.width / 2;
        NewSVGObject.PositionY = objPath.top - objPath.height / 2;
        TemplateObjects.push(NewSVGObject);
    });

    drawingColorEl.onchange = function () {
        canvas.freeDrawingColor = drawingColorEl.value;
    };

    drawingLineWidthEl.onchange = function () {
        canvas.freeDrawingLineWidth = parseInt(drawingLineWidthEl.value, 10) || 1; // disallow 0, NaN, etc.
    };

    canvas.freeDrawingColor = drawingColorEl.value;
    canvas.freeDrawingLineWidth = parseInt(drawingLineWidthEl.value, 10) || 1;

    document.onkeydown = function (e) {
        var obj = canvas.getActiveObject() || canvas.getActiveGroup();
        if (obj && e.keyCode === 8) {
            // this is horrible. need to fix, so that unified interface can be used
            if (obj.type === 'group') {
                // var groupObjects = obj.getObjects();
                //         canvas.discardActiveGroup();
                //         groupObjects.forEach(function(obj) {
                //           canvas.remove(obj);
                //         });
            }
            else {
                //canvas.remove(obj);
            }
            canvas.renderAll();
            // return false;
        }
    };

    setTimeout(function () {
        canvas.calcOffset();
    }, 100);

    if (document.location.search.indexOf('guidelines') > -1) {
        initCenteringGuidelines(canvas);
        initAligningGuidelines(canvas);
    }

    //    gradientifyBtn.onclick = function () {
    //        var obj = canvas.getActiveObject();
    //        if (obj) {
    //            obj.setGradientFill(canvas.getContext(), {
    //                x2: (getRandomInt(0, 1) ? 0 : obj.width),
    //                y2: (getRandomInt(0, 1) ? 0 : obj.height),
    //                colorStops: {
    //                    0: '#' + getRandomColor(),
    //                    1: '#' + getRandomColor()
    //                }
    //            });
    //            canvas.renderAll();
    //        }
    //    };



    var cmdUnderlineBtn = document.getElementById('text-cmd-underline');
    if (cmdUnderlineBtn) {
        activeObjectButtons.push(cmdUnderlineBtn);
        cmdUnderlineBtn.disabled = true;
        cmdUnderlineBtn.onclick = function () {
            var activeObject = canvas.getActiveObject();
            if (activeObject && activeObject.type === 'text') {
                activeObject.textDecoration = (activeObject.textDecoration == 'underline' ? '' : 'underline');
                this.className = activeObject.textDecoration ? 'selected' : '';
                canvas.renderAll();
            }
        };
    }

    var cmdLinethroughBtn = document.getElementById('text-cmd-linethrough');
    if (cmdLinethroughBtn) {
        activeObjectButtons.push(cmdLinethroughBtn);
        cmdLinethroughBtn.disabled = true;
        cmdLinethroughBtn.onclick = function () {
            var activeObject = canvas.getActiveObject();
            if (activeObject && activeObject.type === 'text') {
                activeObject.textDecoration = (activeObject.textDecoration == 'line-through' ? '' : 'line-through');
                this.className = activeObject.textDecoration ? 'selected' : '';
                canvas.renderAll();
            }
        };
    }

    var cmdOverlineBtn = document.getElementById('text-cmd-overline');
    if (cmdOverlineBtn) {
        activeObjectButtons.push(cmdOverlineBtn);
        cmdOverlineBtn.disabled = true;
        cmdOverlineBtn.onclick = function () {
            var activeObject = canvas.getActiveObject();
            if (activeObject && activeObject.type === 'text') {
                activeObject.textDecoration = (activeObject.textDecoration == 'overline' ? '' : 'overline');
                this.className = activeObject.textDecoration ? 'selected' : '';
                canvas.renderAll();
            }
        };
    }

})(this);