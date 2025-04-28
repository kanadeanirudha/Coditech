namespace Coditech.Common.API.Model
{
    public partial class ACCGLOpeningBalanceListModel : BaseListModel
    {
        public List<ACCGLOpeningBalanceModel> ACCGLOpeningBalanceList { get; set; }
        public ACCGLOpeningBalanceListModel()
        {
            ACCGLOpeningBalanceList = new List<ACCGLOpeningBalanceModel>();
        }
    }
}

