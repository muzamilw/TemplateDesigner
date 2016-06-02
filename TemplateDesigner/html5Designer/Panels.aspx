<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Panels.aspx.cs" Inherits="TemplateDesigner.html5Designer.Unndo_Redo" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Kitchensink | Fabric.js Demos</title>
 <script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>
 <script type="text/javascript" src="js/jquery-ui-1.8.18.custom.min.js"></script>
    <link type="text/css" href="styles/smoothness/jquery-ui-1.8.18.custom.css" rel="stylesheet" />	
    <script src="js/helperFunctions.js" type="text/javascript"></script>
  
    <link rel="Stylesheet" type="text/css" href="styles/DesignerStyleSheet.css" />
	<script  type="text/javascript" src="js/fabric.js"></script>
	<script src="js/PanelTransitions.js" type="text/javascript"></script>
    <%--<link rel="stylesheet" href="styles/kitchensink.css" />--%>
	<%--<script src="js/Loader.js" type="text/javascript"></script>--%>
    <script src="js/jquery.corner.js" type="text/javascript"></script>
    <script src="js/animatedcollapse.js" type="text/javascript"></script>
   <%-- <link href="styles/PageLoader.css" rel="stylesheet" type="text/css" />--%>

   
       <script type="text/javascript">
           animatedcollapse.addDiv('addText', 'fade=0,speed=400,group=panel, hide=1');
           animatedcollapse.addDiv('addImage', 'fade=1,speed=400,group=panel,persist=0,hide=1');
           animatedcollapse.addDiv('quickText', 'fade=1,speed=400,persist=0,hide=1');
           animatedcollapse.addDiv('UploadImage', 'fade=1,speed=400,persist=0,hide=1');
           animatedcollapse.addDiv('ImagePropertyPanel', 'fade=1,speed=400,group=Propertypanel,persist=0,hide=1');
           animatedcollapse.addDiv('ShapePropertyPanel', 'fade=1,speed=400,group=Propertypanel,persist=0,hide=1');
           animatedcollapse.addDiv('textPropertPanel', 'fade=1,speed=400,group=Propertypanel,persist=0,hide=1');       
           animatedcollapse.init();

           $(function () {
               $("input:submit, button", ".designer").button();
               $("#QuickTextButton").button();
               $("a", ".demo").click(function () { return false; });
           });

           $(document).ready(function () {
               $("#addText").corner("7px;");
               $("#addImage").corner("7px;");
               $("#quickText").corner("7px;");
               $("#UploadImage").corner("7px;");
               $("#addTextArea").corner("7px;");
               $("#ImagePropertyPanel").corner("7px;");
               $("#ShapePropertyPanel").corner("7px;");
               $("#textPropertPanel").corner("7px;");
               
           }
	    );

           $(function () {
               $("#RedoJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-arrowreturnthick-1-w"
                   }
               });
               $("#UndoJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon ui-icon-arrowrefresh-1-e"
                   }
               });
             
               $("#drawLineJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#drawPencilJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon ui-icon-pencil"
                   }
               });
               $("#addRectJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#addCircleJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon ui-icon-radio-on"
                   }
               });
               $("#alignleftJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#alignCenterJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#alignRightJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#alignObjLeftJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#alignObjCenterJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#alignObjRightJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#GuidesJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-seek-prev"
                   }
               });
               $("#ZoomInJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon ui-icon-circle-zoomin"
                   }
               });

               $("#zoomOutJqueryButton").button({
                   text: false,
                   icons: {
                       primary: "ui-icon-circle-zoomout"
                   }
               });
         

           });

    </script>

       
</head>


<body>

