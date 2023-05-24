$(document).ready(function () {
	Cookies.remove("Authenticatior");
    Cookies.remove("Email");
	Cookies.remove("id");
	Cookies.remove("Login");
	window.location = "Login.html";
});