using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class OrganisationCentrewiseJoiningCodeViewModel : BaseViewModel
    {
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
        public string JoiningCode { get; set; }
        [Required]
        [Display(Name = "Joining Code Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Is Expired")]
        public bool IsExpired { get; set; }
        [Display(Name = "Calling Code")]
        public string CallingCode { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter valid Mobile number")]
        [MaxLength(10)]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [MaxLength(70)]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; }
        public int TotalJoiningCodeCount { get; set; }
        public int ActiveJoiningCodeCount { get; set; }
        public int ExpireJoiningCodeCount { get; set; }

    }
}
