﻿using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface ITaskApprovalSettingClient : IBaseClient
    {
        /// <summary>
        /// Get list of Task Approval Setting.
        /// </summary>
        /// <returns>TaskApprovalSettingListResponse</returns>
        TaskApprovalSettingListResponse List(string SelectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get TaskApprovalSetting by taskApprovalSettingId.
        /// </summary>
        /// <param name="taskMasterId">taskMasterId</param>
        /// <param name="centreCode">centreCode</param>
        /// <returns>Returns TaskApprovalSettingResponse.</returns>
        
        TaskApprovalSettingResponse GetTaskApprovalSetting(short taskMasterId, string centreCode);
        /// <summary>
        /// Create TaskApprovalSettingModel.
        /// </summary>
        /// <param name="TaskApprovalSettingModel">TaskApprovalSettingModel.</param>
        /// <returns>Returns TaskApprovalSettingResponse.</returns>
        TaskApprovalSettingResponse AddUpdateTaskApprovalSetting(TaskApprovalSettingModel body);

        /// <summary>
        /// Get TaskApprovalSetting by taskApprovalSettingId.
        /// </summary>
        /// <param name="taskMasterId">taskMasterId</param>
        /// <param name="centreCode">centreCode</param>
        /// <param name="taskApprovalSettingId">taskApprovalSettingId</param>
        /// <returns>Returns TaskApprovalSettingResponse.</returns>
        TaskApprovalSettingResponse GetUpdateTaskApprovalSetting(short taskMasterId, string centreCode, int taskApprovalSettingId);

        /// <summary>
        /// Update TaskApprovalSetting.
        /// </summary>
        /// <param name="TaskApprovalSettingModel">TaskApprovalSettingModel.</param>
        /// <returns>Returns updated TaskApprovalSettingResponse</returns>
        TaskApprovalSettingResponse UpdateTaskApprovalSetting(TaskApprovalSettingModel model);

        /// <summary>
        /// Delete TaskApprovalSetting.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteTaskApprovalSetting(ParameterModel body);
        
    }
}
