var HospitalDoctorLeaveSchedule = {
    Initialize: function () {
        HospitalDoctorLeaveSchedule.constructor();
    },

    constructor: function () {
    },


    GetHospitalDoctorsListByCentreCodeAndDepartmentId: function () {
        var selectedCentreCode = $("#SelectedCentreCode").val();
        var selectedDepartmentId = $("#SelectedDepartmentId").val();

        if (selectedCentreCode != "" && selectedDepartmentId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalDoctorLeaveSchedule/GetHospitalDoctorsList",
                data: { "selectedCentreCode": selectedCentreCode, "selectedDepartmentId": selectedDepartmentId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#HospitalDoctorId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Hospital Doctors List", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
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
                return false;
            } else {
                // Submit the form if validation passes
                $("#frmHospitalDoctorLeaveScheduleTime").submit();
            }
        }
    },
}









