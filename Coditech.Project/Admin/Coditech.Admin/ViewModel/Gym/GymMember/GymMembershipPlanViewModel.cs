﻿using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GymMembershipPlanViewModel : BaseViewModel
    {
        public GymMembershipPlanViewModel()
        {
        }
        public int GymMembershipPlanlId { get; set; }
        public long MembershipPlanName { get; set; }
        [MaxLength(100)]
        public string PersonCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }

        [MaxLength(500)]
        [Display(Name = "Past Injuries")]
        public string PastInjuries { get; set; }

        [MaxLength(500)]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; }

        [MaxLength(500)]
        [Display(Name = "Other Information")]
        public string OtherInformation { get; set; }

        [Required]
        [Display(Name ="Group")]
        public int GymGroupEnumId { get; set; }
        [Required]
        [Display(Name = "Source")]
        public int? SourceEnumId { get; set; }
        public string ImagePath { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
    }
}
