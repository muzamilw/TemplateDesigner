var IDofObject = "";
var IDOfObject2 = "";

$(function () {
    GetIDs();
});

function GetIDs() {



    PageMethods.GetIDs(OnGetIDSucceeded, OnFailed);
}

function OnGetIDSucceeded(result, userContext, methodName) {



    if (result != "") {


        var ids = result.split('|');


        IDofObject = ids[0];
        IDOfObject2 = ids[1];

        $("#uploader").pluploadQueue({
            // General settings
            runtimes: 'html5,flash',
            url: 'UploadHandler.ashx',
            max_file_size: '10mb',
            chunk_size: '1mb',
            unique_names: false,

            headers: { "IDofObject1": IDofObject, "IDofObject2": IDOfObject2 },
            // custom parameters s
            multipart_params: { pid: productID, ptype: PType },
            // Specify what files to browse for  
            filters: [
               { title: "Files (.jpg, .gif, .png, )", extensions: "jpg,gif,png" }

            ],



            // this path needs to be consistent with where the page using this script is
            // Flash settings
            flash_swf_url: 'js/plupload/plupload.flash.swf',
            // Silverlight settings
            silverlight_xap_url: 'js/plupload/plupload.silverlight.xap'
        });

        //hide the start upload button because we want to use our Submit Item button
        var uploader = $('#uploader').pluploadQueue();

        uploader.bind('Init', function (up, res) {
            // alert("called");
            $('a.plupload_start').css('display', 'none');
            $('.plupload_header').css('display', 'none');
            $('.plupload_content').css('color', 'black');


        });


        //make sure that the start upload button is always disabled
        //because we want our own submit button to take action on the file uploads,
        //not the start button in plupload
        uploader.bind('QueueChanged', function (up, files) {
            //$('a.plupload_start').addClass('plupload_disabled');
            $('a.plupload_start').toggleClass('plupload_disabled', true);

            //alert($('a.plupload_start').hasClass('plupload_disabled'));
        });

        uploader.bind('UploadComplete', function (up, file, res) {

            //alert("all files have been uploaded successfully");

            GetIDs();


        });

        ///////////////////////////////////
        //////////////////////////////////
        // for font uploading  FontUploader

        $("#FontUploader").pluploadQueue({
            // General settings
            runtimes: 'html5,flash',
            url: 'FontUploadHandler.ashx',
            max_file_size: '10mb',
            chunk_size: '1mb',
            unique_names: false,

            headers: { "IDofObject1": IDofObject, "IDofObject2": IDOfObject2 },
            // custom parameters s
            multipart_params: { pid: productID, ptype: PType },
            // Specify what files to browse for  
            filters: [
               { title: "Files (.ttf, .woff, .eot, )", extensions: "ttf,woff,eot" }

            ],



            // this path needs to be consistent with where the page using this script is
            // Flash settings
            flash_swf_url: 'js/plupload/plupload.flash.swf',
            // Silverlight settings
            silverlight_xap_url: 'js/plupload/plupload.silverlight.xap'
        });

        //hide the start upload button because we want to use our Submit Item button
        var uploader1 = $('#FontUploader').pluploadQueue();

        uploader1.bind('Init', function (up, res) {
            // alert("called");
            $('a.plupload_start').css('display', 'none');
            $('.plupload_header').css('display', 'none');
            $('.plupload_content').css('color', 'black');


        });


        //make sure that the start upload button is always disabled
        //because we want our own submit button to take action on the file uploads,
        //not the start button in plupload
        uploader1.bind('QueueChanged', function (up, files) {
            //$('a.plupload_start').addClass('plupload_disabled');
            $('a.plupload_start').toggleClass('plupload_disabled', true);

            //alert($('a.plupload_start').hasClass('plupload_disabled'));
        });

        uploader1.bind('UploadComplete', function (up, file, res) {

            //AddItemToCarousel(productID);
            //  alert(productID);
            alert("Font uploaded sucessfully, it is now availble for use in fonts list.");
            //alert($("input[name=FontName]").val());
            //$("input[name=FontName]").val("");
            // animatedcollapse.toggle('DivUploadFont');
            //animatedcollapse.toggle('textPropertPanel');
            GetIDs();


        });

    }
    else {
        //do something
    }
}


function OnFailed(error, userContext, methodName) {
    if (error !== null) {
        alert("An error occurred: " + error.get_message());
    }
}

