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
        } else {
            // Code in the case checkbox is NOT checked.
            $(".FromUptoTimeDivId").show();
        }
      
    },
}








