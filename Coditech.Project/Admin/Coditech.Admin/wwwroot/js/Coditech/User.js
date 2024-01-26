var User = {
    Initialize: function () {
        User.constructor();
    },
    constructor: function () {
    },

    GetGeneralPersonAddressess: function (personId) {
        CoditechCommon.ShowLodder();
        $.ajax(
            {
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/User/GetGeneralPersonAddressess",
                data: { "personId": personId },
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    $('#generalPersonAddressDivId').html("").html(result);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    if (xhr.status == "401") {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to display record.", "error");
                    CoditechCommon.HideLodder();
                }
            });
    },
}
