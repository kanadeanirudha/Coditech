using Coditech.Common.Helper;
using Coditech.Model;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class OrganisationCentrewiseUserNameRegistrationViewModel : BaseViewModel
    {
        public short OrganisationCentreMasterId { get; set; }
        public short OrganisationCentrewiseUserNameRegistrationId { get; set; }

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
        [Display(Name = "Username BasedOn")]
        public string UserNameBasedOn { get; set; }
        public List<OrganisationCentrewiseUserNameRegistrationModel> CentrewiseUserNameRegistrationList { get; set; }
    }
}
