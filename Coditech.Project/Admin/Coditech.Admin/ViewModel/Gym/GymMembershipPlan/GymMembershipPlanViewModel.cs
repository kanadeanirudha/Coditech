﻿using Coditech.Common.Helper;
using Coditech.Resources;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymMembershipPlanViewModel : BaseViewModel
    {
        public GymMembershipPlanViewModel()
        {
        }
        public int GymMembershipPlanId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "Membership Plan Name")]
        public string MembershipPlanName { get; set; }

        [Required]
        [Display(Name = "Max Cost")]
        public decimal MaxCost { get; set; }

        [Required]
        [Display(Name = "Min Cost")]
        public decimal MinCost { get; set; }

        [Required]
        [Display(Name = "Plan Type")]
        public int PlanTypeEnumId { get; set; }

        [Display(Name = "Plan Type")]
        public string PlanType { get; set; }

        [Display(Name = "Plan Duration In Month")]
        public byte PlanDurationInMonth { get; set; }

        [Display(Name = "Plan Duration In Days")]
        public byte PlanDurationInDays { get; set; }

        [Display(Name = "Plan Duration In Session")]
        public Int16 PlanDurationInSession { get; set; }

        [Display(Name = "Is Renewable")]
        public Boolean IsRenewable { get; set; }

        [Display(Name = "Is Timebase Biometric Access")]
        public Boolean IsTimebaseBiometricAccess { get; set; }

        [Display(Name = "From Time")]
        public TimeSpan FromTime { get; set; }

        [Display(Name = "To Time")]
        public TimeSpan ToTime { get; set; }

        [Display(Name = "Is Active")]
        public Boolean IsActive { get; set; }
    }
}
