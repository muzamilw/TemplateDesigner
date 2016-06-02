<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="weblogin.aspx.cs" Inherits="TemplateDesignerV2.nav.weblogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MyPrintCloud - My Template Library</title>
    <link href="../styles/navStyles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 100px 0 0 0px;">
        <div id="login-box">
            <h2>
                My Template Library</h2>
           
            <div class="clear"></div>
            <br />
            <br />
            <div id ="divLoginBoxContainer" runat="server">
                <div id="login-box-name" style="margin-top: 20px;">
                    Username:</div>
                <div id="login-box-field" style="margin-top: 20px;">
                
                    <asp:TextBox ID="txtUsername" runat="server" class="form-login" title="Username" value="" size="30" maxlength="100"></asp:TextBox>
                
                
                    </div>
                <div id="login-box-name" >
                    Password:</div>
                <div id="login-box-field">
               
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-login" title="Password" value="" size="30"
                        maxlength="100"></asp:TextBox>
                    
                        </div>
                <br />
                <span class="login-box-options">
               
                
                     <asp:Label ID="lblMessage" runat="server"></asp:Label></span>
                <br />
                <br />
                <asp:ImageButton ID="btnLogin" runat="server" Text="Login" ImageUrl="../assets/login-btn.png"  OnClick="btnLogin_Click" style="margin-left: 90px;" />
                    <img id="mpcloginlogo" src="../assets/logo.png" />
            </div>
            <div id="divInvalidParameteredLogin" runat="server" visible="false" class="login-box-name " style="text-align:left;">
               Your login session has expired. <br /> 
                Please relogin in MIS and then access the template libaray. 
                
            </div>
        </div>
    </div>
    
    </form>
</body>
</html>
