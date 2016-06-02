
var D1CD = false;
var D1SD = false;
var D1SK = 16, ctrlKey = 17, vKey = 86, cKey = 67;
var D1CO = [];
var D1IFL;
var IsDesignModified = false;   //c05
var D1SF = 1.2;
var D1CS = 1;
// c06
var D1CZL = 0;
var D1DFT = 1;
var IsCoorporate = 0;
var D1ITE = 0;
//var undoManager = new UndoManager();
var D1LP = "";
// c04

var D1CH;
var D1CCW;
var D1CCML;
var D1CCMT;
var D1CCOI;
var D1CCCO;
var N1LA = 0;
var N111a = 0;
var lCCTxt = "";

function performCoorporate() {
    IsCoorporate = 1;
    //	$("#BtnQuickTextForm").css("visibility", "hidden");
    //	$(".spanQuickTxt").css("visibility", "hidden");
}
var IsInputSelected = false;
$('input, textarea, select').focus(function () {
    IsInputSelected = true;
}).blur(function () {
    IsInputSelected = false;
});
function k4() {

    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();

    if (D1AG) {
        //        var l = D1AG.left - D1AG.getWidth() / 2;
        //        var t = D1AG.top - D1AG.getHeight() / 2;
        //        l = Math.round(l);
        //        t = Math.round(t);
        //        var w;
        //        var h;
        //        w = Math.round(D1AG.getWidth());
        //        h = Math.round(D1AG.getHeight());
        //        $("#inputPositionX").val(l);
        //        $("#inputPositionY").val(t);
        //        $("#inputObjectWidth").val(w);
        //        $("#inputObjectHeight").val(h);

        //        $( "#inputPositionX" ).spinner( "option", "disabled", true );
        //        $( "#inputPositionY" ).spinner( "option", "disabled", true );
        //        $( "#inputObjectWidth" ).spinner( "option", "disabled", true );
        //        $( "#inputObjectHeight" ).spinner( "option", "disabled", true );

    } else if (D1AO && D1AO.IsPositionLocked == false) {
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        var w;
        var h;
        
        if (D1AO.type === 'text' || D1AO.type === 'i-text') {
            w = Math.round(D1AO.getWidth());
            h = Math.round(D1AO.getHeight());
            console.log(dfZ1l);
            $("#inputObjectWidthTxt").val(w / dfZ1l);
            $("#inputObjectHeightTxt").val(h / dfZ1l);
            $("#inputPositionXTxt").val(l / dfZ1l);
            $("#inputPositionYTxt").val(t / dfZ1l);
        } else {
            // animatedcollapse.show('divPositioningPanel');
            w = Math.round(D1AO.getWidth());
            h = Math.round(D1AO.getHeight());
            o = D1AO.getOpacity() * 100;
            $("#inputObjectWidth").val(w / dfZ1l);
            $("#inputObjectHeight").val(h / dfZ1l);
            $("#inputObjectAlpha").val(o);
            $(".transparencySlider").slider("option", "value", o);
            $("#inputPositionX").val(l / dfZ1l);
            $("#inputPositionY").val(t / dfZ1l);
        }
        if (D1AO.IsTextEditable != true) {
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

}

function l2(event) {
    if (event.keyCode == ctrlKey) D1CD = false;

    if (event.keyCode == D1SK) D1SD = false


    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();

    if (D1AG && IsInputSelected == false) {

        // c15 
        if (event.keyCode == 38) {
            //  up      
            if (D1SD)
                D1AG.top -= 1;
            else
                D1AG.top -= 5;
        }
        else if (event.keyCode == 37) {
            // left      
            if (D1SD)
                D1AG.left -= 1;
            else
                D1AG.left -= 5;
        } else if (event.keyCode == 39) {
            //  right      
            if (D1SD)
                D1AG.left += 1;
            else
                D1AG.left += 5;
        }
        else if (event.keyCode == 40) {
            // bottom      
            if (D1SD)
                D1AG.top += 1;
            else
                D1AG.top += 5;
        }
        canvas.renderAll();
        if (D1SD == false) {
            if (event.keyCode == 38 || event.keyCode == 37 || event.keyCode == 39 || event.keyCode == 40) {
                var objectsInGroup = D1AG.getObjects();
                objectsInGroup.forEach(function (OPT) {
                    var clonedItem = fabric.util.object.clone(OPT);
                    clonedItem.left += D1AG.left;
                    clonedItem.top += D1AG.top;
                   // c2(clonedItem);
                    //alert(clonedItem.top);
                });
            }
        }
    }
    else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        // c16
        if (D1AO.isEditing == true) {
            D1AO.onKeyDown(event);
        } else {
            if (event.keyCode == 38) {
                //  up   
                if (D1SD)
                    D1AO.top -= 1;
                else
                    D1AO.top -= 5;
               // c2(D1AO);
                canvas.renderAll();
            }
            else if (event.keyCode == 37) {
                // left  
                if (D1SD)
                    D1AO.left -= 1;
                else
                    D1AO.left -= 5;
               // c2(D1AO);

                canvas.renderAll();
            } else if (event.keyCode == 39) {
                // right   
                if (D1SD)
                    D1AO.left += 1;
                else
                    D1AO.left += 5;

              //  c2(D1AO);

                canvas.renderAll();
            }
            else if (event.keyCode == 40) {
                //bottom   
                if (D1SD)
                    D1AO.top += 1;
                else
                    D1AO.top += 5;

              //  c2(D1AO);

                canvas.renderAll();
            }
        }
    }

    if (event.keyCode == 46 || event.keyCode == 8) {
       
        if (N1LA != 1 && IsInputSelected == false) {
            var D1AO = canvas.getActiveObject();
            var D1AG = canvas.getActiveGroup();
            if (D1AG) {
                if (confirm("Are you sure you want to Remove this Group from the canvas.")) {
                    var objectsInGroup = D1AG.getObjects();
                    canvas.discardActiveGroup();
                    objectsInGroup.forEach(function (OPT) {
                        // c2(OPT, 'delete');
                        c2_del(OPT);
                        canvas.remove(OPT);
                    });

                }
            } else if (D1AO) {
                
                if (confirm("Are you sure you want to Remove this Object from the canvas.")) {
                    //  c2(D1AO, 'delete');
                    c2_del(D1AO);
                    canvas.remove(D1AO);
                }
            }
        }
    }
}
function l3(e) {
    if (e.keyCode == ctrlKey) D1CD = true;
    if (e.keyCode == D1SK) D1SD = true;
    if (e.keyCode >= 37 && e.keyCode <= 40 && IsInputSelected == false) {
        return false

    }
    var sObj = canvas.getActiveObject();
    if (!sObj) {
        if (e.keyCode == 8 && IsInputSelected == false) {
            if (IsDesignModified) {
                if (!confirm("You have unsaved changes. Do you want to leave without saving changes ?")) {
                    return false;
                }
            }
        }
    }

    if (D1SD && (e.keyCode == D1SK)) {
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            var lockedObjectFound = false;
            var objectsInGroup = D1AG.getObjects();
            $.each(objectsInGroup, function (j, Obj) {
                if (Obj.IsPositionLocked == true) {
                    lockedObjectFound = true;
                }
            });
            if (!lockedObjectFound) {
                pcL13();
                pcL36('hide', '#textPropertPanel ,#DivAdvanceColorPanel , #DivColorPallet , #DivColorPallet , #ShapePropertyPanel ,#ImagePropertyPanel , #UploadImage , #quickText , #addImage , #addText');               
                k4();
                pcL36('show',"#DivAlignObjs");
            } else {
              
                pcL13();
                pcL36('hide',"#DivAlignObjs");
            }
        }
    }
    if (D1CD && (e.keyCode == cKey)) {
        if (N1LA != 1) {
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
    }
    else if (D1CD && (e.keyCode == vKey) && IsInputSelected == false) //paste
    {
       
        if (N1LA != 1) {
            var OOID;
            // e0(); // l3
            if (D1CO.length != 0) {
                for (var i = 0; i < D1CO.length; i++) {
                    var TG = fabric.util.object.clone(D1CO[i]);
                    OOID = TG.ProductPageId;
                    TG.ObjectID = --NCI;
                    TG.ProductPageId = SP;
                    TG.$id = (parseInt(TO[TO.length - 1].$id) + 4);
                    // c14
                    if (OOID == SP) {
                        TG.PositionX -= 15;
                        TG.PositionY += 15;
                    }
                    if (TG.EntityKey != null) {
                        delete TG.EntityKey;
                    }
                    TO.push(TG);
                    if (TG.ObjectType == 2) {
                        c0(canvas, TG);
                    }
                    else if (TG.ObjectType == 3) {
                        d1(canvas, TG);
                    }
                    else if (TG.ObjectType == 6) {
                        c9(canvas, TG);
                    }
                    else if (TG.ObjectType == 7) {
                        c8(canvas, TG);
                    }
                    else if (TG.ObjectType == 9) {
                        d4(canvas, TG);
                    }
                    canvas.renderAll();
                }
            }
        }
        //if(sObj) {
        //    if (sObj.type == "text" || sObj.type == "i-text") {
        //        console.log(sObj.isEditing);
        //        if (sObj.isEditing) {
        //        //    $("#getCopied").focus();
        //        }
        //    }
        //}
    }
}

$('#inputSearchGImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k22();
    }
});
$('#inputSearchGbkg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k22Bk();
    }
});
$('#inputSearchTImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k19();
    }
});
$('#inputSearchTImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k19Bk();
    }
});
$('#inputSearchPImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k25();
    }
});
$('#inputSearchPImg').bind('keyup', function (e) {
    if (e.keyCode === 13) {
        k25Bk();
    }
});
$("#BtnSave").click(function (event) {
    save("save");
});
$("#btnResetTemplate").click(function (event) {
    if (confirm("Are you sure you want to restore template to its original state ?")) {
        TO = [];
        TP = [];
        $.each(TORestore, function (i, IT) {
            var obj = fabric.util.object.clone(IT);
            TO.push(obj);
        });
        $.each(TPRestore, function (i, IT) {
            var obj = fabric.util.object.clone(IT);
            TP.push(obj);
        });
        canvas.clear();
        d5(SP);
    }

});

$("#btnMoveObjLeft").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();

    if (D1AG && IsInputSelected == false) {
        //            if (D1SD)
        //                D1AG.left -= 1;
        //            else
        //                D1AG.left -= 5;
        //        canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1SD)
            D1AO.left -= 1;
        else
            D1AO.left -= 5;
     //   c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }
});
$("#btnMoveObjUp").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && IsInputSelected == false) {
        //                if (D1SD)
        //                    D1AG.top -= 1;
        //                else
        //                    D1AG.top -= 5;
        //                canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1SD)
            D1AO.top -= 1;
        else
            D1AO.top -= 5;
        //c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }
});
$("#btnMoveObjDown").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && IsInputSelected == false) {
        //                if (D1SD)
        //                    D1AG.top += 1;
        //                else
        //                    D1AG.top += 5;
        //            canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1SD)
            D1AO.top += 1;
        else
            D1AO.top += 5;
        //c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }

});
$("#btnMoveObjRight").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && IsInputSelected == false) {
        //                if (D1SD)
        //                    D1AG.left += 1;
        //                else
        //            canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {

        if (D1SD)
            D1AO.left += 1;
        else
            D1AO.left += 5;

       // c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }

});

$("#btnMoveObjLeftTxt").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();

    if (D1AG && IsInputSelected == false) {
        //            if (D1SD)
        //                D1AG.left -= 1;
        //            else
        //                D1AG.left -= 5;
        //        canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1SD)
            D1AO.left -= 1;
        else
            D1AO.left -= 5;
      //  c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }
});
$("#btnMoveObjUpTxt").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && IsInputSelected == false) {
        //                if (D1SD)
        //                    D1AG.top -= 1;
        //                else
        //                    D1AG.top -= 5;
        //                canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1SD)
            D1AO.top -= 1;
        else
            D1AO.top -= 5;
     //   c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }
});
$("#btnMoveObjDownTxt").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && IsInputSelected == false) {
        //                if (D1SD)
        //                    D1AG.top += 1;
        //                else
        //                    D1AG.top += 5;
        //            canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {
        if (D1SD)
            D1AO.top += 1;
        else
            D1AO.top += 5;
     //   c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }

});
$("#btnMoveObjRightTxt").click(function (event) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG && IsInputSelected == false) {
        //                if (D1SD)
        //                    D1AG.left += 1;
        //                else
        //            canvas.renderAll();
    } else if (D1AO && D1AO.IsPositionLocked == false && IsInputSelected == false) {

        if (D1SD)
            D1AO.left += 1;
        else
            D1AO.left += 5;

     //   c2(D1AO);
        canvas.renderAll();
        var l = D1AO.left - D1AO.getWidth() / 2;
        var t = D1AO.top - D1AO.getHeight() / 2;
        l = Math.round(l);
        t = Math.round(t);
        $("#inputPositionX").val(l);
        $("#inputPositionY").val(t);
        $("#inputPositionXTxt").val(l);
        $("#inputPositionYTxt").val(t);
    }

});
$("#BtnPreview").click(function (event) {
   // e0();
    save("preview");
});
$("#btnNextStepBC").click(function (event) {
    //    if (IsBCFront == true && TP.length > 1) {
    //        $("#btnNextStepBC").text("Preview");
    //        IsBCFront = false;
    //        // save("save");
    //        BCBackSide = 1;
    //        var left = 0;
    //        var ToAdd = 0;
    //        if (TP[1].Orientation == 1) {
    //            left = ($(window).width() / 2 - Template.PDFTemplateWidth / 2);
    //            ToAdd = Template.PDFTemplateWidth + 50;
    //        } else {
    //            left = ($(window).width() / 2 - Template.PDFTemplateHeight / 2);
    //            ToAdd = Template.PDFTemplateHeight + 50;

    //        }
    //        $('.bcCarouselImages  div').each(function (i) {
    //            $(this).css("border", "none");
    //        });
    //        $(".bcCarouselImages").css("left", "458px");
    //        $('.bcBackImgs  img').each(function (i) {
    //            $(this).css("left", left + "px");
    //            $(this).css("visibility", "visible");
    //            left += ToAdd;
    //        });
    //        if (Template.TemplateType == 1) {
    //            $(".titleBC").html("Multi backs");
    //            $('.btnToggleSelectBack').css("visibility", "visible");
    //            $('.lblBCremoveSide').css("visibility", "visible");
    //            $(".bcBackImgs").css("display", "block");
    //            $(".btnBCcarouselPrevious").css("visibility", "hidden");
    //            $(".divBCCarousel").css("display", "block");
    //        } else {
    //            $(".titleBC").html("Back");
    //        }

    //        k1(TP[1].ProductPageID, true);
    //    } else {
    if (IsEmbedded == true) {
        parent.Next();
    } else {
      //  e0();
        save("preview");
        //        }
    }
});
$("#btnToggleSelectBack").click(function (event) {
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == SP) {
            if (IT.IsPrintable == false) {
                IT.IsPrintable = true;
                $('.btnToggleSelectBack').attr("title", "Remove this design");
                $('.btnToggleSelectBack').css("background", 'url("assets/BcPageNotPrintable.png")');
                $('.lblBCremoveSide').html("Remove this design");
            } else {
                IT.IsPrintable = false;
                $('.btnToggleSelectBack').attr("title", "Enable this design");
                $('.btnToggleSelectBack').css("background", 'url("assets/BcPagePrintable.png")');
                $('.lblBCremoveSide').html("Enable this design");
            }
        }
    });
});
$("#BtnPreviewNew").click(function (event) {
    save("preview");
});

$("#BtnContinue").click(function (event) {
    save("continue");
});
$("#clearBackground").click(function (event) {
    StartLoader("Loading content please wait...");
    canvas.backgroundColor = "#ffffff";
    canvas.setBackgroundImage(null, function (IOL) { });
    canvas.renderAll();// StopLoader();

    $.each(TP, function (op, IT) {
        if (IT.ProductPageID == SP) {
            if (Template.isCreatedManual == false) {
                //IT.BackgroundFileName = TemplateID + "//" + "Side" + IT.PageNo + ".pdf"; // background pdf uploaded 
                IT.BackgroundFileName = "Designer/Products/" + TemplateID + "//" + "templatImgBk" + IT.PageNo + ".jpg";
                canvas.setBackgroundImage(IT.BackgroundFileName, canvas.renderAll.bind(canvas), {
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
        
            } else {
                IT.BackgroundFileName = TemplateID + "//" + "Side" + IT.PageNo + ".pdf";// IT.BackgroundFileName = TemplateID + "//" + IT.PageName + IT.PageNo + ".pdf";
            }
            IT.BackGroundType = 1;
            return;
        }
    });
});
function setActiveStyle(styleName, value, c, m, y, k) {
    object = canvas.getActiveObject();
    if (!object) return;
    if (object.setSelectionStyles && object.isEditing) {
        var style = {};
        style[styleName] = value;
        if (styleName == "color" && (parseInt(c) || parseInt(c)==0)) {
            style['textCMYK'] = c + " " + m + " " + y + " " + k;
        }
        object.setSelectionStyles(style);
        object.setCoords();
    }
    else {
        if (styleName == "color") {
            object.customStyles = [];
            styleName = "fill";
            object.setColor(value);
            object.C = c;
            object.M = m;
            object.Y = y;
            object.K = k;
        } else if (styleName == "font-Size") {
            styleName = "fontSize";
            object.fontSize = value;
        } else if (styleName == "font-Weight") {
            styleName = "fontWeight";
            if (object.fontWeight == 'bold') {
                value = 'normal';
            }
            else {
                value = 'bold';
            }
            object.set('fontWeight', value);

        } else if (styleName == "font-Style") {
            styleName = "fontStyle";
            if (object.fontStyle == 'italic') {
                value = 'normal';
            }
            else {
                value = 'italic';
            }
            object[styleName] = value;
        }

    }

    object.setCoords();
    canvas.renderAll();
};
function setActiveProp(name, value) {
    var object = canvas.getActiveObject();
    if (!object) return;

    object.set(name, value).setCoords();
    canvas.renderAll();
};
//function l1() {
//    var fontSize = $("#BtnFontSize").val();

//    setActiveStyle("fontSize", fontSize);
//}
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
var listToPass = [];
function save_rrs_se_se(obj) {
    $.each(TO, function (j, item) {
        if (item.ContentString.indexOf(obj.VariableTag) != -1) {
            listToPass.push(obj);
            return true;
        }
    });
    return false;
}
function save_rs_se(varlist) {
    listToPass = [];
    $.each(varlist, function (j, obj) {
        save_rrs_se_se(obj);
    });
    return listToPass;
}
function save_rs() {
    var to = "../services/Webstore.svc/saveTemplateVariables";
    var dList = save_rs_se(varList);
    var jsonObjects = JSON.stringify(dList, null, 2);
    var options = {
        type: "POST",
        url: to,
        data: jsonObjects,
        contentType: "text/plain;",
        dataType: "json",
        async: true,
        complete: function (httpresp, returnstatus) {
            if (httpresp.responseText != '"done"') {
                alert("error while saving template variables.");
                console.log(httprespr.responseText);
            }
        }
    };
    var returnText = $.ajax(options).responseText;
}
function save(mode, title) {
   // e0();
    c2_v2();
    c2_v2();
    var dheight = $(window).height();
    dheight = dheight - 50;
    $("#loadingMsg").html("Saving and Generating Preview..");
    $(".firstLoadingMsg").attr("src", 'assets/ImgMessageSave.JPG');
    $(".firstLoadingMsg").css("display", "none");
    StartLoader("Downloading image to your design, please wait....");
    var TPOs = [];
    TPOs = TP;
    var it2 = 20000;
    var it3 = 25000;
    var it4 = 5000;
    var it5 = 10000;
    $.each(TO, function (i, item) {
        item.$id = it4;
        it4++;
        if (item.EntityKey) {
            item.EntityKey.$id = it5;
            it5++;
        }
    });
    $.each(TPOs, function (i, IT) {
        IT.$id = it2;
        it2++;
        IT.EntityKey.$id = it3;
        it3++;
        if (IT.BackgroundFileName.indexOf('Designer/Products/') != -1) {
            var p = IT.BackgroundFileName.split('Designer/Products/');
            IT.BackgroundFileName = p[p.length - 1];
        }
    });
    //	if (IsBC || !ShowBleedArea) { // blead area fix
    //		var it = 1000;
    //		$.each(TO, function (i, IT) {
    //			IT.$id = it;
    //			it++;
    //			IT.PositionX += Template.CuttingMargin;
    //			IT.PositionY += Template.CuttingMargin;
    //		});
    //	}
    // saving variables 
    if (IsCalledFrom == 2) {
        save_rs();
    }
    //saving the objects first
    var obSt = {
        printCropMarks: printCropMarks,
        printWaterMarks: printWaterMarks,
        objects: TO,
        orderCode: orderCode,
        CustomerName: CustomerName,
        objPages: TPOs,
        isRoundCornerrs: IsBCRoundCorners,
        isMultiPageProduct: isMultiPageProduct
    }
    var jsonObjects = JSON.stringify(obSt, null, 2);
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
                    d8(mode, dheight, title);
                }
                else {
                    StopLoader();
                    alert(httpresp.responseText);
                }
            }
            //			if (IsBC) {// blead area fix
            //				$.each(TO, function (i, IT) {
            //					IT.PositionX -= Template.CuttingMargin;
            //					IT.PositionY -= Template.CuttingMargin;
            //				});
            //			}
        }
    };
    var returnText = $.ajax(options).responseText;
}
function g7() {
    var OBS = canvas.getObjects();
    for (i = 0; i < TO.length; i++) {
        OBS.filter(function (obj) {
            if (obj.get('ObjectID') == TO[i].ObjectID) {
                TO[i].DisplayOrderPdf = OBS.indexOf(obj);
            }
        });
    }
}

