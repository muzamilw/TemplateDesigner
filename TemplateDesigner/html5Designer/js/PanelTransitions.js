
// javascript function to show panel using fade out function
// transition are slow , linear and circular

var id;

function ShowPanel(panelID) {
    if ($(panelID).is(":visible")) {
        return;
    }
    else {
        HidePanel(id);
        var p = panelID
        var t = setTimeout(showP(p), 3000);

    }
    
}

function showP(panelID) {
    //  alert("called " + panelID);
    p = panelID;
    var panelID = "#" + panelID;
   
        $(panelID).show("slide", { direction: "up" }, 1000);
        id = p;

}

function show(panelID) {
}

// javascript function to show panel using fade out function
function HidePanel(panelID )
{
	var PanelID = "#" + panelID ;
	if ($(PanelID).is(":visible"))
  	{
 		$(PanelID ).hide("slide", { direction: "up" }, 1000);
 	}
	else
  	{
 		return;
  	}

};

// you can call it by using different dynamic id by default it is set to panels
function HideAllPanels(ParentID) {
    ParentID = "#" + ParentID;
    $("#panels").children().hide();
}