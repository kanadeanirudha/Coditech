var HospitalDoctorOPDSchedule = {
    Initialize: function () {
        HospitalDoctorOPDSchedule.constructor();
    },

    constructor: function () {
    },

    ValidateHospitalDoctorOPDScheduleTime: function () {

        $("#frmHospitalDoctorOPDSchedule").validate();
        $("#errorFromTimeMorning").text('').removeClass("field-validation-error").hide();
        $("#errorUptoTimeMorning").text('').removeClass("field-validation-error").hide();
        $("#errorFromTimeEvening").text('').removeClass("field-validation-error").hide();
        $("#errorUptoTimeEvening").text('').removeClass("field-validation-error").hide();

        if ($("#frmHospitalDoctorOPDSchedule").valid()) {
            var fromTimeMorningValue = $("#FromTimeMorning").val();
            var uptoTimeMorningValue = $("#UptoTimeMorning").val();

            var fromTimeEveningValue = $("#FromTimeEvening").val();
            var uptoTimeEveningValue = $("#UptoTimeEvening").val();

            if (fromTimeMorningValue >= uptoTimeMorningValue) {
                $("#errorUptoTimeMorning").text("Upto time must be greater than From time. Please select a valid time.").addClass("field-validation-error").show();
                return false;
            } else if (fromTimeEveningValue >= uptoTimeEveningValue) {
                $("#errorUptoTimeEvening").text("Upto time must be greater than From time. Please select a valid time.").addClass("field-validation-error").show();
                return false;
            } else {
                // Submit the form if validation passes
                $("#frmHospitalDoctorOPDSchedule").submit();
            }
        }
    },
}









