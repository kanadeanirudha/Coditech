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
    public class GeneralDistrictMasterController : BaseController
    {
        private readonly IGeneralDistrictMasterService _generalDistrictMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralDistrictMasterController(ICoditechLogging coditechLogging, IGeneralDistrictMasterService generalDistrictMasterService)
        {
            _generalDistrictMasterService = generalDistrictMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralDistrictMaster/GetDistrictList")]
        [Produces(typeof(GeneralDistrictListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetDistrictList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralDistrictListModel list = _generalDistrictMasterService.GetDistrictList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralDistrictListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDistrictListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDistrictListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDistrictMaster/CreateDistrict")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralDistrictResponse))]
        public virtual IActionResult CreateDistrict([FromBody] GeneralDistrictModel model)
        {
            try
            {
                GeneralDistrictModel districtMaster = _generalDistrictMasterService.CreateDistrict(model);
                return IsNotNull(districtMaster) ? CreateCreatedResponse(new GeneralDistrictResponse { GeneralDistrictModel = districtMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDistrictResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDistrictResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDistrictMaster/GetDistrict")]
        [HttpGet]
        [Produces(typeof(GeneralDistrictResponse))]
        public virtual IActionResult GetDistrict(short generalDistrictMasterId)
        {
            try
            {
                GeneralDistrictModel generalDistrictMasterModel = _generalDistrictMasterService.GetDistrict(generalDistrictMasterId);
                return IsNotNull(generalDistrictMasterModel) ? CreateOKResponse(new GeneralDistrictResponse { GeneralDistrictModel = generalDistrictMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDistrictResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDistrictResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDistrictMaster/UpdateDistrict")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralDistrictResponse))]
        public virtual IActionResult UpdateDistrict([FromBody] GeneralDistrictModel model)
        {
            try
            {
                bool isUpdated = _generalDistrictMasterService.UpdateDistrict(model);
                return isUpdated ? CreateOKResponse(new GeneralDistrictResponse { GeneralDistrictModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDistrictResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDistrictResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDistrictMaster/DeleteDistrict")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteDistrict([FromBody] ParameterModel districtIds)
        {
            try
            {
                bool deleted = _generalDistrictMasterService.DeleteDistrict(districtIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        [Route("/GeneralDistrictMaster/GetDistrictByRegionWise")]
        [HttpGet]
        [Produces(typeof(GeneralDistrictListResponse))]
        public virtual IActionResult GetDistrictByRegionWise(int generalRegionMasterId)
        {
            try
            {
                GeneralDistrictListModel list = _generalDistrictMasterService.GetDistrictByRegionWise(generalRegionMasterId);
                return IsNotNull(list) ? CreateOKResponse(new GeneralDistrictListResponse { GeneralDistrictList = list.GeneralDistrictList }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralDistrictListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.District.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralDistrictListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}