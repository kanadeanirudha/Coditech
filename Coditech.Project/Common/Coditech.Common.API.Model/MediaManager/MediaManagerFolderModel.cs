namespace Coditech.Common.API.Model
{
    public class MediaManagerFolderModel : BaseModel
    {
        public MediaFolderStructure MediaRootFolder { get; set; }
        public int ActiveFolderId { get; set; }
        public List<Media> MediaFiles { get; set; }
    }

    public class MediaFolderStructure
    {
        public int RootFolderId { get; set; }
        public string RootFolderName { get; set; }
        public bool IsActiveFolder { get; set; }
        public List<MediaFolderStructure> SubFolders { get; set; }

        public MediaFolderStructure()
        {
            SubFolders = new List<MediaFolderStructure>();
        }
    }

    public class Media
    {
        public long MediaId { get; set; }
        public string MediaPath { get; set; }
        public string MediaName { get; set; }
    }
}
