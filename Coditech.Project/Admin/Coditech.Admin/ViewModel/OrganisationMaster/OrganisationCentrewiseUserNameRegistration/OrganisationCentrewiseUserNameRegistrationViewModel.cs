using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseUserNameRegistrationViewModel : BaseViewModel
    {
        [Required]
        public int OrganisationCentrewiseUserNameRegistrationId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Centre Name")]
        public string CentreName { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Centre Code")]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "User Type")]
        public string UserType { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "UserName BasedOn")]
        public string UserNameBasedOn { get; set; }

        [Display(Name = "Created By")]
        public long CreatedBy { get; set; }
        
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Modified By")]
        public long ModifiedBy { get; set; }

        [Display(Name = "Modified Date")]
        public DateTime ModifiedDate { get; set; }

        


    }
}
