<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="uploadfileHandler.aspx.cs" Inherits="TemplateDesigner.html5Designer.uploadfileHandler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample file upload</title>


    <script src="js/plupload/jsapi.js" type="text/javascript"></script>
    <script src="js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="js/plupload/xdev.plupload.js" type="text/javascript"></script>
    <script src="js/plupload/plupload.full.min.js" type="text/javascript"></script>
    <script src="js/plupload/jquery.plupload.queue.min.js" type="text/javascript"></script>
    <link href="js/plupload/plupload.queue.css" rel="stylesheet" type="text/css" />


</head>
<body>
      <form id="form1" runat="server" >
      <input type="hidden" name="productid"  value="998"/>
      <input type="hidden" name="filetype"  value="2"/>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>

        <div id="uploader" style="width: 450px;">
            <p>You browser doesn't have Flash, Silverlight, Gears, BrowserPlus or HTML5 support.</p>
        </div>
        <br />     
        <div style="width: 150px; margin-right: 490px;">
            <a href="#" onclick="SubmitItem(this)">Submit Item</a>
        </div>
   
    </form>
</body>
</html>
