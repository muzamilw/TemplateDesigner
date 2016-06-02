<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TemplateDesignerV2.nav._default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../styles/navStyles.css" rel="stylesheet" type="text/css" />
    <link href="../styles/jquery.rating.css" rel="stylesheet" type="text/css" />
    <script src="../js/p55-17.js" type="text/javascript"></script>
    <script src="../js/p40.js" type="text/javascript"></script>
    <script src="../js/p90.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            /* version 1 */
            $('img.templateImage').error(function () {
                $(this).attr({
                    src: '../assets/preview-not-available.jpg',
                    alt: 'Sorry!  This image is not available!'

                });
            });
            $(".rating").rating({ showCancel: false, disabled: true });

        });


        function createNew() {
            window.location.href = "EditTemplate.aspx?mode=new";
            return false;
        }
    </script>
</head>
<body class="bodybg">
    <form id="form1" runat="server">
    <div id="contentArea">
        <div id="helpbox" class="rounded_corners">
            <img src="../assets/logo.png" />
                <br />
            <div class="logoContainer">
                <asp:ImageButton ID="btnEditCat" title="Create and Edit Template Categories" runat="server"
                OnClick="clickCreateCatBtn" ImageUrl="~/assets/CreateCategories.png" />
            </div>
            <div class="logininfo">
                <asp:Label ID="lblUser" runat="server" Text="Label"></asp:Label>
                <asp:LoginView ID="LoginView1" runat="server">
                    <LoggedInTemplate>
                        <asp:LoginStatus ID="LoginStatusDefault" runat="server" />
                        <br />
                    </LoggedInTemplate>
                </asp:LoginView>
                <span>Home : My Template Library</span>
            </div>
        </div>
        <div id="NavBar" class="rounded_corners" style="height: 60px;">
            
            <asp:ImageButton ID="btnCreate" runtat="server" OnClick="clickCreateTemplateBtn"
                title="Create New Template" runat="server" ImageUrl="~/assets/create.png" />
            <div id="searchctls">
                <label class="searchlabelctl">
                    Region
                </label>
                <asp:DropDownList ID="cmbRegions" runat="server" CssClass="searchinput" OnSelectedIndexChanged="cmbType_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
                <div id="divCategoryType" runat="server">
                    <label class="searchlabelctl" style="width: 44px;">
                        Type
                    </label>
                    <asp:DropDownList ID="cmbType" runat="server" CssClass="searchinput" OnSelectedIndexChanged="cmbType_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="clear spacer">
                </div>
                <label class="searchlabelctl">
                    Category
                </label>
                <asp:DropDownList ID="cmbCategories" runat="server" CssClass="searchinput">
                </asp:DropDownList>
                <label class="searchlabelctl" style="display: none;">
                    Status
                </label>
                <asp:DropDownList ID="cmbStatus" runat="server" CssClass="searchinput" Style="display: none;">
                </asp:DropDownList>
                <label class="searchlabelctl">
                    Keyword
                </label>
                <asp:TextBox ID="txtKeyword" runat="server" CssClass="searchinput"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="searchinput" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div class="catNamecontainer"> <span> Showing Template Category : <asp:Label ID="lblCategoryName" CssClass="boldFont" runat="server"></asp:Label> </span> </div>
        <div id="pager">
            <asp:Button ID="btnFirstPage" runat="server" Text="First" OnClick="BtnFirstPageClick"
                Visible="false" />
            <asp:Button ID="btnPrviousPage" runat="server" Text="Previous" OnClick="BtnPreviousPageClick"
                Visible="false" />
            <asp:Label ID="lblPager" runat="server"></asp:Label>
            <asp:Button ID="btnNextPage" runat="server" Text="Next" OnClick="BtnNextPageClick"
                Visible="false" />
            <asp:Button ID="btnLastPage" runat="server" Text="Last" OnClick="BtnLastPageClick"
                Visible="false" />
        </div>
         <div class="clear spacer">
                </div>
        <div id="grid" class="rounded_corners">
            <asp:Repeater ID="listTemplates" runat="server" OnItemCommand="listTemplates_ItemCommand"
                OnItemDataBound="listTemplates_ItemDataBound">
                <ItemTemplate>
                    <div class="templateBox border_div rounded_corners">
                        <div class="templateImageBox rounded_corners">
                            <asp:HyperLink runat="server" NavigateUrl='<% #Eval("ProductID","~/nav/EditTemplate.aspx?TemplateId={0}&mode=edit") %>'
                                BorderWidth="0">
                                <asp:Image runat="server" CssClass="templateImage" ImageUrl='<% #Eval("Thumbnail") %>' />
                                <div class="iconcontainer">
                                    <asp:Image ID="imgIcon" runat="server" CssClass="templateIcon" ImageUrl='<% # PubPrivIcon(Eval("IsPrivate")) %>'
                                        Visible="false" />
                                </div>
                            </asp:HyperLink>
                        </div>
                        <div class="LblProductNameClass">
                            <asp:Label ID="LblProductName" runat="server" Text='<% #Eval("productname") %> '></asp:Label>
                            <%--<asp:HyperLink ID="HyperLink1" runat="server" class="templateImage"  NavigateUrl='<% #Eval("ProductID","~/designer.aspx?TemplateID={0}&mode=edit") %>' >...</asp:HyperLink>--%>
                        </div>
                        <div class="divbtnCopyTemplate">
                            <asp:Label ID="lblRating" CssClass="displayNone" runat="server" Text='<% #Eval("MPCRating") %> '></asp:Label>
                            <label class="label ratingControlLabel" style="width: 98px; margin-left: 2px;  float:left;">
                                Template Rating :
                            </label>
                            <select class="rating input keepwhitespace" id="ratingControl" runat="server" style="width: 50px; float:left;">
                                <option value="0">Template Rating </option>
                                <option value="1">Template Rating </option>
                                <option value="2" selected="selected">Template Rating</option>
                                <option value="3">Template Rating</option>
                                <option value="4">Template Rating</option>
                            </select>
                            <asp:ImageButton ID="btnCopyTemplate" runat="server" Text="Copy" CommandName="Copy"
                                CommandArgument='<% #Eval("ProductID") %> ' ImageUrl="~/assets/copy.png" Height="24"
                                Width="24" AlternateText="Click here to copy " CssClass="btnCopyTemplate" />
                        </div>
                    </div>
                    <div id="divClear" runat="server">
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div runat="server" id="msgBox" class="msgbox">
                your search criteria did not match any templates.
            </div>
        </div>
    </div>
    </form>
</body>
</html>
