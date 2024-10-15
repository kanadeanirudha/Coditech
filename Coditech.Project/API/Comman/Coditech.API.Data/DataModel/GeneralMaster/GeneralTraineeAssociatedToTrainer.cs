using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralTraineeAssociatedToTrainer
    {
        [Key]
        public long GeneralTraineeAssociatedToTrainerId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public long GeneralTrainerMasterId { get; set; }
        public bool IsCurrentTrainer { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

