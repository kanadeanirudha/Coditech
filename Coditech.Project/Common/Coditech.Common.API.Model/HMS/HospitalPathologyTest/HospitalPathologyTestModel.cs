using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class HospitalPathologyTestModel : BaseModel
    {
        public long HospitalPathologyTestId { get; set; }
        [Required]
        public int HospitalPathologyTestGroupId { get; set; }
        [MaxLength(200)]
        [Required]
        public string PathologyTestName { get; set; }
        [Required]
        public int TestSampleTypeEnumId { get; set; }
        public string PathologyTestGroupName { get; set; }
        public string TestSampleType { get; set; }
    }
}
