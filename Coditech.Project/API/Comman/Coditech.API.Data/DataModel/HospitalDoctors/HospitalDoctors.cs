﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalDoctors
    {
        [Key]
        public int HospitalDoctorId { get; set; }
        public long EmployeeId { get; set; }
        public int MedicalSpecilizationEnumId { get; set; }
        public string WeekDayEnumIds { get; set; }
        public short OrganisationCentrewiseBuildingRoomId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

