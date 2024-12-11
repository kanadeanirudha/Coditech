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

namespace Coditech.API.Controllers
{
    public class DBTMSubscriptionPlanController : BaseController
    {
        private readonly IDBTMSubscriptionPlanService _dBTMSubscriptionPlanService;
        protected readonly ICoditechLogging _coditechLogging;
        public DBTMSubscriptionPlanController(ICoditechLogging coditechLogging, IDBTMSubscriptionPlanService dBTMSubscriptionPlanService)
        {
            _dBTMSubscriptionPlanService = dBTMSubscriptionPlanService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/DBTMSubscriptionPlan/GetDBTMSubscriptionPlanList")]
        [Produces(typeof(DBTMSubscriptionPlanListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetDBTMSubscriptionPlanList(ExpandCollection expand, FilterCollection filter, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                DBTMSubscriptionPlanListModel list = _dBTMSubscriptionPlanService.GetDBTMSubscriptionPlanList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<DBTMSubscriptionPlanListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/DBTMSubscriptionPlan/CreateDBTMSubscriptionPlan")]
        [HttpPost, ValidateModel]
        [Produces(typeof(DBTMSubscriptionPlanResponse))]
        public virtual IActionResult CreateDBTMSubscriptionPlan([FromBody] DBTMSubscriptionPlanModel model)
        {
            try
            {
                DBTMSubscriptionPlanModel plan = _dBTMSubscriptionPlanService.CreateDBTMSubscriptionPlan(model);
                return IsNotNull(plan) ? CreateCreatedResponse(new DBTMSubscriptionPlanResponse { DBTMSubscriptionPlanModel = plan }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/DBTMSubscriptionPlan/GetDBTMSubscriptionPlan")]
        [HttpGet]
        [Produces(typeof(DBTMSubscriptionPlanResponse))]
        public virtual IActionResult GetDBTMSubscriptionPlan(int dBTMSubscriptionPlanId)
        {
            try
            {
                DBTMSubscriptionPlanModel dBTMSubscriptionPlanModel = _dBTMSubscriptionPlanService.GetDBTMSubscriptionPlan(dBTMSubscriptionPlanId);
                return IsNotNull(dBTMSubscriptionPlanModel) ? CreateOKResponse(new DBTMSubscriptionPlanResponse { DBTMSubscriptionPlanModel = dBTMSubscriptionPlanModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/DBTMSubscriptionPlan/UpdateDBTMSubscriptionPlan")]
        [HttpPut, ValidateModel]
        [Produces(typeof(DBTMSubscriptionPlanResponse))]
        public virtual IActionResult UpdateDBTMSubscriptionPlan([FromBody] DBTMSubscriptionPlanModel model)
        {
            try
            {
                bool isUpdated = _dBTMSubscriptionPlanService.UpdateDBTMSubscriptionPlan(model);
                return isUpdated ? CreateOKResponse(new DBTMSubscriptionPlanResponse { DBTMSubscriptionPlanModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new DBTMSubscriptionPlanResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/DBTMSubscriptionPlan/DeleteDBTMSubscriptionPlan")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteDBTMSubscriptionPlan([FromBody] ParameterModel dBTMSubscriptionPlanIds)
        {
            try
            {
                bool deleted = _dBTMSubscriptionPlanService.DeleteDBTMSubscriptionPlan(dBTMSubscriptionPlanIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DBTMSubscriptionPlan.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }        
        }
    }
}