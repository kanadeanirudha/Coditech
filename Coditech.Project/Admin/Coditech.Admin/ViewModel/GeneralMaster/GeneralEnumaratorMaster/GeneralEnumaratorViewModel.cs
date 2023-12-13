using Coditech.Common.Helper;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class GeneralEnumaratorViewModel : BaseViewModel
    {
        public int GeneralEnumaratorMasterId { get; set; }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumGroupCode { get; set; }
        [Required]
        [Display(Name = "EnumName")]
        public string EnumName { get; set; }
        public string EnumDisplayText { get; set; }
        public short EnumValue { get; set; }
        public short SequenceNumber { get; set; }
    }
}
