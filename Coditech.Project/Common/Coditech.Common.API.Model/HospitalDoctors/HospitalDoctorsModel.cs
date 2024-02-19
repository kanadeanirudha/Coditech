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
        public int MedicalSpecilizationEnumId { get; set; }

        [MaxLength(500)]
        [Required]
        public string WeekDayEnumIds { get; set; }

        [Required]
        public short OrganisationCentrewiseBuildingRoomId { get; set; }

        [Required]
        public string SelectedCentreCode { get; set; }

        [Required]
        public string SelectedDepartmentId { get; set; }

        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public bool IsAssociated { get; set; }
    }
}
