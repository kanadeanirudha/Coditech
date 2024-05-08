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
                url: "/GeneralCommanData/GetHospitalDoctorsList",
                data: { "selectedCentreCode": selectedCentreCode, "selectedDepartmentId": selectedDepartmentId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#HospitalDoctorId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Hospital Doctors List", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
    },

    IsFullDay: function () {
        $("#FromTime").val("");
        if ($("#IsFullDay").is(':checked')) {
            // Code in the case checkbox is checked.
            $("#FromTimeDivId").show();
        } else {
            // Code in the case checkbox is NOT checked.
            $("#FromTimeDivId").hide();
        }
        $("#UptoTime").val("");
        if ($("#IsFullDay").is(':checked')) {
            // Code in the case checkbox is checked.
            $("#UptoTimeDivId").show();
        } else {
            // Code in the case checkbox is NOT checked.
            $("#UptoTimeDivId").hide();
        }
    },
    //IsFullDay: function () {
    //    $("#UptoTime").val("");
    //    if ($("#IsFullDay").is(':checked')) {
    //        // Code in the case checkbox is checked.
    //        $("#UptoTimeDivId").show();
    //    } else {
    //        // Code in the case checkbox is NOT checked.
    //        $("#UptoTimeDivId").hide();
    //    }
    //},
}








