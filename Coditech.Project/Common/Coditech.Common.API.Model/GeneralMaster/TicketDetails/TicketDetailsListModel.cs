namespace Coditech.Common.API.Model
{
    public class TicketDetailsListModel : BaseListModel
    {
        public List<TicketDetailsModel> TicketDetailsList { get; set; }
        public TicketDetailsListModel()
        {
            TicketDetailsList = new List<TicketDetailsModel>();
        }

    }
}
