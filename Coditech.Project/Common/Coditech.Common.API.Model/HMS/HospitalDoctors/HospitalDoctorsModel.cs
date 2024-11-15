using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalDoctorsModel : BaseModel
    {
        [Required]
        public int HospitalDoctorId { get; set; }

        [Required]
        public long EmployeeId { get; set; }

        [Required]
        public int MedicalSpecializationEnumId { get; set; }

        public string SelectedCentreCode { get; set; }

        public string SelectedDepartmentId { get; set; }

        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public bool IsAssociated { get; set; }
        public string CentreCode { get; set; }
        public string MedicalSpecialization { get; set; }
        public string PersonCode { get; set; }
    }
}
