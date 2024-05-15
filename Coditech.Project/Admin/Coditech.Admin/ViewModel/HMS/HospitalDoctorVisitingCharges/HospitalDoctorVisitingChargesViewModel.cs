using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorVisitingChargesViewModel : BaseViewModel
    {
        [Required]
        public long HospitalDoctorVisitingChargesId { get; set; }

        [Required]
        [Display(Name = "Hospital Doctor")]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = " From Date")]
        public DateTime FromDate { get; set; }

        [Required]
        [Display(Name = "Upto Date")] 
        public DateTime? UptoDate { get; set; }

        [Required]
        [Display(Name = "Appointment Type")]
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