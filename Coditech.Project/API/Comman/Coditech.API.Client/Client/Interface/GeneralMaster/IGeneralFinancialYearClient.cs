using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralFinancialYearClient : IBaseClient
    {
        /// <summary>
        /// Get list of General FinancialYear.
        /// </summary>
        /// <returns>GeneralFinancialYearListResponse</returns>
        GeneralFinancialYearListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create FinancialYear.
        /// </summary>
        /// <param name="GeneralFinancialYearModel">GeneralFinancialYearModel.</param>
        /// <returns>Returns GeneralFinancialYearResponse.</returns>
        GeneralFinancialYearResponse CreateFinancialYear(GeneralFinancialYearModel body);

        /// <summary>
        /// Get FinancialYear by generalFinancialYearId.
        /// </summary>
        /// <param name="generalFinancialYearId">generalFinancialYearId</param>
        /// <returns>Returns GeneralFinancialYearResponse.</returns>
        GeneralFinancialYearResponse GetFinancialYear(short generalFinancialYearId);

        /// <summary>
        /// Update FinancialYear.
        /// </summary>
        /// <param name="GeneralFinancialYearModel">GeneralFinancialYearModel.</param>
        /// <returns>Returns updated GeneralFinancialYearResponse</returns>
        GeneralFinancialYearResponse UpdateFinancialYear(GeneralFinancialYearModel body);

        /// <summary>
        /// Delete FinancialYear.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteFinancialYear(ParameterModel body);
    }
}
