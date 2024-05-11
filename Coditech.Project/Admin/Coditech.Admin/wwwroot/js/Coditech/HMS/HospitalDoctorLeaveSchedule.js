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
}








