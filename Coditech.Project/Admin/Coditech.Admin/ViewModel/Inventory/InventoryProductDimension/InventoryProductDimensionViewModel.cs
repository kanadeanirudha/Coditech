using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryProductDimensionViewModel : BaseViewModel
    {
        public int InventoryProductDimensionId { get; set; }
        [Display(Name = "Product Dimension Name")]
        [Required]
        public string ProductDimensionName { get; set; }
        [Display(Name = "Product Dimension Code")]
        [Required]
        public string ProductDimensionCode { get; set; }
    }
}
