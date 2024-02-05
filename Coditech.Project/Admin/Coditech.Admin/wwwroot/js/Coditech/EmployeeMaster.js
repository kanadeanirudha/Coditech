var EmployeeMaster = {
    Initialize: function () {
        EmployeeMaster.constructor();
    },
    constructor: function () {
    },
    GetEmployeeMasterByCentreCode: function () {
        $('#DataTables_SearchById').val("")
        if ($("#SelectedCentreCode").val() == "") {
            CoditechNotification.DisplayNotificationMessage("Please select centre.", "error");
        }
        else {
            CoditechDataTable.LoadList("EmployeeMaster", "List");
        }
    },
}
