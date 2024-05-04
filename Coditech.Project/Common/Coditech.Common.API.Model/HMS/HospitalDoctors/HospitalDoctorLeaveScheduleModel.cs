namespace Coditech.Common.API.Model
{
    public class HospitalDoctorLeaveScheduleModel : BaseModel
    {
        public long HospitalDoctorLeaveScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public DateTime LeaveDate { get; set; }
        public bool IsFullDay { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? UptoTime { get; set; }
        public string RoomName { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}
