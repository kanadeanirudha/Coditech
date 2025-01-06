using Coditech.Common.Helper;
namespace Coditech.Admin.ViewModel
{
    public partial class PaymentGatewaysListViewModel : BaseViewModel
    {
        public List<PaymentGatewaysViewModel> PaymentGatewaysList { get; set; }
        public PaymentGatewaysListViewModel()
        {
            PaymentGatewaysList = new List<PaymentGatewaysViewModel>();
        }
    }
}
