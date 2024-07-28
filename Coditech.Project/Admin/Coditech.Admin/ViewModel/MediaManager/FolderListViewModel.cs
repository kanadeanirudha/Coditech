using Coditech.Common.API.Model;
using Coditech.Common.Helper;

namespace Coditech.Admin.ViewModel
{
    public class FolderListViewModel : BaseViewModel
    {
        public List<Folder> Folders { get; set; } = new List<Folder>();
    }
}
