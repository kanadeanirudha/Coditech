using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemStorageDimensionViewModel : BaseViewModel
    {
        public short InventoryItemStorageDimensionId { get; set; }
        [Display(Name = " Category Name")]
        [Required]
        [MaxLength(100)]
        public string CategoryName{ get; set; }
        [Display(Name = "Category Code")]
        [Required]
        [MaxLength(100)]
        public string CategoryCode { get; set; }

    }
}
