var CoditechCommon = {
    Initialize: function () {
        CoditechCommon.constructor();
    },
    constructor: function () {
    },

    ShowLodder: function () {
        $('.spinner').css('display', 'block');
    },

    HideLodder: function () {
        $('.spinner').css('display', 'none');
    },

    GetDepartmentByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        $('#DataTablesDivId tbody').html('');
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/GeneralCommanData/GetDepartmentsByCentreCode",
                data: { "centreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#SelectedDepartmentId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Departments.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $('#DataTablesDivId tbody').html('');
            $("#SelectedDepartmentID").html("");
        }
    },
    LoadListByCentreCodeAndDepartmentId: function (controllerName, methodName) {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else if ($("#SelectedDepartmentId").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select department.", "error");
        }
        else {
            CoditechDataTable.LoadList(controllerName, methodName);
        }
    },

    GetRegionListByCountryId: function () {
        var selectedItem = $("#GeneralCountryMasterId").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/GeneralCommanData/GetRegionListByCountryId",
                data: { "generalCountryMasterId": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#GeneralRegionMasterId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Region.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#GeneralRegionMasterId").html("");
        }
    },

    ValidNumeric: function () {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode >= 48 && charCode <= 57) { return true; }
        else { return false; }   
    },
}
