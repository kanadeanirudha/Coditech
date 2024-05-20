namespace Coditech.Common.API.Model
{
    public class HospitalDoctorOPDScheduleModel : BaseModel
    {
        public int HospitalDoctorOPDScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public int WeekDayEnumId { get; set; }
        public short OPDTimesOfDay { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan UptoTime { get; set; }
        public Byte TimesSlothInMinute { get; set; }
        public string TimeZone { get; set; }
        
    }
}
