namespace Coditech.Common.API.Model
{
    public partial class AccGLTransactionListModel : BaseListModel
    {
        public List<AccGLTransactionModel> AccGLTransactionList { get; set; }
        public AccGLTransactionListModel()
        {
            AccGLTransactionList = new List<AccGLTransactionModel>();
        }
    }
}

