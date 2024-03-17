var HospitalDoctors = {
    Initialize: function () {
        HospitalDoctors.constructor();
    },

    constructor: function () {
    },

    GetDepartmentAndBuildingByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetDepartmentsByCentreCode",
                data: { "centreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#SelectedDepartmentId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Departments.", "error")
                    CoditechCommon.HideLodder();
                }
            });

            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetOrganisationCentrewiseBuildingByCentreCode",
                data: { "centreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#OrganisationCentrewiseBuildingMasterId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Building.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
    },


    GetOrganisationCentrewiseRoomByBuildingId: function () {
        var selectedItem = $("#OrganisationCentrewiseBuildingMasterId").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetOrganisationCentrewiseRoomByBuildingId",
                data: { "buildingMasterId": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#OrganisationCentrewiseBuildingRoomId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Building.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
    },


    GetEmployeeListByCentreCodeAndDepartmentId: function () {
        var selectedCentreCode = $("#SelectedCentreCode").val();
        var selectedDepartmentId = $("#SelectedDepartmentId").val();

        if (selectedCentreCode != "" && selectedDepartmentId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetEmployeeList",
                data: { "selectedCentreCode": selectedCentreCode, "selectedDepartmentId": selectedDepartmentId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#EmployeeId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Employee List", "error")
                    CoditechCommon.HideLodder();
                }
            });

        }
    },

}








