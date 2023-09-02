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
    public class GeneralDepartmentMasterController : BaseController
    {
        private readonly IGeneralDepartmentMasterService _generalDepartmentMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralDepartmentMasterController(ICoditechLogging coditechLogging, IGeneralDepartmentMasterService generalDepartmentMasterService)
        {
            _generalDepartmentMasterService = generalDepartmentMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralDepartmentMaster/GetDepartmentList")]
        [Produces(typeof(GeneralDepartmentListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetDepartmentList(ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralDepartmentListModel list = _generalDepartmentMasterService.GetDepartmentList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralDepartmentListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDepartmentListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDepartmentListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDepartmentMaster/CreateDepartment")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralDepartmentResponse))]
        public IActionResult CreateDepartment([FromBody] GeneralDepartmentModel model)
        {
            try
            {
                GeneralDepartmentModel departmentMaster = _generalDepartmentMasterService.CreateDepartment(model);
                return IsNotNull(departmentMaster) ? CreateCreatedResponse(new GeneralDepartmentResponse { GeneralDepartmentModel = departmentMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDepartmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDepartmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDepartmentMaster/GetDepartment")]
        [HttpGet]
        [Produces(typeof(GeneralDepartmentResponse))]
        public IActionResult GetDepartment(short generalDepartmentId)
        {
            try
            {
                GeneralDepartmentModel generalDepartmentMasterModel = _generalDepartmentMasterService.GetDepartment(generalDepartmentId);
                return IsNotNull(generalDepartmentMasterModel) ? CreateOKResponse(new GeneralDepartmentResponse { GeneralDepartmentModel = generalDepartmentMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDepartmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDepartmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDepartmentMaster/UpdateDepartment")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralDepartmentResponse))]
        public IActionResult UpdateDepartment([FromBody] GeneralDepartmentModel model)
        {
            try
            {
                bool isUpdated = _generalDepartmentMasterService.UpdateDepartment(model);
                return isUpdated ? CreateOKResponse(new GeneralDepartmentResponse { GeneralDepartmentModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDepartmentResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDepartmentResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDepartmentMaster/DeleteDepartment")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteDepartment([FromBody] ParameterModel departmentIds)
        {
            try
            {
                bool deleted = _generalDepartmentMasterService.DeleteDepartment(departmentIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}