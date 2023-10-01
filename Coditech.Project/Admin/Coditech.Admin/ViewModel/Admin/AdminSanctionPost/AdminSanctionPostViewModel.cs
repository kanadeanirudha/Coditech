using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminSanctionPostViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(GeneralResources))]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(GeneralResources))]
        public string SelectedDepartmentId { get; set; }
        public int AdminSanctionPostId { get; set; }
        public Int16 DesignationId { get; set; }
        public Int16 DepartmentId { get; set; }
        public string CentreCode { get; set; }
        public string SanctionPostCode { get; set; }
        public string SanctionedPostDescription { get; set; }
        public Int16 NoOfPost { get; set; } = 1;

        [Required(ErrorMessage = "Post Type Required")]
        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [Required(ErrorMessage = "Designation Type Required")]
        [Display(Name = "Designation Type")]
        public string DesignationType { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "LabelDepartments", ResourceType = typeof(GeneralResources))]
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        [Display(Name = "LabelCentre", ResourceType = typeof(GeneralResources))]
        public string CentreName { get; set; }
    }
}
