using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel

{
    public class TaskMasterViewModel : BaseViewModel
    {
        public short TaskMasterId { get; set; }
        [Required]
        [Display(Name = "Task Code")]
        public string TaskCode { get; set; }
        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; } 
        public bool IsActive { get; set; }  

    }
}