function d8(mode, dheight, title) {
    IsDesignModified = false;
    if (mode == "preview") {
        var ra = fabric.util.getRandomInt(1, 1000);
        $('.frame  img').each(function (i) {
            var s = $(this).attr('src');
            var p = s.split("?");
            var i = p[0];
            i += '?r=' + ra;
            $(this).attr('src', i);
        });
        if (IsBC) {
            $('.thumb').each(function (i) {
                $(this).css('display', 'block');
            });

            $.each(TP, function (i, IT) {
                if (IT.IsPrintable == false) {
                    $('#thumbPage' + IT.ProductPageID).css('display', 'none');
                }
            });
            //            if (IsEmbedded == true) {
            //                parent.hideHeader();
            //            }
        }
        $('#slider  img').each(function (i) {
            var s = $(this).attr('src');
            var p = s.split("?");
            var i = p[0];
            i += '?r=' + ra;
            $(this).attr('src', i);
        });
        if ($('.mcSlc') != undefined) {
            var s = $('.mcSlc').css('background-image');
            if (s != undefined) {
                var p = s.split("?");
                var temp = p[0].split("http://");
                var i = 'url("http://' + temp[1];
                i += '?r=' + ra + '")';
                $('.mcSlc').css('background-image', i);

            }
        }

        if ($('#slider') != undefined) {
            var s = $('#slider').css('background-image');
            if (s != undefined) {
                var p = s.split("?");
                //var i = p[0];
                if (s.indexOf("asset") == -1) {
                    var temp = p[0].split("http://");
                    var i = 'url("http://' + temp[1];
                    i += '?r=' + ra + '")';
                    $('#slider').css('background-image', i);
                }
            }
        }
        $.each(TP, function (i, IT) {
            d8_chk(IT.ProductPageID);

        });
        
        StopLoader();
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            $(".previewerTitle").html('  <span class="lightGray">Proof :</span> " ' + title + ' "');
        } else {
            $(".previewerTitle").html('  <span class="lightGray">Preview :</span> " ' + Template.ProductName + ' "');
        }
        $('.opaqueLayer').css("display", "block");
        $('.opaqueLayer').css("background-color", "#333537");
        pcL36('show',"#PreviewerContainer");


        $("#loadingMsg").html("Saving Content, Please wait..");
    }
    else if (mode == "continue") {
        parent.SaveAttachments();
        //StopLoader();
        //$("#loadingMsg").html("Saving Content, Please wait..");
    }
    else if (returnText != '"true"') {
        alert("error z : " + returnText);
        StopLoader();
        $("#loadingMsg").html("Saving Content, Please wait..");
    }




}
function d8_chk(Pid) {
    $(".overlayLayer" + Pid).css("display", "none");
    $("#overlayLayer" + Pid).css("display", "none");
    $.each(TO, function (i, IT) {
        if (IT.IsOverlayObject == true && IT.ProductPageId == Pid) {
         //   $(".overlayLayer" + Pid).css("visibility", "visible");
            $("#overlayLayer" + Pid).css("display", "block");
            $("#overlayLayer" + Pid).css("visibility", "visible");
        }

    });

}

function d9(c_name, value, exdays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + exdays);
    var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
    document.cookie = c_name + "=" + c_value;
}
function d0(c_name) {
    var i, x, y, ARRcookies = document.cookie.split(";");
    for (i = 0; i < ARRcookies.length; i++) {
        x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
        y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
        x = x.replace(/^\s+|\s+$/g, "");
        if (x == c_name) {
            return unescape(y);
        }
    }
}
function e1(D1CPN, D1LPN) {
    if (D1LPN === 'text' || D1LPN === 'i-text') {
        D1LPN = "textPropertPanel";
    } else if (D1LPN === 'image' || D1LPN === 'path' || D1LPN === 'rect' || D1LPN === 'ellipse') {
        D1LPN = "ImagePropertyPanel";
    } else if (D1LPN === 'group') {
        D1LPN = "DivAlignObjs";
    }

    if (D1LPN != "DivAlignObjs") {
        var D1LPL = $("#" + D1LPN).css("left");
        var D1LPT = $("#" + D1LPN).css("top");
        if (D1LPN != undefined) {
            $("#" + D1CPN).css("left", D1LPL);
            $("#" + D1CPN).css("top", D1LPT);
        }
    }
}

window.onbeforeunload = function () {
    if (IsDesignModified) {
        //window.confirm("You have unsaved changes. Do you want to leave without saving changes ?");
        return "You have unsaved changes. Do you want to leave without saving changes ?";
    }
};

function h3() {
    if (IsEmbedded && IsCalledFrom == 4) {
        if (Template.IsCorporateEditable == false) {
            $("#btnNewTxtPanel").css("display", "none");
            $("#btnImgPanel").css("display", "none");
            $("#btnBkImgPanel").css("display", "none");
            $(".lblAddImgBtn").css("display", "none");
            $("#btnAddRectangle").css("display", "none");
            $("#btnAddCircle").css("display", "none");
            $("#SpanAdd").css("display", "none");

            $("#BtnCopyObj").css("display", "none");
            $("#BtnPasteObj").css("display", "none");
            $(".SpanCopyNew").css("display", "none");
            $(".SpanPasteNew").css("display", "none");
            $(".SpanDragShapes").css("display", "none");
            N1LA = 1;
            $("#divShapesPanelCaller").css("visibility", "hidden");
            $("#divLayersPanelCaller").css("visibility", "hidden");
            $(".SpanLayers").css("visibility", "hidden");
            $(".RetailMenuDiv").css("display", "none");
            $("#BtnImageArrangeOrdr1").css("display", "none");
            $("#BtnImageArrangeOrdr2").css("display", "none");
            $("#BtnImageArrangeOrdr3").css("display", "none");
            $("#BtnImageArrangeOrdr4").css("display", "none");
            $("#BtnTxtarrangeOrder1").css("display", "none");
            $("#BtnTxtarrangeOrder2").css("display", "none");
            $("#BtnTxtarrangeOrder3").css("display", "none");
            $("#BtnTxtarrangeOrder4").css("display", "none");
            $("#DivControlPanel1").css("width", "469px");
            $(".lblCopyBtn").css("display", "none");
            $(".lblPasteBtn").css("display", "none");
            $(".lblAddImgBtn").css("visibility", "hidden");
            $(".lblDragRectBtn").css("visibility", "hidden");
            $(".lblAddImgBtn").css("visibility", "hidden");
            $(".lblLayersBtn").css("visibility", "hidden");

        }
        $("#btnAddImagePlaceHolder").css("visibility", "hidden");
        $(".spanImgPlaceHolder").css("visibility", "hidden");
    }
}

function e2() {
    if (d0("IsTipEnabled") == "0") {
        $("#ShowTips").click();
    }
}
function e3() {
    //  debugger;
    if (D1CS < 2.9) {
        D1CZL += 1;
        dfZ1l = Math.pow(D1SF, D1CZL)
        D1CS = D1CS * D1SF;
        canvas.setHeight(canvas.getHeight() * D1SF);
        canvas.setWidth(canvas.getWidth() * D1SF);
        var OBS = canvas.getObjects();
        for (var i in OBS) {
            var scaleX = OBS[i].scaleX;
            var scaleY = OBS[i].scaleY;
            var left = OBS[i].left;
            var top = OBS[i].top;

            var tempScaleX = scaleX * D1SF;
            var tempScaleY = scaleY * D1SF;
            var tempLeft = left * D1SF;
            var tempTop = top * D1SF;
            OBS[i].scaleX = tempScaleX;
            OBS[i].scaleY = tempScaleY;
            OBS[i].left = tempLeft;
            OBS[i].top = tempTop;

            OBS[i].setCoords();
          
        }
    }
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
    $(".spanZoomContainer").html("Zoom Level - " + D1CS * 100 + " % ");
    $(".zoomToolBar").html(" Zoom " + Math.floor( D1CS * 100) + " % ");
  
}
function g0(left, top, IsQT, QTName, QTSequence, QTWatermark) {
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
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.LineSpacing = 1.4;
    D1NTO.CharSpacing = 0;
    D1NTO.ProductPageId = SP;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    var text = $('#txtAddNewText').val();
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


    D1NTO.FontSize = 12;

    if (IsQT == true) {
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = QTSequence;
        D1NTO.Name = QTName;
        D1NTO.watermarkText = $('#txtQWaterMark').val();
    } else {
        D1NTO.IsQuickText = false;
    }
    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    //D1NTO.IsSpotColor = false;
    canvas.renderAll();
    $('#txtAddNewText').val("");
    $('#txtQTitleChk').val("");
    $('#txtQWaterMark').val("");
    $('#TxtQSequence').val("");
    $("#IsQuickTxtCHK").prop('checked', false);
    $("#addText").css("height", "222px");
    $("#QtxtINRow").css("display", "none");
    $(".popUpQuickTextPanel").css("top", "159px");
    pcL36('toggle', '#addText');
    pcL36('toggle', '#quickText');
    TO.push(D1NTO);
}
function h2(left, top) {
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
    NewCircleObejct.ObjectType = 7; //c07
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
    COL.top = top;
    COL.left = left;

    // c08
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
    f1(6);
   // c2(COL);
}
function h1(left, top) {
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
    D1NTO.ObjectType = 6; //c09
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

    var index;
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, IT) {
        if (IT.ObjectID == ROL.ObjectID) {
            index = i;
        }
    });
    D1NTO.DisplayOrderPdf = index;

    ROL.top = top;
    ROL.left = left;
    D1NTO.PositionX = ROL.left - ROL.maxWidth / 2;
    D1NTO.PositionY = ROL.top - ROL.maxHeight / 2;
    ROL.setCoords();

    ROL.C = "0";
    ROL.M = "0";
    ROL.Y = "0";
    ROL.K = "100";
    canvas.renderAll();
    TO.push(D1NTO);
    f1(6);
   // c2(ROL);
}
function e4() {
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
       // e9(IT);
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
        e9(IT);
    });

    D1CS = 1;
    D1CS = 1;
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
    canvas.renderAll();
    $(".spanZoomContainer").html("Zoom Level - " + D1CS * 100 + " % ");
    $(".zoomToolBar").html(" Zoom " + Math.floor(D1CS * 100) + " % ");
}
function e5() {
    if (D1CS > 0.61) {
        D1CZL -= 1;
        D1CS = D1CS / D1SF;
        canvas.setHeight(canvas.getHeight() * (1 / D1SF));
        canvas.setWidth(canvas.getWidth() * (1 / D1SF));
        var OBS = canvas.getObjects();
        for (var i in OBS) {
            var scaleX = OBS[i].scaleX;
            var scaleY = OBS[i].scaleY;
            var left = OBS[i].left;
            var top = OBS[i].top;
            var tempScaleX = scaleX * (1 / D1SF);
            var tempScaleY = scaleY * (1 / D1SF);
            var tempLeft = left * (1 / D1SF);
            var tempTop = top * (1 / D1SF);
            OBS[i].scaleX = tempScaleX;
            OBS[i].scaleY = tempScaleY;
            OBS[i].left = tempLeft;
            OBS[i].top = tempTop;
            OBS[i].setCoords();

        }
        dfZ1l = Math.pow(D1SF, D1CZL);
    }
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
    $(".spanZoomContainer").html("Zoom Level - " + D1CS * 100 + " % ");
    $(".zoomToolBar").html(" Zoom " + Math.floor(D1CS * 100) + " % ");

}
function e6() {
    //    $("#Previewer").turn("destroy");
    //    $("#PreviewerContainer").hide("clip", null, 500);
    //    var shadow = document.getElementById("previewershadow");
    //    shadow.style.display = "none";
    pcL36('hide', '#PreviewerContainer');
    $('.opaqueLayer').css("display", "none");
    $('.opaqueLayer').css("background-color", "transparent");

    if (IsCalledFrom == 3 || IsCalledFrom == 4) {
        parent.ShowTopBars();
    }
}

function j3(oI, Iloc) {
    var BID = "imgLck" + oI;

    var OBS = canvas.getObjects();

    if (Iloc == true) {
        $.each(OBS, function (i, ite) {
            if (ite.ObjectID == oI) {
                ite.IsPositionLocked = false;
                ite.lockMovementX = false;
                ite.lockMovementY = false;
                ite.lockScalingX = false;
                ite.lockScalingY = false;
                ite.lockRotation = false;
              //  c2(ite);
                i7();
                // $("#" + BID).css("background", '../assets/Lock-Unlock-icon.png');
                return false;
            }
        });
    } else {
        $.each(OBS, function (i, ite) {
            if (ite.ObjectID == oI) {
                ite.IsPositionLocked = true;
                ite.lockMovementX = true;
                ite.lockMovementY = true;
                ite.lockScalingX = true;
                ite.lockScalingY = true;
                ite.lockRotation = true;
             //   c2(ite);
                i7();
                // $("#" + BID).css("background", '../assets/button-icon.png');
                return false;
            }
        });
    }
    if (IsCalledFrom == 3) {
        m0();
    }
}
function i7() {
    var OBS = canvas.getObjects();
    var html = '<ul id="sortableLayers">';
    for (var i = OBS.length - 1; i >= 0; i--) {
        //  $.each(OBS, function (i, ite) {
        var ite = OBS[i];
        $.each(TO, function (i, IT) {
            if (ite.ObjectID == IT.ObjectID) {
                var iLock = false;
                if (ite.IsPositionLocked == true) {
                    iLock = true;
                }
                if (ite.type == "image") {

                    html += i9(ite.ObjectID, 'Image Object', ite.type, ite.getSrc(), iLock);
                } else if (ite.type == "text" || ite.type == "i-text") {
                    html += i9(ite.ObjectID, IT.ContentString, ite.type, "./assets/txtObject.png", iLock);
                } else if (ite.type == "ellipse") {
                    html += i9(ite.ObjectID, 'Ellipse Object', ite.type, "./assets/circleObject.png", iLock);
                } else {
                    html += i9(ite.ObjectID, 'Shape Object', ite.type, "./assets/rectObject.png", iLock);
                }

            }
        });
    }
    //});
    html += '</ul>';
    if (IsCalledFrom != 3) {
        $("#LayerObjectsContainer").html(html);

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
    }
}
function i9(oId, oName, OType, iURL, ILock) {
    var html = "";
    if (ILock == true) {
        html = '<li class="ui-state-default"><span class="selectedObjectID">' + oId + '</span>  <img class="layerImg" src="' + iURL + '" alt="Image" onclick="j1(' + oId + ')" /> <span class="spanLyrObjTxtContainer" onclick="j1(' + oId + ')">' + oName + '</span><button class="btnSelectObject" title="Select Object"onclick="j1(' + oId + ')" ></button> <button class="btnLockObject"  id="imgLck' + oId + '" title="Lock/Unlock Object" onclick="j3(' + oId + ',' + true + ')" ></button> <button class="btnDeleteObjLayer" title="Delete Object" onclick="j2(' + oId + ')" ></button></li>';
    } else {
        html = '<li class="ui-state-default"><span class="selectedObjectID">' + oId + '</span>  <img class="layerImg" src="' + iURL + '" alt="Image" onclick="j1(' + oId + ')"/> <span class="spanLyrObjTxtContainer" onclick="j1(' + oId + ')">' + oName + '</span><button  class="btnSelectObject" title="Select Object"onclick="j1(' + oId + ')" ></button> <button class="btnUnlockLockObject"  id="imgLck' + oId + '" title="Lock/Unlock Object"onclick="j3(' + oId + ',' + false + ')" ></button> <button class="btnDeleteObjLayer" title="Delete Object" onclick="j2(' + oId + ')" ></button></li>';

    }
    return html;
}
function i8(oI, oIn) {
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, ite) {
        if (ite.ObjectID == oI) {
            var dif = oIn - N111a;
            if (dif > 0) {
                for (var i = 0; i < dif; i++) {
                    //  canvas.bringForward(ite);
                    canvas.sendBackwards(ite);
                }
            } else {
                dif = dif * -1;
                for (var i = 0; i < dif; i++) {
                    //  canvas.sendBackwards(ite);
                    canvas.bringForward(ite);
                }
            }
            canvas.renderAll();
         //   c2(ite);
            g7();
            return false;
        }
    });
    i7();
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
function j2(oI) {
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, ite) {
        if (ite.ObjectID == oI) {
            if (confirm("Are you sure you want to delete this object ?")) {
                // c2(ite, 'delete');
                c2_del(ite);
                canvas.remove(ite);
                i7();
            }
            return false;
        }
    });
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
    if (d0("IsTipEnabled") == "1") {
        D1ITE = 0;
        d9("IsTipEnabled", "0", 7);
        pcL36('hide', '#DivToolTip');
        $("#btnShowTipsBC").find('span').text("Show Tips");
        //e7(0);
    } else {
        d9("IsTipEnabled", "1", 7);
        D1ITE = 1;
        f1(6);
        //e7(1);
        $("#btnShowTipsBC").find('span').text("Hide Tips");
    }
});
$("#btnShowTipsBC").click(function (event) {
    $("#ShowTips").click();
});
$("#btnDeleteImg").click(function (event) {
    b8(imgSelected, TemplateID);
    if ($("#radioBtnIllustration").prop('checked')) {
        imType = 18;
    }
    if ($("#radioBtnFrames").prop('checked')) {
        imType = 19;
    }
    if ($("#radioBtnBanners").prop('checked')) {
        imType = 20;
    }
});
$("#btnUpdateImgProp").click(function (event) {
    var title = $("#InputImgTitle").val();
    var desc = $("#InputImgDescription").val();
    var keywords = $("#InputImgKeywords").val();
    while (title.indexOf('/') != -1)
        title = title.replace("/", "___");
    while (desc.indexOf('/') != -1)
        desc = desc.replace("/", "___");
    while (keywords.indexOf('/') != -1)
        keywords = keywords.replace("/", "___");
    while (title.indexOf(',') != -1)
        title = title.replace(",", "__");
    while (desc.indexOf(',') != -1)
        desc = desc.replace(",", "__");
    while (keywords.indexOf(',') != -1)
        keywords = keywords.replace(",", "__");

    if (keywords == "") {
        keywords = title;
    }
    if (desc == "") {
        desc = title;
    }
    var imType = 0;
    if (!isBKpnl) {
        if (IsCalledFrom == 3) {
            imType = 1;
        }
        else if (IsCalledFrom == 1) {
            imType = 1;
        } else if (IsCalledFrom == 2) {
            imType = 1;
        }
        if ($("#radioImageLogo").prop('checked')) {
            imType = 14;
            if (IsCalledFrom == 3) {
                imType = 15;
            }
            if (IsCalledFrom == 2) {
                imType = 17;
            }
        }
        if ($("#radioImageShape").prop('checked')) {
            imType = 13;
            if (IsCalledFrom == 2) {
                imType = 16;
            }
        }
        if ($("#radioBtnIllustration").prop('checked')) {
            imType = 18;
        }
        if ($("#radioBtnFrames").prop('checked')) {
            imType = 19;
        }
        if ($("#radioBtnBanners").prop('checked')) {
            imType = 20;
        }
    }
    StartLoader();
    $.getJSON("services/imageSvcDam/" + imgSelected + "," + imType + "," + title + "," + desc + "," + keywords,
	function (DT) {
	    StopLoader();
	    if (IsCalledFrom == 2) {
	        k30(imgSelected);
	        k27();
	    } else {
	        if (IsCalledFrom == 1 ) {
	            if (imType == 14 || imType == 18 ||imType == 19 || imType == 20) {
	                k27();
	            } else if (imType == 13 || imType == 1) {
	                k27();
	            }
	           
                
	        } else if (IsCalledFrom == 3) {
	            if (imType == 15 || imType == 1) {
	                k27();
	            }
	        }
	        pcL36('show', '#divImageDAM');
	    }

	});


});
$("#btnFlipSides").click(function (event) {
    if (IsBCFront == true) {
        // $("#btnNextStepBC").text("Preview");
        IsBCFront = false;
        // save("save");
        BCBackSide = 1;
        var left = 0;
        var ToAdd = 0;
        if (TP[1].Orientation == 1) {
            left = ($(window).width() / 2 - Template.PDFTemplateWidth / 2);
            ToAdd = Template.PDFTemplateWidth + 50;
        } else {
            left = ($(window).width() / 2 - Template.PDFTemplateHeight / 2);
            ToAdd = Template.PDFTemplateHeight + 50;

        }
        if (Template.TemplateType == 1) {
            $('.bcCarouselImages  div').each(function (i) {
                $(this).css("border", "none");
            });
            $(".bcCarouselImages").css("left", "458px");
            $('.bcBackImgs  img').each(function (i) {
                $(this).css("left", left + "px");
                $(this).css("visibility", "visible");
                left += ToAdd;
            });
            $('.btnToggleSelectBack').css("visibility", "visible");
            $('.lblBCremoveSide').css("visibility", "visible");
            $(".bcBackImgs").css("display", "block");
            $(".btnBCcarouselPrevious").css("visibility", "hidden");
            $(".divBCCarousel").css("display", "block");
        }
        if (Template.TemplateType == 1) {
            $(".titleBC").html("Multi backs");
        } else {
            $(".titleBC").html("Back");
        }
        k1(TP[1].ProductPageID, true);
    } else {
        //  $("#btnNextStepBC").text("Side 2");
        IsBCFront = true;
        $(".divBCCarousel").css("display", "none");
        d5(TP[0].ProductPageID);
        $(".bcBackImgs").css("display", "none");
        $('.btnToggleSelectBack').css("visibility", "hidden");
        $('.lblBCremoveSide').css("visibility", "hidden");
        $(".titleBC").html("Front");

    }
    pcL36('hide', '#quickTextFormPanel');
});
$("#btnBCcarouselNext").click(function (event) {

    //    BCBackSide += 1;
    k1(TP[BCBackSide + 1].ProductPageID);

});

