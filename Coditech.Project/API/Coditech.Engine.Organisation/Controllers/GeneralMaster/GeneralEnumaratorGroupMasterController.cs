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
    public class GeneralEnumaratorGroupMasterController : BaseController
    {
        private readonly IGeneralEnumaratorGroupMasterService _generalEnumaratorGroupMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralEnumaratorGroupMasterController(ICoditechLogging coditechLogging, IGeneralEnumaratorGroupMasterService generalEnumaratorGroupMasterService)
        {
            _generalEnumaratorGroupMasterService = generalEnumaratorGroupMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralEnumaratorGroupMaster/GetEnumaratorGroupList")]
        [Produces(typeof(GeneralEnumaratorGroupListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEnumaratorGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralEnumaratorGroupListModel list = _generalEnumaratorGroupMasterService.GetGeneralEnumaratorGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralEnumaratorGroupListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroupMaster/CreateEnumaratorGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        public virtual IActionResult CreateEnumaratorGroup([FromBody] GeneralEnumaratorGroupModel model)
        {
            try
            {
                GeneralEnumaratorGroupModel enumaratorGroupMaster = _generalEnumaratorGroupMasterService.CreateGeneralEnumaratorGroup(model);
                return IsNotNull(enumaratorGroupMaster) ? CreateCreatedResponse(new GeneralEnumaratorGroupResponse { GeneralEnumaratorGroupModel = enumaratorGroupMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroupMaster/GetEnumaratorGroup")]
        [HttpGet]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        public virtual IActionResult GetEnumaratorGroup(int generalEnumaratorGroupMasterId)
        {
            try
            {
                GeneralEnumaratorGroupModel generalEnumaratorGroupMasterModel = _generalEnumaratorGroupMasterService.GetGeneralEnumaratorGroup(generalEnumaratorGroupMasterId);
                return IsNotNull(generalEnumaratorGroupMasterModel) ? CreateOKResponse(new GeneralEnumaratorGroupResponse { GeneralEnumaratorGroupModel = generalEnumaratorGroupMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroupMaster/UpdateEnumaratorGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        public virtual IActionResult UpdateEnumaratorGroup([FromBody] GeneralEnumaratorGroupModel model)
        {
            try
            {
                bool isUpdated = _generalEnumaratorGroupMasterService.UpdateGeneralEnumaratorGroup(model);
                return isUpdated ? CreateOKResponse(new GeneralEnumaratorGroupResponse { GeneralEnumaratorGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroupMaster/DeleteEnumaratorGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEnumaratorGroup([FromBody] ParameterModel generalEnumaratorGroupIds)
        {
            try
            {
                bool deleted = _generalEnumaratorGroupMasterService.DeleteGeneralEnumaratorGroup(generalEnumaratorGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorGroupMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}