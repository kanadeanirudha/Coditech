namespace Coditech.API.Data
{
    public partial class GeneralPolicyMaster
    {
        public short GeneralPolicyMasterId { get; set; }
        public string PolicyCode { get; set; }
        public string PolicyName { get; set; }
        public string PolicyDescription { get; set; } 
        public string PolicyRelatedToModuleCode { get; set; }
        public string PolicyApplicableStatus { get; set; }
        public bool IsPolicyActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

