using Coditech.Common.API.Model;
using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientTypeViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Hospital Patient Type Id")]
        public byte HospitalPatientTypeId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Patient Type")]
        public string PatientType { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        

    }
}