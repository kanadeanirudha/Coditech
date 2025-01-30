using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public partial class AccSetupBalanceSheetModel : BaseModel
    {       
        public int AccSetupBalanceSheetId { get; set; }
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string AccBalancesheetCode { get; set; }
        public string AccBalancesheetHeadDesc { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
        public string AccBalsheetTypeDesc { get; set; }

    }
}

