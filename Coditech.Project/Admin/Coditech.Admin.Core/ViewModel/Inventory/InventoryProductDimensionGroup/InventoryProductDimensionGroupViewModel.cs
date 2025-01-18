using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryProductDimensionGroupViewModel : BaseViewModel
    {
        public InventoryProductDimensionGroupViewModel()
        {
            InventoryProductDimensionGroupMapperList = new List<InventoryProductDimensionGroupMapperModel>();
        }
        public List<InventoryProductDimensionGroupMapperModel> InventoryProductDimensionGroupMapperList { get; set; }
        public int InventoryProductDimensionGroupId { get; set; }

        [Required]
        [Display(Name = "Product Dimension Group Name")]
        public string ProductDimensionGroupName { get; set; }

        [Required]
        [Display(Name = "Product Dimension Group Code")]
        public string ProductDimensionGroupCode { get; set; }
        public string ProductDimensionGroupMapperData { get; set; }
    }
}
