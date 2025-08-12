using Coditech.Common.Helper;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralBatchViewModel : BaseViewModel
    {
        public int GeneralBatchMasterId { get; set; }
        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }
        [Required]
        [Display(Name = "Batch Name")]
        public string BatchName { get; set; }   
        [Display(Name = "Frequency")]
        public string BatchFrequency { get; set; }
        public string WeekDays { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        [Display(Name = "Start Date")]
        public DateTime? BatchStartDate { get; set; }
        [Required]
        [Display(Name = "Batch Start Time")]
        public TimeSpan? BatchStartTime { get; set; }
        [Display(Name = "Duration")]
        public TimeSpan? Duration { get; set; }
        [Display(Name = "Weekly")]
        public List<string> SelectedWeekDays { get; set; } = new List<string>();
        public List<SelectListItem> SchedulerWeekDaysList { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Duration Hours is required.")]
        public string DurationHours { get; set; }
        [Required(ErrorMessage = "Duration Minutes is required.")]
        public string DurationMinutes { get; set; }
        public string AssignedBy { get; set; }
    }
}
