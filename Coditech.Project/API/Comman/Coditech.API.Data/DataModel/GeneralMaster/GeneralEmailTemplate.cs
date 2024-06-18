namespace Coditech.API.Data
{
    public partial class GeneralEmailTemplate
    {
        public short GeneralEmailTemplateId { get; set; }
        
        public string EmailTemplateCode { get; set; }
        
        public string EmailTemplateName { get; set; }
        
        public string Subject { get; set; }
        
        public string EmailTemplate { get; set; }
        public string ModuleCode { get; set; }
        public bool IsSmsTemplate { get; set; }
        public bool IsWhatsUpTemplate { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