$("#btnReplaceImageRetail").click(function (event) {

    $("#BkImgContainer").css("display", "none");
    $("#ImgCarouselDiv").css("display", "block");
    $(".bkPanel").css("display", "none");
    $(".imgPanel").css("display", "");
    $('#uploader_browse').text("Upload Image");
    $('#uploader_browse').css("padding-left", "15px");
    $('#uploader_browse').css("width", "92px"); $('.RsizeDiv').css("margin-left", "29px");
    $('#uploader_browse').css("margin-left", "27px"); $('.RsizeDiv').css("width", "146px");
        $("#btnAddImagePlaceHolder").css("display", "none");
        $(".spanImgPlaceHolder").css("display", "none");
        $("#btnCompanyPlaceHolder").css("display", "none");
        $("#btnContactPersonPlaceHolder").css("display", "none"); $(".placeHolderControls").css("display", "none");
    pcL13();
    pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate , #DivAdvanceColorPanel ,#quickText');
    pcL36('show', '#divImageDAM');
});
$("#btnBCcarouselPrevious").click(function (event) {

    // BCBackSide -= 1;
    k1(TP[BCBackSide - 1].ProductPageID);

});
function e7(status) {
    // c00
    if (status == 1) {
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
function j8_v1(D1AO, imgSrc) {
    $.each(LiImgs, function (i, IT) {
        if (imgSrc.indexOf(IT.BackgroundImageRelativePath) != -1) {
            IW = IT.ImageWidth;
            IH = IT.ImageHeight;
            var he = D1AO.getHeight();
            var wd = D1AO.getWidth();

            var originalWidth = IW;  
            var originalHeight =IH;  
          
            if(wd < he){  
                he = wd * (originalHeight / originalWidth);
               
            }  
            else if (he < wd) {
                wd = (he * (originalWidth / originalHeight));
            }
            D1AO.height = (he);
            D1AO.width = (wd);
            D1AO.maxHeight = (he);
            D1AO.maxWidth = (wd);
            D1AO.scaleX = 1;
            D1AO.scaleY = 1;

            canvas.renderAll();
            return;
        }
    });
}
function j8(src) {
    var D1AO = canvas.getActiveObject();
    if (D1AO.type === 'image') {
        //  if (D1AO.get('IsQuickText')) {

            $.each(TO, function (i, IT) {
                if (IT.ObjectID == D1AO.ObjectID) {
                    IT.ContentString = src;
                    //j8_v1(D1AO,src);
                    D1AO.ImageClippedInfo = null;
                    d5(SP);
                    return;
                }
            });
       // }
    }

}
function e8(OPT) {

    $.each(TO, function (i, IT) {
        if (IT.ObjectID == OPT.ObjectID) {
            IT.PositionX = OPT.left - IT.MaxWidth / 2;
            IT.PositionY = OPT.top - IT.MaxHeight / 2;
            IT.RotationAngle = OPT.getAngle();
            if (OPT.type != "text" && OPT.type != "i-text") {
                IT.MaxWidth = OPT.width * OPT.scaleX;
                IT.MaxHeight = OPT.height * OPT.scaleY;
                if (OPT.type == "ellipse") {
                    IT.CircleRadiusX = OPT.getRadiusX();
                    IT.CircleRadiusY = OPT.getRadiusY();
                    IT.PositionX = OPT.left - OPT.getWidth() / 2;
                    IT.PositionY = OPT.top - OPT.getHeight() / 2;
                }
            }
            else {
                IT.MaxWidth = OPT.maxWidth;
                IT.MaxHeight = OPT.maxHeight;
                IT.LineSpacing = OPT.lineHeight;
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
        }

    });
}

function e9(OPT) {
    $.each(TO, function (i, IT) {
        if (IT.ObjectID == OPT.ObjectID) {

            IT.PositionX = OPT.left - IT.MaxWidth / 2;
            IT.PositionY = OPT.top - IT.MaxHeight / 2;

            if (OPT.type == "text" && OPT.type == "i-text") {
                IT.ContentString = OPT.text;
            }
            IT.RotationAngle = OPT.getAngle();
            if (OPT.type != "text" && OPT.type == "i-text") {
                IT.MaxWidth = OPT.width * OPT.scaleX;
                IT.MaxHeight = OPT.height * OPT.scaleY;
                if (OPT.type == "ellipse") {
                    IT.CircleRadiusX = OPT.getRadiusX();
                    IT.CircleRadiusY = OPT.getRadiusY();
                    IT.PositionX = OPT.left - OPT.getWidth() / 2;
                    IT.PositionY = OPT.top - OPT.getHeight() / 2;
                }
            }

            else {
                IT.MaxWidth = OPT.maxWidth;
                IT.MaxHeight = OPT.maxHeight;
                IT.LineSpacing = OPT.lineHeight;
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
            IT.IsHidden = OPT.IsHidden;
            IT.IsEditable = OPT.IsEditable;
        }
    });

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
    dfZ1l = Math.pow(D1SF, D1CZL);
    $(".spanZoomContainer").html("Zoom Level - " + D1CS * 100 + " % ");
    $(".zoomToolBar").html(" Zoom " + Math.floor(D1CS * 100) + " % ");
}
$("#btnLockImgObj").click(function (event) {
    var sObj = canvas.getActiveObject();
    if (sObj) {
        if (sObj.IsPositionLocked == true) {
            j3(sObj.ObjectID, true);
        } else {
            j3(sObj.ObjectID, false);
        }
    }
});
$("#btnLockTxtObj").click(function (event) {
    var sObj = canvas.getActiveObject();
    if (sObj) {
        if (sObj.IsPositionLocked == true) {
            j3(sObj.ObjectID, true);
        } else {
            j3(sObj.ObjectID, false);
        }
    }
});
$("#BtnCopyObj").click(function (event) {
    pcL10();
});
$("#BtnCopyObjTxtRetail").click(function (event) {
    pcL10();
});
$("#BtnCopyObjImgRetail").click(function (event) {
    pcL10();
});
$("#BtnPasteObj").click(function (event) {
    pcL36('hide', '#DivLayersPanel'); pcL13();
    var OOID;
   // e0(); // l3
    if (D1CO.length != 0) {
        for (var i = 0; i < D1CO.length; i++) {
            var TG = fabric.util.object.clone(D1CO[i]);
            OOID = TG.ProductPageId;
            TG.ObjectID = --NCI;
            TG.ProductPageId = SP;
            TG.$id = (parseInt(TO[TO.length - 1].$id) + 4);
            if (OOID == SP) {
                TG.PositionX -= 15;
                TG.PositionY += 15;
            }
            if (TG.EntityKey != null) {
                delete TG.EntityKey;
            }
            TO.push(TG);
            if (TG.ObjectType == 2) {
                c0(canvas, TG);
            }
            else if (TG.ObjectType == 3) {
                d1(canvas, TG);
            }
            else if (TG.ObjectType == 6) {
                c9(canvas, TG);
            }
            else if (TG.ObjectType == 7) {
                c8(canvas, TG);
            }
            else if (TG.ObjectType == 9) {
                d4(canvas, TG);
            }
            canvas.renderAll();
        }
    }
});

$("#BtnBCPresets").click(function (event) {
    pcL13();
    pcL36('hide', '#quickText');
    if (IsCalledFrom == 1) {
        slLLID = 0;
        $("#dropDownPresets").val(0);
        //    $(".presetEditorControls").css("display", "none");
        pcL36('show', '#divPresetEditor');
    } else {
        l2_temp();
        pcL36('hide', '#divPositioningPanel');
        pcL36('toggle', '#divBCMenuPresets');
    }
});
function l2_temp() {
    var orientation = 2;
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == SP) {
            orientation = IT.Orientation;
            if (orientation == 1) {
                $(".BtnBCPresetOptionsPort").css("display", "none");
            } else {
                $(".BtnBCPresetOptionsLand").css("display", "none");
            }
            return;
        }
    });

}
$("#btnNextProofing").click(function (event) {

    var email1 = $("input[name=userEmail1]").val();
    var email2 = $("input[name=userEmail2]").val();
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (email1 != "") {
        if (!filter.test(email1)) {
            alert("Please enter a valid email address in address 1");
            return false;
        }
    }
    if (email2 != "") {
        if (!filter.test(email2)) {
            alert("Please enter a valid email address in address 2");
            return false;
        }
    }
    $(".loadingLayer").css("z-index", "10000001");
    $(".firstLoadingMsg").css("display", "none");
    if ($("#chkCheckSpelling").is(':checked')) {
        StartLoader();
        parent.email1 = email1;
        parent.email2 = email2;
        parent.IsRoundedCorners = IsBCRoundCorners;
        parent.SaveAttachments();
    } else {
        alert(ssMsg);
    }
});

function f1(tipType) {
    pcL36('hide', '#DivPersonalizeTemplate');
    var tipCookie = d0("IsTipEnabled");
    if (tipCookie != null && tipCookie != "") {
        D1ITE = tipCookie;
    }
    if (D1ITE == "1") {
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
        if (tipType == 1) {
            $("#DivTootTipTitle").text("TIP");
            $("#DivToolTipText").text(tip[0]);
            $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');

        } else if (tipType == 2) {
            if (D1DFT == 1) {
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[0]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
                D1DFT = 0;
            } else {
                var rand = Math.floor((Math.random() * 6) + 1);
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[rand]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
            }

        } else if (tipType == 3) {
            if (D1DFT == 1) {
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[0]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
                D1DFT = 0;
            } else {

                var rand = Math.floor((Math.random() * 9) + 7);
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[rand]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
            }

        } else if (tipType == 4) {
            if (D1DFT == 1) {
                $("#DivTootTipTitle").text("TIP");
                $("#DivToolTipText").text(tip[0]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
                D1DFT = 0;
            } else {

                $("#DivTootTipTitle").text("TIP");
                var rand = Math.floor((Math.random() * 20) + 10);
                $("#DivToolTipText").text(tip[rand]);
                $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
            }
        } else if (tipType == 5) {
            $("#DivTootTipTitle").text("TIP");
            $("#DivToolTipText").text(tip[21]);
            $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
        } else if (tipType == 6) {
            $("#DivTootTipTitle").text("TIP");
            var rand = Math.floor((Math.random() * 21) + 0);
            $("#DivToolTipText").text(tip[rand]);
            $("#DivNextToolTip").html('<div id="BtnNextToolTip" onclick = "f1(6);">Next tip >></div>');
        }
        pcL36('show', '#DivToolTip');
    }
}


function f2(c, m, y, k, ColorHex, Sname) {

    var D1AO = canvas.getActiveObject();
   
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        var objectsInGroup = D1AG.getObjects();
        $.each(objectsInGroup, function (j, Obj) {
            if (Obj.type == 'text' || Obj.type == "i-text") {
                Obj.setColor(ColorHex);
                Obj.C = c;
                Obj.M = m;
                Obj.Y = y;
                Obj.K = k;
                //            $("#txtAreaUpdateTxt").css("color", ColorHex);
                //            var hexStr = Obj.fill;
                //            var hex = parseInt(hexStr.substring(1), 16);
                //            var r = (hex & 0xff0000) >> 16;
                //            var g = (hex & 0x00ff00) >> 8;
                //            var b = hex & 0x0000ff;
                //            var Y = 0.2126 * r + 0.7152 * g + 0.0722 * b;
                //            if (Y > 128) {
                //                $("#txtAreaUpdateTxt").css("background-color", 'black');
                //            } else {
                //                $("#txtAreaUpdateTxt").css("background-color", 'white');
                //            }

            } else if (Obj.type == 'ellipse' || Obj.type == 'rect' || D1AO.type == 'path-group' || D1AO.type == 'path') {
                Obj.set('fill', ColorHex);
                Obj.C = c;
                Obj.M = m;
                Obj.Y = y;
                Obj.K = k;
            }
            //else if (Obj.type == 'path') {
            //    Obj.set('stroke', ColorHex);
            //    Obj.C = c;
            //    Obj.M = m;
            //    Obj.Y = y;
            //    Obj.K = k;
            //} else if (Obj.type == 'path-group') {
            //    D1AO.set('fill', ColorHex);
            //    D1AO.C = c;
            //    D1AO.M = m;
            //    D1AO.Y = y;
            //    D1AO.K = k;
            //    pcL22_Sub(D1AO);
            //}
           // c2(Obj);
            canvas.renderAll();
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                $.each(TO, function (i, IT) {
                    if (IT.ObjectID == Obj.ObjectID) {
                        IT.IsSpotColor = true;
                        IT.SpotColorName = Sname;
                        return;
                    }
                });
            }

        });
    } else if (D1AO) {
        if (D1AO.IsTextEditable) return;
        if (D1AO.type == 'text') {
            D1AO.setColor(ColorHex);
            D1AO.C = c;
            D1AO.M = m;
            D1AO.Y = y;
            D1AO.K = k;
            $("#txtAreaUpdateTxt").css("color", ColorHex);
            var hexStr = D1AO.fill;
            var hex = parseInt(hexStr.substring(1), 16);
            var r = (hex & 0xff0000) >> 16;
            var g = (hex & 0x00ff00) >> 8;
            var b = hex & 0x0000ff;
            var Y = 0.2126 * r + 0.7152 * g + 0.0722 * b;
            if (Y > 128) {
                $("#txtAreaUpdateTxt").css("background-color", 'black');
            } else {
                $("#txtAreaUpdateTxt").css("background-color", 'white');
            }
            pcL22_Sub(D1AO);
        } else if (D1AO.type == 'i-text') {
            setActiveStyle("color", ColorHex, c, m, y, k);
            pcL22_Sub(D1AO);
        } else if (D1AO.type == 'ellipse' || D1AO.type == 'rect' || D1AO.type == 'path-group' || D1AO.type == 'path') {
            D1AO.set('fill', ColorHex);
            D1AO.C = c;
            D1AO.M = m;
            D1AO.Y = y;
            D1AO.K = k;
            pcL22_Sub(D1AO);
        }
        //}  else if (D1AO.type == 'path') {
        //    D1AO.set('stroke', ColorHex);
        //    D1AO.C = c;
        //    D1AO.M = m;
        //    D1AO.Y = y;
        //    D1AO.K = k;
        //}
       // c2(D1AO);
        canvas.renderAll();
        if (IsCalledFrom == 2 || IsCalledFrom == 4) {
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == D1AO.ObjectID) {
                    IT.IsSpotColor = true;
                    IT.SpotColorName = Sname;
                    return;
                }
            });
        }

    } else {
        canvas.backgroundColor = ColorHex;
        canvas.renderAll();
        //$("#canvas").css("background-color", ColorHex);
        $.each(TP, function (i, IT) {
            if (IT.ProductPageID == SP) {
                IT.ColorC = c;
                IT.ColorM = m;
                IT.ColorY = y;
                IT.ColorK = k;
                IT.BackGroundType = 2;
                return;
            }
        });
    }

}

