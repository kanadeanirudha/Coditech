using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class AdminRoleMediaFolders
    {
        [Key]
        public int AdminRoleMediaFolderId { get; set; }
        public int AdminRoleMasterId { get; set; }
        public int MediaFolderMasterId { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

