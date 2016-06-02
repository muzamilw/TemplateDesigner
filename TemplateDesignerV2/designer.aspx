<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="designer.aspx.cs" Inherits="TemplateDesignerV2.designer" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Designer</title>
    <link rel="stylesheet" href="styles/master.css" />
    <link href="styles/p103.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="styles/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" />
    <link href="styles/DesignerStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="js/a12/a66.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/p25.js"></script>
    <script type="text/javascript" src="js/p64test.js"></script>
    <script type="text/javascript" src="js/p55.js"></script>
    <script type="text/javascript" src="js/p40.js"></script>
    <script src="js/p15.js" type="text/javascript"></script>
    <script src="js/p50.js" type="text/javascript"></script>
    <script src="js/p12.js" type="text/javascript"></script>
    <%-- <script src="js/p31.js" type="text/javascript"></script>--%>
    <%-- <script src="js/p77.js" type="text/javascript"></script>--%>
    <script src="js/p10-v2.js" type="text/javascript"></script>
    <%--<script src="js/p84.js" type="text/javascript"></script>--%>
    <script src="js/p71.js" type="text/javascript"></script>
    <script src="js/p19.js" type="text/javascript"></script>
    <script src="js/Pcf01.js"></script>
    <script src="js/a12/aj9.js" type="text/javascript"></script>
    <script src="js/a12/aj21.js" type="text/javascript"></script>
    <script src="js/a12/aj12.js" type="text/javascript"></script>
    <script src="js/a12/aj1.js" type="text/javascript"></script>
    <script src="js/p101.js" type="text/javascript"></script>
    <script src="js/p1.js"></script>
    <link href="styles/jquery.toastmessage.css" rel="stylesheet" />
    <script src="js/p11.js"></script>
    <%--used for color slider--%>
    <script src="js/p61.js" type="text/javascript"></script>
    <script src="js/pc1.js"></script>
    <script src="js/pc2.js"></script>
    <script src="js/pc3.js"></script>
    <link href="styles/jquery.cropbox.css" rel="stylesheet" />
    <script src="js/XMLWriter.js"></script>
    <!--[if IE]>
		<style>
		#BtnDeleteTxtObj {
			margin-left:-89px;
		}
		#BtnRotateTxtLft
		{
		margin-left:12px;
		}
		#SpanFontSize
		{
			margin-left :114px;
		}
		#SpanLineHeight
		{
			margin-left:14px;
		}
		#BtnChngeClr
		{
			margin-top:-2px;
		}
		#BtnBoldTxt, #BtnItalicTxt
		{
			margin-bottom:5px;
		}

		#DivTxtclr
		{
			 margin-top: -25px;
		}

	
		#MiddlePanelTxt
		{
			margin-top:10px;
		}
		#BtnPasteObj
		{
			margin-left:9px;
		}
		#btnNewTxtPanel
		{
			margin-left:22px;
		}
		.popupUpdateTxt {
			padding:1px 1px 1px 1px;
		}
		</style>
	<![endif]-->
    <!--[if lt IE 9]> 
	  <script src="js/p69.js"></script>
	<![endif]-->
    <script type="text/javascript">
        // polyfill by @paulirish
        if (!window.requestAnimationFrame) {
            window.requestAnimationFrame = (function () {
                return window.webkitRequestAnimationFrame ||
		  window.mozRequestAnimationFrame ||
		  window.oRequestAnimationFrame ||
		  window.msRequestAnimationFrame ||
		  function ( /* function FrameRequestCallback */callback, /* DOMElement Element */element) {
		      window.setTimeout(callback, 1000 / 60);
		  };
            })();
        }
    </script>
    <%-- required for file upload --%>
