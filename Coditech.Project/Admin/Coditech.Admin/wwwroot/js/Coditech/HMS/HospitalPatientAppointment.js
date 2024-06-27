var HospitalPatientAppointment = {
    Initialize: function () {
        HospitalPatientAppointment.constructor();
    },

    constructor: function () {
    },


    GetDoctorsByCentreCodeAndSpecialization: function () {

        var selectedCentreCode = $("#SelectedCentreCode").val();
        var medicalSpecilizationEnumId = $("#MedicalSpecilizationEnumId").val();

        if (selectedCentreCode != "" && medicalSpecilizationEnumId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalPatientAppointment/GetDoctorsByCentreCodeAndSpecialization",
                data: { "selectedCentreCode": selectedCentreCode, "medicalSpecilizationEnumId": medicalSpecilizationEnumId },
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
}









