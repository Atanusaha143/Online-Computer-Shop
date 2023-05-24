$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}

	var email = Cookies.get("Email");
	$.ajax({
		url:"http://localhost:56213/api/buying_agent/",
		method:"GET",
		data:{
			"email": email
		},
		headers:{
            "Authorization":"basic "+Cookies.get("Authenticatior")
        },
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var buyingAgentData = xmlHttp.responseJSON;
				Cookies.set("id",buyingAgentData.id);
				console.log(buyingAgentData.id);
			}
			else
			{
				$("#msg").html("No Content");
			}
		}


	});

	$.ajax({
		url:"http://localhost:56213/api/old_product",
	    headers:{
            "Authorization":"basic "+Cookies.get("Authenticatior")
        },
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var flag = false;
				var oldProducts = "<table border='1' id='oldProductsList' cellspacing='0'><tr><th>Customer ID</th><th>Product Name</th><th>Product Description</th><th>Requested Price</th><th>Category</th><th>Brand</th><th>Features</th><th>Quantity</th><th>Image</th><th>Options</th></tr>";
				var oldProductsData = xmlHttp.responseJSON;
				var accept = "Accept";
				var reject = "Reject";
				// console.log(oldProductsData.length);
				for (var i = 0; i < oldProductsData.length; i++) {
					if(oldProductsData[i].status == "Pending")
					 {
					 	flag = true;
					 	oldProducts += "<tr><td align='center'><a href='BuyingAgentCustomer.html?customerId="+oldProductsData[i].customerId+"' class='linkBtn'>"+oldProductsData[i].customerId+"</a></td><td>"+oldProductsData[i].productName+"</td><td>"+oldProductsData[i].productDescription+"</td><td>"+oldProductsData[i].buyingPrice+"</td><td>"+oldProductsData[i].category+"</td><td>"+oldProductsData[i].brand+"</td><td>"+oldProductsData[i].features+"</td><td>"+oldProductsData[i].quantity+"</td><td><img src='assets/img/Products/"+oldProductsData[i].images+"'width='250' height='300'></td>";
					 	oldProducts += "<td><a href='BuyingAgentHome.html?acceptId="+oldProductsData[i].id+"'class='linkBtn acceptBtn'>"+accept+"</a><br><br><a href='BuyingAgentHome.html?rejectId="+oldProductsData[i].id+"'class='linkBtn logoutBtn''>"+reject+"</a></td><br></tr>";
					 }
				}
				oldProducts += "</table>"
				if(flag)
				{
					$("#showOldProducts").html(oldProducts);
				}
				else
				{
					$("#msg").html("Nothing to show!");
				}
			}
			else
			{
				$("#msg").html("");
			}
		}


	});

    	var aId = GetParameterValues('acceptId');
    	var rId = GetParameterValues('rejectId');      
	    function GetParameterValues(param) {  
	        var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');  
	        for (var i = 0; i < url.length; i++) {  
	            var urlparam = url[i].split('=');  
	            if (urlparam[0] == param) {  
	                return urlparam[1];  
	            }  
	        }  
	    }

	    if(aId)
	    {
	    	$.ajax({
			url:"http://localhost:56213/api/old_product/accept/"+aId,
			method:"POST",
			headers:"Content-Type:application/json",
			headers:{
            	"Authorization":"basic "+Cookies.get("Authenticatior")
        	},
			complete:function(xmlHttp,status){
				if(xmlHttp.status == 201)
				{
					window.location.href = "BuyingAgentHome.html";
					$("#msg").html("Item added in Purchase Log");	
				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}

			});
	    }
	    else if(rId)
	    {
	    	$.ajax({
			url:"http://localhost:56213/api/old_product/reject/"+rId,
			method:"PUT",
			headers:"Content-Type:application/json",
			headers:{
            	"Authorization":"basic "+Cookies.get("Authenticatior")
        	},
			complete:function(xmlHttp,status){
				if(xmlHttp.status == 200)
				{
					window.location.href = "BuyingAgentHome.html";
					$("#msg").html("Item rejected from Old Products");	
				}
				else
				{
					$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
				}
			}

			});
	    }

});

function searchCategory()
{
	var searchedElment = document.getElementById('searchBoxHome').value.toLowerCase();
	var flag = false;
		$.ajax({
		url:"http://localhost:56213/api/old_product",
		headers:{
            	"Authorization":"basic "+Cookies.get("Authenticatior")
        },
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var oldProducts = "<table border='1' id='oldProductsList' cellspacing='0'><tr><th>Customer ID</th><th>Product Name</th><th>Product Description</th><th>Requested Price</th><th>Category</th><th>Brand</th><th>Features</th><th>Quantity</th><th>Image</th><th>Options</th></tr>";
				var oldProductsData = xmlHttp.responseJSON;
				var accept = "Accept";
				var reject = "Reject";
				for (var i = 0; i < oldProductsData.length; i++) {
					if(oldProductsData[i].category.toLowerCase() == searchedElment && oldProductsData[i].status == "Pending")
					 {
					 	flag = true;
					 	oldProducts += "<tr><td>"+oldProductsData[i].customerId+"</td><td>"+oldProductsData[i].productName+"</td><td>"+oldProductsData[i].productDescription+"</td><td>"+oldProductsData[i].buyingPrice+"</td><td>"+oldProductsData[i].category+"</td><td>"+oldProductsData[i].brand+"</td><td>"+oldProductsData[i].features+"</td><td>"+oldProductsData[i].quantity+"</td>"+"</td><td><img src='assets/img/Products/"+oldProductsData[i].images+"'width='250' height='300'></td>";;
					 	oldProducts += "<td><a href='BuyingAgentHome.html?acceptId="+oldProductsData[i].id+"'class='linkBtn acceptBtn'>"+accept+"</a><br><br><a href='BuyingAgentHome.html?rejectId="+oldProductsData[i].id+"'class='linkBtn logoutBtn''>"+reject+"</a></td><br></tr>";
					 }
				}
				oldProducts += "</table>"
				if(flag)
				{
					$("#showOldProducts").html(oldProducts);
				}
				else
				{
					$("#msg").html("No matches!!");
				}
			}
			else
			{
				$("#msg").html(xmlHttp.status+":"+xmlHttp.statusText);
			}
		}

});
}