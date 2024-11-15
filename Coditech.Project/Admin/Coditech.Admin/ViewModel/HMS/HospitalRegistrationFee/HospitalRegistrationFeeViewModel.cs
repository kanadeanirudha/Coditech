using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalRegistrationFeeViewModel : BaseViewModel
    {
        [Required]
        public int HospitalRegistrationFeeId { get; set; }
        [Required]
        public int InventoryGeneralItemLineId { get; set; }

        [MaxLength(15)]
       
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }

        [Required]
        [Display(Name = " From Date")]
        public DateTime FromDate { get; set; }

        [Display(Name = "Upto Date")]
        public DateTime? UptoDate { get; set; }

        [Required]
        [Display(Name = "Charges")]
        public decimal Charges { get; set; }

        [Required]
        [Display(Name = "Is Tax Exclusive")]
        public bool IsTaxExclusive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Registration Service")]
        public string RegistrationService { get; set; }
        public string SelectedCentreCode { get; set; }
    }

}
