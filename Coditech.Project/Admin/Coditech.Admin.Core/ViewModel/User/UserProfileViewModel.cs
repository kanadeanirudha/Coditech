using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public class UserProfileViewModel : BaseViewModel
    {
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Display(Name = "Age")]
        public Nullable<int> Age { get; set; }
        public int GenderEnumId { get; set; }

        [MaxLength(200)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter valid contact number")]
        [MaxLength(10)]
        [Display(Name = "Emergency Contact Number")]
        public string EmergencyContact { get; set; }
        [MaxLength(50)]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        public long PhotoMediaId { get; set; }
        public string PhotoMediaPath { get; set; }
        public string PhotoMediaFileName { get; set; }
        public string UserType { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string LastName { get; set; }
        public long UserMasterId { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }
        [MaxLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "Please Enter The New Password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&#])[A-Za-z\\d@$!%*?&#]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (@$!%*?&#).")]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [MaxLength(100)]
        [MinLength(8)]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Description { get; set; }

    }
}
