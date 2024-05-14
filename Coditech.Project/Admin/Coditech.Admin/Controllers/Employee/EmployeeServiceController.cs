using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class EmployeeServiceController : BaseController
    {
        private readonly IEmployeeServiceAgent _employeeServiceAgent;
        private const string createEditEmployee = "~/Views/EmployeeMaster/CreateEditEmployee.cshtml";

        public EmployeeServiceController(IEmployeeServiceAgent employeeServiceAgent)
        {
            _employeeServiceAgent = employeeServiceAgent;
        }

        #region Employee Service
        public virtual ActionResult EmployeeServiceList(int employeeId,DataTableViewModel dataTableViewModel)
        {
            EmployeeServiceListViewModel list = _employeeServiceAgent.GetEmployeeServiceList(dataTableViewModel,employeeId);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeMaster/_List.cshtml", list);
            }
            return View($"~/Views/EmployeeMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetEmployeeService(long employeeId, long personId)
        {
            EmployeeServiceViewModel employeeServiceViewModel = _employeeServiceAgent.GetEmployeeService(employeeId);
            return View("~/Views/EmployeeMaster/EmployeeService.cshtml", employeeServiceViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeServiceAgent.UpdateEmployeeService(employeeServiceViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetEmployeeService", new { employeeId = employeeServiceViewModel.EmployeeId, personId = employeeServiceViewModel.EmployeeId });
            }
            return View("~/Views/EmployeeMaster/EmployeeeService.cshtml", employeeServiceViewModel);
        }
        #endregion Employee Service
    }
}