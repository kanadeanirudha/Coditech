using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class EmployeeServiceController : BaseController
    {
        private readonly IEmployeeServiceAgent _employeeServiceAgent;        

        public EmployeeServiceController(IEmployeeServiceAgent employeeServiceAgent)
        {
            _employeeServiceAgent = employeeServiceAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            EmployeeServiceListViewModel list = new EmployeeServiceListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && dataTableViewModel.SelectedDepartmentId > 0)
            {
                list = _employeeServiceAgent.GetEmployeeServiceList(dataTableViewModel);
            }
            //list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            //list.SelectedDepartmentId = dataTableViewModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeService/_List.cshtml", list);
            }
            return View($"~/Views/EmployeeService/List.cshtml", list);
        }


        [HttpGet]
        public virtual ActionResult GetEmployeeService(long employeeId, long personId)
        {
            EmployeeServiceViewModel employeeServiceViewModel = _employeeServiceAgent.GetEmployeeService(employeeId);
            return View("~/Views/EmployeeService/UpdateEmployeeeService.cshtml", employeeServiceViewModel);
        }


        //[HttpGet]
        //public virtual ActionResult UpdateEmployeeServicve(long employeeId)
        //{
        //    EmployeeServiceViewModel employeeServiceViewModel = _employeeServiceAgent.GetEmployeeService(employeeId);
        //    return ActionView(createEditEmployee, employeeServiceViewModel);
        //}

        //[HttpPost]
        //public virtual ActionResult UpdateEmployeeService(EmployeeServiceViewModel employeeServiceViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        SetNotificationMessage(_employeeServiceAgent.UpdateEmployeeService(employeeServiceViewModel).HasError
        //        ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
        //        return RedirectToAction("UpdateEmployeePersonalDetails", new { employeeId = employeeServiceViewModel.EmployeeId });
        //    }
        //    return View(createEditEmployee, employeeServiceViewModel);
        //}

    }
}