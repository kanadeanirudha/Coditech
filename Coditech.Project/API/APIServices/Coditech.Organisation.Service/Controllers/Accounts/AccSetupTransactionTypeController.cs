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
    public class AccSetupTransactionTypeController : BaseController
    {
        private readonly IAccSetupTransactionTypeService _accSetupTransactionTypeService;
        protected readonly ICoditechLogging _coditechLogging;
        public AccSetupTransactionTypeController(ICoditechLogging coditechLogging, IAccSetupTransactionTypeService accSetupTransactionTypeService)
        {
            _accSetupTransactionTypeService = accSetupTransactionTypeService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/AccSetupTransactionType/GetTransactionTypeList")]
        [Produces(typeof(AccSetupTransactionTypeListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTransactionTypeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                AccSetupTransactionTypeListModel list = _accSetupTransactionTypeService.GetTransactionTypeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<AccSetupTransactionTypeListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupTransactionType/CreateTransactionType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(AccSetupTransactionTypeResponse))]
        public virtual IActionResult CreateTransactionType([FromBody] AccSetupTransactionTypeModel model)
        {
            try
            {
                AccSetupTransactionTypeModel AccSetupTransactionType = _accSetupTransactionTypeService.CreateTransactionType(model);
                return IsNotNull(AccSetupTransactionType) ? CreateCreatedResponse(new AccSetupTransactionTypeResponse { AccSetupTransactionTypeModel = AccSetupTransactionType }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupTransactionType/GetTransactionType")]
        [HttpGet]
        [Produces(typeof(AccSetupTransactionTypeResponse))]
        public virtual IActionResult GetTransactionType(short accSetupTransactionTypeId)
        {
            try
            {
                AccSetupTransactionTypeModel accSetupTransactionTypeModel = _accSetupTransactionTypeService.GetTransactionType(accSetupTransactionTypeId);
                return IsNotNull(accSetupTransactionTypeModel) ? CreateOKResponse(new AccSetupTransactionTypeResponse { AccSetupTransactionTypeModel = accSetupTransactionTypeModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupTransactionType/UpdateTransactionType")]
        [HttpPut, ValidateModel]
        [Produces(typeof(AccSetupTransactionTypeResponse))]
        public virtual IActionResult UpdateTransactionType([FromBody] AccSetupTransactionTypeModel model)
        {
            try
            {
                bool isUpdated = _accSetupTransactionTypeService.UpdateTransactionType(model);
                return isUpdated ? CreateOKResponse(new AccSetupTransactionTypeResponse { AccSetupTransactionTypeModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new AccSetupTransactionTypeResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/AccSetupTransactionType/DeleteTransactionType")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteFinancialYear([FromBody] ParameterModel AccSetupTransactionTypeIds)
        {
            try
            {
                bool deleted = _accSetupTransactionTypeService.DeleteTransactionType(AccSetupTransactionTypeIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.AccSetupTransactionType.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}