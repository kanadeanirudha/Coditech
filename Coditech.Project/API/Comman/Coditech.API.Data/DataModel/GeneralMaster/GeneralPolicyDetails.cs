using System.ComponentModel.DataAnnotations;
namespace Coditech.API.Data
{
    public partial class GeneralPolicyDetails
    {
        [Key]
        public short GeneralPolicyDetailId { get; set; }
        public short GeneralPolicyRulesId { get; set; }
        public string CentreCode { get; set; }
        public string PolicyAnswered { get; set; }
        public DateTime? ApplicableFromDate { get; set; }
        public DateTime? ApplicableUptoDate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

