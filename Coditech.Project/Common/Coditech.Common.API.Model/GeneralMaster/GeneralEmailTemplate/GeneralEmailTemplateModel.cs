using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralEmailTemplateModel : BaseModel
    {
        public GeneralEmailTemplateModel()
        {

        }
        [Required]
        public short GeneralEmailTemplateId { get; set; }
        [Required]
        public string EmailTemplateCode { get; set;}
        [Required]
        public string EmailTemplateName { get; set;}
        [Required]
        public string Subject { get; set;}
        [Required]
        public string EmailTemplate { get; set;}
        [Required]
        public bool IsActive { get; set;}
    }
}
