using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralEnumaratorGroupModel : BaseModel
    {
        public GeneralEnumaratorGroupModel()
        {

        }
        public int GeneralEnumaratorGroupId { get; set; }
        public string EnumGroupCode { get; set; }
        public string DisaplyText { get; set; }
        public new Nullable<int> CreatedBy { get; set; }
        public new Nullable<System.DateTime> CreatedDate { get; set; }
        public new Nullable<int> ModifiedBy { get; set; }
        public new Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
