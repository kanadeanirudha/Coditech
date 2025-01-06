using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class PaymentGatewayDetailsViewModel : BaseViewModel
    {
        public int PaymentGatewayDetailId { get; set; }
        [Display(Name = "Payment Gateway")]
        public byte PaymentGatewayId { get; set; }
        public string CentreCode { get; set; }
        [MaxLength(100)]
        [Display(Name = "Gateway User name")]
        public string GatewayUsername { get; set; }
        [MaxLength(100)]
        [Display(Name = "Transaction Key")]
        public string TransactionKey { get; set; }
        [MaxLength(100)]
        [Display(Name = "Gateway Password")]
        public string GatewayPassword { get; set; }
        [Display(Name = "Is Auto Captured")]
        public bool IsAutoCaptured { get; set; }
        [Display(Name = "Is LiveMode On")]
        public bool IsLiveMode { get; set; }
        public string Mode { get; set; }
    }
}
