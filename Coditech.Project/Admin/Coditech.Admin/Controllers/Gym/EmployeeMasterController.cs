using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class EmployeeMasterController : BaseController
    {
        private readonly IEmployeeMasterAgent _employeeMasterAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralCountryMaster/CreateEdit.cshtml";

        public EmployeeMasterController(IEmployeeMasterAgent employeeMasterAgent) 
        {
            _employeeMasterAgent = employeeMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            EmployeeMasterListViewModel list = _employeeMasterAgent.GetEmployeeMasterList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralCountryMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralCountryMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new EmployeeMasterViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(EmployeeMasterViewModel employeeMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeMasterViewModel = _employeeMasterAgent.CreateEmployee(employeeMasterViewModel);
                if (!employeeMasterViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<EmployeeMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(employeeMasterViewModel.ErrorMessage));
            return View(createEdit, employeeMasterViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalCountryId)
        {
            EmployeeMasterViewModel employeeMasterViewModel = _employeeMasterAgent.GetEmployeeDetails(generalCountryId);
            return ActionView(createEdit, employeeMasterViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(EmployeeMasterViewModel employeeMasterViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_employeeMasterAgent.UpdateEmployeeDetails(employeeMasterViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalCountryId = employeeMasterViewModel.EmployeeId });
            }
            return View(createEdit, employeeMasterViewModel);
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

        #region Protected

        #endregion
    }
}