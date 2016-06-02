<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFPreview.aspx.cs" Inherits="TemplateDesigner.PDFPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
         html, body, form {width:100%; height:100%; margin:0; padding:0;overflow:hidden;}
        Body
        {
            font-family: Arial;
            margin: 5px;
            padding: 0px;
        }
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td style="Padding:20px;">
                <asp:Label ID="lblView" runat="server" Text=""></asp:Label>
            </td>
            <td align="right" style="Padding:20px;">
                
                <asp:Button ID="btnSide" runat="server"  Text="Button"  OnClick="btnSide_Click" />&nbsp;
                <asp:Button ID="Button1" runat="server" Visible="false" OnClientClick="parent.switchToDesigner();return false;"  Text="Switch to Designer" />
            </td>
        </tr>
       
        <tr>
            <td colspan="2" align="center">
                <iframe height="540px" width="97%" src='ViewPDF.aspx<%if(Request.QueryString["ViewSide"] != null) Response.Write("?ViewSide="+Request.QueryString["ViewSide"]); %>'
                    id="I1" name="I1" frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
