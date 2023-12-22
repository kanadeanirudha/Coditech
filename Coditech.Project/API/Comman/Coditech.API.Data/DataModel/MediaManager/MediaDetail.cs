using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class MediaDetail
    {
        [Key]
        public long MediaId { get; set; }
        public byte MediaConfigurationId { get; set; }
        public int MediaFolderMasterId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
