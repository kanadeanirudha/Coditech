namespace Coditech.Common.API.Model
{
    public class GeneralTemplateListModel : BaseListModel
    {
        public List<GeneralTemplateModel> GeneralTemplateList { get; set; }
        public GeneralTemplateListModel()
        {
            GeneralTemplateList = new List<GeneralTemplateModel>();
        }
    }
}
