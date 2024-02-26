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
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Departments.", "error")
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
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Building.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
    },


    GetOrganisationCentrewiseBuildingAndRoomByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
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
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Building.", "error")
                    CoditechCommon.HideLodder();
                }
            });

            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetOrganisationCentrewiseBuildingRoomByCentreCode",
                data: { "centreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#OrganisationCentrewiseBuildingRoomId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Building Room.", "error")
                    CoditechCommon.HideLodder();
                }
            });

        }
    },


    GetDepartmentAndEmployeeListByCentreCode: function () {
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
                        RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Departments.", "error")
                        CoditechCommon.HideLodder();
                    }
                });

            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctors/GetEmployeeList",
                data: { "selectedCentreCode": selectedCentreCode, "selectedDepartmentId": selectedDepartmentId,selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#EmployeeId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Employee List", "error")
                    CoditechCommon.HideLodder();
                }
            });
         
        }
    },

}