function d1ToCanvas(src, x, y, IW, IH) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    var n = src;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");
    StartLoader();
    if (src.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        }
        $.getJSON("services/imageSvc/DownloadImg/" + n + "," + TemplateID + "," + imgtype,
		function (DT) {
		    // src = DT;
		    StopLoader();
		    D1NIO = fabric.util.object.clone(TO[0]);
		    D1NIO.ObjectID = --NCI;
		    D1NIO.ColorHex = "#000000";
		    D1NIO.IsBold = false;
		    D1NIO.IsItalic = false;
		    D1NIO.ProductPageId = SP;
		    D1NIO.MaxWidth = 100;
		    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
		    D1NIO.PositionX = x;
		    D1NIO.PositionY = y;
		    D1NIO.ObjectType = 3;

		    D1NIO.MaxHeight = IH;
		    D1NIO.Height = IH;
		    D1NIO.MaxWidth = IW;
		    D1NIO.Width = IW;

		    if (IH == 0) {
		        D1NIO.MaxHeight = 50;
		        D1NIO.Height = 50;
		    }
		    else if (IW == 0) {
		        D1NIO.MaxWidth = 50;
		        D1NIO.Width = 50;
		    }
		    D1NIO.ContentString = DT;
		    D1NIO.DisplayOrder = TO.length + 1;
		    d1(canvas, D1NIO);
		    var OBS = canvas.getObjects();

		    D1NIO.DisplayOrderPdf = OBS.length;
		    canvas.renderAll();
		    TO.push(D1NIO);
		    k27();
		    $("#ImgCarouselDiv").tabs("option", "active", 1);
		    $("#BkImgContainer").tabs("option", "active", 1);
		});
    } else {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = x;
        D1NIO.PositionY = y;
        D1NIO.ObjectType = 3;

        D1NIO.MaxHeight = IH;
        D1NIO.Height = IH;
        D1NIO.MaxWidth = IW;
        D1NIO.Width = IW;

        if (IH == 0) {
            D1NIO.MaxHeight = 50;
            D1NIO.Height = 50;
        }
        else if (IW == 0) {
            D1NIO.MaxWidth = 50;
            D1NIO.Width = 50;
        }
        D1NIO.ContentString = src;
        D1NIO.DisplayOrder = TO.length + 1;
        d1(canvas, D1NIO);
        var OBS = canvas.getObjects();

        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
    }

}
function d1SvgToCCC(src, IW, IH) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    var n = src;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");
    StartLoader();
    if (src.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        }
        $.getJSON("services/imageSvc/DownloadImg/" + n + "," + TemplateID + "," + imgtype,
		function (DT) {
		    // src = DT;
		    StopLoader();
		    D1NIO = fabric.util.object.clone(TO[0]);
		    D1NIO.ObjectID = --NCI;
		    D1NIO.ColorHex = "#000000";
		    D1NIO.ColorC = 0;
		    D1NIO.ColorM = 0;
		    D1NIO.ColorY = 0;
		    D1NIO.ColorK = 100;
		    D1NIO.IsBold = false;
		    D1NIO.IsItalic = false;
		    D1NIO.ProductPageId = SP;
		    D1NIO.MaxWidth = 100;
		    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
		    D1NIO.PositionX = 0;
		    D1NIO.PositionY = 0;
		    D1NIO.ObjectType = 9;

		    D1NIO.MaxHeight = 100;
		    D1NIO.Height = 100;
		    D1NIO.MaxWidth = 100;
		    D1NIO.Width = 100;

		    //if (IH == 0) {
		    //    D1NIO.MaxHeight = 50;
		    //    D1NIO.Height = 50;
		    //}
		    //else if (IW == 0) {
		    //    D1NIO.MaxWidth = 50;
		    //    D1NIO.Width = 50;
		    //}
		    D1NIO.ContentString = DT;
		    D1NIO.DisplayOrder = TO.length + 1;
		    d1Svg(canvas, D1NIO, true);
		    var OBS = canvas.getObjects();

		    D1NIO.DisplayOrderPdf = OBS.length;
		    canvas.renderAll();
		    TO.push(D1NIO);
		    k27();
		    $("#ImgCarouselDiv").tabs("option", "active", 1);
		    $("#BkImgContainer").tabs("option", "active", 1);
		});
    } else {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = 0;
        D1NIO.PositionY = 0;
        D1NIO.ObjectType = 9;
        D1NIO.ColorC = 0;
        D1NIO.ColorM = 0;
        D1NIO.ColorY = 0;
        D1NIO.ColorK = 100;
        D1NIO.MaxHeight = 100;
        D1NIO.Height = 100;
        D1NIO.MaxWidth = 100;
        D1NIO.Width = 100;

        //if (IH == 0) {
        //    D1NIO.MaxHeight = 50;
        //    D1NIO.Height = 50;
        //}
        //else if (IW == 0) {
        //    D1NIO.MaxWidth = 50;
        //    D1NIO.Width = 50;
        //}
        D1NIO.ContentString = src;
        D1NIO.DisplayOrder = TO.length + 1;
        d1Svg(canvas, D1NIO, true);
        var OBS = canvas.getObjects();

        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
    }

}
function d1ToCanvasCC(src, IW, IH) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    var n = src;
    while (n.indexOf('/') != -1)
        n = n.replace("/", "___");
    while (n.indexOf(':') != -1)
        n = n.replace(":", "@@");
    while (n.indexOf('%20') != -1)
        n = n.replace("%20", " ");
    while (n.indexOf('./') != -1)
        n = n.replace("./", "");
    StartLoader();
    if (src.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        }
        $.getJSON("services/imageSvc/DownloadImg/" + n + "," + TemplateID + "," + imgtype,
		function (DT) {
		    // src = DT;
		    StopLoader();
		    D1NIO = fabric.util.object.clone(TO[0]);
		    D1NIO.ObjectID = --NCI;
		    D1NIO.ColorHex = "#000000";
		    D1NIO.IsBold = false;
		    D1NIO.IsItalic = false;
		    D1NIO.ProductPageId = SP;
		    D1NIO.MaxWidth = 100;
		    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
		    D1NIO.PositionX = 0;
		    D1NIO.PositionY = 0;
		    D1NIO.ObjectType = 3;

		    D1NIO.MaxHeight = IH;
		    D1NIO.Height = IH;
		    D1NIO.MaxWidth = IW;
		    D1NIO.Width = IW;

		    if (IH == 0) {
		        D1NIO.MaxHeight = 50;
		        D1NIO.Height = 50;
		    }
		    else if (IW == 0) {
		        D1NIO.MaxWidth = 50;
		        D1NIO.Width = 50;
		    }
		    D1NIO.ContentString = DT;
		    D1NIO.DisplayOrder = TO.length + 1;
		    d1(canvas, D1NIO, true);
		    var OBS = canvas.getObjects();

		    D1NIO.DisplayOrderPdf = OBS.length;
		    canvas.renderAll();
		    TO.push(D1NIO);
		    k27();
		    $("#ImgCarouselDiv").tabs("option", "active", 1);
		    $("#BkImgContainer").tabs("option", "active", 1);
		});
    } else {
        D1NIO = fabric.util.object.clone(TO[0]);
        D1NIO.ObjectID = --NCI;
        D1NIO.ColorHex = "#000000";
        D1NIO.IsBold = false;
        D1NIO.IsItalic = false;
        D1NIO.ProductPageId = SP;
        D1NIO.MaxWidth = 100;
        D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        D1NIO.PositionX = 0;
        D1NIO.PositionY = 0;
        D1NIO.ObjectType = 3;

        D1NIO.MaxHeight = IH;
        D1NIO.Height = IH;
        D1NIO.MaxWidth = IW;
        D1NIO.Width = IW;

        if (IH == 0) {
            D1NIO.MaxHeight = 50;
            D1NIO.Height = 50;
        }
        else if (IW == 0) {
            D1NIO.MaxWidth = 50;
            D1NIO.Width = 50;
        }
        D1NIO.ContentString = src;
        D1NIO.DisplayOrder = TO.length + 1;
        d1(canvas, D1NIO, true);
        var OBS = canvas.getObjects();

        D1NIO.DisplayOrderPdf = OBS.length;
        canvas.renderAll();
        TO.push(D1NIO);
    }

}
function d1PlaceHoldToCanvas(x, y) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    D1NIO = fabric.util.object.clone(TO[0]);
    D1NIO.ObjectID = --NCI;
    D1NIO.ColorHex = "#000000";
    D1NIO.IsBold = false;
    D1NIO.IsItalic = false;
    D1NIO.ProductPageId = SP;
    D1NIO.MaxWidth = 100;
    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NIO.PositionX = x;
    D1NIO.PositionY = y;
    D1NIO.ObjectType = 3;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = "./assets/Imageplaceholder.png";
    D1NIO.DisplayOrder = TO.length + 1;
    d1(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}
function d1CompanyLogoToCanvas(x, y) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    D1NIO = fabric.util.object.clone(TO[0]);
    D1NIO.ObjectID = --NCI;
    D1NIO.ColorHex = "#000000";
    D1NIO.IsBold = false;
    D1NIO.IsItalic = false;
    D1NIO.ProductPageId = SP;
    D1NIO.MaxWidth = 100;
    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NIO.PositionX = x;
    D1NIO.PositionY = y;
    D1NIO.ObjectType = 8;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = "./assets/Imageplaceholder_sim.png";
    D1NIO.DisplayOrder = TO.length + 1;
    k31(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}
function d1ContactLogoToCanvas(x, y) {
    var canvasHeight = Math.floor(canvas.height);
    var canvasWidth = Math.floor(canvas.width);
    var D1NIO = {};
    D1NIO = fabric.util.object.clone(TO[0]);
    D1NIO.ObjectID = --NCI;
    D1NIO.ColorHex = "#000000";
    D1NIO.IsBold = false;
    D1NIO.IsItalic = false;
    D1NIO.ProductPageId = SP;
    D1NIO.MaxWidth = 100;
    D1NIO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    D1NIO.PositionX = x;
    D1NIO.PositionY = y;
    D1NIO.ObjectType = 12;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = "./assets/Imageplaceholder_sim.png";
    D1NIO.DisplayOrder = TO.length + 1;
    k31(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}
function f3(DivID, left, top) {
    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    if (DivID == "QuickTxtName") {
        D1NTO.Name = "Name";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 1;
        D1NTO.ContentString = (QTD.Name == null ? "Your Name" : QTD.Name);
    }
    if (DivID == "QuickTxtTitle") {
        D1NTO.Name = "Title";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 2;
        D1NTO.ContentString = QTD.Title == null ? "Your Title" : QTD.Title;
    }
    if (DivID == "QuickTxtCompanyName") {
        D1NTO.Name = "CompanyName";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 3;
        D1NTO.ContentString = (QTD.Company == null ? "Your Company Name" : QTD.Company);
    }
    if (DivID == "QuickTxtCompanyMsg") {
        D1NTO.Name = "CompanyMessage";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 9;
        D1NTO.ContentString = QTD.CompanyMessage == null ? "Your Company Message" : QTD.CompanyMessage;
    }
    if (DivID == "QuickTxtAddress1") {
        D1NTO.Name = "AddressLine1";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 4;
        D1NTO.ContentString = QTD.Address1 == null ? "Address Line 1" : QTD.Address1;
    }
    if (DivID == "QuickTxtTel") {
        D1NTO.Name = "Phone";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 5;
        D1NTO.ContentString = QTD.Telephone == null ? "Telephone / Other" : QTD.Telephone;
    }
    if (DivID == "QuickTxtFax") {
        D1NTO.Name = "Fax";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 6;
        D1NTO.ContentString = QTD.Fax == null ? "Fax / Other" : QTD.Fax;
    }
    if (DivID == "QuickTxtEmail") {
        D1NTO.Name = "Email";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 7;
        D1NTO.ContentString = QTD.Email == null ? "Email address / Other" : QTD.Email;
    }
    if (DivID == "QuickTxtWebsite") {
        D1NTO.Name = "Website";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 8;
        D1NTO.ContentString = QTD.Website == null ? "Website address" : QTD.Website;
    }
    console.log(QTD);
    if (DivID == "QuickTxtMobile") {
        D1NTO.Name = "Mobile";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 10;
        D1NTO.ContentString = QTD.Website == null ? "Mobile number" : QTD.MobileNumber;
    }
    else if (DivID == "QuickTxtTwitter") {
        D1NTO.Name = "Twitter";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 11;
        D1NTO.ContentString = QTD.Website == null ? "Twitter ID" : QTD.TwitterID;
    }
    else if (DivID == "QuickTxtFacebook") {
        D1NTO.Name = "Facebook";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 12;
        D1NTO.ContentString = QTD.Website == null ? "Facebook ID" : QTD.FacebookID;
    }
    else if (DivID == "QuickTxtLinkedIn") {
        D1NTO.Name = "LinkedIn";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 13;
        D1NTO.ContentString = QTD.Website == null ? "LinkedIn ID" : QTD.LinkedInID;
    }
    else if (DivID == "QuickTxtOtherID") {
        D1NTO.Name = "OtherID";
        D1NTO.IsQuickText = true;
        D1NTO.QuickTextOrder = 14;
        D1NTO.ContentString = QTD.Website == null ? "Other ID" : QTD.OtherId;
    }

    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 180;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.CharSpacing = 0;
    D1NTO.LineSpacing = 1;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    canvas.renderAll();
    uiTextObject.setCoords();

}
function g9(DivID, left, top) {
    var N1CH = Math.floor(canvas.height);
    var Te1 = Math.floor(canvas.width);
    if (N1CH < (top + 260)) {
        var dif = (top + 260) - N1CH;
        top = top - dif;
    }
    if (Te1 < (left + 120)) {
        var dif = (left + 120) - Te1;

        left = left - dif;
    }
    if (left < 25) {
        left = 80;
    }
    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Name";
    D1NTO.ContentString = (QTD.Name == null ? "Your Name" : QTD.Name);
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.QuickTextOrder = 1;
    D1NTO.CharSpacing = 0;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Title";
    D1NTO.ContentString = QTD.Title == null ? "Your Title" : QTD.Title;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.QuickTextOrder = 2;
    D1NTO.CharSpacing = 0;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 30;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "CompanyName";
    D1NTO.ContentString = (QTD.Company == null ? "Your Company Name" : QTD.Company);
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 200;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.QuickTextOrder = 3;
    D1NTO.CharSpacing = 0;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);


    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 60;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "CompanyMessage";
    D1NTO.ContentString = QTD.CompanyMessage == null ? "Your Company Message" : QTD.CompanyMessage;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 200;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.QuickTextOrder = 9;
    D1NTO.CharSpacing = 0;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 90;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "AddressLine1";
    D1NTO.ContentString = QTD.Address1 == null ? "Address Line 1" : QTD.Address1;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.QuickTextOrder = 4;
    D1NTO.CharSpacing = 0;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 120;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Phone";
    D1NTO.ContentString = QTD.Telephone == null ? "Telephone / Other" : QTD.Telephone;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    D1NTO.QuickTextOrder = 5;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 150;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Fax";
    D1NTO.ContentString = QTD.Fax == null ? "Fax / Other" : QTD.Fax;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.QuickTextOrder = 6;
    D1NTO.CharSpacing = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 180;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Email";
    D1NTO.ContentString = QTD.Email == null ? "Email address / Other" : QTD.Email;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    D1NTO.QuickTextOrder = 7;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 210;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Website";
    D1NTO.ContentString = QTD.Website == null ? "Website address" : QTD.Website;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    D1NTO.QuickTextOrder = 8;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 240;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    canvas.renderAll();

      /// new fields 

    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Mobile";
    D1NTO.ContentString = QTD.Website == null ? "Mobile number" : QTD.Website;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.QuickTextOrder = 8;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 270;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    canvas.renderAll();


    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Twitter";
    D1NTO.ContentString = QTD.Website == null ? "Twitter ID" : QTD.Website;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.QuickTextOrder = 8;
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 300;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    canvas.renderAll();


    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "Facebook";
    D1NTO.ContentString = QTD.Website == null ? "Facebook ID" : QTD.Website;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    D1NTO.QuickTextOrder = 8;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 330;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    canvas.renderAll();


    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "LinkedIn";
    D1NTO.ContentString = QTD.Website == null ? "LinkedIn ID" : QTD.Website;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    D1NTO.QuickTextOrder = 8;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 360;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    canvas.renderAll();


    var D1NTO = {};
    D1NTO = fabric.util.object.clone(TO[0]);
    D1NTO.Name = "OtherID";
    D1NTO.ContentString = QTD.Website == null ? "Other ID" : QTD.Website;
    D1NTO.ObjectID = --NCI;
    D1NTO.ColorHex = "#000000";
    D1NTO.ColorC = 0;
    D1NTO.ColorM = 0;
    D1NTO.ColorY = 0;
    D1NTO.ColorK = 100;
    D1NTO.IsBold = false;
    D1NTO.IsItalic = false;
    D1NTO.ProductPageId = SP;
    D1NTO.MaxWidth = 100;
    D1NTO.MaxHeight = 30;
    D1NTO.FontSize = 14;
    D1NTO.LineSpacing = 1;
    D1NTO.IsQuickText = true;
    D1NTO.CharSpacing = 0;
    D1NTO.QuickTextOrder = 8;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        D1NTO.IsSpotColor = true;
        D1NTO.SpotColorName = 'Black';
    }
    D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    TO.push(D1NTO);

    var uiTextObject = c0(canvas, D1NTO);
    uiTextObject.left = left;
    uiTextObject.top = top + 390;
    D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
    D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
    uiTextObject.setCoords();
    canvas.renderAll();

}
function j6() {
    $('.QuickTextItemContainer  div').each(function (i) {
        //$(this).find('span').text(++i);
        var QFiel = $(this).find('span.qLabelCorp').text();
        var QTData = $(this).find('input.item-ii').val();
        var QTWaterMark = $(this).find('input.item-i').val();
        $.each(TO, function (j, ite) {
            if (ite.Name == QFiel) {
                ite.Name = QTData;
                ite.watermarkText = QTWaterMark;
                //return false;
            }
        });
    });
    StopLoader();
}
function groupChange(D1AG) {
    D1AG.forEach(function (OPT) {
      //  c2(OPT);
    });
}
function f4() {
    var c = $("#DivColorC").slider("value");
    var m = $("#DivColorM").slider("value");
    var y = $("#DivColorY").slider("value");
    var k = $("#DivColorK").slider("value");
    var hex = getColorHex(c, m, y, k);
    f5(c, m, y, k);
}
function j7(i, n) {

    $.getJSON("services/TemplateSvc/UpdateCorpColor/" + i + "," + n,
		function (DT) {
		    //alert(DT);
		    // var html = "<div class ='ColorPalletCorp' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "></div><div class='ColorPalletCorpName'>" + Sname + "</div>";
		    //$('#DivColorContainer').append(html);
		    if (n == "DeActive") {
		        // var somvar = $("#somediv").html();
		        $("#pallet" + i).clone(true).appendTo('#tabsInActiveColors');
		        $("#textColor" + i).clone(true).appendTo('#tabsInActiveColors');
		        $('#tabsActiveColors #pallet' + i).remove();
		        $('#tabsActiveColors #textColor' + i).remove();
		        $('#btnClr' + i).remove();
		        var html = "<button  id ='btnClr" + i + "' class='btnActiveColor' title='Activate this color' onclick='j7(" + i + ",&quot;Active&quot;);'></button>";
		        $("#pallet" + i).append(html);
		    } else {
		        $("#pallet" + i).clone(true).appendTo('#tabsActiveColors');
		        $("#textColor" + i).clone(true).appendTo('#tabsActiveColors');
		        $('#tabsInActiveColors #pallet' + i).remove();
		        $('#tabsInActiveColors #textColor' + i).remove();
		        $('#btnClr' + i).remove();
		        var html = "<button  id ='btnClr" + i + "' class='btnDeactiveColor' title='Deactivate this color' onclick='j7(" + i + ",&quot;DeActive&quot;);'></button>";
		        $("#pallet" + i).append(html);
		    }
		});


    // alert(n);  // DeActive
    //pallet
    //textColor

}
function f5(c, m, y, k) {
    var Color = getColorHex(c, m, y, k);
    var html = "<label for='ColorPalle' id ='LblCollarPalet'>Click to apply </label><div class ='ColorPallet btnClrPallet' style='background-color:" + Color + "' onclick='f6(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
    $('#LblDivColorC').html(c + "%");
    $('#LblDivColorM').html(m + "%");
    $('#LblDivColorY').html(y + "%");
    $('#LblDivColorK').html(k + "%");
    $('#ColorPickerPalletContainer').html(html);
}
function f6(c, m, y, k, Color) {
    //var PID ="New"+ PCount++;
    var Sname = "";
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        Sname = window.prompt("Enter Spot Color Name Here! (Once a color is created, you cannot change its name or color)", "Spot Color 1");
        if (Sname == null || Sname == "") {
            return false;
        } else {
            $.getJSON("services/TemplateSvc/SaveCorpColor/" + c + "," + m + "," + y + "," + k + "," + Sname + "," + CustomerID,
				function (DT) {
				    var PID = DT;
				    var html = "<div id ='pallet" + PID + "' class ='ColorPalletCorp' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "><button  id ='btnClr" + PID + "' class='btnDeactiveColor' title='Deactivate this color' onclick='j7(" + PID + ",&quot;DeActive&quot;);'></button></div><div  id ='textColor" + PID + "' class='ColorPalletCorpName'>" + Sname + "</div>";
				    $('#tabsActiveColors').append(html);

				});
        }
    } else {

        var size = $("#DivRecentColors > div").size();
        var html = "<div class ='ColorPallet' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
        if (size < 7) {
            $('#DivRecentColors').append(html);
        } else {
            var list = document.getElementById('DivRecentColors');
            list.removeChild(list.childNodes[0]);
            $('#DivRecentColors').append(html);
        }
    }
    f2(c, m, y, k, Color, Sname);
}
function f7(PanelToShow) {
    pcL13();
    switch (PanelToShow) {
        case "addText":
            {
                pcL36('hide', '#UploadImage');
                pcL36('show', '#addText');
                if (IsCalledFrom != 3) {
                    pcL36('show', '#quickText');
                }
                break;
            }
        case "addImage":
            {
                pcL36('hide', '#UploadImage');
                pcL36('hide', '#quickText');
                pcL36('hide', '#addText');
                break;
            }
        case "quickTextFormPanel":
            {
                pcL36('hide', '#addText');
                pcL36('hide', '#quickText');
                pcL36('hide', '#UploadImage');
                pcL36('hide', '#addImage');
                pcL36('show', '#quickTextFormPanel');
                break;
            }
    }
}


