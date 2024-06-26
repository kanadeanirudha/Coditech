﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalDoctorOPDSchedule
    {
        [Key]
        public long HospitalDoctorOPDScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public int WeekDayEnumId { get; set; }
        public string OPDTimesOfDay { get; set; }
        public TimeSpan FromTime { get; set; }
        public TimeSpan UptoTime { get; set; }
        public Byte TimeSlotInMinute { get; set; }
        public string TimeZone { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

