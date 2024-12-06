using Coditech.API.Data;
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

namespace Coditech.Engine.DBTM.Controllers
{
    public class TicketMasterController : BaseController
    {
        private readonly ITicketMasterService _ticketMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public TicketMasterController(ICoditechLogging coditechLogging, ITicketMasterService ticketMasterService)
        {
            _ticketMasterService = ticketMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/TicketMaster/GetTicketMasterList")]
        [Produces(typeof(TicketMasterListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetTicketMasterList(long userMasterId, FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                TicketMasterListModel list = _ticketMasterService.GetTicketMasterList(userMasterId, filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<TicketMasterListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TicketMasterListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TicketMasterListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TicketMaster/CreateTicket")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TicketMasterResponse))]
        public virtual IActionResult CreateTicket([FromBody] TicketMasterModel model)
        {
            try
            {
                TicketMasterModel ticketMastermodel = _ticketMasterService.CreateTicket(model);
                return IsNotNull(ticketMastermodel) ? CreateCreatedResponse(new TicketMasterResponse { TicketMasterModel = ticketMastermodel }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TicketMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TicketMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TicketMaster/GetTicket")]
        [HttpGet]
        [Produces(typeof(TicketMasterResponse))]
        public virtual IActionResult GetTicket(long userId)
        {
            try
            {
                TicketMasterModel ticketMasterModel = _ticketMasterService.GetTicket(userId);
                return IsNotNull(ticketMasterModel) ? CreateOKResponse(new TicketMasterResponse { TicketMasterModel = ticketMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TicketMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TicketMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TicketMaster/UpdateTicket")]
        [HttpPut, ValidateModel]
        [Produces(typeof(TicketMasterResponse))]
        public virtual IActionResult UpdateTicket([FromBody] TicketMasterModel model)
        {
            try
            {
                bool isUpdated = _ticketMasterService.UpdateTicket(model);
                return isUpdated ? CreateOKResponse(new TicketMasterResponse { TicketMasterModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TicketMasterResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TicketMasterResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/TicketMaster/DeleteTicket")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteTicket([FromBody] ParameterModel ticketMasterIds)
        {
            try
            {
                bool deleted = _ticketMasterService.DeleteTicket(ticketMasterIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.TicketMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }        
    }
}