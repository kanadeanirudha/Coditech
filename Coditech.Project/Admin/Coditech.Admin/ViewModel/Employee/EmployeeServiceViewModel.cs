using Coditech.Common.Helper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class EmployeeServiceViewModel : BaseViewModel
    {
        public EmployeeServiceViewModel()
        {
        }

        [Required]
        public long EmployeeServiceId { get; set; }

        [Required]
        [Display(Name = "Employee Id")]
        public long EmployeeId { get; set; }

        [Required]
        [MaxLength(200)]
        [Editable(false)]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Required]
        [Display(Name = "Employee Designation")]
        public short EmployeeDesignationMasterId { get; set; }

        [Display(Name = "Current Designation")]
        public string CurrentDesignation { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

        [Required]
        [Display(Name = "Promotion Demotion Date")]
        public DateTime PromotionDemotionDate { get; set; }

        [Required]
        [Display(Name = "Employee Stage Enum Id")]
        public int EmployeeStageEnumId { get; set; }

        [Required]
        [Display(Name = "Date Of Leaving")]
        public DateTime DateOfLeaving { get; set; }

        [Required]
        [Display(Name = "Is Current Position")]
        public bool IsCurrentPosition { get; set; }

        [MaxLength(100)]
        public string SalaryGradeCode { get; set; }

        [MaxLength(100)]
        public string PayScale { get; set; }
        public DateTime OrderDate { get; set; }

        [MaxLength(100)]
        public string OrderNumber { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }

        public long PersonId { get; set; }

        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }

}
