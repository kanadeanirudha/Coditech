﻿namespace Coditech.Common.API.Model
{
    public class MediaModel : BaseModel
    {
        public long MediaId { get; set; }
        public byte MediaConfigurationId { get; set; }
        public int MediaFolderMasterId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
        public int ActiveFolderId { get; set; }
        public string Type { get; set; }
        public string DownloadPath { get; set; }
    }
}
