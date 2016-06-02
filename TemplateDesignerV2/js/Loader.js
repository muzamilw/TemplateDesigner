$(document).ready(function () {
    StartLoader();
    initUIControls();
    InitData();
    InitColorSlider();

    //window.onbeforeunload = confirmExit;
});

// event to confirm any unsaved changes 
//function confirmExit() {
//    return "You have attempted to leave this page.  If you have made any changes to the fields without clicking the Save button, your changes will be lost.  Are you sure you want to exit this page?";
//}
// function to add style to tool tip while scrolling the page
$(window).scroll(function () {
	//$("#your_obj").center();

    // commented for making tips remember its position 
	$('.DivToolTipStyle').css('position', 'fixed');
	//$('.DivToolTipStyle').css("left", ($(window).width()/2 + 500  - $('.DivToolTipStyle').width() ) + "px");
	//$('.DivToolTipStyle').css("top", ($(window).height() / 2 - $('.DivToolTipStyle').height() / 2) + "px");
	$('.DivToolTipStyle').css("top", "125px");
	$('#textPropertPanel').css('position', 'fixed');
	$('.panelBasics').css('position', 'fixed');
	$('#DivColorPallet').css('position', 'fixed');
	$('#ImagePropertyPanel').css('position', 'fixed');
	$('#DivCropToolContainer').css('position', 'fixed');

	$('#textPropertPanel').css("top", "7px");
	$('.panelBasics').css("top", "245px");

	$('#DivColorPallet').css("top", "9px");
	$('#ImagePropertyPanel').css('top', '9px');
	$('#DivCropToolContainer').css("top", "9px");

    // fixing position of add image 
	$("#addImage").css('position', 'fixed');
	$('#addImage').css("top", "9px");

	$("#quickTextFormPanel").css('position', 'fixed');
	$('#quickTextFormPanel').css("top", "7px");

	$("#AddTextDragable").css('position', 'fixed');
	$('#AddTextDragable').css("top", "0px");
	//$(".popUpQuickTextPanel").css('position', 'fixed');
//	$('.popUpQuickTextPanel').css("top", "127px");
	$(".popUpAdvanceColorPicker").css('position', 'fixed');
	$('.popUpAdvanceColorPicker').css("top", "237px");

	$(".DivAlignObjs").css('position', 'fixed');
	$('.DivAlignObjs').css("top", "9px");
	//    // stciking main menu to top  DivAlignObjs
//	$('#toolbar').css("position", "fixed");
//	$('#toolbar').css('top', '8px');
//	$('#toolbar').css('z-index', '10000');
	//$('#SubNavList').css("position", "fixed");

	//$('#DivCropToolContainer').css("top", "175px");
});

// function to init Color Slider
function InitColorSlider() {

	$("#DivColorC,#DivColorM,#DivColorY, #DivColorK").slider({
		orientation: "horizontal",
		range: "min",
		max: 100,
		slide: sliderUpdate
	});

}






// Show a log of action
var logAction = function (msg) {
	$("#actions").append(
			$("<li>").html(msg)
	);
}

var IsEmbedded = true;   //two possibilities 1 - individual global designer 2 - embedded mode in webstore
var IsCalledFrom = 1; 
var TemplateID = 0;
var Template;
var TemplatePages = [];
var TemplateObjects = [];
var TemplateFonts = [];
var SelectedPageID = 0;
var SelectedPageNo = 0;
var NewControlID = -1;
var IsSnapToGrid = true;
var IsShowGuides = true;
var SnapXPoints = [];   //used for snap on X Axis
var SnapYPoints = [];   //used for snap on Y Axis
var QuickTextData = 0;
var CustomerID;     //webstore borrowed customerID for QuickText
var ContactID;     //webstore borrowed customerID for QuickText


function InitData() {
	// adding fonts to font drop dwb

	//loadFonts();
	$("#loadingMsg").html("Loading Design, please wait..");
	
	TemplateID = getParameterByName("TemplateID");   //getting template ID from the querystring if available, otherwise open the default template
	if (TemplateID != "") {
		loadTemplate(TemplateID);
	}
	else {
		loadTemplate(1545);  //1234
	}
	loadFontFace();

}
// for avoiding multiple calls
var imageCountIterator =0;
// function that shhould be called to add an image to carousel dynamically after adding that image to database 
function UpdateImagesToCarousel(Count) {
	//alert("iterat" + imageCountIterator);
	imageCountIterator += 1;
	if(Count == imageCountIterator)
	{

	    var oldSize = mycarousel_itemList.length;
	    var myCarousel_Items = []; //length
		$.getJSON("Services/imageSvc/" + TemplateID,
			function (data) {
			    var sizee = data.length;
			    $.each(data, function (i, item) {
			        var obj = {
			            url: "./" + item.BackgroundImageRelativePath,
			            title: item.ID,
			            index: TemplateID

			        }
			        myCarousel_Items.push(obj);

			    });
			    mycarousel_itemList = [];
			    mycarousel_itemList = myCarousel_Items;
			    // adding image to carousel 
			    for (var i = oldSize; i < data.length; i++) {
			        var width = 117 * data.length; ;
			        width = width + "px";
			        $("#CarouselImages").css("width", width);
			        $("#CarouselImages").append(mycarousel_getItemHTML(myCarousel_Items[i]));
			        $(".draggable2").draggable({
			            snap: '#dropzone',
			            snapMode: 'inner',
			            revert: 'invalid',
			            helper: 'clone',
			            appendTo: "body",
			            cursor: 'move'

			        });
			    }
			});
			animatedcollapse.toggle('addImage');	
	}	 
}

function InitUI() {
	//select Page 1 of Pages
	SelectedPageID = TemplatePages[0].ProductPageID;
	SelectedPageNo = TemplatePages[0].PageNo;
	PageChange(SelectedPageID);
	if (IsEmbedded) {
		togglePanels("quickTextFormPanel");
	}
	if (TotalImgCalls == TotalImgLoaded && LoadingImagesFirstTime) {
		StopLoader();
		LoadingImagesFirstTime = false;
	}
	//StopLoader();
	canvas.renderAll();
}

