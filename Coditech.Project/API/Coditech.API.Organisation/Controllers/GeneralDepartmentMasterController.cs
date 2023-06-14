using Coditech.API.Service;
using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Exceptions;
using Coditech.Common.Logger;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

using static Coditech.Common.Helper.HelperUtility;

namespace Coditech.API.Controllers
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

        [Route("/GeneralDepartmentMaster/Insert")]
        [HttpPost]
        [Produces(typeof(GeneralDepartmentMasterModel))]
        public IActionResult Insert([FromBody] GeneralDepartmentMasterModel model)
        {
            try
            {
                GeneralDepartmentMasterModel departmentMaster = _generalDepartmentMasterService.Insert(model);
                return IsNotNull(departmentMaster) ? CreateOKResponse(departmentMaster) : null;
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

        [Route("/GeneralDepartmentMaster/Get")]
        [HttpGet]
        [Produces(typeof(GeneralDepartmentMasterModel))]
        public IActionResult Get(short deneralDepartmentMasterId)
        {
            try
            {
                GeneralDepartmentMasterModel generalDepartmentMasterModel = _generalDepartmentMasterService.Get(deneralDepartmentMasterId);
                return IsNotNull(generalDepartmentMasterModel) ? CreateOKResponse(generalDepartmentMasterModel) : NotFound();
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
                GeneralDepartmentMasterModel departmentMaster = _generalDepartmentMasterService.Update(model);
                return IsNotNull(departmentMaster) ? CreateOKResponse(departmentMaster) : null;
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