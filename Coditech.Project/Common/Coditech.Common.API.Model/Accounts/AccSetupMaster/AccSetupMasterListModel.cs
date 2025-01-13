namespace Coditech.Common.API.Model
{
    public class AccSetupMasterListModel : BaseListModel
    {
        public List<AccSetupMasterModel> AccSetupMasterList { get; set; }
        public AccSetupMasterListModel()
        {
            AccSetupMasterList = new List<AccSetupMasterModel>();
        }
              
    }
}
