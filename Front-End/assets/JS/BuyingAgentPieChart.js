$(document).ready(function(){

    if(Cookies.get("Login") != 1)
    {
        window.location = "Login.html";
    }

    $.ajax({
        type: "GET",
        url: 'http://localhost:56213/api/purchase_log/pie_chart',
        headers:"Content-Type:application/json",
        headers:{
            "Authorization":"basic "+Cookies.get("Authenticatior")
        },
        success: function (response) {
            successFunc(response);
        }
    });

function successFunc(jsondata) {

    console.log(jsondata.value);
    var myChart = new Chart(document.getElementById('showPieChart').getContext('2d'), {
        type: 'pie',
        data: {
            labels: jsondata['category'],
            datasets: [{
                label: 'Count',
                data: jsondata['value'],
                backgroundColor: [
                  'rgb(255, 99, 132)',
                  'rgb(54, 162, 235)',
                  'rgb(255, 205, 86)'
                ],
                hoverOffset: 4
            }]
        }
    });
}
});

document.getElementById("download").addEventListener('click', function(){
  /*Get image of canvas element*/
  var url_base64jp = document.getElementById("showPieChart").toDataURL("image/jpg");
  /*get download button (tag: <a></a>) */
  var a =  document.getElementById("download");
  /*insert chart image url to download button (tag: <a></a>) */
  a.href = url_base64jp;
});