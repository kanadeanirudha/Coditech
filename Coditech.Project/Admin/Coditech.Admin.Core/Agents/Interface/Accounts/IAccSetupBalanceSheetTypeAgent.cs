using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;
namespace Coditech.Admin.Agents
{
    public interface IAccSetupBalanceSheetTypeAgent
    {
        /// <summary>
        /// Get list of AccSetupBalanceSheetType
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>PaymentGatewaysListViewModel</returns>
        AccSetupBalanceSheetTypeListViewModel GetAccSetupBalanceSheetTypeList(DataTableViewModel dataTableModel);
        AccSetupBalanceSheetTypeListResponse GetAccSetupBalanceSheetTypeList();
    }
}
