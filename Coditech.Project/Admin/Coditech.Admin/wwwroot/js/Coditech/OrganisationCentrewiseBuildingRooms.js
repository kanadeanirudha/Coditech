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
}
