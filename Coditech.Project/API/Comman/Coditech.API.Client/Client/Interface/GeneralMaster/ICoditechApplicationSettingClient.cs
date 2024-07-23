using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ICoditechApplicationSettingClient : IBaseClient
    {
        /// <summary>
        /// Get list of Coditech Application Setting.
        /// </summary>
        /// <returns>CoditechApplicationSettingListResponse</returns>
        CoditechApplicationSettingListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Coditech Application Setting.
        /// </summary>
        /// <param name="CoditechApplicationSettingModel">CoditechApplicationSettingModel.</param>
        /// <returns>Returns CoditechApplicationSettingResponse.</returns>
        CoditechApplicationSettingResponse CreateCoditechApplicationSetting(CoditechApplicationSettingModel body);

        /// <summary>
        /// Get Coditech Application Setting by coditechApplicationSettingId.
        /// </summary>
        /// <param name="coditechApplicationSettingId">coditechApplicationSettingId</param>
        /// <returns>Returns CoditechApplicationSettingResponse.</returns>
        CoditechApplicationSettingResponse GetCoditechApplicationSetting(short coditechApplicationSettingId);

        /// <summary>
        /// Update Coditech Application Setting.
        /// </summary>
        /// <param name="CoditechApplicationSettingModel">CoditechApplicationSettingModel.</param>
        /// <returns>Returns updated CoditechApplicationSettingResponse</returns>
        CoditechApplicationSettingResponse UpdateCoditechApplicationSetting(CoditechApplicationSettingModel body);

        /// <summary>
        /// Delete Coditech Application Setting.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteCoditechApplicationSetting(ParameterModel body);
    }
}
