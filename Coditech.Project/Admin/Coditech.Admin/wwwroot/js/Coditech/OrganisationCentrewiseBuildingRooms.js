var OrganisationCentrewiseBuildingRooms = {
    Initialize: function () {
        OrganisationCentrewiseBuildingRooms.constructor();
    },

    constructor: function () {
    },

    GetOrganisationCentrewiseBuildingRoomsByCentreCode: function () {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else {
            CoditechDataTable.LoadList("OrganisationCentrewiseBuildingRooms", "List");
        }
    },

    GetOrganisationCentrewiseBuildingByCentreCode: function () {
        var selectedItem = $("#CentreCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/OrganisationCentrewiseBuildingRooms/GetOrganisationCentrewiseBuildingByCentreCode",
                data: { "centreCode": selectedItem },
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
