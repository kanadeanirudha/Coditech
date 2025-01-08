using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorLeaveScheduleViewModel : BaseViewModel
    {
        public long HospitalDoctorLeaveScheduleId { get; set; }
        [Required]
        [Display(Name = "Doctor")]
        public int HospitalDoctorId { get; set; }
        [Required]
        [Display(Name = "Leave Date")]
        public DateTime LeaveDate { get; set; }
        [Required]
        [Display(Name = "Is Full Day")]
        public bool IsFullDay { get; set; }

        [Required]
        [Display(Name = "From Time")]
        public TimeSpan? FromTime { get; set; }

        [Required]
        [Display(Name = "Upto Time")]
        public TimeSpan? UptoTime { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string RoomName { get; set; }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }

        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        [Display(Name = "Remark")]
        public string Remark { get; set; }
    }
}