using Coditech.Common.Helper;
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
        public long CurrentEmployeeServiceId { get; set; }


        [Required]
        [Display(Name = "Employee Id")]
        public long EmployeeId { get; set; }

       
        [MaxLength(200)]
        [Editable(false)]
        [Display(Name = "Employee Code")]
        public string EmployeeCode { get; set; }

        [Required]
        [Display(Name = "Employee Designation")]
        public short EmployeeDesignationMasterId { get; set; }

        [Display(Name = "Current Designation")]
        public string CurrentEmployeeDesignation { get; set; }

        [Required]
        [Display(Name = "Joining Date")]
        public DateTime JoiningDate { get; set; }

      
        [Display(Name = "Promotion Demotion Date")]
        public DateTime? PromotionDemotionDate { get; set; }

        [Required]
        [Display(Name = "Employee Stage")]
        public int EmployeeStageEnumId { get; set; }

        public string EmployeeStage { get; set; }

        [Display(Name = "Date Of Leaving")]
        public DateTime? DateOfLeaving { get; set; }

        [Required]
        [Display(Name = "Is Current Position")]
        public bool IsCurrentPosition { get; set; }

        [MaxLength(100)]
        [Display(Name = "Salary Grade Code")]
        public string SalaryGradeCode { get; set; }

        [MaxLength(100)]
        [Display(Name = "Pay Scale")]
        public string PayScale { get; set; }

        [Display(Name = "Order Date")]
        public DateTime? OrderDate { get; set; }

        [MaxLength(100)]
        [Display(Name = "Order Number")]
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
