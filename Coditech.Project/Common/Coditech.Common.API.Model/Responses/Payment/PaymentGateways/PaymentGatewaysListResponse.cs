namespace Coditech.Common.API.Model.Response
{
    public class PaymentGatewaysListResponse : BaseListResponse
    {
        public List<PaymentGatewaysModel> PaymentGatewaysList { get; set; }
    }
}
