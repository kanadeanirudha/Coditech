namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLBankListModel : BaseListModel
    {
        public List<AccSetupGLBankModel> AccSetupGLBankList { get; set; }
        public AccSetupGLBankListModel()
        {
            AccSetupGLBankList = new List<AccSetupGLBankModel>();
        }
    }
}
