using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorsViewModel : BaseViewModel
    {
        [Required]
        public int HospitalDoctorId { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public long EmployeeId { get; set; }

        [Required]
        [Display(Name = "Specilization")]
        public int MedicalSpecilizationEnumId { get; set; }

        [MaxLength(500)]
        [Required]
        [Display(Name = "Week Days")]
        public string WeekDayEnumIds { get; set; }

        [Required]
        [Display(Name = "Room Name")]
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

        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Email")]
        public string EmailId { get; set; }
        public bool IsAssociated { get; set; }
        public short OrganisationCentrewiseDepartmentId { get; set; }

        public short GeneralDepartmentMasterId { get; set; }

        public string DepartmentName { get; set; }
    }
}