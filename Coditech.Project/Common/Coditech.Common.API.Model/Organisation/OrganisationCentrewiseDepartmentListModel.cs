namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseDepartmentListModel : BaseListModel
    {
        public List<OrganisationCentrewiseDepartmentModel> OrganisationCentrewiseDepartmentList { get; set; }
        public OrganisationCentrewiseDepartmentListModel()
        {
            OrganisationCentrewiseDepartmentList = new List<OrganisationCentrewiseDepartmentModel>();
        }
    }
}

