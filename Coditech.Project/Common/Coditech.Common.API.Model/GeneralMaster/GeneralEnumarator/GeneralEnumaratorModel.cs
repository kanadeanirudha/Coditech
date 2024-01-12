namespace Coditech.Common.API.Model
{
    public class GeneralEnumaratorModel : BaseModel
    {
        public GeneralEnumaratorModel()
        {
        }
        public int GeneralEnumaratorId { get; set; }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumGroupCode { get; set; }
        public string EnumName { get; set; }
        public string EnumDisplayText { get; set; }
        public string RelatedWith { get; set; }
        public short EnumValue { get; set; }
        public short SequenceNumber { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
    }
}
