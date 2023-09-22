using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralDesignationViewModel : BaseViewModel
    {
        public short EmployeeDesignationMasterId { get; set; }
        [Required]
        public string Description { get; set; }
        public int DesignationLevel { get; set; }
        public int Grade { get; set; }
        public string ShortCode { get; set; }
        public string EmpDesigType { get; set; }
        public string RelatedWith { get; set; }
        public bool IsActive { get; set; }
    }
}
