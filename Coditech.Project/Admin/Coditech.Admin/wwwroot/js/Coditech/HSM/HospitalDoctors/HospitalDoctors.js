var HospitalDoctors = {
    Initialize: function () {
        HospitalDoctors.constructor();
    },

    constructor: function () {
    },
    GetEmployeeMasterByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetEmployeeMasterByCentreCode",
                data: { "selectedCentreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#EmployeeId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve City.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#EmployeeId").html("");
        }
    },

    GetOrganisationCentrewiseBuildingRoomsByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetOrganisationCentrewiseBuildingRoomsByCentreCode",
                data: { "selectedCentreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#OrganisationCentrewiseBuildingRoomId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve City.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#OrganisationCentrewiseBuildingRoomId").html("");
        }
    },

    GetOrganisationCentrewiseBuildingByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetOrganisationCentrewiseBuildingByCentreCode",
                data: { "selectedCentreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#OrganisationCentrewiseBuildingMasterId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve City.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#OrganisationCentrewiseBuildingMasterId").html("");
        }
    },
}