function initUIControls() {
	// loading msg change
	$("#loadingMsg").html("Loading UI, please wait..");
	
	// init panels 

	animatedcollapse.addDiv('addText', 'fade=0,speed=400,group=panel, hide=1');
	animatedcollapse.addDiv('addImage', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('quickText', 'fade=1,speed=400,persist=0,hide=1');
	animatedcollapse.addDiv('UploadImage', 'fade=1,speed=400, group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('ImagePropertyPanel', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('ShapePropertyPanel', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('textPropertPanel', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('quickTextFormPanel', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('DivToolTip', 'fade=1,speed=400,persist=0,hide=1');
	animatedcollapse.addDiv('DivUploadFont', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('DivColorPallet', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('DivAdvanceColorPanel', 'fade=1,speed=400,persist=0,hide=1');
	animatedcollapse.addDiv('DivCropToolContainer', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.addDiv('DivAlignObjs', 'fade=1,speed=400,group=panel,persist=0,hide=1');
	animatedcollapse.init();
	// init buttons 
	//$("input:submit, button", ".designer").button();
	$("input:submit, button", ".designer").button();
	//$("input:submit", ".designer").button();
	// mannual buttons initalization 
	$("#BtnSave").button();
	//$("#BtnPreview").button();
	$("#BtnContinue").button();
	$("#QuickTextButton").button();
	$("#uploadImgFileBtn1").button();
	$("#uploadFontFileBtn").button();
	
    $("a", ".demo").click(function () { return false; });
	//make tool tip display in center 
	$('.DivToolTipStyle').css('position', 'fixed');
	$('.DivToolTipStyle').css("left", ($(window).width() / 2 + 500 -8.5 - $('.DivToolTipStyle').width()) + "px");
	$('.DivToolTipStyle').css("top", "125px");

	$(".DivAlignObjs").css('position', 'fixed');
	$('.DivAlignObjs').css("top", "9px");
	// corner divs
	$(".DivAlignObjs").corner("7px;");
	$("#addText").corner("7px;");
	$("#addImage").corner("7px;");
	$("#quickText").corner("7px;");
	$("#UploadImage").corner("7px;");
	$("#addTextArea").corner("7px;");
	$("#ImagePropertyPanel").corner("7px;");
	$("#ShapePropertyPanel").corner("7px;");
	$("#textPropertPanel").corner("7px;");
	$("#quickTextFormPanel").corner("7px;");
	$("#AddColorShape").corner("7px;");
	
	$("#DivToolTip").corner("7px;");
	$("#DivToolTipHeader").corner("7px;");
	$("#DivUploadFont").corner("7px;");
	$("#DivColorPallet").corner("7px;");
	$(".ColorPallet").corner("7px;");
	$("#DivAdvanceColorPanel").corner("7px;");
	$("#DivColorPallet").corner("7px;");
	$("#DivCropToolContainer").corner("7px;");
	//$("#BtnGuides").corner("7px;");

	// setting label of tip based on cookies 
	if (getCookie("IsTipEnabled") == "0") {
	    $("#SpanTips").html("Show");  
	} else {
	    $("#SpanTips").html("Hide");
	}
	// make quick text fields draggable
	$(".QuickTextText").draggable({
		snap: '#dropzone',
		snapMode: 'inner',
		revert: 'invalid',
		helper: 'clone',
		appendTo: "body",
		cursor: 'move'
	});
    var is_chrome = /chrome/i.test(navigator.userAgent);
    if (is_chrome) {
        $("#BtnRotateTxtLft").css("margin-left", "12px");
        $("#BtnImgRotateLeft").css("margin-left", "12px");
        $("#txtQAddress").css("width", "186px");
        $("#BtnUpdateText").css("bottom", "25px");
       
    }
	// giving function to upload font for coorporate users
	if (IsCoorporate == 1) {
	    //$("#BtnUploadFont").css("display", "");
	    $("#BtnQuickTextForm").css("visibility", "hidden");
	} else {
	   // $("#BtnUploadFont").css("display", "none");
	}

	if (IsEmbedded == false) {
	    performCoorporate();
    }
	// making panels draggable
	$("#textPropertPanel").draggable({

		appendTo: "body",
		cursor: 'move'
	});
	$("#ImagePropertyPanel").draggable({

		appendTo: "body",
		cursor: 'move'
	});
	$("#DivColorPickerDraggable").draggable({

		appendTo: "body",
		cursor: 'move',
		cancel: "div #DivColorContainer"
	});
	$("#DivToolTip").draggable({

		appendTo: "body",
		cursor: 'move'
    });

    // making all objects panel dragable 
    $("#quickTextFormPanel").draggable({

        appendTo: "body",
        cursor: 'move'
    });
    $("#AddTextDragable").draggable({

        appendTo: "body",
        cursor: 'move'
    });

    $("#addImage").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #CarouselDiv"
    });
    $("#DivAlignObjs").draggable({

        appendTo: "body",
        cursor: 'move',
        cancel: "div #CarouselDiv"
    });
	// make canvas dropable for objects
	$("#canvas").droppable({
		activeClass: "custom-state-active",
		drop: function (event, ui) {
			if (ui.draggable.attr('src')) {
				var pos = canvas.getPointer(event);
				var currentPos = ui.helper.position();
				var draggable = ui.draggable;
				// function to add image to canvas
				getImgSize(draggable.attr('src'));
		        ResetZoom(); // reseting canvas to actual size before adding image to canvas
                AddImageObjectToCanvas(draggable.attr('src'), pos.x, pos.y, imWidth, imHeight);

			}
            else if (ui.draggable.attr('id') == "textPropertPanel" || ui.draggable.attr('id') == "ImagePropertyPanel" || ui.draggable.attr('id') == "DivColorPickerDraggable" || ui.draggable.attr('id') == "quickTextFormPanel" || ui.draggable.attr('id') == "AddTextDragable" || ui.draggable.attr('id') == "addImage" || ui.draggable.attr('id') == "DivToolTip" || ui.draggable.attr('id') == "DivAlignObjs") {
                // condition to avoid dummy object creation on panel drag and drop on canvas 
			} else {
				var pos = canvas.getPointer(event);
				var draggable = ui.draggable.attr('id');
				AddQuickText(draggable, pos.x, pos.y);
			}
		}
	});

    
//    // initializing the crop tool
//    $(function () {
//        $('#CropTarget').Jcrop(
//        {
//            //            minSize:[100,100],
//            //            maxSize: [150,150]
//            boxWidth:320,
//            boxHeight:300,
//            onChange: UpdateCropImgCoords,
//            onSelect: UpdateCropImgCoords
//        });
//    });
}
// load template colors
function loadColors() {
	$.getJSON("services/TemplateSvc/GetColor/" + TemplateID,
		function (data) {
			$.each(data, function (i, item) {
				addColorPallet(item.ColorC, item.ColorM, item.ColorY, item.ColorK);
			});
		});
	}
	// function to add color pallets
function addColorPallet(c, m,y,k) {
	//var path = "Designer/PrivateFonts/FontFace/"

	var Color = getColorHex(c, m, y, k);
	//alert(Color);
	var html = "<div class ='ColorPallet' style='background-color:" + Color + "' onclick='ChangeColorHandler(" + c + "," + m + "," + y + "," + k + ",&quot;" + Color + "&quot;);'" + "></div>";
	$('#DivColorContainer').append(html);
}

// this function is used to load cufon fonts it not in use now 
function loadFonts()
{
	jQuery.cachedScript = function (url, options) {
		// allow user to set any option except for dataType, cache, and url  
		options = $.extend(options || {},        { dataType: "script", cache: true, url: url });
		// Use $.ajax() since it is more flexible than $.getScript  
		// Return the jqXHR object so we can chain callbacks  
		return jQuery.ajax(options);
	};
	$.getJSON("services/fontSvc/" + "1234,1",
	function (data) {
		$.each(data, function (i, item) {         
			$.cachedScript("Designer/PrivateFonts/" + item.FontFile).done(function (script, textStatus) {
				 addOption('fonts', item.FontName, item.FontName,item.FontFile);
			});
		});
	});
}

// function to load font face fonts that is currently in use know 
function loadFontFace() {
   
	$.getJSON("services/fontSvc/" +TemplateID+ ",1",
		function (data) {
			$.each(data, function (i, item) {
				addOption('BtnSelectFonts', item.FontName, item.FontName, item.FontFile);
				addFontStyle(item.FontName, item.FontFile);
		});
	});
}
	// function to add font style used by font face
function addFontStyle(fontName, fontFileName) {
	var path = "Designer/PrivateFonts/FontFace/"
	var html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".eot" + '); src: url(' + path + fontFileName + ".eot?#iefix" + ') format(" embedded-opentype"), url(' + path + fontFileName + ".woff" + ') format("woff"),  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
	$('head').append(html);
}
		// helper function to dynammically add vale to select input elemet 
function addOption(selectId, value, text,id) {
	var html = '<option  id = ' +id +' value="'+value+'" >'+text+'</option>';
	$('#'+selectId).append(html);
}


function loadTemplate() {
	$.getJSON("services/TemplateSvc/" + TemplateID,
	function (data) {
		Template = data;
		loadTemplatePages(TemplateID);
	});
}
		// appned a page Image Button to Menu
function AppendPageButton(pageID, PageName,pageType) {
	if (pageType == 1) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 2) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 3) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 4) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 5) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 6) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 7) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 8) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 9) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 10) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 11) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 12) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 13) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 14) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 15) {
	    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
	} else if (pageType == 16) {
		var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}
