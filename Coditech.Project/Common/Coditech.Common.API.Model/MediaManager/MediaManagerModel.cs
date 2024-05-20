using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class MediaManagerModel : BaseModel
    {
        public long MediaId { get; set; }
        public int? MediaConfigurationId { get; set; }
        public string Path { get; set; }
        public string Size { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string MediaType { get; set; }
        public int MediaPathId { get; set; }
        [Required]
        public string FileName { get; set; }
        public string ShortDescription { get; set; }
        public string Folder { get; set; }
        public bool IsImage { get; set; }
        public string MediaServerPath { get; set; }
        public string MediaServerThumbnailPath { get; set; }
        public string DisplayName { get; set; }

        public string OldMediaPath { get; set; }
    }
}
