using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyViewModel : BaseViewModel
    {
        [Required]
        public short GeneralPolicyMasterId { get; set; }
        [Display(Name = "Policy Code")]
        public string PolicyCode { get; set; }
        [Display(Name = "Policy Name")]
        public string PolicyName { get; set; }
        [Display(Name = "Policy Description")]
        public string PolicyDescription { get; set; }
        [Display(Name = "Policy Related To Module Code")]
        public string PolicyRelatedToModuleCode { get; set; }
        [Display(Name = "Policy Applicable Status")]
        public string PolicyApplicableStatus { get; set; }
        [Display(Name = "Is Policy Active")]
        public bool IsPolicyActive { get; set; }
    }
}
