namespace Coditech.Common.API.Model
{
    public class GeneralDesignationListModel : BaseListModel
    {
        public List<GeneralDesignationModel> GeneralDesignationList { get; set; }
        public GeneralDesignationListModel()
        {
            GeneralDesignationList = new List<GeneralDesignationModel>();
        }
    }
}
