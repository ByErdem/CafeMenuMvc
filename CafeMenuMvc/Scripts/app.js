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
                // do something with error
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
                // do something with error
            }
        });
    }
}

$(document).ready(function () {

    var x = JSON.parse(window.localStorage.getItem("data")).Data;
    $(".userName").text(x.NAME + " " + x.SURNAME);

    PrintCounts();


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

    //Proje bittiğinde burası aktifleştirilecek
    //setInterval(function () {
    //    PrintCounts();
    //}, 10000);

});