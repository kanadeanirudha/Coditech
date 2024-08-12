using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class AdminRoleMediaFolderAction
    {
        [Key]
        public int AdminRoleMediaFolderActionId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public string MediaAction { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

