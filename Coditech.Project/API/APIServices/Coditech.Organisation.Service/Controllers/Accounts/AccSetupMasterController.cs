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
    public class AccSetupMasterController : BaseController
    {
        private readonly IAccSetupMasterService _accSetupMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        
       
        public AccSetupMasterController(ICoditechLogging coditechLogging, IAccSetupMasterService accSetupMasterService)
        {
            _accSetupMasterService = accSetupMasterService;
            _coditechLogging = coditechLogging;
        }
        [HttpGet]
        [Route("/AccSetupMaster/GetAccSetupMasterList")]
        [Produces(typeof(AccSetupMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetAccSetupMasterList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
               AccSetupMasterListModel list = _accSetupMasterService.GetAccSetupMasterList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupMaster/CreateAccSetupMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccSetupMasterResponse))]
        public virtual IActionResult CreateAccSetupMaster([FromBody] AccSetupMasterModel model)
        {
            try
            {
                AccSetupMasterModel AccSetupMaster = _accSetupMasterService.CreateAccSetupMaster(model);
                return IsNotNull(AccSetupMaster) ? CreateCreatedResponse(new AccSetupMasterResponse { AccSetupMasterModel = AccSetupMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupMaster/GetAccSetupMaster")]
        [HttpGet]
        [Produces(typeof(AccSetupMasterResponse))]
        public virtual IActionResult GetAccSetupMaster(short accSetupMasterId)
        {
            try
            {
                AccSetupMasterModel accSetupMasterModel = _accSetupMasterService.GetAccSetupMaster(accSetupMasterId);
                return IsNotNull(accSetupMasterModel) ? CreateOKResponse(new AccSetupMasterResponse { AccSetupMasterModel = accSetupMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupMaster/UpdateAccSetupMaster")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccSetupMasterResponse))]
        public virtual IActionResult UpdateAccSetupMaster([FromBody] AccSetupMasterModel model)
        {
            try
            {
                bool isUpdated = _accSetupMasterService.UpdateAccSetupMaster(model);
                return isUpdated ? CreateOKResponse(new AccSetupMasterResponse { AccSetupMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupMaster/DeleteAccSetupMaster")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteAccSetupMaster([FromBody] ParameterModel AccSetupMasterIds)
        {
            try
            {
                bool deleted = _accSetupMasterService.DeleteAccSetupMaster(AccSetupMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}