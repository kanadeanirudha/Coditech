using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralTrainerMaster
    {
        [Key]
        public long GeneralTrainerMasterId { get; set; }
        public long EmployeeId { get; set; }
        public int TrainerSpecializationEnumId { get; set; }
        public string UniqueCode { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

