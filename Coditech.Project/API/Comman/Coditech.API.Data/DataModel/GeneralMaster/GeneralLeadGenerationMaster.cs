using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralLeadGenerationMaster
    {

        [Key]
        public long GeneralLeadGenerationMasterId { get; set; } 
        public string UserTypeCode { get; set; }
        public string PersonTitle { get; set; }
        public string FirstName { get; set; } 
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public int GenderEnumId { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public int LeadGenerationSourceEnumId { get; set; }
        public string LeadGenerationCategoryEnumIds { get; set; }
        public string Comments { get; set; }
        public string Location { get; set; }
        public int LeadGenerationStatusEnumId { get; set; }
        public bool IsConverted { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

