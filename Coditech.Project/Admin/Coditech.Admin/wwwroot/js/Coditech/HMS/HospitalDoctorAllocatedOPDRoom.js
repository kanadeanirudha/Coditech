var HospitalDoctorAllocatedOPDRoom = {
    Initialize: function () {
        HospitalDoctorAllocatedOPDRoom.constructor();
    },

    constructor: function () {
    },

    GetOrganisationCentrewiseBuildingRooms: function () {
        var organisationCentrewiseBuildingMasterId = $("#OrganisationCentrewiseBuildingMasterId").val();
        if (organisationCentrewiseBuildingMasterId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctorAllocatedOPDRoom/GetOrganisationCentrewiseBuildingRooms",
                data: { "organisationCentrewiseBuildingMasterId": organisationCentrewiseBuildingMasterId},
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#OrganisationCentrewiseBuildingRoomId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Centrewise Building Room", "error")
                    CoditechCommon.HideLodder();
                }
            });

        }
    },
}








