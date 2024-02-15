var GymMemberBodyMeasurement = {
    Initialize: function () {
        GymMemberBodyMeasurement.constructor();
    },
    constructor: function () {
    },

    AddGymMemberBodyMeasurement: function (modelPopContentId, gymMemberDetailId, gymMemberBodyMeasurementId) {
        CoditechCommon.ShowLodder();
        $.ajax({
            cache: false,
            type: "GET",
            dataType: "html",
            url: "/GymMemberBodyMeasurement/AddGymMemberBodyMeasurement",
            data: { "gymMemberDetailId": gymMemberDetailId, "gymMemberBodyMeasurementId": gymMemberBodyMeasurementId },
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

    //SaveBodyMeasurement: function () {
    //    // Validate the form with the id "frmGymMemberBodyMeasurement" using jQuery Validation plugin.
    //    $("#frmGymMemberBodyMeasurement").validate();

    //    // Remove any previous validation error messages
    //    $("#errorBodyMeasurementValue").text('').removeClass("field-validation-error").hide();
    //    $("#errorCreatedDate").text('').removeClass("field-validation-error").hide();

    //    if ($("#frmGymMemberBodyMeasurement").valid()) {
    //        // Check for the presence of measurement value
    //        if ($("#BodyMeasurementValue").val() == "") {
    //            $("#errorBodyMeasurementValue").text('Please Enter Measurement Value.').addClass("field-validation-error").show();
    //            return false;
    //        }

    //        // Check for the presence of created date
    //        if ($("#CreatedDate").val() == "") {
    //            $("#errorCreatedDate").text('Please Select Measurement Date.').addClass("field-validation-error").show();
    //            return false;
    //        }

    //        // Submit the form if all checks pass
    //        $("#frmGymMemberBodyMeasurement").submit();
    //    }
    //}
};

// Initialize the GymMemberBodyMeasurement module on document ready
$(document).ready(function () {
    GymMemberBodyMeasurement.Initialize();
});
