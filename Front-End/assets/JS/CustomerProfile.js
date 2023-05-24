$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}
	
	var customerId = Cookies.get("customerId")
	$.ajax({
		url:"http://localhost:56213/api/ba_customer/"+customerId,
		method:"GET",
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var customerData = xmlHttp.responseJSON;
				var customer = "<fieldset class='fieldSetBorder' style='width:50%'><table border='0' cellspacing='0' width='50%'>";
				customer += "<tr><td>Full Name</td><td>"+customerData.fullName+"</td></tr>";
				customer += "<tr><td colspan='2'><hr></td></tr>";
				customer += "<tr><td>Username</td><td>"+customerData.userName+"</td></tr>";
				customer += "<tr><td colspan='2'><hr></td></tr>";
				customer += "<tr><td>Email</td><td>"+customerData.email+"</td></tr>";
				customer += "<tr><td colspan='2'><hr></td></tr>";
				customer += "<tr><td>Phone</td><td>"+customerData.phone+"</td></tr>";
				customer += "<tr><td colspan='2'><hr></td></tr>";
				customer += "<tr><td>Address</td><td>"+customerData.address+"</td></tr>";
				customer += "<tr><td colspan='2'><hr></td></tr>";
				customer += "<tr><td>Joining Date</td><td>"+customerData.joiningDate+"</td></tr>";
				customer += "<tr><td colspan='2'><hr></td></tr>";
				customer += "<tr><td>Last Update</td><td>"+customerData.lastUpdated+"</td></tr>";
				customer += "</table></fieldset>";
				$("#showCustomerProfile").html(customer);
			}
			else
			{
				$("#msg").html("No Requestes to show");
			}
		}
	});

	// Cookies.remove("customerId", { path: 'CustomerProfile.html' });
});