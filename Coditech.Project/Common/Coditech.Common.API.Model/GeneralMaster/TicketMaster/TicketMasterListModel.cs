namespace Coditech.Common.API.Model
{
    public class TicketMasterListModel : BaseListModel
    {
        public List<TicketMasterModel> TicketMasterList { get; set; }
        public TicketMasterListModel()
        {
            TicketMasterList = new List<TicketMasterModel>();
        }

    }
}
