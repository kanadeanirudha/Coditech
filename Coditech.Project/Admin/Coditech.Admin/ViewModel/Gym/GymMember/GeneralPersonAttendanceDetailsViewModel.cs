using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonAttendanceDetailsViewModel : BaseViewModel
    {
        public long GeneralPersonAttendanceDetailId { get; set; }
        public int GymMemberDetailId { get; set; }
        public long PersonId { get; set; }
        [Display(Name = "Attendance State")]
        public string GeneralAttendanceStateEnumId { get; set; }

        [Required]
        [Display(Name = "Attendance Date")]
        public DateTime AttendanceDate { get; set; }

        [Display(Name = "Check-In Time")]
        public TimeSpan? LoginTime { get; set; }

        [Display(Name = "Check-Out Time")]
        public TimeSpan? LogoutTime { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public string Duration { get; set; }
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }

    }
}


