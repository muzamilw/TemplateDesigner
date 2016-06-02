function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}


function PointToPixel(Val)
{
        return Val * 96 / 72;
}
function PixelToPoint(Val)
{
        return Val / 96 * 72;
}


function DisplayDiv(divid) {
    if (divid == 1) {
      //  $('#ChangeImgBtn').show();
        $('#AddColorShape').hide();
        $('#AddColorShapeLbl').hide();
        $("#LbImgCrop").css("visibility", "visible");
        $("#BtnCropImg").css("visibility", "visible");
      
    } else if (divid == 2) {
      //  $('#ChangeImgBtn').hide();
        $('#AddColorShape').show();
        $('#AddColorShapeLbl').show();
        $("#LbImgCrop").css("visibility", "hidden");
        $("#BtnCropImg").css("visibility", "hidden");
    }
  
}