namespace Coditech.Common.API.Model
{
    public class HospitalDoctorOPDScheduleModel : BaseModel
    {
        public long HospitalDoctorOPDScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public int WeekDayEnumId { get; set; }
        public short OPDTimesOfDay { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan UptoTime { get; set; }
        public Byte TimesSlothInMinute { get; set; }
        public string TimeZone { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecilization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}
