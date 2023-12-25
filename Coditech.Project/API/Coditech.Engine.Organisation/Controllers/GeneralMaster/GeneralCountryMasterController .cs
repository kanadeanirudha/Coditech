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
    public class GeneralCountryMasterController : BaseController
    {
        private readonly IGeneralCountryMasterService _generalCountryMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralCountryMasterController(ICoditechLogging coditechLogging, IGeneralCountryMasterService generalCountryMasterService)
        {
            _generalCountryMasterService = generalCountryMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralCountryMaster/GetCountryList")]
        [Produces(typeof(GeneralCountryListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetCountryList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralCountryListModel list = _generalCountryMasterService.GetCountryList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralCountryListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCountryMaster/CreateCountry")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralCountryResponse))]
        public virtual IActionResult CreateCountry([FromBody] GeneralCountryModel model)
        {
            try
            {
                GeneralCountryModel countryMaster = _generalCountryMasterService.CreateCountry(model);
                return IsNotNull(countryMaster) ? CreateCreatedResponse(new GeneralCountryResponse { GeneralCountryModel = countryMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCountryMaster/GetCountry")]
        [HttpGet]
        [Produces(typeof(GeneralCountryResponse))]
        public virtual IActionResult GetCountry(short generalCountryMasterId)
        {
            try
            {
                GeneralCountryModel generalCountryMasterModel = _generalCountryMasterService.GetCountry(generalCountryMasterId);
                return IsNotNull(generalCountryMasterModel) ? CreateOKResponse(new GeneralCountryResponse { GeneralCountryModel = generalCountryMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCountryMaster/UpdateCountry")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralCountryResponse))]
        public virtual IActionResult UpdateCountry([FromBody] GeneralCountryModel model)
        {
            try
            {
                bool isUpdated = _generalCountryMasterService.UpdateCountry(model);
                return isUpdated ? CreateOKResponse(new GeneralCountryResponse { GeneralCountryModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralCountryResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralCountryMaster/DeleteCountry")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteCountry([FromBody] ParameterModel countryIds)
        {
            try
            {
                bool deleted = _generalCountryMasterService.DeleteCountry(countryIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}