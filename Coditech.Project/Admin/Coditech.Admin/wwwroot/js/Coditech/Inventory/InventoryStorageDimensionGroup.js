var InventoryStorageDimensionGroup = {
    SaveData: function () {
        var data = [];

        // Loop through each row in the table
        $('#makeEditable tbody tr').each(function () {
            var row = $(this);
            var isActive = row.find('input[id^="Active_"]').prop('checked');
            var isBlankReceiptAllowed = row.find('input[id^="BlankReceiptAllowed_"]').prop('checked');
            var isBlankIssueAllowed = row.find('input[id^="BlankIssueAllowed_"]').prop('checked');
            var isCoveragePlanByDimension = row.find('input[id^="CoveragePlanByDimension_"]').prop('checked');
            var isFinancialInventory = row.find('input[id^="FinancialInventory_"]').prop('checked');
            var isForPurchasePrices = row.find('input[id^="ForPurchasePrices_"]').prop('checked');
            var isForSalePrices = row.find('input[id^="ForSalePrices_"]').prop('checked');
            var isPhysicalInventory = row.find('input[id^="PhysicalInventory_"]').prop('checked');
            var isPrimaryStocking = row.find('input[id^="PrimaryStocking_"]').prop('checked');
            var reference = row.find('input[id^="Reference_"]').val();
            var isTransfer = row.find('input[id^="Transfer_"]').prop('checked');
            var displayOrder = parseInt(row.find('input[id^="DisplayOrder_"]').val());

            var rowData = {
                InventoryStorageDimensionGroupMapperId: parseInt(row.find('input[id^="InventoryStorageDimensionGroupMapperId_"]').val()),
                InventoryStorageDimensionId: parseInt(row.find('input[id^="InventoryStorageDimensionId_"]').val()),
                Active: isActive,
                BlankReceiptAllowed: isBlankReceiptAllowed,
                BlankIssueAllowed: isBlankIssueAllowed,
                CoveragePlanByDimension: isCoveragePlanByDimension,
                FinancialInventory: isFinancialInventory,
                ForPurchasePrices: isForPurchasePrices,
                ForSalePrices: isForSalePrices,
                PhysicalInventory: isPhysicalInventory,
                PrimaryStocking: isPrimaryStocking,
                Reference: reference,
                Transfer: isTransfer,
                DisplayOrder: displayOrder,
                StorageDimensionName: row.find('td:eq(0)').text().trim(),
            };
            data.push(rowData);
        });

        // Stringify the data array
        var jsonData = JSON.stringify(data);

        $("#StorageDimensionGroupMapperData").val(jsonData);

        $("#frmInventoryStorageDimensionGroup").submit();
    }
};
