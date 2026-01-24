using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralTemplateHeaderConfigurationModel : BaseModel
    {
        public int GeneralTemplateHeaderConfigurationId { get; set; }
        public string TemplateCode { get; set; }
        public string HeaderName { get; set; }
        public string HeaderType { get; set; }
        public string CentreCode { get; set; }
        public int OrderBy { get; set; }
        public string DropdownEnumGroupCode { get; set; }
    }
}
