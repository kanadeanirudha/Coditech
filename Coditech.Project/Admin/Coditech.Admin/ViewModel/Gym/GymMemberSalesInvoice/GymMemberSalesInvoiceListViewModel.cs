using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class GymMemberSalesInvoiceListViewModel : BaseViewModel
    {
        public List<GymMemberSalesInvoiceViewModel> GymMemberSalesInvoiceList { get; set; }
        public GymMemberSalesInvoiceListViewModel()
        {
            GymMemberSalesInvoiceList = new List<GymMemberSalesInvoiceViewModel>();
        }
        public long PersonId { get; set; }
        public int GymMemberDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedCentreCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
