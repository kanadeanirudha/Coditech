using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyRulesViewModel : BaseViewModel
    {
        public short GeneralPolicyRulesId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Policy Code")]
        public string PolicyCode { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Policy Question Description")]
        public string PolicyQuestionDescription { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Policy Range")]
        public string PolicyRange { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Policy Default Answer")]
        public string PolicyDefaultAnswer { get; set; }
        [Required]
        [MaxLength(8)]
        [Display(Name = "Policy Ans Type")]
        public string PolicyAnsType { get; set; }
        [Required]
        [MaxLength(2)]
        [Display(Name = "Range Separate By")]
        public string RangeSeparateBy { get; set; }
        public List<GeneralPolicyModel> GeneralPolicyList { get; set; }
    }
}
