using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemModelGroupViewModel : BaseViewModel
    {
        public short InventoryItemModelGroupId { get; set; }

        [Display(Name = "Name")]
        public string ItemModelGroupName { get; set; }

        [Display(Name = "Item Model Group")]
        public string ItemModelGroupCode { get; set; }

        [Display(Name = "Inventory Model")]
        public int InventoryModelEnumId { get; set; }

        [Display(Name = "Stocked Product")]
        public bool StockedProduct { get; set; }

        [Display(Name = "Post Physical Inventory")]
        public bool PostPhysicalInventory { get; set; }

        [Display(Name = "Post Financial Inventory")]
        public bool PostFinancialInventory { get; set; }

        [Display(Name = "Include Physical Value")]
        public bool IsIncludePhysicalValue { get; set; }

        [Display(Name = "Fixed Receipt Price")]
        public bool IsFixedReceiptPrice { get; set; }

        [Display(Name = "Deferred Revenue Account On Sale")]
        public bool PostDeferredRevenueAccountOnSale { get; set; }

        [Display(Name = "Accrue Liability On Product Receipt")]
        public bool AccrueLiabilityOnProductReceipt { get; set; }
    }
}
