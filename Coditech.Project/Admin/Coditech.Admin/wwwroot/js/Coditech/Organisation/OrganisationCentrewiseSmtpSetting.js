var OrganisationCentrewiseSmtpSetting = {
    Initialize: function () {
        OrganisationCentrewiseJoiningCode.constructor();
    },

    constructor: function () { },

    GetSendTestEmailModalSend: function (modelPopContentId, centreCode) {
        console.log("Sending Centre Code:", centreCode);
        CoditechCommon.ShowLodder();
        $.ajax({
            cache: false,
            type: "GET",
            dataType: "html",
            url: "/OrganisationCentreMaster/GetSendTestEmailModalSend",
            data: { centreCode: centreCode },
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('#' + modelPopContentId).html(result); // Inject HTML into modal content container
                CoditechCommon.HideLodder();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status === 401 || xhr.status === 403) {
                    location.reload();
                }
                CoditechNotification.DisplayNotificationMessage("Failed to load details.", "error");
                CoditechCommon.HideLodder();
            }
        });
    }
};
