using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;
namespace Coditech.Admin.ViewModel
{
    public partial class AccSetupBalanceSheetTypeViewModel : BaseViewModel
    {
        public byte AccSetupBalanceSheetTypeId { get; set; }
        public string AccBalsheetTypeCode { get; set; }
        public string AccBalsheetTypeDesc { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemGenerated { get; set; }
    }
}
