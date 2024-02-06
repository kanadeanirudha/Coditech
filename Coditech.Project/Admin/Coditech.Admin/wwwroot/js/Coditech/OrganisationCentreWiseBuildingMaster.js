var OrganisationCentreWiseBuilding = {
    Initialize: function () {
        OrganisationCentreWiseBuilding.constructor();
    },
    constructor: function () {
    },

    ShowLodder: function () {
        $('.spinner').css('display', 'block');
    },

    HideLodder: function () {
        $('.spinner').css('display', 'none');
    },

    GetOrganisationCentreWiseBuildingByCentreCode: function () {
        var selectedItem = $("#SelectedCentreCode").val();
        $('#DataTablesDivId tbody').html('');
        if (selectedItem != "") {
            OrganisationCentreWiseBuilding.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/OrganisationCentreWiseBuildingMaster/List",
                data: { "centreCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#SelectedDepartmentId").html("").html(data);
                    OrganisationCentreWiseBuilding.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    RARIndiaNotification.DisplayNotificationMessage("Failed to retrieve Departments.", "error")
                    OrganisationCentreWiseBuilding.HideLodder();
                }
            });
        }
        else {
            $('#DataTablesDivId tbody').html('');
            $("#SelectedDepartmentID").html("");
        }
    }

}
