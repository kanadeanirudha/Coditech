using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalPathologyTestGroup
    {
        [Key]
        public int HospitalPathologyTestGroupId { get; set; }
        public string PathologyTestGroupName { get; set; }
        public int HospitalPathologyTestGroupParentId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

