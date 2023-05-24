$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}

		$.ajax({
		url:"http://localhost:56213/api/purchase_log",
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var purchaseLog = "<table border='1' cellspacing='0'><tr><th>&nbsp Customer ID &nbsp</th><th>&nbsp Product Name &nbsp</th><th>&nbsp Product Description &nbsp</th><th>&nbsp Buying Price &nbsp</th><th>&nbsp Category &nbsp</th><th>&nbsp Brand &nbsp</th><th>&nbsp Features &nbsp</th><th>&nbsp Quantity &nbsp</th><th>&nbsp Image &nbsp</th><th>&nbsp Purchased Date &nbsp</th></tr>";
				var purchaseLogData = xmlHttp.responseJSON;
				console.log(purchaseLogData.length);
				for (var i = 0; i < purchaseLogData.length; i++) {
					 purchaseLog += "<tr><td>"+purchaseLogData[i].customerId+"</td><td>"+purchaseLogData[i].productName+"</td><td>"+purchaseLogData[i].productDescription+"</td><td>"+purchaseLogData[i].buyingPrice+"</td><td>"+purchaseLogData[i].category+"</td><td>"+purchaseLogData[i].brand+"</td><td>"+purchaseLogData[i].features+"</td><td>"+purchaseLogData[i].quantity+"</td><td><img src='assets/img/Products/"+purchaseLogData[i].images+"' width='250' height='300'></td><td>"+purchaseLogData[i].purchasedDate+"</td></tr>";
				}
				purchaseLog += "</table>"
				$("#showPurchaseLog").html(purchaseLog);
			}
			else
			{
				$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
			}
		}

		});

});



function searchCat()
{
	var searchedElment = document.getElementById('searchBox').value.toLowerCase();
	var flag = false;
		$.ajax({
		url:"http://localhost:56213/api/purchase_log",
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var purchaseLog = "<table border='1' cellspacing='0'><tr><th>&nbsp Customer ID &nbsp</th><th>&nbsp Product Name &nbsp</th><th>&nbsp Product Description &nbsp</th><th>&nbsp Buying Price &nbsp</th><th>&nbsp Category &nbsp</th><th>&nbsp Brand &nbsp</th><th>&nbsp Features &nbsp</th><th>&nbsp Quantity &nbsp</th><th>&nbsp Image &nbsp</th><th>&nbsp Purchased Date &nbsp</th></tr>";
				var purchaseLogData = xmlHttp.responseJSON;
				console.log(purchaseLogData.length);
				for (var i = 0; i < purchaseLogData.length; i++) {
					if(purchaseLogData[i].category.toLowerCase() == searchedElment)
					 {
					 	flag = true;
					 	purchaseLog += "<tr><td>"+purchaseLogData[i].customerId+"</td><td>"+purchaseLogData[i].productName+"</td><td>"+purchaseLogData[i].productDescription+"</td><td>"+purchaseLogData[i].buyingPrice+"</td><td>"+purchaseLogData[i].category+"</td><td>"+purchaseLogData[i].brand+"</td><td>"+purchaseLogData[i].features+"</td><td>"+purchaseLogData[i].quantity+"</td><td><img src='assets/img/Products/"+purchaseLogData[i].images+"' width='250' height='300'></td><td>"+purchaseLogData[i].purchasedDate+"</td></tr>";
					 }
				}
				purchaseLog += "</table>"
				if(flag)
				{
					$("#showPurchaseLog").html(purchaseLog);
				}
				else if(searchedElment == "")
				{
					$("#msg").html("*Please type a category");
				}
				else
				{
					$("#msg").html("*No matches!!");
				}
			}
			else
			{
				$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
			}
		}
});
}


