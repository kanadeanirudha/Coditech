using Coditech.Common.API.Model;
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

        public string WeekDayEnumIds { get; set; }
        [Required]
        [Display(Name = "Week Days")]
        public List<string> SelectedWeekDayEnumIds { get; set; }

        [Required]
        [Display(Name = "Room Name")]
        public short OrganisationCentrewiseBuildingRoomId { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }

        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }

        [Required]
        [Display(Name = "Building Name")]
        public short OrganisationCentrewiseBuildingMasterId { get; set; }

        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecilization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public bool IsAssociated { get; set; }
        public short OrganisationCentrewiseDepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string CentreCode { get; set; }
        public string BuildingRoomName { get; set; }
        public List<GeneralEnumaratorModel> AllWeekDays { get; set; }
    }
}