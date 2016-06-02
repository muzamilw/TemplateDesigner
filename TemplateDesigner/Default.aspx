<%@ Page Title="" Language="C#" MasterPageFile="~/TemplateDesignerEmpty.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TemplateDesigner.designersl" %>

<%@ Register Src="SLDesigherControlWrapper.ascx" TagName="SLDesigherControlWrapper"
    TagPrefix="uc1" %>
<%--    <%@ OutputCache Duration="3600" VaryByParam="None" Location="Any" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.17.custom.css" rel="stylesheet" />
     <script type="text/javascript" src="splashscreen.js"></script>
    <script type="text/javascript" src="Silverlight.js"></script>
     <script src="Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.17.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }
            throw new Error(errMsg);
        }
    </script>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <style type="text/css">
       
       html, body, form {width:100%; height:100%; margin:0; padding:0;overflow:hidden;}
 
        
        #sldesignerdiv
        {
            width: 100%;
            height: 100%;
            text-align: center;
           
        }
        
         #previewdiv
        {
             width: 100%;
            height: 100%;
             z-index:99999;
            
            
        }
    </style>
    <%--<div id="accordionResizer" style="padding: 15px; width: 95%; height: 100%;" class="ui-widget-content">--%>
    <%-- <div id="accordion">
            <h3>
                <a href="#">Designer</a></h3>--%>
    <div id="sldesignerdiv">
       
            <object data="data:application/x-silverlight-2," type="application/x-silverlight-2"
                width="100%" height="100%">
                <param name="source" value="designer.xap" />
                <param name="onError" value="onSilverlightError" />
                <param name="background" value="white" />
                <param name="minRuntimeVersion" value="4.0.50401.0" />
                <param name="autoUpgrade" value="true" />
                <param name="windowless" value="true" />
                <param name="initParams" value="xp=2" />
               <%-- <param name="EnableGPUAcceleration" value="true" />--%>
                  <param name="splashscreensource" value="SplashScreen.xaml" />
            <param name="onSourceDownloadProgressChanged" value="onSourceDownloadProgressChanged" />
                <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50826.0" style="text-decoration: none">
                    <img src="http://go.microsoft.com/fwlink/?LinkId=161376" alt="Get Microsoft Silverlight"
                        style="border-style: none" />
                </a>
            </object>
            <iframe id="_sl_historyFrame" style="visibility: hidden; height: 0px; width: 0px;
                border: 0px"></iframe>
       
    </div>
    
    <div id="previewdiv" Title="PDF Preview">
        <iframe name="previewframe" id="previewframe" src="blankpreview.htm" width="100%" frameborder="0" height="100%" scrolling="auto"></iframe>
    </div>
    <%--<h3>
                <a href="#"></a>
            </h3>
            
            <span class="ui-icon ui-icon-grip-dotted-horizontal" style="margin: 2px auto;"></span>
        </div>--%>
    <%--</div>--%>
    <!-- End accordionResizer -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ClientScripts" runat="server">
   
    <script type="text/javascript">
        jQuery(document).ready(function () {

            //$('#sldesignerdiv').show();
            //$('#previewdiv').hide();

        });

        function switchToPreview(side) {
            //            $("#accordion").accordion("activate", 1);
            $("#previewdiv").dialog("open");
            if (side == 1)
                var newurl = 'PdfPreview.aspx';
            else if (side == 2)
                var newurl = 'PdfPreview.aspx?ViewSide=2';

            $('#previewframe').attr('src', newurl);


           
            return false;
        }

        function BackToTemplateDetails(TemplateID) {
            //window.location = 'EditTemplate.aspx?mode=edit&templateid=' + TemplateID;
        }

        $.fx.speeds._default = 500;
        $(function () {
            $("#previewdiv").dialog({
                autoOpen: false,
                width: "1000",
                height: "750",
                position: "center",
                zIndex: "3999",
                modal: true,
                stack:true
            });

         
        });



        function switchToDesigner() {

            //$('#sldesignerdiv').show();
            $("#previewdiv").dialog("close");
        }


        //        jQuery(document).ready(function () {
        //            $('.accordion .head').click(function () {
        //                $(this).next().toggle();
        //                return false;
        //            }).next().hide();
        //        });
        //        $(function () {
        //            $("#accordion").accordion({
        //                fillSpace: true
        //            });
        //        });
        //        $(function () {
        //            $("#accordionResizer").resizable({
        //                minHeight: 880,
        //                resize: function () {
        //                    $("#accordion").accordion("resize");
        //                }
        //            });
        //        });


        //        function switchToPreview(side) {
        //            $("#accordion").accordion("activate", 1);

        //            if (side == 1)
        //                var newurl = 'PdfPreview.aspx';
        //            else if (side == 2)
        //                var newurl = 'PdfPreview.aspx?ViewSide=2';

        //            $('#previewframe').attr('src', newurl);
        //            //window.frames["previewframe"].location.reload();
        //        }


        $.maxZIndex = $.fn.maxZIndex = function (opt) {
            /// <summary>
            /// Returns the max zOrder in the document (no parameter)
            /// Sets max zOrder by passing a non-zero number
            /// which gets added to the highest zOrder.
            /// </summary>    
            /// <param name="opt" type="object">
            /// inc: increment value, 
            /// group: selector for zIndex elements to find max for
            /// </param>
            /// <returns type="jQuery" />
            var def = { inc: 10, group: "*" };
            $.extend(def, opt);
            var zmax = 0;
            $(def.group).each(function () {
                var cur = parseInt($(this).css('z-index'));
                zmax = cur > zmax ? cur : zmax;
            });
            if (!this.jquery)
                return zmax;

            return this.each(function () {
                zmax += def.inc;
                $(this).css("z-index", zmax);
            });
        }

    </script>
</asp:Content>
