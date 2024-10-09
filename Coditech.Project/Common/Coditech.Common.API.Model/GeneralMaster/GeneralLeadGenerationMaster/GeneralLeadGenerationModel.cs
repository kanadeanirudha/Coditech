using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralLeadGenerationModel : BaseModel
    {
        [Required]
        public long GeneralLeadGenerationMasterId { get; set; }
        public string UserTypeCode { get; set; } 

        [MaxLength(50)]
        [Required]
        public string PersonTitle { get; set; } 

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }  

        [MaxLength(50)]
        public string MiddleName { get; set; }  

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } 

        public DateTime? DateOfBirth { get; set; }  

        [Required]
        public int GenderEnumId { get; set; } 

        [MaxLength(250)]
        public string EmailId { get; set; }  

        [MaxLength(50)]
        public string PhoneNumber { get; set; } 

        [MaxLength(15)]
        [Required]
        public string MobileNumber { get; set; } 

        [Required]
        public int LeadGenerationSourceEnumId { get; set; } 
        [MaxLength]
        [Required]
        public string LeadGenerationCategoryEnumIds { get; set; }
        [Required]
        public int LeadGenerationStatusEnumId { get; set; }

        [Required]
        public bool IsConverted { get; set; } 
        public string LeadGenerationSource { get; set; }
        public string LeadGenerationStatus { get; set; }
        public string Comments { get; set; }
        public string Location { get; set; }
         public string SelectedCentreCode { get; set; }
    }
}
