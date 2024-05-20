using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalDoctorLeaveSchedule
    {
        [Key]
        public long HospitalDoctorLeaveScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public DateTime LeaveDate { get; set; }
        public bool IsFullDay { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? UptoTime { get; set; }
        public string Remark { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

