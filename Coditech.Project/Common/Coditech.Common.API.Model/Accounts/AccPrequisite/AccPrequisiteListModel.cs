namespace Coditech.Common.API.Model
{
    public partial class AccPrequisiteListModel : BaseListModel
    {
        public List<AccPrequisiteModel> AccPrequisiteList { get; set; }
        public AccPrequisiteListModel()
        {
            AccPrequisiteList = new List<AccPrequisiteModel>();
        }
    }
}

