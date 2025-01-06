namespace Coditech.Common.API.Model
{
    public class PaymentGatewayDetailsListModel : BaseListModel
    {
        public List<PaymentGatewayDetailsModel> PaymentGatewayDetailsList { get; set; }
        public PaymentGatewayDetailsListModel()
        {
            PaymentGatewayDetailsList = new List<PaymentGatewayDetailsModel>();
        }
    }
}
