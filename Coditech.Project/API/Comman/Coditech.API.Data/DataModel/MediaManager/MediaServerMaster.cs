using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaServerMaster
    {
        [Key]
        public byte MediaServerMasterId { get; set; }
        public string ServerName { get; set; }
        public string PartialViewName { get; set; }
        public string ClassName { get; set; }
        public bool IsOtherServer { get; set; } = true;
        public string ThumbnailFolderName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