else if (pageType == 17) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 18) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 19) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 20) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 21) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 22) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 23) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}

else if (pageType == 24) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}


else if (pageType == 25) {
    var html = '<div id = ' + pageID + ' class = "PageItemContainer" onclick= "PageChange( &#034;' + pageID + '&#034;)"><div class="PageItem16"> </div> <div id ="PageText">' + PageName + '</div></div>';
}  
	$('#PagesContainer' ).append(html);		
}

///Page Objects loading
function loadTemplatePages() {

	
	$.getJSON("services/TemplatePagesSvc/" + TemplateID,
	function (data) {
		TemplatePages = data;
		// resizing the pages div
		width = 46 * data.length;
		width = width + "px";
		$("#PagesContainer").css("width", width);
		$.each(data, function (i, item) {
			AppendPageButton(item.ProductPageID, item.PageName, item.PageType)
		});
		$(".PageItemContainer").corner("7px;");
		loadTemplateImages(TemplateID);
	});
}

// function to get image hight and width using javascript 
var imHeight;
var imWidth;

function getImgSize(imgSrc) {
	var newImg = new Image();
	newImg.src = imgSrc;
	imHeight = newImg.height;
	imWidth = newImg.width;
}

// load template images function ///////// 
function loadTemplateImages() {

	$.getJSON("services/imageSvc/" + TemplateID,
		function (data) {
			$.each(data, function (i, item) {
				var obj = {
					url: "./" + item.BackgroundImageRelativePath,
					title: item.ID,
					index: TemplateID
				}
				mycarousel_itemList.push(obj);
			});
			addImgToCarousel();
			loadTemplateObjects(TemplateID);
			loadColors();
		});
}
// function that images to carousel div 
function addImgToCarousel() {
	//var width = 135 * mycarousel_itemList.length;
	//width = width + "px";
	//$("#CarouselImages").css("width",width);
	for (i = 0; i < mycarousel_itemList.length; i++) {
		$("#CarouselImages").append(mycarousel_getItemHTML(mycarousel_itemList[i]));
	}
	// carousel.add(i, mycarousel_getItemHTML(mycarousel_itemList[i - 1]));
	$(".draggable2").draggable({
		snap: '#dropzone',
		snapMode: 'inner',
		revert: 'invalid',
		helper: 'clone',
		appendTo: "body",
		cursor: 'move'
	});
}
//array used for dynamic content uploading in image carousel 
	var mycarousel_itemList = [];

// deleting an imaage from Carousel 
function DeleteImgCarousel(imageID, productID) {
// dialog box to confirm image delete
	if (confirm("Are you sure you want to delete")) {
			// logic after image Delete has been confirmed
		//deleting from Database
		StartLoader();
			$.get("Services/imageSvc/" + productID + "," +imageID,
				function (data) {
					// to write function to  check if true is returned
				   
					
				});
				   
					imageID = '#' + imageID;
					//$(imageID).parent().css("display", "none");

					$(imageID).parent().remove();
					RedrawCarousel(imageID);
				
	}
}
// function to redwar Carousel images
function RedrawCarousel(itemID) {
    // remove all previous elements
	var thelist = document.getElementById("CarouselImages");
	while (thelist.hasChildNodes()) {
		thelist.removeChild(thelist.lastChild);
	}
	// copys the content to new array 
	var newCarourselList = [];
	newCarouselList = mycarousel_itemList;
	mycarousel_itemList = [];
	var iterator = 0;

	for (var i =0; i < newCarouselList.length; i++) {
		if("#"+newCarouselList[i].title !=  itemID) {
		
		 mycarousel_itemList[iterator] = newCarouselList[i];
		 iterator += 1;
		}
	  
	}

		ImgCount = 0;
		addImgToCarousel();
		StopLoader();
}
var ImgCount = 0;  
/** * Item html creation helper that is used to dynamically add images to carousel . */
function mycarousel_getItemHTML(item) {
	//return '<div style= "display:inline;border: 4px;border-color: black;border-style: double; margin-right: 7px;height: 100px;width: 100px;float: left;margin-top: 2px;"><img src="' + item.url + '" width="100" height="100"  class="draggable2" style=" z-index:1000; "id = "' + item.title + '" alt="' + item.url + '" /> <a onclick=DeleteImgCarousel(' + item.title + "," + item.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "style = "z-index: 99999; margin-left: 85px;  margin-bottom:98px;  " /></a>  </div>';
	ImgCount += 1;
	if (ImgCount % 2 == 0) {
	    return '<div  class="DivCarouselImgContainerStyle2"><img src="' + item.url + '" width="75" height="75"  class="draggable2" style=" z-index:1000; "id = "' + item.title + '" alt="' + item.url + '" /> <a class="DelImgAnchor" onclick=DeleteImgCarousel(' + item.title + "," + item.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "/></a>  </div> ';

	} else {
    return '<div  class="DivCarouselImgContainer"><img src="' + item.url + '" width="75" height="75"  class="draggable2" style=" z-index:1000; "id = "' + item.title + '" alt="' + item.url + '" /> <a class="DelImgAnchor" onclick=DeleteImgCarousel(' + item.title + "," + item.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "/></a>  </div>';

	}
   
//	getImgSize(item.url);
//	if (imHeight > imWidth) {
//		//alert(imHeight);
//		//var Ratio = imHeight / 100;
//		//var newWidth = imWidth / Ratio;
//	   // newWidth = Math.ceil(newWidth);
//		var newWidth = 100;
//		newWidth = Math.ceil(newWidth);
//		
//		return '<div style= "display:inline;border: 4px;border-color: black;border-style: double; margin-right: 7px;height: 100px;width: 100px;float: left;margin-top: 2px;"><img src="' + item.url + '" width="'+newWidth +'px " height="100px"  class="draggable2" style=" z-index:1000; "id = "' + item.title + '" alt="' + item.url + '" /> <a onclick=DeleteImgCarousel(' + item.title + "," + item.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "style = "z-index: 99999; margin-left: 85px;  margin-bottom:98px;  " /></a>  </div>';
//	} else {
//		//alert(imHeight);
//	   // var Ratio = imWidth / 100;
//	   // var newHeight = imHeight / Ratio;
//	 
//		//newHeight = Math.ceil(newHeight);
//		var newHeight = 100;
//		return '<div style= "display:inline;border: 4px;border-color: black;border-style: double; margin-right: 7px;height: 100px;width: 100px;float: left;margin-top: 2px;"><img src="' + item.url + '" width="100px" height="' + newHeight + 'px "  class="draggable2" style=" z-index:1000; "id = "' + item.title + '" alt="' + item.url + '" /> <a style="position: fixed;float: right;margin-left: 35px;margin-top: -28px;" onclick=DeleteImgCarousel(' + item.title + "," + item.index + ') ><img id = "DelImgBtn" src = " ./assets/button-icon.png "style = "z-index: 99999;  " /></a>  </div>';
//	}
};

