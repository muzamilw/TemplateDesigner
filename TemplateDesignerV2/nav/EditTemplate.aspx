<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditTemplate.aspx.cs" Inherits="TemplateDesignerV2.nav.EditTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/navStyles.css" rel="stylesheet" type="text/css" />
    <link href="../styles/DesignerStyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../styles/jquery.rating.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../styles/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" />
    <script src="../js/p55-17.js" type="text/javascript"></script>
<%--    <script src="../js/p55.js" type="text/javascript"></script>--%>
    <%--  <script src="../js/p77.js" type="text/javascript"></script>
    <script src="../js/p71.js" type="text/javascript"></script>
    <script src="../js/p64.js" type="text/javascript"></script>--%>
    <script src="../js/p40.js" type="text/javascript"></script>
    <script src="../js/p90.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //            animatedcollapse.addDiv('DivColorPallet', 'fade=1,speed=400,group=panel,persist=0,hide=1');
            //            animatedcollapse.addDiv('DivAdvanceColorPanel', 'fade=1,speed=400,persist=0,hide=1');
            //            animatedcollapse.init();
            //      loadColors();
            //    InitColorSlider();
            $(".rating").rating({ showCancel: false });
            $(".ratingDisabled").rating({ showCancel: false,disabled:true });
        });

        function BaseColorOnClick(ID) {
           
            $('input:radio[id=' + ID + ']').attr('checked', true);
            $('input:radio[id=' + ID + ']').parent().parent().addClass('ShadowToBaseColor'); //.css("background-color", "#f3f3f3");
            
             var BaseClrId = $('input:radio[id=' + ID + ']').attr("data-BaseColorID");

            $("#<%=hfBaseColorID.ClientID %>").val(BaseClrId);

                $('input[type=radio]').each(function () {
                    if ($(this).is(':checked')) {
                    } else {
                        $(this).parent().parent().removeClass('ShadowToBaseColor'); //.css("background-color", "#ffffff");
                    }
                });
             
            }
        //function InitColorSlider() {

        //	$("#DivColorC,#DivColorM,#DivColorY, #DivColorK").slider({
        //		orientation: "horizontal",
        //		range: "min",
        //		max: 100,
        //		slide: sliderUpdate
        //	});

        //}
        //// function to update color picker
        //function sliderUpdate() {
        //	var c = $("#DivColorC").slider("value");
        //	var m = $("#DivColorM").slider("value");
        //	var y = $("#DivColorY").slider("value");
        //	var k = $("#DivColorK").slider("value");
        //	var hex = getColorHex(c, m, y, k);
        //	//colorPicker.setColor("#" + hex);
        //	//$("#swatch").css("background-color",  hex);
        //	UpdateColorPallet(c, m, y, k);
        //}
        //// function to add color pallets
        //function UpdateColorPallet(c, m, y, k) {
        //	//var path = "Designer/PrivateFonts/FontFace/"

        //	var Color = getColorHex(c, m, y, k);
        //	//alert(Color);
        //	var html = "<label for='ColorPalle' id ='LblCollarPalet'>Click on the pallet to apply Color</label><div class ='ColorPallet' style='background-color:" + Color + "' onclick='ChangeColorHandler(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);UpdateRecentColorDiv(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
        //	$('#LblDivColorC').html(c + "%");
        //	$('#LblDivColorM').html(m + "%");
        //	$('#LblDivColorY').html(y + "%");
        //	$('#LblDivColorK').html(k + "%");
        //	$('#ColorPickerPalletContainer').html(html);
        //}
        //function UpdateRecentColorDiv(c, m, y, k, Color) {
        //	var size = $("#DivRecentColors > div").size();

        //	var html = "<div class ='ColorPallet' style='background-color:" + Color + "' onclick='ChangeColorHandler(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
        //	if (size < 10) {

        //		$('#DivRecentColors').append(html);
        //	} else {

        //		var list = document.getElementById('DivRecentColors');
        //		list.removeChild(list.childNodes[0]);
        //		$('#DivRecentColors').append(html);

        //	}

        //}
        //// load template colors
        //function loadColors() {
        //	$.getJSON("../services/TemplateSvc/GetColor/" + TemplateID,
        //function (data) {
        //	$.each(data, function (i, item) {
        //		addColorPallet(item.ColorC, item.ColorM, item.ColorY, item.ColorK);
        //	});
        //});
      //  }
        //// function to add color pallets
        //function addColorPallet(c, m, y, k) {
        //	//var path = "Designer/PrivateFonts/FontFace/"

        //	var Color = getColorHex(c, m, y, k);
        //	//alert(Color);
        //	var html = "<div class ='ColorPallet' style='background-color:" + Color + "' onclick='ChangeColorHandler(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
        //	$('#DivColorContainer').append(html);
        //}
        //function ChangeColorHandler(c, m, y, k, ColorHex) {
        //	var rgb = hexToR(ColorHex) + "," + hexToG(ColorHex) + "," + hexToB(ColorHex);
        //	//alert(rgb);

        //	//$(".RGBVal").text(rgb);
        //	$("input[name=RGBVal]").val(rgb)
        //	$(".btnSaveColorData").click();
        //	//$(".btnSaveColorData").trigger("click");  
        //}

        //function hexToR(h) { return parseInt((cutHex(h)).substring(0, 2), 16) }
        //function hexToG(h) { return parseInt((cutHex(h)).substring(2, 4), 16) }
        //function hexToB(h) { return parseInt((cutHex(h)).substring(4, 6), 16) }
        //function cutHex(h) { return (h.charAt(0) == "#") ? h.substring(1, 7) : h }

        //function closeColorPicker() {
        //	$("#DivColorPalletNav").css("display", "none");
        //	$("#DivAdvanceColorPanel1").css("display", "none");
        //}

        //function closePicker() {
        //	$("#DivAdvanceColorPanel1").css("display", "none");
        //}



        function rejection() {
            var x;

            var reason = prompt("Please enter the rejection reason", "");

            if (reason != null) {

                $("#RejectionReason").val(reason);
                return true;
            }
            else
                return false;
        }
    </script>
