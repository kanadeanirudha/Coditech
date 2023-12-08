using Coditech.Common.Helper;

using Microsoft.AspNetCore.Mvc.Rendering;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPersonViewModel : BaseViewModel
    {
        [Required]
        public long PersonId { get; set; }

        [MaxLength(10)]
        [Required]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Person Code")]
        public string PersonCode { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        public List<SelectListItem> TitleList { get; set; }

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
        public string DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderEnumId { get; set; }

        public List<SelectListItem> GenderlList { get; set; }

        [MaxLength(50)]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Telephone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(15)]
        [Required]
        [Display(Name = "Cell Phone")]
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
        [Display(Name = "Id number")]
        public string IndentificationNumber { get; set; }

        [Display(Name = "Indentification")]
        public int IndentificationEnumId { get; set; }

        public List<SelectListItem> IndentificationList { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }

        [Display(Name = "Photo Id")]
        public long PhotoMediaId { get; set; }

        [MaxLength(100)]
        [Display(Name = "Birth Mark")]
        public string BirthMark { get; set; }

        [MaxLength(100)]
        [Display(Name = "Attendance")]
        public string AttendanceIntegrationId { get; set; }

        [Display(Name = "Occupation")]
        public short GeneralOccupationMasterId { get; set; }
    }
}
