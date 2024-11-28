using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class TaskApprovalSettingViewModel : BaseViewModel
    {
        public int TaskApprovalSettingId { get; set; }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }
        [Required]
        public short TaskMasterId { get; set; }
        public long EmployeeId { get; set; }      
        public string CentreName { get; set; }
        [Display(Name = "Task Code")]
        public string TaskCode { get; set; }
        public string TaskDescription { get; set; }
        public byte CountNumber { get; set; }
    }
}
