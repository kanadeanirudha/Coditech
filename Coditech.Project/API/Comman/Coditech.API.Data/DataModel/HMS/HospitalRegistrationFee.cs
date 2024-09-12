using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalRegistrationFee
    {
        [Key]
        public int HospitalRegistrationFeeId { get; set; }
        public string CentreCode { get; set; }
        public int InventoryGeneralItemLineId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? UptoDate { get; set; }
        public decimal Charges { get; set; }
        public bool IsTaxExclusive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}



