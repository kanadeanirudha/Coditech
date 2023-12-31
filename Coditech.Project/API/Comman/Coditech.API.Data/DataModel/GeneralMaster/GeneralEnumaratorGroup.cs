using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class GeneralEnumaratorGroup
    {
        [Key]
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumGroupCode { get; set; }
        public string DisaplyText { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}




