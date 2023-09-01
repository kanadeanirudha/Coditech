namespace Coditech.Common.API.Model
{
    public class OrganisationCentreListModel : BaseListModel
    {
        public List<OrganisationCentreMasterModel> OrganisationCentreList { get; set; }
        public OrganisationCentreListModel()
        {
            OrganisationCentreList = new List<OrganisationCentreMasterModel>();
        }
    }
}

