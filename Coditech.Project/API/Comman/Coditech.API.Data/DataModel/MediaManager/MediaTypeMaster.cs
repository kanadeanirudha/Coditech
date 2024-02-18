using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaTypeMaster
    {
        [Key]
        public byte MediaTypeMasterId { get; set; }
        public string MediaType { get; set; }
        public bool IsActive { get; set; } = true;
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
