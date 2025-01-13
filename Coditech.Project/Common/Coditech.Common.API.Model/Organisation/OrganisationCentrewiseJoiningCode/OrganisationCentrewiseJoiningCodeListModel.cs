namespace Coditech.Common.API.Model
{
    public partial class OrganisationCentrewiseJoiningCodeListModel : BaseListModel
    {
        public List<OrganisationCentrewiseJoiningCodeModel> OrganisationCentrewiseJoiningCodeList { get; set; }
        public OrganisationCentrewiseJoiningCodeListModel()
        {
            OrganisationCentrewiseJoiningCodeList = new List<OrganisationCentrewiseJoiningCodeModel>();
        }
    }
}
