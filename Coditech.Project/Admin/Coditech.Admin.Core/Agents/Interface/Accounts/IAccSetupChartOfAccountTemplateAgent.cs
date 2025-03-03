using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
namespace Coditech.Admin.Agents
{
    public interface IAccSetupChartOfAccountTemplateAgent
    {
        /// <summary>
        /// Get list of AccSetupChartOfAccountTemplate
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>PaymentGatewaysListViewModel</returns>
        AccSetupChartOfAccountTemplateListViewModel GetAccSetupChartOfAccountTemplateList(DataTableViewModel dataTableModel);
        AccSetupChartOfAccountTemplateListResponse GetAccSetupChartOfAccountTemplateList();
    }
}
