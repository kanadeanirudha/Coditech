var DBTMTraineeAssignment = {
    Initialize: function () {
        DBTMTraineeAssignment.constructor();
    },

    constructor: function () {
    },


    GetDBTMTrainerListByCentreCode: function () {

        var centreCode = $("#SelectedCentreCode").val();
        if (centreCode != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/DBTMTraineeAssignment/GetTrainerByCentreCode",
                data: { "centreCode": centreCode },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#GeneralTrainerMasterId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve DBTM Trainer.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#GeneralTrainerMasterId").html("");
        }
    },

    GetDBTMTraineeListByCentreCodeAndTrainerMasterId: function () {

        var centreCode = $("#SelectedCentreCode").val();
        var generalTrainerId = $("#GeneralTrainerMasterId").val();

        if (centreCode != "" && generalTrainerId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/DBTMTraineeAssignment/GetTraineeDetailByCentreCodeAndgeneralTrainerId",
                data: { "centreCode": centreCode, "generalTrainerId": generalTrainerId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#DBTMTraineeDetailId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Trainee Details List", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#DBTMTraineeDetailId").html("");
        }

    },
}








