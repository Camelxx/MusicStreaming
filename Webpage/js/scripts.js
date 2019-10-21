if (!String.prototype.supplant) {
	String.prototype.supplant = function (o) {
		return this.replace(/{([^{}]*)}/g,
			function (a, b) {
				var r = o[b];
				return typeof r === 'string' || typeof r === 'number' ? r : a;
			}
		);
	};
}



window.onload = async function () {
	try {
            this.console.log('onload Running')
            GetVideoFilesName();	
	} catch (error) {
		console.log('VideoScript Error : ' + error);
	}	
};


const WebApiHost = 'http://localhost:8040/api/video/'; 

var divBody = document.getElementById('element')


var tempDiv = '<button type="button" onclick="OnVideoSelected({VideoFullName})">{VideoFullName}</button>';

    

function GetVideoFilesName()
{
    var url = WebApiHost + 'GetAllVideosFilename';
    console.log('====> 1.1 --> GetVideoFilesName Running ');


    return fetch(url,{     
        headers:{
            'Authorization-Key': 'DiroChartingTV',
        },      
      }).then(data => data.json())
      .then(json => {
        console.log('====> 1.1 --> User Symbol received ');

        if(json){      

		var _data = JSON.parse(json);

		_data.forEach(element => {


            AddOrReplaceStock(element, tempDiv);


		
        });        
    }

})



function AddOrReplaceStock(videoFile, template) {
	//var child = createStockNode(stock, type, template);

	// try to replace
	var stockNode = document.getElementById(videoFile.VideoFullName); //document.querySelector(type + "[data-symbol=" + stock.n + "]");
	if (stockNode) {

		//Get prev last price by id = lastId
		var prevLast = document.getElementById(stock.lastId).textContent;

		//Check if last needed to update
		if(prevLast != stock.c){
			//Update last Price
			document.getElementById(stock.lastId).textContent=stock.c;
			//document.getElementById(stock.lastId).setAttribute("class", "last plus-bg");
		}

		//Get prev change  by id = changeId
		var prevChange = document.getElementById(stock.changeId);

		//Check if Change needed to update
		if(prevChange != stock.chg){
			//Update change			
			document.getElementById(stock.changeId).textContent = stock.chg;

			//Set front Colour
			if(stock.chg.includes('-')){
				document.getElementById(stock.changeId).style.color = "#ef5350";	//Red			
			}
			else{
				document.getElementById(stock.changeId).style.color = "#26a69a";	//Green				
			}
		}

	
		var prevPreChane = document.getElementById(stock.preChange).textContent;

		//Check if Change needed to update
		if(prevPreChane != stock.mychange){				
			document.getElementById(stock.preChange).textContent = stock.mychange;

				//Set front Colour
				if(stock.chg.includes('-')){
					document.getElementById(stock.preChange).style.color = "red";				
				}
				else{
					document.getElementById(stock.preChange).style.color = "Green";				
				}
		}
	





		

	



		//document.getElementById(stock.n).remove();
		//this.divBody.innerHTML += stockNode.innerHTML.supplant(stock);
		//table.replaceChild(child, stockNode);
	} else {
		// add new stock
	   this.divBody.innerHTML += template.supplant(videoFile);// += this.tempDiv;
	}



}









}




function OnVideoSelected(videoName){

    console.log('OnVideoSelected is running {0} ' , videoName);



}
