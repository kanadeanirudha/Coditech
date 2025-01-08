using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class TicketDetailsListViewModel : BaseViewModel
    {
        public List<TicketMasterViewModel> TicketDetailsList { get; set; }
        public TicketDetailsListViewModel()
        {
            TicketDetailsList = new List<TicketMasterViewModel>();
        }
    }
}
