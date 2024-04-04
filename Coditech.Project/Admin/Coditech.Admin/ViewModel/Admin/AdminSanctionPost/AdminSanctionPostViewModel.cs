using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminSanctionPostViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; }
        [Required]
        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string SelectedDepartmentId { get; set; }
        public int AdminSanctionPostId { get; set; }
        [Required]
        [Display(Name = "LabelDesignationId", ResourceType = typeof(AdminResources))]
        public short DesignationId { get; set; }
        public short DepartmentId { get; set; }
        public string CentreCode { get; set; }
        [Display(Name = "LabelSanctionPostCode", ResourceType = typeof(AdminResources))]
        public string SanctionPostCode { get; set; }
        [Display(Name = "LabelSanctionedPostDescription", ResourceType = typeof(AdminResources))]
        public string SanctionedPostDescription { get; set; }
        [Required]
        [Display(Name = "LabelNoOfPost", ResourceType = typeof(AdminResources))]
        public short NoOfPost { get; set; } = 1;

        [Required(ErrorMessage = "Post Type Required")]
        [Display(Name = "Post Type")]
        public string PostType { get; set; }

        [Required(ErrorMessage = "Designation Type Required")]
        [Display(Name = "Designation Type")]
        public string DesignationType { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "LabelDepartments", ResourceType = typeof(AdminResources))]
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreName { get; set; }
    }
}
