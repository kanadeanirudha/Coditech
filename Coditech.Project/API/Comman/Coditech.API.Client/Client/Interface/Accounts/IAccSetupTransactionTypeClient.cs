using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Client
{
    public interface IAccSetupTransactionTypeClient : IBaseClient
    {
        /// <summary>
        /// Get list of AccSetupTransactionType.
        /// </summary>
        /// <returns>AccSetupTransactionTypeListResponse</returns>
        AccSetupTransactionTypeListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create TransactionType.
        /// </summary>
        /// <param name="AccSetupTransactionTypeModel">AccSetupTransactionTypeModel.</param>
        /// <returns>Returns AccSetupTransactionTypeResponse.</returns>
        AccSetupTransactionTypeResponse CreateTransactionType(AccSetupTransactionTypeModel body);

        /// <summary>
        /// Get TransactionType by accSetupTransactionTypeId.
        /// </summary>
        /// <param name="accSetupTransactionTypeId">accSetupTransactionTypeId</param>
        /// <returns>Returns AccSetupTransactionTypeResponse.</returns>
        AccSetupTransactionTypeResponse GetTransactionType(short accSetupTransactionTypeId);

        /// <summary>
        /// Update AccSetupTransactionType.
        /// </summary>
        /// <param name="AccSetupTransactionTypeModel">AccSetupTransactionTypeModel.</param>
        /// <returns>Returns updated AccSetupTransactionTypeResponse</returns>
        AccSetupTransactionTypeResponse UpdateTransactionType(AccSetupTransactionTypeModel body);

        /// <summary>
        /// Delete AccSetupTransactionType.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTransactionType(ParameterModel body);
    }
}
