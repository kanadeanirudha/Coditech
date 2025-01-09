using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IAccSetupMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of General AccSetupMaster.
        /// </summary>
        /// <returns>AccSetupMasterListResponse</returns>
        AccSetupMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create AccSetupMaster.
        /// </summary>
        /// <param name="AccSetupMasterModel">AccSetupMasterModel.</param>
        /// <returns>Returns AccSetupMasterResponse.</returns>
        AccSetupMasterResponse CreateAccSetupMaster(AccSetupMasterModel body);

        /// <summary>
        /// Get AccSetupMaster by accSetupMasterId.
        /// </summary>
        /// <param name="accSetupMasterId">accSetupMasterId</param>
        /// <returns>Returns AccSetupMasterResponse.</returns>
        AccSetupMasterResponse GetAccSetupMaster(short accSetupMasterId);

        /// <summary>
        /// Update AccSetupMaster.
        /// </summary>
        /// <param name="AccSetupMasterModel">AccSetupMasterModel.</param>
        /// <returns>Returns updated AccSetupMasterResponse</returns>
        AccSetupMasterResponse UpdateAccSetupMaster(AccSetupMasterModel body);

        /// <summary>
        /// Delete AccSetupMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteAccSetupMaster(ParameterModel body);
    }
}
