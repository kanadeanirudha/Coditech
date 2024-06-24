using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class CoditechApplicationSettingModel : BaseModel
    {
        [Required]
        public short CoditechApplicationSettingId { get; set; }

        [MaxLength(100)]
        [Required]
        public string ApplicationCode { get; set; }

        [MaxLength(500)]
        [Required]
        public string ApplicationValue1 { get; set; }

        [MaxLength(500)]
        public string ApplicationValue2 { get; set; }

        [MaxLength(500)]
        public string ApplicationValue3 { get; set; }
    }
}
