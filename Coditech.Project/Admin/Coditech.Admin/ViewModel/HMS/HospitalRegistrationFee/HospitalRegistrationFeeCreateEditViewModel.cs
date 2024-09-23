using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalRegistrationFeeCreateEditViewModel : GeneralPersonViewModel
    {
        public HospitalRegistrationFeeCreateEditViewModel()
        {
        }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        public int HospitalRegistrationFeeId { get; set; }
        [Required]
        [Display(Name = "InventoryGeneralItemLine")]
        public int InventoryGeneralItemLineId { get; set; }
    }
}
