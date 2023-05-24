$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}
	
	var id = Cookies.get("id");
	var fullName, userName, password, email, phoneNumber, salary, address, joiningDate;
	$.ajax({
		url:"http://localhost:56213/api/buying_agent/"+id,
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var buyingAgentData = xmlHttp.responseJSON;	
				fullName = buyingAgentData.fullName;
				userName = buyingAgentData.userName;
				password = buyingAgentData.password;
				email = buyingAgentData.email;
				phoneNumber = buyingAgentData.phone;
				salary = buyingAgentData.salary;
				address = buyingAgentData.address;
				joiningDate = buyingAgentData.joiningDate; 
			}
			else
			{
				$("#msg").html("No Content");
			}
		}
	});


	$("#save").click(function(){

		// console.log(password);

		var oldPassword = document.getElementById("oldPassword").value;
		var newPassword = document.getElementById("newPassword").value;
		var confirmNewPassword = document.getElementById("confirmNewPassword").value;

		//console.log(newPassword);
		// console.log(confirmNewPassword);

		var flag = false;

		if(oldPassword == "")
		{
			document.getElementById("oldPasswordError").innerHTML = "*Current password required!";
			flag = true;
		}
		else if(oldPassword != password)
		{
			document.getElementById("oldPasswordError").innerHTML = "*Wrong current password!";
			flag = true;	
		}
		if(newPassword == "")
		{
			document.getElementById("newPasswordError").innerHTML = "*New password required!";
			flag = true;
		}
		else if(newPassword.length > 20 || newPassword.length < 8)
		{
			document.getElementById("newPasswordError").innerHTML = "*Password length at least 8 and at most 20 char!";
			flag = true;
		}
		if(confirmNewPassword == "")
		{
			document.getElementById("confirmNewPasswordError").innerHTML = "*Confirm password required!";	
			flag = true;
		}
		else if(confirmNewPassword.length > 20)
		{
			document.getElementById("confirmNewPasswordError").innerHTML = "*Confirm password length at most 20 char!";	
			flag = true;
		}
		else if(newPassword != confirmNewPassword)
		{
			document.getElementById("newPasswordError").innerHTML = "*New and confirm new password mismatch!";
			flag = true;
		}


		if(!flag)
		{

			$.ajax({
				url:"http://localhost:56213/api/buying_agent/update_password/"+id,
				method:"PUT",
				headers:"Content-Type:application/json",
				headers:{
	            	"Authorization":"basic "+Cookies.get("Authenticatior")
	        	},
				data:{
                        "fullName" :fullName,
                        "userName" :userName,
                        "password" :newPassword,
                        "email" :email,
                       	"phone" :phoneNumber,
                       	"salary" :salary,
                       	"address" :address,
                       	"joiningDate" :joiningDate
                    },
				complete:function(xmlHttp,status){
					if(xmlHttp.status == 200)
					{
						$("#msg").html("*Password Changed!"); 
					}
					else
					{
						$("#msg").html(xmlHttp.status);
					}
				}
			});
		}

	});

});


		

