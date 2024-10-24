using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDepartmentViewModel : BaseViewModel
    {
        public short GeneralDepartmentMasterId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Department Short Code")]
        public string DepartmentShortCode { get; set; }
        [Required]
        [Display(Name = "Print Short Desc")]
        public string PrintShortDesc { get; set; }

        [Display(Name = "Work Activity")]
        public bool WorkActivity { get; set; }
    }
}
