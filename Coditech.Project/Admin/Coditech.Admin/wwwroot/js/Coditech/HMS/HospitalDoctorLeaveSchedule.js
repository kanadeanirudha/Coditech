var HospitalDoctorLeaveSchedule = {
    Initialize: function () {
        HospitalDoctorLeaveSchedule.constructor();
    },

    constructor: function () {
    },

    IsFullDay: function () {
        $("#FromTime").val("");
        $("#UptoTime").val("");
        if ($("#IsFullDay").is(':checked')) {
            // Code in the case checkbox is checked.
            $(".FromUptoTimeDivId").hide();
            $("#FromTime").rules("remove", "required")
            $("#UptoTime").rules("remove", "required")
        } else {
            // Code in the case checkbox is NOT checked.
            $(".FromUptoTimeDivId").show();
            $("#FromTime").rules("add", "required")
            $("#UptoTime").rules("add", "required")
        }
    },
   
    ValidateLeaveScheduleTime: function () {

        $("#frmHospitalDoctorLeaveScheduleTime").validate();
        $("#errorFromTime").text('').removeClass("field-validation-error").hide();
        $("#errorUptoTime").text('').removeClass("field-validation-error").hide();

        if ($("#frmHospitalDoctorLeaveScheduleTime").valid()) {
            var fromTimeValue = $("#FromTime").val();
            var uptoTimeValue = $("#UptoTime").val();

            if (uptoTimeValue != '' && fromTimeValue >= uptoTimeValue) {
                $("#errorUptoTime").text(" Upto time must be greater than From time. Please select a valid time.").addClass("field-validation-error").show();
            } else {
                // Submit the form if validation passes
                $("#frmHospitalDoctorLeaveScheduleTime").submit();
            }
        }
    },
}









