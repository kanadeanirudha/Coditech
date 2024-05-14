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
    public class HospitalDoctorVisitingChargesController : BaseController
    {
        private readonly IHospitalDoctorVisitingChargesService _hospitalDoctorVisitingChargesService;
        protected readonly ICoditechLogging _coditechLogging;
        public HospitalDoctorVisitingChargesController(ICoditechLogging coditechLogging, IHospitalDoctorVisitingChargesService hospitalDoctorVisitingChargesService)
        {
            _hospitalDoctorVisitingChargesService = hospitalDoctorVisitingChargesService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingChargesList")]
        [Produces(typeof(HospitalDoctorVisitingChargesListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetHospitalDoctorVisitingChargesList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                HospitalDoctorVisitingChargesListModel list = _hospitalDoctorVisitingChargesService.GetHospitalDoctorVisitingChargesList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<HospitalDoctorVisitingChargesListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorVisitingCharges/CreateHospitalDoctorVisitingCharges")]
        [HttpPost, ValidateModel]
        [Produces(typeof(HospitalDoctorVisitingChargesResponse))]
        public virtual IActionResult CreateHospitalDoctorVisitingCharges([FromBody] HospitalDoctorVisitingChargesModel model)
        {
            try
            {
                HospitalDoctorVisitingChargesModel hospitaldoctorvisitingchargesMaster = _hospitalDoctorVisitingChargesService.CreateHospitalDoctorVisitingCharges(model);
                return IsNotNull(hospitaldoctorvisitingchargesMaster) ? CreateCreatedResponse(new HospitalDoctorVisitingChargesResponse { HospitalDoctorVisitingChargesModel = hospitaldoctorvisitingchargesMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorVisitingCharges/GetHospitalDoctorVisitingCharges")]
        [HttpGet]
        [Produces(typeof(HospitalDoctorVisitingChargesResponse))]
        public virtual IActionResult GetHospitalDoctorVisitingCharges(short hospitalDoctorVisitingChargesId)
        {
            try
            {
                HospitalDoctorVisitingChargesModel hospitalDoctorVisitingChargesModel = _hospitalDoctorVisitingChargesService.GetHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesId);
                return IsNotNull(hospitalDoctorVisitingChargesModel) ? CreateOKResponse(new HospitalDoctorVisitingChargesResponse { HospitalDoctorVisitingChargesModel = hospitalDoctorVisitingChargesModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorVisitingCharges/UpdateHospitalDoctorVisitingCharges")]
        [HttpPut, ValidateModel]
        [Produces(typeof(HospitalDoctorVisitingChargesResponse))]
        public virtual IActionResult UpdateHospitalDoctorVisitingCharges([FromBody] HospitalDoctorVisitingChargesModel model)
        {
            try
            {
                bool isUpdated = _hospitalDoctorVisitingChargesService.UpdateHospitalDoctorVisitingCharges(model);
                return isUpdated ? CreateOKResponse(new HospitalDoctorVisitingChargesResponse { HospitalDoctorVisitingChargesModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new HospitalDoctorVisitingChargesResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/HospitalDoctorVisitingCharges/DeleteHospitalDoctorVisitingCharges")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteHospitalDoctorVisitingCharges([FromBody] ParameterModel hospitaldoctorvisitingchargesIds)
        {
            try
            {
                bool deleted = _hospitalDoctorVisitingChargesService.DeleteHospitalDoctorVisitingCharges(hospitaldoctorvisitingchargesIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.HospitalDoctorVisitingCharges.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}