$(document).ready(function(){

	if(Cookies.get("Login") != 1)
	{
		window.location = "Login.html";
	}

	$("#send").click(function(){

		var text = document.getElementById('message').value;
		if(text == '')
		{
			document.getElementById('msg').innerHTML = "Please write something...";
		}
		else
		{
			document.getElementById('msg').innerHTML = "Message sent...";
		}

	});

});