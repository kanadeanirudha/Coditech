using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class HospitalPathologyTest
    {
        [Key]
        public long HospitalPathologyTestId { get; set; }
        public int HospitalPathologyTestGroupId { get; set; }
        public string PathologyTestName { get; set; }
        public int TestSampleTypeEnumId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

