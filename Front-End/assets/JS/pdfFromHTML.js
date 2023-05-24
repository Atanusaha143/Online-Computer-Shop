function HTMLtoPDF(){
  var pdf = new jsPDF('p', 'pt', 'letter');
  source = $('#showPurchaseLog')[0];
  specialElementHandlers = {
  	'#bypassme': function(element, renderer){
  		return true
  	}
  }
  margins = {
      top: 10,
      left: 10,
      width: 1000
    };
  pdf.fromHTML(
    	source // HTML string or DOM elem ref.
    	, margins.left // x coord
    	, margins.top // y coord
    	, {
    		'width': margins.width // max width of content on PDF
    		, 'elementHandlers': specialElementHandlers
    	},
    	function (dispose) {
          pdf.save('purcahse_history.pdf');
        }
    )		
}