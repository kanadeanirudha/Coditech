using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryCategoryViewModel : BaseViewModel
    {
        public short InventoryCategoryId { get; set; }

        [Display(Name = "Parent Inventory")]
        public short ParentInventoryCategoryId { get; set; }

        [Display(Name = " Category Code")]
        [Required]
        [MaxLength(50)]
        public string CategoryCode { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [Display(Name = "Item Prefix")]
        [MaxLength(20)]
        public string ItemPrefix { get; set; }
    }
}