<div id="Container" >
     <!-- code for the panels task  --> 
       <!-- div containing the designer  --> 
   <div id= "designer" class = "designer">
 
   <!-- div containing the main menu  --> 
       <div id="toolbar" class="ui-corner-all">
           <div class= "CaptionText">
               <span id ="RedoSpan"> Redo</span>
               <span id ="UndoSpan">Undo</span>
               <span id ="DrawSpan">Draw</span>
               <span id ="AlignObjectsSpan">Align Objects</span>
               <span id ="GuidesSpan">Guides</span>
       </div>
	       <div class="JqueryMenuItem">
                <button id="RedoJqueryButton">Redo</button>
	            <button id="UndoJqueryButton">Undo</button>
            </div>
           <div class="Seperator"><a href=""></a></div>
           <div class="JqueryMenuItem">
                <button id="addTxtJqueryButton" onclick="animatedcollapse.toggle('addText');animatedcollapse.hide('UploadImage');animatedcollapse.hide('quickText');">
            <span class="">Add text</span>
             </button>
                <button id="AddImgJqueryButton"  onclick="animatedcollapse.toggle('addImage');animatedcollapse.hide('UploadImage');animatedcollapse.hide('quickText');">
                Add Image
          </button>
	            <button id="drawLineJqueryButton" onclick="animatedcollapse.toggle('ImagePropertyPanel');DisplayDiv('1'); ">draw Line</button>
	            <button id="drawPencilJqueryButton" onclick="animatedcollapse.toggle('ImagePropertyPanel');DisplayDiv('2');">Pencil Tool</button>
	            <button id="addRectJqueryButton" onclick="animatedcollapse.toggle('textPropertPanel');">add Rectangle</button>
	            <button id="addCircleJqueryButton">add Circle</button>
        </div>
           <div class="Seperator"><a href=""></a></div>
           <div class="JqueryMenuItem">
            <button id="alignleftJqueryButton">align Left</button>
	        <button id="alignCenterJqueryButton">align Center</button>
	        <button id="alignRightJqueryButton">alignRight</button>
            <button id="alignObjLeftJqueryButton">align Object Left</button>
	        <button id="alignObjCenterJqueryButton">align Object Center</button>
            <button id="alignObjRightJqueryButton">align Object Right</button>
	    </div>
           <div class="Seperator"><a href=""></a></div>
           <div class="JqueryMenuItem">
            <button id="GuidesJqueryButton">Guides</button>
	        <button id="ZoomInJqueryButton">Zoom In</button>
             <button id="zoomOutJqueryButton">Zoom Out</button>
	    </div>
	 
    </div>

         

     <!-- div containing sub menus  --> 
             <div id="SubNavList">
              <div id="Div4">Sub Menu</div>
              <div class="SpaceSubNav"></div>
              <div id="Div5"><a href=""></a></div>
              <div class="SpaceSubNav"></div>
              <div id="Div6"><a href=""></a></div>
              <div class="SpaceSubNav"></div>
              <div id="Div7" onclick="animatedcollapse.toggle('addText');animatedcollapse.hide('UploadImage');animatedcollapse.hide('quickText');"><a href=""></a></div>
              <div class="SpaceSubNav"></div>
              <div id="Div8"  onclick="animatedcollapse.toggle('addImage');animatedcollapse.hide('UploadImage');animatedcollapse.hide('quickText');"><a href=""></a></div>
              <div class="SpaceSubNav"></div>
     
        </div>
    <!-- div containing the floating panels item and canvas --> 
    <div id ="CanvasContainer">
     <div id= "panels">    
      <div  id = "addText" class= "panelBasics"  >
    
        <div class="panelItemsLeftAligned">
             <label for="addTextArea" id = "lblAddTxtArea"class="largeText" >Enter Your Text</label> 
              <div id="QuicktxtDiv">
          <label  class="largeText QuickTxtLbl">Click to Insert quick text fields into Canvas </label><br />
            <button  class="quickTextRadioButton" id="QuickTextButton"  onclick="animatedcollapse.toggle('quickText')"  >Add Quick Text</button>
         </div> 
        </div>
        <div class="panelItemsLeftAligned">
       
            <textarea id = "addTextArea" rows= "7" cols = "20" ></textarea>
        </div>
        <div class="panelItemsLeftAligned">
            <div class="BlockPanelButton" id= "SearchTextPanelButton"   onclick="" ></div>
            <button class="BlockPanelButton" id= "AddTextPanelButton"   onclick="" >Add</button>
        </div>
        <div class="panelItemsRightAligned">
            <div class="closePanelButton"   onclick="animatedcollapse.hide('addText');animatedcollapse.hide('quickText');" > <br /></div>      
          
        </div>     
      </div>
    
      <div id = "addImage"  class= "panelBasics">
         
            <div class="panelItemsLeftAligned">
            <div class="BlockPanelButton" id= "LblImgbank">
                <label for="ImageBank" class ="largeText">Image bank </label> 
            </div>
              
            <button class="BlockPanelButton" id= "UploadImgBtn"   onclick="animatedcollapse.toggle('UploadImage')" >Upload Image(s)</button>
           
        </div>

        <div class="panelItemsLeftAligned">
             <div class="BlockPanelButton" id= "ImageBank" >
                 <div style= "width: 600px;"> this is a dummy text to see if this div is scrollable or not ..... </div>
             </div>
             <div class="BlockPanelButton" id= "ImgBnkLbl">
                <label for="ImageBank" >Drag and drop images onto Canvas </label> 
             </div>
            
        </div>
        <div class="panelItemsLeftAligned" id="LblLogo"> 
             <label  class="largeText">Logo:  </label> 
        </div>
        <div class="panelItemsLeftAligned" id="Div1"> 
             <div class="LogoImg"  ></div>
              <div class="BlockPanelButton" id= "Div2">
                <label for="LogoImg" >Change </label> 
             </div>
        </div>
        <div class="panelItemsRightAligned">
              <div class="closeImgPanelButton"   onclick="animatedcollapse.hide('addImage');animatedcollapse.hide('UploadImage');" ></div>
        </div>
      </div>

      <div id = "quickText" class="popUpQuickTextPanel">
     
            <div class="panelItemsRightAligned">
            <div class="closePanelButtonQuickText"   onclick="animatedcollapse.hide('quickText')" > <br /></div>

        </div>  
            <label for="addQuickText" >Drag and Drop any, or all, of your quick text fields below</label>
            <div class="QuickTextText"><br />Your Name</div>
            <div class="QuickTextText">Your title</div>
            <div class="QuickTextText">Your company name</div>
            <div class="QuickTextText">Your comapny message</div>
            <div class="QuickTextText">Address 1</div>
            <div class="QuickTextText">Tel/ /other</div>
            <div class="QuickTextText">Fax / other</div>
            <div class="QuickTextText">Email address</div>
            <div class="QuickTextText">Website/other</div>
            <div class="QuickTextAllBtn"><br  />Insert all fields at once</div>
      </div>

      <div id="UploadImage" class="popUpUploadImgPanel">
           <div class="panelItemsRightAligned">
            <div class="closePanelButtonQuickText"   onclick="animatedcollapse.hide('UploadImage')" > <br /></div>

        </div>  
           
           <div class="panelItemsLeftAligned">  
                <button class="BrowseImgBtn"  >Browse Image(s)</button>
           </div>
           <div class="panelItemsRightAligned">
                 <button class="UploadImgFileBtn">Upload </button>      
      </div>
           <div class= "panelItemsLeftAligned" id="UploadImgFileStatus">
              <div class="UploadImgFileStatusimg">   </div>           
           </div>
           <div class= "panelItemsLeftAligned" >
           <br />
           <div class="orangeTxt">Important</div> .  For best results, we recomment you upload all images using: <br />
           <ul>
                <li>
                    High resolution <div class="orangeTxt">JPEG </div> format
                </li>
                <li>
                    <div class="orangeTxt">300dpi </div>
                </li>
           </ul>    
           
           If uplading a full background, allow a bleed of 5 mm for trimming- <br />
           for business card that a canvas size of 95 mm x 65 mm or 1122 x 758 pixels. <br /> <br />
           We also support Gif, PNG ,and SVG images.

           </div>

   </div>
  
      <div id = "ImagePropertyPanel"  class= "panelBasics">
         
            <div class="firstPanel">
            
                <label class ="largeText">Image Properties</label> 
                <label class="largeText"  id = "ArangeOrderLbl">Arrange Order</label>
                <button id="arrangeOrder1" ></button>
                <button id="arrangeOrder2" ></button>
                <button id="arrangeOrder3" ></button>
                <button id="arrangeOrder4" ></button>
          
        </div>
            
            <div class ="middlePanel" id= "middlePanelImage">
                <button>Change Image</button>
                <div id= "Subpanel">               
                 <label class="largeText"> Crop</label>
                 <label class="largeText" id="FlipLbl"> Flip</label>
                 <div id="CropFlipDiv1">
                    <button id= "cropImgBtn"></button>
                    <button id= "FlipImgBtn1"></button>
                    <button id= "FlipImgBtn2"></button>
                 </div>
 
                </div>

            </div>
              <div class ="middlePanel" id="middlePanelShape">
                
                <div id= "Subpanel1">               
                 <label class="largeText"> Opacity</label>
                 <label class="largeText" id="FlipLbl1"> Fill</label>
                 <div id="CropFlipDiv">
                    <button id= "Button5"></button>
                    <button id= "Button6"></button>
                    <button id= "Button7"></button>
                 </div>
 
                </div>

            </div>
            <div class= "lastPanel">
            <button id="DeleteObj">Delete Object</button>
              <input type="checkbox" id="LockPosition"/>
            <label for="LockPosition" class="largeText">Lock Position</label>
            <button class="closeImgPanelButton"   onclick="animatedcollapse.hide('addImage');animatedcollapse.hide('ImagePropertyPanel');" ></button>
            <div class = "ScaleAndRotateImgPropertyDiv">
                <label class="largeText">Scale</label>
                <label class="largeText" id = "RotateLbl">Rotate</label>
                <input type="checkbox" id="PrintObjRadioBtn"/>
                <label for="PrintObjRadioBtn">Do not PRINT this Object</label>
                <div id="ScaleButtons">
                    <button id = "ScaleIN"></button>
                    <button id = "ScaleOut"></button>
                    <button id = "RotateLeft"></button>
                    <button id = "RotateRight"></button>
                </div>
            </div>
            </div>
 
    
      </div>


       <%--<div id = "ShapePropertyPanel"  class= "panelBasics">
         
            <div class="firstPanel">
            
                <label class ="largeText">Shape Properties</label> 
                <label class="largeText"  id = "ArangeOrderLbl1">Arrange Order</label>
                <button id="arrangeOrder5" ></button>
                <button id="arrangeOrder6" ></button>
                <button id="arrangeOrder7" ></button>
                <button id="arrangeOrder8" ></button>
          
        </div>
            
            <div class ="middlePanel" id="middlePanelShape">
                
                <div id= "Subpanel1">               
                 <label class="largeText"> Opacity</label>
                 <label class="largeText" id="FlipLbl1"> Fill</label>
                 <div id="CropFlipDiv">
                    <button id= "Button5"></button>
                    <button id= "Button6"></button>
                    <button id= "Button7"></button>
                 </div>
 
                </div>

            </div>
            <div class= "lastPanel">
            <button id="DeleteObj1">Delete Object</button>
              <input type="checkbox" id="LockPosition1"/>
            <label for="LockPosition1" class="largeText">Lock Position</label>
            <button class="closeImgPanelButton"   onclick="animatedcollapse.hide('addImage');animatedcollapse.hide('ShapePropertyPanel');" ></button>
            <div class = "ScaleAndRotateImgPropertyDiv">
                <label class="largeText">Scale</label>
                <label class="largeText" id = "RotateLbl1">Rotate</label>
                <input type="checkbox" id="PrintObjRadioBtn1"/>
                <label for="PrintObjRadioBtn1">Do not PRINT this Object</label>
                <div id="Div11">
                    <button id = "ScaleIN1"></button>
                    <button id = "ScaleOut1"></button>
                    <button id = "RotateLeft1"></button>
                    <button id = "RotateRight1"></button>
                </div>
            </div>
            </div>
 
    
      </div>--%>

          <div id = "textPropertPanel"  class= "panelBasics1">
         
            <div class="firstPanel">
            
                <label class ="largeText">Edit Text</label> 
                <label class="largeText"  id = "ArangeOrderLbl2">Arrange Order</label>
                <button id="arrangeOrder9" ></button>
                <button id="arrangeOrder10" ></button>
                <button id="arrangeOrder11" ></button>
                <button id="arrangeOrder12" ></button>
          
        </div>
            
            <div class ="middlePanel" id = "MiddlePanelTxt">
                <textarea id="EditTXtArea" rows = "4" cols = "24"></textarea>
                <button id= "SearchTxtBtn"></button>
                <div id= "Div12">               
               
                 <div id="BtnsTextProperty">
                    <button id= "Button1"></button>
                    <button id= "Button2"></button>
                    <button id= "BoldBtn"></button>
                    <button id= "ItalicBtn"></button>
                    <button id= "fillBtn"></button>
                 </div>
 
                </div>

            </div>
            <div class= "lastPanel">
            <button id="DeleteObj2">Delete Object</button>
              <input type="checkbox" id="LockPosition2"/>
            <label for="LockPosition2" >Lock Position</label>

            <button class="closeImgPanelButton"   onclick="animatedcollapse.hide('addImage');animatedcollapse.hide('textPropertPanel');" ></button>
            <div class = "ScaleAndRotateImgPropertyDiv">
                <label class="largeText">Justify</label>
                <label class="largeText" id = "RotateLbl2">Rotate</label>
                <input type="checkbox" id="PrintObjRadioBtn2"/>
                <label for="PrintObjRadioBtn2">Do not PRINT this Object</label>

                <div id="Div14">
                    <button id = "justifyTxt1"></button>
                    <button id = "justifyTxt2"></button>
                    <button id = "justifyTxt3"></button>
                    <button id = "RotateLeft2"></button>
                    <button id = "RotateRight2"></button>
                     <input type="checkbox" id="LockEditing"/>
                    <label for="LockEditing" >Lock Editing</label>
                </div>
            </div>
            </div>
 
    
      </div>

     </div>

        <canvas id="Canvas">
        Your browser does not support the canvas.Please upgrade to latest version
        </canvas>
</div>
   </div>
   
</div>
</body>
</html>
