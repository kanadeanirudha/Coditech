namespace Coditech.Common.API.Model
{
    public class UserTypeListModel : BaseListModel
    {
        public List<UserTypeModel> TypeList { get; set; }
        public UserTypeListModel()
        {
            TypeList = new List<UserTypeModel>();
        }

    }
}
