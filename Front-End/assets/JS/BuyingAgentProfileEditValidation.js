$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}
	
	var id = Cookies.get("id");
	var password;
	var salary;
	var joiningDate;

	$.ajax({
		url:"http://localhost:56213/api/buying_agent/"+id,
		headers:{
        	"Authorization":"basic "+Cookies.get("Authenticatior")
    	},
		complete:function(xmlHttp,status){
			if(xmlHttp.status == 200)
			{
				var buyingAgentData = xmlHttp.responseJSON;	
				document.getElementById("fullName").value = buyingAgentData.fullName;
				document.getElementById("userName").value = buyingAgentData.userName;
				document.getElementById("email").value = buyingAgentData.email;
				document.getElementById("phoneNumber").value = buyingAgentData.phone;
				document.getElementById("address").value = buyingAgentData.address;
				password = buyingAgentData.password;
				salary = buyingAgentData.salary;
				joiningDate = buyingAgentData.joiningDate;
			}
			else
			{
				$("#msg").html("No Content");
			}
		}
	});


	$("#update").click(function(){

		var fullName = document.getElementById("fullName").value;
		var userName = document.getElementById("userName").value;
		var email = document.getElementById("email").value;
		var phoneNumber = document.getElementById("phoneNumber").value;
		var address = document.getElementById("address").value;
		var oldPassword = document.getElementById("oldPassword").value;
		var flag = false;

		console.log(oldPassword);

		if(fullName == "")
		{
			document.getElementById("nameError").innerHTML = "*Name required!";
			flag = true;
		}
		else if(fullName != "")
		{
			for (var i=0; i <fullName.length ; i++) 
			{
				if (fullName.charCodeAt(i) >= 48 && fullName.charCodeAt(i) <= 57 ) 
				{
					document.getElementById('nameError').innerHTML = "*Name must not contain other than char";
					flag = true;
				}
			}
		}
		else if(fullName.length > 50)
		{
			document.getElementById("nameError").innerHTML = "*Name length at most 50 char!";
			flag = true;
		}
		if(userName == "")
		{
			document.getElementById("userNameError").innerHTML = "*User name required!";
			flag = true;
		}
		else if(userName.length > 50)
		{
			document.getElementById("userNameError").innerHTML = "*Username length at most 50 char!";
			flag = true;
		}
		if(email == "")
		{
			document.getElementById("emailError").innerHTML = "*Email required!";	
			flag = true;
		}
		else if(email != "")
		{
			let at = 0;
			let dot = 0;
			for (var i=0; i <email.length ; i++) 
			{ 
				if (email[i] == '@') 
				{
					at++;
				}
				else if (email[i] == '.')
				{
					dot++;
				}
			}
			if(at != 1 || dot == 0) 
			{
				document.getElementById('emailError').innerHTML = "Email is not in proper format"; 
				flag = true;
			}
		}
		else if(email.length > 50)
		{
			document.getElementById("emailError").innerHTML = "*Email length at most 50 char!";	
			flag = true;
		}
		if(phoneNumber == "")
		{
			document.getElementById("phoneError").innerHTML = "*Phone number required!";	
			flag = true;
		}
		else if(phoneNumber != "")
		{
			for (var i=0; i < phoneNumber.length; i++) 
			{ 
				if(phoneNumber[i]=='0' || phoneNumber[i]=='1' || phoneNumber[i]=='2' || phoneNumber[i]=='3' || 
					phoneNumber[i]=='4' || phoneNumber[i]=='5' || phoneNumber[i]=='6' || phoneNumber[i]=='7' || 
					phoneNumber[i]=='8' || phoneNumber[i]=='9') { continue; }
				else
				{ 
					document.getElementById('phoneError').innerHTML = "*Phone Number must be between 0 - 9";  
					flag = true;
					break; 
				}
			}
		}
		else if(phoneNumber.length != 11)
		{
			document.getElementById("phoneError").innerHTML = "*11 digits must!";	
			flag = true;
		}
		if(address == "")
		{
			document.getElementById("addressError").innerHTML = "*Address required!";
			flag = true;
		}
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

		if(!flag)
		{
			var id = 4;
			$.ajax({
				url:"http://localhost:56213/api/buying_agent/"+id,
				method:"PUT",
				headers:"Content-Type:application/json",
				headers:{
	            	"Authorization":"basic "+Cookies.get("Authenticatior")
	        	},
				data:{
                        "fullName" :fullName,
                        "userName" :userName,
                        "passWord" :password,
                        "email" :email,
                       	"phone" :phoneNumber,
                       	"salary":salary,
                       	"joiningDate":joiningDate,
                        "address":address
                    },
				complete:function(xmlHttp,status){
					if(xmlHttp.status == 200)
					{
						window.location.replace("BuyingAgentProfile.html"); 
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


		

