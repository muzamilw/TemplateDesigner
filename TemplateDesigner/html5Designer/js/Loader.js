$(document).ready(function () {
    //$("p").text("The DOM is now loaded and can be manipulated.");
    //alert('he DOM is now loaded and can be manipulated');

    //{ tags: "cat", tagmode: "any", format: "json" }

//    canvas.selectionColor = 'rgba(0,255,0,0.3)';
//    canvas.selectionBorderColor = 'red';
//    canvas.selectionLineWidth = 5;





    loadFonts();


    //loadTemplate(1278);



    $('#loadTemplate').click(function () {
        canvas.clear();
        //alert('Handler for .click() called.');
        loadTemplate($('#txtTemplateID').val());
    });


});


//{ TemplateID: TemplateID }


function loadFonts() {
    
    jQuery.cachedScript = function (url, options) {
        // allow user to set any option except for dataType, cache, and url  
        options = $.extend(options || {},        { dataType: "script", cache: true, url: url });
        // Use $.ajax() since it is more flexible than $.getScript  
        // Return the jqXHR object so we can chain callbacks  
        return jQuery.ajax(options);
    };
    // Usage
//    $.cachedScript("js/Fonts/Impact_400.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });

//    $.cachedScript("js/Fonts/Arial_400.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });

//    $.cachedScript("js/Fonts/Myriad_Web_Pro_400-Myriad_Web_Pro_700.font.js").done(function (script, textStatus) {
//        console.log(textStatus);

//    });

    $.cachedScript("js/Fonts/Arial_400.font.js").done(function (script, textStatus) {
        //console.log(textStatus);

    });


    //1230 card


    loadTemplate(1545);

    StopLoader();
}



function loadTemplate(TemplateID) {

        
    $.getJSON("http://localhost/DesignerService/TemplateSvc/" + TemplateID,
 function (data) {

     canvas.setHeight(data.PDFTemplateHeight);
     canvas.setWidth(data.PDFTemplateWidth);

     c6(TemplateID);

 });
}



function c6(TemplateID) {

    $.getJSON("http://localhost/DesignerService/TemplateObjectsSvc/" + TemplateID,

        function (data) {

            $.each(data, function (i, item) {

                if (item.isSide2Object == false) {
                    if (item.ObjectType == 2) {
                        //alert(item.ContentString);

                        //delicious_500
                        AddTextObject(canvas, item.ContentString, item.PositionX, item.PositionY, 'Arial', item.FontSize, item.IsItalic,item.isBold, item.RotationAngle, item.ColorHex, item.MaxWidth, item.MaxHeight, item.DisplayOrderPDF, item.Allignment, item.VAllignment);


                    }
                    else if (item.ObjectType == 3) {
                        AddImageObject(canvas, "../"+ item.ContentString, item.PositionX, item.PositionY, item.RotationAngle, item.MaxWidth, item.MaxHeight,item.DisplayOrderPDF);
                    }
                }
            });


            //alert(data.ObjectID);
            //            $.each(data.items, function (i, item) {

            //                $("#output").val = item.ObjectID;
            ////                $("<img/>").attr("src", item.media.m).appendTo("#images");
            ////                if (i == 3)
            ////                    return false;
            //            });
        });

    }


    function AddTextObject(cCanvas, contentString, positionX, positionY, fontFamily, fontSize,IsItalic, IsBold, rotationAngle, fontColor, width, height, DisplayOrder, hAlignment, vAlignment) {

        var hAlign = "";

        if (hAlignment == 1)
            hAlign = "left";
        else if (hAlignment == 2)
            hAlign = "center";
        else if (hAlignment == 3)
            hAlign = "right";

        var hStyle = "";
        if (IsItalic)
            hStyle = "italic";
       

        var hWeight = "";
        if (IsBold)
            hWeight = "bold";
            





        var textSample = new fabric.Text(contentString, {
            left: positionX + width/2,
            top: positionY + height/2,
            fontFamily: fontFamily,
            fontStyle: hStyle,
            fontWeight: hWeight,
            fontSize: fontSize,
            angle: rotationAngle,
            fill: fontColor,
            scaleX: 1,
            scaleY: 1,
            maxWidth: width,
            maxHeight:height,
            textAlign: hAlign
            
        });

        textSample.set({
            borderColor: 'red',
            cornerColor: 'green',
            cornersize: 6
        });

        //alert('help');
        //textSample.scaleToWidth(width);
        cCanvas.insertAt(textSample, DisplayOrder);
        //updateComplexity();
    }  // fontStyle:'bold',



    function AddImageObject(cCanvas, imagePath, positionX, positionY, rotationAngle, width, height,DisplayOrder) {
        fabric.Image.fromURL(imagePath, function (image) {
            image.set({
                left: positionX + width/2,
                top: positionY - height/2,
                angle: rotationAngle
            });
            //image.scale(1).setCoords();
            image.scaleToWidth(width);
            cCanvas.insertAt(image, DisplayOrder);
        });
       

        
    }   