using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralDesignationModel : BaseModel
    {
        public GeneralDesignationModel()
        {

        }
        public short EmployeeDesignationMasterId { get; set; }
        [MaxLength(100)]
        [Required]
        public string Description { get; set; }
        [MaxLength(10)]
        public string DesignationLevel { get; set; }
        [MaxLength(10)]
        public string Grade { get; set; }

        [MaxLength(50)]
        [Required]
        public string ShortCode { get; set; }
        [MaxLength(50)]
        public string EmpDesigType { get; set; }
        [MaxLength(10)]
        public string RelatedWith { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}

