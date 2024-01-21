var GymMemberDetails = {
    Initialize: function () {
        GymMemberDetails.constructor();
    },
    constructor: function () {
    },

    GetMemberFollowUp: function (modelPopContentId, gymMemberDetailId, gymMemberFollowUpId, personId) {
        CoditechCommon.ShowLodder();
        $.ajax(
            {
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/GymMemberDetails/GetMemberFollowUp",
                data: { "gymMemberDetailId": gymMemberDetailId, "gymMemberFollowUpId": gymMemberFollowUpId, "personId": personId },
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
        $("#errorGymFollowupTypesEnumId").text('').text("").removeClass("field-validation-error").hide();
        $("#errorReminderDate").text('').text("").removeClass("field-validation-error").hide();
        if ($("#frmGymMemberFollowUp").valid()) {
            if ($("#GymFollowupTypesEnumId").val() == "") {
                $("#errorGymFollowupTypesEnumId").text('').text("Please Select Follow-up Type.").addClass("field-validation-error").show();
                return false;
            }
            if ($("#IsSetReminder").is(':checked') && $("#ReminderDate").val() == "") {
                $("#errorReminderDate").text('').text("Please Select Reminder Date.").addClass("field-validation-error").show();
                return false;
            }
            $("#frmGymMemberFollowUp").submit();
        }
    },

    IsSetReminder: function () {
        debugger
        $("#ReminderDate").val("");
        if ($("#IsSetReminder").is(':checked')) {
            // Code in the case checkbox is checked.
            $("#ReminderDateDivId").show();
        } else {
            // Code in the case checkbox is NOT checked.
            $("#ReminderDateDivId").hide();
        }
    }
}
