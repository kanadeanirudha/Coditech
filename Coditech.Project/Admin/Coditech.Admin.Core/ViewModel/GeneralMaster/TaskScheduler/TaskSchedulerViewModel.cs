using Coditech.Common.Helper;
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
        [Display(Name = "Scheduler Call For")]
        public string SchedulerCallFor { get; set; }
        public string SchedulerCallForDisplayText { get; set; }
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime? ExpireDate { get; set; }
        [Display(Name = "Frequency")]
        public string SchedulerFrequency { get; set; }
        public int RepeatTaskEvery { get; set; }
        public string RepeatTaskForDuration { get; set; }     
        public string WeekDays { get; set; }
        public string Months { get; set; }
        public string Days { get; set; }
        public string OnDays { get; set; }
        public string OnMonths { get; set; }
        public int RecurEvery { get; set; }
        
        [Display(Name = "Cron Expression")]
        public string CronExpression { get; set; }
        public string HangfireJobId { get; set; }
        [Display(Name = "Is Monthly Days")]
        public bool IsMonthlyDays { get; set; }
        [Display(Name = "Is Active")]
        public bool IsEnabled { get; set; }
        [Display(Name = "Week Days")]
        public List<string> SelectedWeekDays { get; set; } = new List<string>();
        public List<SelectListItem> SchedulerWeekDaysList { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [Display(Name = "End Time")]
        public TimeSpan ExpireTime { get; set; }
        public bool IsCronJob { get; set; }
    }
}
