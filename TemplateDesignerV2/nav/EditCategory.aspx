<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="TemplateDesignerV2.nav.EditCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
	<link href="../styles/navStyles.css" rel="stylesheet" type="text/css" />
	<link href="../styles/DesignerStyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../js/p55.js" type="text/javascript"></script>

 
</head>
<body class="bodybg">
   <script type="text/javascript">
       function updateValue() {
           var val1 =document.getElementById("<%=textBoxWidth.ClientID%>").value;
           var val2 = document.getElementById("<%=textboxZoomFactor.ClientID%>").value;
           document.getElementById("<%=textBoxPDFWidth.ClientID%>").value = parseFloat(val1) * parseFloat(val2);

           val1 = document.getElementById("<%=textboxHeight.ClientID%>").value;
           val2 = document.getElementById("<%=textboxZoomFactor.ClientID%>").value;
           document.getElementById("<%=textBoxPDFHeight.ClientID%>").value = parseFloat(val1) * parseFloat(val2);
       }
       //foldLinesContainer
     $(document).ready(function () {

     });
//    $("#radioBtnApplyFoldYes").click(function () {

//        var thisCheck = $(this);
//        if (thisCheck.is(':checked')) {


//        }
//        else {


//        }
//        alert();
//    });
  
    </script>
	<form id="form1" runat="server" >
	 <asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	   
	<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
	<ContentTemplate>
		<asp:UpdateProgress ID="UpdateProgressUserProfile" runat="server">
		<ProgressTemplate>
			<asp:Panel ID="Panel2" CssClass="loader" runat="server" Width="300px">
				<div id="lodingDiv" class="FUPUp rounded_corners">
					<br />
					<div id="loaderimageDiv">
						<img id="Img1" src='~/assets/asdf.gif' alt="" runat="server" />
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
<%--                <div id="NavBar" class="rounded_corners">
		   
					<asp:Button ID="BtnCancel" Text="Close" runat="server"
						CssClass="smargin NavBtn" CausesValidation="false" />
			
			</div>--%>
		<div id="helpbox" class="rounded_corners">
			<img src="../assets/logo.png" />
			<br />
			My Template Library
				<div class="logininfo">
					<asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label>
					<asp:LoginView ID="LoginView1" runat="server">
						<LoggedInTemplate>
							<asp:LoginStatus ID="LoginStatus1" runat="server" />
							<br />
						</LoggedInTemplate>
					</asp:LoginView>
			</div>
		</div>
			<div id="Div3" class="rounded_corners FormBox">
				<div id="DivCloseDesignerBtn" class="closeDesignerButtonCat" runat="server" >
						<%--<asp:ImageButton Height="29px" Width="29px" ImageUrl="~/assets/Button_Close_page.png" runat="server" ID="BtnImgClosePage"  OnClick="BtnCancel_Click"/>--%>
				</div >
                <div class="TTL">
				<div class="DivTitle"> 
					Template Categories</div> </div>
                <div class="TLR">
                    <div runat="server" id="divSavedSucessMsg" style="color:Red ;width:80% ;margin-top:8px; text-align:center; margin-left: 320px;" visible="false">Updated Successfully.</div>
					<div runat="server" id="divMessageDel" style="color:Red ;width:80% ;margin-top:8px; text-align:center; margin-left: 320px;" visible="false">Category Deleted Sucessfully.</div>
						
                </div >
                <div  class="TLR" style="margin-left: 192px;margin-top: -16px;">
                <asp:Button Text="Home" ID="Button1" runat="server" CssClass="NavBtn marginBottom10 marginLeft5 margintop6 " OnClick="BtnCancel_Click" /> 
						<br />
                </div>
                <div class="clearBoth"></div>
				<div class="DivPageContainerr">
					<div class="divCategoryTree  grey_back_div rounded_corners">
						<asp:Button Text="Add New Category" ID="BtnAddNewCategory" runat="server" CssClass="NavBtn marginBottom10 marginLeft5 margintop6 " OnClick="BtnAddNewCategory_click" /> 
						<br />
						<asp:TreeView runat="server" ID="TreeViewCategories" CssClass="rootNodeLinkStyle" OnSelectedNodeChanged="TreeViewCategories_SelectedNodeChanged"  ></asp:TreeView>
					</div>
					
					<div class="divCategoryDetail grey_back_div rounded_corners" id="divCategoryDetail" runat="server" visible ="false">
						<div >
							<asp:Button Text="Edit Category" ID="btnEditCategory" runat="server" 
								CssClass="NavBtn marginBottom10 marginLeft15   margintop6 " onclick="btnEditCategory_Click" /> 
							<asp:Button Text="Update Category" ID="btnUpdateCategory" runat="server"  ValidationGroup="CatDetailVgGroup"
								CssClass="NavBtn marginBottom10 marginLeft15   margintop6 "  Visible="false"
								onclick="btnUpdateCategory_Click" />     
							<asp:Button Text="Add Category" ID="btnAddCategory" runat="server"  ValidationGroup="CatDetailVgGroup"
								CssClass="NavBtn marginBottom10 marginLeft15   margintop6 "  Visible="false"
								onclick="btnCreateCat_click" />         

							<asp:Button Text="Delete Category" ID="btnDeleteCategory" runat="server" 
								CssClass="NavBtn marginBottom10 marginLeft15   margintop6  "  OnClientClick="return confirm('Are you sure to delete this Category? All templates in this category will also be deleted permanently. This process might take a long time.')"
								onclick="btnDeleteCategory_Click" /> 
							<asp:Button Text="Cancel" ID="btnCancelCategoryUpdate" runat="server" 
								CssClass="NavBtn marginBottom10 marginLeft15   margintop6 " 
								onclick="btnCancelCategoryUpdate_Click"  /> 
						</div>
						<div class="TLR">
							
						</div>
						<div class="TLR">
							
						</div>
						<br />

							<div class="normalTextStyle divHalfRightProfile">
								<div class="TLR" >
									Category Name
								</div>
								<div class="TTL">
									<asp:Label ID="lblCategoryID" runat="server" Width="188px" CssClass="text_box200 rounded_corners5"
										Visible="false"></asp:Label>
									<asp:TextBox ID="txtCategoryName" runat="server" Width="188px" CssClass="text_box200 rounded_corners5"
										TabIndex="1" Enabled="false"></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName" ErrorMessage="*Required." Display="Dynamic" CssClass="errmessage" ValidationGroup="CatDetailVgGroup"></asp:RequiredFieldValidator>
								</div>
								<div class="clearBoth">
								</div>
								<br />

								<div class="TLR" >
									Parent Category
								</div>
								<div class="TTL">
									<asp:DropDownList runat="server" CssClass="text_box200 rounded_corners5" ID="dropDownParentCategory" Enabled = "false" TabIndex="2"></asp:DropDownList>
								</div>
								<div class="clearBoth">
								</div>
								<br />
								<div runat="server" id="divCatType">
									<div class="TLR" >
										Catagory Type 
									</div>
									<div class="TTL">
										<asp:DropDownList runat="server" CssClass="text_box200 rounded_corners5" Enabled = "false"  ID="dropDownCategoryType" TabIndex="3"></asp:DropDownList>
									</div>
								</div>
								<div class="clearBoth">
								</div>
								<br />

								<div class="TLR" >
									Region 
								</div>
								<div class="TTL">
									<asp:DropDownList runat="server" CssClass="text_box200 rounded_corners5" ID="dropDownRegion" Enabled = "false"  TabIndex="4"></asp:DropDownList>
								</div>
								<div class="clearBoth">
								</div>
								<br />

								<div class="TLR" >
									Apply Size Restrictions
								</div>
								<div class="TTL marginTop5">
									<asp:RadioButton ID="radioBtnApplySizeTrue" TabIndex="5" Checked="true" GroupName="SizeRestriction" Enabled = "false"  Text="Yes" runat="server"/>
									<asp:RadioButton ID="radioBtnApplySizeFalse" TabIndex="6"  GroupName="SizeRestriction" Enabled = "false"  Text="No" runat="server"/>
								</div>
								<div class="clearBoth">
								</div>
								<br />
                                <div>
								    <div class="TLR" style="margin-left:119px;">
									   PDF Width (mm)
								    </div>
                                    <div class="TLR"  style="width:22%;">
									    PDF Height (mm)
								    </div>
                                </div>
                                <div class="clearBoth"></div>
                                <div>
								    <div class="TLR" style="width:212px; margin-left:81px;">
									    <asp:TextBox ID="textBoxWidth" runat="server" Width="50px" CssClass="text_box200 rounded_corners5" Enabled = "false" 
										    TabIndex="7"></asp:TextBox>   
									    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"  ControlToValidate="textBoxWidth"  runat="server" ErrorMessage="*Invalid" CssClass="errmessage" ValidationGroup="CatDetailVgGroup" ValidationExpression="^[0-9]*\.?[0-9]*"> </asp:RegularExpressionValidator>
									    <br />
									  
								    </div>
								    <div class="TLR" style="width:155px; margin-left:-41px;">
									    <asp:TextBox ID="textboxHeight" runat="server" Width="50px" CssClass="text_box200 rounded_corners5" Enabled = "false" 
										    TabIndex="8"></asp:TextBox>
									   
									    <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  ControlToValidate="textboxHeight"  runat="server" ErrorMessage="*Invalid" CssClass="errmessage" ValidationGroup="CatDetailVgGroup" ValidationExpression="^[0-9]*\.?[0-9]*"> </asp:RegularExpressionValidator>
									     <br />
									    
								    </div>
                                    <div class="clearBoth"></div>
                                    <div class="TLR" style="width:296px; margin-left:97px;">
                                        <asp:Label runat="server" ID="lblWidthTip" Text="Add an additional 10mm for bleed area "></asp:Label>
                                     </div>
                                </div>
								<div class="clearBoth">
								</div>
								<br />
                                <div>
								    <div class="TLR" style="margin-left:86px;">
									   Scale Factor
								    </div>
                                    <div class="TLR" style="margin-left:-30px;">
									    Visible Width (mm)
								    </div>
                                    <div class="TLR"  style="width:22%;  margin-left: -4px;">
									    Visible Height (mm)
								    </div>
                                </div>

								<div class="TLR" style="margin-left:122px;">
									<asp:TextBox ID="textboxZoomFactor"  runat="server" Width="50px" CssClass="text_box200 rounded_corners5" Enabled = "false" 
										TabIndex="9"></asp:TextBox>
									<%--<asp:RegularExpressionValidator ID="RegularExpressionValidator3"  ControlToValidate="textboxZoomFactor"  runat="server" ErrorMessage="Invalid Format" CssClass="errmessage" ValidationGroup="CatDetailVgGroup" ValidationExpression="^[0-9]*\.?[0-9]*"> </asp:RegularExpressionValidator>
                                    --%>
                                    <asp:RangeValidator ID="rvclass" 
                                           runat="server" 
                                           ControlToValidate="textboxZoomFactor" 
                                           ErrorMessage="*Invalid" 
                                           MaximumValue="12"  ValidationGroup="CatDetailVgGroup"
                                           MinimumValue="1" Type="Double">
                                    </asp:RangeValidator>
								</div>
                                <div class="TLR" style="width: 60px; margin-left: 9px;">
                                    <asp:TextBox ID="textBoxPDFWidth" ReadOnly="true" Enabled="true" runat="server" Width="50px" CssClass="text_box200 rounded_corners5"
                                            ></asp:TextBox>      
                                </div>
                                <div class="TLR" style="width: 110px ;  margin-left: 2px;">
                                    <asp:TextBox ID="textBoxPDFHeight" ReadOnly="true" Enabled="true" runat="server" Width="50px" CssClass="text_box200 rounded_corners5"
                                            ></asp:TextBox>
                                </div>

                                <div class="clearBoth">
                                </div>
                                <div>

                                    <div class="clearBoth">
                                    </div>
                                    <br />
								<div class="TLR" >
									Apply Fold lines
								</div>
								<div class="TTL marginTop5">
									<asp:TextBox runat="server" ID="textboxCategoryID" ViewStateMode="Enabled" Visible="false"></asp:TextBox>
									<asp:RadioButton ID="radioBtnApplyFoldYes" CssClass="radioBtnApplyFoldYes"  AutoPostBack="true"
                                        TabIndex="10" GroupName="FoldRestriction" Enabled = "false"  Text="Yes" 
                                        runat="server" oncheckedchanged="radioBtnApplyFoldYes_CheckedChanged"/>
									<asp:RadioButton ID="radioBtnApplyFoldNo"  CssClass="radioBtnApplyFoldNo" AutoPostBack="true" 
                                        TabIndex="11" Checked="true" GroupName="FoldRestriction" Enabled = "false"  
                                        Text="No" runat="server" oncheckedchanged="radioBtnApplyFoldNo_CheckedChanged"/>
									<br />
									<asp:Label runat="server" ID="lblFoldLineMsg" Text="*Save Category to apply Fold lines data. <br /> *Categories containing empty height and widht will <br /> &nbsp; not be shown for template creation or editing."></asp:Label>
								</div>
								<div class="clearBoth">
								</div>
								<br />
							</div>
							<div id="foldLinesContainer" runat="server" class="normalTextStyle divHalfRightProfile foldLinesContainer">
					 
								<div class="TLR" runat="server" id="divTrifoldLinesTxt">
									Fold Lines
								</div>
								<div class="TTL marginTop5" runat="server" id="divRepeaterContainer">
									<asp:Repeater ID="repeaterFoldLines" runat="server" OnItemCommand="Pages_ItemCommand"
										OnItemDataBound="repeaterFoldLines_ItemDataBound">
										<ItemTemplate>
											<div class="DivPageRowFoldLines">
												Offset:
												
												<asp:TextBox runat="server" Width="40px" ID="txtBoxOffsetFromMargin"   CssClass="text_box200 rounded_corners5" Text=' <%# Eval("FoldLineOffsetFromOrigin")  %>' ></asp:TextBox>
												<asp:Label ID="lblFoldLineOrientation" Text='<%# Eval("FoldLineOrientation")  %>' runat="server" Visible="false"></asp:Label>
												<asp:Label ID="lblFoldLineId" Text='<%# Eval("FoldLineID")  %>' runat="server" Visible="false"></asp:Label> 
												<asp:DropDownList runat="server" ID="dropDownOrientation" Width="98px" CssClass="text_box200 rounded_corners5" ></asp:DropDownList>
												<asp:Button runat="server" Text="Update"  CssClass="NavBtn marginBottom10 marginRight5 rounded_corners" Width="50px" CommandName="updateFoldLine"  CommandArgument='<%# Eval("FoldLineID")  %>' ID="btnUpdateFoldLine" />
												 <asp:ImageButton runat="server" ImageUrl="~/assets/PageDelete.png" ID="btnDeleteFoldLine"
													CommandName="deleteFoldline" CommandArgument='<%# Eval("FoldLineID")  %>' Height="22px" Width="22px" CssClass="maginbtm8"
													OnClientClick="return confirm('are you sure to delete this fold line?')" ToolTip="Delete FoldLine" />
											</div>
										</ItemTemplate>
					
									</asp:Repeater>
									<div runat="server" id="lblnoFoldlineExists" class="MarginBottom" visible="false">No foldline Exist</div>
									<asp:Button runat="server" Text="Add FoldLine"     CssClass="NavBtn marginBottom10 marginRight5" ID="btnAddnewFoldLine" OnClick="btnAddnewFoldLine_clicked"/>
									<div class="DivPageRowFoldLines" runat="server" id="divAddFoldLine" visible="false">
										Offset:
										<asp:TextBox runat="server" Width="40px" ID="txtBoxOffsetFromMarginNew"  CssClass="text_box200 rounded_corners5" Text="0"></asp:TextBox>		
										Orientation: 
										<asp:DropDownList runat="server" ID="dropDownOrientationNewFold" Width="98px" CssClass="text_box200 rounded_corners5" ></asp:DropDownList>
										<asp:Button runat="server" Text="Add"  CssClass="NavBtn marginBottom10 marginRight5 rounded_corners" Width="50px"  ID="btnSaveNewFoldLine" OnClick="btnSaveNewFoldLine_click"/>
										<%--<asp:Button runat="server" Text="Cancel"  CssClass="NavBtn marginBottom10 marginRight5 rounded_corners" Width="50px"  ID="btnCancelFoldLineClick" OnClick="btnCancelFoldLineClick_click" />--%>
										<asp:ImageButton runat="server" ImageUrl="~/assets/PageDelete.png" ID="btnDeleteFoldLine"
													Height="22px" Width="22px" CssClass="maginbtm8"  OnClick="btnCancelFoldLineClick_click"
													OnClientClick="return confirm('are you sure to Cancel adding this fold line?')" ToolTip="Cancel Adding New FoldLine" />	
									</div>
								</div>
								<div class="clearBoth">
								</div>
								<br />
							</div>
					
					<div runat="server" id="lblNoCategories" class="MarginBottom" visible="false">No Category Exist</div>
                      
				</div>
				

				<div class="clear">
				</div>
			</div>
  
	</div>

	</ContentTemplate>

	</asp:UpdatePanel>

	</form>
   
    

</body>
</html>
