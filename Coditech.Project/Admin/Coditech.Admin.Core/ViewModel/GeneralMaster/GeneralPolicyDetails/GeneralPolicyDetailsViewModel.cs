using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralPolicyDetailsViewModel : BaseViewModel
    {
        public short GeneralPolicyDetailId { get; set; } 
        public short GeneralPolicyRulesId { get; set; }
        [MaxLength(15)]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }
       
        [MaxLength(500)]
        [Display(Name = "Policy Answered")]
        public string PolicyAnswered { get; set; }
        [Display(Name = "Applicable From Date")]
        public DateTime ApplicableFromDate { get; set; }
        [Display(Name = "Applicable Upto Date")]
        public DateTime ApplicableUptoDate { get; set; }
    }
}
