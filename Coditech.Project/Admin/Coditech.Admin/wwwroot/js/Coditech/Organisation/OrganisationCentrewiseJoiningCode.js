var OrganisationCentrewiseJoiningCode = {
    Initialize: function () {
        OrganisationCentrewiseJoiningCode.constructor();
    },

    constructor: function () {
    },

    GetOrganisationCentrewiseJoiningCodeSend: function (modelPopContentId) {
        CoditechCommon.ShowLodder();
        $.ajax({
            cache: false,
            type: "GET",
            dataType: "html",
            url: "/OrganisationCentrewiseJoiningCode/GetOrganisationCentrewiseJoiningCodeSend",            
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('#' + modelPopContentId).html(result);
                CoditechCommon.HideLodder();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == "401" || xhr.status == "403") {
                    {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to load details.", "error");
                    CoditechCommon.HideLodder();
                }
            }
        });
        $("#frmSendDetails").submit();
    },
}
