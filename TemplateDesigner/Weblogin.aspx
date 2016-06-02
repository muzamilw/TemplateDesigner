<%@ Page Title="" Language="C#" MasterPageFile="TemplateDesignerFull.Master" AutoEventWireup="true" CodeBehind="Weblogin.aspx.cs" Inherits="TemplateDesigner.Weblogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Titlecontent" runat="server">
    Login
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     
                                <table border="0"  cellpadding="0" cellspacing="0" 
                                    style="border-collapse: collapse" width="100%">
                                    <tr>
                                        <td align="center" valign="top" width="78%">
                                         
                                            <table border="0" bordercolor="#cccccc" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="right" class="generaltxt" height="30" valign="bottom">
                                                    </td>
                                                    <td align="left" class="normaltxt" valign="bottom">
                                                        <asp:Label ID="lblError" runat="server" 
                                                            EnableViewState="False" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="generaltxt" height="30">
                                                        
                                                            Email/ User name:
                                                    </td>
                                                    <td align="left" class="normaltxt" valign="bottom">
                                                       
                                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="input" Width="150px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="generaltxt" height="30" >
                                                        Password:</td>
                                                    <td align="left" class="normaltxt" valign="bottom">
                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input" 
                                                            TextMode="Password" Width="150px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="generaltxt" valign="bottom">
                                                        &nbsp;</td>
                                                    <td align="left" class="generaltxt" valign="bottom">
                                                        <asp:Button ID="btnLogin" runat="server" CssClass="button" 
                                                            OnClick="btnLogin_Click" Text="Login" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="generaltxt" height="40" valign="bottom">
                                                        &nbsp;</td>
                                                    <td align="left" class="generaltxt" valign="top">
                                                        <!--&nbsp;<a href="forgotpassword.aspx">Forgot Your Username or Password?</a></td> -->
                                                </tr>
                                                <tr>
                                                    <td align="left" class="generaltxt" height="40" valign="bottom">
                                                        &nbsp;</td>
                                                    <td align="left" class="generaltxt" valign="top">
                                                    </td>
                                                </tr>
                                            </table>
                                            <p class="generaltxt">
                                                &nbsp;</p>
                                            <p>
                                                <font color="#1e5cbe" face="Arial" size="4">
                                                <br />
                                                </font>
                                            </p>
                                        </td>
                                    </tr>
                                </table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ClientScripts" runat="server">
</asp:Content>
