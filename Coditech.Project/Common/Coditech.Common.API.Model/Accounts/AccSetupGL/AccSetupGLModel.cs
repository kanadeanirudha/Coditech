namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLModel : BaseModel
    {
       
        public int AccSetupGLId { get; set; }
        public string GLName { get; set; }
        public int? ParentAccSetupGLId { get; set; }
        public string CategoryCode { get; set; }
        public decimal OpeningBalance { get; set; }
        public bool IsGroup { get; set; }
        public int AccSetupBalancesheetId { get; set; }
        public byte AccSetupChartOfAccountTemplateId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string SelectedCentreCode { get; set; }
        public List<AccSetupGLModel> AccSetupGLList { get; set; }
        public List<AccSetupGLModel> SubAccounts { get; set; } = new List<AccSetupGLModel>();
    }
}
