using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralPolicyRulesModel : BaseModel
    {
        public short GeneralPolicyRulesId { get; set; }
        public string PolicyCode { get; set; }
        public string PolicyQuestionDescription { get; set; }
        public string PolicyRange { get; set; }
        public string PolicyDefaultAnswer { get; set; }
        public string PolicyAnsType { get; set; }
        public string RangeSeparateBy { get; set; }
        public string PolicyApplicableStatus { get; set; }
        public short GeneralPolicyDetailId { get; set; }
        public string PolicyAnswered { get; set; }
        public DateTime? ApplicableFromDate { get; set; }
        public DateTime? ApplicableUptoDate { get; set; }
    }
}
