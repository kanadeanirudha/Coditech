namespace Coditech.Common.API.Model
{
    public partial class AccGLTransactionDetailsListModel : BaseListModel
    {
        public List<AccGLTransactionDetailsModel> AccGLTransactionDetailsList { get; set; }
        public AccGLTransactionDetailsListModel()
        {
            AccGLTransactionDetailsList = new List<AccGLTransactionDetailsModel>();
        }
    }
}

