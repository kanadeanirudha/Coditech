namespace Coditech.API.Data
{
    public partial class GeneralPolicyRules
    {
        public short GeneralPolicyRulesId { get; set; }
        public string PolicyCode { get; set; }
        public string PolicyQuestionDescription { get; set; }
        public string PolicyRange { get; set; }
        public string PolicyDefaultAnswer { get; set; }
        public string PolicyAnsType { get; set; }
        public string RangeSeparateBy { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

