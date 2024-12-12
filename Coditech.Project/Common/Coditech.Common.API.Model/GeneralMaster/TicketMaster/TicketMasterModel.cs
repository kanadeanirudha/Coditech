namespace Coditech.Common.API.Model
{
    public class TicketMasterModel : BaseModel
    {
        public long TicketMasterId { get; set; }
        public string TicketNumber { get; set; }
        public long UserId { get; set; }
        public int TicketStatusEnumId { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public int TicketDepartmentEnumId { get; set; }
        public string Subject { get; set; }
        public int TicketPriorityEnumId { get; set; }
        public string AddCc { get; set; }
        public string Details { get; set; }
        public string TicketStatus { get; set; }
        public bool IsTicketReplied { get; set; }
        public List<TicketDetailsModel> TicketDetailsList { get; set; }

    }
}
