namespace Coditech.API.Data
{
    public partial class GeneralEnumarator : BaseDataModel
    {
        public int GeneralEnumaratorId { get; set; }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumName { get; set; }
        public string EnumDisplayText { get; set; }
        public short EnumValue { get; set; }
        public short SequenceNumber { get; set; }
    }
}




