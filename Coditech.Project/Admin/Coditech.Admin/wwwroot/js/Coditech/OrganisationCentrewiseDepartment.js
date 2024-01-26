﻿var OrganisationCentrewiseDepartment = {
    Initialize: function () {
        OrganisationCentrewiseDepartment.constructor();
    },
    constructor: function () {
    },
    GetDepartmentByCentreCode: function () {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else {
            CoditechDataTable.LoadList("OrganisationCentrewiseDepartment", "List");
        }
    },

    GetAssociateUnAssociateCentrewiseDepartment: function (modelPopContentId, organisationCentrewiseDepartmentId, generalDepartmentMasterId, departmentName) {
        var centreCode = $("#SelectedCentreCode").val();
        var centreName = $("#SelectedCentreCode option:selected").text();
        let organisationCentrewiseDepartmentViewModel = {
            OrganisationCentrewiseDepartmentId: organisationCentrewiseDepartmentId,
            GeneralDepartmentMasterId: generalDepartmentMasterId,
            DepartmentName: departmentName,
            CentreCode: centreCode,
            CentreName: centreName
        };
        CoditechCommon.ShowLodder();
        $.ajax(
            {
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/OrganisationCentrewiseDepartment/GetAssociateUnAssociateCentrewiseDepartment",
                data: organisationCentrewiseDepartmentViewModel,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    $('#' + modelPopContentId).html("").html(result);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to display record.", "error");
                    CoditechCommon.HideLodder();
                }
            });
    },
    AssociateUnAssociateCentrewiseDepartment: function () {
        $("#frmAssociateUnAssociateCentrewiseDepartment").submit();
    },
}
