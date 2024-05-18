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
        private const string createEditEmployeeService = "~/Views/EmployeeMaster/UpdateEmployeeServiceDetails.cshtml";

        public EmployeeServiceController(IEmployeeServiceAgent employeeServiceAgent)
        {
            _employeeServiceAgent = employeeServiceAgent;
        }

        #region Employee Service
        public virtual ActionResult EmployeeServiceList(int employeeId, long personId, DataTableViewModel dataTableViewModel)
        {
            EmployeeServiceListViewModel list = _employeeServiceAgent.GetEmployeeServiceList(dataTableViewModel, employeeId, personId);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeMaster/_ListEmployeeService.cshtml", list);
            }
            return View($"~/Views/EmployeeMaster/ListEmployeeService.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult GetEmployeeService()
        {
            return View(createEditEmployeeService, new EmployeeServiceViewModel());
        }

        [HttpPost]
        public virtual ActionResult GetEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeServiceViewModel = _employeeServiceAgent.CreateEmployee(employeeServiceViewModel);
                if (!employeeServiceViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("EmployeeServiceList", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(employeeServiceViewModel.ErrorMessage));
            return View(createEditEmployeeService, employeeServiceViewModel);
        }
        #endregion Employee Service
    }
}




// [HttpGet]
//  public virtual ActionResult GetEmployeeService(long employeeId, long personId,long employeeServiceId)
//  {
//      EmployeeServiceViewModel employeeServiceViewModel = _employeeServiceAgent.GetEmployeeService(employeeId,personId);
//      return View("~/Views/EmployeeMaster/UpdateEmployeeService.cshtml", employeeServiceViewModel);
//  }

//[HttpPost]
// public virtual ActionResult UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
//  {
//     if (ModelState.IsValid)
//    {
//       SetNotificationMessage(_employeeServiceAgent.UpdateEmployeeService(employeeServiceViewModel).HasError
//      ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
//     : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
//      return RedirectToAction("GetEmployeeService", new { employeeId = employeeServiceViewModel.EmployeeId, personId = employeeServiceViewModel.EmployeeId });
//  }
//     return View("~/Views/EmployeeMaster/UpdateEmployeeService.cshtml", employeeServiceViewModel);
//  }
