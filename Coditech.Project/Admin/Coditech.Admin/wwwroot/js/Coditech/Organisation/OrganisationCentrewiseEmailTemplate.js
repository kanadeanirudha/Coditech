var OrganisationCentrewiseEmailTemplate = {
    Initialize: function () {
        OrganisationCentrewiseEmailTemplate.constructor();
    },
    constructor: function () {
    },


    GetEmailTemplateByCentreCode: function (organisationCentreMasterId) {
        var selectedItem = $("#EmailTemplateCode").val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/OrganisationCentreMaster/GetEmailTemplateByCentreCode",
                data: { "organisationCentreId": organisationCentreMasterId, "emailTemplateCode": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#emailTemplateId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Email.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#emailTemplateId").html("");
        }
    },
}