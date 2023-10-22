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

});