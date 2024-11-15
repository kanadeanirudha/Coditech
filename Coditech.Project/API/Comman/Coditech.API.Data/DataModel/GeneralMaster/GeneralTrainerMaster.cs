using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralTrainerMaster
    {
        [Key]
        public long GeneralTrainerMasterId { get; set; }
        public long EmployeeId { get; set; }
        public int TrainerSpecializationEnumId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

