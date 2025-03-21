﻿using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralLeadGenerationViewModel : BaseViewModel
    {
        [Required]
        [Display(Name = "Lead Generation ID")]
        public long GeneralLeadGenerationMasterId { get; set; }

        [Display(Name = "User Type")]
        [Required]
        public string UserTypeCode { get; set; }

        [MaxLength(50)]
        [MinLength(1)]
        [Required]
        [Display(Name = "Title")]
        public string PersonTitle { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string FirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "These fields must accept alphabetic characters")]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }  

        [Required]
        [Display(Name = "Gender")]
        public int GenderEnumId { get; set; }

        [MaxLength(200)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Id")]
        public string EmailId { get; set; }

        [MaxLength(50)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter valid mobile number")]
        [MaxLength(10)]
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Required]
        [Display(Name = "Lead Generation Source")]
        public int LeadGenerationSourceEnumId { get; set; }  
        
        [Required]
        [Display(Name = "Lead Generation Category")]
        public string LeadGenerationCategoryEnumIds { get; set; }  

        [Required]
        [Display(Name = "Lead Generation Status")]
        public int LeadGenerationStatusEnumId { get; set; }  

        [Required]
        [Display(Name = "Is Converted")]
        public bool IsConverted { get; set; }  

        [Display(Name = "Custom 1")]
        public string Custom1 { get; set; } 
        
        [Display(Name = "Custom 2")]
        public string Custom2 { get; set; } 
        
        [Display(Name = "Custom 3")]
        public string Custom3 { get; set; }  
        
        [Display(Name = "Custom 4")]
        public string Custom4 { get; set; } 
        
        [Display(Name = "Custom 5")]
        public string Custom5 { get; set; }
        
        [Display(Name = "Lead Generation Source")]
        public string LeadGenerationSource { get; set; }

        [Display(Name = "Lead Generation Category")]
        public string LeadGenerationCategory { get; set; }

        [Display(Name = "Lead Generation Status")]
        public string LeadGenerationStatus { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "LabelCentre", ResourceType = typeof(AdminResources))]
        public string CentreCode { get; set; }
        public string SelectedCentreCode { get; set; }
       
    }
}
