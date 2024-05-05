using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class EmployeeMasterController : BaseController
    {
        private readonly IEmployeeMasterAgent _employeeMasterAgent;
        private const string createEditEmployee = "~/Views/EmployeeMaster/CreateEditEmployee.cshtml";

        public EmployeeMasterController(IEmployeeMasterAgent employeeMasterAgent)
        {
            _employeeMasterAgent = employeeMasterAgent;
        }
        #region Employee
        public virtual ActionResult List(DataTableViewModel dataTableViewModel)
        {
            EmployeeMasterListViewModel list = new EmployeeMasterListViewModel();
            if (!string.IsNullOrEmpty(dataTableViewModel.SelectedCentreCode) && dataTableViewModel.SelectedDepartmentId > 0)
            {
                list = _employeeMasterAgent.GetEmployeeMasterList(dataTableViewModel);
            }
            list.SelectedCentreCode = dataTableViewModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableViewModel.SelectedDepartmentId;

            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeMaster/_List.cshtml", list);
            }
            return View($"~/Views/EmployeeMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreateEmployee()
        {
            return View(createEditEmployee, new EmployeeCreateEditViewModel());
        }

        [HttpPost]
        public virtual ActionResult CreateEmployee(EmployeeCreateEditViewModel employeeCreateEditViewModel, string selectedCentreCode, int selectedDepartmentId)
        {
            if (ModelState.IsValid)
            {
                employeeCreateEditViewModel = _employeeMasterAgent.CreateEmployee(employeeCreateEditViewModel);
                if (!employeeCreateEditViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    // Redirect to the List action with selectedCentreCode and selectedDepartmentId
                    return RedirectToAction("List", new { selectedCentreCode, selectedDepartmentId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(employeeCreateEditViewModel.ErrorMessage));
            return View(createEditEmployee, employeeCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateEmployeePersonalDetails(long employeeId, long personId)
        {
            EmployeeCreateEditViewModel employeeCreateEditViewModel = _employeeMasterAgent.GetEmployeePersonalDetails(employeeId, personId);
            return ActionView(createEditEmployee, employeeCreateEditViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateEmployeePersonalDetails(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeMasterAgent.UpdateEmployeePersonalDetails(employeeCreateEditViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("UpdateEmployeePersonalDetails", new { employeeId = employeeCreateEditViewModel.EmployeeId, personId = employeeCreateEditViewModel.PersonId });
            }
            return View(createEditEmployee, employeeCreateEditViewModel);
        }

        public virtual ActionResult Delete(string employeeIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(employeeIds))
            {
                status = _employeeMasterAgent.DeleteEmployee(employeeIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<EmployeeMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<EmployeeMasterController>(x => x.List(null));
        }


        #endregion Employee

        #region Employee Other Detail
        [HttpGet]
        public virtual ActionResult GetEmployeeOtherDetail(long employeeId, long personId)
        {
            EmployeeMasterViewModel employeeMasterViewModel = _employeeMasterAgent.GetEmployeeOtherDetail(employeeId);
            return View("~/Views/EmployeeMaster/UpdateEmployeeeDetails.cshtml", employeeMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult UpdateEmployeeOtherDetail(EmployeeMasterViewModel employeeMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeMasterAgent.UpdateEmployeeOtherDetail(employeeMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("GetEmployeeOtherDetail", new { employeeId = employeeMasterViewModel.EmployeeId, personId = employeeMasterViewModel.PersonId });
            }
            return View("~/Views/EmployeeMaster/UpdateEmployeeeDetails.cshtml", employeeMasterViewModel);
        }
        #endregion Employee Other Detail

        #region Employee Address
        [HttpGet]
        public virtual ActionResult CreateEditEmployeeAddress(long employeeId, long personId)
        {
            GeneralPersonAddressListViewModel model = new GeneralPersonAddressListViewModel()
            {
                EntityId = employeeId,
                PersonId = personId,
                EntityType = UserTypeEnum.Employee.ToString()
            };
            return ActionView("~/Views/EmployeeMaster/CreateEditEmployeeAddress.cshtml", model);
        }

        #endregion Employee Address

        #region Employee Service
        public virtual ActionResult EmployeeServiceList(int employeeId,DataTableViewModel dataTableViewModel)
        {
            EmployeeMasterListViewModel list = _employeeMasterAgent.GetEmployeeMasterList(dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/EmployeeMaster/_List.cshtml", list);
            }
            return View($"~/Views/EmployeeMaster/List.cshtml", list);
        }
        #endregion Employee Service
    }
}