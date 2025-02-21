namespace Coditech.API.Data
{
    public class AccSetupGLBalanceSheet
    {
        public int AccSetupGLBalanceSheetId { get; set; }
        public int AccSetupBalanceSheetId { get; set; }
        public int AccSetupGLId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
