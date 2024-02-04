namespace Coditech.Common.API.Model
{
    public class GeneralLeadGenerationListModel : BaseListModel
    {
        public List<GeneralLeadGenerationModel> GeneralLeadGenerationList { get; set; }
        public GeneralLeadGenerationListModel()
        {
            GeneralLeadGenerationList = new List<GeneralLeadGenerationModel>();
        }

    }
}
