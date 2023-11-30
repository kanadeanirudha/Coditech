using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralEnumaratorGroupModel : BaseModel
    {
        public GeneralEnumaratorGroupModel()
        {

        }
        public short GeneralEnumaratorGroupId { get; set; }
        public string EnumGroup { get; set; }
        public new int CreatedBy { get; set; }
        public new DateTime CreatedDate { get; set; }
        public new int ModifiedBy { get; set; }
        public new DateTime ModifiedDate { get; set; }
    }
}
