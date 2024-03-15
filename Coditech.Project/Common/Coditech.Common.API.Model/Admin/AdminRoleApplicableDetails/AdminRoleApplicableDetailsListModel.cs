namespace Coditech.Common.API.Model
{
    public class AdminRoleApplicableDetailsListModel : BaseListModel
    {
        public List<AdminRoleApplicableDetailsModel> AdminRoleApplicableDetailsList { get; set; }
        public AdminRoleApplicableDetailsListModel()
        {
            AdminRoleApplicableDetailsList = new List<AdminRoleApplicableDetailsModel>();
        }
        public string AdminRoleCode { get; set; }
        public string SanctionPostName { get; set; }
    }
}
