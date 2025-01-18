namespace Coditech.Common.API.Model
{
    public partial class AccSetupTransactionTypeListModel : BaseListModel
    {
        public List<AccSetupTransactionTypeModel> AccSetupTransactionTypeList { get; set; }
        public AccSetupTransactionTypeListModel()
        {
            AccSetupTransactionTypeList = new List<AccSetupTransactionTypeModel>();
        }

    }
}
