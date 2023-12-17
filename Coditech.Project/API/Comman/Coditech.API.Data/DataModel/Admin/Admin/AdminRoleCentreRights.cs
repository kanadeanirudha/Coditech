using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class AdminRoleCentreRights : BaseDataModel
    {
        [Key]
        public int AdminRoleCentreRightId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public string CentreCode { get; set; }
        public bool IsActive { get; set; }
    }
}

