using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralLeadGenerationModel : BaseModel
    {
        [Required]
        public long GeneralLeadGenerationMasterId { get; set; } // bigint
        public string UserTypeCode { get; set; } // nvarchar(50) 

        [MaxLength(50)]
        [Required]
        public string PersonTitle { get; set; } // nvarchar(50) 

        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; } // nvarchar(50) 

        [MaxLength(50)]
        public string MiddleName { get; set; } // nvarchar(50) 

        [MaxLength(50)]
        [Required]
        public string LastName { get; set; } // nvarchar(50) 

        public DateTime? DateOfBirth { get; set; } // datetime 

        [Required]
        public int GenderEnumId { get; set; } // int 

        [MaxLength(250)]
        public string EmailId { get; set; } // varchar(250) 

        [MaxLength(50)]
        public string PhoneNumber { get; set; } // varchar(50) 

        [MaxLength(15)]
        [Required]
        public string MobileNumber { get; set; } // nvarchar(15) 

        [Required]
        public int LeadGenerationSourceEnumId { get; set; } // int 

        [MaxLength]
        [Required]
        public string LeadGenerationCategoryEnumIds { get; set; } // varchar(MAX) 

        [Required]
        public int LeadGenerationStatusEnumId { get; set; } // int 

        [Required]
        public bool IsConverted { get; set; } // bit 

        public string Custom1 { get; set; } // nvarchar(max) 

        public string Custom2 { get; set; } // nvarchar(max) 

        public string Custom3 { get; set; } // nvarchar(max) 

        public string Custom4 { get; set; } // nvarchar(max) 

        public string Custom5 { get; set; } // nvarchar(max) 
        public string LeadGenerationSource { get; set; }
        public string LeadGenerationStatus { get; set; }
    }
}
