var GeneralRunningNumbers = {
    Initialize: function () {
        GeneralRunningNumbers.constructor();
    },
    constructor: function () {
    },
    GetGeneralRunningNumbersByCentreCode: function () {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else {
            CoditechDataTable.LoadList("GeneralRunningNumbers", "List");
        }
    },

    
}
