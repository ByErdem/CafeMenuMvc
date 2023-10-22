
$(document).ready(function () {

    $(document).on("click", ".btn", function (e) {
        e.preventDefault();

        var user = {
            Username: $(".txtUsername").val(),
            Password: $(".txtPassword").val()
        }

        CallRequest("/Login/SignIn", user, function (rsp) {

            if (rsp.ResultStatus == 0) {
                window.location.replace("/Dashboard");
                window.localStorage.setItem("data", JSON.stringify(rsp));
            }

        });

    });

});