// function to compare objects
var lastObj = null;
function CompareObjs(x, y) {
    if (x === y) return true;
    // if both x and y are null or undefined and exactly the same

    if (!(x instanceof Object) || !(y instanceof Object)) return false;
    // if they are not strictly equal, they both need to be Objects

    if (x.constructor !== y.constructor) return false;
    // they must have the exact same prototype chain, the closest we can do is
    // test there constructor.

    for (var p in x) {
        if (!x.hasOwnProperty(p)) continue;
        // other properties were tested using x.constructor === y.constructor

        if (!y.hasOwnProperty(p)) return false;
        // allows to compare x[ p ] and y[ p ] when set to undefined

        if (x[p] === y[p]) continue;
        // if they have the same strict value or identity then they are equal

        if (typeof (x[p]) !== "object") return false;
        // Numbers, Strings, Functions, Booleans must be strictly equal

        if (!Object.equals(x[p], y[p])) return false;
        // Objects and Arrays must be tested recursively
    }

    for (p in y) {
        if (y.hasOwnProperty(p) && !x.hasOwnProperty(p)) return false;
        // allows x[ p ] to be set to undefined
    }
    return true;
}

//////////////////////////////////
function onObjModified(e, action, skipmode,redoCall) {
	IsDesignModified = true;
	var object = null;
	try {
		object = e.memo.target;
	}
	catch (e) {
	}

	if (!object)
	{
		object = e;
	}
    // zooming out to orignal level
	if (currentZoomLevel != 0) {
	    ActionZoom();
    }
           
    if (action == undefined) {
       // alert(object.type);
		//logAction(object.ObjectID + " is modified " + object.hasStateChanged() + " " + object.isMoving);
        



		// implementing logic for group
		if (object.type == "group") {
			var GroupObjects = object.getObjects();
			$.each(GroupObjects, function (j, obj) {

			    $.each(TemplateObjects, function (i, item) {

			        if (item.ObjectID == obj.ObjectID) {
			            var clonedItemRedo;
			            if (skipmode == undefined) {
			                var clonedItem = fabric.util.object.clone(item);
//			                if (CompareObjs(clonedItem, lastObj)) {
//			                    skipmode = true;
//			                }

			            }

			            item.PositionX = object.left - item.MaxWidth / 2;
			            item.PositionY = object.top - item.MaxHeight / 2;

			            if (obj.type == "text") {
			                item.ContentString = obj.text;
			                //                else if (object.type == "image") {
			                //                    item.ContentString = object.getSrc();

			            }

			            item.RotationAngle = obj.getAngle();

			            if (obj.type != "text") {  // all other objects like image, vectors and svg
			                item.MaxWidth = obj.width * obj.scaleX;
			                item.MaxHeight = obj.height * obj.scaleY;


			                if (obj.type == "ellipse") {

			                    item.CircleRadiusX = obj.getRadiusX();
			                    item.CircleRadiusY = obj.getRadiusY();
			                    //alert(item.CircleRadiusX);
			                    item.PositionX = obj.left - obj.width / 2;
			                    item.PositionY = obj.top - obj.height / 2;
			                }
			            }

			            else { //object is text
			                item.MaxWidth = obj.maxWidth;
			                item.MaxHeight = obj.maxHeight;
			                item.LineSpacing = obj.lineHeight;
			            }

			            if (obj.textAlign == "left")
			                item.Allignment = 1;
			            else if (obj.textAlign == "center")
			                item.Allignment = 2;
			            else if (obj.textAlign == "right")
			                item.Allignment = 3;

			            if (obj.fontFamily != undefined)
			                item.FontName = object.fontFamily;
			            else
			                item.FontName = "";

			            if (obj.fontSize != undefined)
			                item.FontSize = object.fontSize;
			            else
			                item.FontSize = 0;

			            if (obj.fontWeight == "bold")
			                item.IsBold = true;
			            else
			                item.IsBold = false;

			            if (obj.fontStyle == "italic")
			                item.IsItalic = true;
			            else
			                item.IsItalic = false;

			            if (obj.type != "image") {
			                item.ColorHex = object.fill;

			            }

			            if (obj.type == "path") {
			                item.ExField1 = object.strokeWidth;
			            }


			            item.Opacity = obj.opacity;
			            item.ColorC = obj.C;
			            item.ColorM = obj.M;
			            item.ColorY = obj.Y;
			            item.ColorK = obj.K;


			            item.IsPositionLocked = obj.IsPositionLocked;
			            item.IsHidden = obj.IsHidden;
			            item.IsEditable = obj.IsEditable;

			            clonedItemRedo = fabric.util.object.clone(item);
			           // lastObj = clonedItemRedo;

			            // register an undo or redo call 
			            if (skipmode == undefined) {
			                // undoManager.register(undefined, UndoFunc, [clonedItem], 'Undo', undefined, RedoFunc, [clonedItemRedo], 'Redo');
			                undoManager.register(undefined, ModifyObject, [clonedItem], 'Undo', undefined, ModifyObject, [clonedItemRedo], 'Redo');
			            }

			        }

			    });

			});

		} else {
			//update state of single object.
		$.each(TemplateObjects, function (i, item) {
		    if (item.ObjectID == object.ObjectID) {
		        var clonedItemRedo;
		        if (skipmode == undefined) {
		            var clonedItem = fabric.util.object.clone(item);
//		            if (CompareObjs(clonedItem, lastObj)) {
//		                skipmode = true;
//		            }
		            
		        }

		        item.PositionX = object.left - item.MaxWidth / 2;
		        item.PositionY = object.top - item.MaxHeight / 2;

		        if (object.type == "text") {
		            item.ContentString = object.text;
		            //                else if (object.type == "image") {
		            //                    item.ContentString = object.getSrc();

		        }

		        item.RotationAngle = object.getAngle();

		        if (object.type != "text") {  // all other objects like image, vectors and svg
		            item.MaxWidth = object.width * object.scaleX;
		            item.MaxHeight = object.height * object.scaleY;


		            if (object.type == "ellipse") {

		                item.CircleRadiusX = object.getRadiusX();
		                item.CircleRadiusY = object.getRadiusY();
		                //alert(item.CircleRadiusX);
		                item.PositionX = object.left - object.width / 2;
		                item.PositionY = object.top - object.height / 2;
		            }
		        }

		        else { //object is text
		            item.MaxWidth = object.maxWidth;
		            item.MaxHeight = object.maxHeight;
		            item.LineSpacing = object.lineHeight;
		        }

		        if (object.textAlign == "left")
		            item.Allignment = 1;
		        else if (object.textAlign == "center")
		            item.Allignment = 2;
		        else if (object.textAlign == "right")
		            item.Allignment = 3;

		        if (object.fontFamily != undefined)
		            item.FontName = object.fontFamily;
		        else
		            item.FontName = "";

		        if (object.fontSize != undefined)
		            item.FontSize = object.fontSize;
		        else
		            item.FontSize = 0;

		        if (object.fontWeight == "bold")
		            item.IsBold = true;
		        else
		            item.IsBold = false;

		        if (object.fontStyle == "italic")
		            item.IsItalic = true;
		        else
		            item.IsItalic = false;

		        if (object.type != "image") {
		            item.ColorHex = object.fill;

		        }

		        if (object.type == "path") {
		            item.ExField1 = object.strokeWidth;
		        }


		        item.Opacity = object.opacity;

		        item.ColorC = object.C;
		        item.ColorM = object.M;
		        item.ColorY = object.Y;
		        item.ColorK = object.K;

		        item.IsPositionLocked = object.IsPositionLocked;
		        item.IsHidden = object.IsHidden;
		        item.IsEditable = object.IsEditable;

		        clonedItemRedo = fabric.util.object.clone(item);
		       // lastObj = clonedItemRedo;
		        // register an undo or redo call 
		        if (skipmode == undefined) {
		            // undoManager.register(undefined, UndoFunc, [clonedItem], 'Undo', undefined, RedoFunc, [clonedItemRedo], 'Redo');
		            undoManager.register(undefined, ModifyObject, [clonedItem], 'Undo', undefined, ModifyObject, [clonedItemRedo], 'Redo');
		        }



		    }

		});
        }

	}
	else if (action == "delete") {
		//logAction(object.ObjectID + " is deleted " + object.hasStateChanged());

		// delete action for group 
		if (object.type == "group") {
			var GroupObjects = object.getObjects();
			$.each(GroupObjects, function (j, obj) {
			    $.each(TemplateObjects, function (i, item) {
			        if (item.ObjectID == obj.ObjectID) {

			            var objects = canvas.getObjects();
			            $.each(objects, function (i, SubItem) {
			                if (SubItem.ObjectID == item.ObjectID) {

			                    clonedItemRedo = fabric.util.object.clone(SubItem);
			                }
			            });
			            clonedItem = fabric.util.object.clone(item);
			            // undoManager.register(undefined, UndoFuncDel, [clonedItem], 'Undo', undefined, RedoFuncDel, [clonedItemRedo], 'Redo');
			            undoManager.register(undefined, ModifyObject, [clonedItem, 'delete'], 'Undo', undefined, ModifyObject, [clonedItemRedo, 'redoDelete'], 'Redo');
			            
			            //actually deleting the object from underlying array
			            fabric.util.removeFromArray(TemplateObjects, item);
			            return false;

			        }
			    });
			});
		} else {      // for single object 
			$.each(TemplateObjects, function (i, item) {
				if (item.ObjectID == object.ObjectID) {

					var objects = canvas.getObjects();
					$.each(objects, function (i, SubItem) {
						if (SubItem.ObjectID == item.ObjectID) {

							clonedItemRedo = fabric.util.object.clone(SubItem);
						}
					});
					clonedItem = fabric.util.object.clone(item);
					// undoManager.register(undefined, UndoFuncDel, [clonedItem], 'Undo', undefined, RedoFuncDel, [clonedItemRedo], 'Redo');
					undoManager.register(undefined, ModifyObject, [clonedItem, 'delete'], 'Undo', undefined, ModifyObject, [clonedItemRedo,'redoDelete'], 'Redo');
					
					//actually deleting the object from underlying array
					fabric.util.removeFromArray(TemplateObjects, item);
					return false;

				}
			});
		}

    }

    // zooming to current level 
    if (currentZoomLevel != 0 && redoCall != true) {
        if (currentZoomLevel > 0) {

            for (var i = 0; i < currentZoomLevel; i++) {
                ZoomIn();
            }
        } else if (currentZoomLevel < 0) {
            var currentZoomLevel1 = currentZoomLevel * -1;
            for (var i = 0; i < currentZoomLevel1; i++) {
                zoomOut();
            }
        
        }
    }
    canvas.renderAll();

}


