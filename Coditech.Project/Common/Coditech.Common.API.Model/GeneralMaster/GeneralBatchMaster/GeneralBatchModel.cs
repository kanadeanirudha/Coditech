using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralBatchModel : BaseModel
    {
        public int GeneralBatchMasterId { get; set; }
        [Required]
        public string CentreCode { get; set; }
        [Required]
        public string BatchName { get; set; }
        [Required]
        public TimeSpan BatchTime { get; set; }
        [Required]
        public TimeSpan BatchStartTime { get; set; }
        public bool IsActive { get; set; }
    }
}
