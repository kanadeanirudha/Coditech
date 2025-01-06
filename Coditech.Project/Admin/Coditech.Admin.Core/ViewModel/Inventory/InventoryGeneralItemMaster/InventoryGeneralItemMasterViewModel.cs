using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryGeneralItemMasterViewModel : BaseViewModel
    {
        public int InventoryGeneralItemMasterId { get; set; }
        [Display(Name = "Category")]
        public short InventoryCategoryId { get; set; }

        [Required(ErrorMessage = "Item Number is required")]
        [Display(Name = "Item Number")]
        public string ItemNumber { get; set; }

        [Required(ErrorMessage = "Item Name is required")]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }

        [Required(ErrorMessage = "HSN/SAC is required")]
        [Display(Name = "HSN/SAC Code")]
        public string HSNSACCode { get; set; }

        [Required(ErrorMessage = "Product Type is required")]
        [Display(Name = "Product Type")]
        public int ProductTypeEnumId { get; set; }

        [Required(ErrorMessage = "Product SubType is required")]
        [Display(Name = "Product SubType")]
        public int ProductSubTypeEnumId { get; set; }

        [Display(Name = "Tax Group")]
        public byte GeneralTaxGroupMasterId { get; set; }

        
        [Display(Name = "Inventory Model")]
        public int InventoryModelEnumId { get; set; }

        [Display(Name = "Product Dimension Group")]
        public int InventoryProductDimentionGroupId { get; set; }

        [Display(Name = "Item Group")]
        public int InventoryItemGroupId { get; set; }

        [Display(Name = "Storage Dimension Group")]
        public int InventoryStorageDimentionGroupId { get; set; }

        [Display(Name = "Tracking Dimension Group")]
        public int InventoryTrackingDimentionGroupId { get; set; }
        [Display(Name = "Inventory Base UoM")]
        public short InventoryBaseUoMMasterId { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Is Base Uom")]
        public bool IsBaseUom { get; set; } = true;
    }
}
