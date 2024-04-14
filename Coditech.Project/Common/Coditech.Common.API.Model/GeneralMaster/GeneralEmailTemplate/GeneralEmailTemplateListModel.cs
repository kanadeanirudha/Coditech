namespace Coditech.Common.API.Model
{
    public class GeneralEmailTemplateListModel : BaseListModel
    {
        public List<GeneralEmailTemplateModel> GeneralEmailTemplateList { get; set; }
        public GeneralEmailTemplateListModel()
        {
            GeneralEmailTemplateList = new List<GeneralEmailTemplateModel>();
        }
    }
}