//called in case of undo and redo 
function ModifyObject(target, action) {
    // zooming out to orignal level
    if (currentZoomLevel != 0) {
        ActionZoom();
    }

    if (action == undefined) {
        var canvasObjects = canvas.getObjects(); 
        $.each(canvasObjects, function (i, item) {
            if (item.ObjectID == target.ObjectID) {
                if (item.type == "text") {
                    item.left = target.PositionX + target.MaxWidth / 2;
                    item.top = target.PositionY + target.MaxHeight / 2;
                    item.text = target.ContentString;
                    item.setAngle(target.RotationAngle);
                    item.maxWidth = target.MaxWidth;
                    item.maxHeight = target.MaxHeight;

                    if (target.tAllignment == 1)
                        item.textAlign = "left"
                    else if (target.Allignment == 2)
                        item.textAlign = "center";
                    else if (target.Allignment == 3)
                        item.textAlign = "right";

                    item.fontFamily = target.FontName;
                    item.fontSize = target.FontSize;
                    item.fontWeight = (target.IsBold == true ? 'bold' : '')
                    item.fontStyle = (target.IsItalic == true ? 'italic' : '')
                    item.fill = target.ColorHex;
                    item.C = target.ColorC;
                    item.M = target.ColorM;
                    item.Y = target.ColorY;
                    item.K = target.ColorK;
                }
                else if (item.type == "image") {

                    item.left = target.PositionX + target.MaxWidth / 2;
                    item.top = target.PositionY + target.MaxHeight / 2;
                    item.setAngle(target.RotationAngle);
                    item.maxWidth = target.MaxWidth;
                    item.maxHeight = target.MaxHeight;
                    item.scaleToWidth(target.MaxWidth);
                }
                else {  //objects

                    item.left = target.PositionX + target.MaxWidth / 2;
                    item.top = target.PositionY + target.MaxHeight / 2;
                    item.setAngle(target.RotationAngle);
                    item.maxWidth = target.MaxWidth;
                    item.maxHeight = target.MaxHeight;


                    item.fill = target.ColorHex;

                    item.C = target.ColorC;
                    item.M = target.ColorM;
                    item.Y = target.ColorY;
                    item.K = target.ColorK;

                }

                onObjModified(item, action, true,true);

            }
        });


	}
	else if (action == "delete") {
		//alert('recovering object of ID ' + target.ObjectID);
		TemplateObjects.push(target);
		if (target.ObjectType == 2) {
			AddTextObject(canvas, target);
		}
		else if (target.ObjectType == 3) {
			AddImageObject(canvas, target);
		}
		else if (target.ObjectType == 6) {
			AddRectangleObject(canvas, target);
		}
		else if (target.ObjectType == 7) {
			AddCircleObject(canvas, target);
		}
		else if (target.ObjectType == 9) {
			AddPathObject(canvas, target);
		}
		//canvas.renderAll();

		} // redo for deleted images
		else if (action == "redoDelete") {
			var ItemToDel;
			$.each(TemplateObjects, function (i, item) {
				if (item.ObjectID == target.ObjectID) {

					var objects = canvas.getObjects();
					$.each(objects, function (i, SubItem) {
						if (SubItem.ObjectID == item.ObjectID) {
							ItemToDel = SubItem
							clonedItem = fabric.util.object.clone(item);
							clonedItemRedo = fabric.util.object.clone(SubItem);
							undoManager.register(undefined, ModifyObject, [clonedItem], 'Undo', undefined, undefined, [undefined], 'Redo');


						}
					});
					canvas.remove(ItemToDel);
					fabric.util.removeFromArray(TemplateObjects, item);

					//actually deleting the object from underlying array

					return false;

				}
			});
		}

    // zooming to current level 
    if (currentZoomLevel != 0) {
        if (currentZoomLevel > 0) {
            for (var i = 0; i < currentZoomLevel; i++) {
                ZoomIn();
               // alert(i + " " + currentZoomLevel);
            }
        } else if (currentZoomLevel < 0) {
            var currentZoomLevel1 = currentZoomLevel * -1;
            for (var i = 0; i < currentZoomLevel1; i++) {
                zoomOut();
               // alert(i + " " + currentZoomLevel);
            }

        }
    }
    canvas.renderAll();
}


