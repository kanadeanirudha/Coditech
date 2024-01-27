﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class EmployeeMasterController : BaseController
    {
        private readonly IEmployeeMasterAgent _employeeMasterAgent;
        private const string createEditEmployee = "~/Views/EmployeeMaster/GeneralCountryMaster/CreateEdit.cshtml";

        public EmployeeMasterController(IEmployeeMasterAgent employeeMasterAgent) 
        {
            _employeeMasterAgent = employeeMasterAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            EmployeeMasterListViewModel list = _employeeMasterAgent.GetEmployeeMasterList(dataTableModel);
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
        public virtual ActionResult CreateEmployee(EmployeeCreateEditViewModel employeeCreateEditViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeCreateEditViewModel = _employeeMasterAgent.CreateEmployee(employeeCreateEditViewModel);
                if (!employeeCreateEditViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<EmployeeMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(employeeCreateEditViewModel.ErrorMessage));
            return View(createEditEmployee, employeeCreateEditViewModel);
        }

        [HttpGet]
        public virtual ActionResult UpdateEmployeePersonalDetails(long employeeId, long personId)
        {
            EmployeeCreateEditViewModel employeeCreateEditViewModel = _employeeMasterAgent.GetEmployeePersonalDetails(personId);
            employeeCreateEditViewModel.EmployeeId = employeeId;
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
                return RedirectToAction("UpdateEmployeePersonalDetails", new { employeeId = employeeCreateEditViewModel.EmployeeId });
            }
            return View(createEditEmployee, employeeCreateEditViewModel);
        }

        //[HttpGet]
        //public virtual ActionResult GetEmployeeOtherDetail(int gymMemberDetailId)
        //{
        //    EmployeeMasterViewModel employeeMasterViewModel = _employeeMasterAgent.GetGymMemberOtherDetails(gymMemberDetailId);
        //    return View("~/Views/Gym/GymMemberDetails/UpdateGymMemberOtherDetails.cshtml", gymMemberDetailsViewModel);
        //}

        //[HttpPost]
        //public virtual ActionResult UpdateEmployeeOtherDetail(EmployeeMasterViewModel employeeMasterViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        SetNotificationMessage(_employeeMasterAgent.UpdateGymMemberOtherDetails(gymMemberDetailsViewModel).HasError
        //        ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
        //        : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
        //        return RedirectToAction("MemberOtherDetails", new { gymMemberDetailId = gymMemberDetailsViewModel.GymMemberDetailId, personId = gymMemberDetailsViewModel.PersonId });
        //    }
        //    return View("~/Views/Gym/GymMemberDetails/UpdateGymMemberOtherDetails.cshtml", gymMemberDetailsViewModel);
        //}
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
    }
}