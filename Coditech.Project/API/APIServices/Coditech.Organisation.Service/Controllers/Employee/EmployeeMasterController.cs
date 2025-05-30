﻿using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
{
    public class EmployeeMasterController : BaseController
    {
        private readonly IEmployeeMasterService _employeeMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public EmployeeMasterController(ICoditechLogging coditechLogging, IEmployeeMasterService employeeMasterService)
        {
            _employeeMasterService = employeeMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/EmployeeMaster/GetEmployeeList")]
        [Produces(typeof(EmployeeMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEmployeeList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                EmployeeMasterListModel list = _employeeMasterService.GetEmployeeList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<EmployeeMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        //EmployeeListByCentreCode
        [HttpGet]
        [Route("/EmployeeMaster/GetEmployeeListByCentreCode")]
        [Produces(typeof(EmployeeMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEmployeeListByCentreCode(string centreCode, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                EmployeeMasterListModel list = _employeeMasterService.GetEmployeeListByCentreCode(centreCode, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<EmployeeMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/EmployeeMaster/GetEmployeeOtherDetail")]
        [HttpGet]
        [Produces(typeof(EmployeeMasterResponse))]
        public virtual IActionResult GetEmployeeOtherDetail(long employeeId)
        {
            try
            {
                EmployeeMasterModel employeeMasterModel = _employeeMasterService.GetEmployeeOtherDetail(employeeId);
                return IsNotNull(employeeMasterModel) ? CreateOKResponse(new EmployeeMasterResponse { EmployeeMasterModel = employeeMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new EmployeeMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/EmployeeMaster/UpdateEmployeeOtherDetail")]
        [HttpPut, ValidateModel]
        [Produces(typeof(EmployeeMasterResponse))]
        public virtual IActionResult UpdateEmployeeOtherDetail([FromBody] EmployeeMasterModel model)
        {
            try
            {
                bool isUpdated = _employeeMasterService.UpdateEmployeeOtherDetail(model);
                return isUpdated ? CreateOKResponse(new EmployeeMasterResponse { EmployeeMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new EmployeeMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new EmployeeMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/EmployeeMaster/DeleteEmployee")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEmployee([FromBody] ParameterModel employeeIds)
        {
            try
            {
                bool deleted = _employeeMasterService.DeleteEmployee(employeeIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EmployeeMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}