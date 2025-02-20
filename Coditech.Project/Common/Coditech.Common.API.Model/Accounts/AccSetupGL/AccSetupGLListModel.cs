namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLListModel : BaseListModel
    {
        public List<AccSetupGLModel> AccSetupGLList { get; set; }
        public AccSetupGLListModel()
        {
            AccSetupGLList = new List<AccSetupGLModel>();
        }
    }
}
