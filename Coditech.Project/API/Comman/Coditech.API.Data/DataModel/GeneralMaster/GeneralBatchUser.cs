using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralBatchUser
    {
        [Key]
        public long GeneralBatchUserId { get; set; }
        public int GeneralBatchMasterId { get; set; }
        public long EntityId { get; set; }
        public string UserType { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

