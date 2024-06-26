﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralDepartmentMasterController : BaseController
    {
        private readonly IGeneralDepartmentAgent _generalDepartmentAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralDepartmentMaster/CreateEdit.cshtml";

        public GeneralDepartmentMasterController(IGeneralDepartmentAgent generalDepartmentAgent)
        {
            _generalDepartmentAgent = generalDepartmentAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralDepartmentListViewModel list = _generalDepartmentAgent.GetDepartmentList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralDepartmentMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralDepartmentMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralDepartmentViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralDepartmentViewModel generalDepartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                generalDepartmentViewModel = _generalDepartmentAgent.CreateDepartment(generalDepartmentViewModel);
                if (!generalDepartmentViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    // Redirect to the list action method instead of directly to the view
                    return RedirectToAction("List");
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalDepartmentViewModel.ErrorMessage));
            return View(createEdit, generalDepartmentViewModel);
        }



        [HttpGet]
        public virtual ActionResult Edit(int generalDepartmentId)
        {
            GeneralDepartmentViewModel generalDepartmentViewModel = _generalDepartmentAgent.GetDepartment(generalDepartmentId);
            return ActionView(createEdit, generalDepartmentViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralDepartmentViewModel generalDepartmentViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalDepartmentAgent.UpdateDepartment(generalDepartmentViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalDepartmentId = generalDepartmentViewModel.GeneralDepartmentMasterId });
            }
            return View(createEdit, generalDepartmentViewModel);
        }

        public virtual ActionResult Delete(string departmentIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(departmentIds))
            {
                status = _generalDepartmentAgent.DeleteDepartment(departmentIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralDepartmentMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralDepartmentMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}