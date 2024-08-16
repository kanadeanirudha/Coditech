using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPathologyTestViewModel : BaseViewModel
    {
        public long HospitalPathologyTestId { get; set; }
        [Required]
        public int HospitalPathologyTestGroupId { get; set; }
        [MaxLength(200)]
        [Required]
        [Display(Name = "Pathology Test Name")]
        public string PathologyTestName { get; set; }
        [Required]
        [Display(Name = "Test Sample Type")]
        public int TestSampleTypeEnumId { get; set; }

        [Display(Name = "Pathology Test Group Name")]
        public string PathologyTestGroupName { get; set; }
        public string TestSampleType { get; set; }
    }
}