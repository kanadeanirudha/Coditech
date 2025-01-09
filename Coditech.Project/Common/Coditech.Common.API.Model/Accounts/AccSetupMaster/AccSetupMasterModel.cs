namespace Coditech.Common.API.Model
{
    public class AccSetupMasterModel : BaseModel
    {
        public short AccSetupMasterId { get; set; }
        public byte FiscalYearDay { get; set; }
        public string CentreCode{ get; set; }
        public byte FiscalYearMonth { get; set; }
        public bool IsActive { get; set; }
    }
}
