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
        public string PolicyApplicableStatus { get; set; }
        public short GeneralPolicyDetailId { get; set; }
        [MaxLength(500)]
        [Display(Name = "Policy Answered")]
        public string PolicyAnswered { get; set; }
        [Display(Name = "Applicable From Date")]
        public DateTime? ApplicableFromDate { get; set; }
        [Display(Name = "Applicable Upto Date")]
        public DateTime? ApplicableUptoDate { get; set; }
    }
}
