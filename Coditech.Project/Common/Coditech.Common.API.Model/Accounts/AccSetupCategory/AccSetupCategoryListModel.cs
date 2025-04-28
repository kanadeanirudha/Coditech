namespace Coditech.Common.API.Model
{
    public partial class AccSetupCategoryListModel : BaseListModel
    {
        public List<AccSetupCategoryModel> AccSetupCategoryList { get; set; }
        public AccSetupCategoryListModel()
        {
            AccSetupCategoryList = new List<AccSetupCategoryModel>();
        }
    }
}

