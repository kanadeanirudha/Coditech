namespace Coditech.Common.API.Model
{
    public partial class OrganisationCentrewiseAccountSetupModel : BaseModel
    {
        public int OrganisationCentrewiseAccountSetupId { get; set; }
        public string CentreCode { get; set; }
        public short GeneralCurrencyMasterId { get; set; }
    }
}
