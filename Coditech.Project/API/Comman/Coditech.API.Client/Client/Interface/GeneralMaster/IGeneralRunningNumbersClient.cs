using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralRunningNumbersClient : IBaseClient
    {
        /// <summary>
        /// Get list of GeneralRunningNumbers.
        /// </summary>
        /// <returns>GeneralRunningNumbersListResponse</returns>
        GeneralRunningNumbersListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create GeneralRunningNumbers.
        /// </summary>
        /// <param name="GeneralRunningNumbersModel">GeneralRunningNumbersModel.</param>
        /// <returns>Returns GeneralRunningNumbersResponse.</returns>
        GeneralRunningNumbersResponse CreateRunningNumbers(GeneralRunningNumbersModel body);

        /// <summary>
        /// Get General Running Numbers by generalRunningNumberId.
        /// </summary>
        /// <param name="generalRunningNumberId">generalRunningNumberId</param>
        /// <returns>Returns GeneralRunningNumbersResponse.</returns>
        GeneralRunningNumbersResponse GetRunningNumbers(long generalRunningNumberId);

        /// <summary>
        /// Update GeneralRunningNumbers.
        /// </summary>
        /// <param name="GeneralRunningNumbersModel">GeneralRunningNumbersModel.</param>
        /// <returns>Returns updated GeneralRunningNumbersResponse</returns>
        GeneralRunningNumbersResponse UpdateRunningNumbers(GeneralRunningNumbersModel body);

        /// <summary>
        /// Delete GeneralRunningNumbers.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteRunningNumbers(ParameterModel body);
    }
}