function SubmitItem(e) {

    //get a copy of the current uploader
    var uploader = $('#uploader').pluploadQueue();
    var obj = uploader.settings;


    // Validate number of uploaded files
    if (uploader.total.uploaded == 0) {
        // Files in queue upload them first
        if (uploader.files.length > 0) {
            // When all files are uploaded submit form
            uploader.bind('UploadProgress', function () {
                if (uploader.total.uploaded == uploader.files.length)
                    $('form1').submit();

            });

            //now go insert the files into the DB
            uploader.bind('UploadComplete', function (up, f) {

                var obj = up.settings;

                //alert("Upload Complete.");

                for (i = 0; i < f.length; i++) {

                    //alert(f[i].name);
                    //alert("ID: " + f[i].id);
                    //  alert("product id " + f[i].pid);
                    InsertFileRecord(obj.headers.IDofObject1, obj.headers.IDofObject2, f[i].id, f[i].name);

                }



            });
            //  uploader.Refresh();
            uploader.start();
            //   uploader.init();


        }
        else {

            alert("No files to upload"); //take out or do whatever you would like here because there were zero files
        }
    }

}

function VarifyFontNames(font1, font2, font3) {

    var ext1 = font1.substr(font1.lastIndexOf('.') + 1);
    var name1 = font1.replace('.' + ext1, '').toLowerCase();
    var ext2 = font2.substr(font2.lastIndexOf('.') + 1);
    var name2 = font2.replace('.' + ext2, '').toLowerCase();
    var ext3 = font3.substr(font3.lastIndexOf('.') + 1);
    var name3 = font3.replace('.' + ext3, '').toLowerCase();

    ext1 = ext1.toLowerCase();
    ext2 = ext2.toLowerCase();
    ext3 = ext3.toLowerCase();

    if (ext1 == "ttf") {
        if (ext2 == "eot") {
            if (ext3 == "woff") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        } else if (ext2 == "woff") {
            if (ext3 == "eot") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        }

    } else if (ext1 == "eot") {
        if (ext2 == "ttf") {
            if (ext3 == "woff") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        } else if (ext2 == "woff") {
            if (ext3 == "ttf") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        }
    } else if (ext1 == "woff") {
        if (ext2 == "ttf") {
            if (ext3 == "eot") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        } else if (ext2 == "eot") {
            if (ext3 == "ttf") {
                if (name1 == name2 & name1 == name3) {
                    return true;
                }
            }
        }

    }
    return false;
}

/// function that is called on submit font click 
function SubmitFontItem(e) {

    var uploader = $('#FontUploader').pluploadQueue();
    var obj = uploader.settings;
    if (IsFontLegal == true) {
        if (uploader.total.uploaded == 0 && uploader.files.length == 3 && $("input[name=FontName]").val() != "") {
            // Files in queue upload them first
            if (VarifyFontNames(uploader.files[0].name, uploader.files[1].name, uploader.files[2].name)) {

                // When all files are uploaded submit form
                uploader.bind('UploadProgress', function () {
                    if (uploader.total.uploaded == uploader.files.length)
                        $('form2').submit();


                });

                uploader.bind('UploadComplete', function (up, f) {
                    var obj = up.settings;
                    var ext1 = f[0].name.substr(f[0].name.lastIndexOf('.') + 1);
                    var fontName = f[0].name;
                    fontName = fontName.replace('.' + ext1, '')
                    //alert($("input[name=FontName]").val());
                    InsertFontRecord($("input[name=FontName]").val(), fontName);
                    UpdateFontToUI($("input[name=FontName]").val(), fontName);
                    //alert(fontName);
                    $("input[name=FontName]").val("");
                });
                uploader.start();
            }
            else {

                alert("Please Upload the files correctly"); //take out or do whatever you would like here because there were zero files
            }
        }
        else {
            alert("Files are not according to criteria");
        }
    } else {
        alert("Confirm that this font is free from any copyright infringements. ");
    }
}

function UpdateFontToUI(fontName, fontFileName) {
    var path = "Designer/PrivateFonts/FontFace/"
    var html = '<style> @font-face { font-family: ' + fontName + '; src: url(' + path + fontFileName + ".eot" + '); src: url(' + path + fontFileName + ".eot?#iefix" + ') format(" embedded-opentype"), url(' + path + fontFileName + ".woff" + ') format("woff"),  url(' + path + fontFileName + ".ttf" + ') format("truetype");  font-weight: normal; font-style: normal;}</style>';
    $('head').append(html);
    var html1 = '<option  id = ' + fontFileName + ' value="' + fontName + '" >' + fontName + '</option>';
    $('#' + "BtnSelectFonts").append(html1);
}

function InsertFontRecord(fontName, fileName) {
    PageMethods.InsertFontRecord(fontName, fileName);
}

var Count = 0;
function InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName) {
    idOfObject1 = getParameterByName("TemplateID");
    PageMethods.InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName, TemplateID, OnSucceedFileRecord, OnFailed);
    //alert(Count);
    Count += 1;
}


function OnSucceedFileRecord(result, userContext, methodName) {
    UpdateImagesToCarousel(Count);

    //TODO: whatever is needed for your solution
}