﻿using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IMediaSettingMasterClient : IBaseClient
    {
        /// <summary>
        /// Get list of Media Setting Master.
        /// </summary>
        /// <returns>MediaSettingMasterListResponse</returns>
        MediaSettingMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get MediaSettingMaster by mediaTypeMasterId.
        /// </summary>
        /// <param name="mediaTypeMasterId">mediaTypeMasterId</param>
        /// <returns>Returns MediaSettingMasterResponse.</returns>
        MediaSettingMasterResponse GetMediaSettingMaster(byte mediaTypeMasterId);

        /// <summary>
        /// Update MediaSettingMaster.
        /// </summary>
        /// <param name="MediaSettingMasterModel">MediaSettingMasterModel.</param>
        /// <returns>Returns updated MediaSettingMasterResponse</returns>
        MediaSettingMasterResponse UpdateMediaSettingMaster(MediaSettingMasterModel body);

        /// <summary>
        /// Delete MediaSettingMaster.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteMediaSettingMaster(ParameterModel body);
    }
}
