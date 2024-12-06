using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class TicketMaster
    {
        [Key]
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
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

