using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalPathologyTestGroupViewModel : BaseViewModel
    {
        public int HospitalPathologyTestGroupId { get; set; }

        [MaxLength(200)]
        [Required]
        [Display(Name = "Pathology Test Group Name")]
        public string PathologyTestGroupName { get; set; }

        [Display(Name = "Pathology Test Group Parent ID")]
        public int HospitalPathologyTestGroupParentId { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}