</head>
<body>
    <!-- Loading Div -->
    <div id="DivShadow" class="opaqueLayer">
    </div>
    <div id="DivLoading" class="loadingLayer">
        <div id="previewershadow" class="opaqueLayer">
        </div>
        <p id="loadingMsg">
            Loading design, please wait..
        </p>
        <p id="ParaLoadFonts" style="display: none;">
            Loading Fonts please wait
        </p>
        <img src="assets/ImgMessage.JPG" class="firstLoadingMsg" alt="loading.." />
        <br />
        <div class="loadingimg">
            <img src="assets/loading.gif" alt="loading.." />
        </div>
    </div>
    <div id="LargePreviewer" class="LargePreviewer" title="Full Size Image">
        <iframe id="LargePreviewerIframe" class="LargePreviewerIframe" src="Previewer.aspx" ></iframe>
    </div>
    <div id="PreviewerContainer" class="ui-corner-all propertyPanel">
        <a class="PreviewerDownloadImg" onclick="k9_im()" target="_blank">Download Image </a>
        <a class="PreviewerDownloadPDF" onclick="k9()" target="_blank">Download PDF </a>
        <div class="PreviewerContainerClose" onclick="e6()">
            Close
        </div>
        <div class="previewerTitle">
            <span class="lightGray">Preview :</span>
        </div>
        <div class="sliderLine">
        </div>
        <div id="Previewer" class="ui-corner-all">
            <div id="sliderFrame">
            </div>
            <div class="sliderLine sliderLineBtm">
            </div>
            <div id="previewProofing">
                <div class="divTxtProofing">
                    <div class="ConfirmPopupProof">
                        <label class="lblChkSpellings" runat="server" id="lblConfirmSpellings">
                            Confirm spellings and details</label>
                        <input id="chkCheckSpelling" name="chkCheckSpelling" runat="server" class="regular-checkbox-new"
                            type="checkbox" />
                        <label for="chkCheckSpelling" style="display: inline;">
                        </label>
                    </div>
                    <div style="margin-top: 17px;">
                        <label style="visibility: hidden;">
                            <span class="">Send </span>email proofs to
                        </label>
                        <input type="email" name="userEmail1" id="userEmail1" class="clssInputProofing" style="visibility: hidden;">
                        <%--E-mail Address 2 :--%>
                        <input type="email" name="userEmail2" id="userEmail2" class="clssInputProofing" style="display: none;">
                    </div>
                </div>
                <button id="btnNextProofing" class="btnBlueProofing">
                    NEXT</button>
            </div>
        </div>
    </div>
    <!-- Loading Div -->
    <img id="placeHolderTxt" src="assets/placeholderTxt.png" />
     <div id="divBkCropTool" class="divBkCropTool">
    </div>
    <div class="NewCropToolCotainer" id="NewCropToolCotainer">
        <div class="CropControls">
            <menu class="crop CroptoolBar underneath" style=" transform: translate3d(-3px, -47px, 0px);">
                <li class="enabled confirmationToolbarOk"><a class="cropButton">Ok</a></li>
                <li class="enabled confirmationToolbarCancel"><a onclick="pcl20_newCropCls();">Cancel</a></li>
            </menu>
          <%--  <div class="closePanelButtonCropTool" onclick="pcl20_newCropCls();">
                <br>
            </div>--%>
            <img class="cropimage" alt="" src="" cropwidth="200" cropheight="200" />
       
            <div class="overlayHoverboxContainer"></div> <div class="overlayHoverbox">
            <img class="imgOrignalCrop" alt="" src=""  />
        </div>
        </div>
        
    </div>
    <div id="Container">
        <div id="bd-wrapper">
            <div id="designer" class="designer">
                <!-- div containing sub menus  -->
                <div id="SubNavList">
                    <div>
                        <div id="DivDimentions">
                        </div>
                        <div id="DivRulerTool">
                            <div class="DivTipsBtnContainer">
                                <div class="DivspanTopMenu">
                                    <p id="GuidesSpan">
                                        Hide Rulers
                                    </p>
                                    <br />
                                    <br />
                                    <br />
                                    <p id="SpanTips">
                                        Hide Tips
                                    </p>
                                </div>
                                <div class="DivspanTopMenu">
                                    <button id="BtnGuides" title="Toggle ruler tool over Canvas">
                                    </button>
                                    <br />
                                    <button id="ShowTips" title="Toggle tips panel ">
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div id="PagesContainer">
                        </div>
                        <div class="previewBtnContainer">
                            <button id="BtnPreviewNew" class="PreviewBtnImg" title="Preview Template PDF">
                            </button>
                            <div id="PageText">
                                Preview
                            </div>
                            <div id="Div2" class="marginTop5">
                                © MyPrintCloud
                            </div>
                        </div>
                        <div id="SubNavListContainer">
                            <div class="CaptionText">
                            </div>
                            <%--	<button id="BtnRuler">
							</button>--%>
                        </div>
                    </div>
                    <div style="display: none;" id="drawing-mode-options">
                        Width:<input value="10" id="drawing-line-width" size="2" />
                        Color:
                        <input type="color" value="rgb(0,0,0)" id="drawing-color" size="15" />
                    </div>
                </div>
                <div runat="server" id="divDesignerClose" visible="false" class="divDesignerClosePanel">
                    <div runat="server" id="DivCloseDesignerBtn" class="closeDesignerButton" onclick="jsvascript:window.location.href = 'nav/EditTemplate.aspx?mode=edit&templateID=' + TemplateID">
                        <br />
                    </div>
                </div>
                <div class="divbtnNextBC">
                    <button id="btnNextStepBC" class="btnNextStepBC">
                        Next</button>
                </div>
                <!-- div containing the floating panels item and canvas -->
                <div id="panels">
                    <div id="divPositioningPanel" class="divPositioningPanel popUpaddTextPanel propertyPanel">
                        <div class="closePanelButton" onclick="pcL36('hide','#divPositioningPanel');">
                            <br />
                        </div>
                        <div class="DivTitleLbl">
                            <label class="lblGroupProperties titletxt">
                                Positioning Properties</label>
                        </div>
                        <div class="DivBlockWithNoHeight divAlignmentsPositioning  marginTop5 ">
                            <div class="leftPanel DivBlockWithNoHeight">
                                <%--    <button id="BtnTxtCanvasAlignLeft" class="btnCj1" title="">
								</button>
								<button id="BtnTxtCanvasAlignCenter" class="btnCj2" title="">
								</button>
								<button id="BtnTxtCanvasAlignRight" class="btnCj3" title="">
								</button>
								<br />
								<button id="BtnTxtCanvasAlignTop" class="btnCj4" title="">
								</button>
								<button id="BtnTxtCanvasAlignMiddle" class="btnCj5" title="">
								</button>
								<button id="BtnTxtCanvasAlignBottom" class="btncj6" title="">
								</button>--%>
                            </div>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <div class="fontSize11" style="padding-left: 8px;">
                            <br />
                            Or use scroll and shift keys
                        </div>
                    </div>
                    <div id="UploadImage" class="popUpUploadImgPanel propertyPanel">
                        <div class="panelItemsRightAligned">
                            <div class="closePanelButtonQuickText" title="Close this panel" onclick="pcL36('hide','#addImage')">
                            </div>
                        </div>
                        <div id="DivUploadImgTxt" class="panelItemsLeftAligned leftalignTxt largeText">
                            <br />
                            <div class="titletxt">
                                Important.
                            </div>
                            For best results, we recommend you upload all images using:
                            <br />
                            <ul>
                                <li>High resolution
                                    <div class="orangeTxt">
                                        JPEG
                                    </div>
                                    format </li>
                                <li>
                                    <div class="orangeTxt">
                                        300dpi
                                        <br />
                                        <br />
                                    </div>
                                </li>
                            </ul>
                            If uploading a full background, allow a bleed of 5 mm
                            <br />
                            for trimming- for business card that a canvas size of
                            <br />
                            95 mm x 65 mm or 1122 x 758 pixels.
                            <br />
                            <br />
                            We also support Gif, PNG ,and SVG images.
                        </div>
                        <div class="panelItemsLeftAligned" style="width: 450px; margin-top: 5px; padding: 0px; margin-left: 0px;">
                        </div>
                    </div>
                    <div id="DivUploadFont" class="propertyPanel">
                        <div class="panelItemsRightAligned">
                            <div class="closePanelButtonQuickText" onclick="pcL36('hide','#DivUploadFont');  e2();">
                            </div>
                        </div>
                        <div id="FontUploadingMsgDiv" class="panelItemsLeftAligned leftalignTxt largeText">
                            <br />
                            <div class="titletxt">
                                Important.
                            </div>
                            <br />
                            For each font you wish to upload, download the font kit from font squirrel font
                            kit generator (http://www.fontsquirrel.com/fontface).
                            <br />
                            <br />
                            <br />
                            <br />
                            <p class="Step1FontUpload">
                                <span class="titletxt">Step 1: </span>Select the 3 font files (.woff, .eot and .ttf
                                files) from Font face for that font family.<br />
                                <br />
                            </p>
                            <p class="Step2FontUpload">
                                <span class="titletxt">Step 2 :</span> Enter in exact FONT Family name as it appears
                                in the TTF Font Definaton field.<br />
                                <br />
                            </p>
                            <p class="Step3FontUpload">
                                <span class="titletxt">Step 3:</span> Confirm that this font is free from any copyright
                                infringements.
                                <br />
                                <br />
                            </p>
                            <p class="Step4FontUpload">
                                <span class="titletxt">Step 4:</span> Click on upload button to complete the new
                                font addition.<br />
                                <br />
                            </p>
                            <p class="Step5FontUpload">
                                <span class="titletxt">Step 5:</span> The new font should appear in your font style
                                list as the name you entered in Step 2.
                                <br />
                                <br />
                            </p>
                        </div>
                        <div class="DivFontUploader">
                            <form id="form2">
                                <div id="FontUploader">
                                    <p class="ErrorMessage textWhite">
                                        You browser doesn't have Flash, Silverlight or HTML5 support.
                                    </p>
                                </div>
                                <div id="DivFontUploadingName" class="largeText">
                                    <input type="text" name="FontName" class="FontNameTxt" />
                                    <p class="ChkBoxFontConfirm">
                                        <input type="checkbox" id="ChkBoxFontAllowed" />
                                    </p>
                                    <p class="uploadFontBtnPara">
                                        <a id="uploadFontFileBtn" onclick="SubmitFontItem(this)">Upload</a>
                                    </p>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div id="DivColorPickerDraggable">
                        <div id="DivColorPallet" class="popUpaddTextPanel propertyPanel">
                            <div class="closePanelButton" onclick="pcL36('hide','#DivColorPallet , #DivAdvanceColorPanel');">
                                <br />
                            </div>
                            <p id="lbtSelectColor" class="titletxt">
                                Select Colour
                            </p>
                            <div id="DivColorContainer">
                            </div>
                            <div>
                                <div id="DivRecentColors">
                                </div>
                                <button id="BtnAdvanceColorPicker" title="Show advance color picker">
                                    More Colours
                                </button>
                            </div>
                        </div>
                        <div id="DivAdvanceColorPanel" class="popUpAdvanceColorPicker propertyPanel">
                            <div class="panelItemsRightAligned">
                                <div class="closePanelButtonQuickText" onclick="pcL36('hide','#DivAdvanceColorPanel')">
                                    <br />
                                </div>
                            </div>
                            <div id="DivLblMoveSlider" class="largeText">
                                Move the Slider to Change Colour Percentage
                            </div>
                            <div id="sliders">
                                <div id="DivColorC">
                                </div>
                                <div id="DivColorM">
                                </div>
                                <div id="DivColorY">
                                </div>
                                <div id="DivColorK">
                                </div>
                            </div>
                            <div id="ColorPickerPercentageContainer">
                                <label for="DivColorC" id="LblDivColorC">
                                    0%</label>
                                <label for="DivColorM" id="LblDivColorM">
                                    0%</label>
                                <label for="DivColorY" id="LblDivColorY">
                                    0%</label>
                                <label for="DivColorK" id="LblDivColorK">
                                    0%</label>
                            </div>
                            <div id="ColorPickerPalletContainer" class="largeText">
                                <label for="ColorPallet" id="LblCollarPalet">
                                    Click to apply</label>
                                <div class="ColorPallet btnClrPallet" style="background-color: White" onclick="f2(0,0,0,0,&quot;#ffffff&quot;);f6(0,0,0,0,&quot;#ffffff&quot;);">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DivCropToolContainer" class="popUpaddTextPanel propertyPanel">
                        <div class="closePanelButton" onclick="pcL36('hide','#DivCropToolContainer');">
                            <br />
                        </div>
                        <div class="DivTitleLbl">
                            <label class="largeText panelTextPropertyRow  titletxt">
                                Crop Tool</label>
                        </div>
                        <p id="P1" class="largeText">
                            Highlight the area on image below to define its new cropped size.
                            <br />
                            <br />
                            <br />
                            <span class="orangeTxt largeText">Important Note: </span>
                            <br />
                            Cropping this image will permanently crop all the instances of this image on all
                            pages including the original uploaded image and resize to fit in the picture box.
                        </p>
                        <div id="CropImgContainer">
                            <img id="CropTarget" src="assets/sprite.png" style="visibility: hidden;" />
                        </div>
                        <button id="BtnApplyCropImg" title="Crop and Replace original Image">
                            Replace original image</button>
                        <button id="BtnApplyCropImgNew" title="Crop and create new Image">
                            Create new image</button>
                    </div>
                    <div id="addImage" class="popUpaddTextPanel addimgPanel propertyPanel">
                        <div class="closePanelButton" title="Close this panel" onclick="pcL36('hide','#addImage');">
                            <br />
                        </div>
                        <p class="titletxt">
                            Image bank
                        </p>
                        <div>
                            <%-- <button id="UploadImgBtn" title="Click to upload new image" onclick="animatedcollapse.toggle('UploadImage')">
                                Upload Image(s).</button>--%>

                            <div class="clearBoth">
                            </div>
                            <%--   <p class="P3">
                                <span class="spanDragImg">Drag images </span><span class="spanImgPlaceHolder">Drag holder</span>
                            </p>--%>
                            <div style="margin-bottom: 5px;">
                                <%--   <button id="btnAddRectangle" class="rect " title="Drag rectangle to Canvas">
                                </button>
                                <button id="btnAddCircle" class="circle " title="Drag circle to Canvas">
                                </button>--%>
                            </div>
                        </div>
                        <div class="panelItemsLeftAligned" id="CarouselDiv">
                            <div id="ImgBnkLbl">
                                <label for="ImageBank">
                                </label>
                            </div>
                            <div id="CarouselImages" style="display: block">
                            </div>
                        </div>
                    </div>
                    <div id="AddTextDragable">
                        <div id="addText" class="popUpaddTextPanel propertyPanel">
                            <div class="closePanelButton" title="Close this panel" onclick="pcL36('hide', '#addText ,#quickText')">
                                <br />
                            </div>
                            <p id="lblQuickTextArea2" class="titletxt">
                                Enter your text here.
                            </p>
                            <div class="checkboxRowsTxt marginLeft12 QtxtChkContainer">
                                <input type="checkbox" id="IsQuickTxtCHK" />
                                <label for="IsQuickTxt" id="Label1" class="QuickTextTextWOMargin ">
                                    Is Quick Text</label>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <div class="QuickTextHfields">
                                <div id="QtxtINRow" class="panelQuickTextFormRow marginLeft16 displayNone">
                                    <div class="qLabel2">
                                        Label title
                                        <%--<span class="lblSequence  displayNone">Sequence</span>--%>
                                    </div>
                                    <input id="txtQTitleChk" maxlength="20" style="width: 202px" />
                                    <div class="qLabel2" style="margin-top: 3px;">
                                        User suggestion mask
                                        <%--<span class="lblSequence  displayNone">Sequence</span>--%>
                                    </div>
                                    <input id="txtQWaterMark" maxlength="20" style="width: 202px" />
                                    <%--<input id="TxtQSequence" class="displayNone" type="text" maxlength="3" style="width: 46px" />--%>
                                </div>
                            </div>
                            <div>
                                <textarea id="txtAddNewText" rows="7" cols="20"></textarea>
                            </div>
                            <button id="BtnAddNewText" title="Drag Text button to place text on canvas">
                                Drag me to canvas
                            </button>
                            <div class="retailTxtBtns" style="display: none;">
                                <p id="btnHeadingTxt" class="txtbtnsRetail">
                                    Add as Heading text
                                    <img src="assets/AddTxtImage.png" width="20" />
                                </p>
                                <p id="btnSubTitleTxt" class="txtbtnsRetail">
                                    Add as Sub title text
                                    <img src="assets/AddTxtImage.png" width="20" />
                                </p>
                                <p id="btnBodyTxt" class="txtbtnsRetail">
                                    Add as Body text
                                    <img src="assets/AddTxtImage.png" width="20" />
                                </p>
                            </div>
                            <div>
                            </div>
                        </div>
                        <div id="quickText" class="popUpQuickTextPanel propertyPanel">
                            <div class="panelItemsRightAligned">
                            </div>
                            <p= id="lblQuickTextArea" class="largeText">
                                Or Drag and Drop the fields below
                            </p>
                            <div id="QuickTxtName" class="QuickTextText" title="Drag this object to the canvas">
                                <br />
                                Your Name
                            </div>
                            <div id="QuickTxtTitle" class="QuickTextText" title="Drag this object to the canvas">
                                Your title
                            </div>
                            <div id="QuickTxtCompanyName" title="Drag this object to canvas" class="QuickTextText">
                                Your company name
                            </div>
                            <div id="QuickTxtCompanyMsg" class="QuickTextText" title="Drag this object to the canvas">
                                Your company message
                            </div>
                            <div id="QuickTxtAddress1" class="QuickTextText" title="Drag this object to the canvas">
                                Address 1
                            </div>
                            <div id="QuickTxtTel" class="QuickTextText" title="Drag this object to the canvas">
                                Tel/ /other
                            </div>
                            <div id="QuickTxtFax" class="QuickTextText" title="Drag this object to the canvas">
                                Fax / other
                            </div>
                            <div id="QuickTxtEmail" class="QuickTextText" title="Drag this object to the canvas">
                                Email address
                            </div>
                            <div id="QuickTxtWebsite" class="QuickTextText" title="Drag this object to the canvas">
                                Website/other
                            </div>
                            <div id="QuickTxtMobile" class="QuickTextText" title="Drag this object to the canvas">
                                Mobile Number
                            </div>
                            <div id="QuickTxtTwitter" class="QuickTextText" title="Drag this object to the canvas">
                                Twitter ID
                            </div>
                            <div id="QuickTxtFacebook" class="QuickTextText" title="Drag this object to the canvas">
                                Facebook ID
                            </div>
                            <div id="QuickTxtLinkedIn" class="QuickTextText" title="Drag this object to the canvas">
                                Linkedin ID 
                            </div>
                            <div id="QuickTxtOtherID" class="QuickTextText" title="Drag this object to the canvas">
                                Other ID
                            </div>
                            <div id="QuickTxtAllFields" class="QuickTextText QuickTextAllBtn" title="Drag all objects to the canvas">
                                Drag all fields at once
                            </div>
                        </div>
                    </div>
                    <div id="quickTextFormPanel" class="panelQuickTextForm propertyPanel">
                        <div class="closePanelButton" onclick="pcL36('hide','#quickTextFormPanel');">
                            <br />
                        </div>
                        <div class=" largeText panelQuickTexthead">
                            Q Text
                        </div>
                        <div class=" qSubHeading">
                            Save your information into your profile.
                        </div>
                        <div id="QuickTextItemContainer" class="QuickTextItemContainer">
                        </div>
                        <div class="panelQuickTextFormRow">
                            <div class="qLabel">
                                &nbsp;
                            </div>
                            <button id="BtnQuickTextSave" title="Apply quick text to this template">
                                Save</button>
                        </div>
                    </div>
                    <div id="DivAlignObjs" class="DivAlignObjs popUpaddTextPanel propertyPanel">
                        <div class="closePanelButton" onclick="pcL36('hide','#DivAlignObjs');">
                            <br />
                        </div>
                        <div class="DivTitleLbl">
                            <label class="lblGroupProperties titletxt">
                                Group Properties</label>
                        </div>
                        <div class="AlignObjsDiv">
                            <label class="largeText panelTextPropertyRow  ">
                                Align Objects</label>
                        </div>
                        <div>
                            <button id="BtnAlignObjLeft">
                            </button>
                            <button id="BtnAlignObjCenter">
                            </button>
                            <button id="BtnAlignObjRight">
                            </button>
                            <button id="BtnAlignObjTop">
                            </button>
                            <button id="BtnAlignObjMiddle">
                            </button>
                            <button id="BtnAlignObjBottom">
                            </button>
                        </div>
                        <div id="divRetailColorGroup" class="propertyPanelControlDiv">
                            <div class="ColorPallet" style="background-color: rgb(2, 3, 2); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(0,0,0,100,&quot;#020302&quot;,&quot;null&quot;);"></div>
                            <div class="ColorPallet" style="background-color: rgb(158, 40, 14); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(0,90,100,40,&quot;#9E280E&quot;,&quot;40&quot;);"></div>
                            <div class="ColorPallet" style="background-color: rgb(138, 113, 60); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(30,40,80,30,&quot;#8A713C&quot;,&quot;null&quot;);"></div>
                            <div class="ColorPallet" style="background-color: rgb(0, 114, 54); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(100,0,100,40,&quot;#007236&quot;,&quot;null&quot;);"></div>
                            <div class="ColorPallet" style="background-color: rgb(0, 125, 151); border-top-left-radius: 7px; border-top-right-radius: 7px; border-bottom-left-radius: 7px; border-bottom-right-radius: 7px;" onclick="f2(100,5,20,30,&quot;#007D97&quot;,&quot;null&quot;);"></div>
                            <button id="AddColorGroupRetail" class="" title="Colour picker">MORE</button>
                        </div>
                    </div>
                    <div id="divPresetEditor" class="divPresetEditor propertyPanel">
                        <div class="closePanelButton" onclick="pcL36('hide','#divPresetEditor');">
                            <br />
                        </div>
                        <div class="DivTitleLbl">
                            <label class="lblGroupProperties titletxt">
                                Presets Editor</label>
                        </div>
                        <div id="dd" class="propertyPanelControlDiv">
                            <label>Select Preset</label>
                            <select id="dropDownPresets" title="Select Preset to edit">
                                <option value="0">(select)</option>
                            </select>
                            <br />
                            <br />
                            <div class="presetEditorControls">
                                <label>Preset Title</label>
                                <input type="text" id="presetTitle" /><br />
                                <label>Icon</label>
                                <select id="presetLogo" title="Select Preset to edit">
                                    <option value="1" selected="selected">Landscape 1</option>
                                    <option value="2">Landscape 2</option>
                                    <option value="3">Landscape 3</option>
                                    <option value="4">Landscape 4</option>
                                    <option value="5">Landscape 5</option>
                                    <option value="6">Landscape 6</option>
                                    <option value="7">Landscape 7</option>
                                    <option value="13">Landscape 8</option>
                                    <option value="14">Landscape 9</option>
                                    <option value="15">Flyers Landscape 1</option>
                                    <option value="16">Flyers Landscape 2</option>
                                    <option value="17">Flyers Landscape 3</option>
                                    <option value="8">Portrait 1</option>
                                    <option value="9">Portrait 2</option>
                                    <option value="10">Portrait 3</option>
                                    <option value="11">Portrait 4</option>
                                    <option value="12">Portrait 5</option>
                                    <option value="18">Flyers Portrait 1</option>
                                    <option value="19">Flyers Portrait 2</option>
                                    <option value="20">Flyers Portrait 3</option>
                                </select><br />
                                <div class="imgPresetContainer" style="visibility: hidden;">
                                    <div class="ImgLbl" style="width: 66px; padding-top: 13px;">Icon preview </div>
                                    <img id="imgPreviewPreset" src="" /><br />
                                </div>
                                <div class="clearBoth"></div>
                                <div class="classUpdateBtns" style="display: none;">
                                    <button id="btnUpdatePreset" title="Update Preset">Update</button>
                                    <button id="btnDeletePreset" title="Delete Preset">Remove</button>
                                </div>
                            </div>
                            <button id="btnAddPreset" title="Add new Preset">Add New</button>

                        </div>
                    </div>
                    <div id="DivToolTip" class="DivToolTipStyle propertyPanel">
                        <div id="DivToolTipHeader">
                            <div id="DivToolTipLogo">
                            </div>
                            <div id="DivTootTipTitle">
                            </div>
                            <div class="closePanelButton" onclick="pcL36('hide','#DivToolTip');">
                            </div>
                        </div>
                        <div id="DivToolTipText">
                        </div>
                        <div id="DivNextToolTip">
                        </div>
                    </div>
                    <div id="DivPersonalizeTemplate" class="DivToolTipStyle propertyPanel">
                        <div id="DivPersonalizeHeader">
                            <div id="DivPersonalizeLogo">
                            </div>
                            <div id="DivPersonalizeTitle">
                                Personalize this template
                            </div>
                            <div class="closePanelButton" onclick="pcL36('hide','#DivPersonalizeTemplate');">
                            </div>
                        </div>
                        <div id="DivPersonalizeText">
                            Click on any object to start editing
                        </div>
                        <div id="DivNextPersonalize">
                            <img src="assets/iconLEft.png" width="50px" />
                        </div>
                    </div>
                    <div id="DivControlPanelDraggable">
                        <div id="DivControlPanel1" class="divControlPanel DivControlPanel1 ">
                            <div>
                                <div class="divMenuRow" style="margin-left: 0px;">
                                    <div>
                                        <button id="btnResetTemplate" title="Reset Templates" class="uiButtonTopMenu">
                                        </button>
                                        <button id="BtnUndo" title="Undo an object to last state" class="uiButtonTopMenu" >
                                        </button>
                                        <button id="BtnRedo" title="Redo an object to last state" class="uiButtonTopMenu" >
                                        </button>
                                        <button id="BtnCopyObj" title="Copy" class="uiButtonTopMenu">
                                        </button>
                                        <button id="BtnPasteObj" title="Paste" class="uiButtonTopMenu">
                                        </button>
                                        <button id="BtnQuickTextForm" class="rect uiButtonTopMenu" title="Show Quick Text Panel">
                                        </button>
                                    </div>
                                    <%--  <div style="margin-top: 5px;">
                                        <span style="margin-left: 10px;">Reload</span> <span style="margin-left: 16px;" id="spanUndo">Undo </span><span style="margin-left: 23px; display: none;" id="spanRedo">Redo</span> <span style="margin-left: 22px;"
                                            class="lblCopyBtn">Copy</span> <span style="margin-left: 21px;" class="lblPasteBtn">Paste</span><span id="lblQuickTxtBtn" style="margin-left: 15px;">Quick Text </span>
                                    </div>--%>
                                </div>

                                <div class="divMenuRow RetailMenuDiv">
                                    <div>
                                        <button id="BtnBCPresets" class="BtnBCPresets uiButtonTopMenu" title="Text Presets">
                                        </button>
                                        <button id="btnBkImgPanel" class="rect uiButtonTopMenu" title="Show Background images Panel">
                                        </button>
                                        <button id="btnImgPanel" class="rect uiButtonTopMenu" title="Show Images Panel">
                                        </button>
                                        <button id="btnShowIcons" class="rect uiButtonTopMenu" title="Show Icons/Shapes Panel">
                                        </button>
                                        <button id="btnShowLogo" class="rect uiButtonTopMenu" title="Show Logo Panel">
                                        </button>
                                        <button id="btnNewTxtPanel" class="rect uiButtonTopMenu" title="Show Text Panel">
                                        </button>
                                        <button id="divLayersPanelCaller" class="circle uiButtonTopMenu" title="Show/Hide layers panel">
                                        </button>
                                    </div>
                                    <%--    <div style="margin-top: 5px;">
                                        <span class="lblLayouts" style="margin-left: 21px; display: none;">Layouts</span>  <span style="margin-left: 15px;" class="lblAddImgBtn">Backgrounds</span><span style="margin-left: 16px;" class="lblAddImgBtn">Pictures</span>
                                        <span id="spanShowIcons" style="margin-left: 14px;">Icons / Shapes</span><span id="spanLogos" style="margin-left: 14px;">Logos</span>
                                        <span style="margin-left: 16px;"
                                            class="lblAddImgBtn">Add Text</span> <span style="margin-left: 11px;" class="lblLayersBtn">Show Layers</span>
                                    </div>--%>
                                </div>

                                <div class="divMenuRow" id="divZoomContainer">
                                    <button id="BtnZoomIn" title="Canvas zoom in" class="btnZoomCanvas">
                                        +
                                    </button>
                                    <div style="margin-left: 5px; margin-top: 5px; margin-bottom: 2px; font-size: 9px;" class="zoomToolBar">Zoom 100%</div>
                                    <button id="BtnZoomOut" title="Canvas zoom out" class="btnZoomCanvas">
                                        -
                                    </button>
                                </div>
                                <div class="divMenuRow" style="padding-top: 0px; background-color: transparent; padding-right: 0px; font-size: 8px;">
                                    <button id="btnShowTipsBC" class="btnShowTips">Show Tips</button>
                                    <br />
                                    <button id="BtnGuidesBC" class="btnShowTips">Hide Bleed and Trim lines</button>
                                    <br />
                                    <button id="BtnOrignalZoom" title="Reset zoom to original state" class="btnShowTips">Reset zoom level</button>
                                </div>
                            </div>
                        </div>
                        <div id="divBCMenuPresets" class="divBCMenuPresets divBCMenuPresets">
                            <div class="closePanelButton" onclick="pcL36('hide','#divBCMenuPresets');">
                                <br />
                            </div>
                            <div class="DivTitleLbl">
                                <label class="largeText panelTextPropertyRow  titletxt">
                                    Layouts</label>
                            </div>
                            <div class="divLayoutBtnContainer"></div>
                        </div>
                    </div>
                    <div id="textPropertPanel" class="propertyPanel">
                        <div class="textPropertyPanel1">
                            <div class="closePanelButton" onclick="pcL36('hide','#addImage, #textPropertPanel')">
                                <br />
                            </div>
                            <%--  <div class="DivTitleLbl">
								<label class="largeText panelTextPropertyRow  titletxt">
									Text Properties</label>
							</div>--%>
                            <%--<div class="propertyPanelControlDiv">
                                <textarea id="txtAreaUpdateTxtPropPanel" class="popupUpdateTxtArea" style="height: 55px;width: 297px;"></textarea>
                            </div>--%>
                            <div class="propertyPanelControlDiv">
                                <select id="BtnSelectFonts" title="Select Font" class="styledBorder">
                                    <option value="(select)">(select)</option>
                                </select>
                                <button id="BtnBoldTxt" class="" title="Bold">
                                </button>
                                <button id="BtnItalicTxt" title="Italic">
                                </button>
                                <button id="BtnChngeClr" class="BtnColorPicker" title="Colour picker">
                                </button>
                                <input id="BtnFontSize" style="" />

                            </div>
                            <div class="propertyPanelControlDiv">
                                <button id="BtnJustifyTxt1" title="Justify text left">
                                </button>
                                <button id="BtnJustifyTxt2" title="Justify text center">
                                </button>
                                <button id="BtnJustifyTxt3" title="Justify text right">
                                </button>
                                <img src="assets/lineHeight.png" class="iconsPropPanel imgLineHeight" />
                                <input name="txtLineHeight" id="txtLineHeight"  class="inputCharSpacing zindex100" />
                                <img src="assets/imgCharSpace.png" class="iconsPropPanel imgCharHeight" />
                                <input name="inputcharSpacing" id="inputcharSpacing" class="inputCharSpacing zindex100" />
                            </div>
                            <div id="AddColorTxtRetailDiv" class="propertyPanelControlDiv">
                                <div class="ColorPallet" style="background-color: #ED1C24" onclick="f2(0,100,100,0,&quot;#ED1C24&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #F5821F" onclick="f2(0,60,100,0,&quot;#F5821F&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #FFF200" onclick="f2(0,0,100,0,&quot;#FFF200&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #1E9860" onclick="f2(75,0,75,20,&quot;#1E9860&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #00AEEF" onclick="f2(100,0,0,0,&quot;#00AEEF&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #4C4C4E" onclick="f2(0,0,0,85,&quot;#4C4C4E&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #FFFFFF" onclick="f2(0,0,0,0,&quot;#FFFFFF&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #A7A9AC" onclick="f2(0,0,0,40,&quot;#A7A9AC&quot;,&quot;null&quot;);"></div>
                                <button id="AddColorTxtRetail" class="" title="Colour picker">
                                    MORE
                                </button>
                            </div>
                            <div class="propertyPanelControlDiv">
                                <label class="largeText  lbtTxtArea" for="EditTXtArea">
                                    Double click to edit text</label>
                            </div>
                        </div>
                        <div class="textPropertyPanel2">
                            <div class="propertyPanelControlDiv">
                                <span class="largeText RotateLbl3" id="lblArrangeOrderTxt">Layer</span> <span class="marginLeft15 marginRight5 largeText"
                                    style="margin-left: 47px;">Width </span>
                                <input name="inputObjectWidthTxt" id="inputObjectWidthTxt"  class="width28 zindex100" />
                                <span class="marginLeft15 marginRight7 largeText">Height</span>
                                <input name="inputObjectHeightTxt" id="inputObjectHeightTxt" class="width28 zindex100" />
                            </div>
                            <div class="propertyPanelControlDiv largeText" style="padding-bottom: 0px;">
                                <span style="margin-left: 4px;" id="lblTxtArrange4">To front </span><span style="margin-left: 23px;">+
                                </span><span style="margin-left: 36px;">- </span><span style="margin-left: 22px;" id="lblTxtArrange1">To
                                    back </span>
                            </div>
                            <div class="propertyPanelControlDiv">
                                <button id="BtnTxtarrangeOrder1" title="Bring text to front">
                                </button>
                                <button id="BtnTxtarrangeOrder2" title="Bring text a step front">
                                </button>
                                <button id="BtnTxtarrangeOrder3" title="Send text a step back">
                                </button>
                                <button id="BtnTxtarrangeOrder4" title="Send text to back">
                                </button>
                                <button id="BtnRotateTxtLft" title="Rotate text left">
                                </button>
                                <button id="BtnRotateTxtRight" title="Rotate text right">
                                </button>
                            </div>
                            <div class="DivBlockWithNoHeight vertLine" style="width: 313px;">
                            </div>
                            <div class="propertyPanelControlDiv">
                                <div class="divTxtAlignmentControls">
                                    <button id="BtnTxtCanvasAlignLeft" class="btnCj1" title="">
                                    </button>
                                    <button id="BtnTxtCanvasAlignCenter" class="btnCj2" title="">
                                    </button>
                                    <button id="BtnTxtCanvasAlignMiddle" class="btnCj5" title="">
                                    </button>
                                    <button id="BtnTxtCanvasAlignRight" class="btnCj3" title="">
                                    </button>

                                    <%--   <button id="btnMoveObjLeftTxt">
                                    </button>
                                    <button id="btnMoveObjUpTxt">
                                    </button>
                                    <button id="btnMoveObjDownTxt">
                                    </button>
                                    <button id="btnMoveObjRightTxt">
                                    </button>--%>
                                    <div class="checkboxRowsTxtLock">
                                        <input type="checkbox" id="BtnLockTxtPosition" />
                                        <label for="LockPosition2" id="lblLockPositionTxt">
                                            Lock</label>
                                    </div>
                                </div>
                                <div class="divTxtPositioningCotnrols">
                                    <span class="marginLeft15 marginRight5" style="margin-right: 25px;">X </span>
                                    <input name="inputPositionXTxt" id="inputPositionXTxt" class="width28 zindex100" />
                                    <br />
                                    <br />
                                    <span class="marginLeft15 marginRight5" style="margin-right: 25px;">Y </span>
                                    <input name="inputPositionYTxt" id="inputPositionYTxt" class="width28 zindex100" />

                                </div>
                                <div class="clearBoth">
                                </div>
                            </div>
                            <div class="DivBlockWithNoHeight vertLine" style="width: 313px;">
                            </div>
                            <div class="propertyPanelControlDiv">
                                <div class="checkboxRowsTxt ">
                                    <input type="checkbox" id="BtnPrintObj" />
                                    <label for="PrintObjRadioBtn2" id="lblDoNotPrintTxt">
                                        Show on Proof but DO NOT print on PDF</label>
                                </div>
                                <div class="checkboxRowsTxt">
                                    <input type="checkbox" id="BtnAllowOnlyTxtChange" />
                                    <label for="LockEditing" id="lblAllowOnlyTxtChng">
                                        Lock Formatting</label>
                                </div>
                                <div class="checkboxRowsTxt">
                                    <input type="checkbox" id="BtnLockEditing" />
                                    <label for="BtnLockEditing" id="lblLockEditingTxt">
                                        Lock Editing</label>
                                </div>
                                <div class="checkboxRowsTxt">
                                    <input type="checkbox" id="chkboxAutoShrink" />
                                    <label for="chkboxAutoShrink" id="lblAutoShrink">
                                        Auto shrink text</label>
                                </div>
                                  <div class="checkboxRowsTxt">
                                    <input type="checkbox" id="chkboxOverlayTxt" />
                                    <label for="chkboxOverlayTxt" id="lblchkboxOverlayTxt">
                                        Is overlay Object</label>
                                </div>
                                <div class="divDelTxtObj">
                                    <button id="BtnUploadFont" title="Click to upload your own font">
                                        Upload Font</button>
                                    <button id="BtnDeleteTxtObj" title="Delete text">
                                        Remove
                                    </button>
                                </div>
                                <div class="clearBoth">
                                </div>
                            </div>
                        </div>
                        <%-- <div class="DivBlockWithNoHeight marginTop6">
								<textarea id="EditTXtArea" rows="4" cols="30" style="overflow: auto"></textarea>
								<button id="BtnSearchTxt">
								</button>
							</div>
							<div class="marginTop5 DivBlockWithNoHeight width224 ">
								<button id="BtnUpdateText" title="Update text">
									Update</button>
							</div>
						<select id="txtLineHeight" title="Select Font Size">--%>
                    </div>
                    <div id="ImagePropertyPanel" class="propertyPanel">
                        <div class="textPropertyPanel1">
                            <div class="closeImgPanelButton" onclick="pcL36('hide','#addImage , #ImagePropertyPanel');">
                                <br />
                            </div>
                            <div class="propertyPanelControlDiv">
                                <p class="largeText RotateLbl3" id="P2">
                                    Scale
                                </p>
                                <button id="BtnImgScaleIN" title="Scale in">
                                </button>
                                <button id="BtnImgScaleOut" title="Scale out">
                                </button>
                            </div>
                            <div class="propertyPanelControlDiv">
                                <p class="largeText RotateLbl3" id="LbImgCrop">
                                    Crop
                                </p>
                                <button id="BtnCropImg" title="Crop object">
                                </button>
                                <button id="BtnCropImg2" title="Crop object">
                                </button>
                                <p id="AddColorShapeLbl" class="largeText RotateLbl3">
                                    Colour
                                </p>
                                <button id="AddColorShape" class="" title="Colour picker">
                                </button>

                            </div>
                            <div id="AddColorShapeRetailDiv" class="propertyPanelControlDiv">
                                <div class="ColorPallet" style="background-color: #ED1C24" onclick="f2(0,100,100,0,&quot;#ED1C24&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #F5821F" onclick="f2(0,60,100,0,&quot;#F5821F&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #FFF200" onclick="f2(0,0,100,0,&quot;#FFF200&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #1E9860" onclick="f2(75,0,75,20,&quot;#1E9860&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #00AEEF" onclick="f2(100,0,0,0,&quot;#00AEEF&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #4C4C4E" onclick="f2(0,0,0,85,&quot;#4C4C4E&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #FFFFFF" onclick="f2(0,0,0,0,&quot;#FFFFFF&quot;,&quot;null&quot;);"></div>
                                <div class="ColorPallet" style="background-color: #A7A9AC" onclick="f2(0,0,0,40,&quot;#A7A9AC&quot;,&quot;null&quot;);"></div>
                                <button id="AddColorShapeRetail" class="" title="Colour picker">
                                    MORE
                                </button>
                            </div>
                            <div class="propertyPanelControlDiv">
                                <br />
                            </div>
                        </div>
                        <div class="textPropertyPanel2">
                            <div class="propertyPanelControlDiv">
                                <span class="largeText RotateLbl3" id="spanLblTransparency">Opacity</span>
                                <input name="inputObjectAlpha" id="inputObjectAlpha" class="width28 zindex100" />
                                <span class="marginLeft15 marginRight5 largeText"
                                    style="margin-left: 21px;">W </span>
                                <input name="inputObjectWidth" id="inputObjectWidth" class="width28 zindex100" />
                                <span class="marginLeft15 marginRight7 largeText">H</span>
                                <input name="inputObjectHeight" id="inputObjectHeight" class="width28 zindex100" />
                            </div>
                            <div class="propertyPanelControlDiv largeText" style="padding-bottom: 0px;">
                                <span style="margin-left: 4px;">To front </span><span style="margin-left: 23px;">+
                                </span><span style="margin-left: 36px;">- </span><span style="margin-left: 22px;">To
                                    back </span>
                            </div>
                            <div class="propertyPanelControlDiv">
                                <button id="BtnImageArrangeOrdr1" title="Bring object to front">
                                </button>
                                <button id="BtnImageArrangeOrdr2" title="Bring object a step front">
                                </button>
                                <button id="BtnImageArrangeOrdr3" title="Send object a step back">
                                </button>
                                <button id="BtnImageArrangeOrdr4" title="Send object to back">
                                </button>
                                <button id="BtnImgRotateLeft" title="Rotate object left">
                                </button>
                                <button id="BtnImgRotateRight" title="Rotate object right">
                                </button>
                            </div>
                            <div class="DivBlockWithNoHeight vertLine" style="width: 313px;">
                            </div>
                            <div class="propertyPanelControlDiv">
                                <div class="divTxtAlignmentControls">
                                    <button id="btnImgCanvasAlignLeft" class="btnCj1" title="">
                                    </button>
                                    <button id="BtnImgCanvasAlignCenter" class="btnCj2" title="">
                                    </button>
                                    <button id="BtnImgCanvasAlignMiddle" class="btnCj5" title="">
                                    </button>
                                    <button id="BtnImgCanvasAlignRight" class="btnCj3" title="">
                                    </button>
                                    <div class="checkboxRowsTxtLock">
                                        <input type="checkbox" id="LockPositionImg" />
                                        <label for="LockPosition" class="largeText" id="lblLockPositionImg">
                                            Lock</label>
                                    </div>
                                    <%--        <button id="btnMoveObjLeft">
                                    </button>
                                    <button id="btnMoveObjUp">
                                    </button>
                                    <button id="btnMoveObjDown">
                                    </button>
                                    <button id="btnMoveObjRight">
                                    </button>--%>
                                </div>
                                <div class="divTxtPositioningCotnrols">
                                    <span class="marginLeft15 marginRight5" style="margin-right: 25px;">X </span>
                                    <input name="inputPositionX" id="inputPositionX" class="width28 zindex100" />
                                    <br />
                                    <br />
                                    <span class="marginLeft15 marginRight5" style="margin-right: 25px;">Y </span>
                                    <input name="inputPositionY" id="inputPositionY" class="width28 zindex100" />

                                </div>
                                <div class="clearBoth">
                                </div>
                            </div>
                            <div class="DivBlockWithNoHeight vertLine" style="width: 313px;">
                            </div>
                            <div class="propertyPanelControlDiv">
                                <div class="checkboxRowsTxt ">
                                    <input type="checkbox" id="BtnPrintImage" />
                                    <label for="BtnPrintImage " id="lblDoNotPrintImg">
                                        Show on Proof but DO NOT print on PDF
                                    </label>
                                </div>
                                <div class="checkboxRowsTxt">
                                    <input type="checkbox" id="LockImgProperties" />
                                    <label for="LockImgProperties" id="lblLockImgProperties">
                                        Lock Formatting</label>
                                </div>
                                <div class="checkboxRowsTxt">
                                    <input type="checkbox" id="chkboxOverlayImg" />
                                    <label for="chkboxOverlayImg" id="LblchkboxOverlayImg" style="font-size:14px;">
                                        Is overlay Object</label>
                                </div>
                                <div class="divDelTxtObj" style="float: right; margin-right: 9px;">
                                    <button id="btnDeleteImage" title="Delete Object">
                                        Remove
                                    </button>
                                </div>
                                 
                                <div class="clearBoth">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="DivLayersPanel" class="popUpaddTextPanel addimgPanel propertyPanel">
                        <div class="closePanelButton" title="Close this panel" onclick="pcL36('hide','#DivLayersPanel');">
                            <br />
                        </div>
                        <p class="titletxt">
                            Layers
                        </p>
                        <div id="LayerObjectsContainer" style="display: block">
                        </div>
                    </div>
                    <div id="divLayersPanelRetail" class="popupEditTemplateObjs addimgPanel propertyPanel" style="display: none;">

                        <div>
                            <p class="titletxt  layerPanelTitleRetail">
                                Edit template objects
                            </p>
                            <button id="btnNewTxtPanelRetail" class="rect uiButtonTopMenu" title="Show Text Panel">
                            </button>
                             <button id="btnImgPanelRetail" class="rect uiButtonTopMenu" title="Show Images Panel">
                                        </button>
                             
                                <menu class="toolbarMenuPictures toolbarMoreGroup1 toolbarReferencePoint retailPropPanelsSubMenu ">
                                    <li id="btnImgPanelRetailStore" class="elementToolbarForward enabled anchorImages" style="margin-top: 0px;">Images</li>
                                    <li id="btnShowIconsRetail" class="elementToolbarForward enabled anchorShapes" style="margin-top: 0px;">Shapes / Icons</li>
                                    <li id="btnShowLogoRetail" class="elementToolbarForward enabled anchorLogos" style="margin-top: 0px;">Logos</li>
                         
                                </menu>
  
                        </div>
                        <div id="LayerObjectsContainerRetail" style="display: block">
                        </div>
                    </div>
                    <div class="popupUpdateTxt propertyPanel" id="divPopupUpdateTxt">
                        <textarea id="txtAreaUpdateTxt" class="popupUpdateTxtArea"></textarea>
                    </div>
                    <div class="divVariableContainer propertyPanel" id="divVariableContainer">
                        <div class="closePanelButton" title="Close this panel" onclick="pcL36('hide','#divVariableContainer');">
                            <br />
                        </div>
                        <p class="titletxt" id="titleVarList" style="color: White; text-align: left;">
                            Company/User Data Variables
                        </p>
                        <div id="divVarList" class="divVarList" style="">
                            <div class="titletxt">
                                Company
                            </div>
                            <div class="divVar" title="Company Account number">{{AccountNumber}}</div>
                            <div class="divVar" title="Company name">{{Name}}</div>
                            <div class="divVar" title="Company URL">{{URL}}</div>
                            <div class="divVar" title="Company ISBN">{{ISBN}}</div>
                            <div class="divVar" title="Company VAT registration number">{{VATRegNumber}}</div>
                            <div class="divVar" title="Company Type">{{FlagID}}</div>
                            <div class="divVar" title="Company Web access code">{{WebAccessCode}}</div>
                            <%--<div class="divVar" title="Company logo">{{Image}}</div>--%>
                            <div class="divVar" title="Company twitter URL">{{TwitterURL}}</div>
                            <div class="divVar" title="Company facebook URL">{{FacebookURL}}</div>
                            <div class="divVar" title="Company linkedin URL">{{LinkedinURL}}</div>
                            <div class="titletxt">
                                Contact Person
                            </div>
                            <div class="divVar" title="Contact first name">{{FirstName}}</div>
                            <div class="divVar" title="Contact middle name">{{MiddleName}}</div>
                            <div class="divVar" title="Contact last name">{{LastName}}</div>
                            <div class="divVar" title="Contact title">{{Title}}</div>
                            <div class="divVar" title="Contact home tel 1">{{HomeTel1}}</div>
                            <div class="divVar" title="Contact home tel 2">{{HomeTel2}}</div>
                            <div class="divVar" title="Contact home tel 1 extension">{{HomeExtension1}}</div>
                            <div class="divVar" title="Contact home tel 2 extension">{{HomeExtension2}}</div>
                            <div class="divVar" title="Contact mobile">{{Mobile}}</div>
                            <div class="divVar" title="Contact pager">{{Pager}}</div>
                            <div class="divVar" title="Contact email">{{Email}}</div>
                            <div class="divVar" title="Contact Fax">{{FAX}}</div>
                            <div class="divVar" title="Contact Job title">{{JobTitle}}</div>
                            <div class="divVar" title="Contact department">{{Department}}</div>
                            <div class="divVar" title="Contact Skype id">{{contact_SkypeID}}</div>
                            <div class="divVar" title="Contact Linkedin URL">{{contact_LinkedinURL}}</div>
                            <div class="divVar" title="Contact facebook URL">{{contact_FacebookURL}}</div>
                            <div class="divVar" title="Contact twitter URL">{{contact_TwitterURL}}</div>
                            <div class="divVar" title="Contact website">{{contact_URL}}</div>

                            <div class="divVar" title="Contact website">{{POBoxAddress}}</div>
                            <div class="divVar" title="Contact website">{{CorporateUnit}}</div>
                            <div class="divVar" title="Contact website">{{OfficeTradingName}}</div>
                            <div class="divVar" title="Contact website">{{ContractorName}}</div>
                            <div class="divVar" title="Contact website">{{BPayCRN}}</div>
                            <div class="divVar" title="Contact website">{{ABN}}</div>
                            <div class="divVar" title="Contact website">{{ACN}}</div>
                            <div class="divVar" title="Contact website">{{AdditionalField1}}</div>
                            <div class="divVar" title="Contact website">{{AdditionalField2}}</div>
                            <div class="divVar" title="Contact website">{{AdditionalField3}}</div>
                            <div class="divVar" title="Contact website">{{AdditionalField4}}</div>
                            <div class="divVar" title="Contact website">{{AdditionalField5}}</div>
            <%--                <div class="titletxt">
                                Contact Person Address
                            </div>
                            <div class="divVar" title="Contact address title">{{contact_AddressName}</div>
                            <div class="divVar" title="Contact Address 1">{{contact_Address1}}</div>
                            <div class="divVar" title="Contact Address 2">{{contact_Address2}}</div>
                            <div class="divVar" title="Contact Address 3">{{contact_Address3}}</div>
                            <div class="divVar" title="Contact city">{{contact_City}}</div>
                            <div class="divVar" title="Contact state">{{contact_State}}</div>
                            <div class="divVar" title="Contact country">{{contact_Country}}</div>
                            <div class="divVar" title="Contact postcode">{{contact_PostCode}}</div>
                            <div class="divVar" title="Contact fax">{{contact_Fax}}</div>
                            <div class="divVar" title="Contact email">{{contact_Email}}</div>
                            <div class="divVar" title="Contact website">{{contact_URL}}</div>
                            <div class="divVar" title="Contact tel 1">{{contact_Tel1}}</div>
                            <div class="divVar" title="Contact tel 2">{{contact_Tel2}}</div>
                            <div class="divVar" title="Contact tel ext 1">{{contact_Extension1}}</div>
                            <div class="divVar" title="Contact FAO">{{contact_FAO}}</div>
                            <div class="divVar" title="Contact tel ext 2">{{contact_Extension2}}</div>
                            <div class="divVar" title="Contact address reference no.">{{contact_Reference}}</div>--%>
                            <div class="titletxt">
                                Shipping Address
                            </div>
                            <div class="divVar" title="Shipping Address name">{{DefaultShipping_AddressName}}</div>
                            <div class="divVar" title=" Shipping Address 1">{{DefaultShipping_Address1}}</div>
                            <div class="divVar" title=" Shipping Address 2">{{DefaultShipping_Address2}}</div>
                            <div class="divVar" title=" Shipping Address 3">{{DefaultShipping_Address3}}</div>
                            <div class="divVar" title=" Shipping Address City">{{DefaultShipping_City}}</div>
                            <div class="divVar" title=" Shipping Address State">{{DefaultShipping_State}}</div>
                            <div class="divVar" title=" Shipping Address Country">{{DefaultShipping_Country}}</div>
                            <div class="divVar" title=" Shipping Address PostCode">{{DefaultShipping_PostCode}}</div>
                            <div class="divVar" title=" Shipping Address fax">{{DefaultShipping_Fax}}</div>
                            <div class="divVar" title=" Shipping Address email">{{DefaultShipping_Email}}</div>
                            <div class="divVar" title=" Shipping Address URL">{{DefaultShipping_URL}}</div>
                            <div class="divVar" title=" Shipping Address Tel 1">{{DefaultShipping_Tel1}}</div>
                            <div class="divVar" title=" Shipping Address Tel 2">{{DefaultShipping_Tel2}}</div>
                            <div class="divVar" title=" Shipping Address Tel ext 1">{{DefaultShipping_Extension1}}</div>
                            <div class="divVar" title=" Shipping Address Tel ext 2">{{DefaultShipping_Extension2}}</div>
                            <div class="divVar" title=" Shipping Address Refernce">{{DefaultShipping_Reference}}</div>
                            <div class="divVar" title=" Shipping Address FAO">{{DefaultShipping_FAO}}</div>
                            <div class="titletxt">
                                Billing Address
                            </div>
                            <div class="divVar" title="Billing Address Name">{{DefaultBilling_AddressName}}</div>
                            <div class="divVar" title="Billing Address 1">{{DefaultBilling_Address1}}</div>
                            <div class="divVar" title="Billing Address 2">{{DefaultBilling_Address2}}</div>
                            <div class="divVar" title="Billing Address 3">{{DefaultBilling_Address3}}</div>
                            <div class="divVar" title="Billing Address city">{{DefaultBilling_City}}</div>
                            <div class="divVar" title="Billing Address state">{{DefaultBilling_State}}</div>
                            <div class="divVar" title="Billing Address country">{{DefaultBilling_Country}}</div>
                            <div class="divVar" title="Billing Address postcode">{{DefaultBilling_PostCode}}</div>
                            <div class="divVar" title="Billing Address fax">{{DefaultBilling_Fax}}</div>
                            <div class="divVar" title="Billing Address email">{{DefaultBilling_Email}}</div>
                            <div class="divVar" title="Billing Address url">{{DefaultBilling_URL}}</div>
                            <div class="divVar" title="Billing Address tel 1">{{DefaultBilling_Tel1}}</div>
                            <div class="divVar" title="Billing Address tel 2">{{DefaultBilling_Tel2}}</div>
                            <div class="divVar" title="Billing Address tel ext 1">{{DefaultBilling_Extension1}}</div>
                            <div class="divVar" title="Billing Address  tel ext 2">{{DefaultBilling_Extension2}}</div>
                            <div class="divVar" title="Billing Address Reference no">{{DefaultBilling_Reference}}</div>
                            <div class="divVar" title="Billing Address FAO">{{DefaultBilling_FAO}}</div>
                        </div>
                    </div>
                    <div id="divImageDAM" class=" propertyPanel">
                        <div class="closePanelButton" title="Close this panel" onclick="pcL36('hide','#divImageDAM');">
                            <br />
                        </div>

                        <div id="">
                            <div id="imgBtnContainer">
                                <div class="divMenuRow" style="margin-left: 0px; height: 60px; padding-top: 0px;">
                                    <div>
                                        <button id="btnChngeCanvasColor" class="BtnColorPickerBC  bkPanel" title="Change Background Colour">
                                            Change Background Colour
                                        </button>
                                        <button id="btnAddRectangle" class="rect imgPanel2" title="Drag rectangle to Canvas">
                                        </button>
                                        <button id="btnAddCircle" class="circle imgPanel2" title="Drag circle to Canvas">
                                        </button>

                                    </div>
                                    <div style="margin-top: 5px;">
                                        <span style="margin-left: 15px; width: 130px; display: none;" class="spanImgPlaceHolder pLabel ">Change Background Colour</span> <span style="margin-left: 15px;" id="span2" class="imgPanel2">Rectangle </span><span style="margin-left: 33px;" id="span3" class="imgPanel2">Circle</span>
                                        <div class="clearBoth"></div>
                                    </div>
                                </div>

                                <div class="imgBtnRow floatLeft" style="width: 146px;">
                                    <%-- <span>Upload an image or background</span>--%>
                                    <form id="form1" runat="server" class="imageUploadForm">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                                        </asp:ScriptManager>
                                        <div id="uploader">
                                            <p class="textWhite">
                                                You browser doesn't have Flash, Silverlight or HTML5 support.
                                            </p>
                                        </div>
                                        <div id="uploaderButtonDiv" style="display: none;">
                                            <a id="uploadImgFileBtn1" href="#" onclick="SubmitItem(this)">Click to upload</a>
                                        </div>
                                    </form>
                                    <div class="clearBoth"></div>
                                    <div class="RsizeDiv">Recommended 300 dpi 
                                        <br />
                                        CMYK JPeg or PDF.</div>
                                </div>

                                <div class="clearBoth"></div>
                                <div class=" floatLeft bkPanel" style="height: 60px;">
                                    <button id="clearBackground" class="bkPanel">Clear Background</button>
                                </div>
                                <div class="clearBoth"></div>
                                <div class="divMenuRow placeHolderControls" style="margin-left: 0px; height:auto; padding-top: 0px;" id="divCorpPlaceHolders">
                                    <div>
                                        <button id="btnAddImagePlaceHolder" class="rect imgPanel" title="Add Image Place Holder to Canvas">
                                        </button>
                                        <button id="btnCompanyPlaceHolder" class="rect imgPanel" title="Add Image Place Holder to Canvas">
                                        </button>
                                        <button id="btnContactPersonPlaceHolder" class="rect imgPanel" title="Add Image Place Holder to Canvas">
                                        </button>
                                    </div>
                                    <div style="margin-top: 5px;">
                                        <p style="margin-left: 12px;" class="spanImgPlaceHolder pLabel imgPanel">
                                            Image
                                            <br />
                                            Placeholder
                                        </p>
                                        <p style="margin-left: 21px;" id="span4" class="spanImgPlaceHolder pLabel imgPanel">
                                            Company<br />
                                            Logo
                                        </p>
                                        <p style="margin-left: 33px;" id="span5" class="spanImgPlaceHolder pLabel imgPanel">
                                            Your Profile
                                            <br />
                                            Image
                                        </p>
                                        <div class="clearBoth"></div>
                                    </div>
                                    <div class="propertyVarContainer" >
                                        <p style="margin-left: 12px; margin-top:10px;font-size: 13px;margin-bottom: 6px;" id="P4" class="spanImgPlaceHolder pLabel imgPanel">
                                           Real Estate variables
                                        </p>
                                        <div class="clearBoth"></div>
                                    </div>
                                </div>
                                <div class="clearBoth"></div>
                            </div>
                            <div id="ImgCarouselDiv">
                                <ul style="border-color: transparent;">
                                    <li id="btnFreeImg"><a href="#divGlobalImages">Free Images</a></li>
                                    <li id="btnTempImg"><a href="#divTemplateImages">Template Images</a></li>
                                    <li id="btnMyImgDam"><a href="#divPersonalImages">My Images</a></li>
                                    <li id="btnDivIllustrations"><a href="#divIllustrations">Illustrations</a></li>
                                    <li id="btnDivFrames"><a href="#divFrames">Frames</a></li>
                                    <li id="btnDivBanners"><a href="#divBanners">Banners</a></li>
                                    <li id="btnRealStateImages"><a href="#divRSImages">Real State Images</a></li>
                                </ul>
                                <div id="divGlobalImages" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchGImg" />
                                        <span class="imCountdivGlobImgContainer imTxtContainer"></span><%--<button onclick="k22()">GO</button>--%>
                                    </div>
                                    <div class="divGlobImgContainer DamImgContainer"></div>
                                    <a class="btnImgNextPg btndivGlobImgContainer" onclick="k21()">Load More Images</a>
                                </div>
                                <div id="divTemplateImages" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchTImg" />
                                        <span class="imCountdivTempImgContainer imTxtContainer"></span><%--<button onclick="k19()">GO</button>--%>
                                    </div>
                                    <div class="divTempImgContainer DamImgContainer"></div>
                                    <a class="btnImgNextPg btndivTempImgContainer" onclick="k17()">Load More Images</a>
                                </div>
                                <div id="divPersonalImages" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <%--
                                        <button  onclick="k23()">Previous</button>--%>
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchPImg" />
                                        <span class="imCountdivPersImgContainer imTxtContainer"></span><%--<button  onclick="k25()">GO</button>--%>
                                    </div>
                                    <div class="divPersImgContainer DamImgContainer"></div>
                                    <a class="btnImgNextPg btndivPersImgContainer" onclick="k24()">Load More Images</a>
                                </div>
                                <div id="divIllustrations" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchIllustrations" />
                                        <span class="imCountdivIllustrationContainer imTxtContainer"></span><%--<button  onclick="k25()">GO</button>--%>
                                    </div>
                                    <div class="divIllustrationContainer DamImgContainer"></div>
                                    <a class="btnImgNextPg btndivIllustrationContainer" onclick="k24ilus()">Load More Images</a>
                                </div>
                                <div id="divFrames" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchFrames" />
                                        <span class="imCountdivFramesContainer imTxtContainer"></span><%--<button  onclick="k25()">GO</button>--%>
                                    </div>
                                    <div class="divFramesContainer DamImgContainer"></div>
                                    <a class="btnImgNextPg btndivFramesContainer" onclick="k24frames()">Load More Images</a>
                                </div>
                                <div id="divBanners" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchBanners" />
                                        <span class="imCountdivBannersContainer imTxtContainer"></span><%--<button  onclick="k25()">GO</button>--%>
                                    </div>
                                    <div class="divBannersContainer DamImgContainer"></div>
                                    <a class="btnImgNextPg btndivBannersContainer" onclick="k24banners()">Load More Images</a>
                                </div>
                                <div id="divRSImages" class="ImgsTab">
                                    <div class="divRSImagesContainer DamImgContainer">

                                    </div>
                                    
                                </div>
                            </div>
                            <div id="BkImgContainer">
                                <ul style="border-color: transparent;">
                                    <li id="btnFreeBkg"><a href="#divGlobalBackg">Backgrounds</a></li>
                                    <li id="btnTempBkg"><a href="#divTemplateBkg">Template Backgrounds</a></li>
                                    <li id="btnMyBkg"><a href="#divPersonalBkg">My Backgrounds</a></li>
                                </ul>
                                <div id="divGlobalBackg" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchGbkg" />
                                        <span class="imCountdivGlobBkImgContainer imTxtContainer"></span>
                                    </div>
                                    <div class="divGlobBkImgContainer DamImgContainer">
                                    </div>
                                    <a class="btndivGlobBkImgContainer btnImgNextPg" onclick="k21Bk()">Load More Images</a>
                                </div>
                                <div id="divTemplateBkg" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchTBkg" />
                                        <span class="imCountdivTempBkImgContainer imTxtContainer"></span>
                                    </div>
                                    <div class="divTempBkImgContainer DamImgContainer">
                                    </div>
                                    <a class="btnImgNextPg btndivTempBkImgContainer" onclick="k17Bk()">Load More Images</a>
                                </div>
                                <div id="divPersonalBkg" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchPBkg" />
                                        <span class="imCountdivPersBkImgContainer imTxtContainer"></span>
                                    </div>
                                    <div class="divPersBkImgContainer DamImgContainer">
                                    </div>
                                    <a class="btnImgNextPg btndivPersBkImgContainer" onclick="k24Bk()">Load More Images</a>
                                </div>
                            </div>
                            <div id="ShapesContainer">
                                <ul style="border-color: transparent;">
                                    <li id="Li1"><a href="#divGlobalBackg">Shapes/Icons</a></li>
                                </ul>
                                <div id="divGlobalShapes" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchShapes" />
                                        <span class="imCountdivGlobBkImgContainer imTxtContainer"></span>
                                    </div>
                                    <div class="divShapesContainer DamImgContainer">
                                    </div>
                                    <a class="btndivShapesContainer btnImgNextPg" onclick="k21Sh()">Load More Images</a>
                                </div>
                            </div>
                            <div id="LogosContainer">
                                <ul style="border-color: transparent;">
                                    <li id="Li2"><a href="#divGlobalLogos">Free Logos</a></li>
                                    <li id="myLogosList"><a href="#divPersonalLogos">My Logos</a></li>
                                </ul>
                                <div id="divGlobalLogos" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchLogos" />
                                        <span class="imCountdivGlobBkImgContainer imTxtContainer"></span>
                                    </div>
                                    <div class="divLogosContainer DamImgContainer">
                                    </div>
                                    <a class="btndivLogosContainer btnImgNextPg" onclick="k21Log()">Load More Images</a>
                                </div>
                                <div id="divPersonalLogos" class="ImgsTab">
                                    <div class="divSearchFunctionality">
                                        <input type="text" placeholder="Search Images" class="InputImgContainer" id="inputSearchPLogos" />
                                        <span class="imCountdivGlobBkImgContainer imTxtContainer"></span>
                                    </div>
                                    <div class="divPLogosContainer DamImgContainer">
                                    </div>
                                    <a class="btndivPLogosContainer btnImgNextPg" onclick="k21PLog()">Load More Images</a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divImageEditScreen">
                        <div class="closePanelButton" title="Close this panel" onclick="pcL36('hide','#divImageEditScreen');">
                            <br />
                        </div>
                        <p class="titletxt" id="P3" style="color: White; text-align: left;">
                            Edit Image
                        </p>
                        <div id="progressbar">
                            <div class="progress-label">Uploading Image...</div>
                        </div>
                        <div class="imageEditScreenContainer">

                            <div class="ImageContainer">
                                <div class="pnlImgBank">
                                    <div class="ImgLbl" style="">Image Title</div>
                                    <input type="text" name="InputImgTitle" id="InputImgTitle" placeholder="Image Title" />
                                </div>
                                <div class="pnlImgBank">
                                    <div class="ImgLbl" style="">Image Description</div>
                                    <input type="text" name="InputImgDescription" id="InputImgDescription" placeholder="Image Description" />
                                </div>
                                <div class="pnlImgBank">
                                    <div class="ImgLbl" style="">Image </div>
                                    <img id="ImgDAMDetail" src="" />
                                </div>

                                <div class="pnlImgBank">
                                    <div class="ImgLbl" style="">Image Keywords</div>
                                    <input type="text" name="InputImgKeywords" id="InputImgKeywords" placeholder="Image Keywords" />
                                </div>
                                <div class="divImageTypes">
                                    <div class="ImgLbl" style="">Image Type</div>
                                    <div class="radioBtnRow">
                                        <input id="radioImagePicture" type="radio" name="imageTypes" checked="checked" value="Picture">
                                        <label for="radioImagePicture" id="Label2">
                                            Pictures</label>
                                    </div>
                                    <div class="radioBtnRow" id="divIconShapesRadioBtn">
                                        <input id="radioImageShape" type="radio" name="imageTypes" value="Shape">
                                        <label for="radioImageShape" id="Label3">
                                            Icons/Shapes</label>
                                    </div>
                                    <div class="radioBtnRow">
                                        <input id="radioImageLogo" type="radio" name="imageTypes" value="Logo">
                                        <label for="radioImageLogo" id="Label4">
                                            Logos</label>
                                    </div>
                                    <div class="clearBoth"></div>
                                    <div class="ImgLbl" style=""></div>
                                    <div class="radioBtnRow onlyRetail">
                                        <input id="radioBtnIllustration" type="radio" name="imageTypes" checked="checked" value="Illustrations">
                                        <label for="radioBtnIllustration" id="Label5">
                                            Illustrations</label>
                                    </div>
                                    <div class="radioBtnRow onlyRetail" id="div1">
                                        <input id="radioBtnFrames" type="radio" name="imageTypes" value="Frames">
                                        <label for="radioBtnFrames" id="Label6">
                                            Frames</label>
                                    </div>
                                    <div class="radioBtnRow onlyRetail">
                                        <input id="radioBtnBanners" type="radio" name="imageTypes" value="Banners">
                                        <label for="radioBtnBanners" id="Label7">
                                            Banners</label>
                                    </div>
                                    <div class="clearBoth"></div>
                                    <br />
                                </div>
                                <div class="" style="margin: 5px; clear: both; text-align: left;" id="territroyContainer">
                                    <div class="ImgLbl" style="">Select Territories</div>
                                    <div id="dropDownTerritories">
                                    </div>
                                </div>
                                <div class="pnlImgBank">
                                    <div class="ImgLbl" style=""></div>
                                    <button id="btnUpdateImgProp">Update</button>
                                    <button id="btnDeleteImg">Delete</button>
                                </div>

                            </div>
                        </div>
                    </div>
                   
                    <div id="divTxtPropPanelRetail" class=" divTxtPropPanelRetail retailPropPanels " style="display: none;">
                        <menu class=" textToolbar txtMenuDiv firstMenuDiv">
                            <li class="textToolbarFont">
                                <select id="BtnSelectFontsRetail" title="Select Font" class="styledBorder retailStoreSelect">
                                    <option value="">(select)</option>
                                </select>
                                <input id="BtnFontSizeRetail" style="" />
                            </li>
                        </menu>
                        <div class="clearBoth"></div>
                        <menu class=" textToolbar txtMenuDiv secondMenuDiv">
                            <li class="textToolbarColor"><a id="AddColorTxtRetailNew" class="pickAColor" href="#" title="Pick a color">
                                <span style="background-color: orange;" class="spanTxtcolour">Pick a color</span></a></li>
                            <li class="elementToolbarLock enabled"><a id="btnLockTxtObj" href="#" title="Lock/Unlock Position">Lock</a></li>
                            <li class="elementToolbarDelete enabled"><a id="btnDeleteTxt" href="#" title="Delete">Delete</a></li>
                            <li class="textToolbarMore"><a onclick="pcL01(1);">More</a>
                                <menu class="toolbarMoreGroup1 toolbarReferencePoint retailPropPanelsSubMenu toolbarText">
                                    <li id="BtnBoldTxtRetail" class="textToolbarBold">Bold</li>
                                    <li id="BtnItalicTxtRetail" class="textToolbarItalic">Italic</li>
                                    <li id="BtnJustifyTxt1Retail" class="textToolbarLeft enabled">Left –</li>
                                    <li id="BtnJustifyTxt2Retail" class="textToolbarCenter enabled on">– Center –</li>
                                    <li id="BtnJustifyTxt3Retail" class="textToolbarRight enabled">– Right</li>
                                    <li id="BtnCopyObjTxtRetail" class="dividerAbove elementToolbarCopy enabled anchorCopy">Copy</li>
                                    <li id="BtnRotateTxtLftRetail" class="elementToolbarLink enabled AnchorRotateLeft">Rotate left</li>
                                    <li id="BtnRotateTxtRightRetail" class="elementToolbarTransparency enabled AnchorRotateRight">Rotate right</li>
                                    <li id="BtnTxtarrangeOrder1Retail" class="elementToolbarForward enabled AnchorMoveUp" style="margin-top: 0px;">Bring forward</li>
                                    <li id="BtnTxtarrangeOrder2Retail" class="elementToolbarForward enabled AnchorMoveUp" style="margin-top: 0px;">Forward</li>
                                    <li id="BtnTxtarrangeOrder3Retail" class="elementToolbarBackward enabled AnchorMoveDown" style="margin-top: 0px;">Back</li>
                                    <li id="BtnTxtarrangeOrder4Retail" class="elementToolbarForward enabled AnchorMoveDown" style="margin-top: 0px;">Send back</li>
                                </menu>
                            </li>
                        </menu>
                    </div>

                    <div id="divImgPropPanelRetail" class=" divTxtPropPanelRetail retailPropPanels " style="display: none;">

                        <menu class=" textToolbar imgMenuDiv firstMenuDiv">
                            <li class="elementToolbar enabled"><a id="BtnCopyObjImgRetail" title="Copy" class="textIndent0" style="border-left: 0px;">Copy</a></li>
                            <li class="elementToolbar enabled elementCrop"><a id="btnReplaceImageRetail" class="textIndent0" title="Replace image" style="border-left: 0px;">Replace </a></li>
                            <li class="elementToolbar enabled"><a id="BtnImageArrangeOrdr2Retail" title="Copy" class="textIndent0" style="border-left: 0px;">Forward</a></li>
                            <li class="elementToolbar enabled"><a id="BtnImageArrangeOrdr3Retail" title="Copy" class="textIndent0" style="border-left: 0px;">Back</a></li>
                        </menu>
                        <div class="clearBoth"></div>
                        <menu class=" textToolbar imgMenuDiv  secondMenuDiv">
                            <li class="elementToolbar disabled elementCrop"><a id="BtnCropImgRetail" class="textIndent0" title="Crop">Crop</a></li>

                            <li class="textToolbarColor elementColorImg width50"><a id="AddColorImgRetailNew" class="pickAColor" href="#" title="Pick a color">
                                <span class="spanRectColour" style="background-color: orange;">Pick a color</span></a></li>
                            <li class="elementToolbarLock enabled"><a id="btnLockImgObj" href="#" title="Lock/Unlock Position">Lock</a></li>
                            <li class="elementToolbarDelete enabled"><a id="btnDelImgRetail" href="#" title="Delete">Delete</a></li>
                            <li class="textToolbarMore"><a onclick="pcL01(2);">More</a>
                                <menu class="toolbarMoreGroup1 toolbarReferencePoint retailPropPanelsSubMenu toolbarImage">
                                    <li id="BtnImgScaleINRetail" class="elementToolbarLink enabled anchorScaleIn">Scale up</li>
                                    <li id="BtnImgScaleOutRetail" class="elementToolbarTransparency enabled anchorScaleOut">Scale down</li>
                                    <li id="BtnImgRotateLeftRetail" class="dividerAbove elementToolbarLink enabled AnchorRotateLeft" style="line-height: 17px;">Rotate left</li>
                                    <li id="BtnImgRotateRightRetail" class="elementToolbarTransparency enabled AnchorRotateRight">Rotate right</li>
                                    <li id="BtnImageArrangeOrdr1Retail" class="elementToolbarForward enabled AnchorMoveUp" style="margin-top: 0px;">Bring forward</li>
                                   <%-- <li id="" class="elementToolbarForward enabled AnchorMoveUp" style="margin-top: 0px;">Forward</li>
                                    <li id="" class="elementToolbarBackward enabled AnchorMoveDown" style="margin-top: 0px;">Back</li>--%>
                                    <li id="BtnImageArrangeOrdr4Retail" class="elementToolbarForward enabled AnchorMoveDown" style="margin-top: 0px;">Send back</li>
                                    <li id="btnImgTransparency" class="elementToolbarForward enabled anchorTransparency" onclick="pcL01(3);">Transparency</li>
                                </menu>

                                <menu class="toobarTransparency retailPropPanelsSubMenu toolbarImageTransparency">
                                    <span>Transparency</span> <span style="margin-left: 10px;">0 </span>
                                    <div class="transparencySlider"></div>
                                    <span>100</span>
                                </menu>
                            </li>
                        </menu>
                    </div>

                </div>
                <div id="CanvasContainer">
                    <div class="divflipSidesIconBC">
                        <label class="titleBcContainer">Showing side - <span class="titleBC">Front</span> </label>
                        <button id="btnToggleSelectBack" class="btnToggleSelectBack" title="Remove this design">
                        </button>
                        <label class="lblBCremoveSide" style="visibility: hidden;">
                            Remove this side</label>
                        <button id="btnFlipSides" class="btnFlipSides" title="Flip sides">
                            Flip
                        </button>
                    </div>
                    <div class="bcBackImgs">
                    </div>
                    <canvas id="canvas">Your browser does not support the canvas.Please upgrade to latest version
                    </canvas>
                    <ul id="actions">
                    </ul>
                    <div class="dimentionsContainer">
                        <span class="dimentionsBC"></span>
                    </div>
                    <div class="divBCCarousel">
                        <button id="btnBCcarouselPrevious" class="btnBCcarouselPrevious">
                        </button>
                        <div class="bcCarouselImagesOuterDiv" id="bcCarouselImagesOuterDiv">
                            <div class="bcCarouselImages" id="bcCarouselImages">
                            </div>
                        </div>
                        <button id="btnBCcarouselNext" class="btnBCcarouselNext">
                        </button>
                    </div>
                </div>
                <script type="text/javascript" src="js/p35.js"></script>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        (function () {
            var mainScriptEl = document.getElementById('main');
            if (!mainScriptEl) return;
            var el = document.createElement('pre');
            el.innerHTML = mainScriptEl.innerHTML;
            el.lang = 'javascript';
            el.className = 'prettyprint';
            document.getElementById('bd-wrapper').appendChild(el);
            prettyPrint();

        })();

    </script>
    <input id="getCopied" style="position:absolute;left:-115px;top:-25px;height:20px;width:100px;"/>
</body>
</html>
