using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralTemplateModel : BaseModel
    {
        public int GeneralTemplateMasterId { get; set; }
        public string TemplateCode { get; set; }
        public string TemplateName { get; set; }
        public string TemplateType { get; set; }
        public List<GeneralTemplateHeaderConfigurationModel> HeaderConfigurationList { get; set; }
    }
}
