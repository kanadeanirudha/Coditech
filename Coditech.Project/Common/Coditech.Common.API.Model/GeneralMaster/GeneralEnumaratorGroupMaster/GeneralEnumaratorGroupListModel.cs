namespace Coditech.Common.API.Model
{
    public class GeneralEnumaratorGroupListModel : BaseListModel
    {
        public List<GeneralEnumaratorGroupModel> GeneralEnumaratorGroupList { get; set; }
        public GeneralEnumaratorGroupListModel()
        {
            GeneralEnumaratorGroupList = new List<GeneralEnumaratorGroupModel>();
        }

    }
}
