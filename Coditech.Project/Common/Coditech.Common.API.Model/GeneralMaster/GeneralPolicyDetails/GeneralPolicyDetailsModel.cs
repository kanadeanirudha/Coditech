using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralPolicyDetailsModel : BaseModel
    {
        public short GeneralPolicyDetailId { get; set; }
        public short GeneralPolicyRulesId { get; set; }
        public string CentreCode { get; set; }         
        public string PolicyAnswered { get; set; }      
        public DateTime? ApplicableFromDate { get; set; } 
        public DateTime? ApplicableUptoDate { get; set; }
        public string PolicyCode { get; set; }
        public string PolicyQuestionDescription { get; set; }
    }
}
