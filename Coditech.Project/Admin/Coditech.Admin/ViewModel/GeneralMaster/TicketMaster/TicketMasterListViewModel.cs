using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class TicketMasterListViewModel : BaseViewModel
    {
        public List<TicketMasterViewModel> TicketMasterList { get; set; }
        public TicketMasterListViewModel()
        {
            TicketMasterList = new List<TicketMasterViewModel>();
        }
        public bool IsTicketReplied { get; set; }
    }
}
