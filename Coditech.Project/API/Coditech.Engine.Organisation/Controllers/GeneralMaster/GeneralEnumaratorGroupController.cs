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
    public class GeneralEnumaratorGroupController : BaseController
    {
        private readonly IGeneralEnumaratorGroupService _generalEnumaratorGroupService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralEnumaratorGroupController(ICoditechLogging coditechLogging, IGeneralEnumaratorGroupService generalEnumaratorGroupService)
        {
            _generalEnumaratorGroupService = generalEnumaratorGroupService;
            _coditechLogging = coditechLogging;
        }
        #region EnumaratorGroup
        [HttpGet]
        [Route("/GeneralEnumaratorGroup/GetEnumaratorGroupList")]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEnumaratorGroupList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralEnumaratorGroupListModel list = _generalEnumaratorGroupService.GetEnumaratorGroupList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralEnumaratorGroupResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroup/CreateEnumaratorGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        public virtual IActionResult CreateEnumaratorGroup([FromBody] GeneralEnumaratorGroupModel model)
        {
            try
            {
                GeneralEnumaratorGroupModel enumaratorGroup = _generalEnumaratorGroupService.CreateEnumaratorGroup(model);
                return IsNotNull(enumaratorGroup) ? CreateCreatedResponse(new GeneralEnumaratorGroupResponse { GeneralEnumaratorGroupModel = enumaratorGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroup/GetEnumaratorGroup")]
        [HttpGet]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        public virtual IActionResult GetEnumaratorGroup(int generalEnumaratorGroupId)
        {
            try
            {
                GeneralEnumaratorGroupModel generalEnumaratorGroupModel = _generalEnumaratorGroupService.GetEnumaratorGroup(generalEnumaratorGroupId);
                return IsNotNull(generalEnumaratorGroupModel) ? CreateOKResponse(new GeneralEnumaratorGroupResponse() { GeneralEnumaratorGroupModel = generalEnumaratorGroupModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroup/UpdateEnumaratorGroup")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralEnumaratorGroupResponse))]
        public virtual IActionResult UpdateEnumaratorGroup([FromBody] GeneralEnumaratorGroupModel model)
        {
            try
            {
                bool isUpdated = _generalEnumaratorGroupService.UpdateEnumaratorGroup(model);
                return isUpdated ? CreateOKResponse(new GeneralEnumaratorGroupResponse { GeneralEnumaratorGroupModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorGroupResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroup/DeleteEnumaratorGroup")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEnumaratorGroup([FromBody] ParameterModel EnumaratorGroupIds)
        {
            try
            {
                bool deleted = _generalEnumaratorGroupService.DeleteEnumaratorGroup(EnumaratorGroupIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.EnumaratorGroup.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        #endregion

        #region Enumarator
        [Route("/GeneralEnumaratorGroup/InsertUpdateEnumarator")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralEnumaratorResponse))]
        public virtual IActionResult InsertUpdateEnumarator([FromBody] GeneralEnumaratorModel model)
        {
            try
            {
                GeneralEnumaratorModel enumaratorGroup = _generalEnumaratorGroupService.InsertUpdateEnumarator(model);
                return IsNotNull(enumaratorGroup) ? CreateCreatedResponse(new GeneralEnumaratorResponse { GeneralEnumaratorModel = enumaratorGroup }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroup/GetEnumarator")]
        [HttpGet]
        [Produces(typeof(GeneralEnumaratorResponse))]
        public virtual IActionResult GetEnumarator(int generalEnumaratorId)
        {
            try
            {
                GeneralEnumaratorModel generalEnumaratorModel = _generalEnumaratorGroupService.GetEnumarator(generalEnumaratorId);
                return IsNotNull(generalEnumaratorModel) ? CreateOKResponse(new GeneralEnumaratorResponse() { GeneralEnumaratorModel = generalEnumaratorModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumaratorGroup/DeleteEnumarator")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEnumarator([FromBody] ParameterModel enumaratorIds)
        {
            try
            {
                bool deleted = _generalEnumaratorGroupService.DeleteEnumarator(enumaratorIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.Enumarator.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
        #endregion
    }
}
