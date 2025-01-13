namespace Coditech.Common.API.Model
{
    public partial class OrganisationCentrewiseJoiningCodeModel : BaseModel
    {
        public string CentreCode { get; set; }
        public string JoiningCode { get; set; }
        public int Quantity { get; set; }
        public bool IsExpired { get; set; }
    }
}
