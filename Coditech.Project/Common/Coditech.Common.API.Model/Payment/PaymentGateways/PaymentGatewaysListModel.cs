namespace Coditech.Common.API.Model
{
    public class PaymentGatewaysListModel : BaseListModel
    {
        public List<PaymentGatewaysModel> PaymentGatewaysList { get; set; }
        public PaymentGatewaysListModel()
        {
            PaymentGatewaysList = new List<PaymentGatewaysModel>();
        }
    }
}
