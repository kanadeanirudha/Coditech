using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientRegistrationCreateEditViewModel : GeneralPersonViewModel
    {
        public HospitalPatientRegistrationCreateEditViewModel()
        {
        }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        public long HospitalPatientRegistrationId { get; set; }
    }
}
