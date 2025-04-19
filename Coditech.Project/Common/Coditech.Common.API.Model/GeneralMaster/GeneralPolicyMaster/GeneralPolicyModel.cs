namespace Coditech.Common.API.Model
{
    public class GeneralPolicyModel : BaseModel
    {
        public short GeneralPolicyMasterId { get; set; }
        public string PolicyCode { get; set; }
        public string PolicyName { get; set; } 
        public string PolicyDescription { get; set; }
        public string PolicyRelatedToModuleCode { get; set; } 
        public string PolicyApplicableStatus { get; set; } 
        public bool IsPolicyActive { get; set; }
    }
}
