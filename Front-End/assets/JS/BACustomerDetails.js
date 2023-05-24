var customerId = GetParameterValues('customerId');      
    function GetParameterValues(param) {  
        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');  
        for (var i = 0; i < url.length; i++) {  
            var urlparam = url[i].split('=');  
            if (urlparam[0] == param) {  
                return urlparam[1];  
            }  
        }  
    }

$(document).ready(function(){
	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}
	Cookies.set("customerId", customerId);
	$.ajax({
		url:"http://localhost:56213/api/ba_customer/purchase_history/"+customerId,
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var purchaseProducts = "<table border='1' cellspacing='0'><tr><th>&nbsp Product Name &nbsp</th><th>&nbsp Product Description &nbsp</th><th>&nbsp Buying Price &nbsp</th><th>&nbsp Category &nbsp</th><th>&nbsp Brand &nbsp</th><th>&nbsp Features &nbsp</th><th>&nbsp Quantity &nbsp</th><th>&nbsp Image &nbsp</th><th>&nbsp Purchased Date &nbsp</th></tr>";
				var purchaseProductsData = xmlHttp.responseJSON;
				console.log(purchaseProductsData.length);
				for (var i = 0; i < purchaseProductsData.length; i++) {
					 	purchaseProducts += "<td align='center'>"+purchaseProductsData[i].productName+"</td><td>"+purchaseProductsData[i].productDescription+"</td><td>"+purchaseProductsData[i].buyingPrice+"</td><td>"+purchaseProductsData[i].category+"</td><td>"+purchaseProductsData[i].brand+"</td><td>"+purchaseProductsData[i].features+"</td><td>"+purchaseProductsData[i].quantity+"</td><td><img src='assets/img/Products/"+purchaseProductsData[i].images+"' width='250' height='300'></td><td>"+purchaseProductsData[i].purchasedDate+"</td></tr>";
				}
				purchaseProducts += "</table>"
				$("#showPurchaseProducts").html(purchaseProducts);
			}
			else
			{
				$("#msg").html("Nothing to show");
			}
		}

	});

});