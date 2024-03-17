var GymSalesInvoice = {
    Initialize: function () {
        GymSalesInvoice.constructor();
    },
    constructor: function () {
    },

    GetSaleInvoiceListByCentreCode: function (controllerName, methodName) {
        $("#SelectedParameter1").val($("#FromDate").val());
        $("#SelectedParameter2").val($("#ToDate").val());
        CoditechCommon.LoadListByCentreCode(controllerName, methodName);
    },
}
