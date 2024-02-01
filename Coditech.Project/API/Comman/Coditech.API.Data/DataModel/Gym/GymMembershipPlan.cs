using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GymMembershipPlan
    {
        [Key]
        public int GymMembershipPlanId { get; set; }
        public long MembershipPlanName { get; set; }
        [MaxLength(100)]
        public string PersonCode { get; set; }
        public string UserType { get; set; }
        public string PastInjuries { get; set; }
        public string MedicalHistory { get; set; }
        public string OtherInformation { get; set; }
        public int? GymGroupEnumId { get; set; }
        public int? SourceEnumId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

