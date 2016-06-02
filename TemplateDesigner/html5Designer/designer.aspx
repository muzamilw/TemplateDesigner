<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="designer.aspx.cs" Inherits="TemplateDesigner.designer" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Designer</title>
    <link rel="stylesheet" href="styles/master.css" />
    <script type="text/javascript" src="js/fabric.js"></script>
    <link type="text/css" href="styles/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.18.custom.min.js"></script>
    <link rel="stylesheet" href="styles/kitchensink.css" />
    <script src="js/Loader.js" type="text/javascript"></script>
    <link href="styles/PageLoader.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/PageLoader.js"></script>
    <script type="text/javascript">

        $(window).load(function () {
            StopLoader();
        });
        $(document).ready(function () {
            StartLoader();
        }
	    );

    </script>
    <!--[if lt IE 9]>
	  <script src="js/excanvas.js"></script>
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
</head>
<body>
    <!-- Loading Div -->
    <div id="shadow" class="opaqueLayer">
    </div>
    <div id="loading" class="loadingLayer">
        Loading content, please wait..
        <img src="assets/loading.gif" alt="loading.." />
    </div>
    <!-- Loading Div -->
    <div id="Container">
        <div id="bd-wrapper">
            <h2>
                <script type="text/javascript" src="js/fonts/Delicious_500.font.js"></script>
                <script src="js/Fonts/Arial_Narrow_400.font.js" type="text/javascript"></script>
                <div id="controls">
                    <button type="button" id="btnzoomin">
                        Zoom In</button>
                    <button type="button" id="btnzoomout">
                        Zoom out</button>
                    <button class="rect">
                        Rectangle</button>
                    <button class="circle">
                        Circle</button>
                    <button class="triangle">
                        Triangle</button>
                </div>
                 <div id="commands">
                    <input type="number" id="txtTemplateID" />
                    <button id="loadTemplate">
                        Load Template</button>
                  <button class="image1">
                                Image 1</button>
                            <button class="image2">
                                Image 2</button>
                              
                  
                  
                      
                            <button class="shape" id="shape25">
                                <strong>36</strong> paths</button></li>
                      
                 
                   
                            <button id="rasterize">
                                Rasterize canvas to image</button>
                            <button class="clear">
                                Clear canvas</button>
                            <button id="add-text">
                                Add text</button>
                      
                            <button id="remove-selected">
                                Remove selected object/group</button>
                      
                            <button id="lock-horizontally">
                                Lock horizontal movement</button>
                            <button id="lock-vertically">
                                Lock vertical movement</button>
                            <button id="lock-scaling-x">
                                Lock horizontal scaling</button>
                            <button id="lock-scaling-y">
                                Lock vertical scaling</button>
                            <button id="lock-rotation">
                                Lock rotation</button>
                    
                            <button id="gradientify">
                                Gradientify</button>
                      
                            <button id="drawing-mode">
                                Enter drawing mode</button>
                            <div style="display: none;" id="drawing-mode-options">
                                Width:
                                <input value="10" id="drawing-line-width" size="2" />
                                Color:
                                <input type="color" value="rgb(0,0,0)" id="drawing-color" size="15" />
                            </div>
                      
                 
                </div>
                 <canvas id="canvas" width="800" height="700">
                    </canvas>
                <div id="textEditor" style="position: absolute; width: 804px;left :1200px;">
                   
                    <div id="complexity">
                        Canvas complexity (number of paths):<strong></strong></div>
                    <div id="text-controls">
                        <textarea id="text" cols='3' rows='3'></textarea>
                        <div id="text-control-buttons">
                            <button type="button" id="text-cmd-italic">
                                Italic</button>
                            <button type="button" id="text-cmd-underline">
                                Underline</button>
                            <button type="button" id="text-cmd-linethrough">
                                Linethrough</button>
                            <button type="button" id="text-cmd-overline">
                                Overline</button>
                            <button type="button" id="text-cmd-shadow">
                                Shadow</button>
                            <label for="text-align" style="display: inline-block">
                                Text align:</label>
                            <select id="text-align">
                                <option>Left</option>
                                <option>Center</option>
                                <option>Right</option>
                            </select>
                            <label>
                                Bg color:
                                <input type="color" value="" id="text-bg-color" size="10" /></label>
                        </div>
                    </div>
                    <div>
                        <textarea rows='20' cols='30' id='output'></textarea>
                    </div>
                </div>
               
                <script type="text/javascript" src="js/DesignerMain.js"></script>
                <script type="text/javascript" src="js/font_definitions.js"></script>
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
    </div>
    <script type="text/javascript">        StopLoader();</script>
</body>
</html>
