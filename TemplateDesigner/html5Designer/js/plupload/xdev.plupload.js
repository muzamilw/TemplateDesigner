var IDofObject = "";
var IDOfObject2 = "";

$(function () {
    GetIDs();
});

function GetIDs() {

    //alert("Inside GetIDs");

    PageMethods.GetIDs(OnGetIDSucceeded, OnFailed);
}

function OnGetIDSucceeded(result, userContext, methodName) {

    //alert("Inside OnGetIDSucceeded");

    if (result != "") {

        //alert("id: " + result);

        var ids = result.split('|');
               
        //alert("id 0: " + ids[0]);
        //alert("id 1: " + ids[1]);

        IDofObject = ids[0];
        IDOfObject2 = ids[1];

        $("#uploader").pluploadQueue({
            // General settings
            runtimes: 'flash,silverlight,html4,html5',
            url: 'UploadHandler.ashx', 
            max_file_size: '10mb',
            chunk_size: '1mb',
            unique_names: true,
            headers: { "IDofObject1": IDofObject, "IDofObject2": IDOfObject2 },

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

            $('a.plupload_start').css('display', 'none');
               
        });

        //make sure that the start upload button is always disabled
        //because we want our own submit button to take action on the file uploads,
        //not the start button in plupload
        uploader.bind('QueueChanged', function (up, files) {
            //$('a.plupload_start').addClass('plupload_disabled');
            $('a.plupload_start').toggleClass('plupload_disabled', true);

            //alert($('a.plupload_start').hasClass('plupload_disabled'));
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

    //alert("Local js variable ID1: " + IDofObject);
    //alert("Local js variable ID2: " + IDOfObject2);

    //alert("Header Object1: " + obj.headers.IDofObject1);
    //alert("Header Object2: " + obj.headers.IDofObject2);

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

                    //alert("Name: " + f[i].name);
                    //alert("ID: " + f[i].id);

                    InsertFileRecord(obj.headers.IDofObject1, obj.headers.IDofObject2, f[i].id, f[i].name);
                }

               

            });

            uploader.start();
        }
        else {

            alert("No files to upload"); //take out or do whatever you would like here because there were zero files
        }
    }

}

function InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName) {

    PageMethods.InsertFileRecord(idOfObject1, idOfObject2, fileID, fileName, OnSucceedFileRecord, OnFailed);
}

function OnSucceedFileRecord(result, userContext, methodName) {

    //TODO: whatever is needed for your solution
}