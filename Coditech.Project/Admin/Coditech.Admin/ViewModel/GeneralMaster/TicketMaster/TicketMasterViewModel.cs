using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class TicketMasterViewModel : BaseViewModel
    {
        public long TicketMasterId { get; set; }
        public long TicketDetailsId { get; set; }      
       
        [Display(Name = "Ticket Number")]
        public string TicketNumber { get; set; }
        public long UserId { get; set; }

        [Required]
        [Display(Name = "Ticket Status")]
        public int TicketStatusEnumId { get; set; }

        [MaxLength(10)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "Ticket Department")]
        public int TicketDepartmentEnumId { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Display(Name = "Ticket Priority")]
        public int TicketPriorityEnumId { get; set; }

        [Display(Name = "Add Cc")]
        public string AddCc { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }
        public string TicketStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<TicketDetailsModel> TicketDetailsList { get; set; }
    }
}
