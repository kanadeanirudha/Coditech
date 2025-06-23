using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyViewModel : BaseViewModel
    {
        public short GeneralPolicyMasterId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Policy Code")]
        public string PolicyCode { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Policy Name")]
        public string PolicyName { get; set; }
        [Required]
        [MaxLength(255)]
        [Display(Name = "Policy Description")]
        public string PolicyDescription { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Policy Related To Module Code")]
        public string PolicyRelatedToModuleCode { get; set; }
        [Required]
        [MaxLength(15)]
        [Display(Name = "Policy Applicable Status")]
        public string PolicyApplicableStatus { get; set; }
        [Display(Name = "Is Policy Active")]
        public bool IsPolicyActive { get; set; }
    }
}
