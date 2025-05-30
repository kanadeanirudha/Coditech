﻿var CoditechNotification = {
    Initialize: function () {
        CoditechNotification.constructor();
    },
    constructor: function () {
    },

    DisplayNotificationMessage: function (message, messageType) {
        var notificationStyle = "";
        switch (messageType) {
            case "success":
                notificationStyle = "bg-success";
                break;
            case "error":
                notificationStyle = "bg-danger";
                break;
            case "info":
                notificationStyle = "bg-info";
                break;
            case "warning":
                notificationStyle = "bg-warning";
                break;
        }
        $("#notificationMessageId").html("").html(message);
        $("#notificationDivId").addClass(notificationStyle);
        $("#notificationDivId").show();
        $("#notificationDivId").fadeOut(10000); // fades slowly
    },
}

$("#notificationCloseId").click(function () {
    $("#notificationDivId").stop(true, true).fadeOut(1000);
});
$("#notificationDivId").fadeOut(10000);