function f8(coords) {
    D1CCW = Math.round(coords.w);
    D1CH = Math.round(coords.h);
    D1CCML = Math.round(coords.x);
    D1CCMT = Math.round(coords.y);
    var activeObj = canvas.getActiveObject();
    D1CCOI = activeObj.ObjectID;
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
    });

    function f9() {
        if (ISG1) {
            ISG1 = false;
            $("#BtnGuidesBC").find('span').text("Show Bleed and Trim lines");
        } else {
            ISG1 = true;
            $("#BtnGuidesBC").find('span').text(" Hide Bleed and Trim lines");
        }
        d5(SP);
    }
    $("#BtnGuides").click(function (event) {
        f9();
    });
    $("#BtnGuidesBC").click(function (event) {
        f9();
    });
    $("#BtnCropImg").click(function (event) {
        pcL20();
    });
    $("#BtnCropImg2").click(function (event) {
        //pcL20();
        pcL20_new();
    });
    $("#BtnCropImgRetail").click(function (event) {
        pcL20();
    });
    $("#BtnImgScaleIN").click(function (event) {
        pcL14();
    });

    $("#BtnImgScaleOut").click(function (event) {
        pcL15();

    });
    $("#BtnImgScaleINRetail").click(function (event) {
        pcL14();
    });

    $("#BtnImgScaleOutRetail").click(function (event) {
        pcL15();

    });
    $("#BtnApplyCropImg").click(function (event) {
        l1(1);

    });

    function l1(mode) {
        var D1AO = canvas.getActiveObject();
        var contentString;
        var DisplayOrderObj;
        if (D1AO.ObjectID == D1CCOI) {
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == D1AO.ObjectID) {
                    contentString = IT.ContentString;
                    DisplayOrderObj = IT.DisplayOrderPdf;
                }
            });

            // c11 
            //var spR = contentString.split("Designer/Products/");
            var n = contentString;
            while (n.indexOf('/') != -1)
                n = n.replace("/", "___");
            while (contentString.indexOf('%20') != -1)
                contentString = contentString.replace("%20", " ");

            var contentString2 = contentString;
            while (contentString2.indexOf('./') != -1)
                contentString2 = contentString2.replace("./", "");
            //            var n = contentString.replace("/", "___");
            //            n = n.replace("/", "___");
            //            n = n.replace("/", "___");
            StartLoader();
            $.getJSON("services/imageSvc/CropImg/" + n + "," + D1CCML + "," + D1CCMT + "," + D1CCW + "," + D1CH + "," + TemplateID + "," + mode + "," + D1AO.ObjectID,
			function (DT) {
			    k27();
			    $.each(TO, function (i, IT) {
			        if (IT.ContentString == contentString || IT.ContentString == contentString2) {
			            IT.ContentString = DT;
			        }

			    });
			    $("#CarouselImages").html("");
			    MCL = [];
			    IC = 0;
			    $.getJSON("services/imageSvc/" + TemplateID,
				function (DT) {
				    LiImgs = DT;
				    $.each(DT, function (i, IT) {
				        var obj = {
				            url: "./" + IT.BackgroundImageRelativePath,
				            title: IT.ID,
				            index: TemplateID
				        }
				        MCL.push(obj);
				    });
				    b7();
				    StopLoader();
				});
			    pcL36('toggle', '#DivCropToolContainer');
			    d5(SP);
			    canvas.renderAll();

			});
        }
    }
    $("#BtnApplyCropImgNew").click(function (event) {
        l1(2);
    });

    $("#BtnZoomIn").click(function (event) {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();

        e3();
        canvas.renderAll();

    });
    $("#BtnOrignalZoom").click(function (event) {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        D1CZL = 0;
        e0();
        canvas.renderAll();

    });



    $('#BtnZoomOut').click(function (event) {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        
        e5();

        canvas.renderAll();
    });

    $('#BtnUploadFont').click(function (event) {
        var Tc1 = CustomerID;
        pcL36('hide', '#divPositioningPanel');
        if (IsCalledFrom == 1 || IsCalledFrom == 2) {
            pcL36('toggle', '#DivUploadFont');
            if (d0("IsTipEnabled") == "1") {
                $("#ShowTips").click();
            }
        }
    });

    $('#BtnAdvanceColorPicker').click(function (event) {
        pcL36('toggle', '#DivAdvanceColorPanel');
    });

    $('#BtnChngeClr').click(function (event) {
        pcL36('toggle', '#DivColorPallet');
    });
    $('#AddColorTxtRetail').click(function (event) {
        pcL02();
    });
    $('#AddColorTxtRetailNew').click(function (event) {
        pcL02();
    });
    $('#AddColorImgRetailNew').click(function (event) {
        pcL02();
    });
    $('#btnChngeCanvasColor').click(function (event) {
        pcL36('hide', '#divPositioningPanel');
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        pcL36('toggle', '#DivColorPallet');
    });
    $('#AddColorShape').click(function (event) {
        pcL36('toggle', '#DivColorPallet');
    });
    $('#AddColorShapeRetail').click(function (event) {
        pcL36('toggle', '#DivColorPallet');
    });
    function updateUIRedoUndo() {
     //   var btnUndo = document.getElementById('BtnUndo');
      //  var btnRedo = document.getElementById('BtnRedo');
       // btnUndo.disabled = !undoManager.hasUndo();
       // btnRedo.disabled = !undoManager.hasRedo();
    }
    $('#BtnUndo').click(function (event) {
       // pcL36('hide', '#DivLayersPanel'); pcL13();
        if(canvas.getActiveGroup()) canvas.discardActiveGroup();
        if(canvas.getActiveObject()) canvas.discardActiveObject();
    //    canvas.renderAll();
       // undoManager.undo();
        undo();

    });
    $('#BtnRedo').click(function (event) {
        //pcL13();
        //pcL36('hide', '#DivLayersPanel');
        //canvas.discardActiveGroup();
        //canvas.renderAll();
        //  undoManager.redo();
        redo();

    });

    $('#BtnQuickTextForm').click(function (event) {
        pcL13();
        pcL36('hide', '#DivPersonalizeTemplate , #quickText ,#DivAdvanceColorPanel ,#divPositioningPanel ');
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        j5();
        if (IsEmbedded) {
            $("#quickTextFormPanel").css("width", "306px");//402px
            $(".qLabel").css("float", "left");
            $(".qLabel").css("padding-top", "8px");
            $(".panelQuickTextFormRow INPUT").css("width", "270px");
            $(".panelQuickTextFormRow INPUT").css("-moz-border-radius", "5px");
            $(".panelQuickTextFormRow INPUT").css("-webkit-border-radius", "5px");
            $(".panelQuickTextFormRow INPUT").css("-khtml-border-radius", "5px");
            $(".panelQuickTextFormRow INPUT").css("border-radius", "5px");
        }

    });
    $('#divLayersPanelCaller').click(function (event) {
        pcL13();
        pcL36('hide', '#DivPersonalizeTemplate , #DivAdvanceColorPanel ');
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        i7();
        pcL36('show', '#DivLayersPanel');
    });
    $('#BtnQuickTextSave').click(function (event) {
        StartLoader();
        if (IsEmbedded == false) {
            j6();
        } else {
            c5();
            if (IsCalledFrom == 3) {
                m0();
            }
        }
        pcL36('hide', '#quickTextFormPanel');
    });


    $('#btnNewTxtPanel').click(function (event) {
        pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate ,#DivAdvanceColorPanel');pcL13();
        f7('addText');
        f7('addText');
        $("#txtAddNewText").focus();
    });
    $('#btnNewTxtPanelRetail').click(function (event) {
        pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate ,#DivAdvanceColorPanel'); pcL13();
        f7('addText');
        f7('addText');
        $("#txtAddNewText").focus();
    });
    $('#btnBCRectCorner').click(function (event) {
        $("#canvas").uncorner();
        $(".upper-canvas").uncorner();
        $(".canvas-container").uncorner();
        $(".lower-canvas").uncorner();

        $(".bcCarouselImg").uncorner();
        $(".bcCarouselThumbImgLand").uncorner();
        $(".bcCarouselThumbImgPort").uncorner();
        $(".thumbNailFrame").uncorner();

        $('#btnBCRectCorner').css("background", 'url("assets/sprite.png") -120px -166px');
        $('#btnBCcirularCorner').css("background", 'url("assets/sprite.png") -156px -166px');
        IsBCRoundCorners = false;
    });
    $('#btnBCcirularCorner').click(function (event) {
        if (IsBCAlert) {
            if (confirm("Rounded corners will cost extra price. Are you sure you want to continue? Don't worry you can always change your mind before ordering.")) {

                $("#canvas").corner("30px;");
                $(".upper-canvas").corner("30px;");
                $(".canvas-container").corner("30px;");
                $(".lower-canvas").corner("30px;");

                $(".bcCarouselImg").corner("30px;");
                $(".bcCarouselThumbImgLand").corner("10px;");
                $(".bcCarouselThumbImgPort").corner("10px;");
                $(".thumbNailFrame").corner("10px;");
                $('#btnBCRectCorner').css("background", 'url("assets/sprite.png") -189px -166px');
                $('#btnBCcirularCorner').css("background", 'url("assets/sprite.png") -226px -167px');
                IsBCAlert = false;
            }
        } else {
            $("#canvas").corner("30px;");
            $(".upper-canvas").corner("30px;");
            $(".canvas-container").corner("30px;");
            $(".lower-canvas").corner("30px;");
            $(".bcCarouselImg").corner("30px;");
            $(".bcCarouselThumbImgLand").corner("15px;");
            $(".bcCarouselThumbImgPort").corner("15px;");
            $('#btnBCRectCorner').css("background", 'url("assets/sprite.png") -189px -166px');
            $('#btnBCcirularCorner').css("background", 'url("assets/sprite.png") -226px -167px');
        }
        IsBCRoundCorners = true;
    });
    $('#btnImgPanelRetail').click(function (event) {
        if ($(".toolbarMenuPictures").css("opacity") == "0") {
            $(".toolbarMenuPictures").css("display", "block");
            $(".toolbarMenuPictures").css("opacity", "1");
        } else {
            $(".toolbarMenuPictures").css("display", "none");
            $(".toolbarMenuPictures").css("opacity", "0");
        }
    });
    $('#btnImgPanelRetailStore').click(function (event) {
        pcl37();
    });
    $('#btnImgPanel').click(function (event) {
        pcl37();
    });
    $('#btnShowIconsRetail').click(function (event) {
        pcL38();
    });
    $('#btnShowIcons').click(function (event) {
        pcL38();
    });
    $('#btnShowLogo').click(function (event) {
        pcl39();
    });
    $('#btnShowLogoRetail').click(function (event) {
        pcl39();
    });
    $('#btnBkImgPanel').click(function (event) {
        $("#BkImgContainer").tabs("option", "active", 0);
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        isBKpnl = true;
        $("#BkImgContainer").css("display", "block"); $(".divImageTypes").css("display", "none");
        $("#ImgCarouselDiv, #ShapesContainer, #LogosContainer").css("display", "none");
        $(".imgPanel2").css("display", "none");
        $(".placeHolderControls").css("display", "none");
        $(".bkPanel").css("display", "block");
        $(".imgPanel").css("display", "none");
        $('#uploader_browse').text("Upload Background");
        $('#uploader_browse').css("padding-left", "11px");
        $('#uploader_browse').css("width", "129px");
        $('#uploader_browse').css("margin-left", "4px");
        $('.RsizeDiv').css("width", "253px");
        $('.RsizeDiv').css("margin-left", "18px");
        if (!IsEmbedded) {
            $('.DamImgContainer').css("height", "311px");
        } pcL13();
        pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate ,#DivAdvanceColorPanel ,#quickText');
        pcL36('show', '#divImageDAM');
    });
    $('#btnShowMoreOptions').click(function (event) {
    });

    $('#btnAddRectangle').click(function (event) {
        //c12
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
            width: 100* dfZ1l,
            height: 100* dfZ1l,
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
        f1(6);
        pcL36('hide', '#divImageDAM');
    });

    $('#btnAddCircle').click(function (event) {
        //c13
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
        //COL.setFill(""black"");
        //c2(COL,undefined,""abc"");
        // c2(COL);
        pcL36('hide', '#divImageDAM');
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

    $("body").click(function (event) {
        if (event.target.id == "") {
            //  animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        } else if (event.target.id == "btnNewTxtPanel") {
            // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        } else if (event.target.id == "btnImgPanel" || event.target.id == "btnBkImgPanel") {
            // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
        } else if (event.target.id == "bd-wrapper" || event.target.id == "designer" || event.target.id == "CanvasContainer") {
            var D1AO = canvas.getActiveObject();
            var D1AG = canvas.getActiveGroup();
            if (D1AG) {
                canvas.discardActiveGroup();
            } else if (D1AO) {
                canvas.discardActiveObject();
            }
            canvas.renderAll();
            pcL13();
            pcL36('hide', '#textPropertPanel ,#divPopupUpdateTxt ,#divVariableContainer ,#DivAdvanceColorPanel ,#DivColorPallet ,#ShapePropertyPanel ,#ImagePropertyPanel ,#UploadImage ,#quickText ,#divPositioningPanel ,#DivAlignObjs ,#DivToolTip ,#addText ,#addImage');
           
        }

    });


    $('body').keydown(function (e) {
        var DIA0 = canvas.getActiveObject();
        if (DIA0 && DIA0.isEditing) {
            return
        } else {
            l3(e);
            //            e.preventDefault();
            //            e.stopPropagation();
        }
    });

    $('body').keyup(function (event) {
        var DIA0 = canvas.getActiveObject();
        if (DIA0 && DIA0.isEditing) {
            return
        } else {
            l2(event);
            //             event.preventDefault();
            //            event.stopPropagation();
        }

    });
    function f0(D1AO) {
        if ((IsEmbedded && (IsCalledFrom == 4)) || (IsEmbedded && (IsCalledFrom == 3))) {

            $("#LockPositionImg").css("visibility", "hidden");
            $("#BtnPrintImage").css("visibility", "hidden");
            // hiding labels 
            $("#lblLockPositionImg").css("visibility", "hidden");
            $("#lblDoNotPrintImg").css("visibility", "hidden");

            $("#LockImgProperties").css("visibility", "hidden");
            $("#lblLockImgProperties").css("visibility", "hidden");

            $("#divImgPosBoxSeperator").css("visibility", "hidden");
            $("#divImgPosBox").css("visibility", "hidden");
            $("#ImagePropertyPanel").css("height", "330px");

        }
        if (D1AO.IsPositionLocked) {
            $("#LockPositionImg").prop('checked', true);
        }
        else {
            $("#LockPositionImg").prop('checked', false);
        }
        if (D1AO.IsHidden) {
            $("#BtnPrintImage").prop('checked', true);
        }
        else {
            $("#BtnPrintImage").prop('checked', false);
        }

        if (D1AO.IsTextEditable) {
            $("#LockImgProperties").prop('checked', true);
        }
        else {
            $("#LockImgProperties").prop('checked', false);
        }
        if (D1AO.AutoShrinkText) {
            $("#chkboxAutoShrink").prop('checked', true);
        }
        else {
            $("#chkboxAutoShrink").prop('checked', false);
        }
        if (IsEmbedded && D1AO.IsPositionLocked) {
            $("#LockPositionImg").attr("disabled", "disabled");
            $("#LockImgProperties").attr("disabled", "disabled");
        } 
        if (D1AO.IsOverlayObject) {
            $("#chkboxOverlayImg").prop('checked', true);
        }
        else {
            $("#chkboxOverlayImg").prop('checked', false);
        }
        
        //$.each(TO, function (i, IT) {

        //    if (IT.ObjectID == D1AO.ObjectID) {
               

        //        return false;
        //    }
        //});


    }
    function g1(D1AO) {
        $("#BtnSearchTxt").removeAttr("disabled");
        if (IsCalledFrom == 3) {
            $("#BtnSelectFontsRetail").fontSelector('option', 'font', D1AO.get('fontFamily'));
        } else {
            $("#BtnSelectFonts").fontSelector('option', 'font', D1AO.get('fontFamily'));
        }
        $("#BtnFontSize").val(k13(D1AO.get('fontSize')));
        $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
        $("#txtLineHeight").val(D1AO.get('lineHeight'));
        $("#inputcharSpacing").val(k13(D1AO.get('charSpacing')));
        //  $("#txtAreaUpdateTxtPropPanel").val(D1AO.text);
        if (D1AO.IsPositionLocked) {
            $("#BtnLockTxtPosition").prop('checked', true);
        }
        else {
            $("#BtnLockTxtPosition").prop('checked', false);
        }
        if (D1AO.IsHidden) {
            $("#BtnPrintObj").prop('checked', true);
        }
        else {
            $("#BtnPrintObj").prop('checked', false);
        } //alert(IsEmbedded + " " +  IT.IsTextEditable);
        if (D1AO.IsTextEditable) {
            $("#BtnAllowOnlyTxtChange").prop('checked', true);
        }
        else {
            $("#BtnAllowOnlyTxtChange").prop('checked', false);
        }
        if (D1AO.AutoShrinkText) {
            $("#chkboxAutoShrink").prop('checked', true);
        }
        else {
            $("#chkboxAutoShrink").prop('checked', false);
        }

        if (!D1AO.IsEditable) {
            $("#BtnLockEditing").prop('checked', true);
        } else {
            $("#BtnLockEditing").prop('checked', false);
        }
        if (D1AO.IsOverlayObject) {
            $("#chkboxOverlayTxt").prop('checked', true);
        }
        else {
            $("#chkboxOverlayTxt").prop('checked', false);
        }
        if (D1AO.IsTextEditable != true) {
            if (D1AO.IsEditable) {
                $("#BtnLockEditing").prop('checked', false);
            }
            $("#BtnJustifyTxt1").removeAttr("disabled");
            $("#BtnJustifyTxt2").removeAttr("disabled");
            $("#BtnJustifyTxt3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder1").removeAttr("disabled");
            $("#BtnTxtarrangeOrder2").removeAttr("disabled");
            $("#BtnTxtarrangeOrder3").removeAttr("disabled");
            $("#BtnTxtarrangeOrder4").removeAttr("disabled");
            //$("#EditTXtArea").removeAttr("disabled");
            $("#BtnSearchTxt").removeAttr("disabled");
            //$("#BtnUpdateText").removeAttr("disabled");
            $("#BtnSelectFonts").removeAttr("disabled");
            if (IsCalledFrom == 3) {
                $("#BtnSelectFontsRetail").removeAttr("disabled");
            }
            $("#BtnFontSize").removeAttr("disabled");
            $("#BtnFontSizeRetail").removeAttr("disabled");
            $("#BtnBoldTxt").removeAttr("disabled");
            $("#BtnBoldTxtRetail").removeAttr("disabled");
            $("#BtnItalicTxt").removeAttr("disabled");
            $("#BtnItalicTxtRetail").removeAttr("disabled");
            $("#txtLineHeight").removeAttr("disabled");
            $("#BtnChngeClr").removeAttr("disabled");
          
            $("#BtnRotateTxtLft").removeAttr("disabled");
            $("#BtnRotateTxtRight").removeAttr("disabled");
            $("#BtnLockTxtPosition").removeAttr("disabled");
            $("#BtnPrintObj").removeAttr("disabled");
            if (IsEmbedded && !D1AO.IsEditable) {
                $("#BtnLockEditing").attr("disabled", "disabled");
            }
            if (IsEmbedded && D1AO.IsPositionLocked) {
                $("#BtnLockTxtPosition").attr("disabled", "disabled");
            } $(".fontSelector").removeAttr("disabled");
            $("#BtnTxtCanvasAlignLeft").removeAttr("disabled");
            $("#BtnTxtCanvasAlignCenter").removeAttr("disabled");
            $("#BtnTxtCanvasAlignRight").removeAttr("disabled");
            $("#BtnTxtCanvasAlignTop").removeAttr("disabled");
            $("#BtnTxtCanvasAlignMiddle").removeAttr("disabled");
            $("#BtnTxtCanvasAlignBottom").removeAttr("disabled");
            $("#inputcharSpacing").spinner("option", "disabled", false);
            $("#BtnFontSize").spinner("option", "disabled", false);
            $("#txtLineHeight").spinner("option", "disabled", false);
            $("#inputObjectWidthTxt").spinner("option", "disabled", false);
            $("#inputObjectHeightTxt").spinner("option", "disabled", false);
            $("#inputPositionXTxt").spinner("option", "disabled", false);
            $("#inputPositionYTxt").spinner("option", "disabled", false);
        }
        else {
            $("#inputcharSpacing").spinner("option", "disabled", true);
            if (!D1AO.IsEditable) {
                $("#BtnLockEditing").prop('checked', true);
            }
            $("#BtnJustifyTxt1").attr("disabled", "disabled");
            $("#BtnJustifyTxt2").attr("disabled", "disabled");
            $("#BtnJustifyTxt3").attr("disabled", "disabled");
            $("#BtnTxtarrangeOrder1").attr("disabled", "disabled");
            $("#BtnTxtarrangeOrder2").attr("disabled", "disabled");
            $("#BtnTxtarrangeOrder3").attr("disabled", "disabled");
            $("#BtnTxtarrangeOrder4").attr("disabled", "disabled");
            //$("#EditTXtArea").attr("disabled", "disabled");
            $("#BtnSearchTxt").attr("disabled", "disabled");
            //$("#BtnUpdateText").attr("disabled", "disabled");
            $("#BtnSelectFonts").attr("disabled", "disabled");
            if (IsCalledFrom == 3) {
                $("#BtnSelectFontsRetail").attr("disabled", "disabled");
            }
            $("#BtnFontSize").attr("disabled", "disabled");
            $("#BtnFontSizeRetail").attr("disabled", "disabled");
            $("#BtnBoldTxt").attr("disabled", "disabled");
            $("#BtnBoldTxtRetail").attr("disabled", "disabled");
            $("#BtnItalicTxt").attr("disabled", "disabled");
            $("#BtnItalicTxtRetail").attr("disabled", "disabled");
            $("#txtLineHeight").attr("disabled", "disabled");
            $("#BtnChngeClr").attr("disabled", "disabled");
            
            $("#BtnRotateTxtLft").attr("disabled", "disabled");
            $("#BtnRotateTxtRight").attr("disabled", "disabled");
            $("#BtnLockTxtPosition").attr("disabled", "disabled");
            $("#BtnPrintObj").attr("disabled", "disabled");

            $("#BtnTxtCanvasAlignLeft").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignCenter").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignRight").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignTop").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignMiddle").attr("disabled", "disabled");
            $("#BtnTxtCanvasAlignBottom").attr("disabled", "disabled");
            $("#BtnFontSize").spinner("option", "disabled", true);
            $("#txtLineHeight").spinner("option", "disabled", true);

            $("#inputObjectWidthTxt").spinner("option", "disabled", true);
            $("#inputObjectHeightTxt").spinner("option", "disabled", true);
            $("#inputPositionXTxt").spinner("option", "disabled", true);
            $("#inputPositionYTxt").spinner("option", "disabled", true);
            $(".fontSelector").attr("disabled", "disabled");
        }
        if (D1AO.IsEditable) {
            $("#BtnDeleteTxtObj").removeAttr("disabled");
        }
        else {
            $("#BtnDeleteTxtObj").attr("disabled", "disabled");
        }
        //$.each(TO, function (i, IT) {

        //    if (IT.ObjectID == D1AO.ObjectID) {
              
        //    }
        //});


        if ((IsEmbedded && (IsCalledFrom == 4)) || (IsEmbedded && (IsCalledFrom == 3))) {
            $("#BtnLockTxtPosition").css("visibility", "hidden");
            $("#lblLockPositionTxt").css("visibility", "hidden");
            $("#BtnAllowOnlyTxtChange").css("visibility", "hidden");
            $("#lblAllowOnlyTxtChng").css("visibility", "hidden");
            $("#BtnLockEditing").css("visibility", "hidden");
            $("#lblLockEditingTxt").css("visibility", "hidden");
            $("#BtnPrintObj").css("display", "none");
            $("#lblDoNotPrintTxt").css("display", "none");
            $("#BtnUploadFont").css("visibility", "hidden");
            $("#textPropertPanel").css("height", "330px");
            $("#divSepratrTxtPos").css("visibility", "hidden");
            $("#chkboxAutoShrink").css("visibility", "hidden");
            $("#lblAutoShrink").css("visibility", "hidden");
        }
    }





    function g2(e) {
        pcL36('hide', '#DivPersonalizeTemplate , #DivAdvanceColorPanel , #quickText');
        pcL13();
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        var lastPanelLocal = D1LP;
        //animatedcollapse.show('DivControlPanel1');
        if (D1AO && D1LP != D1AO.type) {
            pcL13();
            pcL36('hide', '#textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText, #addImage , #addText , #DivAlignObjs');
           
        }
        D1LP = D1AO.type;
        if (D1AG) {
            pcL13();
            pcL36('hide', '#textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText, #addImage , #addText , #DivToolTip');
            pcL36('show', '#DivAlignObjs');
        }

        if (D1AO && D1AO.type === 'text' || D1AO && D1AO.type === 'i-text') {
            g1(D1AO);
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                $("#AddColorTxtRetailDiv").css("display", "none");
            } else {
                $("#BtnChngeClr").css("visibility", "hidden");
            }
            if (IsCalledFrom == 3) {
                pcL22(D1AO);
                m0();
            } else {
                e1("textPropertPanel", lastPanelLocal);
                pcL36('show', '#textPropertPanel');

            }


        }
        else if (D1AO && D1AO.type === 'image') {
            if ((IsEmbedded && D1AO.IsTextEditable && (IsCalledFrom == 4))) {
            } else {
                if (IsCalledFrom == 3) {
                    //pcL36('hide', '#divBCMenuPresets , #DivCropToolContainer');
                    //var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
                    //var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
                    //if (!IsBC) {
                    //    h -= 148;
                    //}
                    //$("#divImgPropPanelRetail").css("display", "block");
                    //$("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
                    $(".toolbarImage").css("display", "none");
                    $(".toolbarImage").css("opacity", "0");
                    $(".elementColorImg").css("display", "none"); $(".elementCrop").css("display", "table-cell");
                    m0();
                } else {
                    f0(D1AO);
                    e1("ImagePropertyPanel", lastPanelLocal);
                    pcL36('show', '#ImagePropertyPanel');
                    DisplayDiv('1');
                }
            }
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                $("#AddColorShapeRetailDiv").css("display", "none");
            }
        }  else if (D1AO && D1AO.type === 'rect') {
            if ((D1AO.IsTextEditable && (IsCalledFrom == 4))) {
            } else {
                if (IsCalledFrom == 3) {
                    //var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
                    //var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
                    //if (!IsBC) {
                    //    h -= 148;
                    //}
                    //$("#divImgPropPanelRetail").css("display", "block");
                    //$("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
                    $(".toolbarImage").css("display", "none");
                    $(".toolbarImage").css("opacity", "0"); $(".spanRectColour").css("background-color", D1AO.fill);
                    $(".elementCrop").css("display", "none"); $(".elementColorImg").css("display", "table-cell");
                    m0();
                } else {
                    f0(D1AO);
                    e1("ImagePropertyPanel", lastPanelLocal);
                    pcL36('show', '#ImagePropertyPanel');
                    DisplayDiv('2');
                }
            }
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                $("#AddColorShapeRetailDiv").css("display", "none");
            }
        } else if (D1AO && D1AO.type === 'ellipse') {
            if ((D1AO.IsTextEditable && (IsCalledFrom == 4))) {
            } else {
                if (IsCalledFrom == 3) {
                    //var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
                    //var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
                    //if (!IsBC) {
                    //    h -= 148;
                    //}
                    //$("#divImgPropPanelRetail").css("display", "block");
                    //$("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
                    $(".toolbarImage").css("display", "none");
                    $(".toolbarImage").css("opacity", "0");
                    $(".elementCrop").css("display", "none"); $(".spanRectColour").css("background-color", D1AO.fill);
                    $(".elementColorImg").css("display", "table-cell");
                    m0();
                } else {
                    f0(D1AO);
                    e1("ImagePropertyPanel", lastPanelLocal);
                    pcL36('show', '#ImagePropertyPanel');
                    DisplayDiv('2');
                }
            }
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                $("#AddColorShapeRetailDiv").css("display", "none");
            }
        } else if (D1AO && (D1AO.type === 'path-group' || D1AO.type === 'path')) {
            if ((D1AO.IsTextEditable && (IsCalledFrom == 4))) {
            } else {
                if (IsCalledFrom == 3) {
                    //var l = $("#canvas").offset().left - $(".imgMenuDiv").width() + D1AO.left + D1AO.getWidth() / 2;
                    //var h = $("#canvas").offset().top - $(".imgMenuDiv").height() + D1AO.top - D1AO.getHeight() / 2 - 20;
                    //if (!IsBC) {
                    //    h -= 148;
                    //}
                    //$("#divImgPropPanelRetail").css("display", "block");
                    //$("#divImgPropPanelRetail").css("-webkit-transform", "translate3d(" + l + "px, " + h + "px, 0px)");
                    $(".toolbarImage").css("display", "none");
                    $(".toolbarImage").css("opacity", "0");
                    $(".elementCrop").css("display", "none"); $(".spanRectColour").css("background-color", D1AO.fill);
                    $(".elementColorImg").css("display", "table-cell");
                    m0();
                } else {
                    f0(D1AO);
                    e1("ImagePropertyPanel", lastPanelLocal);
                    pcL36('show', '#ImagePropertyPanel');
                    DisplayDiv('2');
                }
            }
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                $("#AddColorShapeRetailDiv").css("display", "none");
            }
        }
        //}
        if (D1AG && D1AG.type === 'group') {
            D1LP = D1AG.type;
            $("#BtnSearchTxt").attr("disabled", "disabled");

        }
        else if (D1AG && D1AG.type === 'image') {
            pcL36('show', '#ImagePropertyPanel');
            DisplayDiv('1');
        }
    }

    $('#BtnAddNewText').click(function () {
        //e0(); // l3
        if ($("#IsQuickTxtCHK").is(':checked')) {
            var val1 = $("#txtQTitleChk").val();
            // var val2 = $("#TxtQSequence").val();
            if (val1 == "") {
                return false;
            } else {

            }
        }
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
        D1NTO.IsBold = false;
        D1NTO.IsItalic = false;
        D1NTO.LineSpacing = 1.4;
        D1NTO.CharSpacing = 0;
        D1NTO.ProductPageId = SP;
        if (IsCalledFrom == 2 || IsCalledFrom == 4) {
            D1NTO.IsSpotColor = true;
            D1NTO.SpotColorName = 'Black';
        }
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
        if ($("#IsQuickTxtCHK").is(':checked')) {
            D1NTO.IsQuickText = true;
            D1NTO.QuickTextOrder = TOFZ++; // $('#TxtQSequence').val();
            D1NTO.Name = $('#txtQTitleChk').val();
            D1NTO.watermarkText = $('#txtQWaterMark').val();
        } else {
            D1NTO.IsQuickText = false;
        }

        D1NTO.FontSize = 12;


        var uiTextObject = c0(canvas, D1NTO);
        canvas.centerObject(uiTextObject);
        D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
        D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
        canvas.renderAll();
        uiTextObject.setCoords();
        $('#txtAddNewText').val("");
        $('#txtQTitleChk').val("");
        $('#txtQWaterMark').val("");
        $('#TxtQSequence').val("");
        $("#IsQuickTxtCHK").prop('checked', false);
        $("#addText").css("height", "192px");
        $("#QtxtINRow").css("display", "none");
        $(".popUpQuickTextPanel").css("top", "131px");
        pcL36('toggle', '#addText');
        pcL36('toggle', '#quickText');
        TO.push(D1NTO);
        //		var D1NTO = {};
        //		D1NTO = fabric.util.object.clone(TO[0]);
        //		D1NTO.Name = "New Text";
        //		D1NTO.ContentString = $('#txtAddNewText').val();
        //		D1NTO.ObjectID = --NCI;
        //		D1NTO.ColorHex = "#000000";
        //		D1NTO.ColorC = 0;
        //		D1NTO.ColorM = 0;
        //		D1NTO.ColorY = 0;
        //		D1NTO.ColorK = 100;
        //		D1NTO.IsBold = false;
        //		D1NTO.IsItalic = false;
        //		D1NTO.LineSpacing = 1;
        //		D1NTO.ProductPageId = SP;
        //		D1NTO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
        //		var text = $('#txtAddNewText').val();
        //		//alert(text.length);
        //		var textLength = text.length;
        //		D1NTO.MaxWidth = 100;
        //		D1NTO.MaxHeight = 30;

        //		if(textLength < 30)
        //		{
        //			var diff = textLength /10;
        //			D1NTO.MaxWidth = 100* diff;
        //		} else 
        //		{
        //			D1NTO.MaxWidth = 300;
        //			var diff = textLength /30;
        //			D1NTO.MaxHeight = 15*diff;
        //		}
        //		
        //		
        //		D1NTO.FontSize = 10;


        //		var uiTextObject = c0(canvas, D1NTO);
        //		canvas.centerObject(uiTextObject);
        //		D1NTO.PositionX = uiTextObject.left - uiTextObject.maxWidth / 2;
        //		D1NTO.PositionY = uiTextObject.top - uiTextObject.maxHeight / 2;
        //		canvas.renderAll();
        //		$('#txtAddNewText').val("");
        //		animatedcollapse.toggle('addText');
        //		animatedcollapse.toggle('quickText');
        //		TO.push(D1NTO);
    });
    $('#btnHeadingTxt').click(function () {
        pcL29(26.67, true);
        if (IsCalledFrom == 3) {
            m0();
        }
    });
    $('#btnSubTitleTxt').click(function () {
        pcL29(21.33, false);
        if (IsCalledFrom == 3) {
            m0();
        }
    });
    $('#btnBodyTxt').click(function () {
        pcL29(13.33, false);
        if (IsCalledFrom == 3) {
            m0();
        }
    });
    $('.InputImgContainer').click(function (event) {
        event.stopPropagation();$('.InputImgContainer').focus();
    });
    
    //	document.getElementById('BtnUpdateText').onclick = function (ev) {
    //		var D1AO = canvas.getActiveObject();
    //		if (D1AO) {
    //			var text = $('#EditTXtArea').val();
    //			var textLength = text.length;
    //			// checking for empty text
    //			if (textLength > 0) {
    //				D1AO.text = $("#EditTXtArea").val();
    //				D1AO.saveState();
    //				c2(D1AO);
    //				canvas.renderAll();
    //				animatedcollapse.toggle('textPropertPanel');
    //			}
    //			else {
    //				alert("if you want to empty the text in this field, kindly delete it!");
    //			}
    //		}

    //	}


    canvas.on('object:over', function (e) {
        if (e.TG.IsQuickText == true && e.TG.type == 'image' && e.TG.getWidth() > 112 && e.TG.getHeight() > 64) {
            $("#placeHolderTxt").css("visibility", "visible")
            var width = $("#placeHolderTxt").width() / 2;
            $("#placeHolderTxt").css("left", ($(window).width() / 2 - canvas.getWidth() / 2 + e.TG.left - width) + "px");
            $("#placeHolderTxt").css("top", (160 + e.TG.top - 13) + "px");
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
    function g4(event) {

    }
    function g3(event) {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            if (D1SD == false) {

                var objectsInGroup = D1AG.getObjects();
                objectsInGroup.forEach(function (OPT) {
                    var clonedItem = fabric.util.object.clone(OPT);
                    clonedItem.left += D1AG.left;
                    clonedItem.top += D1AG.top;
                   // c2(clonedItem);
                    //alert(clonedItem.top);
                });
            }
        } else if (D1AO) {
           // c2(D1AO);
        }
    }
    //    canvas.observe('object:over', function (e) {
    //        e.memo.target.setFill('red');
    //        var ctx = canvas.getContext();
    //        e.memo.target.drawCorners(ctx);
    //        canvas.renderAll();
    //    });
    //    canvas.observe('object:out', function (e) {
    //        e.memo.target.setFill('green');
    //        canvas.renderAll();
    //    });

    canvas.observe('selection:cleared', function (e) {
        pcL36('hide', '#divPopupUpdateTxt , #quickText , #textPropertPanel , #divVariableContainer , #divPositioningPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #addImage , #addText , #DivPersonalizeTemplate , #DivAlignObjs , #DivToolTip');
        pcL13();
        if (IsCalledFrom != 3) {
            pcL36('hide', '#quickTextFormPanel');
        } else {
            m0();
        }
    });


    canvas.observe('object:selected', g5);
    //	canvas.observe('group:selected', g4);
    fabric.util.addListener(fabric.document, 'dblclick', j4);
    // fabric.util.removeListener(canvas.upperCanvasEl, 'dblclick', j4);
    canvas.observe('object:moving', g6);

    function g4(e) {
        var D1AG = canvas.getActiveGroup();
        g2(e);
    }
    function j4(e) {
        if ($(e.target).hasClass("ui-button-text") || e.target.id == "btnMoveObjLeftTxt" || e.target.id == "btnMoveObjUpTxt" || e.target.id == "btnMoveObjDownTxt" || e.target.id == "btnMoveObjRightTxt" || e.target.id == "divPositioningPanel" || $(e.target).hasClass("DivTitleLbl")) {
        } else {
            var DIAO = canvas.getActiveObject();
            if (DIAO && (DIAO.type === 'text' || DIAO.type === 'i-text')) {

            } else if (DIAO && DIAO.type === 'image' && DIAO.IsQuickText == true) {
                $(".placeHolderControls").css("display", "block");
                $("#BkImgContainer").css("display", "none");
                $("#ImgCarouselDiv").css("display", "block");
                $(".bkPanel").css("display", "none");
                $(".imgPanel").css("display", "");
                $('#uploader_browse').text("Upload Image");
                $('#uploader_browse').css("padding-left", "15px");
                $('#uploader_browse').css("width", "92px"); $('.RsizeDiv').css("margin-left", "29px");
                $('#uploader_browse').css("margin-left", "27px"); $('.RsizeDiv').css("width", "146px");
                if (IsEmbedded) {
                    $("#btnAddImagePlaceHolder").css("display", "none");
                    $(".spanImgPlaceHolder").css("display", "none");
                    $("#btnCompanyPlaceHolder").css("display", "none");
                    $("#btnContactPersonPlaceHolder").css("display", "none"); $(".placeHolderControls").css("display", "none");
                } else {
                    $('.DamImgContainer').css("height", "311px");
                }
                pcL13();
                pcL36('hide', '#DivLayersPanel ,#DivPersonalizeTemplate , #DivAdvanceColorPanel ,#quickText');
                pcL36('show', '#divImageDAM');
                //$('#btnImgPanel').click();//animatedcollapse.show('divImageDAM');
            }
            //alert();
        }
    }
    $("#txtAreaUpdateTxt").keyup(function () {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            var text = $('#txtAreaUpdateTxt').val();
            var textLength = text.length;
            // checking for empty text
            if (textLength > 0) {
                D1AO.text = $("#txtAreaUpdateTxt").val();
                D1AO.saveState();
               // c2(D1AO);
                canvas.renderAll();
            }
            else {
                if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                    D1AO.text = $("#txtAreaUpdateTxt").val();
                    D1AO.saveState();
                  //  c2(D1AO);
                    canvas.renderAll();
                } else {
                    alert("If you want to remove this field from the canvas, then click on the 'Remove' button on the property panel.");
                }
            }
        }
        //   $("#txtAreaUpdateTxtPropPanel").val($("#txtAreaUpdateTxt").val());
    });
    //    $("#txtAreaUpdateTxtPropPanel").keyup(function () {
    //        var D1AO = canvas.getActiveObject();
    //        if (D1AO) {
    //            var text = $('#txtAreaUpdateTxtPropPanel').val();
    //            var textLength = text.length;
    //            // checking for empty text
    //            if (textLength > 0) {
    //                D1AO.text = $("#txtAreaUpdateTxtPropPanel").val();
    //                D1AO.saveState();
    //                c2(D1AO);
    //                canvas.renderAll();
    //            }
    //            else {
    //                if(IsCalledFrom == 2 || IsCalledFrom == 4) {
    //                    D1AO.text = $("#txtAreaUpdateTxtPropPanel").val();
    //                    D1AO.saveState();
    //                    c2(D1AO);
    //                    canvas.renderAll();
    //                } else {
    //                    alert("If you want to remove this field from the canvas, then click on the 'Remove' button on the property panel.");
    //                }
    //            }
    //        }
    //        $("#txtAreaUpdateTxt").val($("#txtAreaUpdateTxtPropPanel").val());
    //    });
    function g5(e) {
        IsDesignModified = true;
        //animatedcollapse.hide('divPopupUpdateTxt');
        var selName = "select#BtnSelectFonts";
        if (IsCalledFrom == 3) {
            selName = "select#BtnSelectFontsRetail";
        }
       // $('.fontSelector h4').text("");
        $(selName).fontSelector('option', 'close', 'Arial Black');
        pcL13();
        pcL36('hide', '#divVariableContainer');
        var D1AO = canvas.getActiveObject(); 
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            buildUndo(D1AG);
        } else {
            buildUndo(D1AO);
        }
        if (D1AG && D1AG.type === 'group') {
            f1(1);
            pcL36('show', '#DivAlignObjs');
        }
        else if (D1AG && D1AG.type === 'image') {
            f1(2);
        }
        else if (D1AO && (D1AO.IsPositionLocked != true || IsEmbedded == false)) {

            if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
                f1(3);
            }
            else if (D1AO && D1AO.type === 'image') {

                f1(2);
            } else if (D1AO && D1AO.type === 'path') {

                f1(2);
            } else if (D1AO && D1AO.type === 'rect') {
                f1(2);
            } else if (D1AO && D1AO.type === 'circle') {
                f1(2);
            } else if (D1AO && D1AO.type === 'path-group') {
                f1(2);
            }
            k4();
            g2(e);
        } else {
            if (D1AO) {

                pcL13();
                pcL36('hide', '#DivAlignObjs , #textPropertPanel , #DivAdvanceColorPanel , #DivColorPallet , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText , #addImage , #addText , #DivToolTip , #DivAlignObjs , #quickTextFormPanel , #DivPersonalizeTemplate ');
                if (D1AO.type === 'image' && D1AO.IsQuickText == true && IsCalledFrom == 4) {
                    //if ($("#DivCropToolContainer").css("display") == "none") {
                    //    $("#BtnCropImg").click();
                    //}
                      pcL20_new();
                } else if (D1AO.type === 'i-text') {
                } else {
                    if (IsCalledFrom != 3) { 
                        canvas.discardActiveObject();
                    }
                }
                if (IsCalledFrom == 3) { 
                    m0();
                }
            }
        }
    }


    function g6(e) {
        IsDesignModified = true;
        k4();
        var X = e.left;
        var Y = e.top;
        //logAction(e.memo.target.ObjectID + " pos x " +  e.memo.target.left);

        if (ISG) {
            var line1 = 0;
            var line2 = 0;

            line1 = SXP[0];
            line2 = SXP[1];

            var iCounter = 1;

            while (iCounter < SXP.length - 1) {

                if (X > line1 && X < line2) {
                    X = line1;
                    break;
                }

                iCounter++;
                line1 = SXP[iCounter - 1];
                line2 = SXP[iCounter];
            }

            line1 = 0;
            line2 = 0;

            line1 = SYP[0];
            line2 = SYP[1];

            iCounter = 1;

            while (iCounter < SYP.length - 1) {

                if (Y > line1 && Y < line2) {
                    Y = line1;
                    break;
                }

                iCounter++;
                line1 = SYP[iCounter - 1];
                line2 = SYP[iCounter];
            }

            e.left = X;
            e.top = Y;

        }

    }


    //	$('#EditTXtArea').keyup(function (event) {

    //		if (event.stopPropagation) {
    //			event.stopPropagation();
    //		}
    //		else {
    //			event.cancelBubble = true;
    //		}

    //	});



    $('#txtAddNewText').keyup(function (event) {

        if (event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            event.cancelBubble = true;
        }

    });

    $("#BtnLockTxtPosition").click(function () {

        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.type === 'text' || D1AO.type === 'i-text') { 
            if (thisCheck.is(':checked')) {
                D1AO.IsPositionLocked = true;
                D1AO.lockMovementX = true;
                D1AO.lockMovementY = true;
                D1AO.lockScalingX = true;
                D1AO.lockScalingY = true;
                D1AO.lockRotation = true;
            }
            else {
                D1AO.IsPositionLocked = false;
                D1AO.lockMovementX = false;
                D1AO.lockMovementY = false;
                D1AO.lockScalingX = false;
                D1AO.lockScalingY = false;
                D1AO.lockRotation = false;
            }
           // c2(D1AO);

        }
        else if (D1AO.type === 'group') {
            var objectsInGroup = D1AO.getObjects();
            objectsInGroup.forEach(function (OPT) {
                if (thisCheck.is(':checked')) {
                    OPT.IsPositionLocked = true;
                    OPT.lockMovementX = true;
                    OPT.lockMovementY = true;
                    OPT.lockScalingX = true;
                    OPT.lockScalingY = true;
                    OPT.lockRotation = true;
                }
                else {
                    OPT.IsPositionLocked = false;
                    OPT.lockMovementX = false;
                    OPT.lockMovementY = false;
                    OPT.lockScalingX = false;
                    OPT.lockScalingY = false;
                    OPT.lockRotation = false;
                }
              //  c2(OPT);
            });

        }
    });

    $("#BtnPrintObj").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.type === 'text' || D1AO.type === 'i-text') {
            if (thisCheck.is(':checked')) {
                D1AO.IsHidden = true;
            }
            else {
                D1AO.IsHidden = false;
            }
         //   c2(D1AO);
        }
        else if (D1AO.type === 'group') {
            var objectsInGroup = D1AO.getObjects();
            objectsInGroup.forEach(function (OPT) {
                if (thisCheck.is(':checked')) {
                    D1AO.IsHidden = true;
                }
                else {
                    D1AO.IsHidden = false;
                }
            });
        }
    });
    $("#TxtQSequence").keydown(function (event) {
        // Allow: backspace, delete, tab, escape, and enter
        if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
            // Allow: Ctrl+A
			(event.keyCode == 65 && event.ctrlKey === true) ||
            // Allow: home, end, left, right
			(event.keyCode >= 35 && event.keyCode <= 39)) {
            // let it happen, don't do anything
            return;
        }
        else {
            // Ensure that it is a number and stop the keypress
            if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                event.preventDefault();
            }
        }
    });
    $("#BtnLockEditing").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.type === 'text' || D1AO.type === 'i-text') {
            
            if (D1AO.get('IsEditable') == true) {
                D1AO.IsEditable = false;
                D1AO.IsTextEditable = false;
               // $("#BtnAllowOnlyTxtChange").prop('checked', false);
                pcL13();
                pcL36('hide', '#divPopupUpdateTxt , #divVariableContainer ');
                
            }
            else {
                D1AO.IsEditable = true;
            }
           // c2(D1AO);
        }
        g1(D1AO);

    });
    $("#chkboxAutoShrink").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.type === 'text' || D1AO.type === 'i-text') {
           
            if (D1AO.get('AutoShrinkText')) {
                D1AO.AutoShrinkText = false;
                $("#chkboxAutoShrink").prop('checked', false);
                pcL13();
            }
            else {
                D1AO.AutoShrinkText = true;
                $("#chkboxAutoShrink").prop('checked', true);
            }
        }
        
        g1(D1AO);
    });
    $("#chkboxOverlayTxt").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.type === 'text' || D1AO.type === 'i-text') {

            if (D1AO.get('IsOverlayObject')) {
                D1AO.IsOverlayObject = false;
                $("#chkboxOverlayTxt").prop('checked', false);
                pcL13();
            }
            else {
                D1AO.IsOverlayObject = true;
                $("#chkboxOverlayTxt").prop('checked', true);
            }
        }

        g1(D1AO);
    });
    $("#chkboxOverlayImg").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.get('IsOverlayObject')) {
            D1AO.IsOverlayObject = false;
            $("#chkboxOverlayImg").prop('checked', false);
            pcL13();
        }
        else {
            D1AO.IsOverlayObject = true;
            $("#chkboxOverlayImg").prop('checked', true);
        }
        //  c2(D1AO);
        g1(D1AO);
    });
    $("#LockImgProperties").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (thisCheck.is(':checked')) {
            D1AO.IsTextEditable = true;
            D1AO.IsPositionLocked = true;
            $("#LockPositionImg").prop('checked', true);
        }
        else {
            D1AO.IsTextEditable = false;
        }
      //  c2(D1AO);
        g1(D1AO);
    });
    $("#IsQuickTxtCHK").click(function () {
        var thisCheck = $(this);
        if (thisCheck.is(':checked')) {
            $("#addText").css("height", "308px");
            $("#QtxtINRow").css("display", "block");

            $(".popUpQuickTextPanel").css("top", "248px");
        }
        else {
            $("#addText").css("height", "192px");
            $("#QtxtINRow").css("display", "none");
            $(".popUpQuickTextPanel").css("top", "131px");
        }
    });
    $("#BtnAllowOnlyTxtChange").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        if (D1AO.type === 'i-text') {
            if (D1AO.get('IsTextEditable')) {
                D1AO.IsTextEditable = false;
                
            }
            else {
                D1AO.IsTextEditable = true;
             //   D1AO.IsEditable = false; 
            }
          //  c2(D1AO);
        }
        g1(D1AO);
        //animatedcollapse.toggle('textPropertPanel');
    });

    document.getElementById('BtnSelectFonts').onchange = function (ev) {
        pcL04(1);
    }
    document.getElementById('BtnSelectFontsRetail').onchange = function (ev) {
        pcL04(2);
    }
    document.getElementById('dropDownPresets').onchange = function (ev) {
        var id = $('#dropDownPresets').val();
        slLLID = id;
        $(".classUpdateBtns").css("display", "block");
        $(".imgPresetContainer").css("visibility", "visible");
        l5(id);
    }
    document.getElementById('presetLogo').onchange = function (ev) {
        var id = $('#presetLogo').val();
        $(".imgPresetContainer").css("visibility", "visible");
        l8(parseInt(id));
    }
    $("#btnDeletePreset").click(function () {
        if (slLLID != 0) {
            var svcURL = "services/layoutsvc/delete/";
            StartLoader();
            $.getJSON(svcURL + slLLID,
            function (DT) {
                pcL36('hide', '#divPresetEditor');
                $("itemPre" + slLLID).remove();
                b3_1(1);//StopLoader();
                $("#presetTitle").val("");

            });
            slLLID = 0;
        } else {
            alert("please select a preset first!");
        }
    });
    $("#btnUpdatePreset").click(function () {
        if (slLLID != 0) {
            l7(1);
        } else {
            alert("please select a preset first!");
        }
    });

    $("#btnAddPreset").click(function () {
        l7(2);
    });
    function l7(mode) {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
       // e0();
        StartLoader();
        var title = $("#presetTitle").val();
        var orientation = 2;
        var logoType = $("#presetLogo").val();
        $.each(TP, function (i, IT) {
            if (IT.ProductPageID == SP) {
                orientation = IT.Orientation;
            }
        });
        var ID = slLLID;
        var objLayout = {
            Title: title,
            ProductCategoryID: Template.ProductCategoryID,
            Orientation: orientation,
            ImageLogoType: logoType,
            LayoutID: ID
        }
        var listData = [];
        TempOB = [];
        TempFinO("Name");
        TempFinO("Title");
        TempFinO("CompanyName");
        TempFinO("CompanyMessage");
        TempFinO("AddressLine1");
        TempFinO("Phone");
        TempFinO("Fax");
        TempFinO("Email");
        TempFinO("Website");
        $.each(TempOB, function (i, item) {
            if (item != null && item != "") {
                var objPres = {
                    maxWidth: item.maxWidth,
                    maxHeight: item.maxHeight,
                    fontSize: item.fontSize,
                    textAlign: item.textAlign,
                    fontWeight: item.fontWeight,
                    LeftPos: item.left * dfZ1l,
                    topPos: item.top * dfZ1l,
                    FeildName: item.Name
                }
                listData.push(objPres);
            }
        });
        var obSt = {
            obj: objLayout,
            objsAttr: listData
        }
        var jsonObjects = JSON.stringify(obSt, null, 2);
        var to;
        if (mode == 1)
            to = "services/layoutsvc/update/";
        else
            to = "services/layoutsvc/addNew/";

        var options = {
            type: "POST",
            url: to,
            data: jsonObjects,
            contentType: "application/json;",
            dataType: "json",
            async: true,
            complete: function (httpresp, returnstatus) {
                pcL36('hide', '#divPresetEditor ');
                $("#presetTitle").val("");
                b3_1(1);
                //StopLoader();

                var id = parseInt(httpresp.responseText);
                if (mode == 2) {
                    b1("dropDownPresets", id, title, "itemPre" + id);

                }
            }
        };
        var returnText = $.ajax(options).responseText;
    }
    $('#BtnFontSize').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
        event.stopPropagation();
    });
    $('#BtnFontSizeRetail').keypress(function (event) {
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
        event.stopPropagation();
    });
    //    document.getElementById('BtnFontSize').onchange = function (ev) {
    //        var fz = $('#BtnFontSize').val();
    //        var fontSize = parseInt(fz);
    //        if (fontSize != "") {
    //            var selectedObject = canvas.getActiveObject();
    //            if (selectedObject) {
    //                
    //                setActiveStyle("font-Size",fontSize);
    //                c2(selectedObject);
    //                canvas.renderAll();

    //            }
    //        }
    //    }
    document.getElementById('BtnBoldTxt').onclick = function (ev) {
        pcL05();
    }
    document.getElementById('BtnBoldTxtRetail').onclick = function (ev) {
        pcL05();

    }
    var cmdItalicBtn = document.getElementById('BtnItalicTxt');
    if (cmdItalicBtn) {
        cmdItalicBtn.onclick = function () {
            pcL06();
        };
    }
    document.getElementById('BtnItalicTxtRetail').onclick = function (ev) {
        pcL06();
    }
    //    $("#txtLineHeight").change(function (event) {
    //        var D1AO = canvas.getActiveObject();
    //        if (D1AO && D1AO.type === 'text') {
    //            D1AO.lineHeight = $("#txtLineHeight").val();
    //            $("#txtAreaUpdateTxt").css("line-height", $("#txtLineHeight").val());
    //        }
    //        c2(D1AO);
    //        canvas.renderAll();
    //        if (event.stopPropagation) {
    //            event.stopPropagation();
    //        }
    //        else {
    //            event.cancelBubble = true;
    //        }
    //    });

    $("#txtLineHeight").keyup(function (event) {
        if (event.stopPropagation) {
            event.stopPropagation();
        }
        else {
            event.cancelBubble = true;
        }
    });
    //    $(".panelQuickTextFormRow input").keyup(function (event) {
    //        if (event.stopPropagation) {
    //            event.stopPropagation();
    //        }
    //        else {
    //            event.cancelBubble = true;
    //        }
    //    });
    document.getElementById('BtnJustifyTxt1').onclick = function (ev) {
        pcL07();
    }

    document.getElementById('BtnJustifyTxt2').onclick = function (ev) {
        pcL08();
    }
    document.getElementById('BtnJustifyTxt3').onclick = function (ev) {
        pcL09();
    }
    document.getElementById('BtnJustifyTxt1Retail').onclick = function (ev) {
        pcL07();
    }

    document.getElementById('BtnJustifyTxt2Retail').onclick = function (ev) {
        pcL08();
    }
    document.getElementById('BtnJustifyTxt3Retail').onclick = function (ev) {
        pcL09();
    }

    document.getElementById('BtnTxtarrangeOrder1').onclick = function (ev) {
        pcL26();
    }
    document.getElementById('BtnTxtarrangeOrder1Retail').onclick = function (ev) {
        pcL26();
    }

    document.getElementById('BtnTxtarrangeOrder2').onclick = function (ev) {
        pcL27();
    }
    document.getElementById('BtnTxtarrangeOrder3').onclick = function (ev) {
        pcL28();
    }
    document.getElementById('BtnTxtarrangeOrder2Retail').onclick = function (ev) {
        pcL27();
    }
    document.getElementById('BtnTxtarrangeOrder3Retail').onclick = function (ev) {
        pcL28();
    }
   
    document.getElementById('BtnTxtarrangeOrder4').onclick = function (ev) {
        pcL25();
    }
    document.getElementById('BtnTxtarrangeOrder4Retail').onclick = function (ev) {
        pcL25();
    }
    document.getElementById('BtnRotateTxtLft').onclick = function (ev) {
        pcL11();
    }
    document.getElementById('BtnRotateTxtRight').onclick = function (ev) {
        pcL12();
    }
    document.getElementById('BtnRotateTxtLftRetail').onclick = function (ev) {
        pcL11();
    }
    document.getElementById('BtnRotateTxtRightRetail').onclick = function (ev) {
        pcL12();
    }
    function g8(obj1, obj2) {
        if (obj1.width > obj2.width && obj1.height > obj2.height) {
            return true;
        }
        else {
            return false;
        }
    }

    document.getElementById('BtnAlignObjCenter').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var D1AG = canvas.getActiveGroup().getObjects();
            if (D1AG) {
                //c17
                var minID = 0;
                var mintop = 0;
                var left = 0;
                mintop = D1AG[0].top;
                minID = D1AG[0].ObjectID;
                left = D1AG[0].left;
                // ModifiedObjectIds.push(D1AG[0].ObjectID);
                if (D1AG) {
                    for (i = 0; i < D1AG.length; i++) {
                        if (D1AG[i].ObjectID != minID) {
                            D1AG[i].left = left;
                            //c2(D1AG[i]);
                            //ModifiedObjectIds.push(D1AG[i].ObjectID);

                        }

                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < D1AG.length; i++) {
                      //  c2(D1AG[i]);
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
                    //                            c2(canvasObjects[j]);
                    //                            //alert();
                    //                        }
                    //                    }
                    //                }

                }
                canvas.renderAll();
            }
        }
    }

    document.getElementById('BtnAlignObjLeft').onclick = function (ev) {

        if (canvas.getActiveGroup()) {
            var D1AG = canvas.getActiveGroup().getObjects();
            if (D1AG) {

                //c01
                var minID = 0;
                var mintop = 0;
                var left = 0
                mintop = D1AG[0].top;
                minID = D1AG[0].ObjectID;
                left = D1AG[0].left - D1AG[0].currentWidth / 2;

                if (D1AG) {
                    for (i = 0; i < D1AG.length; i++) {
                        if (D1AG[i].ObjectID != minID) {
                            D1AG[i].left = left + D1AG[i].currentWidth / 2;
                            //c2(D1AG[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < D1AG.length; i++) {
                      //  c2(D1AG[i]);
                    }
                }
                canvas.renderAll();
            }
        }

    }

    document.getElementById('BtnAlignObjRight').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var D1AG = canvas.getActiveGroup().getObjects();
            if (D1AG) {

                var minID = 0;
                var mintop = 0;
                var left = 0

                mintop = D1AG[0].top;
                minID = D1AG[0].ObjectID;
                left = D1AG[0].left + D1AG[0].currentWidth / 2;

                if (D1AG) {
                    for (i = 0; i < D1AG.length; i++) {
                        if (D1AG[i].ObjectID != minID) {
                            D1AG[i].left = left - D1AG[i].currentWidth / 2;
                            //c2(D1AG[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < D1AG.length; i++) {
                     //   c2(D1AG[i]);
                    }
                }
                canvas.renderAll();
            }
        }

        //		var diff =  0;
        //		var D1AG = canvas.getActiveGroup().getObjects();
        //		if(D1AG) {     
        //			for (i=1;i<D1AG.length;i++) {
        //				if (g8(D1AG[0],D1AG[i])) {
        //					D1AG[i].left     = (D1AG[0].width) /2   - (D1AG[i].width) /2; 
        //				}
        //				else {                
        //					diff = D1AG[i].left  - D1AG[0].left  ;
        //					D1AG[i].left     = (D1AG[0].width) /2   - (D1AG[i].width) /2 - diff;   
        //				}
        //			}
        //			//c2(D1AG);
        //			canvas.renderAll();
        //		}
    }

    document.getElementById('BtnAlignObjTop').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var D1AG = canvas.getActiveGroup().getObjects();
            if (D1AG) {

                var minID = 0;
                var minLeft = 99999;
                var top = 0

                minLeft = D1AG[0].left;
                minID = D1AG[0].ObjectID;
                top = D1AG[0].top - D1AG[0].currentHeight / 2;

                if (D1AG) {
                    for (i = 0; i < D1AG.length; i++) {
                        if (D1AG[i].ObjectID != minID) {
                            D1AG[i].top = top + D1AG[i].currentHeight / 2;

                            //c2(D1AG[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < D1AG.length; i++) {
                      //  c2(D1AG[i]);
                    }
                }
                canvas.renderAll();
            }
        }

        //		var D1AG = canvas.getActiveGroup().getObjects();
        //		if(D1AG) {
        //			for (i=1;i<D1AG.length;i++) {
        //				if (g8(D1AG[0],D1AG[i])) {
        //					D1AG[i].top     =  D1AG[0].top/2  -   (D1AG[i].top/2)  ;//((D1AG[0].height/2) * (-1)) +  (D1AG[i].height) /2
        //				}
        //				else {
        //					var diff = D1AG[0].top - D1AG[i].top;
        //					D1AG[i].top     =  D1AG[0].top  -   (D1AG[i].height) /2  - diff ;//((D1AG[0].height/2) * (-1)) +  (D1AG[i].height) /2
        //				}
        //				//alert(D1AG[i].height);
        //			 }
        //			//c2(D1AG);
        //			canvas.renderAll();
        //		}
    }

    document.getElementById('BtnAlignObjMiddle').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var D1AG = canvas.getActiveGroup().getObjects();
            if (D1AG) {

                var minID = 0;
                var minLeft = 99999;
                var top = 0
                //            var len = D1AG.length;
                //            for (var i = 0; i < len; i++) 
                //            {
                //                if (D1AG[i].left < minLeft) 
                //                {
                //                    minLeft = D1AG[i].left;
                //                    minID = D1AG[i].ObjectID;
                //                    top = D1AG[i].top;
                //                }
                //            }

                minLeft = D1AG[0].left;
                minID = D1AG[0].ObjectID;
                top = D1AG[0].top;

                if (D1AG) {
                    for (i = 0; i < D1AG.length; i++) {
                        if (D1AG[i].ObjectID != minID) {
                            D1AG[i].top = top;
                            //c2(D1AG[i]);
                        }
                    }

                    canvas.discardActiveGroup();
                    for (var i = 0; i < D1AG.length; i++) {
                       // c2(D1AG[i]);
                    }
                }
                canvas.renderAll();
            }
        }
    }

    document.getElementById('BtnAlignObjBottom').onclick = function (ev) {
        if (canvas.getActiveGroup()) {
            var D1AG = canvas.getActiveGroup().getObjects();
            if (D1AG) {

                var minID = 0;
                var minLeft = 99999;
                var top = 0

                //c03
                minLeft = D1AG[0].left;
                minID = D1AG[0].ObjectID;
                top = D1AG[0].top + D1AG[0].currentHeight / 2;

                if (D1AG) {
                    for (i = 0; i < D1AG.length; i++) {
                        if (D1AG[i].ObjectID != minID) {
                            D1AG[i].top = top - D1AG[i].currentHeight / 2;
                            //c2(D1AG[i]);
                        }
                    }
                    canvas.discardActiveGroup();
                    for (var i = 0; i < D1AG.length; i++) {
                        //c2(D1AG[i]);
                    }
                }
                canvas.renderAll();
            }
        }
    }
    /////////////////////////////////


    ////////////////////////////////
    document.getElementById('BtnTxtCanvasAlignCenter').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            D1AO.left = canvas.getWidth() / 2;
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        }
    }

    document.getElementById('BtnTxtCanvasAlignLeft').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
            if (!IsBC) {
                D1AO.left = Template.CuttingMargin + D1AO.width / 2 + 8.49;
            } else {
                D1AO.left = Template.CuttingMargin + D1AO.width / 2 + 8.49 + 5;
            }
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        } else {
            if (D1AO) {
                if (!IsBC) {
                    D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
                } else {
                    D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
                }
//c2(D1AO);
                D1AO.setCoords();
                canvas.renderAll();
            }
        }
    }

    document.getElementById('BtnTxtCanvasAlignRight').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO && (D1AO.type == "text" || D1AO.type === 'i-text')) {
            if (!IsBC) {
                D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.width / 2 - 8.49;
            } else {
                D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.width / 2 - 8.49 - 5;
            }
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        } else {
            if (D1AO) {
                if (!IsBC) {
                    D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
                } else {
                    D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
                }
              //  c2(D1AO);
                D1AO.setCoords();
                canvas.renderAll();
            }
        }
    }

    //    document.getElementById('BtnTxtCanvasAlignTop').onclick = function (ev) {
    //        var D1AO = canvas.getActiveObject();
    //        if (D1AO && D1AO.type == "text") {
    //            if(!IsBC) {
    //                D1AO.top = Template.CuttingMargin + D1AO.height / 2 + 8.49;
    //            } else {
    //                D1AO.top = D1AO.height / 2 + 8.49;
    //            }
    //            c2(D1AO);
    //            D1AO.setCoords();
    //            canvas.renderAll();
    //        } else {
    //            if (D1AO) {
    //                if(!IsBC) {
    //                    D1AO.top = Template.CuttingMargin + D1AO.currentHeight / 2 + 8.49;
    //                } else {
    //                    D1AO.top =  D1AO.currentHeight / 2 + 8.49;
    //                }
    //                c2(D1AO);
    //                D1AO.setCoords();
    //                canvas.renderAll();
    //            }
    //        }
    //    }

    document.getElementById('BtnTxtCanvasAlignMiddle').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            D1AO.top = canvas.getHeight() / 2;
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        }
    }

    //    document.getElementById('BtnTxtCanvasAlignBottom').onclick = function (ev) {
    //        var D1AO = canvas.getActiveObject();
    //        if (D1AO  && D1AO.type == "text") {
    //            if(!IsBC) {
    //                D1AO.top = canvas.getHeight() - Template.CuttingMargin - 8.49 - D1AO.height / 2;
    //            } else {
    //                D1AO.top = canvas.getHeight() - 8.49 - D1AO.height / 2;
    //            }
    //            c2(D1AO);
    //            D1AO.setCoords();
    //            canvas.renderAll();
    //        } else {
    //            if (D1AO) {
    //                if(!IsBC) {
    //                     D1AO.top = canvas.getHeight() - Template.CuttingMargin - D1AO.currentHeight / 2 - 8.49;
    //                } else {
    //                    D1AO.top = canvas.getHeight() - D1AO.currentHeight / 2 - 8.49;
    //                }
    //                c2(D1AO);
    //                D1AO.setCoords();
    //                canvas.renderAll();
    //            }
    //        }
    //    }

    ///////////////////////////////
    ////////////////////////////////
    document.getElementById('BtnImgCanvasAlignCenter').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            D1AO.left = canvas.getWidth() / 2;
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        }
    }

    document.getElementById('btnImgCanvasAlignLeft').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            D1AO.left = Template.CuttingMargin + D1AO.currentWidth / 2 + 8.49;
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        }
    }

    document.getElementById('BtnImgCanvasAlignRight').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            D1AO.left = canvas.getWidth() - Template.CuttingMargin - D1AO.currentWidth / 2 - 8.49;
          //  c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        }
    }

    //    document.getElementById('BtnImgCanvasAlignTop').onclick = function (ev) {
    //        var D1AO = canvas.getActiveObject();
    //        if (D1AO) {
    //            D1AO.top = Template.CuttingMargin + D1AO.currentHeight / 2 + 8.49;
    //            c2(D1AO);
    //            D1AO.setCoords();
    //            canvas.renderAll();
    //        }
    //    }

    document.getElementById('BtnImgCanvasAlignMiddle').onclick = function (ev) {
        var D1AO = canvas.getActiveObject();
        if (D1AO) {
            D1AO.top = canvas.getHeight() / 2;
         //   c2(D1AO);
            D1AO.setCoords();
            canvas.renderAll();
        }
    }

    //    document.getElementById('BtnImgCanvasAlignBottom').onclick = function (ev) {
    //        var D1AO = canvas.getActiveObject();
    //        if (D1AO) {
    //            D1AO.top = canvas.getHeight() - Template.CuttingMargin - D1AO.currentHeight / 2 - 8.49;
    //            c2(D1AO);
    //            D1AO.setCoords();
    //            canvas.renderAll();
    //        }
    //    }
    ///////////////////////////////
    document.getElementById('btnDeleteTxt').onclick = function (ev) {
        pcL03();
        if (IsCalledFrom == 3) {
            m0();
        }
    }
    var removeSelectedEl = document.getElementById('BtnDeleteTxtObj');
    removeSelectedEl.onclick = function () {
        pcL03();
    };


    document.getElementById('BtnImageArrangeOrdr1').onclick = function (ev) {
        pcL26();
    }
    document.getElementById('BtnImageArrangeOrdr1Retail').onclick = function (ev) {
        pcL26();
    }
    document.getElementById('BtnImageArrangeOrdr2').onclick = function (ev) {
        pcL18();
    }

    document.getElementById('BtnImageArrangeOrdr3').onclick = function (ev) {
        pcL19();
    }
    document.getElementById('BtnImageArrangeOrdr2Retail').onclick = function (ev) {
        pcL18();
    }

    document.getElementById('BtnImageArrangeOrdr3Retail').onclick = function (ev) {
        pcL19();
    }
    document.getElementById('BtnImageArrangeOrdr4').onclick = function (ev) {
        pcL25();
    }
    document.getElementById('BtnImageArrangeOrdr4Retail').onclick = function (ev) {
        pcL25();
    }
    document.getElementById('BtnImgRotateLeft').onclick = function (ev) {
        pcL16();
    }

    document.getElementById('BtnImgRotateRight').onclick = function (ev) {
        pcL17();
    }
    document.getElementById('BtnImgRotateLeftRetail').onclick = function (ev) {
        pcL16();
    }

    document.getElementById('BtnImgRotateRightRetail').onclick = function (ev) {
        pcL17();
    }
    $("#LockPositionImg").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();

        if (thisCheck.is(':checked')) {
            D1AO.IsPositionLocked = true;
            D1AO.lockMovementX = true;
            D1AO.lockMovementY = true;
            D1AO.lockScalingX = true;
            D1AO.lockScalingY = true;
            D1AO.lockRotation = true;
        }
        else {
            D1AO.IsPositionLocked = false;
            D1AO.lockMovementX = false;
            D1AO.lockMovementY = false;
            D1AO.lockScalingX = false;
            D1AO.lockScalingY = false;
            D1AO.lockRotation = false;
        }
      //  c2(D1AO);

        //        }
        //        else if (D1AO.type ==='group') {
        //            var objectsInGroup = D1AO.getObjects();
        //            objectsInGroup.forEach(function (OPT) {  
        //                if (thisCheck.is (':checked')) {
        //                    OPT.IsPositionLocked = true;       
        //                    OPT.lockMovementX = true;
        //                    OPT.lockMovementY = true;
        //                    OPT.lockScalingX = true;
        //                    OPT.lockScalingY = true;
        //                    OPT.lockRotation = true;
        //                }
        //                else {
        //                    OPT.IsPositionLocked = false;
        //                    OPT.lockMovementX = false;
        //                    OPT.lockMovementY = false;
        //                    OPT.lockScalingX = false;
        //                    OPT.lockScalingY = false;
        //                    OPT.lockRotation = false;
        //                }
        //                c2(OPT);
        //            });

        //        }
    });
    $("#UploadImgBtn").click(function () {
        isBackgroundImg = false;
        pcL36('toggle', '#UploadImage');
        // $("#uploader_browse").trigger("click");

    });
    $("#UploadImgBackgroundBtn").click(function () {
        isBackgroundImg = true;
        pcL36('toggle', '#UploadImage');
        // $("#uploader_browse").trigger("click");
    });

    $("#ChkBoxFontAllowed").click(function () {
        var thisCheck = $(this);

        if (thisCheck.is(':checked')) {
            D1IFL = true;
        }
        else {
            D1IFL = false;
        }


    });
   
    $("#BtnPrintImage").click(function () {
        var thisCheck = $(this);
        var D1AO = canvas.getActiveObject();
        //        if (D1AO.type === 'text') {
        if (thisCheck.is(':checked')) {
            D1AO.IsHidden = true;
        }
        else {
            D1AO.IsHidden = false;
        }
        //c2(D1AO);
        //        }
        //        else if (D1AO.type ==='group') {
        //            var objectsInGroup = D1AO.getObjects();
        //            objectsInGroup.forEach(function (OPT) {  
        //                if (thisCheck.is (':checked')){
        //                    D1AO.IsHidden = true; 
        //                }
        //                else {
        //                    D1AO.IsHidden = false; 
        //                }
        //            });
        //        }        
    });
    // 

    //	document.getElementById('BtnFlipImg1').onclick = function (ev) {
    //		var D1AO = canvas.getActiveObject();
    //		if (D1AO.get('flipX')) {
    //			D1AO.set('flipX', false);
    //		} else {
    //			D1AO.set('flipX', true);
    //		}
    //		c2(D1AO);
    //		canvas.renderAll();
    //	}

    //	document.getElementById('BtnFlipImg2').onclick = function (ev) {
    //		var D1AO = canvas.getActiveObject();
    //		if (D1AO.get('flipY')) {
    //			D1AO.set('flipY', false);
    //		} else {
    //			D1AO.set('flipY', true);
    //		}
    //		c2(D1AO);
    //		canvas.renderAll();
    //	}

    var removeSelectedEl = document.getElementById('btnDeleteImage');
    removeSelectedEl.onclick = function () {
        pcL21();
    };
    $("#btnDelImgRetail").click(function () {
        pcL21();
        if (IsCalledFrom == 3) {
            m0();
        }
    });
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
            //				var D1AO = canvas.getActiveObject(),
            //				D1AG = canvas.getActiveGroup();
            //				if (D1AO || D1AG) {
            //					(D1AO || D1AG).setOpacity(parseInt(this.value, 10) / 100);
            //					canvas.renderAll();
            //				}
            //			};
            //			slider2.onchange = function () {
            //				var D1AO = canvas.getActiveObject(),
            //				D1AG = canvas.getActiveGroup();
            //				if (D1AO || D1AG) {
            //					(D1AO || D1AG).setOpacity(parseInt(this.value, 10) / 100);
            //					canvas.renderAll();
            //				}
            //			};
        })();
    }

    if (supportsColorpicker()) {
        (function () {
            //            var controls = document.getElementById('controls');

            //            var label = document.createElement('label');
            //            label.htmlFor = 'color';
            //            label.innerHTML = 'Color: ';
            //            label.style.marginLeft = '10px';

            //            var colorpicker = document.createElement('input');
            //            colorpicker.type = 'color';
            //            colorpicker.id = 'color';
            //            colorpicker.style.width = '40px';

            //            controls.appendChild(label);
            //            controls.appendChild(colorpicker);

            //            canvas.calcOffset();

            //            colorpicker.onchange = function () {
            //                var D1AO = canvas.getActiveObject(),
            //                D1AG = canvas.getActiveGroup();
            //                if (D1AO || D1AG) {
            //                    (D1AO || D1AG).setFill(this.value);
            //                    canvas.renderAll();
            //                }
            //            };
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
    for (var i = activeObjectButtons.length; i--;) {
        // commented for testing
        //  activeObjectButtons[i].disabled = true;
    }


    //    var drawingModeEl = document.getElementById('drawing-mode'),
    //    drawingOptionsEl = document.getElementById('drawing-mode-options'),
    //    drawingColorEl = document.getElementById('drawing-color'),
    //    drawingLineWidthEl = document.getElementById('drawing-line-width');

    //    drawingModeEl.onclick = function () {
    //        canvas.isDrawingMode = !canvas.isDrawingMode;
    //        if (canvas.isDrawingMode) {
    //            drawingModeEl.text = 'Cancel drawing mode';
    //            drawingModeEl.className = 'is-drawing';
    //            drawingOptionsEl.style.display = 'none';
    //        }
    //        else {
    //            drawingModeEl.innerHTML = '';
    //            drawingModeEl.className = '';
    //            drawingOptionsEl.style.display = 'none';

    //        }
    //    };

    //c19
    //    canvas.observe('path:created', function (newPath) {

    //        var OP = newPath.memo.path;
    //        var D1SVO = {};
    //        D1SVO = fabric.util.object.clone(TO[0]);

    //        D1SVO.Name = "Path";
    //        D1SVO.ObjectID = --NCI;
    //        D1SVO.ColorHex = "#000000";
    //        D1SVO.ColorC = 0;
    //        D1SVO.ColorM = 0;
    //        D1SVO.ColorY = 0;
    //        D1SVO.ColorK = 100;
    //        D1SVO.IsBold = false;
    //        D1SVO.IsItalic = false;
    //        D1SVO.ObjectType = 9; //c20
    //        D1SVO.ProductPageId = SP;
    //        D1SVO.MaxWidth = OP.width;
    //        D1SVO.MaxHeight = OP.height;
    //        D1SVO.ExField1 = OP.strokeWidth;

    //        D1SVO.$id = (parseInt(TO[TO.length - 1].$id) + 4);
    //        D1SVO.ContentString = OP.toSVG();

    //        OP.set({
    //            borderColor: 'red',
    //            cornerColor: 'green',
    //            cornersize: 6
    //        });
    //        OP.ObjectID = D1SVO.ObjectID;
    //        D1SVO.PositionX = OP.left - OP.width / 2;
    //        D1SVO.PositionY = OP.top - OP.height / 2;
    //        TO.push(D1SVO);
    //    });

    //    drawingColorEl.onchange = function () {
    //        canvas.freeDrawingColor = drawingColorEl.value;
    //    };

    //    drawingLineWidthEl.onchange = function () {
    //        canvas.freeDrawingLineWidth = parseInt(drawingLineWidthEl.value, 10) || 1; // disallow 0, NaN, etc.
    //    };

    //    canvas.freeDrawingColor = drawingColorEl.value;
    //    canvas.freeDrawingLineWidth = parseInt(drawingLineWidthEl.value, 10) || 1;

    //	document.onkeydown = function (e) {
    //		var obj = canvas.getActiveObject() || canvas.getActiveGroup();
    //		if (obj && e.keyCode === 8) {
    //			// this is horrible. need to fix, so that unified interface can be used
    //			if (obj.type === 'group') {
    //				// var GO = obj.getObjects();
    //				//         canvas.discardActiveGroup();
    //				//         GO.forEach(function(obj) {
    //				//           canvas.remove(obj);
    //				//         });
    //			}
    //			else {
    //				//canvas.remove(obj);
    //			}
    //			canvas.renderAll();
    //			// return false;
    //		}
    //	};

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
            var D1AO = canvas.getActiveObject();
            if (D1AO && D1AO.type === 'text') {
                D1AO.textDecoration = (D1AO.textDecoration == 'underline' ? '' : 'underline');
                this.className = D1AO.textDecoration ? 'selected' : '';
                canvas.renderAll();
            }
        };
    }

    var cmdLinethroughBtn = document.getElementById('text-cmd-linethrough');
    if (cmdLinethroughBtn) {
        activeObjectButtons.push(cmdLinethroughBtn);
        cmdLinethroughBtn.disabled = true;
        cmdLinethroughBtn.onclick = function () {
            var D1AO = canvas.getActiveObject();
            if (D1AO && D1AO.type === 'text') {
                D1AO.textDecoration = (D1AO.textDecoration == 'line-through' ? '' : 'line-through');
                this.className = D1AO.textDecoration ? 'selected' : '';
                canvas.renderAll();
            }
        };
    }

    var cmdOverlineBtn = document.getElementById('text-cmd-overline');
    if (cmdOverlineBtn) {
        activeObjectButtons.push(cmdOverlineBtn);
        cmdOverlineBtn.disabled = true;
        cmdOverlineBtn.onclick = function () {
            var D1AO = canvas.getActiveObject();
            if (D1AO && D1AO.type === 'text') {
                D1AO.textDecoration = (D1AO.textDecoration == 'overline' ? '' : 'overline');
                this.className = D1AO.textDecoration ? 'selected' : '';
                canvas.renderAll();
            }
        };
    }

})(this);
