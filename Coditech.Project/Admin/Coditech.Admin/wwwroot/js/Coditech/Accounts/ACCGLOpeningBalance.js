var AccGLOpeningBalance = {
    Initialize: function () {
    },

    constructor: function () {
    },
    GetAccSetUpCategoryListByGLCategory: function () {
        var accSetupCategoryId = $("#AccSetupCategoryId").val();
        $('#AccGLOpeningBalanceDivId').html('');
        if (accSetupCategoryId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/AccGLOpeningBalance/UpdateNonControlHeadType",
                data: { "accSetupCategoryId": accSetupCategoryId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#AccGLOpeningBalanceDivId").html("").html(data);

                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401" || xhr.status == "403") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Acc SetUp Category.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
    },

    SaveDataDeatils: function () {
        var data = [];

        // Loop through each row in the table
        $('#makeEditable tbody tr').each(function () {
            var row = $(this);
            var rowData = {
                AccSetupGLId: parseInt(row.find('input[id^="AccSetupGLId"]').val()),
                OpeningBalance: parseFloat(row.find('input[id^="OpeningBalance_"]').val()),
            };
            data.push(rowData);
        });

        // Stringify the data array
        var jsonData = JSON.stringify(data);

        //console.log('JSON data:', jsonData);
        $("#AccGLOpeningBalanceData").val(jsonData);
        console.log('JSON data:', jsonData);
        $("#frmAccGLOpeningBalanceDetails").submit();
    }
}



