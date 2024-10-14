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

    LoadListByCentreCode: function (controllerName, methodName) {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else {
            CoditechDataTable.LoadList(controllerName, methodName);
        }
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
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Departments.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $('#DataTablesDivId tbody').html('');
            $("#SelectedDepartmentId").html("");
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

    GetHospitalDoctorsListByCentreCodeAndDepartmentId: function () {
        var selectedCentreCode = $("#SelectedCentreCode").val();
        var selectedDepartmentId = $("#SelectedDepartmentId").val();

        if (selectedCentreCode != "" && selectedDepartmentId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/GeneralCommanData/GetHospitalDoctorsList",
                data: { "selectedCentreCode": selectedCentreCode, "selectedDepartmentId": selectedDepartmentId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#HospitalDoctorId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Hospital Doctors List", "error")
                    CoditechCommon.HideLodder();
                }
            });
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
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Region.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#GeneralRegionMasterId").html("");
        }
    },
    GetDistrictListByRegionId: function () {
        var selectedItem = $("#GeneralRegionMasterId").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/GeneralCommanData/GetDistrictListByRegionId",
                data: { "generalRegionMasterId": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#GeneralDistrictMasterId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve District.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#GeneralDistrictMasterId").html("");
        }
    },
    ValidNumeric: function () {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode >= 48 && charCode <= 57) { return true; }
        else { return false; }
    },

    ValidDecimalNumeric: function () {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
            return false;

        return true;
    },

    AvoidSpacing: function () {
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode != 32) {
            return true;
        }
        else {
            return false;
        }
    },
    AllowOnlyAlphabetWithouSpacing: function ()
    {
        debugger;   
        const charCode = event.which || event.keyCode;
        if ((charCode >= 65 && charCode <= 90) || (charCode >= 97 && charCode <= 122) || charCode === 8)
        {
           return true;
        }
           return false;
    },

    SearchDatatableData: function () {
        debugger;
        var charCode = (event.which) ? event.which : event.keyCode;
        if (charCode == 13) {
            $("#DataTables_SearchButton").click();
        }
        else {
            return false;
        }
    },
}
