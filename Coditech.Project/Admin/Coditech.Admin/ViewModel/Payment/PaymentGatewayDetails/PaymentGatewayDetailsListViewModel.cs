using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class PaymentGatewayDetailsListViewModel : BaseViewModel
    {
        public List<PaymentGatewayDetailsViewModel> PaymentGatewayDetailsList { get; set; }
        public PaymentGatewayDetailsListViewModel()
        {
            PaymentGatewayDetailsList = new List<PaymentGatewayDetailsViewModel>();
        }

        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string SelectedCentreCode { get; set; } =null;
        public string SelectedParameter1 { get; set; } =null;
      
    }
}
