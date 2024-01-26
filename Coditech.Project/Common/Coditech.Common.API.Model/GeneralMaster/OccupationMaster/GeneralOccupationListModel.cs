namespace Coditech.Common.API.Model
{
    public class GeneralOccupationListModel : BaseListModel
    {
        public List<GeneralOccupationModel> GeneralOccupationList { get; set; }
        public GeneralOccupationListModel()
        {
            GeneralOccupationList = new List<GeneralOccupationModel>();
        }

    }
}