</head>
<body class="bodybg">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <script type="text/javascript" language="javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $(".rating").rating({ showCancel: false });
                $(".ratingDisabled").rating({ showCancel: false, disabled: true });
            }

        </script>

        <asp:HiddenField ID="hfBaseColorID" runat="server" />
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgressUserProfile" runat="server">
                    <ProgressTemplate>
                        <asp:Panel ID="Panel2" CssClass="loader" runat="server" Width="300px">
                            <div id="lodingDiv" class="FUPUp rounded_corners">
                                <br />
                                <div id="loaderimageDiv">
                                    <img src='~/assets/asdf.gif' alt="" runat="server" />
                                </div>
                                <br />
                                <div id="lodingBar" style="text-align: center;">
                                    Loading Please wait....
                                </div>
                            </div>
                        </asp:Panel>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="contentArea">
                    <div id="NavBar" class="rounded_corners">



                       <div style="float:left;">
                           <asp:HiddenField ID="RejectionReason" runat="server" ClientIDMode="Static" />
                        <asp:Button ID="BtnSaveTemplate" Text="Save &amp; close" runat="server" OnClick="BtnSaveTemplateClick"
                            CssClass="smargin NavBtn" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit for Approval" CssClass="smargin NavBtn" Visible="false"
                            OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="smargin NavBtn" Visible="False"
                            OnClick="btnApprove_Click" />
                        <asp:Button ID="btnReject" Text="Reject Template" runat="server" OnClick="BtnRejectTemplateClick"
                            CssClass="smargin NavBtn" Visible="False" OnClientClick="return rejection();" />

                        <asp:Button ID="BtnCancel" Text="Cancel" runat="server" OnClick="BtnCancel_Click"
                            CssClass="smargin NavBtn" CausesValidation="false" />
                       </div>
                        
                        <div style="float:right; margin-top: 5px;">
                             <asp:Button ID="BtnDeleteTemplate" Text="Delete" runat="server" OnClick="BtnDeleteTemplateClick"
                            CssClass="floatRight RedBtn MarginRight" OnClientClick="return window.confirm('Are you sure you want to delete this template, this action is irreversible and will result in deletion of all information')" />
                        </div>
                    </div>
                    <div>
                        <asp:ValidationSummary ID="ValidationSummary1" HeaderText="  Missing information :"
                            runat="server" BorderStyle="Dotted" BorderColor="Red" ForeColor="Red" Font-Size="Large" />
                        <asp:Label ID="lblError" runat="server" Text="" BorderStyle="Dotted" BorderColor="Red"
                            ForeColor="Red" Font-Size="Large" Visible="false"></asp:Label>
                    </div>
                    <div id="Div1" class="left_roundedCorners FormBox" style="width:440px; float:left; min-height:420px;">
                        <div class="DivTitle">
                            Step 1 : Template Information
                        </div>
                        <label class="label">
                            Template / Matching set name :</label><br />
                        <asp:TextBox ID="TextBoxMatchingSet" runat="server" CssClass="input"></asp:TextBox><asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" ErrorMessage="Template Name is required"
                            ControlToValidate="TextBoxMatchingSet" Display="None"></asp:RequiredFieldValidator>
                        <label class="labelright bigText" id="LblTemplateStatusLabel" runat="server" style="display: none;">
                            Template Status :
                        </label><br />
                        <label runat="server" id="LblTemplateStatus" class="rControl bigText" style="display: none;">
                        </label>
                        <label class="label">
                            Catagory :
                        </label><br />
                        <asp:DropDownList ID="cboCategory" runat="server" CssClass="input keepwhitespace" Width="205px">
                        </asp:DropDownList>
                         <label class="label">
                                Base Color
                            </label><br />
                            <asp:DropDownList ID="DropDownColor" runat="server" CssClass="input keepwhitespace" Width="205px" style="display:none;">
                            </asp:DropDownList>
                        <div class="clear">
                            &nbsp;
                        </div>
                        <div id="BaseColorsContainer" runat="server">
                                    </div> <div class="clear">
                            &nbsp;
                        </div>
                            <label id="lblSLCode" class="label" runat="server">
                                SL Code :
                            </label><br />
                            <asp:TextBox ID="txtTemplateCode" runat="server" CssClass="input" /><br />
                       <div style="clear:both;">
                           &nbsp;
                       </div>
                        <div style=" background-color:#f3f3f3; padding:10px; clear:both; padding-bottom:30px;">
                           <div id="editorControl" runat="server" visible="false" style="float:left; margin-right:30px;">
                            <label id="Label1" runat="server" class="label">
                                Mark as Editors Choice :</label>
                            <asp:RadioButton Text="Yes" ID="RadioBtnEditorYes" CssClass="MarginRight marginleft7" runat="server"
                                GroupName="EditorChoice" />
                            <asp:RadioButton ID="RadioBtnEditorNo" Text="No" CssClass="MarginRight"
                                runat="server" GroupName="EditorChoice" />
                        </div>
                            <div style="float:left; ">
                                <label class="ratingControlLabel" style="line-height:2;">
                            Template Rating :
                        </label>
                        <select class="rating  keepwhitespace" id="ratingControl" runat="server">
                            <option value="0">Template Rating </option>
                            <option value="1">Template Rating </option>
                            <option value="2" selected="selected">Template Rating</option>
                            <option value="3">Template Rating</option>
                            <option value="4">Template Rating</option>
                        </select>
                            </div>
                        <div style="float:left; display:none;">
                            <label class=" ratingControlLabel" style="line-height:2;">
                            User Rating :
                        </label>
                        <select class="ratingDisabled  keepwhitespace" id="ratingControlUser" runat="server">
                            <option value="0" selected="selected">User Rating </option>
                            <option value="1">User Rating </option>
                            <option value="2">User Rating</option>
                            <option value="3">User Rating</option>
                            <option value="4">User Rating</option>
                        </select>
                        </div>
                        <span style="height: 20px; width: 20px;"></span>
                        <div class="clear">
                            &nbsp;
                        </div>
                            </div>
                        <label id="lblSubmittedLabel2" runat="server" class="labelright">
                            Submitted by :
                        </label>
                        <label id="lblSubtmittedByLabel" class="rControl" runat="server">
                        </label>
                        <label id="lblApproveLabel2" runat="server" class="labelright">
                            Approved by :
                        </label>
                        <label id="lblApprovedByLabel" class="rControl" runat="server">
                        </label>
                        <label id="lblRejectLabel2" runat="server" class="labelright">
                            Rejected Reason :
                        </label>
                        <label id="lblRejectLabel" class="rControl" runat="server" style="color: Red;">
                        </label>
                        
                       
                        <div class="clear">
                        </div>
                    </div>
                  <div id="Div4" class="right_roundedCorners FormBox" style="width:500px; float:left; min-height:420px;">
                        <div class="DivTitle" style="display:none;">
                            Step 3 : Filtering Information
                        </div>
                        <div >
                            <div class="FloatLeft">
                                 <label class="label">
                                Narrative Tag :</label><br />
                            <asp:TextBox ID="txtNarrativeTag2" runat="server" Height="100px" TextMode="MultiLine"
                                CssClass="input"></asp:TextBox>
                           
                            <%--            <asp:RequiredFieldValidator ID="FVTemplateCode" runat="server" ErrorMessage="SL Code is required"
					ControlToValidate="txtTemplateCode" Display="None"></asp:RequiredFieldValidator>--%>
                            <label class="label" runat="server" id="lblrbtn">
                                &nbsp
                            </label>
                            <asp:RadioButton ID="rbtnTempPrivate" runat="server" Text="This is a private template"
                                CssClass="inputNoBorder" GroupName="priv" Checked="true" />
                            <label class="label">
                                &nbsp
                            </label>
                            <asp:RadioButton ID="rbtnTempPublic" runat="server" Text="This is a public template available to all MyPrintCloud Customers, MPC offers benefits once these templates are approved."
                                CssClass="inputNoBorder" GroupName="priv" />
                            </div>
                            <div class="FloatLeft" style="margin-left:20px;">
                                   <label class="label">
                                Front :</label><br />
                                <div>
                                <asp:Image ID="imgFront"  runat="server"  style="max-height: 110px;" /></div>
                            </div>
                           <div style="clear:both;">
                               &nbsp;
                           </div>
                        </div>
                        <div class="">
                            <div class="FloatLeft">
                                <label class="label">
                                Industry Tags :
                            </label><br />
                            <div class="ListItem">
                                <asp:CheckBoxList ID="ListIndustryTags" runat="server" OnDataBound="ListIndustryTags_DataBound">
                                </asp:CheckBoxList>
                            </div>
                            </div>
                            <div class="FloatLeft" style="margin-left:20px;">
                                 <label class="label">
                                Back:
                            </label><br />
                                <asp:Image ID="imgBack" runat="server"  style="max-height: 110px;" />
                            </div>
                            
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                    <div style="clear:both;">
                        &nbsp;
                    </div>
                    <div id="Div2" class="rounded_corners FormBox" style="display: none;">
                        <div class="DivTitle">
                            Step 3 : Template Thumbnails
                        </div>
                        <div class="imgThumbnailBox">
                            <label>
                                SL Thumbnail</label>
                            <br />
                            <asp:Image ID="imgSLThumbNail" runat="server" CssClass="imgThumbnail" />
                            <br />
                            <asp:FileUpload runat="server" ID="FileSLThumbNail" />
                        </div>
                        <div class="imgThumbnailBox">
                            <label>
                                Full View</label>
                            <br />
                            <asp:Image ID="imgFullView" runat="server" CssClass="imgThumbnail" /><asp:FileUpload
                                runat="server" ID="FileFullView" />
                        </div>
                        <div class="imgThumbnailBox">
                            <label>
                                Super View</label><br />
                            <asp:Image ID="imgSuperView" runat="server" CssClass="imgThumbnail" /><asp:FileUpload
                                runat="server" ID="FileSuperView" />
                        </div>

                        <asp:Button ID="BtnUploadThumbnails" Text="Upload Thumbnails" runat="server"
                            OnClick="BtnSaveTemplateClick"
                            CssClass="saveDesignButton NavBtn MarginRight" />

                        <div class="clear">
                        </div>
                    </div>
                    <div id="Div3" class="rounded_corners FormBox">
                        <div class="DivTitle">
                            Step 2 : Template Sides   
                            <asp:Button Text="Add Side" ID="BtnAddNewPage" runat="server" CssClass="NavBtnOrange MarginRight saveDesignButton" OnClick="BtnAddNewPage_Click" />
                        </div>
                        <div id="DivColorPickerDraggable">
                            <div id="DivColorPalletNav" runat="server" class="popUpaddTextPanel">
                                <div class="closePanelButton" onclick="closeColorPicker()">
                                    <br />
                                </div>
                                <p id="lbtSelectColor" class="largeText">
                                    Select Color
                                </p>
                                <div id="DivColorContainer">
                                </div>
                                <div>
                                    <div id="DivRecentColors">
                                    </div>
                                    <asp:Button ID="BtnAdvanceColorPicker" runat="server" Text="Advance Color Picker"
                                        OnClick="BtnAdvanceColorPicker_Click" />
                                </div>
                            </div>
                            <div id="DivAdvanceColorPanel1" runat="server" class="popUpQuickTextPanel">
                                <div class="panelItemsRightAligned">
                                    <div class="closePanelButtonQuickText" onclick="closePicker()">
                                        <br />
                                    </div>
                                </div>
                                <div id="DivLblMoveSlider" class="largeText">
                                    Move the Slider to Change Color Percentage
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
                                        Click on the pallet to apply Color</label>
                                    <div class="ColorPallet" style="background-color: White" onclick="ChangeColorHandler(0,0,0,0,&quot;#ffffff&quot;);UpdateRecentColorDiv(0,0,0,0,&quot;#ffffff&quot;);">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="errorMessage" id="DiverrorMessage" runat="server">
                            File specified in background type does not match the file you Uploaded
                        </div>
                        <div class="DivPageContainerr">
                            <asp:Repeater ID="RepeaterPages" runat="server" OnItemCommand="Pages_ItemCommand"
                                OnItemDataBound="RepeaterPages_ItemDataBound">
                                <ItemTemplate>
                                    <div class="DivPageRow">
                                        Page &nbsp;<asp:Label ID="lblPageNo" Text='<%# Eval("PageNo")  %>' runat="server"></asp:Label>
                                        <asp:RadioButton Text="LandScape" ID="RadioBtnLandScape" CssClass="MarginRight" runat="server"
                                            GroupName="Orientation" OnCheckedChanged="PageStateChanged" AutoPostBack="true" />
                                        <asp:RadioButton ID="RadioButtonPortrait" Text="Portrait" CssClass="MarginRight"
                                            runat="server" GroupName="Orientation" AutoPostBack="true" OnCheckedChanged="PageStateChanged" />
                                        <asp:Label ID="Label2" runat="server" CssClass="MarginRight "> Page Type</asp:Label>
                                        <asp:DropDownList runat="server" ID="DropDownPageType" Width="180px" OnSelectedIndexChanged="PageStateChanged"
                                            CssClass="MarginRight" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:Label ID="Label3" runat="server" CssClass="" Visible="false"> BackgroundType</asp:Label>
                                        <asp:DropDownList runat="server" ID="DropDownBackground" Width="90px" OnSelectedIndexChanged="PageStateChanged"
                                            CssClass="MarginRight" AutoPostBack="true" Visible="false">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="PageID" Text='<%# Eval("ProductPageId")  %>' Visible="False"
                                            CssClass="MarginRight"></asp:Label>
                                        <asp:ImageButton runat="server" ImageUrl="~/assets/PageUp.png" ID="BtnMovePageUp"
                                            CommandName="MovePageUp" CommandArgument='<%# Eval("PageNo")  %>' CssClass="ImgBtn MarginRight" ToolTip="Move side upwards" />
                                        <asp:ImageButton runat="server" ImageUrl="~/assets/Pagedown.png" ID="BtnMovePageDown"
                                            CommandName="MovePageDown" CommandArgument='<%# Eval("PageNo")  %>' CssClass="ImgBtn MarginRight" ToolTip="Move side downwards" />
                                        <asp:ImageButton runat="server" ImageUrl="~/assets/PageDelete.png" ID="BtnDeletePage"
                                            CommandName="DeletePage" CommandArgument='<%# Eval("PageNo")  %>' CssClass="ImgBtn MarginRight floatRight"
                                            OnClientClick="return confirm('are you sure to delete the template page?')" ToolTip="Delete side" />
                                        <span runat="server" id="DivFileUploader" visible="false">
                                            <label class="FileUploadLbl">
                                                File
                                            </label>
                                            <asp:FileUpload ID="UploaderBkType" runat="server" Visible="false" />
                                            <asp:Button ID="UploadFileBtn" runat="server" CommandName="UploadBackground" CommandArgument='<%# Eval("ProductPageId")  %>'
                                                Text="Upload File" Visible="false"></asp:Button>
                                        </span><span id="DivColorupdator" runat="server" visible="false">
                                            <label>
                                                Color Value :
                                            </label>
                                            <input type="text" id="RGBColorVal" runat="server" />
                                        </span>
                                    </div>
                                </ItemTemplate>

                            </asp:Repeater>
                            <div runat="server" id="lblnoSides" class="MarginBottom" visible="false">No Sides Exist</div>
                            <asp:Button ID="Button1" runat="server" OnClick="SaveColrData_Click" CssClass="btnSaveColorData" />
                            <input type="text" name="RGBVal" id="RGBVal" runat="server" class="RGBVal" />
                        </div>

                        <asp:Button ID="BtnEditTemplate" Text="Design Template" runat="server"
                            OnClick="BtnEditTemplateClick" CssClass="saveDesignButton NavBtn MarginRight clearBoth" />
                        <div class="clear">
                        </div>
                    </div>
                    
                    
                </div>
            </ContentTemplate>
            <Triggers>

                <asp:PostBackTrigger ControlID="BtnUploadThumbnails" />

            </Triggers>

        </asp:UpdatePanel>
       
    </form>
</body>
</html>
