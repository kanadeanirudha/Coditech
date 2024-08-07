using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalPathologyTestGroupModel : BaseModel
    {
        public int HospitalPathologyTestGroupId { get; set; }
        [MaxLength(200)]
        [Required]
        public string PathologyTestGroupName { get; set; }
        public int HospitalPathologyTestGroupParentId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
