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
    public class GeneralEnumaratorMasterController : BaseController
    {
        private readonly IGeneralEnumaratorMasterService _generalEnumaratorService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralEnumaratorMasterController(ICoditechLogging coditechLogging, IGeneralEnumaratorMasterService generalEnumaratorService)
        {
            _generalEnumaratorService = generalEnumaratorService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralEnumarator/GetEnumaratorList")]
        [Produces(typeof(GeneralEnumaratorResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetEnumaratorList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralEnumaratorListModel list = _generalEnumaratorService.GetEnumaratorList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralEnumaratorResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumarator/CreateEnumarator")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralEnumaratorResponse))]
        public virtual IActionResult CreateEnumarator([FromBody] GeneralEnumaratorModel model)
        {
            try
            {
                GeneralEnumaratorModel Enumarator = _generalEnumaratorService.CreateEnumarator(model);
                return IsNotNull(Enumarator) ? CreateCreatedResponse(new GeneralEnumaratorResponse { GeneralEnumaratorModel = Enumarator }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumarator/GetEnumarator")]
        [HttpGet]
        [Produces(typeof(GeneralEnumaratorResponse))]
        public virtual IActionResult GetEnumarator(int GeneralEnumaratorId)
        {
            try
            {
                GeneralEnumaratorModel generalEnumaratorModel = _generalEnumaratorService.GetEnumarator(GeneralEnumaratorId);
                return IsNotNull(generalEnumaratorModel) ? CreateOKResponse(new GeneralEnumaratorResponse() { GeneralEnumaratorModel = generalEnumaratorModel }) : NotFound();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumarator/UpdateEnumarator")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralEnumaratorResponse))]
        public virtual IActionResult UpdateEnumarator([FromBody] GeneralEnumaratorModel model)
        {
            try
            {
                bool isUpdated = _generalEnumaratorService.UpdateEnumarator(model);
                return isUpdated ? CreateOKResponse(new GeneralEnumaratorResponse { GeneralEnumaratorModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralEnumaratorResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralEnumarator/DeleteEnumarator")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteEnumarator([FromBody] ParameterModel generalEnumaratorIds)
        {
            try
            {
                bool deleted = _generalEnumaratorService.DeleteEnumarator(generalEnumaratorIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.GeneralEnumaratorMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}
