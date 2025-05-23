﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class TaskSchedulerMaster
    {
        [Key]
        public int TaskSchedulerMasterId { get; set; }
        public int ConfiguratorId { get; set; }
        public string SchedulerName { get; set; }
        public string SchedulerType { get; set; }
        public string SchedulerCallFor { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> ExpireDate { get; set; }
        public string SchedulerFrequency { get; set; }
        public int RepeatTaskEvery { get; set; }
        public string RepeatTaskForDuration { get; set; }
        public string WeekDays { get; set; }
        public string Months { get; set; }
        public string Days { get; set; }
        public string OnDays { get; set; }
        public string OnMonths { get; set; }
        public int RecurEvery { get; set; }
        public string CronExpression { get; set; }
        public string HangfireJobId { get; set; }
        public bool IsMonthlyDays { get; set; }
        public bool IsEnabled { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

