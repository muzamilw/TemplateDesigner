﻿$(document).ready(function () {
   $("#content").css("display", "block");
   $("#content").stop().animate({
       opacity: 1
   },1500)
    $("#content").addClass("on");
    StartLoader();
    fu02UI();
    fu02();
});

$(window).resize(function () {
      var height = $(window).height() ;
    $('.scrollPane').slimscroll({
        height: height
    });
    $('.scrollPane2').slimscroll({
        height: $(window).height()
    });
    $('.resultLayoutsScroller').slimscroll({
        height: height
    }).bind('slimscroll', function (e, pos) {
        if (pos == "bottom") {
            fu09();
        }
    });
});
$(window).scroll(function () {
    canvas.calcOffset();
});

$("#canvaDocument").scroll(function () {

    canvas.calcOffset();
});
//$(window).load(function () {
//    downloadJSAtOnload("js/a12/aj9.js");
//    downloadJSAtOnload("js/a12/aj21-v2.js");
//    downloadJSAtOnload("js/a12/aj12.js");
//    downloadJSAtOnload("js/a12/aj1.js");
//});
$("body").click(function (event) {
    if (event.target.id == "") {
        //  animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
    } else if (event.target.id == "btnNewTxtPanel") {
        // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
    } else if (event.target.id == "btnImgPanel" || event.target.id == "btnBkImgPanel") {
        // animatedcollapse.hide(['textPropertPanel', 'ShapePropertyPanel', 'ImagePropertyPanel','UploadImage','quickText','addImage','addText']);
    } else if (event.target.id == "bd-wrapper" || event.target.id == "canvasSection" || event.target.id == "CanvasContainer") {
        var D1AO = canvas.getActiveObject();
        var D1AG = canvas.getActiveGroup();
        if (D1AG) {
            canvas.discardActiveGroup();
        } else if (D1AO) {
            canvas.discardActiveObject();
        }
        canvas.renderAll();
        pcL13();
        pcL36('hide', '#textPropertPanel ,#divPopupUpdateTxt ,#divVariableContainer ,#DivAdvanceColorPanel ,#DivColorPallet ,#ShapePropertyPanel ,#ImagePropertyPanel ,#UploadImage ,#quickText ,#divPositioningPanel ,#DivAlignObjs ,#DivToolTip ,#addText ,#addImage');

    }

});
function b3_1(caller) {
    if (IsCalledFrom == 1 || IsCalledFrom == 3) {
        var catID = Template.ProductCategoryID;
        var svcURL = "services/layoutsvc/";
        if (IsCalledFrom == 3) {
            catID = cIDv2;
            svcURL = V2Url + "services/layoutsvc/";
        }
        $.getJSON(svcURL + catID,
        function (DT) {
            llData = DT;
            l4(caller);
        });
    }
}

function b8_svc(imageID, productID) {
    $.get("Services/imageSvc/" + productID + "," + imageID,
        function (DT) {
            b8_svc_CallBack(DT);
        });
}
function fu03() {
    $.getJSON(V2Url + "services/TemplateSvc/GetCategoryV2/" + cIDv2,
   function (DT) {
       TempHMM = DT.HeightRestriction;
       TempWMM = DT.WidthRestriction;
       fu04();
   });
}
function fu04() {
    $.getJSON("services/TemplateSvc/TemplateV2/" + tID + "," + cID + "," + TempHMM + "," + TempWMM,
   function (DT) {
       fu04_callBack(DT);
   });

}
function fu04_01() {
    $.getJSON("services/TemplateObjectsSvc/" + tID,
      function (DT) {
          TO = DT;
          fu07();
          fu06();
          // if (firstLoad) {
          fu05();
          //   }
      });
    k0();

}
function fu05_Cl() {
    var Cid = 0;
    if (IsCalledFrom == 2 || IsCalledFrom == 4) {
        Cid = CustomerID;
    }
    $.getJSON("services/TemplateSvc/GetColor/" + tID + "," + Cid,
       function (DT) {
           fu05_svcCall(DT);
       });
}
function fu05() {
    //CustomerID = parent.CustomerID;
    //ContactID = parent.ContactID;
    $(".QuickTextFields").html("");
    //  $(".QuickTextFields").append('<li><a class="add addTxtSubtitle ThemeColor" style="" data-style="title">Update your Quick text Profile</a></li>');
    $.getJSON("../services/Webstore.svc/getquicktext?Customerid=" + CustomerID + "&contactid=" + ContactID,
        function (xdata) {
            fu05_SvcCallback(xdata);
        });
}
function fu09() {
    if (tcAllcc) return;
    tcAllcc = true;

    startInlineLoader(1);
    $.getJSON(V2Url + "services/TemplateSvc/GetCatList/" + cIDv2 + "," + tcListCc + "," + 16,
 function (DT) {
     fu09_SvcCallBack(DT);

 });
}
function svcCall1(ca, gtID) {
    $.getJSON("../services/Webstore.svc/mergeTemplates?RemoteTemplateID=" + gtID + "&LocalTempalteID=" + tID,
          function (xdata) {
              fu04();

          });
}
function svcCall2(n, tID, imgtype) {
    $.getJSON("services/imageSvc/DownloadImg/" + n + "," + tID + "," + imgtype,
    function (DT) {
        j9_21(DT);
    });
}
function svcCall3(imToLoad) {
    $.getJSON("services/imageSvcDam/" + imToLoad,
      function (DT) {
          k26_Dt(DT);
      });
}

function svcCall4(n, tID, imgtype) {
    $.getJSON("services/imageSvc/DownloadImg/" + n + "," + tID + "," + imgtype,
        function (DT) {
            k32_load(DT);
        });
}

function fu06() {
    //CustomerID = parent.CustomerID;
    //ContactID = parent.ContactID;
    var str = '<option value="">(select)</option>';
    var fname = 'BtnSelectFontsRetail';
    if (panelMode == 1) {
        fname = 'BtnSelectFonts';
    }
    $('#' + fname).html(str);
    $.getJSON("services/fontSvc/" + tID + "," + CustomerID,
        function (DT) {
            fu06_SvcCallback(DT,fname);
        });
}





