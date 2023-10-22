$(document).ready(function () {
    $(document).on("click", ".btnUpdate", function (e) {
        e.preventDefault();
        console.log("Update");
    });

    $(document).on("click", ".btnDelete", function (e) {
        e.preventDefault();
        console.log("Delete");
    });
});