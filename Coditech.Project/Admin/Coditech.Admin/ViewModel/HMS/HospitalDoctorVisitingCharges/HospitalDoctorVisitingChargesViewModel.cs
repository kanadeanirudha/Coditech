using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorVisitingChargesViewModel : BaseViewModel
    {
        [Required]
        public int HospitalDoctorVisitingChargesId { get; set; }

        [Required]
        [Display(Name = "Hospital Doctor")]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = " From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "Upto Date")] 
        public DateTime UptoDate { get; set; }

        [Required]
        [Display(Name = "Appointment")]
        public int AppointmentTypeEnumId { get; set; }

        [Required]
        [Display(Name = "Charges")]
        public decimal Charges { get; set; }

        [Required]
        [MaxLength(500)]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        
    }
}