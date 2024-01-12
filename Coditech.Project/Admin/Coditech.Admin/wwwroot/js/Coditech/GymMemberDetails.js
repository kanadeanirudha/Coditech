var GymMemberDetails = {
    Initialize: function () {
        GymMemberDetails.constructor();
    },
    constructor: function () {
    },

    OpenModelPopUp: function (controllerName, methodName, modelPopContentId) {
        CoditechCommon.ShowLodder();
        $.ajax(
            {
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/" + controllerName + "/" + methodName,
                data: { "gymMemberDetailId": 0,"gymMemberFollowUpId": 0 },
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
    SaveFollowup: function () {
        $("#frmGymMemberFollowUp").validate();
        debugger
        if ($("#frmGymMemberFollowUp").valid()) {
            if ($("#GymFollowupTypesEnumId").val() == "") {
                return false;
            }
            $("#frmGymMemberFollowUp").submit();
        }
    }
}
