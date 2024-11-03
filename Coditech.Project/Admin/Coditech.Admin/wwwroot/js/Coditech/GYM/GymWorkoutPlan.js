﻿var GymWorkoutPlan = {
    Initialize: function () {
        GymWorkoutPlan.constructor();
    },
    constructor: function () {

    },

    AddWorkoutPlanDetails: function (modelPopContentId, gymWorkoutPlanId, week, day) {
        CoditechCommon.ShowLodder();

        $.ajax({
            cache: false,
            type: "GET",
            dataType: "html",
            url: "/GymWorkoutPlan/AddWorkoutPlanDetails",
            data: {
                gymWorkoutPlanId: gymWorkoutPlanId,
                numberOfWeeks: week,
                numberOfDaysPerWeek: day
            },
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $('#' + modelPopContentId).html(result);
                CoditechCommon.HideLodder();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == 401) {
                    location.reload();
                } else {
                    CoditechNotification.DisplayNotificationMessage("Failed to load workout details.", "error");
                    CoditechCommon.HideLodder();
                }
            }
        });
    }, 

    SaveData: function () {
        var data = [];

        $('#makeEditable tbody tr.set-fields').each(function () {
            var row = $(this);
            var rowData = {
                GymWorkoutSetId: parseInt(row.find('input[id^="GymWorkoutSetId_"]').val()),
                GymWorkoutPlanDetailId: parseInt(row.find('input[id^="GymWorkoutPlanDetailId_"]').val()),
                Weight: parseFloat(row.find('input[id^="Weight_"]').val()) || null,  
                Repetitions: parseInt(row.find('input[id^="Repetitions_"]').val()) || null,  
                Duration: parseInt(row.find('input[id^="Duration_"]').val()) || null 
            };
            data.push(rowData);
        });

        var jsonData = JSON.stringify(data);

        $("#GymWorkoutPlanData").val(jsonData);
        console.log('JSON data:', jsonData);

        $("#frmWorkoutPlanDetails").submit();
    }
};
