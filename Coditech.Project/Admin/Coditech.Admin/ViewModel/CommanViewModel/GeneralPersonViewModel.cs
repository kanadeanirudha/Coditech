using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonViewModel : BaseViewModel
    {
        public long PersonId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Person Code")]
        public string PersonCode { get; set; }

        [MaxLength(50)]
        [MinLength(1)]
        [Display(Name = "Title")]
        public string PersonTitle { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderEnumId { get; set; }

        [MaxLength(200)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [MaxLength(20)]
        [Display(Name = "Telephone Number")]
        public string PhoneNumber { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter valid mobile number")]
        [MaxLength(10)]
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [MaxLength(15)]
        [Display(Name = "Emergency Contact Number")]
        public string EmergencyContact { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        public short GeneralNationalityMasterId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; } 

        [MaxLength(50)]
        [Display(Name = "Identification Number")]
        public string IndentificationNumber { get; set; }

        [Display(Name = "Identification Type")]
        public int IndentificationEnumId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Photo")]
        public long PhotoMediaId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Birthmark")]
        public string BirthMark { get; set; }

        [MaxLength(100)]
        [Display(Name = "Attendance Integration ID")]
        public string AttendanceIntegrationId { get; set; }

        [Display(Name = "Occupation")]
        public short GeneralOccupationMasterId { get; set; }
        public string UserType { get; set; }
        [Display(Name = "Anniversary Date")]
        public DateTime? AnniversaryDate { get; set; }
        public long EntityId { get; set; }
    }
}
