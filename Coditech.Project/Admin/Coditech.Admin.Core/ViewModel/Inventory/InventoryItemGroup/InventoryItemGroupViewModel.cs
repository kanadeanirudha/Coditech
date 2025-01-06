using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class InventoryItemGroupViewModel : BaseViewModel
    {
        public Int16 InventoryItemGroupId { get; set; }
        [Display(Name = "Item Group Name")]
        [Required]
        public string ItemGroupName { get; set; }
        [Display(Name = "Item Group Code")]
        [Required]

        public string ItemGroupCode { get; set; }
        [Display(Name = "Consider In Prod Report")]
        public bool ConsiderInProdReport { get; set; }
    }
}
