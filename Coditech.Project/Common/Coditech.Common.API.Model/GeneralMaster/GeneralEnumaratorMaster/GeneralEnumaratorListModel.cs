namespace Coditech.Common.API.Model
{
    public class GeneralEnumaratorListModel : BaseListModel
    {
        public List<GeneralEnumaratorModel> GeneralEnumaratorList { get; set; }
        public GeneralEnumaratorListModel()
        {
            GeneralEnumaratorList = new List<GeneralEnumaratorModel>();
        }

    }
}
