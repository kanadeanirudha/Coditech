namespace Coditech.Common.API.Model
{
    public partial class OrganisationCentrewiseAccountSetupListModel : BaseListModel
    {
        public List<OrganisationCentrewiseAccountSetupModel> OrganisationCentrewiseAccountSetupList { get; set; }
        public OrganisationCentrewiseAccountSetupListModel()
        {
            OrganisationCentrewiseAccountSetupList = new List<OrganisationCentrewiseAccountSetupModel>();
        }

    }
}