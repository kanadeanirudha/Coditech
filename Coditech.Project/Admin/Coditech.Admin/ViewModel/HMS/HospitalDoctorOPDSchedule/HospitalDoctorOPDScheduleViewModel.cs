using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorOPDScheduleViewModel : BaseViewModel
    {
        public long HospitalDoctorOPDScheduleId { get; set; }

        [Required]
        [Display(Name = "Hospital Doctor")]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = "WeekDay Enum")]
        public int WeekDayEnumId { get; set; }

        [Required]
        [Display(Name = "OPD Times")]
        public short OPDTimesOfDay { get; set; }

        [Required]
        [Display(Name = "From Time")]
        public TimeSpan FromTime { get; set; }

        [Required]
        [Display(Name = "Upto Time")]
        public TimeSpan UptoTime { get; set; }

        [Required]
        [Display(Name = "Times Sloth")]
        public Byte TimesSlothInMinute { get; set; }

        [Required]
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
    }
}