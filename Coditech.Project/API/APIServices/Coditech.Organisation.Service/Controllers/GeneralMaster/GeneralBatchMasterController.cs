using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers

{
    public class GeneralBatchMasterController : BaseController
    {
        private readonly IGeneralBatchMasterService _generalBatchMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralBatchMasterController(ICoditechLogging coditechLogging, IGeneralBatchMasterService generalBatchMasterService)
        {
            _generalBatchMasterService = generalBatchMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralBatchMaster/GetBatchList")]
        [Produces(typeof(GeneralBatchListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetBatchList(string selectedCentreCode, long userId, ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralBatchListModel list = _generalBatchMasterService.GetBatchList(selectedCentreCode, userId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralBatchListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralBatchMaster/CreateGeneralBatch")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralBatchResponse))]
        public virtual IActionResult CreateGeneralBatch([FromBody] GeneralBatchModel model)
        {
            try
            {
                GeneralBatchModel batch = _generalBatchMasterService.CreateGeneralBatch(model);
                return IsNotNull(batch) ? CreateCreatedResponse(new GeneralBatchResponse { GeneralBatchModel = batch }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralBatchResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralBatchMaster/GetGeneralBatch")]
        [HttpGet]
        [Produces(typeof(GeneralBatchResponse))]
        public virtual IActionResult GetGeneralBatch(int generalBatchMasterId)
        {
            try
            {
                GeneralBatchModel generalBatchModel = _generalBatchMasterService.GetGeneralBatch(generalBatchMasterId);
                return IsNotNull(generalBatchModel) ? CreateOKResponse(new GeneralBatchResponse { GeneralBatchModel = generalBatchModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralBatchResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralBatchMaster/UpdateGeneralBatch")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralBatchResponse))]
        public virtual IActionResult UpdateGeneralBatch([FromBody] GeneralBatchModel model)
        {
            try
            {
                bool isUpdated = _generalBatchMasterService.UpdateGeneralBatch(model);
                return isUpdated ? CreateOKResponse(new GeneralBatchResponse { GeneralBatchModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralBatchResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralBatchMaster/DeleteGeneralBatch")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteGeneralBatch([FromBody] ParameterModel generalBatchMasterIds)
        {
            try
            {
                bool deleted = _generalBatchMasterService.DeleteGeneralBatch(generalBatchMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatch.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [HttpGet]
        [Route("/GeneralBatchMaster/GetGeneralBatchUserList")]
        [Produces(typeof(GeneralBatchUserListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetGeneralBatchUserList(int generalBatchMasterId, string userType, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralBatchUserListModel list = _generalBatchMasterService.GetGeneralBatchUserList(generalBatchMasterId, userType,filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralBatchUserListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatchUser.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchUserListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralBatchUser.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchUserListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralBatchMaster/AssociateUnAssociateBatchwiseUser")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralBatchUserResponse))]
        public virtual IActionResult AssociateUnAssociateBatchwiseUser([FromBody] GeneralBatchUserModel model)
        {
            try
            {
                bool isUpdated = _generalBatchMasterService.AssociateUnAssociateBatchwiseUser(model);
                return isUpdated ? CreateOKResponse(new GeneralBatchUserResponse { GeneralBatchUserModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociateUnAssociateBatchwiseUser.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralBatchUserResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AssociateUnAssociateBatchwiseUser.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralBatchUserResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}