using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class TicketDetails
    {
        [Key]
        public long TicketDetailsId { get; set; }
        public long TicketMasterId { get; set; }
        public string Details { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

