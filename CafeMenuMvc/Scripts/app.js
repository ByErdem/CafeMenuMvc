function CallRequest(link, data, event) {

    if (data != undefined) {
        $.ajax({
            type: "POST",
            url: link,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            success: function (rsp) {
                event(rsp);
            },
            error: function (req, status, error) {

            }
        });
    }
    else {
        $.ajax({
            type: "POST",
            url: link,
            contentType: 'application/json; charset=utf-8',
            success: function (rsp) {
                event(rsp);
            },
            error: function (req, status, error) {

            }
        });
    }
}

function ParseDate(date) {
    var timeStamp = date.match(/\d+/)[0];
    var timeStampNumber = parseInt(timeStamp, 10);
    var result = new Date(timeStampNumber);
    return FormatDate(result);
}

function FormatDate(date) {
    var day = date.getDate().toString().padStart(2, '0');
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var year = date.getFullYear();
    var hour = date.getHours().toString().padStart(2, '0');
    var minute = date.getMinutes().toString().padStart(2, '0');
    var seconds = date.getSeconds().toString().padStart(2, '0');

    return `${day}.${month}.${year} ${hour}:${minute}:${seconds}`;
}

function PrintCounts() {

    var ProductCount = $.find("#ProductCount");
    var CategoryCount = $.find("#CategoryCount");
    var UserCount = $.find("#UserCount");

    if (ProductCount.length != 0) {
        CallRequest("/Dashboard/GetCounts", null, function (rsp) {
            $(ProductCount[0]).text(rsp.Data.ProductCount)
            $(CategoryCount[0]).text(rsp.Data.CategoryCount)
            $(UserCount[0]).text(rsp.Data.UserCount)
        });
    }
}


$(document).ready(function () {

    var x = JSON.parse(window.localStorage.getItem("data")).Data;
    $(".userName").text(x.NAME + " " + x.SURNAME);

    PrintCounts();

    //Proje bittiğinde burası aktifleştirilecek
    //setInterval(function () {
    //    PrintCounts();
    //}, 10000);

});