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
    public class GeneralUserMainMenuMasterController : BaseController
    {
        private readonly IGeneralUserMainMenuMasterService _generalUserMainMenuMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralUserMainMenuMasterController(ICoditechLogging coditechLogging, IGeneralUserMainMenuMasterService generalUserMainMenuMasterService)
        {
            _generalUserMainMenuMasterService = generalUserMainMenuMasterService;
            _coditechLogging = coditechLogging;
        }

        [HttpGet]
        [Route("/GeneralUserMainMenuMaster/GetUserMainMenuList")]
        [Produces(typeof(GeneralCountryListResponse))]
        [TypeFilter(typeof(BindQueryFilter))]
        public virtual IActionResult GetUserMainMenuList(FilterCollection filter, ExpandCollection expand, SortCollection sort, int pageIndex, int pageSize)
        {
            try
            {
                GeneralUserMainMenuListModel list = _generalUserMainMenuMasterService.GetUserMainMenuList(filter, sort.ToNameValueCollectionSort(), expand.ToNameValueCollectionExpands(), pageIndex, pageSize);
                string data = ApiHelper.ToJson(list);
                return !string.IsNullOrEmpty(data) ? CreateOKResponse<GeneralUserMainMenuListResponse>(data) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuListResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.CountryMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuListResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserMainMenuMaster/CreateUserMainMenu")]
        [HttpPost, ValidateModel]
        [Produces(typeof(GeneralUserMainMenuResponse))]
        public virtual IActionResult CreateUserMainMenu([FromBody] GeneralUserMainMenuModel model)
        {
            try
            {
                GeneralUserMainMenuModel userMainMenuMaster = _generalUserMainMenuMasterService.CreateUserMainMenu(model);
                return IsNotNull(userMainMenuMaster) ? CreateCreatedResponse(new GeneralUserMainMenuResponse { GeneralUserMainMenuModel = userMainMenuMaster }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserMainMenuMaster/GetUserMainMenu")]
        [HttpGet]
        [Produces(typeof(GeneralUserMainMenuResponse))]
        public virtual IActionResult GetUserMainMenu(short generalUserMainMenuMasterId)
        {
            try
            {
                GeneralUserMainMenuModel generalUserMainMenuMasterModel = _generalUserMainMenuMasterService.GetUserMainMenu(generalUserMainMenuMasterId);
                return IsNotNull(generalUserMainMenuMasterModel) ? CreateOKResponse(new GeneralUserMainMenuResponse { GeneralUserMainMenuModel = generalUserMainMenuMasterModel }) : CreateNoContentResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserMainMenuMaster/UpdateUserMainMenu")]
        [HttpPut, ValidateModel]
        [Produces(typeof(GeneralUserMainMenuResponse))]
        public virtual IActionResult UpdateUserMainMenu([FromBody] GeneralUserMainMenuModel model)
        {
            try
            {
                bool isUpdated = _generalUserMainMenuMasterService.UpdateUserMainMenu(model);
                return isUpdated ? CreateOKResponse(new GeneralUserMainMenuResponse { GeneralUserMainMenuModel = model }) : CreateInternalServerErrorResponse();
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new GeneralUserMainMenuResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralUserMainMenuMaster/DeleteUserMainMenu")]
        [HttpPost, ValidateModel]
        [Produces(typeof(TrueFalseResponse))]
        public virtual IActionResult DeleteUserMainMenu([FromBody] ParameterModel userMainMenuIds)
        {
            try
            {
                bool deleted = _generalUserMainMenuMasterService.DeleteUserMainMenu(userMainMenuIds);
                return CreateOKResponse(new TrueFalseResponse { IsSuccess = deleted });
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Warning);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.UserMainMenuMaster.ToString(), TraceLevel.Error);
                return CreateInternalServerErrorResponse(new TrueFalseResponse { HasError = true, ErrorMessage = ex.Message });
            }
        }
    }
}