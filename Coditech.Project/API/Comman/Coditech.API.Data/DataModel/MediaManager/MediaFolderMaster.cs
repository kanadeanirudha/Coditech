using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaFolderMaster
    {
        [Key]
        public int MediaFolderMasterId { get; set; }
        public int MediaFolderParentId { get; set; }
        public string FolderName { get; set; }
        public bool IsActive { get; set; } = true;
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
