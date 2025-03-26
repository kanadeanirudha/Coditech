var AccGLTransaction = {
    SelectedXmlData: null,
    map: {},
    map2: {},
    flag: true,
    rowCount: 0,
    valuTransactionType: null,

    Initialize: function () {
        this.BindEvents();
    },

    GetFinancialYearListByCentreCode: function () {
        var selectedCentreCode = $("#SelectedCentreCode").val();
        if (selectedCentreCode !== "") {
            CoditechCommon.ShowLodder();
            $.ajax({
                cache: false,
                type: "GET",
                dataType: "html",
                url: "/AccGLTransaction/GetFinancialYearListByCentreCode",
                data: { "selectedCentreCode": selectedCentreCode },
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#GeneralFinancialYearId").html(data);
                    CoditechCommon.HideLodder();
                },
                error: function (xhr) {
                    if (xhr.status === 401 || xhr.status === 403) {
                        location.reload();
                    }
                    CoditechNotification.DisplayNotificationMessage("Failed to retrieve BalanceSheet.", "error");
                    CoditechCommon.HideLodder();
                }
            });
        } else {
            CoditechNotification.DisplayNotificationMessage("Please select Centre and Balance Sheet Type.", "error");
        }
    },

    getData: function (query, process, valuTransactionType) {
        console.log("✅ Selected Transaction Type Code:", valuTransactionType);

        $.ajax({
            url: "/AccGLTransaction/GetAccSetupGLAccountList",
            type: "POST",
            data: {
                term: query,
                maxResults: 10,
                accountId: 0,
                personType: "",
                transactionTypeCode: valuTransactionType
            },
            dataType: "json",
            beforeSend: function () {
                console.log("🚀 Sending Data:", {
                    term: query,
                    transactionTypeCode: valuTransactionType
                });
            },
            success: function (response) {
                console.log("✅ Full Response:", response);

                if (!response || !response.Value || !response.Value.data) {
                    console.error("❌ Error: Response structure is incorrect", response);
                    return;
                }

                var actualData = response.Value.data;
                console.log("🔄 Processed Data Array:", actualData);

                if (!Array.isArray(actualData) || actualData.length === 0) {
                    console.warn("⚠️ No data found in response.Value.data!");
                    return;
                }

                var suggestions = [];

                $.each(actualData, function (i, transaction) {
                    console.log("🛠 Debugging Each Item:", transaction);

                    if (transaction && transaction.GLName) {
                        console.log("🟢 Found GLName:", transaction.GLName);
                        AccGLTransaction.map[transaction.GLName] = transaction;
                        suggestions.push(transaction.GLName);
                    } else {
                        console.warn("⚠️ No GLName found in this item:", transaction);
                    }
                });

                console.log("✅ Final Suggestions:", suggestions);
                process(suggestions);
            }
        });
    },

    AddRow: function () {
        var tableLength = $('#example tbody tr').length;
        var newRowCount = tableLength + 1;
        var valuTransactionType = $('#AccSetupTransactionTypeId').val();
        console.log("✅ Inside AddRow: Selected Transaction Type Code:", valuTransactionType);

        if (tableLength === 0) {
            $("#debitBal").val(0);
            $("#creditBal").val(0);
        }

        $('#tableDebitCredit').show();
        $('#example tbody tr td input[type=text]').attr('disabled', true);

        $("#example tbody").append(
            `<tr id="row${newRowCount}">
                <td><input id="AccGlName${newRowCount}" class="form-control input-sm typeahead" placeholder="Search Account*" type="text" maxlength="200" /></td>
                <td><input class="form-control input-sm" type="text" maxlength="500" placeholder="Narration" /></td>
                <td><input class="form-control input-sm debit-field validate-number" type="text" id="debitBal${newRowCount}" maxlength="15" value="0" /></td>
                <td><input class="form-control input-sm credit-field validate-number" type="text" id="creditBal${newRowCount}" maxlength="15" value="0" /></td>
                <td>
                    <a href="#" class="btn btn-sm btn-soft-success edit-row" title="Edit"><i class="fas fa-edit"></i></a>
                    <a href="#" class="btn btn-sm btn-soft-danger remove-row" title="Delete" data-rowid="row${newRowCount}"><i class="fas fa-trash-alt"></i></a>
                </td>
            </tr>`
        );

        AccGLTransaction.valuTransactionType = valuTransactionType;
        AccGLTransaction.InitializeAutocomplete("#AccGlName" + newRowCount, valuTransactionType);
        AccGLTransaction.calculateTotals();
    },
    SaveData: function () {
        var data = [];

        $('#example tbody tr').each(function () { // Loop through each row
            var row = $(this);
            var rowId = row.attr('id').replace("row", ""); // Extract row number dynamically

            var debitAmount = parseFloat(row.find(`#debitBal${rowId}`).val()) || 0; // Get debit amount
            var creditAmount = parseFloat(row.find(`#creditBal${rowId}`).val()) || 0; // Get credit amount

            var rowData = {
                AccGlName: row.find(`#AccGlName${rowId}`).val() || "", // Correct input selector
                Narration: row.find("td:eq(1) input").val() || "", // Get narration field
                DebitAmount: debitAmount,
                CreditAmount: creditAmount,
                TransactionAmount: debitAmount + creditAmount // Corrected calculation
            };

            data.push(rowData);
        });

        var jsonData = JSON.stringify(data);

        $("#TransactionDetailsData").val(jsonData);
        console.log('🚀 JSON Data:', jsonData);

        $("#frmWorkoutPlanDetails").submit();
    },

    editRow: function (row) {
        row.find("input").prop("readonly", false);
    },
    calculateTotals: function () {
        let totalDebit = 0, totalCredit = 0;

        $(".debit-field").each(function () {
            let debitValue = parseFloat($(this).val()) || 0;
            totalDebit += debitValue;
        });

        $(".credit-field").each(function () {
            let creditValue = parseFloat($(this).val()) || 0;
            totalCredit += creditValue;
        });

        console.log("🔢 Total Debit:", totalDebit, " | Total Credit:", totalCredit);

        $("#debitBal").val(totalDebit.toFixed(2));
        $("#creditBal").val(totalCredit.toFixed(2));

        // Check if debit equals credit
        if (totalDebit !== totalCredit) {
            $("#debitBal, #creditBal").css("border", "2px solid red");
            console.warn("⚠️ Debit and Credit do not match!");
        } else {
            $("#debitBal, #creditBal").css("border", "");
        }
    },
    //calculateTotals: function () {
    //    var totalDebit = 0, totalCredit = 0;

    //    $(".debit-field").each(function () {
    //        totalDebit += parseFloat($(this).val()) || 0;
    //    });

    //    $(".credit-field").each(function () {
    //        totalCredit += parseFloat($(this).val()) || 0;
    //    });

    //    $("#debitBal").val(totalDebit.toFixed(2));
    //    $("#creditBal").val(totalCredit.toFixed(2));
    //},

    InitializeAutocomplete: function (selector, transactionTypeCode) {
        transactionTypeCode = transactionTypeCode || AccGLTransaction.valuTransactionType;
        console.log("✅ Using Stored TransactionTypeCode:", transactionTypeCode);

        $(selector).autocomplete({
            source: function (request, response) {
                console.log("🔍 Sending AJAX Request with TransactionType:", transactionTypeCode);

                $.ajax({
                    url: "/AccGLTransaction/GetAccounts",
                    type: "POST",
                    data: {
                        term: request.term,
                        maxResults: 10,
                        accountId: 0,
                        personType: "",
                        transactionTypeCode: transactionTypeCode
                    },
                    dataType: "json",
                    success: function (data) {
                        console.log("✅ Received Data:", data);

                        let actualData = [];
                        if (Array.isArray(data.Value)) {
                            actualData = data.Value;
                        } else if (data.Value && Array.isArray(data.Value.data)) {
                            actualData = data.Value.data;
                        } else {
                            console.error("❌ Invalid Data Format:", data);
                            response([]);
                            return;
                        }

                        var suggestions = $.map(actualData, function (item) {
                            console.log("🟢 Adding Suggestion:", item.GLName);
                            return {
                                label: item.GLName,
                                value: item.GLName,
                                id: item.AccSetupGLId, // If you need the ID
                            };
                        });

                        console.log("✅ Final Suggestions:", suggestions);

                        if (suggestions.length > 0) {
                            console.log("🔽 Setting Autocomplete Source:", suggestions);
                        } else {
                            console.warn("⚠️ No suggestions found.");
                        }

                        response(suggestions);
                        $(selector).autocomplete("option", "source", suggestions);
                    }
                });
            },
            select: function (event, ui) {
                console.log("✅ Selected Account:", ui.item);

                // Store selected account data (optional)
                $(selector).data("selected-account", ui.item);
            }
        });
    },

    BindEvents: function () {
        $(document).on("focus", ".typeahead", function () {
            let transactionTypeCode = AccGLTransaction.valuTransactionType || $('#AccSetupTransactionTypeId').val();
            console.log("✅ Fetching TransactionTypeCode on Focus:", transactionTypeCode);
            AccGLTransaction.InitializeAutocomplete(this, transactionTypeCode);
        });

        $(document).on("click", ".remove-row", function () {
            var rowId = $(this).data("rowid");
            $("#" + rowId).remove();
            AccGLTransaction.calculateTotals();
        });

        $('#btnAdd').on('click', function () {
            $('#ResetAccountTransactionMasterRecord').show();
            $('#CreateAccountTransactionMasterRecord').show();
            AccGLTransaction.AddRow();
        });

        $(document).on("input", ".debit-field, .credit-field", function () {
            AccGLTransaction.calculateTotals();
        });

        $(document).on("keypress", ".validate-number", function (e) {
            var charCode = e.which ? e.which : e.keyCode;
            if ((charCode < 48 || charCode > 57) && charCode !== 46) e.preventDefault();
            if (charCode === 46 && $(this).val().includes(".")) e.preventDefault();
        });
    }
};

// ✅ Initialize on Page Load
$(document).ready(function () {
    AccGLTransaction.InitializeAutocomplete(".typeahead");
    AccGLTransaction.Initialize();
});