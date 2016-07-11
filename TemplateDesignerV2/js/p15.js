$(document).ready(function () {
    StartLoader();
    a5();
    a2();
    a1();

    //$('#spanLblTransparency').text("Layers");
    $('.divGlobBkImgContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlImExBk) {
                pcL30();
            }
        }
    });
    $('.divTempBkImgContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (TeImExBk) {
                pcL31();
            }
        }
    });
    $('.divPersBkImgContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (UsImCBkEx) {
                pcL32();
            }
        }
    });
    $('.divGlobImgContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlImEx) {
                pcL33();
            }
        }
    });
    $('.divTempImgContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (TeImCEx) {
                pcL34();
            }
        }
    });
    $('.divPersImgContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (UsImEx) {
                pcL35();
            }
        }
    });
    $('.divLogosContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlLogCnEx) {
                k21Log();
            }
        }
    });
    $('.divShapesContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlShpCnEx) {
                k21Sh();
            }
        }
    });
    $('.divIllustrationContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlIllsEx) {
                k24ilus();
            }
        }
    });
    $('.divFramesContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlFramesEx) {
                k24frames();
            }
        }
    });
    $('.divBannersContainer').bind('scroll', function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= this.scrollHeight) {
            if (GlBannerEx) {
                k24banners();
            }
        }
    });
    $('input:checkbox').focus(function () {
        var D1A0 = canvas.getActiveObject();
        if (!D1A0) return;
        if (D1A0.isEditing) {
            D1A0.exitEditing(); canvas.renderAll();
        }
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
});
$(window).scroll(function () {
    $('.DivToolTipStyle').css('position', 'fixed');
    $('#textPropertPanel').css('position', 'fixed');
    $('.panelBasics').css('position', 'fixed');
    $('#ImagePropertyPanel').css('position', 'fixed');
    $('.panelBasics').css("top", "245px");
    $("#quickTextFormPanel").css('position', 'fixed');
    $("#AddTextDragable").css('position', 'fixed');
    $("#addImage").css('position', 'fixed');
    $("#divImageDAM").css('position', 'fixed');
    $("#divImageEditScreen").css('position', 'fixed');
    $("#DivLayersPanel").css('position', 'fixed');
    if (IsCalledFrom == 3) {
        $("#divLayersPanelRetail").css('position', 'fixed');
    }
    $("#DivColorPickerDraggable").css('position', 'fixed');
    canvas.calcOffset();
});
function a1() {
    $("#DivColorC,#DivColorM,#DivColorY, #DivColorK").slider({
        orientation: "horizontal",
        range: "min",
        max: 100,
        change: f4,
        slide: f4
    });
}
var logAction = function (msg) {
    $("#actions").append(
            $("<li>").html(msg)
    );
}
var IsEmbedded = true;   //l1
var IsCalledFrom = 1;
var Territory = 0;
var TemplateID = 0;
var Template;
var CustomerID = 0;
var TempChkQT = 0;
var printCropMarks = false;
var printWaterMarks = false;
var isPinkCards = false;
var ShowBleedArea = true;
var orderCode = null;
var ssMsg = "Please confirm spellings and details!";
var CustomerName = null;
var TemplateFonts = [];
var ContactID;
var TotalImgLoaded = 0;
var TP = [];
var TO = [];
var TORestore = [];
var TPRestore = [];
var SP = 0;
var SPN = 0;
var NCI = -1;
var ISG = true;
var ISG1 = true;
var SXP = [];   
var SYP = [];   
var QTD = 0;
var IH;
var IW;
var IC = 0;  
var MCL = [];
var ICI = 0;
var TIC = 0;
var LIFT = true;
var N1FL = null;
var T0FU = [];
var T0FN = [];
var TOFZ = 10;
var IsBC = false;
var IsBCAlert = false;
var IsBCFront = true;
var BCBackSide = 1;
var IsBCRoundCorners = false;
var LiImgs = [];
var TeImC = 1;
var GlImC = 1;
var UsImC = 1;
var TeImCBk = 1;
var GlImCBk = 1;
var GlShpCn = 1;
var GlLogCn = 1;
var GlLogCnP = 1;
var UsImCBk = 1;
var GlIlsC = 1;
var GlframC = 1;
var GlBanC = 1;
var GlImEx = true,TeImCEx = true;
var UsImEx = true;
var TeImExBk = true;
var GlImExBk = true;
var GlShpEx = true;
var GlLogEx = true;
var UsImCBkEx = true;
var GlShpCnEx = true;
var GlLogCnEx = true;
var GlLogCnExP = true;
var GlIllsEx = true;
var GlFramesEx = true;
var GlBannerEx = true;
var isBackgroundImg = false;
var imgSelected = 0;
var isImgUpl = false;
var isBKpnl = false;
var llData = [];
var slLLID = 0;
var isLoading = true;
var highlightEditableText = true;
var udCutMar = 0;
var bleedPrinted = false;
var dfZ1l = 1;
var varList = [];
var isRealestateproduct = false;
var allowPdfDownload = false;
var allowImgDownload = false;
var isMultiPageProduct = false;
var propertyID = 0;
var propertyImages = [];
function a2() {
    $("#loadingMsg").html("Loading Design, please wait..");
    TemplateID = getParameterByName("TemplateID");   
    if (TemplateID != "" || TemplateID != null) {
        a9();
    }
    else {
    }
}
function a3(Count) {
    ICI += 1;
    if(Count == ICI)
    {
        var oldSize = MCL.length;
        var LA1 = [];
        $.getJSON("Services/imageSvc/" + TemplateID,
            function (DT) {
                LiImgs = DT;
                var sizee = DT.length;
                $.each(DT, function (i, IT) {
                    var obj = {
                        url: "./" + IT.BackgroundImageRelativePath,
                        title: IT.ID,
                        index: TemplateID
                    }
                    LA1.push(obj);
                });
                MCL = [];
                MCL = LA1;
                for (var i = oldSize; i < DT.length; i++) {
                    var width = 117 * DT.length; ;
                    width = width + "px";
                    $("#CarouselImages").css("width", width);
                    $("#CarouselImages").append(b0(LA1[i]));
                    $(".draggable2").draggable({
                        snap: '#dropzone',
                        snapMode: 'inner',
                        revert: 'invalid',
                        helper: 'clone',
                        appendTo: "body",
                        cursor: 'move'

                    });
                }
            });
    }	 
}
function a4() {
    SP = TP[0].ProductPageID;
    SPN = TP[0].PageNo;
    d5(SP);
    if (IsEmbedded) {
      
        $(".QtxtChkContainer").css("display", "none");
        if (IsCalledFrom == 3) {
            $("#btnImgPanel").css("display", "none");
            //$("#btnShowIcons").css("display", "none");
            //$("#btnShowLogo").css("display", "none");
            $("#btnNewTxtPanel").css("display", "none");
            $("#divLayersPanelCaller").css("display", "none");
            $("#DivControlPanel1").css("width", "610px");
            $("#DivControlPanelDraggable").css("left", "328px");
            $("#divLayersPanelRetail").css("top", "11px");
         //   animatedcollapse.show('addImage');
        }
        if (TempChkQT > 0) {
            $(".panelQuickTexthead").html("Save your quick text profile (Q Text)");
            $(".qSubHeading").css("display", "none");
            $("#quickTextFormPanel").css("width", "306px");
            $(".qLabel").css("float", "left");
            $(".qLabel").css("padding-top", "8px");
            $(".panelQuickTextFormRow INPUT").css("width", "270px");
            $(".panelQuickTextFormRow INPUT").css("-moz-border-radius", "5px");
            $(".panelQuickTextFormRow INPUT").css("-webkit-border-radius", "5px");
            $(".panelQuickTextFormRow INPUT").css("-khtml-border-radius", "5px");
            $(".panelQuickTextFormRow INPUT").css("border-radius", "5px");
            if (IsCalledFrom != 3) {
                pcL36('show', '#quickTextFormPanel');
//                animatedcollapse.show('');
            }
//            $("#BtnUndo").css("display", "none");
//            $("#BtnRedo").css("display", "none");
//            $("#spanUndo").css("display", "none");
//            $("#spanRedo").css("display", "none");

        } else {
            $("#BtnQuickTextForm").css("visibility", "hidden");
            $("#lblQuickTxtBtn").css("visibility", "hidden");
        }
        $("#btnAddImagePlaceHolder").css("display", "none");
        $(".spanImgPlaceHolder").css("display", "none");
        $("#btnCompanyPlaceHolder").css("display", "none");
        $("#btnContactPersonPlaceHolder").css("display", "none");
    } else {
        $("#btnNextStepBC").text("Proofs");
    }
    if (TIC == TotalImgLoaded && LIFT) {   
        StopLoader();
        LIFT = false;	
    }
    canvas.renderAll();

}
function a5() {
    $("#loadingMsg").html("Loading UI, please wait..");
    $("input:submit, button", ".designer").button();
    $("#BtnSave").button();
    $("#BtnContinue").button();
    $("#QuickTextButton").button();
    $("#uploadImgFileBtn1").button();
    $("#uploadFontFileBtn").button();
    $(".btnBlueProofing").button();
    $(".plupload_button").button();
    $('#BtnAddNewText').button().draggable({cancel:false});
    $('#btnAddRectangle').button().draggable({ cancel: false });
    $('#btnAddImagePlaceHolder').button().draggable({ cancel: false });
    $('#btnCompanyPlaceHolder').button().draggable({ cancel: false });
    $('#btnContactPersonPlaceHolder').button().draggable({ cancel: false });
    $('#btnAddCircle').button().draggable({cancel:false});
    $("a", ".demo").click(function () { return false; });
    $('.DivToolTipStyle').css("left", ($(window).width() / 2 +149.5) + "px");
    $('.DivToolTipStyle').css("top", "162px");
    $('.divControlPanel').css("top", "-60px");
    $('.divControlPanel').css("left", "5px");// ($(window).width() / 2 - 500 - 7.5) + "px");
    $(".divPresetEditor").css('top', '90px');
    $(".divPresetEditor").css('position', 'fixed');
    $('.divPresetEditor').css("left", "5px"); //($(window).width() / 2 + 500 - 12.5 - $('.DivAlignObjs').width()) + "px");
    //    $("#divBCMenu").css("top", "0px");
    //    $('#divBCMenu').css("left", ($(window).width() / 2 + 40) + "px");
    $("#divBCMenuPresets").css("top", "19px");
    $('.divBCMenuPresets').css("left", "5px");//($(window).width() / 2 + $('.divBCMenuPresets').width() / 2 -140) + "px");
    $("#ZoomTools").css("top", "0px");
    $('#ZoomTools').css("left", ($(window).width() / 2 - 500 - 7.5 + 560) + "px");
    if (IsCalledFrom == 3 || IsCalledFrom == 4) {
        $('#ZoomTools').css("left", ($(window).width() / 2 - 500 - 7.5 + 566) + "px");
    }


    $("#btnShowMoreOptions").css("left", ($(window).width() / 2 + 500 - 7.5 - $('#btnShowMoreOptions').width()) + "px");
    $('#btnShowMoreOptions').css("top", "161px");
    $("#quickTextFormPanel").css("top", "90px");
    $("#quickTextFormPanel").css("left","5px");// ($(window).width() / 2 - 500 - 7.5) + "px");
    $(".addimgPanel").css("left", ($(window).width() / 2 + 500 - 8.5 - 9 - $('.addimgPanel').width()) + "px");
    $("#addImage").css("top", "11px");
    $("#DivLayersPanel").css("top", "90px");
    $("#DivLayersPanel").css("left", "5px");// ($(window).width() / 2 + 500 - 8.5 - 14 - $('#DivLayersPanel').width()) + "px");
    if (IsCalledFrom == 3) {
        $("#divLayersPanelRetail").css("top", "90px");
        $("#divLayersPanelRetail").css("left", "5px");
    }
    $("#divVariableContainer").css("top", "11px");
    $("#divVariableContainer").css("left", ($(window).width() / 2 + 500 - 8.5 - 9 - $('#divVariableContainer').width()) + "px");

    $("#AddTextDragable").css("top", "81px");
    $("#AddTextDragable").css("left", "5px");//($(window).width() / 2 + 500 - 8.5 - 4 - $('#addText').width()) + "px");
    if (IsCalledFrom == 3) {
        $("#AddTextDragable").css("top", "88px");
        $("#AddTextDragable").css("left", "18px");
        $("#addText").css("width", "273px");
        $("#txtAddNewText").css("width", "243px");
        $("#txtAddNewText").css("margin-left", "10px");
    }
    $("#textPropertPanel").css("left", "5px");//($(window).width() / 2 - 500 - 7) + "px");
    $("#textPropertPanel").css("top", "90px");
    $("#ImagePropertyPanel").css("top", "90px");
    $("#ImagePropertyPanel").css("left", "5px");//($(window).width() / 2 - 500 - 7) + "px");
    $("#DivControlPanelDraggable").css('position', 'fixed');
    $('#DivControlPanelDraggable').css("top", "71px");
    $('#DivControlPanelDraggable').css("left", "0px");
    $('#DivControlPanelDraggable').css("top", "71px");
    $('#DivControlPanelDraggable').css("left", "0px");
    $('#DivColorPickerDraggable').css("top", "160px");
    $('#DivColorPickerDraggable').css("position", "fixed");
    $("#DivColorPickerDraggable").css("left", "5px");// ($(window).width() / 2 - 500 - 7) + "px");
    //$('#DivColorPickerDraggable').css("left", ($(window).width() / 2 + 500 -11- $('#DivAdvanceColorPanel').width()) + "px");
    $(".DivAlignObjs").css('position', 'fixed');
   // $(".divPresetEditor").corner("7px;");
    $('#DivCropToolContainer').css('position', 'fixed');
    $('#DivCropToolContainer').css('top', '89px');
    $("#DivCropToolContainer").css("left", "6px");//($(window).width() / 2 - 500 - 7) + "px");
    $('.DivAlignObjs').css("left", ($(window).width() / 2 + 500 - 12.5  - $('.DivAlignObjs').width()) + "px");
 //   $(".divPanelCaller").corner("7px;");
  //  $(".DivAlignObjs").corner("7px;");
    $(".DivAlignObjs").css('top', '11px');
  //  $(".divPositioningPanel").corner("7px;");
    $(".divPositioningPanel").css('position', 'fixed');
    $('.divPositioningPanel').css("left", ($(window).width() / 2 - 500  -7) + "px");
    $('.divPositioningPanel').css("top", "364px");
    if (IsCalledFrom == 1 || IsCalledFrom == 2) {
        $('.divPositioningPanel').css("top", "499px");
    }
    $('#DivToolTip').css("top", "90px");
    //$(".divControlPanel").corner("7px");
    //$(".divBCMenu").corner("7px");
    //$(".popUpaddTextPanel").corner("7px"); 
    //$("#DivLoading").corner("7px");
    //$("#DivPersonalizeTemplate").corner("7px");
    //$("#addText").corner("7px;");
    //$("#addImage").corner("7px;");
    //$("#divImageDAM").corner("7px;");
    //$("#divImageEditScreen").corner("7px;");
    //$("#DivLayersPanel").corner("7px;");
    //$("#quickText").corner("7px;");
    //$("#UploadImage").corner("7px;");
    //$("#addTextArea").corner("7px;");
    //$("#ImagePropertyPanel").corner("7px;");
    //$("#ShapePropertyPanel").corner("7px;");
    //$(".textPropertyPanel1").corner("7px;");
    //$(".textPropertyPanel2").corner("7px;");
    //$("#quickTextFormPanel").corner("7px;");
    //$("#divVariableContainer").corner("7px;");
    //$("#AddColorShape").corner("7px;");
    //$("#DivToolTip").corner("7px;");
    //$("#DivToolTipHeader").corner("7px;");
    //$("#DivPersonalizeHeader").corner("7px;");
    //$("#DivUploadFont").corner("7px;");
    //$("#DivColorPallet").corner("7px;");
    //$(".ColorPallet").corner("7px;");
    //$("#DivAdvanceColorPanel").corner("7px;");
    //$("#DivColorPallet").corner("7px;");
    //$("#DivCropToolContainer").corner("7px;");
    //$("#divBCMenuPresets").corner("7px;");
    $('#divImageDAM').css("top", "90px");
    if(IsCalledFrom == 1 ) {
        $('.DamImgContainer').css("height", "311px");
    } else {
        $('.DamImgContainer').css("height", "243px");
    }
    $('#divImageDAM').css("left", "5px");
    $('#divImageDAM').css("position", "fixed");
    $('#divImageEditScreen').css("top", "90px");
    $('#divImageEditScreen').css("position", "fixed");
//	$("#canvas").corner("17px;");
//	$(".upper-canvas").corner("17px;");
//	$(".canvas-container").corner("17px;");
    //	$(".lower-canvas").corner("17px;");
    $("#ImgCarouselDiv").tabs();
    $("#BkImgContainer").tabs();
    $("#LogosContainer").tabs();
    $("#ShapesContainer").tabs();
    $(".divbtnNextBC").css("left", ($(window).width() / 2 + 500 - 100) + "px");
    if (d0("IsTipEnabled") == "0") {
        $("#SpanTips").html("Show Tips");  
    } else {
        $("#SpanTips").html("Hide Tips");
    }

    $(".QuickTextText").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });


    $(".divVar").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#BtnAddNewText").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#btnAddRectangle").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#btnAddImagePlaceHolder").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#btnCompanyPlaceHolder").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#btnContactPersonPlaceHolder").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#btnAddCircle").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
    $("#progressbar").progressbar({
        value: 2
    });
    

    var progressbar = $("#progressbar"),
    progressLabel = $(".progress-label");

    progressbar.progressbar({
        value: false,
        change: function () {
            progressLabel.text(progressbar.progressbar("value") + "%");
        },
        complete: function () {
            progressLabel.text("Upload Complete Getting file records!");
            $(".progress-label").css("left", "9%");
        }
    });
       
    var is_chrome = /chrome/i.test(navigator.userAgent);
    if (is_chrome) {
     //   $("#BtnImgRotateLeft").css("margin-left", "12px");
        $("#txtQAddress").css("width", "186px");
       
    }
    if (IsCoorporate == 1) {
        $("#BtnQuickTextForm").css("visibility", "hidden");
        $(".spanQuickTxt").css("visibility", "hidden");
    } else {
       // $("#BtnUploadFont").css("display", "none");
    }
    if ($.browser.mozilla) {
        $("#BtnPasteObj").css("margin-left", "9px");
        $(".popupUpdateTxt").css("padding", "1px 1px 1px 1px");
        $("#btnNewTxtPanel").css("margin-left", "22px");
    }
    if (IsEmbedded == false) {
        performCoorporate();
    }
    $("#textPropertPanel").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $("#divVariableContainer").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $("#ImagePropertyPanel").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $("#DivColorPickerDraggable").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #DivColorContainer"
    });
    $("#DivToolTip").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $("#quickTextFormPanel").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $("#AddTextDragable").draggable({

        appendTo: "body",
        cursor: 'move'
    });

    $("#addImage").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #CarouselDiv"
    });
    $("#divImageDAM").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #ImgCarouselDiv #BkImgContainer "
    });
    $("#divImageEditScreen").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "input"
    });
    $("#DivAlignObjs").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #CarouselDiv"
    });
    $("#divPositioningPanel").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #divTxtPositoning"
    });
    $("#DivLayersPanel").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #LayerObjectsContainer"
    });
    if (IsCalledFrom == 3) {
        $("#divLayersPanelRetail").draggable({

            appendTo: "body",
            cursor: 'move',
            cancel: "div #LayerObjectsContainerRetail"
        });
    }
    $("#DivControlPanelDraggable").draggable({
        appendTo: "body",
        cursor: 'move',
        cancel: "div #CarouselDiv "
    });
    $("#inputPositionX").spinner({
        spin: k5,
        stop:k5
    });
    $("#inputPositionY").spinner({
        spin: k5,
        stop: k5
    });
    $("#inputObjectWidth").spinner({
        spin: k7,
        stop: k7
    });
    $("#inputObjectAlpha").spinner({
        change: k7_trans,
        stop: k7_trans
    });
    $("#inputObjectHeight").spinner({
        spin: k6,
        stop: k6
    });
    $("#inputObjectWidthTxt").spinner({
        spin: k7,
        stop: k7
    });
    $("#inputObjectHeightTxt").spinner({
        spin: k6,
        stop: k6
    });
    $("#inputPositionXTxt").spinner({
        spin: k5,
        stop: k5
    });
    $("#inputPositionYTxt").spinner({
        spin: k5,
        stop: k5
    });
    $("#inputcharSpacing").spinner({
        step: 0.1,
        numberFormat: "n",
        change: k8,
        stop: k8
    });
    
    $("#LargePreviewer").dialog({
        autoOpen: false,
        height: $(window).height() -40,
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
    $(".transparencySlider").slider({
        range: "min",
        value: 1,
        min: 1,
        max: 100,
        slide: function (event, ui) {
            k7_trans_retail(ui.value);
 //           $("#amount").val("$" + ui.value);
        }
    });
//    $("#BtnFontSize").spinner({
//        step: 0.1,
//        numberFormat: "n",
//        change: l1,
//        stop: l1
//    });
    $("#txtLineHeight").spinner({
        step: 0.01,
        numberFormat: "n",
        change: k15,
        stop: k15
    });
    var listFontSize = [];
    for (var i = 1; i <= 200; i++) {
        var It = i * 0.5;
        var it2 = It + "";
        listFontSize.push(it2);
    }
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
    $("#BtnFontSize").autocomplete({
        source: listFontSize,
        minChars: 0,
        change: function (event, ui) {
            var fz = $('#BtnFontSize').val();
            k12(fz);
        },
        select: function (event, ui) {
            var fz = ui.item.value;
            k12(fz);
        },
        search: function (event, ui) {
            var fz = $('#BtnFontSize').val();
            k12(fz);
        }
    });
    $("#BtnFontSizeRetail").autocomplete({
        source: listFontSize,
        minChars: 0,
        change: function (event, ui) {
            var fz = $('#BtnFontSizeRetail').val();
            k12(fz);
        },
        select: function (event, ui) {
            var fz = ui.item.value;
            k12(fz);
        },
        search: function (event, ui) {
            var fz = $('#BtnFontSizeRetail').val();
            k12(fz);
        }
    });
     //.focus(function () {
       // $(this).trigger('keydown.autocomplete');
      //  $(this).autocomplete("search", "");
    //}) ;



    $("#canvas").droppable({
        activeClass: "custom-state-active",
        accept: function (dropElem) {
            var draggable = dropElem.attr('id');
            if (draggable == "BtnAddNewText") {
                if ($("#IsQuickTxtCHK").is(':checked')) {
                    var val1 = $("#txtQTitleChk").val();
                    if (val1 == "") {
                        return false;
                    } else {
                        return true;
                    }
                } else {
                    return true;
                }

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
                    if (ui.draggable.attr('class') == "imgCarouselDiv draggable2 bkImg  ui-draggable") {
                        var src = "" + ui.draggable.attr('src');
                        var id = ui.draggable.attr('id');
                        k32(id, TemplateID, src);
                    } else {
                        var pos = canvas.getPointer(event);
                        var currentPos = ui.helper.position();
                        var draggable = ui.draggable;
                        var url = "";
                        if (draggable.attr('src').indexOf('.svg') == -1) {
                            var p = draggable.attr('src').split('_thumb.');
                            url +=p[0]+ "." + p[1];
                       
                        } else {
                            url = draggable.attr('src');
                        }
                        // l2
                        b4(url);
                       // e0(); // l3
                        if (url.indexOf(".svg") == -1) {
                            d1ToCanvas(url, pos.x, pos.y, IW, IH);
                        } else {
                            d1SvgToCCC(url, IW, IH);
                        }
                        //d1ToCanvas(draggable.attr('src'), pos.x, pos.y, IW, IH);
                    }
                }
            }
            else if (ui.draggable.attr('class') == "ui-state-default ui-sortable-helper" || ui.draggable.attr('id') == "sortableLayers" || ui.draggable.attr('id') == "DivLayersPanel" || ui.draggable.attr('id') == "divLayersPanelRetail" || ui.draggable.attr('id') == "ImagePropertyPanel" || ui.draggable.attr('id') == "DivColorPickerDraggable" || ui.draggable.attr('id') == "quickTextFormPanel" || ui.draggable.attr('id') == "AddTextDragable" || ui.draggable.attr('id') == "addImage" || ui.draggable.attr('id') == "divImageDAM" || ui.draggable.attr('id') == "divImageEditScreen" || ui.draggable.attr('id') == "DivControlPanelDraggable" || ui.draggable.attr('id') == "DivAlignObjs" || ui.draggable.attr('id') == "divPositioningPanel" || ui.draggable.attr('id') == "divVariableContainer") {
                //l4
            } else {
                var pos = canvas.getPointer(event);
                var draggable = ui.draggable.attr('id');
                if (draggable == "QuickTxtAllFields") {
                   // e0(); // l3
                    g9(draggable, pos.x, pos.y);
                } else if (draggable == "BtnAddNewText") {
                    //e0(); // l3
                    if ($('#txtAddNewText').val() != "") {
                        if ($("#IsQuickTxtCHK").is(':checked')) {
                            var val1 = $("#txtQTitleChk").val();
                            var val2 = TOFZ++; // $("#TxtQSequence").val();
                            var val3 = $('#txtQWaterMark').val();
                            g0(pos.x, pos.y, true, val1, val2, val3);
                        } else {
                            g0(pos.x, pos.y, false, "", "", "");
                        }

                    }
                    //return false;

                } else if (draggable == "btnAddRectangle") {
                    pcL36('hide', '#DivLayersPanel');
                    //e0(); // l3
                    h1(pos.x, pos.y);

                } else if (draggable == "btnAddCircle") {
                    pcL36('hide', '#DivLayersPanel');
                   // e0(); // l3
                    h2(pos.x, pos.y);
                } else if (draggable == "btnAddImagePlaceHolder") {
                    pcL36('hide', '#DivLayersPanel');
                    d1PlaceHoldToCanvas(pos.x, pos.y);
                } else if (draggable == "btnCompanyPlaceHolder") {
                    pcL36('hide', '#DivLayersPanel');
                    d1CompanyLogoToCanvas(pos.x, pos.y);
                } else if (draggable == "btnContactPersonPlaceHolder") {
                    pcL36('hide', '#DivLayersPanel');
                    d1ContactLogoToCanvas(pos.x, pos.y);
                } else {
                    if (draggable == "QuickTxtName" || draggable == "QuickTxtTitle" || draggable == "QuickTxtCompanyName" || draggable == "QuickTxtCompanyMsg" || draggable == "QuickTxtAddress1" || draggable == "QuickTxtTel" || draggable == "QuickTxtFax" || draggable == "QuickTxtEmail" || draggable == "QuickTxtWebsite" || draggable == "QuickTxtMobile" || draggable == "QuickTxtTwitter" || draggable == "QuickTxtFacebook" || draggable == "QuickTxtLinkedIn" || draggable == "QuickTxtOtherID") {

                       // e0(); // l3  

                        f3(draggable, pos.x, pos.y);
                    } else {
                        if (ui.draggable.attr('class') == "divVar ui-draggable") {
                            var txt = " " + $(ui.draggable).html() + " ";
                            var DIAO = canvas.getActiveObject();
                            if (!DIAO) return;
                            if (DIAO.isEditing) {
                                if (IsCalledFrom == 2) {
                                    var id = $(ui.draggable).attr("id");
                                    var objToAdd = { "VariableTag": txt, "VariableID": id, "TemplateID": TemplateID };
                                    varList.push(objToAdd);
                                }
                                for (var i = 0; i < txt.length; i++) {
                                    DIAO.insertChars(txt[i]);
                                }
                            }
                            //insertAtCaret("txtAreaUpdateTxt", txt);
                        }
                    }
                }
            }
        }
    });
    if (IsCalledFrom == 3 || IsCalledFrom == 4) {
        $(".previewBtnContainer").css("display", "none");
        $(".PreviewerDownloadPDF").css("display", "none");
        $("#BtnQuickTextSave").css("margin-right", "16px");
    }
    if (allowImgDownload && IsCalledFrom != 2) {
        $(".PreviewerDownloadImg").css("display", "block");
    } else {
        $(".PreviewerDownloadImg").css("display", "none");
    }
    if (allowPdfDownload) {
        $(".previewBtnContainer").css("display", "block"); 
        $(".PreviewerDownloadPDF").css("display", "block");
    } else {
        $(".previewBtnContainer").css("display", "none");
        $(".PreviewerDownloadPDF").css("display", "none");
    }
     if ( IsCalledFrom == 4) {

    }
}

