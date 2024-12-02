var TaskApprovalSetting = {
    Initialize: function () {
        TaskApprovalSetting.constructor();
    },
    constructor: function () {
    },

    GetEmployeeListByCentreCode: function (centreCode, countNumber) {

        CoditechCommon.ShowLodder();
        countNumber = $("#CountNumber").val();
        $.ajax({
            cache: false,
            type: "GET",
            dataType: "html",
            url: "/TaskApprovalSetting/GetEmployeeListByCentreCode?centreCode=" + centreCode + "&countNumber=" + countNumber,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                $("#EmployeeListId").html("").html(result);
                CoditechCommon.HideLodder();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                if (xhr.status == "401" || xhr.status == "403") {
                    {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to load TaskApprovalSetting details.", "error");
                    CoditechCommon.HideLodder();
                }
            }
        });
    },

    SaveData: function () {
        var dropdownValues = [];
        $('#makeEditable tbody tr').each(function () {
            var employeeId = $(this).find('select').val();
            if (employeeId) {
                dropdownValues.push(employeeId); 
            }
        });

        $('#EmployeeIds').val(dropdownValues.join(","));
        console.log('Prepared JSON Data:', jsonData);

        $("#frmTaskApprovalSetting").submit();
    }
}