function searchByDate()
{
	var from = document.getElementById('from').value;
	var to = document.getElementById('to').value;
	var today = new Date();
	var dateM = (today.getMonth()+1).toString();
	var dateD = today.getDate().toString();
	if(dateM.length == 1)
	{
		dateM = '0'+dateM;
	}
	if(dateD.length == 1)
	{
		dateD = '0'+dateD;
	}
	var date = today.getFullYear()+'-'+dateM+'-'+dateD;

	// console.log(from);
	// console.log(to);
	// console.log(date);

	if(from == "" || to == "")
	{
		document.getElementById("msg").innerHTML = "*Date range required";	
	}
	else if(to > date || from > date)
	{
		document.getElementById("msg").innerHTML = "*Invalid date range";
	}
	else
	{
		$.ajax({
		url:"http://localhost:56213/api/purchase_log",
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var purchaseLog = "<table border='1' cellspacing='0'><tr><th>&nbsp Customer ID &nbsp</th><th>&nbsp Product Name &nbsp</th><th>&nbsp Product Description &nbsp</th><th>&nbsp Buying Price &nbsp</th><th>&nbsp Category &nbsp</th><th>&nbsp Brand &nbsp</th><th>&nbsp Features &nbsp</th><th>&nbsp Quantity &nbsp</th><th>&nbsp Image &nbsp</th><th>&nbsp Purchased Date &nbsp</th></tr>";
				var purchaseLogData = xmlHttp.responseJSON;
				for (var i = 0; i < purchaseLogData.length; i++) {

					var date = purchaseLogData[i].purchasedDate.slice(0,10);
					// console.log(date);
					if(from <= date && date <= to)
					{
						purchaseLog += "<tr><td>"+purchaseLogData[i].customerId+"</td><td>"+purchaseLogData[i].productName+"</td><td>"+purchaseLogData[i].productDescription+"</td><td>"+purchaseLogData[i].buyingPrice+"</td><td>"+purchaseLogData[i].category+"</td><td>"+purchaseLogData[i].brand+"</td><td>"+purchaseLogData[i].features+"</td><td>"+purchaseLogData[i].quantity+"</td><td><img src='assets/img/Products/"+purchaseLogData[i].images+"' width='250' height='300'></td><td>"+purchaseLogData[i].purchasedDate+"</td></tr>";
					}
				}
				purchaseLog += "</table>"
				$("#showPurchaseLog").html(purchaseLog);
			}
			else
			{
				$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
			}
		}

		});
	}


}

function sortPurchaseData()
{
	var sortBy = document.getElementById("select").value;
	
	$.ajax({
	url:"http://localhost:56213/api/purchase_log/sortBy/"+sortBy,
	headers:{
    	"Authorization":"basic "+Cookies.get("Authenticatior")
	},
	complete:function(xmlHttp,status){
		if(xmlHttp.status == 200)
		{
			var purchaseLog = "<table border='1' cellspacing='0'><tr><th>&nbsp Customer ID &nbsp</th><th>&nbsp Product Name &nbsp</th><th>&nbsp Product Description &nbsp</th><th>&nbsp Buying Price &nbsp</th><th>&nbsp Category &nbsp</th><th>&nbsp Brand &nbsp</th><th>&nbsp Features &nbsp</th><th>&nbsp Quantity &nbsp</th><th>&nbsp Image &nbsp</th><th>&nbsp Purchased Date &nbsp</th></tr>";
			var purchaseLogData = xmlHttp.responseJSON;
			console.log(purchaseLogData.length);
			for (var i = 0; i < purchaseLogData.length; i++) {
				 purchaseLog += "<tr><td>"+purchaseLogData[i].customerId+"</td><td>"+purchaseLogData[i].productName+"</td><td>"+purchaseLogData[i].productDescription+"</td><td>"+purchaseLogData[i].buyingPrice+"</td><td>"+purchaseLogData[i].category+"</td><td>"+purchaseLogData[i].brand+"</td><td>"+purchaseLogData[i].features+"</td><td>"+purchaseLogData[i].quantity+"</td><td><img src='assets/img/Products/"+purchaseLogData[i].images+"' width='250' height='300'></td><td>"+purchaseLogData[i].purchasedDate+"</td></tr>";
			}
			purchaseLog += "</table>"
			$("#showPurchaseLog").html(purchaseLog);
		}
		else
		{
			$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
		}
	}

	});
}