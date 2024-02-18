namespace Coditech.Common.API.Model
{
    public class GeneralEnumaratorGroupModel : BaseModel
    {
        public GeneralEnumaratorGroupModel()
        {
        }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumGroupCode { get; set; }
        public string DisplayText { get; set; }
        public List<GeneralEnumaratorModel> GeneralEnumaratorList { get; set; }
    }
}
