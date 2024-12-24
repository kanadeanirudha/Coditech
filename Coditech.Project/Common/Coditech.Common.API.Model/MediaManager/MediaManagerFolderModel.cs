namespace Coditech.Common.API.Model
{
    public class MediaManagerFolderModel : BaseListModel
    {
        public MediaFolderStructureModel MediaRootFolder { get; set; }
        public int ActiveFolderId { get; set; }
        public List<MediaModel> MediaFiles { get; set; }
        public double TotalFileSize { get; set; }
        public int TotalCount { get; set; }

    }

    public class MediaFolderStructureModel
    {
        public int RootFolderId { get; set; }
        public string RootFolderName { get; set; }
        public bool IsActiveFolder { get; set; }
        public List<MediaFolderStructureModel> SubFolders { get; set; }
        public List<int> adminRoleMediaFolders { get; set; }

        public MediaFolderStructureModel()
        {
            SubFolders = new List<MediaFolderStructureModel>();
        }
    }

}
