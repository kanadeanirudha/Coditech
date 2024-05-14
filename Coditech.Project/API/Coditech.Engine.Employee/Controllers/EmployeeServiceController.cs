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
    public class EmployeeServiceController : BaseController
    {
        private readonly IEmployeeServiceService _employeeServiceService;
        protected readonly ICoditechLogging _coditechLogging;
        public EmployeeServiceController(ICoditechLogging coditechLogging, IEmployeeServiceService employeeServiceService)
        {
            _employeeServiceService = employeeServiceService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/EmployeeService/GetEmployeeList")]
        [Produces(typeof(EmployeeServiceListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEmployeeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                EmployeeServiceListModel list = _employeeServiceService.GetEmployeeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<EmployeeServiceListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeServiceListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeServiceListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/EmployeeService/GetEmployeeService")]
        [HttpGet]
        [Produces(typeof(EmployeeServiceResponse))]
        public virtual IActionResult GetEmployeeService(long employeeId)
        {
            try
            {
                EmployeeServiceModel employeeServiceModel = _employeeServiceService.GetEmployeeService(employeeId);
                return IsNotNull(employeeServiceModel) ? CreateOKResponse(new EmployeeServiceResponse { EmployeeServiceModel = employeeServiceModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new EmployeeServiceResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeServiceResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/EmployeeService/UpdateEmployeeService")]
        [HttpPut, ValidateModel]
        [Produces(typeof(EmployeeServiceResponse))]
        public virtual IActionResult UpdateEmployeeService([FromBody] EmployeeServiceModel model)
        {
            try
            {
                bool isUpdated = _employeeServiceService.UpdateEmployeeService(model);
                return isUpdated ? CreateOKResponse(new EmployeeServiceResponse { EmployeeServiceModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new EmployeeServiceResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeServiceResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/EmployeeService/DeleteEmployee")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEmployee([FromBody] ParameterModel employeeIds)
        {
            try
            {
                bool deleted = _employeeServiceService.DeleteEmployee(employeeIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeService.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}