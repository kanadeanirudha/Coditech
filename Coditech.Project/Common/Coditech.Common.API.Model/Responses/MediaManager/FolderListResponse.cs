using Coditech.Common.API.Model.Response;

namespace Coditech.Common.API.Model.Responses
{
    public class FolderListResponse : BaseListResponse
    {
        public FolderListModel FolderList { get; set; } = new FolderListModel();
    }
}
