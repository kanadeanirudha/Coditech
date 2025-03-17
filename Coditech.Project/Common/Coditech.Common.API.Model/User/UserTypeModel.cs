using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class UserTypeModel : BaseModel
    {
        [Required]
        public short UserTypeId { get; set; }
        public string UserTypeCode { get; set; }
        public string UserDescription { get; set; }
        public bool IsCommon { get; set; } = true;
        public bool IsLoginRequired { get; set; } = true;
        public bool IsActive { get; set; }
    }
}
