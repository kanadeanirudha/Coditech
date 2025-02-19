using Coditech.Common.Helper;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class TaskSchedulerViewModel : BaseViewModel
    {

        [Required]
        public int TaskSchedulerMasterId { get; set; }
        [Required]
        public int ConfiguratorId { get; set; }
        [Display(Name = "Scheduler Name")]
        public string SchedulerName { get; set; }
        [Display(Name = "Scheduler Type")]
        public string SchedulerType { get; set; }
        public string SchedulerCallFor { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Expire Date")]
        public DateTime ExpireDate { get; set; }
        [Display(Name = "Frequency")]
        public string SchedulerFrequency { get; set; }
        public int RepeatTaskEvery { get; set; }
        public string RepeatTaskForDuration { get; set; }
        [Display(Name = "Week Days")]
        public string WeekDays { get; set; }
        public string Months { get; set; }
        public string Days { get; set; }
        public string OnDays { get; set; }
        public string OnMonths { get; set; }
        public int RecurEvery { get; set; }
        [Display(Name = "Is Monthly Days")]
        public bool IsMonthlyDays { get; set; }
        [Display(Name = "Is Active")]
        public bool IsEnabled { get; set; }
        public List<string> SelectedWeekDays { get; set; } = new List<string>();
        public List<SelectListItem> SchedulerWeekDaysList { get; set; }
    }
}
