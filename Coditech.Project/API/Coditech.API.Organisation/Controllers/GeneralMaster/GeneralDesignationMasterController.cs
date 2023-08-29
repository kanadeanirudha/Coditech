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
    public class GeneralDesignationMasterController : BaseController
    {
        private readonly IGeneralDesignationMasterService _generalDesignationMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralDesignationMasterController(ICoditechLogging coditechLogging, IGeneralDesignationMasterService generalDesignationMasterService)
        {
            _generalDesignationMasterService = generalDesignationMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralDesignationMaster/GetDesignationList")]
        [Produces(typeof(GeneralDesignationListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetDesignationList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralDesignationListModel list = _generalDesignationMasterService.GetDesignationList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralDesignationListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDesignationListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDesignationListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDesignationMaster/CreateDesignation")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralDesignationResponse))]
        public IActionResult CreateDesignation([FromBody] GeneralDesignationModel model)
        {
            try
            {
                GeneralDesignationModel designation = _generalDesignationMasterService.CreateDesignation(model);
                return IsNotNull(designation) ? CreateCreatedResponse(new GeneralDesignationResponse { GeneralDesignationModel = designation }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDesignationResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDesignationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDesignationMaster/GetDesignation")]
        [HttpGet]
        [Produces(typeof(GeneralDesignationModel))]
        public IActionResult GetDesignation(short generalDesignationMasterId)
        {
            try
            {
                GeneralDesignationModel generalDesignationModel = _generalDesignationMasterService.GetDesignation(generalDesignationMasterId);
                return IsNotNull(generalDesignationModel) ? CreateOKResponse(generalDesignationModel) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDesignationModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDesignationModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDesignationMaster/UpdateDesignation")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralDesignationResponse))]
        public IActionResult UpdateDesignation([FromBody] GeneralDesignationModel model)
        {
            try
            {
                bool isUpdated = _generalDesignationMasterService.UpdateDesignation(model);
                return isUpdated ? CreateOKResponse(new GeneralDesignationResponse { GeneralDesignationModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDesignationResponse { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDesignationResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDesignationMaster/DeleteDesignation")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteDesignation([FromBody] ParameterModel designationIds)
        {
            try
            {
                bool deleted = _generalDesignationMasterService.DeleteDesignation(designationIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Designation.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
