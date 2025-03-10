namespace Coditech.Common.API.Model
{
    public partial class AccSetupGLTypeListModel : BaseListModel
    {
        public List<AccSetupGLTypeModel> AccSetupGLTypeList { get; set; }
        public AccSetupGLTypeListModel()
        {
            AccSetupGLTypeList = new List<AccSetupGLTypeModel>();
        }
    }
}
