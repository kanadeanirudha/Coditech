namespace Coditech.Common.API.Model
{
    public class AdminSnPostsListModel : BaseListModel
    {
        public List<AdminSnPostsModel> AdminSnPostsList { get; set; }
        public AdminSnPostsListModel()
        {
            AdminSnPostsList = new List<AdminSnPostsModel>();
        }
    }
}
