namespace Coditech.Common.API.Model
{
    public partial class PaymentGatewayDetailsModel : BaseModel
    {
        public int PaymentGatewayDetailId { get; set; }
        public byte PaymentGatewayId { get; set; }
        public string CentreCode { get; set; }
        public string GatewayUsername { get; set; }
        public string TransactionKey { get; set; }
        public string GatewayPassword { get; set; }
        public bool IsAutoCaptured { get; set; }
        public bool IsLiveMode { get; set; }
        public string Mode { get; set; }
    }
}
