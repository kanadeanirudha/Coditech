using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public partial class PaymentGatewayDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentGatewayDetailId { get; set; }
        public byte PaymentGatewayId { get; set; }
        public string CentreCode { get; set; }
        public string GatewayUsername { get; set; }
        public string TransactionKey { get; set; }
        public string GatewayPassword { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public bool IsAutoCaptured { get; set; }
        public bool IsLiveMode { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
