using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryCategoryTypeViewModel : BaseViewModel
    {
        public byte InventoryCategoryTypeMasterId { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name = "Category Type Name")]
        public string CategoryTypeName { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
