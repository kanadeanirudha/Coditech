namespace Coditech.Common.API.Model
{
    public class AccGLSetupNarrationListModel : BaseListModel
    {
        public List<AccGLSetupNarrationModel> AccGLSetupNarrationList { get; set; }
        public AccGLSetupNarrationListModel()
        {
            AccGLSetupNarrationList = new List<AccGLSetupNarrationModel>();
        }

    }
}
