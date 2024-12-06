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
        public long UserId { get; set; }
        public string TicketStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
