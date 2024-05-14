using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalDoctorVisitingChargesModel : BaseModel
    {
        [Required]
        public int HospitalDoctorVisitingChargesId { get; set; }

        public int HospitalDoctorId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime UptoDate { get; set; }
        public int AppointmentTypeEnumId { get; set; }
        public decimal Charges { get; set; }
        public string Remark { get; set; }
        
    }
}
