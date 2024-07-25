using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentrewiseEmailTemplate
    {
        [Key]
        public short OrganisationCentrewiseEmailTemplateId { get; set; }      
        public string CentreCode { get; set; }
        public string EmailTemplateCode { get; set; }        
        public string Subject { get; set; }
        public string EmailTemplate { get; set; }
        public bool IsActive { get; set; }
        public bool IsSmsTemplate { get; set; }
        public bool IsWhatsAppTemplate { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
