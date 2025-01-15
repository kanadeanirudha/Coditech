using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAccSetupBalanceSheetClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Designation.
        /// </summary>
        /// <returns>AccSetupBalanceSheetListResponse</returns>
        AccSetupBalanceSheetListResponse List(string selectedCentreCode, byte accSetupBalanceSheetTypeId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="AccSetupBalanceSheetModel">AccSetupBalanceSheetModel.</param>
        /// <returns>Returns AccSetupBalanceSheetResponse.</returns>
        AccSetupBalanceSheetResponse CreateBalanceSheet(AccSetupBalanceSheetModel body);

        /// <summary>
        /// Get Designation by designationId.
        /// </summary>
        /// <param name="balanceSheetId">designationId</param>
        /// <returns>Returns AccSetupBalanceSheetResponse.</returns>
        AccSetupBalanceSheetResponse GetBalanceSheet(int balanceSheetId);

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="AccSetupBalanceSheetModel">AccSetupBalanceSheetModel.</param>
        /// <returns>Returns updated AccSetupBalanceSheetResponse</returns>
        AccSetupBalanceSheetResponse UpdateBalanceSheet(AccSetupBalanceSheetModel body);

        /// <summary>
        /// Delete Designation.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteBalanceSheet(ParameterModel body);
    }
}
