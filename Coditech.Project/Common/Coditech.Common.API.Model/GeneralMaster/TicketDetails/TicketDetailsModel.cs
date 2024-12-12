namespace Coditech.Common.API.Model
{
    public class TicketDetailsModel : BaseModel
    {
        public long TicketDetailsId { get; set; }
        public long TicketMasterId { get; set; }
        public string Details { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
