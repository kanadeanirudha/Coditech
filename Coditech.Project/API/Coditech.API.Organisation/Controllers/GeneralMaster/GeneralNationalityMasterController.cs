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
    public class GeneralNationalityMasterController : BaseController
    {
        private readonly IGeneralNationalityMasterService _generalNationalityMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralNationalityMasterController(ICoditechLogging coditechLogging, IGeneralNationalityMasterService generalNationalityMasterService)
        {
            _generalNationalityMasterService = generalNationalityMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralNationalityMaster/GetNationalityList")]
        [Produces(typeof(GeneralNationalityListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetNationalityList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralNationalityListModel list = _generalNationalityMasterService.GetNationalityList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralNationalityListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNationalityListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNationalityListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralNationalityMaster/CreateNationality")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralNationalityResponse))]
        public IActionResult CreateNationality([FromBody] GeneralNationalityModel model)
        {
            try
            {
                GeneralNationalityModel nationality = _generalNationalityMasterService.CreateNationality(model);
                return IsNotNull(nationality) ? CreateCreatedResponse(new GeneralNationalityResponse { GeneralNationalityModel = nationality }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralNationalityResponse { HasError = true, ErrorCode = ex.ErrorCode,ErrorMessage = ex.ErrorMessage });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNationalityResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralNationalityMaster/GetNationality")]
        [HttpGet]
        [Produces(typeof(GeneralNationalityResponse))]
        public IActionResult GetNationality(short generalNationalityMasterId)
        {
            try
            {
                GeneralNationalityModel generalNationalityModel = _generalNationalityMasterService.GetNationality(generalNationalityMasterId);
                return IsNotNull(generalNationalityModel) ? CreateOKResponse(new GeneralNationalityResponse() { GeneralNationalityModel = generalNationalityModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralNationalityResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNationalityResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralNationalityMaster/UpdateNationality")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralNationalityResponse))]
        public IActionResult UpdateNationality([FromBody] GeneralNationalityModel model)
        {
            try
            {
                bool isUpdated = _generalNationalityMasterService.UpdateNationality(model);
                return isUpdated ? CreateOKResponse(new GeneralNationalityResponse { GeneralNationalityModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralNationalityResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralNationalityResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralNationalityMaster/DeleteNationality")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteNationality([FromBody] ParameterModel nationalityIds)
        {
            try
            {
                bool deleted = _generalNationalityMasterService.DeleteNationality(nationalityIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Nationality.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}