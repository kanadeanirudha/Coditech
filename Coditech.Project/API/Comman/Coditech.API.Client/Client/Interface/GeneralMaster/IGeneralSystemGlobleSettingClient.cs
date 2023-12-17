using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralSystemGlobleSettingClient : IBaseClient
    {
        /// <summary>
        /// Get list of General System Globle Setting.
        /// </summary>
        /// <returns>GeneralSystemGlobleSettingListResponse</returns>
        GeneralSystemGlobleSettingListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create General System Globle Setting.
        /// </summary>
        /// <param name="GeneralSystemGlobleSettingModel">GeneralSystemGlobleSettingModel.</param>
        /// <returns>Returns GeneralSystemGlobleSettingResponse.</returns>
        GeneralSystemGlobleSettingResponse CreateSystemGlobleSetting(GeneralSystemGlobleSettingModel body);

        /// <summary>
        /// Get General System Globle Setting by generalSystemGlobleSettingId.
        /// </summary>
        /// <param name="generalSystemGlobleSettingId">generalSystemGlobleSettingId</param>
        /// <returns>Returns GeneralSystemGlobleSettingResponse.</returns>
        GeneralSystemGlobleSettingResponse GetSystemGlobleSetting(short generalSystemGlobleSettingId);

        /// <summary>
        /// Update System Globle Setting.
        /// </summary>
        /// <param name="GeneralSystemGlobleSettingModel">GeneralSystemGlobleSettingModel.</param>
        /// <returns>Returns updated GeneralSystemGlobleSettingResponse</returns>
        GeneralSystemGlobleSettingResponse UpdateSystemGlobleSetting(GeneralSystemGlobleSettingModel body);

        /// <summary>
        /// Delete System Globle Setting.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteSystemGlobleSetting(ParameterModel body);
    }
}
