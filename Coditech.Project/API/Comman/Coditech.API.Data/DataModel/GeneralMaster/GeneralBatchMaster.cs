using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralBatchMaster
    {
        [Key]
        public int GeneralBatchMasterId { get; set; }
        public string CentreCode { get; set; }
        public string BatchName { get; set; }
        public TimeSpan BatchTime { get; set; }
        public TimeSpan BatchStartTime { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

