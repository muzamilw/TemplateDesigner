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
    p = panelID;
    var panelID = "#" + panelID;
        $(panelID).show("slide", { direction: "up" }, 1000);
        id = p;
}
function show(panelID) {
}
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
function HideAllPanels(ParentID) {
    ParentID = "#" + ParentID;
    $("#panels").children().hide();
}