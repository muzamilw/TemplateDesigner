/*
developed by saqib ali 
place a div with id= loader  
example  
<!-- Loading Div -->

<div id="loading"  class="loadingLayer">
		
Loading content, please wait..
<img src="loader\loading.gif" alt="loading.." />
</div>
<!-- Loading Div -->
<div class="otherLayer">
there are basically two functions 
1. start loader 
2. Stop Loader

1. Start Loader will basically start loader you should call it when calling data function
2. Stop loader will be called to stop loader it should be called in $(window).load() function 
*/

 function StartLoader(msg)
	{

	    if (msg != undefined) {
	        $("#loadingMsg").innerText = msg;

	    }
	    else {
	        $("#loadingMsg").innerText = "Loading design, please wait..";
	    }
	showLayer();
	$("#loading").show(1000);		
	}
	
	
	
function StopLoader()
	{
	hideLayer();
	$("#loading").hide(1000);	
	}
	
	
/*
function to getbrowser hieght 
function setLayerPosition changes the position of the content using z index
function showLayer basically displayers the loader layer
function hidelayer basically hides the loader la
 */
	

            function getBrowserHeight() { 
                var intH = 0; 
                var intW = 0; 
               

                if(typeof window.innerWidth == 'number' ) { 
                   intH = window.innerHeight; 
                   intW = window.innerWidth; 
                } 
                else if(document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {

                    intH = document.documentElement.clientHeight; 
                    intW = document.documentElement.clientWidth; 
                } 
                else if(document.body && (document.body.clientWidth || document.body.clientHeight)) {

                    intH = document.body.clientHeight; 
                    intW = document.body.clientWidth; 
                } 


                return { width: parseInt(intW), height: parseInt(intH) }; 
            } 


            function setLayerPosition() {
                var shadow = document.getElementById("DivShadow");
                var loader = document.getElementById("DivLoading"); 


                var bws = getBrowserHeight(); 
                shadow.style.width = bws.width + "px"; 
                shadow.style.height = bws.height + "px";

                var left = parseInt((bws.width - 350) / 2);
                var top = parseInt((bws.height - 200) / 2);
                //loader.style.left = parseInt((bws.width - 350) / 2);
                //loader.style.top = parseInt((bws.height - 200) / 2);
                $("#DivLoading").css('left', left);
                $("#DivLoading").css('top', top);
                $("#DivLoading").css('display', '');
                //alert(loader.style.top);

                shadow = null; 
                loader = null; 
            } 


            function showLayer() { 
			
			  $("#Container").css("opacity","0.8");
			   
			   /* edit an transparent layer*/
			   setLayerPosition();


			   var shadow = document.getElementById("DivShadow");
			   var loading = document.getElementById("DivLoading"); 
 

                shadow.style.display = "block"; 
                loading.style.display = "block"; 


                shadow = null; 
                loading = null; 
            
           
            } 
            

            function hideLayer() { 
			 $("#Container").css("opacity","1");
			 
			   /* remove the transparent layer*/
			 var shadow = document.getElementById("DivShadow");
			 var loading = document.getElementById("DivLoading"); 
 

                shadow.style.display = "none"; 
                loading.style.display = "none"; 


                shadow = null; 
                loading = null; 
	
            } 