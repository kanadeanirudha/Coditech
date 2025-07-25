﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class TaskSchedulerModel : BaseModel
    {
        public int TaskSchedulerMasterId { get; set; }
        public int ConfiguratorId { get; set; }
        public string SchedulerName { get; set; }
        public string SchedulerClassName { get; set; }
        public string SchedulerType { get; set; }
        public string SchedulerCallFor { get; set; }
        public string SchedulerCallForDisplayText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string SchedulerFrequency { get; set; }
        public int RepeatTaskEvery { get; set; }
        public string RepeatTaskForDuration { get; set; }
        public bool IsEnabled { get; set; }
        public string WeekDays { get; set; }
        public string Months { get; set; }
        public string Days { get; set; }
        public string OnDays { get; set; }
        public string OnMonths { get; set; }
        public int RecurEvery { get; set; }
        public string CronExpression { get; set; }
        public string HangfireJobId { get; set; }
        public bool IsMonthlyDays { get; set; }
        public List<string> SelectedWeekDays { get; set; } = new List<string>();
        public TimeSpan StartTime { get; set; }
        public TimeSpan ExpireTime { get; set; }
        public bool IsInstantJob { get; set; }
        public bool IsCronJob { get; set; }
        public string ExeParameters { get; set; }
        public string CentreCode { get; set; }
    }
}

