namespace Coditech.Common.API.Model
{
    public class TicketDetailsModel : BaseModel
    {
        public long TicketDetailsId { get; set; }
        public long TicketMasterId { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
