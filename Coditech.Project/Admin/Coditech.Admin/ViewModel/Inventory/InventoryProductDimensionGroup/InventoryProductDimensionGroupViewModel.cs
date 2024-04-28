using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryProductDimensionGroupViewModel : BaseViewModel
    {
        public int InventoryProductDimensionGroupId { get; set; }

        [Required]
        [Display(Name = "Product Dimension Group Name")]
        public string ProductDimensionGroupName { get; set; }

        [Required]
        [Display(Name = "Product Dimension Group Code")]
        public string ProductDimensionGroupCode { get; set; }

        [Display(Name = "Inventory Product Dimension Id")]
        public byte InventoryProductDimensionId { get; set; }

        [Display(Name = "For Purchase Prices")]
        public bool ForPurchase { get; set; }

        [Display(Name = "For Sale Prices")]
        public bool ForSale { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
