using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class MediaManagerFolderListViewModel : BaseViewModel
    {
        public MediaFolderStructureModel MediaRootFolder { get; set; }
        public int ActiveFolderId { get; set; }
        public List<MediaModel> MediaFiles { get; set; }
        public double TotalFileSize {  get; set; }
        public string SelectedParameter1 { get; set; }
    }
}
