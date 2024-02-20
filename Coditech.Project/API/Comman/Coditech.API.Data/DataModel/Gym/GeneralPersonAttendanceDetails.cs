using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data.DataModel.Gym
{
    public partial class GeneralPersonAttendanceDetails
    {
        [Key]
        public long GeneralPersonAttendanceDetailId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan? LoginTime { get; set; }
        public TimeSpan? LogoutTime { get; set; }
        public string Remark { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
