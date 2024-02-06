namespace Coditech.Common.API.Model
{
    public class OrganisationCentrewiseBuildingListModel : BaseListModel
    {
        public List<OrganisationCentrewiseBuildingModel> OrganisationCentrewiseBuildingList { get; set; }
        public OrganisationCentrewiseBuildingListModel()
        {
            OrganisationCentrewiseBuildingList = new List<OrganisationCentrewiseBuildingModel>();
        }
    }
} 

