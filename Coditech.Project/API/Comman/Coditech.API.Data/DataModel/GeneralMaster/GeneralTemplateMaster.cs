using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralTemplateMaster
    {
        [Key]
        public int GeneralTemplateMasterId { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
