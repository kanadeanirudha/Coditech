using Microsoft.AspNetCore.Http;

namespace Coditech.Common.API.Model
{
    public partial class MediaModel : BaseModel
    {
        public long MediaId { get; set; }
        public byte MediaConfigurationId { get; set; }
        public int MediaFolderMasterId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public string Size { get; set; }
        public int ActiveFolderId { get; set; }
        public string Type { get; set; }
        public string DownloadPath { get; set; }
        public IFormFile MediaFile { get; set; }
    }
}
