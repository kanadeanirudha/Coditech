using Coditech.Common.API.Model;
using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorOPDScheduleViewModel : BaseViewModel
    {
        public HospitalDoctorOPDScheduleViewModel()
        {
            WeekDaysList = new List<GeneralEnumaratorModel>();
        }
        public List<GeneralEnumaratorModel> WeekDaysList { get; set; }
        [Required]
        [Display(Name = "Hospital Doctor")]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = "WeekDay Enum")]
        public int WeekDayEnumId { get; set; }
        
        [Display(Name = "Time Zone")]
        public string TimeZone { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecilization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }

        public long HospitalDoctorOPDScheduleMorningId { get; set; }

        [Required]
        [Display(Name = "From Time")]
        public TimeSpan FromTimeMorning { get; set; }

        [Required]
        [Display(Name = "Upto Time")]
        public TimeSpan UptoTimeMorning { get; set; }

        [Required]
        [Display(Name = "Times Slot In Minutes")]
        public Byte TimeSlotInMinuteMorning { get; set; }


        public long HospitalDoctorOPDScheduleEveningId { get; set; }
        [Required]
        [Display(Name = "From Time")]
        public TimeSpan FromTimeEvening { get; set; }

        [Required]
        [Display(Name = "Upto Time")]
        public TimeSpan UptoTimeEvening { get; set; }

        [Required]
        [Display(Name = "Times Slot In Minutes")]
        public Byte TimeSlotInMinuteEvening { get; set; }
    }
}