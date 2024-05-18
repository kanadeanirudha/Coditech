using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientRegistrationViewModel : BaseViewModel
    {
        public HospitalPatientRegistrationViewModel()
        {
        }
        public long HospitalPatientRegistrationId { get; set; }
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

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }

}
