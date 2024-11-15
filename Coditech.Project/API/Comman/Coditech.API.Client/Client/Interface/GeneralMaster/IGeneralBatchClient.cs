using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralBatchClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Batch.
        /// </summary>
        /// <returns>GeneralBatchListResponse</returns>
        GeneralBatchListResponse List(string SelectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create GeneralBatch.
        /// </summary>
        /// <param name="GeneralBatchModel">GeneralBatchModel.</param>
        /// <returns>Returns GeneralBatchResponse.</returns>
        GeneralBatchResponse CreateGeneralBatch(GeneralBatchModel body);

        /// <summary>
        /// Get GeneralBatch by generalBatchMasterId.
        /// </summary>
        /// <param name="generalBatchMasterId">generalBatchMasterId</param>
        /// <returns>Returns GeneralBatchResponse.</returns>
        GeneralBatchResponse GetGeneralBatch(int generalBatchMasterId);

        /// <summary>
        /// Update GeneralBatch.
        /// </summary>
        /// <param name="GeneralBatchModel">GeneralBatchModel.</param>
        /// <returns>Returns updated GeneralBatchResponse</returns>
        GeneralBatchResponse UpdateGeneralBatch(GeneralBatchModel body);

        /// <summary>
        /// Delete GeneralBatch.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteGeneralBatch(ParameterModel body);

        /// <summary>
        /// Get list of GeneralBatchUser.
        /// </summary>
        /// <returns>GeneralBatchUserListResponse</returns>
        GeneralBatchUserListResponse GetGeneralBatchUserList(int generalBatchMasterId, string userType,IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Update Associate UnAssociate Batchwise User.
        /// </summary>
        /// <param name="GeneralBatchUserModel">GeneralBatchUserModel.</param>
        /// <returns>Returns updated GeneralBatchUserResponse</returns>
        GeneralBatchUserResponse AssociateUnAssociateBatchwiseUser(GeneralBatchUserModel body);
    }
}
