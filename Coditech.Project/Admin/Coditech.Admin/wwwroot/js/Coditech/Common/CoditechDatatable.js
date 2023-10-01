﻿var CoditechDataTable = {
    Initialize: function () {
        CoditechDataTable.constructor();
    },
    constructor: function () {
    },

    // LoadList method is used to load List page
    LoadList: function (controllerName, methodName) {
        var dataTableModel = BindDataTableModel($('#DataTables_PageIndexId').val());
        CallListPage(controllerName, methodName, dataTableModel);
    },

    // LoadList method is used to load List page
    LoadListFirst: function (controllerName, methodName) {
        var dataTableModel = BindDataTableModel(1);
        CallListPage(controllerName, methodName, dataTableModel);
    },

    LoadListPrevious: function (controllerName, methodName) {
        var PageIndex = $('#DataTables_PageIndexId').val() == "1" ? 1 : dataTableModel.PageIndex = parseInt($('#DataTables_PageIndexId').val()) - 1;
        var dataTableModel = BindDataTableModel(PageIndex);
        CallListPage(controllerName, methodName, dataTableModel);
    },
    // LoadList method is used to load List page
    LoadListLast: function (controllerName, methodName, pageSize) {
        var dataTableModel = BindDataTableModel(pageSize);
        CallListPage(controllerName, methodName, dataTableModel);
    },

    LoadListNext: function (controllerName, methodName) {
        var PageIndex = parseInt($('#DataTables_PageIndexId').val()) + 1;
        var dataTableModel = BindDataTableModel(PageIndex);
        CallListPage(controllerName, methodName, dataTableModel);
    },

    LoadListSortBy: function (controllerName, methodName, columnName, sortBy) {
        sortBy = sortBy == "asc" ? "desc" : "asc";
        $('#DataTables_SortColumn').val(columnName);
        $('#DataTables_SortBy').val(sortBy);
        var dataTableModel = BindDataTableModel($('#DataTables_PageIndexId').val());
        CallListPage(controllerName, methodName, dataTableModel);
    },
}

function CallListPage(controllerName, methodName, dataTableModel) {
    CoditechCommon.ShowLodder();
    $.ajax(
        {
            cache: false,
            type: "POST",
            dataType: "html",
            url: "/" + controllerName + "/" + methodName,
            data: dataTableModel,
            success: function (result) {
                //Rebind Grid Data
                $('#DataTablesDivId').html(result);
                $("#DataTables_PageSizeId").attr("disabled", false);
                $("#DataTables_SearchById").attr("disabled", false);
                CoditechCommon.HideLodder();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                CoditechNotification.DisplayNotificationMessage("Failed to display record.", "error");
                CoditechCommon.HideLodder();
            }
        });
}

function BindDataTableModel(PageIndex) {
    $("#notificationDivId").hide();
    let dataTableModel = {
        SearchBy: $('#DataTables_SearchById').val() != undefined ? $('#DataTables_SearchById').val().trim() : "",
        SortByColumn: $('#DataTables_SortColumn').val(),
        SortBy: $('#DataTables_SortBy').val(),
        PageIndex: PageIndex,
        PageSize: $('#DataTables_PageSizeId').val(),
        SelectedCentreCode: $("#SelectedCentreCode").length > 0 ? $("#SelectedCentreCode").val() : "",
        SelectedDepartmentId: $("#SelectedDepartmentId").length > 0 ? $("#SelectedDepartmentId").val() : 0,
    }
    $("#DataTables_PageSizeId").attr("disabled", true);
    $("#DataTables_SearchById").attr("disabled", true);
    return dataTableModel;
}
