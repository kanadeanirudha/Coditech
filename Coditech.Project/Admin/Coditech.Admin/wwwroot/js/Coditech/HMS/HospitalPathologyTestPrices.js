var HospitalPathologyTestPrices = {
    Initialize: function () {
        HospitalPathologyTestPrices.constructor();
    },

    constructor: function () {
    },

    GetPathologyTestNameByPathologyPriceCategory: function () {

        var hospitalPathologyPriceCategoryEnumId = $("#HospitalPathologyPriceCategoryEnumId").val();
        if (hospitalPathologyPriceCategoryEnumId != "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/HospitalPathologyTestPrices/GetPathologyTestNameByPathologyPriceCategory",
                data: { "hospitalPathologyPriceCategoryEnumId": hospitalPathologyPriceCategoryEnumId },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#HospitalPathologyTestId").html("").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve Pathology Test Name List.", "error")
                    CoditechCommon.HideLodder();
                }
            });
        }
        else {
            $("#HospitalPathologyTestId").html("");
        }
    },
}







