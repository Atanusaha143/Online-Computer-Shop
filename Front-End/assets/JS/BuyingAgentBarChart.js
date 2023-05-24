$(document).ready(function(){

    if(Cookies.get("Login") != 1)
    {
        window.location = "Login.html";
    }

    $.ajax({
        type: "GET",
        url: 'http://localhost:56213/api/purchase_log/bar_chart',
        headers:"Content-Type:application/json",
        headers:{
            "Authorization":"basic "+Cookies.get("Authenticatior")
        },
        success: function (response) {
            successFunc(response);
        }
    });

function successFunc(jsondata) {

    var myChart = new Chart(document.getElementById('showBarChart').getContext('2d'), 
    {
        type: 'bar',
        data: {
            labels: jsondata['category'],
            datasets: [{
                label: 'Category Wise Total Purcahse',
                data: jsondata['value'],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)',
                    'rgba(1, 192, 112, 0.2)',
                    'rgba(1, 102, 215, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)',
                    'rgba(1, 192, 112, 1)',
                    'rgba(1, 102, 215, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
});

document.getElementById("download").addEventListener('click', function(){
  var url_base64jp = document.getElementById("showBarChart").toDataURL("image/jpg", 1);
  var a =  document.getElementById("download");
  a.href = url_base64jp;
});