using Coditech.API.Service;
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
    public class GeneralPersonAttendanceDetailsController : BaseController
    {
        private readonly IGeneralPersonAttendanceDetailsService _generalPersonAttendanceDetailsService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralPersonAttendanceDetailsController(ICoditechLogging coditechLogging, IGeneralPersonAttendanceDetailsService generalPersonAttendanceDetailsService)
        {
            _generalPersonAttendanceDetailsService = generalPersonAttendanceDetailsService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralPersonAttendanceDetails/GetPersonAttendanceList")]
        [Produces(typeof(GeneralPersonAttendanceDetailsListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetPersonAttendanceList(long personId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralPersonAttendanceDetailsListModel list = _generalPersonAttendanceDetailsService.GetPersonAttendanceList(personId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralPersonAttendanceDetailsListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonAttendanceDetails/CreatePersonAttendance")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralPersonAttendanceDetailsResponse))]
        public virtual IActionResult CreatePersonAttendance([FromBody] GeneralPersonAttendanceDetailsModel model)
        {
            try
            {
                GeneralPersonAttendanceDetailsModel PersonAttendance = _generalPersonAttendanceDetailsService.CreatePersonAttendance(model);
                return IsNotNull(PersonAttendance) ? CreateCreatedResponse(new GeneralPersonAttendanceDetailsResponse { GeneralPersonAttendanceDetailsModel = PersonAttendance }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonAttendanceDetails/GetPersonAttendance")]
        [HttpGet]
        [Produces(typeof(GeneralPersonAttendanceDetailsResponse))]
        public virtual IActionResult GetPersonAttendance(long generalPersonAttendanceDetailsId)
        {
            try
            {
                GeneralPersonAttendanceDetailsModel generalPersonAttendanceDetailsModel = _generalPersonAttendanceDetailsService.GetPersonAttendance(generalPersonAttendanceDetailsId);
                return IsNotNull(generalPersonAttendanceDetailsModel) ? CreateOKResponse(new GeneralPersonAttendanceDetailsResponse { GeneralPersonAttendanceDetailsModel = generalPersonAttendanceDetailsModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonAttendanceDetails/UpdatePersonAttendance")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralPersonAttendanceDetailsResponse))]
        public virtual IActionResult UpdatePersonAttendance([FromBody] GeneralPersonAttendanceDetailsModel model)
        {
            try
            {
                bool isUpdated = _generalPersonAttendanceDetailsService.UpdatePersonAttendance(model);
                return isUpdated ? CreateOKResponse(new GeneralPersonAttendanceDetailsResponse { GeneralPersonAttendanceDetailsModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralPersonAttendanceDetailsResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralPersonAttendanceDetails/DeletePersonAttendance")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeletePersonAttendance([FromBody] ParameterModel PersonAttendanceIds)
        {
            try
            {
                bool deleted = _generalPersonAttendanceDetailsService.DeletePersonAttendance(PersonAttendanceIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.PersonAttendance.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}