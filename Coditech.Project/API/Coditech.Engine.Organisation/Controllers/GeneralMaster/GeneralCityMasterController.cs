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
    public class GeneralCityMasterController : BaseController
    {
        private readonly IGeneralCityMasterService _generalCityMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralCityMasterController(ICoditechLogging coditechLogging, IGeneralCityMasterService generalCityMasterService)
        {
            _generalCityMasterService = generalCityMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralCityMaster/GetCityList")]
        [Produces(typeof(GeneralCityListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetCityList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralCityListModel list = _generalCityMasterService.GetCityList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralCityListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCityListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCityListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCityMaster/CreateCity")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralCityResponse))]
        public virtual IActionResult CreateCity([FromBody] GeneralCityModel model)
        {
            try
            {
                GeneralCityModel city = _generalCityMasterService.CreateCity(model);
                return IsNotNull(city) ? CreateCreatedResponse(new GeneralCityResponse { GeneralCityModel = city }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCityResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCityResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCityMaster/GetCity")]
        [HttpGet]
        [Produces(typeof(GeneralCityResponse))]
        public virtual IActionResult GetCity(int generalCityMasterId)
        {
            try
            {
                GeneralCityModel generalCityModel = _generalCityMasterService.GetCity(generalCityMasterId);
                return IsNotNull(generalCityModel) ? CreateOKResponse(new GeneralCityResponse() { GeneralCityModel = generalCityModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCityResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCityResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCityMaster/UpdateCity")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralCityResponse))]
        public virtual IActionResult UpdateCity([FromBody] GeneralCityModel model)
        {
            try
            {
                bool isUpdated = _generalCityMasterService.UpdateCity(model);
                return isUpdated ? CreateOKResponse(new GeneralCityResponse { GeneralCityModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCityResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCityResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCityMaster/DeleteCity")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteCity([FromBody] ParameterModel cityIds)
        {
            try
            {
                bool deleted = _generalCityMasterService.DeleteCity(cityIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.City.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCityMaster/GetCityByRegionWise")]
        [HttpGet]
        [Produces(typeof(GeneralCityResponse))]
        public virtual IActionResult GetCityByRegionWise(int generalRegionMasterId)
        {
            try
            {
                GeneralCityListModel list = _generalCityMasterService.GetCityByRegionWise(generalRegionMasterId);
                return IsNotNull(list) ? CreateOKResponse(new GeneralCityListResponse { GeneralCityList = list.GeneralCityList }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCityListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Region.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCityListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}