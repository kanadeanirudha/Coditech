﻿namespace Coditech.Common.API.Model
{
    public class MediaManagerFolderModel : BaseListModel
    {
        public MediaFolderStructure MediaRootFolder { get; set; }
        public int ActiveFolderId { get; set; }
        public List<Media> MediaFiles { get; set; }
        public double TotalFileSize { get; set; }
        public int TotalCount { get; set; }

    }

    public class MediaFolderStructure
    {
        public int RootFolderId { get; set; }
        public string RootFolderName { get; set; }
        public bool IsActiveFolder { get; set; }
        public List<MediaFolderStructure> SubFolders { get; set; }
        public List<int> adminRoleMediaFolders { get; set; }

        public MediaFolderStructure()
        {
            SubFolders = new List<MediaFolderStructure>();
        }
    }

    public class Media : BaseModel
    {
        public long MediaId { get; set; }
        public string MediaPath { get; set; }
        public string MediaName { get; set; }
        public long MediaSize { get; set; }
        public int ActiveFolderId { get; set; }
        public string ContentType { get; set; }
        public string DownloadPath { get; set; }
    }
}
