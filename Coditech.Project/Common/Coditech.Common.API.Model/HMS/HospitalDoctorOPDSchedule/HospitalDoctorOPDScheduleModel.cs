namespace Coditech.Common.API.Model
{
    public class HospitalDoctorOPDScheduleModel : BaseModel
    {
        public int HospitalDoctorId { get; set; }
        public int WeekDayEnumId { get; set; }

        public long HospitalDoctorOPDScheduleMorningId { get; set; }
        public TimeSpan FromTimeMorning { get; set; }
        public TimeSpan UptoTimeMorning { get; set; }
        public Byte TimeSlotInMinuteMorning { get; set; }


        public long HospitalDoctorOPDScheduleEveningId { get; set; }
        public TimeSpan FromTimeEvening { get; set; }
        public TimeSpan UptoTimeEvening { get; set; }
        public Byte TimeSlotInMinuteEvening { get; set; }

        public string TimeZone { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecilization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}
