namespace Coditech.Common.API.Model.Response
{
    public class PaymentGatewayDetailsListResponse : BaseListResponse
    {
        public List<PaymentGatewayDetailsModel> PaymentGatewayDetailsList { get; set; }
    }
}
