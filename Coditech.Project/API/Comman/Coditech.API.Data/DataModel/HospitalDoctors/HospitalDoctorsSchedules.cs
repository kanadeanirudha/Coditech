using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalDoctorsSchedules
    {
        [Key]
        public long HospitalDoctorScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public int HospitalWorkEnumId { get; set; }
        public int WeekDayEnumId { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? UptoTime { get; set; }
        public byte TimeSlot { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

