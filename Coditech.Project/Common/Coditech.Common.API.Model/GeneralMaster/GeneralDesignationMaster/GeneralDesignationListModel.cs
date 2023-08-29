namespace Coditech.Common.API.Model
{
    public class GeneralDesignationListModel : BaseListModel
    {
        public List<GeneralDesignationMasterModel> GeneralDesignationList { get; set; }
        public GeneralDesignationListModel()
        {
            GeneralDesignationList = new List<GeneralDesignationMasterModel>();
        }
    }
}

