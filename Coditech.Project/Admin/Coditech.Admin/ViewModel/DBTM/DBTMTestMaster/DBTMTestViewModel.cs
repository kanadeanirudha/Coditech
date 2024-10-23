using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class DBTMTestViewModel : BaseViewModel
    {
        public int DBTMTestMasterId { get; set; }

        [Display(Name = "Activity Category")]
        public short DBTMActivityCategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Test Name")]
        public string TestName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Test Code")]
        public string TestCode { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
