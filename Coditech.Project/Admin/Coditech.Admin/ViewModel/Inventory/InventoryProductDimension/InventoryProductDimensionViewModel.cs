using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryProductDimensionViewModel : BaseViewModel
    {
        public int InventoryProductDimensionId { get; set; }
        [Display(Name = "Product Dimension Name")]
        public string ProductDimensionName { get; set; }
        [Display(Name = "Product Dimension Code")]
        public string ProductDimensionCode { get; set; }
    }
}
