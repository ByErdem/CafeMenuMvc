$(document).ready(function () {

    function DeleteCategory(e) {

        var categoryid = $(e).attr("categoryid");

        var data = {
            "CATEGORYID": Number(categoryid)
        }

        CallRequest("/Category/Delete", data, function (rsp) {
            var tr = $(e).parent().parent();
            $(tr).remove();
            PrintCounts();
        });
    }

    $('.select2').select2({
        placeholder: 'Choose one',
        searchInputPlaceholder: 'Search'
    });

    $(document).on("click", ".btnSave", function (e) {
        e.preventDefault();
        console.log(e);
        var formData = $('#frmCategory').serializeArray();

        var data = {
            "CATEGORYNAME": formData[0].value,
            "PARENTCATEGORYID": formData[1].value,
        }

        $("#FrmNewCategory").modal("hide");

        CallRequest("/Category/Create", data, function (rsp) {

            var PARENTCATEGORYNAME = rsp.Data.PARENTCATEGORYNAME ?? "";
            var CREATEDDATE = ParseDate(rsp.Data.CREATEDDATE);
            var CATEGORYID = rsp.Data.CATEGORYID;

            var newRow = "<tr><td>" + rsp.Data.CATEGORYNAME + "</td><td>" + PARENTCATEGORYNAME + "</td><td>" + CREATEDDATE + "</td>";
            newRow += '<td><button categoryid="' + CATEGORYID + '" type="button" class="btn btn-danger btnUpdate" style="color:white;"><i class="fa fa-edit"></i></button>&nbsp;'
            newRow += '<button categoryid="' + CATEGORYID + '" type="button" class="btn btn-danger btnDelete" style="color:white;"><i class="far fa-trash-alt"></i></button></td></tr>';

            var NoData = $.find("#NoData");
            if (NoData.length != 0) {
                $(NoData[0]).remove();
            }

            $('#TblCategory > tbody:last-child').append(newRow);

            $(document).on("click", ".btnDelete" + CATEGORYID, function (e) {
                DeleteCategory(e);
            });

            PrintCounts();

        });
    });

    $(document).on("click", ".btnUpdate", function (e) {
        console.log("Update");
    });

    $(document).on("click", ".btnDelete", function (e) {
        e.stopPropagation();
        DeleteCategory(this);
    });

});