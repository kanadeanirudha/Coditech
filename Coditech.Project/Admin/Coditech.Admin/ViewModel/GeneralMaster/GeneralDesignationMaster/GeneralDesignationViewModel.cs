using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDesignationViewModel : BaseViewModel
    {
        public short EmployeeDesignationMasterId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }

        [Display(Name = "Designation Level")]
        [MaxLength(10)]
        public string DesignationLevel { get; set; }
        [MaxLength(10)]
        public string Grade { get; set; }

        [Display(Name = "Short Code")]
        [MaxLength(50)]
        [Required]
        public string ShortCode { get; set; }


        [MaxLength(50)]
        public string EmpDesigType { get; set; }

        [Display(Name = "Related With")]
        [MaxLength(10)]
        public string RelatedWith { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
