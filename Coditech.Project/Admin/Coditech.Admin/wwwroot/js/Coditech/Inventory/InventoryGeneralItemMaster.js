$(document).ready(function () {
    $('#ProductTypeEnumId').change(function () {
        var selectedProductType = $(this).val();
        var productSubtypeDropdown = $('#ProductSubTypeEnumId');

        if (selectedProductType === 'Item') {
            // Show both options in the product subtype dropdown
            productSubtypeDropdown.find('option').show();
        } else if (selectedProductType === 'Service') {
            // Hide the 'Product' option and show the 'Product Master' option
            productSubtypeDropdown.find('option[value="product"]').hide();
            productSubtypeDropdown.val('productMaster');
        }
    });
});
