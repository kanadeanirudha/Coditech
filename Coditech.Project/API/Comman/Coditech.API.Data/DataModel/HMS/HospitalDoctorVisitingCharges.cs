using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalDoctorVisitingCharges
    {
        [Key]
        public long HospitalDoctorVisitingChargesId { get; set; }
        public int HospitalDoctorId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? UptoDate { get; set; }
        public int AppointmentTypeEnumId { get; set; }
        public decimal Charges { get; set; }
        public string Remark { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

