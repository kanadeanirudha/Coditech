var OrganisationCentrewiseEmailTemplate = {
    Initialize: function () {
        OrganisationCentrewiseEmailTemplate.constructor();
    },
    constructor: function () {
    },


    GetEmailTemplateByCentreCode: function (centreCode) {
        var selectedItem = $(".EmailTemplate_" + centreCode).val();
        if (selectedItem != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/GeneralCommanData/GetEmailTemplateByCentreCode",
                data: { "emailTemplate": selectedItem },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $(".EmailTemplate_" + centreCode).html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Email.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $(".EmailTemplate_" + centreCode).html("");
        }
    },
}