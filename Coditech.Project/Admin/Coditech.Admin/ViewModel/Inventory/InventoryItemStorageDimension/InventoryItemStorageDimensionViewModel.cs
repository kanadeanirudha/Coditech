using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemStorageDimensionViewModel : BaseViewModel
    {
        public short InventoryItemStorageDimensionId { get; set; }
        [Display(Name = " Storage Dimension Name")]
        [Required]
        [MaxLength(100)]
        public string StorageDimensionName { get; set; }
        [Display(Name = "Storage Dimension Code")]
        [Required]
        [MaxLength(100)]
        public string StorageDimensionCode { get; set; }

    }
}
