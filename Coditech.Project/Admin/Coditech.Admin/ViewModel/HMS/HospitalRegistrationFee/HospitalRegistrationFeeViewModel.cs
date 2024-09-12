using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalRegistrationFeeViewModel : BaseViewModel
    {
        public HospitalRegistrationFeeViewModel()
        {
        }

        [Required]
        public int HospitalRegistrationFeeId { get; set; }
        [Required]
        public int InventoryGeneralItemLineId { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Centre", ResourceType = typeof(AdminResources))]
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

        public long PersonId { get; set; }

        [MaxLength(100)]
        [Editable(false)]
        [Display(Name = "UAH Number")]
        public string UAHNumber { get; set; }

        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }

        public string UserType { get; set; }

        public string ImagePath { get; set; }

        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }
    }

}
