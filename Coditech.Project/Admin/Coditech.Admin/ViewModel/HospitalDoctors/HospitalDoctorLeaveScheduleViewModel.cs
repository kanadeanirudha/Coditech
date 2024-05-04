using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorLeaveScheduleViewModel : BaseViewModel
    {
        public long HospitalDoctorLeaveScheduleId { get; set; }
        [Required]
        public int HospitalDoctorId { get; set; }
        [Required]
        public DateTime LeaveDate { get; set; }
        [Required]
        public bool IsFullDay { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? UptoTime { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string RoomName { get; set; }
    }
}