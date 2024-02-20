namespace Coditech.Common.API.Model
{
    public class GeneralPersonAttendanceDetailsModel : BaseModel
    {
        public long GeneralPersonAttendanceDetailId { get; set; }
        public int GymMemberDetailId { get; set; }

        public long EntityId { get; set; }
        public string UserType { get; set; }

        public DateTime? AttendanceDate { get; set; }

        public TimeSpan? LoginTime { get; set; }

        public TimeSpan? LogoutTime { get; set; }
        public string Remark { get; set; }
        public string AttendanceState { get; set; }
    }
}
