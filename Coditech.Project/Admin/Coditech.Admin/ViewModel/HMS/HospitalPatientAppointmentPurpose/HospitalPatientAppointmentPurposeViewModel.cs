﻿using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPatientAppointmentPurposeViewModel : BaseViewModel
    {
        public short HospitalPatientAppointmentPurposeId { get; set; }
        [Required]
        [Display(Name = "Appointment Purpose")]
        public string AppointmentPurpose { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}