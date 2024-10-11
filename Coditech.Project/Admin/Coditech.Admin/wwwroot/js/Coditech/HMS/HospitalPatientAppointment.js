var HospitalPatientAppointment = {
    Initialize: function () {
        HospitalPatientAppointment.constructor();
    },

    constructor: function () {
    },

    GetDoctorsAndPatientsListByCentreCodeAndSpecialization: function () {

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
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Hospital Doctors List", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#HospitalDoctorId").html("");
        }

        var selectedCentreCode = $("#SelectedCentreCode").val();
        if (selectedCentreCode != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalPatientAppointment/GetHospitalPatientsListByCentreCode",
                data: { "selectedCentreCode": selectedCentreCode },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#HospitalPatientRegistrationId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Patients List.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#HospitalPatientRegistrationId").html("");
        }

    },

    GetTimeSlotByDoctorsAndAppointmentDate: function () {

        var hospitalDoctorId = $("#HospitalDoctorId").val();
        var appointmentDate = $("#AppointmentDate").val();

        if (hospitalDoctorId != "" && appointmentDate != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalPatientAppointment/GetTimeSlotByDoctorsAndAppointmentDate",
                data: { "hospitalDoctorId": hospitalDoctorId, "appointmentDate": appointmentDate },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#RequestedTimeSlot").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Requested Time Slot ", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#RequestedTimeSlot").html("");
        }
    },
}







