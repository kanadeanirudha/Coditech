using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class AdminRoleApplicableDetailsListViewModel : BaseViewModel
    {
        public AdminRoleApplicableDetailsListViewModel()
        {
            AdminRoleApplicableDetailsList = new List<AdminRoleApplicableDetailsViewModel>();
        }
        public List<AdminRoleApplicableDetailsViewModel> AdminRoleApplicableDetailsList { get; set; }
        public int AdminRoleMasterId { get; set; }
        [Display(Name = "Admin Role Code")]
        public string AdminRoleCode { get; set; }
        [Display(Name = "Role Description")]
        public string SanctionPostName { get; set; }
    }
}
