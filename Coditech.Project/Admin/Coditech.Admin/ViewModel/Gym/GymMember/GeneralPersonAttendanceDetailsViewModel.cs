using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonAttendanceDetailsViewModel : BaseViewModel
    {
        public long GeneralPersonAttendanceDetailId { get; set; }
        public int GymMemberDetailId { get; set; }
        public long PersonId { get; set; }

        public int GeneralAttendanceStateEnumId { get; set; }


        [Required]
        [Display(Name = "Attendance Date")]
        public DateTime Attendancedate { get; set; }

        [Display(Name = "LoginTime")]
        public DateTime LoginTime { get; set; }

        [Display(Name = "LogoutTime")]
        public DateTime LogoutTime { get; set; }

        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public string Duration { get; set; }
    }
}


