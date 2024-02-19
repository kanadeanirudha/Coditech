using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorsViewModel : BaseViewModel
    {
        [Required]
        public long HospitalDoctorId { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        [Required]
        public int MedicalSpecilizationEnumId { get; set; }

        [MaxLength(500)]
        [Required]
        public string WeekDayEnumIds { get; set; }

        [Required]
        public short OrganisationCentrewiseBuildingRoomId { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }

        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }

        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}