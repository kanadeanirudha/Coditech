using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalDoctorVisitingChargesModel : BaseModel
    {
        [Required]
        public long HospitalDoctorVisitingChargesId { get; set; }

        [Display(Name = "LabelCentre")]
        public string SelectedCentreCode { get; set; }
        [Display(Name = "LabelDepartments")]
        public string SelectedDepartmentId { get; set; }
        public int HospitalDoctorId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? UptoDate { get; set; }
        public int AppointmentTypeEnumId { get; set; }
        public string AppointmentType { get; set; }
        public decimal Charges { get; set; }
        public string Remark { get; set; }
        public bool IsTaxExclusive { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecilization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}
