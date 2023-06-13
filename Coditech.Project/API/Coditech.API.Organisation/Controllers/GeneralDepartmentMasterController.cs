using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace Coditech.API.DepartmentMaster.Controllers
{
    [ApiController]
    public class GeneralDepartmentMasterController : BaseController
    {
        private readonly IGeneralDepartmentMasterService _generalDepartmentMasterService;
        protected readonly ICoditechLogging _coditechLogging;
        public GeneralDepartmentMasterController(ICoditechLogging coditechLogging, IGeneralDepartmentMasterService generalDepartmentMasterService)
        {
            _generalDepartmentMasterService = generalDepartmentMasterService;
            _coditechLogging = coditechLogging;
        }

        [Route("/GeneralDepartmentMaster/Get")]
        [HttpGet]
        [Produces(typeof(GeneralDepartmentMasterModel))]
        public IActionResult Get(short deneralDepartmentMasterId)
        {
            try
            {
                GeneralDepartmentMasterModel generalDepartmentMasterModel = _generalDepartmentMasterService.Get(deneralDepartmentMasterId);
                return HelperUtility.IsNotNull(generalDepartmentMasterModel) ? CreateOKResponse(generalDepartmentMasterModel) : null;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new GeneralDepartmentMasterModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateUnauthorizedResponse(new GeneralDepartmentMasterModel { HasError = true, ErrorMessage = ex.Message });
            }
        }

        [Route("/GeneralDepartmentMaster/Update")]
        [HttpPost]
        [Produces(typeof(GeneralDepartmentMasterModel))]
        public IActionResult Update([FromBody] GeneralDepartmentMasterModel model)
        {
            try
            {
                GeneralDepartmentMasterModel DepartmentMaster = _generalDepartmentMasterService.Update(model);
                return HelperUtility.IsNotNull(DepartmentMaster) ? CreateOKResponse(DepartmentMaster) : null;
            }
            catch (CoditechException ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Warning);
                return CreateUnauthorizedResponse(new GeneralDepartmentMasterModel { HasError = true, ErrorCode = ex.ErrorCode });
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.DepartmentMaster.ToString(), TraceLevel.Error);
                return CreateUnauthorizedResponse(new GeneralDepartmentMasterModel { HasError = true, ErrorMessage = ex.Message });
            }

        }

    }
}