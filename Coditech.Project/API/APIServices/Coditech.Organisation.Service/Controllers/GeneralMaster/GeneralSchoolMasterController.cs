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
    public class GeneralSchoolMasterController : BaseController
    {
        private readonly IGeneralSchoolMasterService _generalSchoolMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralSchoolMasterController(ICoditechLogging coditechLogging, IGeneralSchoolMasterService generalSchoolMasterService)
        {
            _generalSchoolMasterService = generalSchoolMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralSchoolMaster/GetSchoolList")]
        [Produces(typeof(GeneralSchoolListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetSchoolList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralSchoolListModel list = _generalSchoolMasterService.GetSchoolList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralSchoolListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSchoolListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSchoolListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSchoolMaster/CreateSchool")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralSchoolResponse))]
        public virtual IActionResult CreateSchool([FromBody] GeneralSchoolModel model)
        {
            try
            {
                GeneralSchoolModel schoolMaster = _generalSchoolMasterService.CreateSchool(model);
                return IsNotNull(schoolMaster) ? CreateCreatedResponse(new GeneralSchoolResponse { GeneralSchoolModel = schoolMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSchoolResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSchoolResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSchoolMaster/GetSchool")]
        [HttpGet]
        [Produces(typeof(GeneralSchoolResponse))]
        public virtual IActionResult GetSchool(short generalSchoolMasterId)
        {
            try
            {
                GeneralSchoolModel generalSchoolMasterModel = _generalSchoolMasterService.GetSchool(generalSchoolMasterId);
                return IsNotNull(generalSchoolMasterModel) ? CreateOKResponse(new GeneralSchoolResponse { GeneralSchoolModel = generalSchoolMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSchoolResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSchoolResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSchoolMaster/UpdateSchool")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralSchoolResponse))]
        public virtual IActionResult UpdateSchool([FromBody] GeneralSchoolModel model)
        {
            try
            {
                bool isUpdated = _generalSchoolMasterService.UpdateSchool(model);
                return isUpdated ? CreateOKResponse(new GeneralSchoolResponse { GeneralSchoolModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralSchoolResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralSchoolResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralSchoolMaster/DeleteSchool")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteSchool([FromBody] ParameterModel SchoolIds)
        {
            try
            {
                bool deleted = _generalSchoolMasterService.DeleteSchool(SchoolIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.SchoolMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}