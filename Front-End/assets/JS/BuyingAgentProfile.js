$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}

	$.ajax({
		url:"http://localhost:56213/api/buying_agent/"+Cookies.get("id"),
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var buyingAgentData = xmlHttp.responseJSON;
				var buyingAgent = "<fieldset class='fieldSetBorder' style='width:50%'><table border='0' cellspacing='0' width='50%'>";
				buyingAgent += "<tr><td>Full Name</td><td>"+buyingAgentData.fullName+"</td></tr>";
				buyingAgent += "<tr><td colspan='2'><hr></td></tr>";
				buyingAgent += "<tr><td>Username</td><td>"+buyingAgentData.userName+"</td></tr>";
				buyingAgent += "<tr><td colspan='2'><hr></td></tr>";
				buyingAgent += "<tr><td>Email</td><td>"+buyingAgentData.email+"</td></tr>";
				buyingAgent += "<tr><td colspan='2'><hr></td></tr>";
				buyingAgent += "<tr><td>Phone</td><td>"+buyingAgentData.phone+"</td></tr>";
				buyingAgent += "<tr><td colspan='2'><hr></td></tr>";
				buyingAgent += "<tr><td>Address</td><td>"+buyingAgentData.address+"</td></tr>";
				buyingAgent += "<tr><td colspan='2'><hr></td></tr>";
				buyingAgent += "<tr><td>Joining Date</td><td>"+buyingAgentData.joiningDate+"</td></tr>";
				buyingAgent += "<tr><td colspan='2'><hr></td></tr>";
				buyingAgent += "<tr><td>Last Update</td><td>"+buyingAgentData.lastUpdated+"</td></tr>";
				buyingAgent += "</table><br><br><a href='BuyingAgentProfileEdit.html' class='linkBtn updateBtn'>Edit Profile</a>";
				buyingAgent += "<a href='BuyingAgentChangePassword.html' class='linkBtn yellowtBtn'>Change Password</a></fieldset>";
				$("#showBuyingAgentProfile").html(buyingAgent);
			}
			else
			{
				$("#msg").html("Nothing to show!");
			}
		}


	});

});