function a6() {
    
    var Cid = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        Cid = CustomerID;    
    }
    $.getJSON("services/TemplateSvc/GetColor/" + TemplateID + "," + Cid,
        function (DT) {
            if (IsCalledFrom == 2 || IsCalledFrom == 4) {
                var html = "<div id='tabs'><ul class='tabsList'><li><a href='#tabsActiveColors'>Active</a></li><li class='inactiveTabs'><a href='#tabsInActiveColors'>Disabled</a></li></ul><div id='tabsActiveColors' class='ColorTabsContainer'></div><div id='tabsInActiveColors' class='ColorTabsContainer'></div></div>";
                $('#DivColorContainer').append(html);
                $("#divRetailColorGroup").css("display", "none");
            }
            $.each(DT, function (i, IT) {
                a7(IT.ColorC, IT.ColorM, IT.ColorY, IT.ColorK, IT.SpotColor, IT.IsColorActive, IT.PelleteID);
            });
            $("#tabs").tabs();
            if (IsCalledFrom == 4) {
                $("#BtnAdvanceColorPicker").css("visibility", "hidden");
                $(".tabsList").css("display", "none");
                $("#tabs").css("padding", "0px");
                $(".btnDeactiveColor").css("visibility", "hidden");
            }
            if (IsCalledFrom == 3) {
                $("#DivColorContainer .ColorPallet").css("-moz-box-shadow", "inset 2px 2px 1px rgba(0, 0, 0, 0.15), inset -1px -1px 0 rgba(255, 255, 255, 0.25)");
                $("#DivColorContainer .ColorPallet").css("-webkit-box-shadow", "inset 2px 2px 1px rgba(0, 0, 0, 0.15), inset -1px -1px 0 rgba(255, 255, 255, 0.25)");
                $("#DivColorContainer .ColorPallet").css("box-shadow", "inset 2px 2px 1px rgba(0, 0, 0, 0.15), inset -1px -1px 0 rgba(255, 255, 255, 0.25)");
                $("#DivColorContainer .ColorPallet").css("-moz-border-radius", "30px");
                $("#DivColorContainer .ColorPallet").css("-webkit-border-radius", "30px");
                $("#DivColorContainer .ColorPallet").css("border-radius", "30px");
                $("#DivColorContainer .ColorPallet").css("height", "41px");
                $("#DivColorContainer .ColorPallet").css("width", "41px");
                $(".divControlPanel").css("width", "975px");
                $("#BtnAddNewText").css("display", "none");
                $(".retailTxtBtns").css("display", "block"); 
                $("#addText").css("height", "230px");
              //  $("#btnShowIcons, #btnShowLogo, #spanShowIcons, #spanLogos").css("display", "inline-block");
                if (isPinkCards.toString() == "true") {
                    //$("#BtnUndo").css("display", "none");
                    $("#BtnCopyObj").css("display", "none");
                    $(".lblCopyBtn").css("display", "none");
                    $("#spanUndo").css("display", "none");
                    $("#BtnPasteObj").css("margin-left", "24px");
                    
                }
                $("#BtnAdvanceColorPicker").css("background", "url('assets/AddColour.png')");
                $("#BtnAdvanceColorPicker").css("background-repeat", "no-repeat");
                $("#BtnAdvanceColorPicker").css("width", "45px");
                $("#BtnAdvanceColorPicker").css("height", "45px");
                $("#BtnAdvanceColorPicker").css("bottom", "-8px");
                $("#BtnAdvanceColorPicker").css("border", "none");
                $("#BtnAdvanceColorPicker").text("");
            }
        });

}
function a7(c, m,y,k,Sname,IsACT,PID) {
    var Color = getColorHex(c, m, y, k);
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        if (IsACT == true) {
            var html = "<div id ='pallet" + PID + "' class ='ColorPalletCorp' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "><button  id ='btnClr" + PID + "' class='btnDeactiveColor' title='Deactivate this color' onclick='j7(" + PID + ",&quot;DeActive&quot;);'></button></div><div  id ='textColor" + PID + "' class='ColorPalletCorpName'>" + Sname + "</div>";
            html += "";
            $('#tabsActiveColors').append(html);

        } else {
            var html = "<div  id ='pallet" + PID + "' class ='ColorPalletCorp' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "><button  id ='btnClr" + PID + "' class='btnActiveColor' title='Activate this color'  onclick='j7(" + PID + ",&quot;Active&quot;);' ></button></div><div  id ='textColor" + PID + "' class='ColorPalletCorpName'>" + Sname + "</div>";
            html += "";
            $('#tabsInActiveColors').append(html);
        }
    } else {
        var html = "<div class ='ColorPallet' style='background-color:" + Color + "' onclick='f2(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;" + ",&quot;" + Sname + "&quot;);'" + "></div>";
        $('#DivColorContainer').append(html);    
    }
}
function a8()
{
    jQuery.cachedScript = function (url, options) {
        options = $.extend(options || {},        { dataType: "script", cache: true, url: url });
        return jQuery.ajax(options);
    };
    $.getJSON("services/fontSvc/" + "1234,1",
    function (DT) {
        $.each(DT, function (i, IT) {         
            $.cachedScript("Designer/PrivateFonts/" + IT.FontFile).done(function (script, textStatus) {
                 b1('fonts', IT.FontName, IT.FontName,IT.FontFile);
            });
        });
    });
}
function a9() {
    if (IsCalledFrom != 2) {
        CustomerID = parent.CustomerID;
        ContactID = parent.ContactID;
    }
    var Tc1 = CustomerID ; 
    if ( IsCalledFrom == 1)
    {
        Tc1 =   -1;
    } 
    $.getJSON("services/fontSvc/" +TemplateID+ "," + Tc1,
        function (DT) {
            $.each(DT, function (i, IT) {
                b1('BtnSelectFonts', IT.FontName, IT.FontName);
                if (IsCalledFrom == 3) {
                    b1('BtnSelectFontsRetail', IT.FontName, IT.FontName);
                }
                a0(IT.FontName, IT.FontFile,IT.FontPath);
                h8(IT.FontName, IT.FontFile,IT.FontPath);
        });
            h9();
            var selName = "select#BtnSelectFonts";
            if (IsCalledFrom == 3) {
                selName = "#BtnSelectFontsRetail";
                
            }
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
          //  $(selName).fontSelector('option', 'font', 'Arial Black');
         
        });
}
function h8(FN, FF,FP) {
    var p = "";
    if (IsEmbedded) {
        p = "/DesignEngine/";
    }
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
function h9()
{
     WebFontConfig = {
        custom: { families: T0FN,
        urls:T0FU },
        active: function() { 
            b6(TemplateID);
        },
        inactive: function() {
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


function a0(fontName, fontFileName) {
    var path = "";
    if (IsEmbedded) {
        path = "/DesignEngine/";
    }
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
function b1(selectId, value, text,id) {
    var html = '<option  id = ' +id +' value="'+value+'" >'+text+'</option>';
    $('#'+selectId).append(html);
}
function b6() {
    $.getJSON("services/TemplateSvc/" + TemplateID,
    function (DT) {
        Template = DT;
        if (Template.TemplateType == 1 || Template.TemplateType == 2) {
            IsBC = true
        } else {
            IsBC = false;
            $("#SubNavList").css("display", "block");
        }
        if (IsCalledFrom == 1 || IsCalledFrom == 2) {
            ShowBleedArea = true;
        }
        b3(TemplateID);
        b3_1();
        if (IsCalledFrom == 2) {
            c4_RS();
        } else if (IsCalledFrom == 4) {
            c4_RS_eU();
        }
      //  alert(allowImgDownload + " " + allowPdfDownload);
    });
}
function b3_1(caller) {
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        var catID = Template.ProductCategoryID;
        var svcURL = "services/layoutsvc/";
        if (IsCalledFrom == 3) {
            catID = Template.TemplateOwner;
            svcURL = "http://designerv2.myprintcloud.com/services/layoutsvc/";
        }
        $.getJSON(svcURL + catID,
        function (DT) {
            llData = DT;
            l4(caller);
        });
    }
}
function b2(pageID, PageName,pageType) {
    var HT = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "d5( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
    $('#PagesContainer' ).append(HT);		
}


function b3() {
    $.getJSON("services/TemplatePagesSvc/" + TemplateID,
    function (DT) {
        TP = DT;
        if (Template.TemplateType == 2) {
          //  $("#BtnBCPresets").css("display", "inline-block");
          //  $(".lblLayouts").css("display", "inline-block");
        }
        if (TP.length == 2) {
            IsBC = true;
            Template.TemplateType = 2;
        }
        if (IsBC) {
            $("#SubNavList").css("display", "none");
            if (TP[0].Orientation == 1) {
                //$("#btnPresetText3").css("display", "none");
                $(".divBCCarousel").css("height", "59px");
                $(".divBCCarousel").css("background-position-y", "64px");
            } else {
            }
            //if (Template.TemplateType == 1) {
            //    $.each(TP, function (i, IT) {
            //        if (IT.Orientation == 1) {
            //            $(".bcBackImgs img").eq(i).css("height", (IT.PDFTemplateHeight -20) + "px");
            //            $(".bcBackImgs img").eq(i).css("width", (IT.PDFTemplateWidth - 20) + "px");
            //        } else {
            //            $(".bcBackImgs img").eq(i).css("height", (IT.PDFTemplateWidth - 20) + "px");
            //            $(".bcBackImgs img").eq(i).css("width", (IT.PDFTemplateHeight - 20) + "px");
            //        }
            //    });
            //}
            $("#GuidesSpan").css("visibility", "hidden");
            $("#BtnGuides").css("visibility", "hidden");
            $(".titleBC").css("display", "inline");
            $(".titleBC").html("Front");
            $(".bcBackImgs").css("width", $(window).width() + "px");
            $(".canvas-container").css("z-index", "2");
            b2(DT[0].ProductPageID, DT[0].PageName, DT[0].PageType);
            var html = "";
            var html2 = "";
            var left = 0;
            if (DT.length > 1) {
                if (DT[1].Orientation == 1) {
                    left = ($(window).width() / 2 - Template.PDFTemplateWidth / 2);
                    $(".bcBackImgs").css("height", Template.PDFTemplateHeight + 20 + "px");
                } else {
                    left = ($(window).width() / 2 - Template.PDFTemplateHeight / 2);
                    $(".bcBackImgs").css("height", Template.PDFTemplateWidth + 20 + "px");
                }
            } else {
                $("#btnFlipSides").css("visibility", "hidden");
            }

            for (var i = 1; i < DT.length; i++) {

                if (DT[i].Orientation == 1) {
                    html2 += '<img  id="bcCarouselBigImg' + DT[i].ProductPageID + '"  src="designer/products/' + TemplateID + '/p' + DT[i].PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '"  class="bcCarouselImgLand" height = "' + Template.PDFTemplateHeight + 'px" width = "' + Template.PDFTemplateWidth + 'px" style="top:0px;left:' + left + 'px;"  onclick="k1(' + DT[i].ProductPageID + ')"/>';
                    left += Template.PDFTemplateWidth + 50;
                    html += '<div class="bcCarouselImgContainer" id="bcCarouselImg' + DT[i].ProductPageID + '" onclick="k1(' + DT[i].ProductPageID + ')"> <img src="designer/products/' + TemplateID + '/p' + DT[i].PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '"  class="bcCarouselThumbImgLand" id="bcCarouselSmallImg' + DT[i].ProductPageID + '" /> <img src="assets/BcPageNotPrintable.png"  class="bcCarouselEnableImgLand" id="bcCarouselEnable' + DT[i].ProductPageID + '" onClick=k3(' + DT[i].ProductPageID + ') /></div>';

                } else {
                    html2 += '<img id="bcCarouselBigImg' + DT[i].ProductPageID + '"  src="designer/products/' + TemplateID + '/p' + DT[i].PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '"  class="bcCarouselImgPort" height = "' + Template.PDFTemplateWidth + 'px" width = "' + Template.PDFTemplateHeight + 'px" style="top:0px;left:' + left + 'px;"  onclick="k1(' + DT[i].ProductPageID + ')" />';
                    left += Template.PDFTemplateHeight + 50;
                    html += '<div class="bcCarouselImgContainer" id="bcCarouselImg' + DT[i].ProductPageID + '" onclick="k1(' + DT[i].ProductPageID + ')"> <img src="designer/products/' + TemplateID + '/p' + DT[i].PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '"  class="bcCarouselThumbImgPort" id="bcCarouselSmallImg' + DT[i].ProductPageID + '" /> <img src="assets/BcPageNotPrintable.png"  class="bcCarouselEnableImgLand" id="bcCarouselEnable' + DT[i].ProductPageID + '" onClick=k3(' + DT[i].ProductPageID + ') /></div>';
                }
            }
            $(".bcBackImgs").html(html2);
            $(".bcCarouselImages").html(html);
            if (IsCalledFrom == 1 || IsCalledFrom == 3) {
                pcL36('show', '#divBCMenu');
            }
            //$("#divBCMenuPresets").tabs({ active: 0 });
            $(".divflipSidesIconBC").css("display", "block");
            $("#PagesContainer").css("visibility", "hidden");
            $(".previewBtnContainer").css("visibility", "hidden");
            // commented for Bleed area
            //    Template.PDFTemplateHeight = Template.PDFTemplateHeight - (Template.CuttingMargin * 2);
            //  Template.PDFTemplateWidth = Template.PDFTemplateWidth - (Template.CuttingMargin * 2)
            $("#SubNavList").css("display", "none");
            $(".divDesignerClosePanel").css("left", ($(window).width() / 2 + 500 + 26) + "px");
            $(".divbtnNextBC").css("display", "block");
            $("#CanvasContainer").css("position", "absolute");
            $("#CanvasContainer").css("width", "100%");
            $("#CanvasContainer").css("left", "0px");
            //  $(".divBCCarousel").css("top", "100px");
            $("#CanvasContainer").css("margin-top", "90px");
            if (Template.TemplateType == 1) {
                $("#CanvasContainer").css("top", "0px");
                $(".divBCCarousel").css("left", $(window).width() / 2 - 500 + "px");

            }
            $("#CanvasContainer").css("top", "0px");
            $("#btnFlipSides").css("font-size", "18px");
            $(".lblBCremoveSide").css("display", "none");
            $("#btnToggleSelectBack").css("display", "none");

            $(".divbtnNextBC").css("left", ($(window).width() / 2 + 500 - 100) + "px");
        } else {
            $.each(DT, function (i, IT) {
                b2(IT.ProductPageID, IT.PageName, IT.PageType)
            });
        }
        //$(".PageItemContainer").corner("7px;");
        b5(TemplateID);
        $.each(TP, function (i, IT) {
            var obj = fabric.util.object.clone(IT);
            TPRestore.push(obj);
        });
        //TPRestore = fabric.util.object.clone(TP);// $.merge([], TP); //jQuery.extend(true, {}, TP); ;

        k16(1, TeImC, "Loader");
        k16(12, TeImCBk, "Loader");
        if (IsCalledFrom == 2 || IsCalledFrom == 4) {
            k16(2, GlImC, "Loader");
            k16(3, GlImCBk, "Loader");
            k16(17, GlLogCn, "Loader");
            k16(16, GlShpCn, "Loader");
            $("#btnShowIcons, #btnShowLogo, #spanShowIcons, #spanLogos").css("display", "inline-block");
            if (IsCalledFrom == 4) {
                k16(4, UsImC, "Loader");
                k16(5, UsImCBk, "Loader");
            }
            if (IsCalledFrom == 2) {
                $("#btnMyImgDam").css("display", "none");
                $("#btnMyBkg").css("display", "none");
                $(".divImageTypes").css("display", "block");
            }
        }

        if (IsCalledFrom == 1 || IsCalledFrom == 3) {
            
            if (IsCalledFrom == 3) {
                k16(8, UsImC, "Loader");
                k16(6, GlImC, "Loader");
                k16(9, UsImCBk, "Loader");
                k16(7, GlImCBk, "Loader");
                k16(15, GlLogCnP, "Loader");
                k16(14, GlLogCn, "Loader");
                k16(13, GlShpCn, "Loader");

                k16(18, GlLogCnP, "Loader");
                k16(19, GlLogCn, "Loader");
                k16(20, GlShpCn, "Loader");
                $("#btnDivIllustrations, #btnDivFrames, #btnDivBanners").css("display", "list-item");
                
            }
            if (IsCalledFrom == 1) {
                if (CustomerID != -999) {
                    $("#btnFreeImg").css("display", "none");
                    $("#btnFreeBkg").css("display", "none");
                    $("#ImgCarouselDiv").tabs("option", "active", 2);
                    $("#BkImgContainer").tabs("option", "active", 2);
                    k16(10, GlImC, "Loader");
                    k16(11, GlImCBk, "Loader"); 
                    $("#myLogosList").css("display", "none");
                } else {
                    $("#btnMyImgDam").css("display", "none");
                    $("#btnMyBkg").css("display", "none");
                    k16(6, GlImC, "Loader");
                    k16(7, GlImCBk, "Loader");
                    $("#divImageTypes").css("display", "block"); $("#DivControlPanel1").css("width", "1100px");
                    if (IsCalledFrom != 3) {
                        $("#btnShowIcons, #btnShowLogo, #spanShowIcons, #spanLogos").css("display", "inline-block");
                    }
                    k16(14, GlLogCn, "Loader");
                    k16(13, GlShpCn, "Loader");
                    k16(18, GlLogCnP, "Loader");
                    k16(19, GlLogCn, "Loader");
                    k16(20, GlShpCn, "Loader");
                    $("#btnDivIllustrations, #btnDivFrames, #btnDivBanners").css("display", "list-item");
                    $(".onlyRetail").css("display", "inline-block");
                }

            }
            if (IsCalledFrom == 3) {
                $("#divIconShapesRadioBtn").css("display", "none");
            }
        }

    });
    pcL36('show', '#DivControlPanel1');
    if (!IsBC) {
        $("#CanvasContainer").css("margin-top", "10px");
        $(".divDesignerClosePanel").css("left", ($(window).width() / 2 + 500 + 26) + "px");
    }
//    if (!ShowBleedArea && !IsBC) {  // blead area fix
//        Template.PDFTemplateHeight = Template.PDFTemplateHeight - (Template.CuttingMargin * 2);
//        Template.PDFTemplateWidth = Template.PDFTemplateWidth - (Template.CuttingMargin * 2)
//    }
}

function b4(imgSrc) {
    IW = 150;
    IH = 150;
    var he = Template.PDFTemplateHeight;
    var wd = Template.PDFTemplateWidth;
    $.each(LiImgs, function (i, IT) {
        if (imgSrc.indexOf( IT.BackgroundImageRelativePath) != -1) {
            IW = IT.ImageWidth;
            IH = IT.ImageHeight;
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
//	var newImg = new Image();
//	newImg.src = imgSrc;
//	IH = newImg.height;
//	IW = newImg.width;
}
function b5() {
    //$.getJSON("services/imageSvc/" + TemplateID,
    //    function (DT) {
    //        LiImgs = DT;
    //        $.each(DT, function (i, IT) {
    //            var obj = {
    //                url: "./" + IT.BackgroundImageRelativePath,
    //                title: IT.ID,
    //                index: TemplateID
    //            }
    //            MCL.push(obj);
    //        });
         //   b7();
            c6(TemplateID);
            h3();
            a6();
        //});
}

function b7() {
    for (i = 0; i < MCL.length; i++) {
        $("#CarouselImages").append(b0(MCL[i]));
    }
    $(".draggable2").draggable({
        snap: '#dropzone',
        snapMode: 'inner',
        revert: 'invalid',
        helper: 'clone',
        appendTo: "body",
        cursor: 'move'
    });
}
function b8(imageID, productID) {
    if (confirm("Delete this image from all instances on canvas on all pages! Do you still wish to delete this image now?")) {
        StartLoader();
        $.get("Services/imageSvc/" + productID + "," + imageID,
                function (DT) {
                    if (DT != "false") {
                        $("#" + imageID).parent().remove();
                        // k27();
                        //  imageID = '#' + imageID;
                        //  $(imageID).parent().remove();
                        //  b9(imageID);
                         i2(DT);
                        StopLoader();
                    }
                });	   
    }
}
function b9(itemID) {
    var thelist = document.getElementById("CarouselImages");
    while (thelist.hasChildNodes()) {
        thelist.removeChild(thelist.lastChild);
    }
    var newCarourselList = [];
    newCarouselList = MCL;
    MCL = [];
    var iterator = 0;
    for (var i =0; i < newCarouselList.length; i++) {
        if("#"+newCarouselList[i].title !=  itemID) {
         MCL[iterator] = newCarouselList[i];
         iterator += 1;
        }
    }
    IC = 0;
    b7();

    StopLoader();
}

function b0(IT) {
    IC += 1;
    if (IC % 2 == 0) {
        return '<div  class="DivCarouselImgContainerStyle2"><img src="' + IT.url + '" width="75" height="75"  class="draggable2 cursorPointer" style=" z-index:1000; "id = "' + IT.title + '" alt="' + IT.url + '"  /> <a class="DelImgAnchor" onclick=b8(' + IT.title + "," + IT.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "/></a>  </div> ';

    } else {
        return '<div  class="DivCarouselImgContainer"><img src="' + IT.url + '" width="75" height="75"  class="draggable2 cursorPointer" style=" z-index:1000; "id = "' + IT.title + '" alt="' + IT.url + '"  /> <a class="DelImgAnchor" onclick=b8(' + IT.title + "," + IT.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "/></a>  </div>';
    }
    $("#" + IT.title).click(function (event) {
        j9(event, IT.url,IT.title);
    });
};

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
function i3(ia) {
    for(i = 0; i < TO.length ; i++) {
        if (TO[i].ObjectID == ia) {
            fabric.util.removeFromArray(TO, TO[i]);
            return false;
        }
    }
}
var lastObj = null;
function c1(x, y) {
    if (x === y) return true;
    if (!(x instanceof OPT) || !(y instanceof OPT)) return false;
    if (x.constructor !== y.constructor) return false;
    for (var p in x) {
        if (!x.hasOwnProperty(p)) continue;
        if (!y.hasOwnProperty(p)) return false;
        if (x[p] === y[p]) continue;
        if (typeof (x[p]) !== "object") return false;
        if (!OPT.equals(x[p], y[p])) return false;
    }
    for (p in y) {
        if (y.hasOwnProperty(p) && !x.hasOwnProperty(p)) return false;
    }
    return true;
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
function c2_del(obj) {
    $.each(TO, function (i, IT) {
        if (IT.ObjectID == obj.ObjectID) {
          //  clonedItemRedo = fabric.util.object.clone(obj);
         //   clonedItem = fabric.util.object.clone(IT);
          //  undoManager.register(undefined, c3, [clonedItem, 'delete'], 'Undo', undefined, c3, [clonedItemRedo, 'redoDelete'], 'Redo');
            fabric.util.removeFromArray(TO, IT);
            return false;
        }
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
                            textCMYK: objStyle['textCMYK'],
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
                } else {
                    IT.textStyles = null;
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
                    IT.PositionY = orgTop - (OPT.height * orgSy) / 2;
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
function c2(e, action, skipmode,redoCall) {
    IsDesignModified = true;
    var OPT = null;
    try {
        OPT = e.memo.target;
    }
    catch (e) {
    }

    if (!OPT)
    {
        OPT = e;
    }
    if (D1CZL != 0) {
        e4();
    }
           
    if (action == undefined && OPT != null) {
        //logAction(OPT.ObjectID + " is modified " + OPT.hasStateChanged() + " " + OPT.isMoving);
        if (OPT.type == "group") {
            var GO = OPT.getObjects();
            $.each(GO, function (j, obj) {

                $.each(TO, function (i, IT) {

                    if (IT.ObjectID == obj.ObjectID) {
                       // var clonedItemRedo;
                        //if (skipmode == undefined) {
                        //   // var clonedItem = fabric.util.object.clone(IT);
                        //}

                        IT.PositionX = OPT.left - IT.MaxWidth / 2;
                        IT.PositionY = OPT.top - IT.MaxHeight / 2;

                        if (obj.type == "text" || obj.type == "i-text") {
                            IT.ContentString = obj.text;
                        }

                        IT.RotationAngle = obj.getAngle();

                        if (obj.type != "i-text") {
                            IT.MaxWidth = obj.width * obj.scaleX;
                            IT.MaxHeight = obj.height * obj.scaleY;
                            obj.maxWidth = IT.MaxWidth;
                            obj.maxHeight = IT.MaxHeight;
                            if (obj.type == "ellipse") {
                                IT.CircleRadiusX = obj.getRadiusX();
                                IT.CircleRadiusY = obj.getRadiusY();
                                IT.PositionX = OPT.left - OPT.getWidth() / 2;
                                IT.PositionY = OPT.top - OPT.getHeight() / 2;
                            }
                        }

                        else {
                            IT.MaxWidth = obj.maxWidth;
                            IT.MaxHeight = obj.maxHeight;
                            IT.LineSpacing = obj.lineHeight;
                        }
                        if (obj.textAlign == "left")
                            IT.Allignment = 1;
                        else if (obj.textAlign == "center")
                            IT.Allignment = 2;
                        else if (obj.textAlign == "right")
                            IT.Allignment = 3;

                        if (obj.fontFamily != undefined)
                            IT.FontName = OPT.fontFamily;
                        else
                            IT.FontName = "";

                        if (obj.fontSize != undefined)
                            IT.FontSize = OPT.fontSize;
                        else
                            IT.FontSize = 0;

                        if (obj.fontWeight == "bold")
                            IT.IsBold = true;
                        else
                            IT.IsBold = false;

                        if (obj.fontStyle == "italic")
                            IT.IsItalic = true;
                        else
                            IT.IsItalic = false;

                        if (obj.type != "image") {
                            IT.ColorHex = OPT.fill;
                        }

                        if (obj.type == "path") {
                            IT.ExField1 = OPT.strokeWidth;
                        }


                        IT.Opacity = obj.opacity;
                        IT.ColorC = obj.C;
                        IT.ColorM = obj.M;
                        IT.ColorY = obj.Y;
                        IT.ColorK = obj.K;
                        IT.IsPositionLocked = obj.IsPositionLocked;
                        IT.IsOverlayObject = obj.IsOverlayObject;
                        IT.IsTextEditable = obj.IsTextEditable;
                        IT.AutoShrinkText = obj.AutoShrinkText;
                        IT.IsHidden = obj.IsHidden;
                        IT.IsEditable = obj.IsEditable;

                      //  clonedItemRedo = fabric.util.object.clone(IT);
                        if (skipmode == undefined) {
                          //  undoManager.register(undefined, c3, [clonedItem], 'Undo', undefined, c3, [clonedItemRedo], 'Redo');
                        }
                    }
                });
            });

        } else {
        $.each(TO, function (i, IT) {
            if (IT.ObjectID == OPT.ObjectID) {
               // var clonedItemRedo;
                if (skipmode == undefined) {
                 //   var clonedItem = fabric.util.object.clone(IT);
                }

                IT.PositionX = OPT.left - IT.MaxWidth / 2;
                IT.PositionY = OPT.top - IT.MaxHeight / 2;
                if (OPT.type == "text" || OPT.type == "i-text") {
                    IT.ContentString = OPT.text;
                    var CustomStylesList = [];
                    for (var prop in OPT.customStyles) {
                        var objStyle = OPT.customStyles[prop];
                        if (objStyle != undefined) {
                            var obj = {
                                textCMYK: objStyle['textCMYK'],
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
                    //IT.CharSpacing = OPT.charSpacing;
                }

                IT.RotationAngle = OPT.getAngle();
                if (OPT.type != "text" && OPT.type != "i-text") {
                    IT.MaxWidth = OPT.width * OPT.scaleX;
                    IT.MaxHeight = OPT.height * OPT.scaleY;
                    OPT.maxWidth = IT.MaxWidth;
                    OPT.maxHeight = IT.MaxHeight;
                    if (OPT.type == "ellipse") {
                        IT.CircleRadiusX = OPT.getRadiusX();
                        IT.CircleRadiusY = OPT.getRadiusY();
                        IT.PositionX = OPT.left - OPT.getWidth() / 2;
                        IT.PositionY = OPT.top - OPT.getHeight() / 2;
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
                //clonedItemRedo = fabric.util.object.clone(IT);
                if (skipmode == undefined) {
                 //   undoManager.register(undefined, c3, [clonedItem], 'Undo', undefined, c3, [clonedItemRedo], 'Redo');
                }
            }
        });
        }

    }
    else if (action == "delete") {
        //logAction(OPT.ObjectID + " is deleted " + OPT.hasStateChanged());
        if (OPT.type == "group") {
            var GO = OPT.getObjects();
            $.each(GO, function (j, obj) {
                $.each(TO, function (i, IT) {
                    if (IT.ObjectID == obj.ObjectID) {

                        var OBS = canvas.getObjects();
                        $.each(OBS, function (i, SubItem) {
                            if (SubItem.ObjectID == IT.ObjectID) {

                              //  clonedItemRedo = fabric.util.object.clone(SubItem);
                            }
                        });
                       // clonedItem = fabric.util.object.clone(IT);
                        // undoManager.register(undefined, UndoFuncDel, [clonedItem], 'Undo', undefined, RedoFuncDel, [clonedItemRedo], 'Redo');
                      //  undoManager.register(undefined, c3, [clonedItem, 'delete'], 'Undo', undefined, c3, [clonedItemRedo, 'redoDelete'], 'Redo');
                        fabric.util.removeFromArray(TO, IT);
                        return false;

                    }
                });
            });
        } else {   
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == OPT.ObjectID) {

                    var OBS = canvas.getObjects();
                    $.each(OBS, function (i, SubItem) {
                        if (SubItem.ObjectID == IT.ObjectID) {

                         //   clonedItemRedo = fabric.util.object.clone(SubItem);
                        }
                    });
                   // clonedItem = fabric.util.object.clone(IT);
                 //   undoManager.register(undefined, c3, [clonedItem, 'delete'], 'Undo', undefined, c3, [clonedItemRedo,'redoDelete'], 'Redo');
                    fabric.util.removeFromArray(TO, IT);
                    return false;

                }
            });
        }

    }
    //if (D1CZL != 0 && redoCall != true) {
    //    if (D1CZL > 0) {

    //        for (var i = 0; i < D1CZL; i++) {
    //          //  e3();
    //        }
    //    } else if (D1CZL < 0) {
    //        var currentZoomLevel1 = D1CZL * -1;
    //        for (var i = 0; i < currentZoomLevel1; i++) {
    //          //  e5();
    //        }

    //    }
    //    canvas.renderAll();
    //}
    

}

function c3(TG, action) {
    if (D1CZL != 0) {
        e4();
    }
    if (action == undefined) {
        var canvasObjects = canvas.getObjects(); 
        $.each(canvasObjects, function (i, IT) {
            if (IT.ObjectID == TG.ObjectID) {
                if (IT.type == "text" || IT.type == "i-text") {
                    IT.left = TG.PositionX + TG.MaxWidth / 2;
                    IT.top = TG.PositionY + TG.MaxHeight / 2;
                    IT.text = TG.ContentString;
                    IT.setAngle(TG.RotationAngle);
                    IT.maxWidth = TG.MaxWidth;
                    IT.maxHeight = TG.MaxHeight;

                    if (TG.tAllignment == 1)
                        IT.textAlign = "left"
                    else if (TG.Allignment == 2)
                        IT.textAlign = "center";
                    else if (TG.Allignment == 3)
                        IT.textAlign = "right";

                    IT.fontFamily = TG.FontName;
                    IT.fontSize = TG.FontSize;
                    IT.fontWeight = (TG.IsBold == true ? 'bold' : '')
                    IT.fontStyle = (TG.IsItalic == true ? 'italic' : '')
                    IT.fill = TG.ColorHex;
                    IT.C = TG.ColorC;
                    IT.M = TG.ColorM;
                    IT.Y = TG.ColorY;
                    IT.K = TG.ColorK;

                    IT.IsPositionLocked = TG.IsPositionLocked;
                    IT.IsOverlayObject = TG.IsOverlayObject;
                    IT.IsTextEditable = TG.IsTextEditable;
                    IT.AutoShrinkText = TG.AutoShrinkText;
                    IT.IsHidden = TG.IsHidden;
                    IT.IsEditable = TG.IsEditable;
                }
                else if (IT.type == "image") {

                    IT.left = TG.PositionX + TG.MaxWidth / 2;
                    IT.top = TG.PositionY + TG.MaxHeight / 2;
                    IT.setAngle(TG.RotationAngle);
                    IT.maxWidth = TG.MaxWidth;
                    IT.maxHeight = TG.MaxHeight;
                    IT.scaleToWidth(TG.MaxWidth);
                    IT.IsPositionLocked = TG.IsPositionLocked;
                    IT.IsOverlayObject = TG.IsOverlayObject;
                    IT.IsTextEditable = TG.IsTextEditable;
                    IT.AutoShrinkText = TG.AutoShrinkText;
                    IT.IsHidden = TG.IsHidden;
                    IT.IsEditable = TG.IsEditable;
                }
                else { 
                    IT.left = TG.PositionX + TG.MaxWidth / 2;
                    IT.top = TG.PositionY + TG.MaxHeight / 2;
                    IT.setAngle(TG.RotationAngle);
                    IT.maxWidth = TG.MaxWidth;
                    IT.maxHeight = TG.MaxHeight;
                    IT.fill = TG.ColorHex;
                    IT.C = TG.ColorC;
                    IT.M = TG.ColorM;
                    IT.Y = TG.ColorY;
                    IT.K = TG.ColorK;
                    IT.IsPositionLocked = TG.IsPositionLocked;
                    IT.IsOverlayObject = TG.IsOverlayObject;
                    IT.IsTextEditable = TG.IsTextEditable;
                    IT.AutoShrinkText = TG.AutoShrinkText;
                    IT.IsHidden = TG.IsHidden;
                    IT.IsEditable = TG.IsEditable;
                    
                }
                c2(IT, action, true,true);
            }
        });


    }
    else if (action == "delete") {
        TO.push(TG);
        if (TG.ObjectType == 2) {
            c0(canvas, TG);
        }
        else if (TG.ObjectType == 3 || TG.ObjectType == 8 || TG.ObjectType == 12 || TG.ObjectType == 13) {
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
    } 
        else if (action == "redoDelete") {
            var ItemToDel;
            $.each(TO, function (i, IT) {
                if (IT.ObjectID == TG.ObjectID) {

                    var OBS = canvas.getObjects();
                    $.each(OBS, function (i, SubItem) {
                        if (SubItem.ObjectID == IT.ObjectID) {
                            ItemToDel = SubItem
                           // clonedItem = fabric.util.object.clone(IT);
                           // clonedItemRedo = fabric.util.object.clone(SubItem);
                          //  undoManager.register(undefined, c3, [clonedItem], 'Undo', undefined, undefined, [undefined], 'Redo');


                        }
                    });
                    canvas.remove(ItemToDel);
                    fabric.util.removeFromArray(TO, IT);
                    return false;

                }
            });
        }

    //if (D1CZL != 0) {
    //    if (D1CZL > 0) {
    //        for (var i = 0; i < D1CZL; i++) {
    //            e3();
    //        }
    //    } else if (D1CZL < 0) {
    //        var currentZoomLevel1 = D1CZL * -1;
    //        for (var i = 0; i < currentZoomLevel1; i++) {
    //            e5();
    //        }

    //    }
    //}
    canvas.renderAll();
}
function c4_RS_eU() {
    if (propertyID != 0) {
        $.getJSON("../services/Webstore.svc/GetPropertyImages?propertyID=" + propertyID,
          function (xdata) {
              propertyImages = xdata;
              // load property images panel 
              $("#btnRealStateImages").css("display", "list-item");
              $.each(propertyImages, function (j, IT) {
                  var url ="../"+ IT.ImageURL;

                  var title ="LstImg" +IT.ListingImageID;
                  var draggable = '';
                  var urlThumbnail = url;
                  $(".divRSImagesContainer").append('<div  class="DivCarouselImgContainerStyle2"><img src="' + urlThumbnail +
                            '" class="imgCarouselDiv ' + draggable + ' " style=" z-index:1000; "id = "' + title + '" alt="' + url + '"   />  '
                            + ' </div> ');

                  $("#" + title).click(function (event) {
                      c4_RS_LI(event, url, title, IT.ListingImageID);
                  });
              });

          });
    }
}
function c4_RS_LI(e, url, title, listID) {
    StartLoader("Downloading image please wait...");
    $.getJSON("../services/Webstore.svc/DownloadPropertyImage?listingImageID=" + listID + "&templateId=" + TemplateID,
    function (xdata) {
        j8(xdata);
        StopLoader();
    });
}
function c4_RS() {
    $.getJSON("../services/Webstore.svc/GetVariablesData?isRealestateproduct=" + isRealestateproduct,
        function (xdata) {
            $("#divVarList").html("");
            var sc = "";
            var html = "";
            $.each(xdata, function (j, Obj) {
                if (Obj.VariableType != 3) {
                    if (Obj.SectionName != sc) {
                        html += '<div class="titletxt">' + Obj.SectionName + '</div>';
                        sc = Obj.SectionName;
                    }
                    html += '<div id="' + Obj.VariableID + '" class="divVar" title="' + Obj.VariableName + '">' + Obj.VariableTag + '</div>';
                } else {
                    if (IsCalledFrom == 2) {
                        var btnHtml = "<button class='" + Obj.VariableName + " listingImg' onClick='AddImgVar(&#39;" + Obj.VariableTag + "&#39;,"+Obj.VariableID +")'></button>";
                        $(".propertyVarContainer").css("display", "block");
                        $(".propertyVarContainer").append(btnHtml);
                    }
                }
            });
            $("#divVarList").html(html);
            $(".divVar").draggable({
                snap: '#dropzone',
                snapMode: 'inner',
                revert: 'invalid',
                helper: 'clone',
                appendTo: "body",
                cursor: 'move'
            });
        });
    $.getJSON("../services/Webstore.svc/GetTemplateVariables?templateID=" + TemplateID,
    function (xdata) {
        varList = xdata;
    });
}
function c4() {
    CustomerID = parent.CustomerID;
    ContactID = parent.ContactID;
   
    $.getJSON("../services/Webstore.svc/getquicktext?Customerid=" + CustomerID + "&contactid=" + ContactID,
        function (xdata) {
            QTD = xdata;
            if (QTD.Name == "" || QTD.Name== null) {
                QTD.Name = "Your Name"
            } 
             if (QTD.Title == "" || QTD.Title== null) {
                QTD.Title = "Your Title"
            } 
             if (QTD.Company == "" || QTD.Company== null) {
                QTD.Company = "Your Company Name"
            } 
             if (QTD.CompanyMessage == "" || QTD.CompanyMessage== null) {
                QTD.CompanyMessage = "Your Company Message"
            }
            if (QTD.Address1 == "" || QTD.Address1 == null) {
                QTD.Address1 = "Address Line 1"
            } 
             if (QTD.Telephone == "" || QTD.Telephone== null) {
                QTD.Telephone = "Telephone / Other"
            } 
             if (QTD.Fax == "" || QTD.Fax== null) {
                QTD.Fax = "Fax / Other"
            } 
             if (QTD.Email == "" || QTD.Email== null) {
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
            c4toUI();
            a4();
        });  
}
function h6() {
    var he = Template.PDFTemplateHeight;
    var wd =  Template.PDFTemplateWidth;
    he =  he / 96 * 72;
    wd =  wd / 96 * 72;
    he = he / 2.834645669;
    wd = wd / 2.834645669;    
    if (IsBC == false) { // blead area fix
        he = he - 10;
        wd = wd - 10;
    }
    $.getJSON("../services/Webstore.svc/GetConvertedDimentions?Height=" + he + "&Width=" + wd + "&TemplateID=" + TemplateID,
        function (xdata) {
            document.getElementById("DivDimentions").innerHTML = xdata;
            var p = xdata.split("<br /><br />");
            var txt = p[0];
            while (txt.indexOf("<br />") != -1)
                txt = txt.replace("<br />", " ");
            var des = txt.split(":");
            $(".dimentionsBC").html("Trim size - " + des[1]);
            $(".dimentionsBC").append("<br /><span class='spanZoomContainer'> Zoom - " + D1CS * 100 + " % </span>");
            $(".zoomToolBar").html(" Zoom " + Math.floor(D1CS * 100) + " % ");
           // $(".dimentionsBC").html("Trim size - " + $(".dimentionsBC").html());

        });
}

function c4toUI() {
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
}


function c5() {
    QuickTxtName = $("#txtQName").val();
    QuickTxtTitle = $("#txtQTitle").val();
    QuickTxtCompanyName = $("#txtQCompanyName").val();
    QuickTxtCompanyMsg = $("#txtQCompanyMessage").val();
    QuickTxtAddress1 = $("#txtQAddressLine1").val();
    QuickTxtTel = $("#txtQPhone").val();
    QuickTxtFax = $("#txtQFax").val();
    QuickTxtEmail = $("#txtQEmail").val();
    QuickTxtWebsite = $("#txtQWebsite").val();
    var QtxtMobile = $("#txtQMobile").val();
    var QtxtTwitter = $("#txtQTwitter").val();
    var QtxtFacebook = $("#txtQFacebook").val();
    var QtxtLinkedin = $("#txtQLinkedIn").val();
    var QtxtOtherID = $("#txtQOtherID").val();
    if ($("#txtQCompanyName").length > 0) {
        if (QuickTxtCompanyName != undefined || QuickTxtCompanyName != null) {
            QTD.Company = (QuickTxtCompanyName == "" ? "Your Company Name" : QuickTxtCompanyName);
        } else {
            QTD.Company = "";
        }
    }
    if ($("#txtQCompanyMessage").length > 0) {
        if (QuickTxtCompanyMsg != undefined || QuickTxtCompanyMsg != null) {
            QTD.CompanyMessage = QuickTxtCompanyMsg == "" ? "Your Company Message" : QuickTxtCompanyMsg;
        } else {
            QTD.CompanyMessage = "";
        }
    }
    if ($("#txtQName").length > 0) {
        if (QuickTxtName != undefined || QuickTxtName != null) {
            QTD.Name = QuickTxtName == "" ? "Your Name" : QuickTxtName;
        } else {
            QTD.Name = "";
        }
    }
    if ($("#txtQTitle").length > 0) {
        if (QuickTxtTitle != undefined || QuickTxtTitle != null) {
            QTD.Title = QuickTxtTitle == "" ? "Your Title" : QuickTxtTitle;
        } else {
            QTD.Title = "";
        }
    }
    if ($("#txtQAddressLine1").length > 0) {
        if (QuickTxtAddress1 != undefined || QuickTxtAddress1 != null) {
            QTD.Address1 = QuickTxtAddress1 == "" ? "Address Line 1" : QuickTxtAddress1;
        } else {
            QTD.Address1 = "";
        }
    }
    if ($("#txtQPhone").length > 0) {
        if (QuickTxtTel != undefined || QuickTxtTel != null) {
            QTD.Telephone = QuickTxtTel == "" ? "Telephone / Other" : QuickTxtTel;
        } else {
            QTD.Telephone = "";
        }
    }
    if ($("#txtQFax").length > 0) {
        if (QuickTxtFax != undefined || QuickTxtFax != null) {
            QTD.Fax = QuickTxtFax == "" ? "Fax / Other" : QuickTxtFax;
        } else {
            QTD.Fax = "";
        }
    }
    if ($("#txtQEmail").length > 0) {
        if (QuickTxtEmail != undefined || QuickTxtEmail != null) {
            QTD.Email = QuickTxtEmail == "" ? "Email address / Other" : QuickTxtEmail;
        } else {
            QTD.Email = "";
        }
    }
    if ($("#txtQWebsite").length > 0) {
        if (QuickTxtWebsite != undefined || QuickTxtWebsite != null) {
            QTD.Website = QuickTxtWebsite == "" ? "Website address" : QuickTxtWebsite;
        } else {
            QTD.Website = "";
        }
    }

    if ($("#txtQMobile").length > 0) {
        
        if (QtxtMobile != undefined || QtxtMobile != null) {
            QTD.MobileNumber = QtxtMobile == "" ? "Mobile number" : QtxtMobile;
        } else {
            QTD.MobileNumber = "";
        }
    }
    if ($("#txtQTwitter").length > 0) {
        
        if (QtxtTwitter != undefined || QtxtTwitter != null) {
            QTD.TwitterID = QtxtTwitter == "" ? "Twitter ID" : QtxtTwitter;
        } else {
            QTD.TwitterID = "";
        }
    }
    if ($("#txtQFacebook").length > 0) {
        
        if (QtxtFacebook != undefined || QtxtFacebook != null) {
            QTD.FacebookID = QtxtFacebook == "" ? "Facebook ID" : QtxtFacebook;
        } else {
            QTD.FacebookID = "";
        }
    }

    if ($("#txtQLinkedIn").length > 0) {
        
        if (QtxtLinkedin != undefined || QtxtLinkedin != null) {
            QTD.LinkedInID = QtxtLinkedin == "" ? "LinkedIn ID" : QtxtLinkedin;
        } else {
            QTD.LinkedInID = "";
        }
    }
    if ($("#txtQOtherID").length > 0) {
        
        if (QtxtOtherID != undefined || QtxtOtherID != null) {
            QTD.OtherId = QtxtOtherID == "" ? "Other ID" : QtxtOtherID;
        } else {
            QTD.OtherId = "";
        }
    }


    $.each(TO, function (i, IT) {
        if (IT.IsQuickText == true && IT.ObjectType != 3 && IT.ObjectType != 8 && IT.ObjectType != 12 && IT.ObjectType != 13) {
            var id = IT.Name.split(' ').join('');
            id = id.replace(/\W/g, '');
            if ($("#txtQ" + id).length) {
                var val = $("#txtQ" + id).val();
                
                if (val == "Your Company Name" || val == "Your Company Message" || val == "Your Name" || val == "Your Title" || val == "Address Line 1" || val == "Telephone / Other" || val == "Fax / Other" || val == "Email address / Other" || val == "Website address") {
                    val = "";
                } else {
                    if (val == IT.watermarkText) {
                        val = "";
                    }
                }
                IT.ContentString = val;
                c5_co(IT.ObjectID, val);
            }
        }
    });

    d5(SP);

    if (CustomerID != 0 && ContactID != 0) {
        StartLoader();
        var jsonObjects = JSON.stringify(QTD, null, 2);
        var to;
        to = "../services/Webstore.svc/update/";
        var options = {
            type: "POST",
            url: to,
            data: jsonObjects,
            contentType: "text/plain;",
            dataType: "json",
            async: true,
            success: function (response) {
            },
            error: function (msg) { alert("Error : " + msg); }
        };
        var returnText = $.ajax(options).responseText;
        StopLoader();
    }
    //StopLoader();
}
function c5_co(id,value) {
    var objs = canvas.getObjects();

    $.each(objs, function (i, IT) {
        if (IT.ObjectID == id) {
            IT.set("text", value);
            return;
        }
    });

}

function c6() {
    $.getJSON("services/TemplateObjectsSvc/" + TemplateID,
        function (DT) {
            TO = DT;

            //            if (IsBC || !ShowBleedArea) { // blead area fix
            //                $.each(TO, function (i, IT) {
            //                    IT.PositionX -= Template.CuttingMargin;
            //                    IT.PositionY -= Template.CuttingMargin;
            //                });
            //            }
            if (IsCalledFrom == 2) {
                k28();
            }
            if (IsEmbedded) {
                c4();
                h6();
            }
            else {
                a4();
                if (IsCalledFrom == 2) {
                    h6();
                } else {
                    var w = Template.PDFTemplateWidth;
                    var h = Template.PDFTemplateHeight;
                    h = h / 96 * 72;
                    w = w / 96 * 72;
                    h = h / 2.834645669;
                    w = w / 2.834645669;
                    w = w.toFixed(3);
                    h = h.toFixed(3);
                    h = h - 10;
                    w = w - 10;
                    w = w * Template.ScaleFactor;
                    h = h * Template.ScaleFactor;
                    document.getElementById("DivDimentions").innerHTML = "Product Size <br /><br /><br />" + w + " (w) *  " + h + " (h) mm";
                    $(".dimentionsBC").html("Trim size -" + " " + w + " (w) *  " + h + " (h) mm");
                    $(".dimentionsBC").append("<br /><span class='spanZoomContainer'> Zoom - " + D1CS * 100 + " % </span>");
                    $(".zoomToolBar").html(" Zoom " + Math.floor(D1CS * 100) + " % ");
                }
            }
            j5Loader();
            $.each(TO, function (i, IT) {
                var obj = fabric.util.object.clone(IT);
                TORestore.push(obj);
            });
            //  = // $.merge([], TO); // jQuery.extend(true, {}, TO); 
        });
        h4();
        k0();
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
                } else if (IT.ObjectType == 13) {
                    k31(canvas, IT);
                }
            }
        });

        d2();      
    }

    function c8(cCanvas, CO) {
        var COL = new fabric.Ellipse({
            left: (CO.PositionX + CO.MaxWidth / 2) * dfZ1l,
            top: (CO.PositionY + CO.MaxHeight / 2) * dfZ1l,
            fill: CO.ColorHex,
            rx: (CO.CircleRadiusX) * dfZ1l,
            ry: (CO.CircleRadiusY) * dfZ1l,
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
            left: (RO.PositionX + RO.MaxWidth / 2) * dfZ1l,
            top: (RO.PositionY + RO.MaxHeight / 2) * dfZ1l,
            fill: RO.ColorHex,
            width: (RO.MaxWidth) * dfZ1l,
            height: (RO.MaxHeight) * dfZ1l,
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

function c0(cCanvas,TOC) {		
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
            if (IT.textCMYK) {
                styleName = 'textCMYK';
                value = IT.textCMYK;
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
        lineHeight:(TOC.LineSpacing == 0 ? 1 : TOC.LineSpacing) ,
        fontSize: TOC.FontSize,
        angle: TOC.RotationAngle,
        fill: TOC.ColorHex,
        scaleX:  dfZ1l,
        scaleY:  dfZ1l,
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
    if(TOC.IsQuickText == true)
    {
        TOL.set({
                borderColor: 'green',
                cornerColor: 'green',
                cornersize: 10
        });
        TOL.IsQuickText = true;
    }
    cCanvas.insertAt(TOL, TOC.DisplayOrderPdf);
    return TOL;
        
}

function d1SvgOl(cCanvas, IO) {
    TIC += 1;
    fabric.loadSVGFromURL(IO.ContentString, function (objects, options) {

        var loadedObject = fabric.util.groupSVGElements(objects, options);
        loadedObject.set({
            left: (IO.PositionX + IO.MaxWidth / 2) * dfZ1l,
            top: (IO.PositionY + IO.MaxHeight / 2) * dfZ1l,
            angle: IO.RotationAngle
        });
        loadedObject.maxWidth = IO.MaxWidth;
        loadedObject.maxHeight = IO.MaxHeight;
        loadedObject.ObjectID = IO.ObjectID;
        loadedObject.fill = IO.ColorHex;
        loadedObject.scaleX = (loadedObject.maxWidth / loadedObject.width) * dfZ1l;
        loadedObject.scaleY = (loadedObject.maxHeight / loadedObject.height) * dfZ1l;
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
        loadedObject.setOpacity(IO.Opacity );
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
    
function d1(cCanvas, IO,isCenter) {
    TIC += 1;
    if (IO.MaxWidth == 0) {
        IO.MaxWidth = 50;
    }
    if (IO.MaxHeight == 0) {
        IO.MaxHeight = 50;
    }
    var url = IO.ContentString;
    if (url == "{{ListingImage1}}") {
        url = "./assets/placeholder1.png";
    } else if (url == "{{ListingImage2}}") {
        url = "./assets/placeholder2.png";
    } else if (url == "{{ListingImage3}}") {
        url = "./assets/placeholder3.png";
    } else if (url == "{{ListingImage4}}") {
        url = "./assets/placeholder4.png";
    } else if (url == "{{ListingImage5}}") {
        url = "./assets/placeholder5.png";
    } else if (url == "{{ListingImage6}}") {
        url = "./assets/placeholder6.png";
    } else if (url == "{{ListingImage7}}") {
        url = "./assets/placeholder7.png";
    } else if (url == "{{ListingImage8}}") {
        url = "./assets/placeholder8.png";
    } else if (url == "{{ListingImage9}}") {
        url = "./assets/placeholder9.png";
    } else if (url == "{{ListingImage10}}") {
        url = "./assets/placeholder10.png";
    } else if (url == "{{ListingImage11}}") {
        url = "./assets/placeholder11.png";
    } else if (url == "{{ListingImage12}}") {
        url = "./assets/placeholder12.png";
    } else if (url == "{{ListingImage13}}") {
        url = "./assets/placeholder13.png";
    } else if (url == "{{ListingImage14}}") {
        url = "./assets/placeholder14.png";
    } else if (url == "{{ListingImage15}}") {
        url = "./assets/placeholder15.png";
    } else if (url == "{{ListingImage16}}") {
        url = "./assets/placeholder16.png";
    } else if (url == "{{ListingImage17}}") {
        url = "./assets/placeholder17.png";
    } else if (url == "{{ListingImage18}}") {
        url = "./assets/placeholder18.png";
    } else if (url == "{{ListingImage19}}") {
        url = "./assets/placeholder19.png";
    } else if (url == "{{ListingImage20}}") {
        url = "./assets/placeholder20.png";
    }
    fabric.Image.fromURL(url, function (IOL) {
        IOL.set({
            left: (IO.PositionX + IO.MaxWidth / 2) * dfZ1l,
            top: (IO.PositionY + IO.MaxHeight / 2) * dfZ1l,
            angle: IO.RotationAngle
        });
        IOL.maxWidth = IO.MaxWidth;
        IOL.maxHeight = IO.MaxHeight;
        IOL.ObjectID = IO.ObjectID;
        IOL.scaleX = (IOL.maxWidth / IOL.width) * dfZ1l;
        IOL.scaleY = (IOL.maxHeight / IOL.height) * dfZ1l;
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
    StartLoader();
    if (LIFT && TIC === TotalImgLoaded) { 
        LIFT = false;
        StopLoader();
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
        //   i7();
        if (Template.ProductCategoryID == '85' || Template.ProductCategoryID == '350' || Template.TemplateOwner == 26) {
          //  $("#BtnZoomIn").click();
         //   $("#BtnZoomIn").click();
        } else if (Template.ProductCategoryID == '344' || Template.ProductCategoryID == '344') {
        //    $("#BtnZoomOut").click();
         //   $("#BtnZoomOut").click();
        } else if (Template.TemplateOwner == 390) {
         //   $("#BtnZoomIn").click();
        }
        if (IsCalledFrom == 3) {
            m0();
        }
      
       
    } else {
        if (TIC == TotalImgLoaded) {
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
            if (Template.ProductCategoryID == '85' || Template.ProductCategoryID == '350' || Template.TemplateOwner == 361 || Template.TemplateOwner == 390) {
              //  $("#BtnZoomIn").click();
              //  $("#BtnZoomIn").click();
            } else if (Template.ProductCategoryID == '344' || Template.ProductCategoryID == '344') {
              //  $("#BtnZoomOut").click();
              //  $("#BtnZoomOut").click();
            }
            StopLoader();
            if (IsCalledFrom == 3) {
                m0();
            }
          
        }
    }
    
}

function d1New(cCanvas, imagePath, positionX, positionY, rotationAngle, width, height, DisplayOrder) {
    fabric.Image.fromURL(imagePath, function (IOL) {
        IOL.set({
            left: positionX + width / 2,
            top: positionY - height / 2,
            angle: rotationAngle

        });
        IOL.width = width;
        IOL.height = height;
        IOL.maxWidth = width;
        IOL.maxHeight = height;
        IOL.ObjectID = --NCI;
        IOL.setAngle(rotationAngle);
        IOL.set({
            borderColor: 'red',
            cornerColor: 'orange',
            cornersize: 10
        });
        IOL.ImageClippedInfo = IO.ClippedInfo;
        cCanvas.insertAt(IOL, DisplayOrder);
    });
}


function d4(cCanvas, CO) {
    var OP = new fabric.Path(CO.ContentString);
    OP.set({
        borderColor: 'red',
        cornerColor: 'orange',
        cornersize: 10
    });
    OP.ObjectID = CO.ObjectID;
    OP.setAngle(CO.RotationAngle);
    OP.IsOverlayObject = CO.IsOverlayObject;
    OP.IsPositionLocked = CO.IsPositionLocked;
    OP.IsHidden = CO.IsHidden;
    OP.IsEditable = CO.IsEditable;
    OP.IsTextEditable = CO.IsTextEditable;
    OP.AutoShrinkText = CO.AutoShrinkText;
    OP.fill = null;
    OP.stroke = CO.ColorHex;
    OP.strokeWidth = CO.ExField1;
    OP.set("left", CO.PositionX + OP.width /2);
    OP.set("top", CO.PositionY + OP.height /2);
    canvas.insertAt(OP, CO.DisplayOrderPdf);

}

function d5(pageID) {
    undoArry = [];
    redoArry = [];
    bleedPrinted = false;
    pcL36('hide', '#textPropertPanel, #divPopupUpdateTxt , #divVariableContainer , #ShapePropertyPanel , #ImagePropertyPanel , #UploadImage , #quickText , #addImage , #divImageDAM , #divImageEditScreen , #DivLayersPanel , #addText , #DivToolTip , #DivUploadFont , #DivColorPallet , #DivAdvanceColorPanel , #DivCropToolContainer , #DivAlignObjs');
    if (IsCalledFrom != 3) {
        pcL36('hide', '#quickTextFormPanel');
    } 
    //e0("d5");
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
  //  e0("d5");
    canvas.renderAll();
    c2_v2();
    c2_v2();
    SP = pageID;
    $(".PageItemContainer").css("background-color", "#FAF9F7");
    $(".PageItemContainer").css("color", "black");
    $("#" + pageID).css("background-color", "#A5A6AD"); 
    $("#" + pageID).css("color", "white");



    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == SP) {
            canvas.clear();
            canvas.setBackgroundImage(null, function (IOL) { canvas.renderAll(); StopLoader(); });
            canvas.backgroundColor = "#ffffff";
            if (IT.Orientation == 1) {
                canvas.setHeight(Template.PDFTemplateHeight * dfZ1l);
                canvas.setWidth(Template.PDFTemplateWidth * dfZ1l);
            }
            else {
                canvas.setHeight(Template.PDFTemplateWidth * dfZ1l);
                canvas.setWidth(Template.PDFTemplateHeight * dfZ1l);
            }
            if (IT.BackgroundFileName != "") {
                StartLoader();
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
                //  IT.BackgroundFileName += "?r=" + fabric.util.getRandomInt(1, 100);
                var bk = IT.BackgroundFileName + "?r=" + fabric.util.getRandomInt(1, 100);
                if (IT.BackgroundFileName != "") {
                    canvas.setBackgroundImage(bk, canvas.renderAll.bind(canvas), {
                        left: 0,
                        top :0,
                        height : canvas.getHeight(),
                        width : canvas.getWidth(),
                        maxWidth : canvas.getWidth(),
                        maxHeight : canvas.getHeight(),
                        originX: 'left',
                        originY: 'top'
                    }); StopLoader();
                    canvas.renderAll();
                  //  canvas.drawImage(IT.BackgroundFileName, 0, 0);
//                    fabric.util.loadImage(IT.BackgroundFileName, function (IOL) {
//                        IOL.left = canvas.getWidth() / 2;
//                        IOL.top = canvas.getHeight() / 2;
//                        IOL.height = canvas.getHeight();
//                        IOL.width = canvas.getWidth();
//                        IOL.maxWidth = canvas.getWidth();
//                        IOL.maxHeight = canvas.getHeight();


//                    });
                    //   canvas.setBackgroundImage(IT.BackgroundFileName, function (IOL) { canvas.renderAll(); StopLoader(); });
                } else {
                    canvas.backgroundColor = "#ffffff";
                    canvas.setBackgroundImage(null, function (IOL) { canvas.renderAll(); StopLoader(); });
                }
            }

            if (IT.BackGroundType == 2) {
                canvas.setBackgroundImage(null, function (IOL) { canvas.renderAll(); StopLoader(); });
                var colorHex = getColorHex(IT.ColorC, IT.ColorM, IT.ColorY, IT.ColorK);
                //$("#canvas").css("background-color", colorHex);
                canvas.backgroundColor = colorHex;
                canvas.renderAll();

            }
        }
    });
    
    c7(pageID);
    h5();
}

function d6(width, height, showguides) {
    
    //if (showguides && !IsBC &&  ShowBleedArea) { // blead area fix
    if (showguides && !bleedPrinted) {
        bleedPrinted = true;
//	    var leftline = d7([cutmargin, cutmargin, cutmargin, cutmargin + height - cutmargin * 2], -980, 'gray');
//	    var topline = d7([cutmargin, cutmargin, cutmargin + width - cutmargin * 2, cutmargin], -981, 'gray');
//	    var rightline = d7([cutmargin + width - cutmargin * 2, cutmargin, cutmargin + width - cutmargin * 2, cutmargin + height - cutmargin * 2], -982, 'gray');
//	    var bottomline = d7([cutmargin, cutmargin + height - cutmargin * 2, cutmargin + width - cutmargin * 2, cutmargin + height - cutmargin * 2], -983, 'gray');
        var cutmargin = Template.CuttingMargin * dfZ1l;
        if (udCutMar != 0) {
            cutmargin = udCutMar * dfZ1l;
        }
        //cutmargin = cutmargin * 96 / 72;
        var leftline = i4([0, 0, 0, cutmargin + height - cutmargin ], -980, 'white', cutmargin*2);
        var topline = i4([cutmargin + 0.39, 0, cutmargin + width - 0.39 - cutmargin * 2, 0], -981, 'white', cutmargin * 2);
        var rightline = i4([width - 1, 0, width - 1, cutmargin + height - cutmargin], -982, 'white', cutmargin * 2);
        var bottomline = i4([cutmargin + 0.39, height, cutmargin + width - 0.39 - cutmargin * 2, height], -983, 'white', cutmargin * 2);

        var topCutMarginTxt = i5((14 * dfZ1l), width / 2, 17, 100, 10, 'Bleed Area', -975, 0, 'gray');
        var leftCutMarginTxt = i5(height / 2, width - (12* dfZ1l), 17, 100, 10, 'Bleed Area', -974, 90, 'gray');
        var rightCutMarginTxt = i5(height / 2, (13* dfZ1l), 17, 100, 10, 'Bleed Area', -973, -90, 'gray');
        var bottomCutMarginTxt = i5(height - 6, width / 2, 17, 100, 10, 'Bleed Area', -972, 0, 'gray');	
//        var topSafeZoneTxt = i5(cutmargin+8.5, width / 2, 17, 100,9, 'Safe Zone', -975,0,'Grey');
//		var leftSafeZoneTxt = i5(height/2, width-9-cutmargin, 17, 100, 9, 'Safe Zone', -974,90,'Grey');
//		var rightSafeZoneTxt = i5(height/2, 9+cutmargin, 17, 100, 9, 'Safe Zone', -973,-90,'Grey');
//		var bottomSafeZoneTxt = i5(height+1.5 - cutmargin, width / 2, 17, 100, 9, 'Safe Zone', -972,0,'Grey');
        var zafeZoneMargin = cutmargin; // + 8.49;
        var sleftline = d7([zafeZoneMargin, zafeZoneMargin, zafeZoneMargin, zafeZoneMargin + height - zafeZoneMargin * 2], -984, 'gray');
        var stopline = d7([zafeZoneMargin, zafeZoneMargin, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin], -985, 'gray');
        var srightline = d7([zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin + height - zafeZoneMargin * 2], -986, 'gray');
        var sbottomline = d7([zafeZoneMargin, zafeZoneMargin + height - zafeZoneMargin * 2, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin + height - zafeZoneMargin * 2], -987, 'gray');
        canvas.add(leftline, topline, rightline, bottomline, sleftline, stopline, srightline, sbottomline, topCutMarginTxt,bottomCutMarginTxt,leftCutMarginTxt,rightCutMarginTxt);
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
    var line =  new fabric.Line(coords,
        { fill: color, strokeWidth: 1, selectable: false
        });

    line.ObjectID = ObjectID;
    return line;
}
function i4(coords, ObjectID, color,cutMargin) {
    var line = new fabric.Line(coords,
        { fill: color, strokeWidth: cutMargin, selectable: false, opacity:0.5,border:'none'
        });

    line.ObjectID = ObjectID;
    return line;
}
function i5(top, left, maxHeight, maxWidth,fontsize,text, ObjectID,rotationangle,Color) {
    var hAlign = "";
    hAlign = "center";
    var hStyle = "";
    var hWeight = "";
    var TOL = new fabric.Text(text, {
        left: left ,
        top: top ,
        fontFamily: 'Arial',
        fontStyle: hStyle,
        fontWeight: hWeight,
        fontSize: fontsize,
        angle:rotationangle,
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
function h4()
{   
    $.getJSON("services/TemplateSvc/GetFolds/" + TemplateID,
        function (DT) { 
            if (DT != null && DT != undefined && DT.length != 0)
            {
                N1FL = DT;
                h5(); 
            }
        });
}


function h5()
{
    var cutmargin = Template.CuttingMargin;
    var width = Template.PDFTemplateWidth;
    var height = Template.PDFTemplateHeight;
    var orientation = 0;
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == SP) {
            if (IT.Orientation == 1)  { // portrail
                orientation = 1;
                return false;
            }
        }
    });
    if (N1FL != null)
    {
        $.each(N1FL, function (i, IT) {  
                var line = null ;
                var line2 = null ;
                var line3 = null ;
                var FoldLineOrientation =  IT.FoldLineOrientation.toString(); 
                FoldLineOrientation = FoldLineOrientation.toLowerCase(); 
                if(FoldLineOrientation == "true" && orientation == 1) //l5
                {  
                    line = d7([cutmargin + IT.FoldLineOffsetFromOrigin, cutmargin, cutmargin  + IT.FoldLineOffsetFromOrigin , cutmargin + height - cutmargin * 2], -1000 + i, 'cyan');
                    line2 = d7([cutmargin + IT.FoldLineOffsetFromOrigin - 8, cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin - 8, cutmargin + height - cutmargin * 2], -1000 + i, 'orange');
                    line3 = d7([cutmargin + IT.FoldLineOffsetFromOrigin + 8, cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin + 8, cutmargin + height - cutmargin * 2], -1000 + i, 'orange');
                } else if (FoldLineOrientation == "false" && orientation == 0) { // l7
                    line = d7([cutmargin, cutmargin  + IT.FoldLineOffsetFromOrigin , cutmargin + width - cutmargin * 2, cutmargin  + IT.FoldLineOffsetFromOrigin  ], -1000 + i, 'cyan');
                    line2 = d7([cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin - 8, cutmargin + width - cutmargin * 2, cutmargin + IT.FoldLineOffsetFromOrigin - 8], -1000 + i, 'orange');
                    line3 = d7([cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin + 8, cutmargin + width - cutmargin * 2, cutmargin + IT.FoldLineOffsetFromOrigin + 8], -1000 + i, 'orange');
                
                } else if(FoldLineOrientation == "true" && orientation == 0) //l6
                {
                    line = d7([cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin, cutmargin + height - cutmargin * 2, cutmargin + IT.FoldLineOffsetFromOrigin], -1000 + i, 'cyan');
                    line2 = d7([cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin - 8, cutmargin + height - cutmargin * 2, cutmargin + IT.FoldLineOffsetFromOrigin - 8], -1000 + i, 'orange');
                    line3 = d7([cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin + 8, cutmargin + height - cutmargin * 2, cutmargin + IT.FoldLineOffsetFromOrigin + 8], -1000 + i, 'orange');
                } else if (FoldLineOrientation == "false" && orientation == 1) { // l8
                    line = d7([cutmargin + IT.FoldLineOffsetFromOrigin, cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin, cutmargin + width - cutmargin * 2], -1000 + i, 'cyan');
                    line2 = d7([cutmargin + IT.FoldLineOffsetFromOrigin - 8, cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin - 8, cutmargin + width - cutmargin * 2], -1000 + i, 'orange');
                    line3 = d7([cutmargin + IT.FoldLineOffsetFromOrigin + 8, cutmargin, cutmargin + IT.FoldLineOffsetFromOrigin + 8, cutmargin + width - cutmargin * 2], -1000 + i, 'orange');
                } 
                canvas.add(line,line2,line3);
        });
    }
}


function j5() {
    var AQTD = [];
    var NameArr = [];
    var HM = "";
    $.each(TO, function (i, IT) {
        if (IT.IsQuickText == true && IT.ObjectType != 3 && IT.ObjectType != 8 && IT.ObjectType != 12 && IT.ObjectType != 13) {
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
                if (IsEmbedded) {
                    if (IT.IsEditable != false) {  
                        NameArr.push(IT.Name);
                        AQTD.push(obj);
                    }
                } else {
                    NameArr.push(IT.Name);
                    AQTD.push(obj);
                }
            }
        }
    });
    AQTD.sort(function(obj1, obj2) {
        return obj1.Order - obj2.Order;
    });
    if (AQTD.length >= 1) {
        TOFZ = AQTD[AQTD.length - 1].Order+1;
        //alert(TOFZ);
    }
    $.each(AQTD, function (i, ITOD) {
        var id = ITOD.Name.split(' ').join('');
        id = id.replace(/\W/g, '');
        if (IsEmbedded == false) {
            HM += "<div class='panelQuickTextFormRow ui-state-default addClassQTxtCorp'><span class='qLabelCorp'>" + ITOD.Name + "</span><input id=txtQ" + id + " maxlength='500' value = '" + ITOD.Name + "' class='item-ii' /><br /><span class='qLabelCorpWM'> Watermark Text </span> <input id=txtWM" + id + " maxlength='500' value = '" + ITOD.watermarkText + "' class='item-i' /></div>";
        } else {
            HM += "<div class='panelQuickTextFormRow '><div class='qLabel'>" + ITOD.Name + "</div><input id=txtQ" + id + " maxlength='500' class='qTextInput' /></div>";
        }
    });
    $("#QuickTextItemContainer").html(HM);

    $.each(AQTD, function (i, ITOD) {
        var id = ITOD.Name.split(' ').join('');
        id = id.replace(/\W/g, '');
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            if (ITOD.ContentString == "") {
                ITOD.ContentString = ITOD.watermarkText;
            }
            if (ITOD.ContentString == ITOD.watermarkText) {
                $("#txtQ" + id).addClass("greyColor");
            } else {
                $("#txtQ" + id).removeClass("greyColor");
            }
            $("#txtQ" + id).attr("placeholder", ITOD.watermarkText);
            $("#txtQ" + id).val(ITOD.ContentString);
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
        }
    });
    if (IsEmbedded == false) {
        $(".qSubHeading").text("Re-arrange and update Q Text Labels");
        $("#QuickTextItemContainer").sortable({
            placeholder: "ui-state-highlight",
            update: function (event, ui) {
                //i8($(ui.item).children(".selectedObjectID").text(), ui.item.index());
            },
            start: function (e, ui) {
                // N111a = ui.item.index();
            },
            stop: function (event, ui) {
                $('.QuickTextItemContainer  div').each(function (i) {
                    var index = ++i
                    var QFiel = $(this).find('.qLabelCorp').text();
                    $.each(TO, function (j, ite) {
                        if (ite.Name == QFiel) {
                            ite.QuickTextOrder = index;
                            //return false;
                        }
                    });
                });
            }
        });
    } else {
        $("#txtQName").val(QTD.Name);
        $("#txtQTitle").val(QTD.Title);
        $("#txtQCompanyName").val(QTD.Company);
        $("#txtQCompanyMessage").val(QTD.CompanyMessage);
        $("#txtQAddressLine1").val(QTD.Address1);
        $("#txtQPhone").val(QTD.Telephone);
        $("#txtQFax").val(QTD.Fax);
        $("#txtQEmail").val(QTD.Email);
        $("#txtQWebsite").val(QTD.Website);
       
        $("#txtQName" ).attr("placeholder", QTD.Name);
        $("#txtQTitle" ).attr("placeholder", QTD.Title);
        $("#txtQCompanyName" ).attr("placeholder", QTD.Company);
        $("#txtQCompanyMessage" ).attr("placeholder", QTD.CompanyMessage);
        $("#txtQAddressLine1" ).attr("placeholder", QTD.Address1);
        $("#txtQPhone" ).attr("placeholder", QTD.Telephone);
        $("#txtQFax" ).attr("placeholder", QTD.Fax);
        $("#txtQEmail" ).attr("placeholder", QTD.Email);
        $("#txtQWebsite").attr("placeholder", QTD.Website);

        // ui change 
        $(".panelQuickTexthead").html("Save your quick text profile (Q Text)");
        $(".qSubHeading").css("display", "none");
        $("#quickTextFormPanel").css("width", "309px");
        $(".qLabel").css("float", "left");
        $(".qLabel").css("padding-top", "8px");
        $(".panelQuickTextFormRow INPUT").css("width", "178px");
        $(".panelQuickTextFormRow INPUT").css("-moz-border-radius", "5px");
        $(".panelQuickTextFormRow INPUT").css("-webkit-border-radius", "5px");
        $(".panelQuickTextFormRow INPUT").css("-khtml-border-radius", "5px");
        $(".panelQuickTextFormRow INPUT").css("border-radius", "5px");
    }
//	$("#QuickTextItemContainer").disableSelection();
    
    if (AQTD.length >= 1) {
        pcL36('show' ,"#quickTextFormPanel");
        //f7("quickTextFormPanel");
      //  alert();
    }
    $('input, textarea, select').focus(function () {
        IsInputSelected = true;
    }).blur(function () {
        IsInputSelected = false;
    });
}

function j5Loader() {
    var AQTD = []; 
    var NameArr = [];
    var HM = "";
    var hQText = false;
    $.each(TO, function (i, IT) {
        if (IT.IsQuickText == true && IT.ObjectType != 3 && IT.ObjectType != 8 && IT.ObjectType != 12 && IT.ObjectType != 13) {
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
                if (IsEmbedded) {
                    if (IT.IsEditable != false) {   // show only editable text
                        NameArr.push(IT.Name);
                        AQTD.push(obj);
                    }
                } else {
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
        if (IsEmbedded == false) {
            HM += "<div class='panelQuickTextFormRow ui-state-default addClassQTxtCorp'><span class='qLabelCorp'>" + ITOD.Name + "</span><input id=txtQ" + id + " maxlength='500' value = '" + ITOD.Name + "' class='item-ii' /><br /><span class='qLabelCorpWM' > Watermark Text </span> <input id=txtWM" + id + " maxlength='500' value = '" + ITOD.watermarkText + "' class='item-i' /></div>";
        } else {
            HM += "<div class='panelQuickTextFormRow '><div class='qLabel'>" + ITOD.Name + "</div><input id=txtQ" + id + " maxlength='500' /></div>";
        }
    });
    $("#QuickTextItemContainer").html(HM);
    $.each(AQTD, function (i, ITOD) {
        var id = ITOD.Name.split(' ').join('');
        id = id.replace(/\W/g, '');
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            $("#txtQ" + id).attr("placeholder", ITOD.watermarkText);
            $("#txtQ" + id).val(ITOD.ContentString);
            var tn = "txtQ" + id;
            var addEvent = function (elem, type, fn) {
                if (elem.addEventListener) elem.addEventListener(type, fn, false);
                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
            },
            textField = document.getElementById(tn),
            text=ITOD.ContentString ,
            placeholder = ITOD.watermarkText;
            addEvent(textField, 'focus', function () {
                if (this.value === placeholder) this.value = '';
            });
            addEvent(textField, 'blur', function () {
                if (this.value === '') this.value = placeholder;
            });
        }
    });
   

    if (IsEmbedded == false) {
        $(".qSubHeading").text("Re-arrange and update Q Text Labels");
        $("#QuickTextItemContainer").sortable({
            placeholder: "ui-state-highlight",
            update: function (event, ui) {
                //i8($(ui.item).children(".selectedObjectID").text(), ui.item.index());
            },
            start: function (e, ui) {
                // N111a = ui.item.index();
            },
            stop: function (event, ui) {
                $('.QuickTextItemContainer  div').each(function (i) {
                    var index = ++i
                    var QFiel = $(this).find('span').text();
                    $.each(TO, function (j, ite) {
                        if (ite.Name == QFiel) {
                            ite.QuickTextOrder = index;
                            //return false;
                        }
                    });
                });
            }
        });
    } else {
        $("#txtQName").val(QTD.Name);
        $("#txtQTitle").val(QTD.Title);
        $("#txtQCompanyName").val(QTD.Company);
        $("#txtQCompanyMessage").val(QTD.CompanyMessage);
        $("#txtQAddressLine1").val(QTD.Address1);
        $("#txtQPhone").val(QTD.Telephone);
        $("#txtQFax").val(QTD.Fax);
        $("#txtQEmail").val(QTD.Email);
        $("#txtQWebsite").val(QTD.Website);

        $("#txtQName").attr("placeholder", QTD.Name);
        $("#txtQTitle").attr("placeholder", QTD.Title);
        $("#txtQCompanyName").attr("placeholder", QTD.Company);
        $("#txtQCompanyMessage").attr("placeholder", QTD.CompanyMessage);
        $("#txtQAddressLine1").attr("placeholder", QTD.Address1);
        $("#txtQPhone").attr("placeholder", QTD.Telephone);
        $("#txtQFax").attr("placeholder", QTD.Fax);
        $("#txtQEmail").attr("placeholder", QTD.Email);
        $("#txtQWebsite").attr("placeholder", QTD.Website);
        if (IsCalledFrom != 3) {
            $("#addText").css("height", "172px");
        }
        $(".popUpQuickTextPanel").css("top", "115px");
        if (IsCalledFrom != 3) {
             pcL36('show','#quickTextFormPanel');
        }
    }
//	$("#QuickTextItemContainer").disableSelection();

    $('input, textarea, select').focus(function () {
        IsInputSelected = true;
    }).blur(function () {
        IsInputSelected = false;
    });
    TempChkQT = AQTD.length;
    
}


function j9(e, url1,id) {
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        if (D1AO.type === 'image') {
          //  if (D1AO.get('IsQuickText')) {
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
                        StartLoader();
                        var imgtype = 2;
                        if (isBKpnl) {
                            imgtype = 4;
                        }
                        $.getJSON("services/imageSvc/DownloadImg/" + n + "," + TemplateID + "," + imgtype,
                        function (DT) {
                            StopLoader();
                            k27();
                            parts = DT.split("Designer/Products/");
                            $("#ImgCarouselDiv").tabs("option", "active", 1);
                            var imgName = parts[parts.length - 1];
                            while (imgName.indexOf('%20') != -1)
                                imgName = imgName.replace("%20", " ");

                            var path = "./Designer/Products/" + imgName;
                            j8(path);
                        });
                    } else {
                        parts = src.split("Designer/Products/");
                        var imgName = parts[parts.length - 1];
                        while (imgName.indexOf('%20') != -1)
                            imgName = imgName.replace("%20", " ");

                        var path = "./Designer/Products/" + imgName;
                        j8(path);
                    }

                }
           // }
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
        if ($(srcElement).attr('class') == "imgCarouselDiv draggable2 bkImg  ui-draggable") {
            var id = $(srcElement).attr('id');
            k32(id, TemplateID, src);
        } else {
            // l2
           // e0(); // l3
            
            if (src.indexOf(".svg") == -1) {
                b4(src); 
                d1ToCanvasCC(src, IW, IH);
            } else {
                d1SvgToCCC(src, IW, IH);
            }
        }
        pcL36('hide','#divImageDAM');
    }
}

function k0() {
    $("#sliderFrame").html('<p class="sliderframeMsg">Click on image below to see higher resolution preview.</p><div id="slider">  </div> <div id="thumbs"></div> <div style="clear:both;height:0;"></div>');
    if (IsCalledFrom == 1 || IsCalledFrom == 2) {
        $(".sliderframeMsg").css("display","none");
    }
    if (IsBC) {
        $('#PreviewerContainer').css("width", "800px");
        $('#Previewer').css("width", "776px");
        $('#sliderFrame').css("width", "740px");
        $('#slider').css("width", "542px");
        $('#previewProofing').css("width", "760px");
//	    $('.divTxtProofing').css("margin-left", "208px");
        $('#PreviewerContainer').css("height", "562px");
        $('#PreviewerContainer').css("left", (($(window).width() - $('#PreviewerContainer').width()) / 2) + "px");
        $('#PreviewerContainer').css("top", (($(window).height() - $('#PreviewerContainer').height()) / 2) + "px");
        $('.sliderLine').css("width", "744px");
        $('#Previewer').css("height", ((500-46)) + "px");
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            $('#sliderFrame').css("height", $('#Previewer').height() - 50 - 40 + "px");
            $('#slider').css("height", $('#Previewer').height() - 50 - 40 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 50 - 40 + "px");
            //  $('.mcSlc').css("height", $('#sliderFrame').height() + "px");
        } else {
            $('#sliderFrame').css("height", $('#Previewer').height() - 33 + "px");
            $('#slider').css("height", $('#Previewer').height() - 33 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 33 + "px");
        }
      //  $('.clssInputProofing').css("width", "150px");
        $('.divTxtProofing').css("width", "624px");
        $('.btnBlueProofing').css("width", "108px");
        $('.previewerTitle').css("padding-left", "7px");
        $('.previewerTitle').css("padding-top", "7px");
        $('.previewerTitle').css("padding-bottom", "7px");
    } else {

        if ($(window).width() > 1200  && (IsCalledFrom == 1 || IsCalledFrom == 3)) {
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
            //  $('.mcSlc').css("height", $('#sliderFrame').height() + "px");
        } else {
            $('#sliderFrame').css("height", $('#Previewer').height() - 33 + "px");
            $('#slider').css("height", $('#Previewer').height() - 33 + "px");
            $('#thumbs').css("height", $('#Previewer').height() - 33 + "px");
        }
    }
//	if (IsBC) {
//	    $('#slider').css("height", "350px");
//	    $('#slider').css("width", "600px");
//	    $('#slider').css("margin-right", "184px");
//	    $('#slider').css("margin-top", "60px");
//    }

    $.each(TP, function (i, IT) {

        $("#slider").append('<img src="designer/products/' + TemplateID + '/p' + IT.PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '"  alt="' + IT.PageName + '" />');
        $("#thumbs").append(' <div id="thumbPage' + IT.ProductPageID + '" class="thumb"><div class="frame"><img src="designer/products/' + TemplateID + '/p' + IT.PageNo + '.png?r=' + fabric.util.getRandomInt(1, 100) + '" class="thumbNailFrame" /></div><div class="thumb-content"><p>' + IT.PageName + '</p></div><div style="clear:both;"></div></div>');

    });

    $.each(TP, function (i, IT) {

        $("#slider").append('<img class="overlayLayer' + IT.ProductPageID + '" style="visibility:hidden;" src="designer/products/' + TemplateID + '/p' + IT.PageNo + 'overlay.png?r=' + fabric.util.getRandomInt(1, 100) + '"  alt="' + IT.PageName + '" />');
        $("#thumbs").append(' <div id="overlayLayer' + IT.ProductPageID + '" style="visibility:hidden;" class="thumb"><div class="frame"><img src="designer/products/' + TemplateID + '/p' + IT.PageNo + 'overlay.png?r=' + fabric.util.getRandomInt(1, 100) + '" class="thumbNailFrame" /></div><div class="thumb-content"><p>' + IT.PageName + ' - Overlay Layer</p></div><div style="clear:both;"></div></div>');
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
        $(".PreviewerDownloadPDFCorp").css("display", "block");
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
                $("#loadingMsg").html("Loading content please wait..");
                StartLoader();
                img.onload = function () {
                    StopLoader();
                    var src = "Previewer.aspx?tId=" + TemplateID + "&pID=" + im[0];
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
                img.src = "designer/products/" + TemplateID + "/" + im[0];
                
               
                
            }
          
        });
    }

}

function k1(PI, IsStr) {
    var D1AO = canvas.getActiveObject();
    var D1AG = canvas.getActiveGroup();
    if (D1AG) {
        canvas.discardActiveGroup();
    } else if (D1AO) {
        canvas.discardActiveObject();
    }
    canvas.renderAll();
    $("#bcCarouselImg" + TP[BCBackSide].ProductPageID).css("border", "none");
    $("#bcCarouselImg" + PI).css("border", "solid 1px");

    var oi = BCBackSide;
    var ci = BCBackSide;
    var cv = $(".bcCarouselImgContainer").width() + 15;
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == PI) {
            BCBackSide = i;
            ci = i;
        }
        if (IT.IsPrintable == false) {
            //$("#bcCarouselEnable" + IT.ProductPageID).css("visibility", "visible");
            $("#bcCarouselEnable" + IT.ProductPageID).attr("src", 'assets/BcPageNotPrintable.png');
        } else {
            //$("#bcCarouselEnable" + IT.ProductPageID).css("visibility", "hidden");
            $("#bcCarouselEnable" + IT.ProductPageID).attr("src", 'assets/BcPagePrintable.png');
        }

    });

    if (ci == 1) {
        $(".btnBCcarouselPrevious").css("visibility", "hidden");
        $(".btnBCcarouselNext").css("visibility", "visible");
        if (TP.length == 2) {
            $(".btnBCcarouselNext").css("visibility", "hidden");
        }
    } else if (ci == TP.length-1) {
        $(".btnBCcarouselNext").css("visibility", "hidden");
        $(".btnBCcarouselPrevious").css("visibility", "visible");
    } else {
        $(".btnBCcarouselNext").css("visibility", "visible");
        $(".btnBCcarouselPrevious").css("visibility", "visible");
}
if (TP[ci].IsPrintable == false) {
    $('.btnToggleSelectBack').attr("title", "Enable this design");
    $('.btnToggleSelectBack').css("background", 'url("assets/BcPagePrintable.png")');
    $('.lblBCremoveSide').html("Enable this design");
} else {
    $('.btnToggleSelectBack').attr("title", "Remove this design");
    $('.btnToggleSelectBack').css("background", 'url("assets/BcPageNotPrintable.png")');
    $('.lblBCremoveSide').html("Remove this design");
}
if (IsStr == true) {
    d5(PI);
    $("#" + "bcCarouselBigImg" + PI).css("visibility", "hidden");
} else {
    $(".bcBackImgs").css("z-index", "3");
    $('.bcBackImgs  img').each(function (i) {
        $(this).css("visibility", "visible");
    });
    $(".canvas-container").css("visibility", "hidden");
    $("#btnFlipSides").css("visibility", "hidden");
    $("#btnNextStepBC").css("visibility", "hidden");
    //id = "bcCarouselSmallImg' + DT[i].ProductPageID + '"
    $('.btnToggleSelectBack').css("visibility", "hidden");
    $('.lblBCremoveSide').css("visibility", "hidden");

    var w = 0;
    var le = 0;
    var LeS = 0;
    if (ci > oi) {
        w = ci - oi;
        if (TP[1].Orientation == 1) {
            le = (Template.PDFTemplateWidth + 50) * w;
            LeS = 96 * w;
        } else {
            le = (Template.PDFTemplateHeight + 50) * w;
            LeS = 60 * w;
        }
        
    } else {
        w = oi - ci;
        if (TP[1].Orientation == 1) {
            le = (Template.PDFTemplateWidth + 50) * w * -1;
            LeS = -96 * w;
        } else {
            le = (Template.PDFTemplateHeight + 50) * w * -1;
            LeS = -60 * w;
        }
        
    }
    var L = "-=" + le + "px";

    $('.bcBackImgs  img').each(function (i) {
        if (i <= w) {
        }
    });
    var LeSt = "-=" + LeS + "px";
    $(".bcCarouselImages").animate({ "left": LeSt }, "slow");


    $('.bcBackImgs  img').each(function (i) {
        $(this).animate({
            "left": L
        }, {
            duration: 1200,
            complete: function () {
                var d = $(this).attr('id');

                if (d == "bcCarouselBigImg" + PI) {
                    $("#btnFlipSides").css("visibility", "visible");
                    $("#btnNextStepBC").css("visibility", "visible");
                    $('.btnToggleSelectBack').css("visibility", "visible");
                    $('.lblBCremoveSide').css("visibility", "visible");
                    var is = canvas.toDataURL('jpeg');
                    $("#bcCarouselBigImg" + TP[oi].ProductPageID).attr('src', is);
                    $("#bcCarouselSmallImg" + TP[oi].ProductPageID).attr('src', is);

                    d5(PI);
                    $(".canvas-container").css("visibility", "visible");
                    if (BCBackSide == ci) {
                        $("#" + d).css("visibility", "hidden");
                    }
                    $(".bcBackImgs").css("z-index", "1");
                }

            }
        });
    });
    }
}

function k2() {
    $.each(TP, function (i, IT) {
        if (i != 0) {
            d5(IT.ProductPageID);
            var is = canvas.toDataURL('jpeg');
            $("#bcCarouselBigImg" + IT.ProductPageID).attr('src', is);
            $("#bcCarouselSmallImg" + IT.ProductPageID).attr('src', is);
        }
    });
    d5(TP[0].ProductPageID);
}
function k3(pid) {
    $.each(TP, function (i, IT) {
        if (IT.ProductPageID == pid) {
            if (IT.IsPrintable == false) {
                IT.IsPrintable = true;
            } else {
                IT.IsPrintable = false;
            }
        }
        if (IT.IsPrintable == false) {
            //$("#bcCarouselEnable" + IT.ProductPageID).css("visibility", "visible");
            $("#bcCarouselEnable" + IT.ProductPageID).attr("src", 'assets/BcPageNotPrintable.png');
        } else {
            //$("#bcCarouselEnable" + IT.ProductPageID).css("visibility", "hidden");
            $("#bcCarouselEnable" + IT.ProductPageID).attr("src", 'assets/BcPagePrintable.png');
        }

    });
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
    var dL =( $("#inputPositionX").val()* dfZ1l) - l;
    var dT =( $("#inputPositionY").val()* dfZ1l) - t;
    if (D1AO && (D1AO.type === 'text' || D1AO.type === 'i-text')) {
        dL =( $("#inputPositionXTxt").val()* dfZ1l) - l;
        dT =( $("#inputPositionYTxt").val()* dfZ1l) - t;
    }
  //  if (l != (dL * dfZ1l) || t != (dT * dfZ1l)) {
        D1AO.left += dL;
        D1AO.top += dT;
        // c2(D1AO);
        canvas.renderAll(); D1AO.setCoords();
    //}
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
    //var oldH1 = D1AO.getHeight();
    if (D1AO.type === 'text' || D1AO.type === 'i-text') {
        var h = $("#inputObjectHeightTxt").val();
        var oldH = (D1AO.getHeight());
        console.log(oldH + " " + D1AO.getHeight());
        if (oldH != (h * dfZ1l)) {
            D1AO.maxHeight = h;
            var newScaleY = D1AO.maxHeight / D1AO.height;
            var height = newScaleY * D1AO.height;
            D1AO.set('height', height);
            D1AO.set('maxHeight', height);
            dif = D1AO.getHeight() - oldH;
            dif = dif / 2
            D1AO.top = D1AO.top + dif;
            canvas.renderAll(); D1AO.setCoords();
            k4();
        }

    } else {
        var h = $("#inputObjectHeight").val()* dfZ1l;
        var oldH = D1AO.getHeight();
        if (oldH != (h)) {
            D1AO.maxHeight = h;
            D1AO.scaleY = D1AO.maxHeight / D1AO.height;
            var dif = D1AO.getHeight() - oldH;
            dif = dif / 2
            D1AO.top = D1AO.top + dif;
            canvas.renderAll(); D1AO.setCoords();
            k4();
        }
    }
  //  c2(D1AO);
   
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
        var o =  $("#inputObjectAlpha").val();
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
        if(oldW != (w* dfZ1l)) {
            D1AO.maxWidth = w;
            var scaleX = D1AO.maxWidth / D1AO.width;
            var width = D1AO.width * scaleX;
            D1AO.set('width', width);
            D1AO.set('maxWidth', width);
            var dif = D1AO.getWidth() - oldW;
            dif = dif / 2
            D1AO.left = D1AO.left + dif;
            canvas.renderAll(); D1AO.setCoords();
            k4();
        }
    } else {
        var w = $("#inputObjectWidth").val() * dfZ1l;
        var oldW = D1AO.getWidth();
        if (oldW != (w  )) {
            D1AO.maxWidth = w ;
            D1AO.scaleX = D1AO.maxWidth / D1AO.width;
            var dif = D1AO.getWidth() - oldW;
            dif = dif / 2
            D1AO.left = D1AO.left + dif;
            canvas.renderAll(); D1AO.setCoords();
            k4();
        }
    }
  //  c2(D1AO);
  
}
function k9() {
    //var ra = fabric.util.getRandomInt(1, 1000);

    if ($('#slider') != undefined) {
        var s = $('#slider').css('background-image');
        if (s != undefined) {
            var p = s.split("?");
            //var i = p[0];
            if (s.indexOf("asset") == -1) {
                var temp = p[0].split("http://");
                var t2 = temp[1].split(".png");
                var i = 'http://' + t2[0] + '.pdf'; //+= '?r=' + ra ;
                if (isMultiPageProduct) {
                    var t3 = t2[0].split("/");
                    var res = 'http://';
                    for (var ip = 0 ; ip < t3.length-1; ip++) {
                        res += t3[ip] + "/";
                    } res += 'pages.pdf';

                    if (IsCalledFrom == 2) {
                        $(".PreviewerDownloadPDFCorp").attr("href", res);
                    } else {
                        $(".PreviewerDownloadPDF").attr("href", res);
                    }
                } else {
                    if (IsCalledFrom == 2) {
                        $(".PreviewerDownloadPDFCorp").attr("href", i);
                    } else {
                        $(".PreviewerDownloadPDF").attr("href", i);
                    }
                }
                
                
               // alert(i);
            }
        }
    }
}
function k9_im() {
    //var ra = fabric.util.getRandomInt(1, 1000);

    if ($('#slider') != undefined) {
        var s = $('#slider').css('background-image');
        if (s != undefined) {
            var p = s.split("?");
            if (s.indexOf("asset") == -1) {
                var temp = p[0].split("http://");
                $(".PreviewerDownloadImg").attr("href", "http://" + temp[1]);
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
                    //D1AO.fontSize = fontSize;
              //      c2(D1AO);
                    canvas.renderAll();
                }
            }
        } else {
            alert("Please enter valid font size!");
            $("#BtnFontSize").val(k13(D1AO.get('fontSize')));
            $("#BtnFontSizeRetail").val(k13(D1AO.get('fontSize')));
        }
    } else {
        alert("Please enter valid font size!");
        $("#BtnFontSize").val(k13(D1AO.get('fontSize')));
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
    if (D1AO &&( D1AO.type === 'text' || D1AO.type === 'i-text')) {
        D1AO.lineHeight = $("#txtLineHeight").val();
     //   $("#txtAreaUpdateTxt").css("line-height", $("#txtLineHeight").val());
    }
   // c2(D1AO);
    canvas.renderAll();

}
function insertAtCaret(Id, text) {
    var txtarea = document.getElementById(Id);
    var scrollPos = txtarea.scrollTop;
    var strPos = 0;
    var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
        "ff" : (document.selection ? "ie" : false));
    if (br == "ie") {
        txtarea.focus();
        var range = document.selection.createRange();
        range.moveStart('character', -txtarea.value.length);
        strPos = range.text.length;
    }
    else if (br == "ff") strPos = txtarea.selectionStart;

    var front = (txtarea.value).substring(0, strPos);
    var back = (txtarea.value).substring(strPos, txtarea.value.length);
    txtarea.value = front + text + back;
    strPos = strPos + text.length;
    if (br == "ie") {
        txtarea.focus();
        var range = document.selection.createRange();
        range.moveStart('character', -txtarea.value.length);
        range.moveStart('character', strPos);
        range.moveEnd('character', 0);
        range.select();
    }
    else if (br == "ff") {
        txtarea.selectionStart = strPos;
        txtarea.selectionEnd = strPos;
        txtarea.focus();
    }
    txtarea.scrollTop = scrollPos;
    var D1AO = canvas.getActiveObject();
    if (D1AO) {
        var text = $('#txtAreaUpdateTxt').val();
        var textLength = text.length;
        if (textLength > 0) {
            D1AO.text = $("#txtAreaUpdateTxt").val();
            D1AO.saveState();
          //  c2(D1AO);
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
}
function k16(TempImgType,ImC,Caller) {
    //var TempImgType = 1;
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
            jsonPath += "http://designerv2.myprintcloud.com/"
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
        if ($('#inputSearchTBkg').val() != "") {
            searchTerm = $('#inputSearchTBkg').val();
        }
        isBackground = true;
    } else if (TempImgType == 2) {
        strName = "divGlobImgContainer";
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
        if ($('#inputSearchGImg').val() != "") {
            searchTerm = $('#inputSearchGImg').val();
        }
    }
    else if (TempImgType == 3) {
        strName = "divGlobBkImgContainer";
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
        if ($('#inputSearchGbkg').val() != "") {
            searchTerm = $('#inputSearchGbkg').val();
        }
        isBackground = true;
    } 
    else if (TempImgType == 4) {
        strName = "divPersImgContainer";
        if ($('#inputSearchPImg').val() != "") {
            searchTerm = $('#inputSearchPImg').val();
        }
    } else if (TempImgType == 5) {
        strName = "divPersBkImgContainer";
        if ($('#inputSearchPBkg').val() != "") {
            searchTerm = $('#inputSearchPBkg').val();
        }
        isBackground = true;
    } else if (TempImgType == 6) {
        strName = "divGlobImgContainer";
        if ($('#inputSearchGImg').val() != "") {
            searchTerm = $('#inputSearchGImg').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    } else if (TempImgType == 7) {
        strName = "divGlobBkImgContainer";
        if ($('#inputSearchGbkg').val() != "") {
            searchTerm = $('#inputSearchGbkg').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
        isBackground = true;
    } else if (TempImgType == 8) {
        strName = "divPersImgContainer";
        if ($('#inputSearchPImg').val() != "") {
            searchTerm = $('#inputSearchPImg').val();
        }
    }
    else if (TempImgType == 9) {
        strName = "divPersBkImgContainer";
        if ($('#inputSearchPBkg').val() != "") {
            searchTerm = $('#inputSearchPBkg').val();
        }
        isBackground = true;
    } else if (TempImgType == 10) {
        strName = "divPersImgContainer";
        if ($('#inputSearchPImg').val() != "") {
            searchTerm = $('#inputSearchPImg').val();
        }
    } else if (TempImgType == 11) {
        strName = "divPersBkImgContainer";
        if ($('#inputSearchPBkg').val() != "") {
            searchTerm = $('#inputSearchPBkg').val();
        }
        isBackground = true;
    }
    else if (TempImgType == 13) {
        strName = "divShapesContainer";
        if ($('#inputSearchShapes').val() != "") {
            searchTerm = $('#inputSearchShapes').val();
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
        if ($('#inputSearchLogos').val() != "") {
            searchTerm = $('#inputSearchLogos').val();
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
        if ($('#inputSearchPLogos').val() != "") {
            searchTerm = $('#inputSearchPLogos').val();
        }
        if (IsCalledFrom == 3) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 16) {
        strName = "divShapesContainer";
        if ($('#inputSearchShapes').val() != "") {
            searchTerm = $('#inputSearchShapes').val();
        }
        if (IsCalledFrom == 1 || IsCalledFrom ==2) {
            ImIsEditable = true;
        } else {
            ImIsEditable = false;
        }
        //isBackground = true;
    }
    else if (TempImgType == 17) {
        strName = "divLogosContainer";
        if ($('#inputSearchLogos').val() != "") {
            searchTerm = $('#inputSearchLogos').val();
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
        if ($('#inputSearchIllustrations').val() != "") {
            searchTerm = $('#inputSearchIllustrations').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    }
    else if (TempImgType == 19) {
        strName = "divFramesContainer";
        if ($('#inputSearchFrames').val() != "") {
            searchTerm = $('#inputSearchFrames').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    }
    else if (TempImgType == 20) {
        strName = "divBannersContainer";
        if ($('#inputSearchBanners').val() != "") {
            searchTerm = $('#inputSearchBanners').val();
        }
        if (IsCalledFrom == 3 || IsCalledFrom == 4) {
            ImIsEditable = false;
        }
    }
    jsonPath += "Services/imageSvcDam/" + IsCalledFrom + "," + TempImgType + "," + TemplateID + "," + CustomerID + "," + ContactID + "," + Territory + "," + ImC + "," + searchTerm
    oldHtml = $("." + strName).html();
  //  $("." + strName).html("");
    $.getJSON(jsonPath,
            function (DT) {
                // alert(DT);
                if (Caller != "Loader") {
                    StopLoader();
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
                        } else if (TempImgType == 3 ) {
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
                        $(".btn" + strName).css("display", "none");

                    }
                }
                else {
                    // $(".imCount" + strName).html(DT.ImageCount + " Images found.");
                    $(".imCount" + strName).html("Drag an image to canvas.");
                    $.each(DT.objsBackground, function (j, IT) {
                        LiImgs.push(IT); // for image size 
                        var url = "./" + IT.BackgroundImageRelativePath;
                        if (IsCalledFrom == 3) {
                            if (TempImgType == 6 || TempImgType == 7 || TempImgType == 13 || TempImgType == 14 || TempImgType == 18 || TempImgType == 19 || TempImgType == 20) {
                                url = "http://designerv2.myprintcloud.com/" + IT.BackgroundImageRelativePath;
                            }
                        }
                        var title = IT.ID;
                        var index = TemplateID;
                        var draggable = 'draggable2';
                        var bkContainer = '';
                        if (isBackground) {
                            draggable = "draggable2 bkImg";
                            bkContainer =  ' <button class="btnImgSetBk" onclick=k32(' + title + "," + index + ',"' + url + '")>Set as Background</button> ';
                        }
                        var urlThumbnail = "";
                        if (url.indexOf('.svg') == -1) {
                            var p = url.split('.');
                            for (var z = 0; z <= p.length - 2; z++) {
                                if (p[z] != "") {
                                    if (z == 0 && IsCalledFrom == 3) {
                                        urlThumbnail +=  p[z];
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
                            $("." + strName).append('<div  class="DivCarouselImgContainerStyle2"><img src="' + urlThumbnail + '" class="imgCarouselDiv ' + draggable + ' " style=" z-index:1000; "id = "' + title + '" alt="' + url + '"  />   ' + bkContainer + ' <a class="EditImgBtn" onclick=k26(' + title + "," + index + ') ><img id = "" class="btnEditImg" src = " ./assets/editImgIcon.png " width="13px"/></a>   </div> ');
                        } else {
                            $("." + strName).append('<div  class="DivCarouselImgContainerStyle2"><img src="' + urlThumbnail + '" class="imgCarouselDiv ' + draggable + ' " style=" z-index:1000; "id = "' + title + '" alt="' + url + '"   />  ' + bkContainer + ' </div> ');

                        }
                        $("#" + title).click(function (event) {
                            j9(event, url, title);
                        });
                    });
                    
                    $(".imgOrignalCrop").draggable({
                        //snap: '#dropzone',
                        //snapMode: 'inner',
                        //revert: 'invalid',
                        //helper: 'clone',
                        //appendTo: "body",
                        //cursor: 'move',
                        //helper: function () {
                        //    var helper = $(this).clone(); // Untested - I create my helper using other means...
                        //    // jquery.ui.sortable will override width of class unless we set the style explicitly.
                        //    helper.css({ 'width': '150px', 'height': '150px' });
                        //    return helper;
                        //}

                    });
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
                            helper.css({ 'width': '150px', 'height': '150px' });
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
    StartLoader();
    TeImC += 1;
    k16(1,TeImC,"fun");
}
function k17Bk() {
    StartLoader();
    TeImCBk += 1;
    k16(12, TeImCBk, "fun");
}
function k18() {
    StartLoader();
    if (TeImC != 1) {
        TeImC -= 1;
    }
    k16(1, TeImC, "fun");
}
function k18Bk() {
    StartLoader();
    if (TeImCBk != 1) {
        TeImCBk -= 1;
    }
    k16(12, TeImCBk, "fun");
}
function k19() {
    StartLoader();
    TeImC = 1;
    $(".divTempImgContainer").html("");
    $(".btndivTempImgContainer").css("display", "block");
    k16(1, TeImC, "fun");
}
function k19Bk() {
    StartLoader();
    TeImCBk = 1;
    $(".divTempBkImgContainer").html("");
    $(".btndivTempBkImgContainer").css("display", "block");
    k16(12, TeImCBk, "fun");
}
function k20() {
    StartLoader();
    if (GlImC != 1) {
        GlImC -= 1;
    }
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(2, GlImC, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(6, GlImC, "fun");
    }
}
function k20Bk() {
    StartLoader();
    if (GlImCBk != 1) {
        GlImCBk -= 1;
    }
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(3, GlImCBk, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(7, GlImCBk, "fun");
    }
}
function k21() {
    StartLoader();
    GlImC += 1;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(2, GlImC, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(6, GlImC, "fun");
    }
}
function k21Bk() {
    StartLoader();
    GlImCBk += 1;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        k16(3, GlImCBk, "fun");
    }
    else if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(7, GlImCBk, "fun");
    }
}
function k21Sh() {
    StartLoader();
    GlShpCn += 1;
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(13, GlShpCn, "fun");
    } else {
        k16(16, GlShpCn, "fun");
    }
}
function k21Log() {
    StartLoader();
    GlLogCn += 1;
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(14, GlLogCn, "fun");
    } else {
        k16(17, GlLogCn, "fun");
    }
}
function k21PLog() {
    StartLoader();
    GlLogCnP += 1;
    k16(15, GlLogCnP, "fun");
}
function k22() {
    StartLoader();
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
function k22Bk() {
    StartLoader();
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

function k23() {
    StartLoader();
    if (UsImC != 1) {
        UsImC -= 1;
    }
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
function k23Bk() {
    StartLoader();
    if (UsImCBk != 1) {
        UsImCBk -= 1;
    }
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
    StartLoader();
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
function k24ilus() {
    StartLoader();
    GlIlsC += 1;
    k16(18, GlIlsC, "fun");
}
function k24frames() {
    StartLoader();
    GlframC += 1;
    k16(19, GlframC, "fun");
}
function k24banners() {
    StartLoader();
    GlBanC += 1;
    k16(20, GlBanC, "fun");
}
function k25Ills() {
    StartLoader();
    GlIllsEx = 1;
    $(".divIllustrationContainer").html("");
    $(".btndivIllustrationContainer").css("display", "block");
    k16(18, GlIlsC, "fun");
}
function k25Frames() {
    StartLoader();
    GlframC = 1;
    $(".divFramesContainer").html("");
    $(".btndivFramesContainer").css("display", "block");
    k16(19, GlframC, "fun");
}
function k25Banners() {
    StartLoader();
    GlBanC = 1;
    $(".divBannersContainer").html("");
    $(".btndivBannersContainer").css("display", "block");
    k16(20, GlBanC, "fun");
}
function k24Bk() {
    StartLoader();
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
function k25() {
    StartLoader();
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
    StartLoader();
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

function k26(id, n) {
    imgSelected = id;
    var imToLoad = parseInt(id);
    pcL36('show',"#divImageEditScreen");
    // $("#prgoressLoadingImg").css("display", "block");
    StartLoader();
    $(".ImageContainer").css("display", "none");
    $("#progressbar").css("display", "none");
    $.getJSON("services/imageSvcDam/" + imToLoad,
    function (DT) {
        $("#InputImgTitle").val(DT.ImageTitle);
        $("#InputImgDescription").val(DT.ImageDescription);
        $("#InputImgKeywords").val(DT.ImageKeywords);
        $("#ImgDAMDetail").attr("src", "./" + DT.BackgroundImageRelativePath);
        // image set type 12 = global logos
        // image set type 13 = global shapes/icons
        if (DT.ImageType == 14) {
            $("#radioImageLogo").prop('checked', true);
        } else if (DT.ImageType == 15) {
            $("#radioImageLogo").prop('checked', true);
        } else if (DT.ImageType == 13) {
            $("#radioImageShape").prop('checked', true);
        } else if (DT.ImageType == 17) {
            $("#radioImageLogo").prop('checked', true);
        } else if (DT.ImageType == 16) {
            $("#radioImageShape").prop('checked', true);
        } else if (DT.ImageType == 18) {
            $("#radioBtnIllustration").prop('checked', true);
        } else if (DT.ImageType == 19) {
            $("#radioBtnFrames").prop('checked', true);
        } else if (DT.ImageType == 20) {
            $("#radioBtnBanners").prop('checked', true);
        } else {
            $("#radioImagePicture").prop('checked', true);
        } 
        if (IsCalledFrom == 2) {
            $.getJSON("services/imageSvcDam/getTerritories/" + imToLoad,
            function (DT) {
                $('#dropDownTerritories  div :input').each(function (i) {
                    $(this).attr('checked', false);
                });
                $.each(DT, function (i, IT) {
                    $("#ter_" + IT.TerritoryID).attr('checked', true);
                });
                StopLoader();
            });
        } else {
            StopLoader();
        }
        //  $("#prgoressLoadingImg").css("display", "none");
        $(".ImageContainer").css("display", "block");
    });
}


function k27() {
    k25();
    k22();
    k19();
    k25Bk();
    k22Bk();
    k19Bk();
    k22Log();
    k22Sh();
    if (IsCalledFrom == 3) {
        k22LogP();
    }
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k25Ills();
        k25Frames();
        k25Banners();
    }
}
function k22Sh() {
    StartLoader();
    GlShpCn = 1;
    $(".divShapesContainer").html("");
    $(".btndivShapesContainer").css("display", "block");
   // k16(13, GlShpCn, "fun");
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(13, GlShpCn, "fun");
    } else {
        k16(16, GlShpCn, "fun");
    }

}
function k22Log() {
    StartLoader();
    GlLogCn = 1;
    $(".divLogosContainer").html("");
    $(".btndivLogosContainer").css("display", "block");
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        k16(14, GlLogCn, "fun");
    } else {
        k16(17, GlLogCn, "fun");
    }
   

}
function k22LogP() {
    StartLoader();
    GlLogCnP = 1;
    $(".divPLogosContainer").html("");
    $(".btndivPLogosContainer").css("display", "block");
    k16(15, GlLogCnP, "fun");

}
function k28() {

    $.getJSON("../services/Webstore.svc/GetTerritories?ContactCompanyID=" + CustomerID,
        function (xdata) {
            $.each(xdata, function (i, item) {
                k29("dropDownTerritories","ter_"+ item.TerritoryID, item.TerritoryName,"territroyContainer");
            });
        });
}
function k29(divID, itemID, itemName,Container) {
    var html = '<div class="checkboxRowsTxt"><input type="checkbox" id="' + itemID + '"><label for="' + itemID + '">' + itemName + '</label></div>';
    $('#' + divID).append(html);
    $('#' + Container).css("display", "block");
}
function k30(imageID) {
    var Territories = "_";
    $('#dropDownTerritories  div :input').each(function (i) {
        if ($(this).prop('checked')) {
            var arr = $(this).attr('id').split('_');
            Territories += arr[arr.length - 1] + "_";
        }
    });
    //if (Territories != "") {
        StartLoader();
        $.getJSON("services/imageSvcDam/" + imageID + "," + Territories,
        function (DT) {
            StopLoader();  
            pcL36('show','#divImageDAM');
        });
   // }
}

function k31(cCanvas, IO) {
    TIC += 1;
    if (IO.MaxWidth == 0) {
        IO.MaxWidth = 50;
    }
    if (IO.MaxHeight == 0) {
        IO.MaxHeight = 50;
    }
    var url = IO.ContentString;
    if (url == "{{ListingImage1}}") {
        url = "./assets/placeholder1.png";
    } else if (url == "{{ListingImage2}}") {
        url = "./assets/placeholder2.png";
    } else if (url == "{{ListingImage3}}") {
        url = "./assets/placeholder3.png";
    } else if (url == "{{ListingImage4}}") {
        url = "./assets/placeholder4.png";
    } else if (url == "{{ListingImage5}}") {
        url = "./assets/placeholder5.png";
    } else if (url == "{{ListingImage6}}") {
        url = "./assets/placeholder6.png";
    } else if (url == "{{ListingImage7}}") {
        url = "./assets/placeholder7.png";
    } else if (url == "{{ListingImage8}}") {
        url = "./assets/placeholder8.png";
    } else if (url == "{{ListingImage9}}") {
        url = "./assets/placeholder9.png";
    } else if (url == "{{ListingImage10}}") {
        url = "./assets/placeholder10.png";
    } else if (url == "{{ListingImage11}}") {
        url = "./assets/placeholder11.png";
    } else if (url == "{{ListingImage12}}") {
        url = "./assets/placeholder12.png";
    } else if (url == "{{ListingImage13}}") {
        url = "./assets/placeholder13.png";
    } else if (url == "{{ListingImage14}}") {
        url = "./assets/placeholder14.png";
    } else if (url == "{{ListingImage15}}") {
        url = "./assets/placeholder15.png";
    } else if (url == "{{ListingImage16}}") {
        url = "./assets/placeholder16.png";
    } else if (url == "{{ListingImage17}}") {
        url = "./assets/placeholder17.png";
    } else if (url == "{{ListingImage18}}") {
        url = "./assets/placeholder18.png";
    } else if (url == "{{ListingImage19}}") {
        url = "./assets/placeholder19.png";
    } else if (url == "{{ListingImage20}}") {
        url = "./assets/placeholder20.png";
    }
    fabric.Image.fromURL(url, function (IOL) {
        IOL.set({
            left: (IO.PositionX + IO.MaxWidth / 2) * dfZ1l,
            top: (IO.PositionY + IO.MaxHeight / 2) * dfZ1l,
            angle: IO.RotationAngle
        });
        IOL.ImageClippedInfo = IO.ClippedInfo;
        IOL.maxWidth = IO.MaxWidth;
        IOL.maxHeight = IO.MaxHeight;
        IOL.ObjectID = IO.ObjectID;
        IOL.scaleX = (IOL.maxWidth / IOL.width) * dfZ1l;
        IOL.scaleY = (IOL.maxHeight / IOL.height) * dfZ1l;
        IOL.setAngle(IO.RotationAngle);
        IOL.setOpacity(IO.Opacity);
        IOL.IsHidden = IO.IsHidden;
        if (IsCalledFrom == 1 || IsCalledFrom == 2) {
            IOL.lockMovementX = false;
            IOL.lockMovementY = false;
            IOL.lockScalingX = false;
            IOL.lockScalingY = false;
            IOL.lockRotation = false;
            IOL.IsPositionLocked = false;
        //    IOL.IsHidden = false;
            IOL.IsEditable = false;
            IOL.IsTextEditable = true;
        } else {
            IOL.lockMovementX = true;
            IOL.lockMovementY = true;
            IOL.lockScalingX = true;
            IOL.lockScalingY = true;
            IOL.lockRotation = true;
            IOL.IsPositionLocked = true;
       //     IOL.IsHidden = false;
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
    StartLoader();
    if (eleID.indexOf('UserImgs') != -1) {
        var imgtype = 2;
        if (isBKpnl) {
            imgtype = 4;
        }
        $.getJSON("services/imageSvc/DownloadImg/" + n + "," + TemplateID + "," + imgtype,
        function (DT) {
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
            });
            StopLoader();
            canvas.renderAll();
            k27();
            $.each(TP, function (op, IT) {
                if (IT.ProductPageID == SP) {
                    $("#ImgCarouselDiv").tabs("option", "active", 1);
                    IT.BackgroundFileName = Tid + "/" + i;
                    IT.BackGroundType = 3;
                    return;
                }
            });
        });
    } else {
        var bkImgURL = eleID.split("./Designer/Products/"); ;
        StopLoader();
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

                IT.BackgroundFileName = bkImgURL[bkImgURL.length-1];
                IT.BackGroundType = 3;
                return;
            }
        });
    }

}
function l4(caller) {
    if (llData.length > 0 || IsCalledFrom == 1) {
        $("#BtnBCPresets").css("display", "inline-block");
        $(".lblLayouts").css("display", "inline");
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

var br13 = null;
function l5(lID) {
    $.each(llData, function (i, IT) {
        if (IT.LayoutID == lID) {
            $("#presetTitle").val(IT.Title);
            l8(parseInt(IT.ImageLogoType));
            $("#presetLogo").val(IT.ImageLogoType);
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
                    l6(IT.LayoutAttributes, item.Name);
                    var Pres1 = br13;
                    if (Pres1 != null && Pres1 != undefined) {
                        item.maxWidth =parseInt( Pres1.maxWidth);
                        item.maxHeight =parseInt(  Pres1.maxHeight);
                        item.fontSize =parseInt(  Pres1.fontSize);
                        item.textAlign = Pres1.textAlign;
                        item.fontWeight =  Pres1.fontWeight;
                        item.top = parseInt(Pres1.topPos) * dfZ1l;
                        item.left = parseInt(Pres1.LeftPos) * dfZ1l;
                       // c2(item);
                        item.setCoords();
                    }

                }
            });
            canvas.renderAll();
        }
        // $('#btnPreset'+IT.LayoutID).css( //for image
    });
}

function l6(array,attr) {
    $.each(array, function (i, item) {
        if (item.FeildName == attr) {
            br13 = item;
            return;
        }
    });
    return null;
}
var TempOB = [];
function TempFinO(n) {
    var found = false;
    $.each(TO, function (i, item) {
        if (item.Name == n && item.IsQuickText == true && item.ProductPageId == SP) {
            TempFinO2(item.ObjectID,n);
            found = true;
            return false
        }
    });
    if (found == false) {
        TempOB.push("");
    }
}
function TempFinO2(n,no) {
    var OBS = canvas.getObjects();
    $.each(OBS, function (i, item) {
        if (item.ObjectID == n) {
            item.Name = no;
            TempOB.push(item);
            return false;
        }
    });
}
function l8(mode) {

    if (mode == 1) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset5_2.png");
    } else if (mode == 2) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset5_1.png");
    } else if (mode == 3) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset5.png");
    } else if (mode == 4) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset4.png");
    } else if (mode == 5) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset3.png");
    } else if (mode == 6) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset2.png");
    } else if (mode == 7) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset1.png");
    } else if (mode == 8) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset6.png");
    } else if (mode == 9) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset7.png");
    } else if (mode == 10) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset8.png");
    } else if (mode == 11) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset9.png");
    } else if (mode == 12) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset10.png");
    } else if (mode == 13) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset10_1.png"); 
    } else if (mode == 14) {
        $("#imgPreviewPreset").prop("src", "assets/presets/preset10_2.png"); 
    } else if (mode == 15) {
        $("#imgPreviewPreset").prop("src", "assets/presets/presets14.png");
    } else if (mode == 16) {
        $("#imgPreviewPreset").prop("src", "assets/presets/presets-15.png");
    } else if (mode == 17) {
        $("#imgPreviewPreset").prop("src", "assets/presets/presets16.png");
    } else if (mode == 18) {
        $("#imgPreviewPreset").prop("src", "assets/presets/presets11.png");
    } else if (mode == 19) {
        $("#imgPreviewPreset").prop("src", "assets/presets/presets12.png"); 
    } else if (mode == 20) {
        $("#imgPreviewPreset").prop("src", "assets/presets/presets-13.png"); 
    }
}
function m0() {
    m0_prePop();
    pcL36('show', '#divLayersPanelRetail');

}
function m0_prePop() {
    $("#divTxtPropPanelRetail").appendTo("#panels");
    $("#divImgPropPanelRetail").appendTo("#panels");
    $("#divImgPropPanelRetail").css("display", "none");
    var OBS = canvas.getObjects();
    var html = '<ul id="sortableLayers">';
    var index1 = 0;
    for (var i = OBS.length - 1; i >= 0; i--) {
        //  $.each(OBS, function (i, ite) {
        var ite = OBS[i]; 
        $.each(TO, function (ij, IT) {
            if (ite.ObjectID == IT.ObjectID && ite.IsEditable != false) {
                if (i == 0) {
                    index1 = -1;
                } 
                if (ite.type == "image") {
                    html += m0_i9(ite.ObjectID, 'Image Object', ite.type, ite.getSrc(), index1);
                } else if (ite.type == "text" || ite.type == "i-text") {
                    html += m0_i9(ite.ObjectID, IT.ContentString, ite.type, "./assets/txtObject.png", index1);
                } else if (ite.type == "ellipse") {
                    html += m0_i9(ite.ObjectID, 'Ellipse Object', ite.type, "./assets/circleObject.png", index1);
                } else {
                    html += m0_i9(ite.ObjectID, 'Shape Object', ite.type, "./assets/rectObject.png", index1);
                }
                index1 += 1;
            } 
        }); 
        
    }
    //});
    html += '</ul>';
    $("#LayerObjectsContainerRetail").html(html);
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

    var sObj = canvas.getActiveObject();
    if (sObj) {     
        if (sObj.type == "i-text") {
            if ($("#selobj_" + sObj.ObjectID).length > 0) {
                $("#divTxtPropPanelRetail").appendTo("#selobj_" + sObj.ObjectID);
            } else {
                $("#divTxtPropPanelRetail").appendTo("#LayerObjectsContainerRetail");
            }
            if (sObj.IsPositionLocked == true) {
                $("#btnLockTxtObj").css("background", "url('assets/Lock-Lock-icon.png')");
            } else {
                $("#btnLockTxtObj").css("background", "url('assets/Lock-Unlock-icon.png')");
            }
            $("#divTxtPropPanelRetail").css("display", "block");
        } else {
            if ($("#selobj_" + sObj.ObjectID).length > 0) {
                $("#divImgPropPanelRetail").appendTo("#selobj_" + sObj.ObjectID);
            } else {
                $("#divImgPropPanelRetail").appendTo("#LayerObjectsContainerRetail");
            }
            $("#divImgPropPanelRetail").css("display", "block");
            if (sObj.IsPositionLocked == true) {
                $("#btnLockImgObj").css("background", "url('assets/Lock-Lock-icon.png')");
            } else {
                $("#btnLockImgObj").css("background", "url('assets/Lock-Unlock-icon.png')");
            }
        }
        $("#selobj_" + sObj.ObjectID).css("padding", "0px 0px 0px 0px");
    }
    $(".btnMoveLayerUp").click(function () {
        var id = $(this).parent().children(".selectedObjectID").text();
        pcL27_find(id);
        m0_prePop();
    });
    $(".btnMoveLayerDown").click(function () {
        var id = $(this).parent().children(".selectedObjectID").text();
        pcL28_find(id);
        m0_prePop();
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
    
    if (cid == oId) {
        var innerHtml = "";
        html = '<li id="selobj_' + oId + '" class="ui-state-default" style="padding:5px;"><span class="selectedObjectID">' + oId + '</span>  <img class="layerImg" src="' + iURL + '" alt="Image" onclick="j1(' + oId + ')" /> <span class="spanLyrObjTxtContainer" onclick="j1(' + oId + ')">' + oName + '</span>'+btnHtml+' <br /></li>';;//'<li id="selobj_' + oId + '" class="ui-state-default"></li>';
    } else {
        html = '<li id="selobj_' + oId + '" class="ui-state-default" style="padding:5px;"><span class="selectedObjectID">' + oId + '</span>  <img class="layerImg" src="' + iURL + '" alt="Image" onclick="j1(' + oId + ')" /> <span class="spanLyrObjTxtContainer" onclick="j1(' + oId + ')">' + oName + '</span>' + btnHtml + '</li>';

    }
    return html;
}

function AddImgVar(varTag,varId) {
    var center = canvas.getCenter();
    d1PHRealStateToCanvas(center.left - 150, center.top - 150, varTag);
    var objToAdd = { "VariableTag": varTag, "VariableID": varId, "TemplateID": TemplateID };
    varList.push(objToAdd);
}

function d1PHRealStateToCanvas(x, y,varTag) {
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
    D1NIO.ObjectType = 13;

    D1NIO.MaxHeight = 300;
    D1NIO.Height = 300;
    D1NIO.MaxWidth = 300;
    D1NIO.Width = 300;

    D1NIO.IsQuickText = true;
    D1NIO.ContentString = varTag;//"./assets/Imageplaceholder.png";
    D1NIO.DisplayOrder = TO.length + 1;
    d1(canvas, D1NIO);
    var OBS = canvas.getObjects();

    D1NIO.DisplayOrderPdf = OBS.length;
    canvas.renderAll();
    TO.push(D1NIO);

}