//loads quick text data
function loadQuickTextData() {

	//get CustomerID and ContactID from parent pane of iframe
	CustomerID = parent.CustomerID;
	ContactID = parent.ContactID;

	$.getJSON("../services/Webstore.svc/getquicktext?Customerid=" + CustomerID + "&contactid=" + ContactID,

		function (xdata) {

			//alert(xdata.Name);
			QuickTextData = xdata;
		   
			loadQuickTextDatatoUI();
			InitUI();

		});



}



function loadQuickTextDatatoUI() {

	//alert(QuickTextData.Name);
	$("#txtQName").val(QuickTextData.Name);
	$("#txtQTitle").val(QuickTextData.Name);
	$("#txtQCompanyName").val(QuickTextData.Company);
	$("#txtQCompanyMessage").val(QuickTextData.CompanyMessage);
	$("#txtQAddress").val(QuickTextData.Address1);
	$("#txtQtelephone").val(QuickTextData.Telephone);
	$("#txtQFax").val(QuickTextData.Fax);
	$("#txtQEmail").val(QuickTextData.Email);
	$("#txtQWebsite").val(QuickTextData.Website);


}


function SaveQuickText() {

	QuickTxtName = $("#txtQName").val();
	QuickTxtTitle = $("#txtQTitle").val();
	QuickTxtCompanyName = $("#txtQCompanyName").val();
	QuickTxtCompanyMsg = $("#txtQCompanyMessage").val();
	QuickTxtAddress1 = $("#txtQAddress").val();
	QuickTxtTel = $("#txtQtelephone").val();
	QuickTxtFax = $("#txtQFax").val();
	QuickTxtEmail = $("#txtQEmail").val();
	QuickTxtWebsite = $("#txtQWebsite").val();


	QuickTextData.Company = (QuickTxtCompanyName == "" ? "Your Company Name" : QuickTxtCompanyName);
	QuickTextData.CompanyMessage = QuickTxtCompanyMsg == "" ? "Your Company Message" : QuickTxtCompanyMsg;
	QuickTextData.Name = QuickTxtName == "" ? "Your Name" : QuickTxtName;
	QuickTextData.Title = QuickTxtTitle == "" ? "Your Title" : QuickTxtTitle;
	QuickTextData.Address1 = QuickTxtAddress1 == "" ? "Address Line 1" : QuickTxtAddress1;


	QuickTextData.Telephone = QuickTxtTel == "" ? "Telephone / Other" : QuickTxtTel;
	QuickTextData.Fax = QuickTxtFax == "" ? "Fax / Other" : QuickTxtFax;
	QuickTextData.Email = QuickTxtEmail == "" ? "Email address / Other" : QuickTxtEmail;
	QuickTextData.Website = QuickTxtWebsite == "" ? "Website address" : QuickTxtWebsite;


    
	//iterate in all objects and change the values if matching
	$.each(TemplateObjects, function (i, item) {

	    switch (item.Name) {
	        case "CompanyName":
	            {
	                item.ContentString = QuickTextData.Company;
	                // alert(TemplateObjects[i].ContentString);
	                break;
	            }
	        case "CompanyMessage":
	            {
	                item.ContentString = QuickTextData.CompanyMessage;
	                break;
	            }
	        case "Name":
	            {
	                item.ContentString = QuickTextData.Name;
	                break;
	            }
	        case "Title":
	            {
	                item.ContentString = QuickTextData.Title;
	                break;
	            }
	        case "AddressLine1":
	            {
	                item.ContentString = QuickTextData.Address1;
	                break;
	            }
	        case "Phone":
	            {
	                item.ContentString = QuickTextData.Telephone;
	                break;
	            }
	        case "Fax":
	            {
	                item.ContentString = QuickTextData.Fax;
	                break;
	            }
	        case "Email":
	            {
	                item.ContentString = QuickTextData.Email;
	                break;
	            }
	        case "Website":
	            {
	                item.ContentString = QuickTextData.Website;
	                break;
	            }

	    }

	});

	//now refresh the current page
	PageChange(SelectedPageID);

	if (CustomerID != 0 && ContactID != 0) {

		//attempt saving the quick text via service call


		StartLoader();
		//saving the objects first
		var jsonObjects = JSON.stringify(QuickTextData, null, 2);
		var to;


		to = "../services/Webstore.svc/update/";


		var options = {
			type: "POST",
			url: to,
			data: jsonObjects,
			contentType: "text/plain;",
			dataType: "json",
			async: false,
			success: function (response) {
				//alert("success: " + response);
			},
			error: function (msg) { alert("Error : " + msg); }
		};



		var returnText = $.ajax(options).responseText;


//        if (returnText == '"false"') {
//            StopLoader();
//        }



		StopLoader();

	}



}



		 ////////////////////////////////////
