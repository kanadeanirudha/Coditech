namespace Coditech.Common.API.Model
{
    public partial class AccSetupChartOfAccountTemplateListModel : BaseListModel
    {
        public List<AccSetupChartOfAccountTemplateModel> AccSetupChartOfAccountTemplateList { get; set; }
        public AccSetupChartOfAccountTemplateListModel()
        {
            AccSetupChartOfAccountTemplateList = new List<AccSetupChartOfAccountTemplateModel>();
        }

    }
}
