namespace Coditech.Common.API.Model
{
    public class AdminSanctionPostListModel : BaseListModel
    {
        public List<AdminSanctionPostModel> AdminSanctionPostList { get; set; }
        public AdminSanctionPostListModel()
        {
            AdminSanctionPostList = new List<AdminSanctionPostModel>();
        }
    }
}