function loadTemplateObjects() {

	$.getJSON("services/TemplateObjectsSvc/" + TemplateID,

		function (data) {

			TemplateObjects = data;


			if (IsEmbedded) {
				//load quick text information if available
				loadQuickTextData();
				
			}
			else {
				InitUI();
			}



		});

	}

	function showPageObjects(PageID) {

	//  1,2,4 text  5 vector line   6 rectangle  7 ellipse cirlce   8 logo image

	    $.each(TemplateObjects, function (i, item) {

	        if (item.ProductPageId == PageID) {
	            if (item.ObjectType == 2) {
	                //delicious_500
	                
	                AddTextObject(canvas, item);

	            }
	            else if (item.ObjectType == 3) {
	                //alert(item.DisplayOrderPdf);
	                $("#loadingMsg").html("Loading Design Images, please wait..");
	                AddImageObject(canvas, item);
	            }
	            else if (item.ObjectType == 6) {
	                AddRectangleObject(canvas, item);
	            }
	            else if (item.ObjectType == 7) {
	                AddCircleObject(canvas, item);
	            }
	            else if (item.ObjectType == 9) {
	                AddPathObject(canvas, item);
	            }
	        }


	    });

	}

	function AddCircleObject(cCanvas, CircleObj) {
		var circleObject = new fabric.Ellipse({
			left: CircleObj.PositionX + CircleObj.MaxWidth / 2,
			top: CircleObj.PositionY + CircleObj.MaxHeight / 2,
			fill: CircleObj.ColorHex,
			rx: CircleObj.CircleRadiusX,
			ry: CircleObj.CircleRadiusY,
			opacity: CircleObj.Opacity
			
		})

		circleObject.C = CircleObj.ColorC;
		circleObject.M = CircleObj.ColorM;
		circleObject.Y = CircleObj.ColorY;
		circleObject.K = CircleObj.ColorK;

		circleObject.IsPositionLocked = CircleObj.IsPositionLocked;
		circleObject.IsHidden = CircleObj.IsHidden;
		circleObject.IsEditable = CircleObj.IsEditable;

		circleObject.setAngle(CircleObj.RotationAngle);
		//circleObject.scaleToWidth(CircleObj.MaxWidth)

		//circleObject.scaleX = circleObject.maxWidth / circleObject.width;
		//circleObject.scaleY = circleObject.maxHeight / circleObject.height;

		circleObject.ObjectID = CircleObj.ObjectID;

		circleObject.set({
			borderColor: 'red',
			cornerColor: 'orange',
			cornersize: 10
		});
		canvas.insertAt(circleObject, CircleObj.DisplayOrderPdf);
		canvas.renderAll();


	}

	function AddRectangleObject(cCanvas, RectObj) {

		var rectObject = new fabric.Rect({
			left: RectObj.PositionX + RectObj.MaxWidth / 2,
			top: RectObj.PositionY + RectObj.MaxHeight / 2,
			fill: RectObj.ColorHex,
			width: RectObj.MaxWidth,
			height: RectObj.MaxHeight,
			opacity: 1
		});

		rectObject.setAngle(RectObj.RotationAngle);

		rectObject.C = RectObj.ColorC;
		rectObject.M = RectObj.ColorM;
		rectObject.Y = RectObj.ColorY;
		rectObject.K = RectObj.ColorK;

		rectObject.IsPositionLocked = RectObj.IsPositionLocked;
		rectObject.IsHidden = RectObj.IsHidden;
		rectObject.IsEditable = RectObj.IsEditable;

		rectObject.set({
			borderColor: 'red',
			cornerColor: 'orange',
			cornersize: 10
		});

        


		rectObject.ObjectID = RectObj.ObjectID;
		canvas.insertAt(rectObject, RectObj.DisplayOrderPdf);
		//NewTextObejct.PositionX = rectObject.left - rectObject.maxWidth / 2;
	   // NewTextObejct.PositionY = rectObject.top - rectObject.maxHeight / 2;
		canvas.renderAll();
	}

function AddTextObject(cCanvas,TextObject) {


			
	var hAlign = "";

	if (TextObject.Allignment == 1)
		hAlign = "left";
	else if (TextObject.Allignment == 2)
		hAlign = "center";
	else if (TextObject.Allignment == 3)
		hAlign = "right";

	var hStyle = "";
	if (TextObject.IsItalic)
		hStyle = "italic";
	   

	var hWeight = "";
	if (TextObject.IsBold)
		hWeight = "bold";


	var txtObject = new fabric.Text(TextObject.ContentString, {
		left: TextObject.PositionX + TextObject.MaxWidth / 2,
		top: TextObject.PositionY + TextObject.MaxHeight / 2,
		fontFamily: TextObject.FontName,
		fontStyle: hStyle,
		fontWeight: hWeight,
		lineHeight:(TextObject.LineSpacing == 0 ? 1 : TextObject.LineSpacing) ,
		fontSize: TextObject.FontSize,
		angle: TextObject.RotationAngle,
		fill: TextObject.ColorHex,
		scaleX: 1,
		scaleY: 1,
		maxWidth: TextObject.MaxWidth,
		maxHeight: TextObject.MaxHeight,
		textAlign: hAlign

		});

	 
	txtObject.ObjectID = TextObject.ObjectID;
	txtObject.C = TextObject.ColorC;
	txtObject.M = TextObject.ColorM;
	txtObject.Y = TextObject.ColorY;
	txtObject.K = TextObject.ColorK;


	txtObject.IsPositionLocked = TextObject.IsPositionLocked;
	txtObject.IsHidden = TextObject.IsHidden;
	txtObject.IsEditable = TextObject.IsEditable;

	txtObject.setAngle(TextObject.RotationAngle);

	if (TextObject.IsPositionLocked) {
		txtObject.lockMovementX = true;
		txtObject.lockMovementY = true;
		txtObject.lockScalingX = true;
		txtObject.lockScalingY = true;
		txtObject.lockRotation = true;
	}
	else {
		txtObject.lockMovementX = false;
		txtObject.lockMovementY = false;
		txtObject.lockScalingX = false;
		txtObject.lockScalingY = false;
		txtObject.lockRotation = false;
	}

	txtObject.set({
				borderColor: 'red',
				cornerColor: 'orange',
				cornersize: 10
			});


			cCanvas.insertAt(txtObject, TextObject.DisplayOrderPdf);
	return txtObject;
		
}


	
function AddImageObject(cCanvas, ImageObject) {

	// fuction called incrementing variable for loader logic
    TotalImgCalls += 1;
    // checking if object width or height is zero 
    if (ImageObject.MaxWidth == 0) {
        ImageObject.MaxWidth = 50;
    }
    if (ImageObject.MaxHeight == 0) {
        ImageObject.MaxHeight = 50;
    }


	//fabric.Image.fromURL(ImageObject.ContentString + "?r=" + fabric.util.getRandomInt(1, 100), function (imgObject) {
	fabric.Image.fromURL(ImageObject.ContentString, function (imgObject) {
		imgObject.set({
			left: ImageObject.PositionX + ImageObject.MaxWidth / 2,
			top: ImageObject.PositionY + ImageObject.MaxHeight / 2,
			angle: ImageObject.RotationAngle
		});

		imgObject.maxWidth = ImageObject.MaxWidth;
		imgObject.maxHeight = ImageObject.MaxHeight;
		imgObject.ObjectID = ImageObject.ObjectID;
		//imgObject.scaleToWidth(ImageObject.MaxWidth);
		imgObject.scaleX = imgObject.maxWidth / imgObject.width;
		imgObject.scaleY = imgObject.maxHeight / imgObject.height;

		imgObject.setAngle(ImageObject.RotationAngle);

		imgObject.IsPositionLocked = ImageObject.IsPositionLocked;
		imgObject.IsHidden = ImageObject.IsHidden;
		imgObject.IsEditable = ImageObject.IsEditable;
		
        imgObject.set({
			borderColor: 'red',
			cornerColor: 'orange',
			cornersize: 10
		});

		cCanvas.insertAt(imgObject, ImageObject.DisplayOrderPdf);
		// function to check all images have been loaded
		TotalImgLoaded += 1;
		CheckAllImageLoaded();

	});

}


