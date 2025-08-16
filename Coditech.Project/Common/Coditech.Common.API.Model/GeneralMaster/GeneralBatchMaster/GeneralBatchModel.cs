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
        public DateTime BatchStartDate { get; set; }
        [Required]
        public DateTime BatchExpireDate { get; set; }
        [Required]
        public TimeSpan BatchStartTime { get; set; }
        public string BatchFrequency { get; set; }
        public string WeekDays { get; set; }        
        public TimeSpan? Duration { get; set; }
        public bool IsActive { get; set; }
        public List<string> SelectedWeekDays { get; set; } = new List<string>();
        public string DurationHours { get; set; }
        public string DurationMinutes { get; set; }
        public string AssignedBy { get; set; }
    }
}
