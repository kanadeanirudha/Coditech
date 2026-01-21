using System.ComponentModel.DataAnnotations;
namespace Coditech.API.Data
{
    public partial class GeneralTemplateHeaderConfiguration
    {
        [Key]
        public int GeneralTemplateHeaderConfigurationId { get; set; }
        public string TemplateCode { get; set; }
        public string HeaderName { get; set; }
        public string HeaderType { get; set; }
        public string CentreCode { get; set; }
        public int OrderBy { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