// variable to store total image calls
var TotalImgCalls = 0;
// variable to store total images loaded
var TotalImgLoaded = 0;
var LoadingImagesFirstTime = true;
// function to check if all images have been loaded or not
function CheckAllImageLoaded() {
	
	// commenting out this for multiple pages
	if(LoadingImagesFirstTime &&  TotalImgCalls === TotalImgLoaded)
	//if(  TotalImgCalls === TotalImgLoaded)
	{
		LoadingImagesFirstTime = false;
		StopLoader();
	}
}

	   //overload to add image object
function AddImageObjectNew(cCanvas, imagePath, positionX, positionY, rotationAngle, width, height, DisplayOrder) {
	
	fabric.Image.fromURL(imagePath, function (imgObject) {
		imgObject.set({
			left: positionX + width / 2,
			top: positionY - height / 2,
			angle: rotationAngle

		});

		imgObject.width = width;
		imgObject.height = height;

		imgObject.maxWidth = width;
		imgObject.maxHeight = height;
		imgObject.ObjectID = --NewControlID;
		//imgObject.scaleToWidth(width);

		imgObject.setAngle(rotationAngle);

		imgObject.set({
			borderColor: 'red',
			cornerColor: 'orange',
			cornersize: 10
		});

		cCanvas.insertAt(imgObject, DisplayOrder);
	});

		   

}

		//SVG Path Objects type = 9
function AddPathObject(cCanvas, CircleObj) {
	var objPath = new fabric.Path(CircleObj.ContentString);

	objPath.set({
		borderColor: 'red',
		cornerColor: 'orange',
		cornersize: 10
	});
	objPath.ObjectID = CircleObj.ObjectID;


	objPath.setAngle(CircleObj.RotationAngle);

	objPath.IsPositionLocked = CircleObj.IsPositionLocked;
	objPath.IsHidden = CircleObj.IsHidden;
	objPath.IsEditable = CircleObj.IsEditable;


	objPath.fill = null;
	objPath.stroke = CircleObj.ColorHex;
	objPath.strokeWidth = CircleObj.ExField1;
	objPath.set("left", CircleObj.PositionX + objPath.width /2);
	objPath.set("top", CircleObj.PositionY + objPath.height /2);
	canvas.insertAt(objPath, CircleObj.DisplayOrderPdf);

//            objPath.set("left", minX + (maxX - minX) / 2)
//            .set("top", minY + (maxY - minY) / 2).setCoords();

		   

}


// function to change page on canvas
function PageChange(pageID) {
    animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel', 'UploadImage', 'quickText', 'addImage', 'addText', 'quickTextFormPanel', 'DivToolTip', 'DivUploadFont', 'DivColorPallet', 'DivAdvanceColorPanel', 'DivCropToolContainer', 'DivAlignObjs']);
    ResetZoom("pageChange");
    //deselecting all active objects 
    var activeObject = canvas.getActiveObject();
    var activeGroup = canvas.getActiveGroup();
    if (activeGroup) {
        canvas.discardActiveGroup();
    } else if (activeObject) {
        canvas.discardActiveObject();
    }
    ResetZoom("pageChange");
    canvas.renderAll();
    //
   
	SelectedPageID = pageID;
	$(".PageItemContainer").css("background-color", "#FAF9F7");
	$(".PageItemContainer").css("color", "black");
	$("#" + pageID).css("background-color", "#A5A6AD"); // dark gray color for current page icon
	$("#" + pageID).css("color", "white"); // dark gray color for current page icon


	$.each(TemplatePages, function (i, item) {
		if (item.ProductPageID == SelectedPageID) {
			//resetting the orientation of Canvas based on the orientation of Canvas 
			canvas.clear();

			if (item.Orientation == 1) // portrail
			{
				canvas.setHeight(Template.PDFTemplateHeight);
				canvas.setWidth(Template.PDFTemplateWidth);
				InitPageGuides(Template.PDFTemplateWidth, Template.PDFTemplateHeight, IsShowGuides);
			}
			else {
				canvas.setHeight(Template.PDFTemplateWidth);
				canvas.setWidth(Template.PDFTemplateHeight);
				InitPageGuides(Template.PDFTemplateHeight, Template.PDFTemplateWidth, IsShowGuides);
			}

		}

	});

	showPageObjects(pageID);
}


function InitPageGuides(width, height, showguides) {

	if (showguides) {
		var cutmargin = Template.CuttingMargin
		var leftline = makeLine([cutmargin, cutmargin, cutmargin, cutmargin + height - cutmargin * 2], -980, 'gray');
		var topline = makeLine([cutmargin, cutmargin, cutmargin + width - cutmargin * 2, cutmargin], -981, 'gray');
		var rightline = makeLine([cutmargin + width - cutmargin * 2, cutmargin, cutmargin + width - cutmargin * 2, cutmargin + height - cutmargin * 2], -982, 'gray');
		var bottomline = makeLine([cutmargin, cutmargin + height - cutmargin * 2, cutmargin + width - cutmargin * 2, cutmargin + height - cutmargin * 2], -983, 'gray');

		var zafeZoneMargin = cutmargin + 8.49;

		var sleftline = makeLine([zafeZoneMargin, zafeZoneMargin, zafeZoneMargin, zafeZoneMargin + height - zafeZoneMargin * 2], -984, 'cyan');
		var stopline = makeLine([zafeZoneMargin, zafeZoneMargin, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin], -985, 'cyan');
		var srightline = makeLine([zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin + height - zafeZoneMargin * 2], -986, 'cyan');
		var sbottomline = makeLine([zafeZoneMargin, zafeZoneMargin + height - zafeZoneMargin * 2, zafeZoneMargin + width - zafeZoneMargin * 2, zafeZoneMargin + height - zafeZoneMargin * 2], -987, 'cyan');

		canvas.add(leftline, topline, rightline, bottomline, sleftline, stopline, srightline, sbottomline);
	}

	var iCounter = 1;
	while (iCounter < width) {
		SnapXPoints.push(iCounter);
		iCounter += 5;
	}

	iCounter = 1;
	while (iCounter < height) {
		SnapYPoints.push(iCounter);
		iCounter += 5;
	}

}


function makeLine(coords, ObjectID, color) {
	var line =  new fabric.Line(coords,
		{ fill: color, strokeWidth: 1, selectable: false
		});

	line.ObjectID = ObjectID;
	return line;
}