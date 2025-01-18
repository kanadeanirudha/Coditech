namespace Coditech.Common.API.Model
{
    public partial class AccSetupBalanceSheetTypeModel : BaseModel
    {
        public byte AccSetupBalanceSheetTypeId { get; set; } 
        public string AccBalsheetTypeCode { get; set; }  
        public string AccBalsheetTypeDesc { get; set; }
        public bool IsActive { get; set; }   
        public bool IsSystemGenerated { get; set; }   
    }
}
