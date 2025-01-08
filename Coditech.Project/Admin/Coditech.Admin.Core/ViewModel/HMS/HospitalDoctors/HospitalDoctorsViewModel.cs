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
        [Display(Name = "Specialization")]
        public int MedicalSpecializationEnumId { get; set; }

        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }

        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecialization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public bool IsAssociated { get; set; }
        public int OrganisationCentrewiseDepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string CentreCode { get; set; }

        [MaxLength(50)]
        [Editable(false)]
        [Display(Name = "Doctor Code")]
        public string PersonCode { get; set; }
    }
}