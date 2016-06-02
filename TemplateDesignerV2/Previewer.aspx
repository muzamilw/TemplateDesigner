<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Previewer.aspx.cs" Inherits="TemplateDesignerV2.Previewer" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Full Screen Image</title>
    <script src="js/p55.js"></script>

    <link href="styles/DesignerStyleSheet.css" rel="stylesheet" />
</head>
<body style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 100%;height: 100%;border: none; ">
     <img src="" style="margin:auto;" alt=""  class="imgPreviewer"/>
    <script>        
        function getParam( name )
        {
            name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
            var regexS = "[\\?&]"+name+"=([^&#]*)";
            var regex = new RegExp( regexS );
            var results = regex.exec( window.location.href );
            if( results == null )
                return "";
            else
                return results[1];
        }
        $(document).ready(function () {
            var pId = getParam("pID");
            var tId = getParam("tId");
            $(".imgPreviewer").attr("src", "designer/products/"+ tId + "/"+pId);
        });
    </script>
</body>
</html>
