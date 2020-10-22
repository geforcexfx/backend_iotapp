// create the Chartist object
$(document).ready(function () {
    $('#checkbox').change(function () {
        console.log("yes")
        $('#chartArea').toggle();
    });
    $('#checkbox1').change(function () {
        console.log("yes")
        $('#chartArea1').toggle();
    });
    $('#checkbox2').change(function () {
        console.log("yes")
        $('#chartArea2').toggle();
    });
    $('#checkbox3').change(function () {
        console.log("yes")
        $('#chartArea3').toggle();
    });
    $('#checkbox4').change(function () {
        console.log("yes")
        $('#chartArea4').toggle();
    });
    $('#checkbox5').change(function () {
        console.log("yes")
        $('#chartArea5').toggle();
    });
    $('#checkbox6').change(function () {
        console.log("yes")
        $('#chartArea6').toggle();
    });
    $('#checkbox7').change(function () {
        console.log("yes")
        $('#chartArea7').toggle();
    });

    
});
var lineChart = new Chartist.Line('#chartArea', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart1 = new Chartist.Line('#chartArea1', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart2 = new Chartist.Line('#chartArea2', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart3 = new Chartist.Line('#chartArea3', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart4 = new Chartist.Line('#chartArea4', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart5 = new Chartist.Line('#chartArea5', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart6 = new Chartist.Line('#chartArea6', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });
var lineChart7 = new Chartist.Line('#chartArea7', {
    labels: [],
    series: [[]]
},
    {
        low: 0,
        showArea: true
    });



// build a signalR HubConnection
var connection = new signalR.HubConnectionBuilder().withUrl("/msgHub").build();

// define behavior when the "ReceiveValue" event is sent from the server
connection.on("ReceiveValue", (value) => {
    console.log(value);
    //if (value.hasOwnProperty('light')) {
      //  console.log("dht22 sensor");
        //console.log("temp: "+value.temperature);
    //}
    var val_obj = JSON.parse(value);
    
    if (!isNaN(val_obj.gas)) {
        console.log("val: ");
        console.log(val_obj);
        lineChart4.data.series[0].push(val_obj.bmetemperature);
        lineChart4.update();
        $("#BMEtemp").text(val_obj.bmetemperature);
        $("#BMEhumid").text(val_obj.humidity);
        $("#pressure").text(val_obj.pressure);
        $("#gas").text(val_obj.gas);
        lineChart5.data.series[0].push(val_obj.humidity);
        lineChart5.update();
        lineChart6.data.series[0].push(val_obj.pressure);
        lineChart6.update();
        lineChart7.data.series[0].push(val_obj.gas);
        lineChart7.update();
    }
    else {
        
        lineChart.data.series[0].push(val_obj.temperature);
        lineChart.update();
        $("#temp").text(val_obj.temperature);
        $("#humid").text(val_obj.humidity);
        $("#light").text(val_obj.light);
        $("#heat").text(val_obj.heat_index);
        lineChart1.data.series[0].push(val_obj.humidity);
        lineChart1.update();
        lineChart2.data.series[0].push(val_obj.light);
        lineChart2.update();
        lineChart3.data.series[0].push(val_obj.heat_index);
        lineChart3.update();
        
    }

});

connection.start();

// Send the value to SendValue method in GraphHub.cs
var sendValue = async function (value,sensor) {
    if (sensor==2) {
        lineChart = new Chartist.Line('#chartArea', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
        lineChart1 = new Chartist.Line('#chartArea1', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
        lineChart2 = new Chartist.Line('#chartArea2', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
        lineChart3 = new Chartist.Line('#chartArea3', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
    }
    if (sensor==1) {
        console.log("gas");
        lineChart4 = new Chartist.Line('#chartArea4', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
        lineChart5 = new Chartist.Line('#chartArea5', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
        lineChart6 = new Chartist.Line('#chartArea6', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
        lineChart7 = new Chartist.Line('#chartArea7', {
            labels: [],
            series: [[]]
        },
            {
                low: 0,
                showArea: true
            });
    }
    console.log("value: ");
    console.log(value);
    await connection.invoke("SendValue", value);
};

// Send the user entered value to GraphHub.SendValue
$("#submitBtn").on("click", () => {
    const value = $("#datapoint").val();
    $.ajax({
        url: "http://localhost:5000/data/getpart/"+value,
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            //console.log(res);
            res.data.forEach((element, index, array) => {
                if (!isNaN(element.temperature)) {
                    console.log("hi");
                    var strJson = JSON.stringify(element)
                    
                    sendValue(strJson,2);
                    $("#datapoint").val("");
                }
                console.log(element.temperature); // 100, 200, 300
                console.log(index); // 0, 1, 2
                //console.log(array); // same myArray object 3 times
            });
            console.log(res.data);
            //alert(res);
        }
    });
    //if (value && !isNaN(value)) {
      //  sendValue(value);
        //$("#datapoint").val("");
    //}
});


// Send a random number to GraphHub.SendValue
$("#randomValueBtn").on("click", () => {
    const value = $("#datapoint").val();
    $.ajax({
        url: "http://localhost:5000/data/getbmepart/" + value,
        type: 'GET',
        dataType: 'json', // added data type
        success: function (res) {
            //console.log(res);
            res.data.forEach((element, index, array) => {
                if (!isNaN(element.bmetemperature)) {
                    console.log("hi");
                    var strJson = JSON.stringify(element);
                    sendValue(strJson,1);
                    $("#datapoint").val("");
                }
                console.log(element.bmeTemperature); // 100, 200, 300
                console.log(index); // 0, 1, 2
                //console.log(array); // same myArray object 3 times
            });
            console.log(res.data);
            //alert(res);
        }
    });

});