﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coditech.Admin.Controllers
{
    public class GeneralPolicyMasterController : BaseController
    {
        private readonly IGeneralPolicyAgent _generalPolicyAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralPolicyMaster/CreateEdit.cshtml";

        public GeneralPolicyMasterController(IGeneralPolicyAgent generalPolicyAgent)
        {
            _generalPolicyAgent = generalPolicyAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralPolicyListViewModel list = _generalPolicyAgent.GetPolicyList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralPolicyMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralPolicyMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            GeneralPolicyViewModel generalPolicyViewModel = new GeneralPolicyViewModel();
            BindDropdown(generalPolicyViewModel);
            return View(createEdit, new GeneralPolicyViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralPolicyViewModel generalPolicyViewModel)
        {
            if (ModelState.IsValid)
            {
                generalPolicyViewModel = _generalPolicyAgent.CreatePolicy(generalPolicyViewModel);
                if (!generalPolicyViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("Edit", new { policyCode = generalPolicyViewModel.PolicyCode });
                }
            }
            BindDropdown(generalPolicyViewModel);
            SetNotificationMessage(GetErrorNotificationMessage(generalPolicyViewModel.ErrorMessage));
            return View(createEdit, generalPolicyViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(string policyCode)
        {
            GeneralPolicyViewModel generalPolicyViewModel = _generalPolicyAgent.GetPolicy(policyCode);
            BindDropdown(generalPolicyViewModel);
            return ActionView(createEdit, generalPolicyViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralPolicyViewModel generalPolicyViewModel)
        {
            if (ModelState.IsValid)
            {
                generalPolicyViewModel = _generalPolicyAgent.UpdatePolicy(generalPolicyViewModel);
                SetNotificationMessage(generalPolicyViewModel.HasError
                ? GetErrorNotificationMessage(generalPolicyViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { policyCode = generalPolicyViewModel.PolicyCode });
            }
            BindDropdown(generalPolicyViewModel);
            return View(createEdit, generalPolicyViewModel);
        }

        public virtual ActionResult Delete(string policyCode)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(policyCode))
            {
                status = _generalPolicyAgent.DeletePolicy(policyCode, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralPolicyMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralPolicyMasterController>(x => x.List(null));
        }

        #region General Policy Rules
        //General Policy Rules
        public virtual ActionResult GetGeneralPolicyRulesList(string policyCode)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel();
            GeneralPolicyRulesListViewModel list = _generalPolicyAgent.GetGeneralPolicyRulesList(policyCode, dataTableViewModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/_GeneralPolicyRulesList.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/GeneralPolicyRulesList.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult CreatePolicyRules(string policyCode)
        {
            GeneralPolicyRulesViewModel generalPolicyRulesViewModel = new GeneralPolicyRulesViewModel()
            {
                PolicyCode = policyCode
            };
            return View("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/CreateEditPolicyRules.cshtml", generalPolicyRulesViewModel);
        }

        [HttpPost]
        public virtual ActionResult CreatePolicyRules(GeneralPolicyRulesViewModel generalPolicyViewModel)
        {
            if (ModelState.IsValid)
            {
                generalPolicyViewModel = _generalPolicyAgent.CreatePolicyRules(generalPolicyViewModel);
                if (!generalPolicyViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("GetGeneralPolicyRulesList", new { policyCode = generalPolicyViewModel.PolicyCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalPolicyViewModel.ErrorMessage));
            return View("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/CreateEditPolicyRules.cshtml", generalPolicyViewModel);
        }


        [HttpGet]
        public virtual ActionResult EditRules(short generalPolicyRulesId, string policyApplicableStatus)
        {
            GeneralPolicyRulesViewModel generalPolicyRulesViewModel = _generalPolicyAgent.GetPolicyRules(generalPolicyRulesId, policyApplicableStatus);
            return ActionView("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/CreateEditPolicyRules.cshtml", generalPolicyRulesViewModel);
        }

        [HttpPost]
        public virtual ActionResult EditRules(GeneralPolicyRulesViewModel generalPolicyRulesViewModel)
        {
            if (ModelState.IsValid)
            {
                generalPolicyRulesViewModel = _generalPolicyAgent.UpdatePolicyRules(generalPolicyRulesViewModel);
                SetNotificationMessage(generalPolicyRulesViewModel.HasError
                ? GetErrorNotificationMessage(generalPolicyRulesViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("EditRules", new { generalPolicyRulesId = generalPolicyRulesViewModel.GeneralPolicyRulesId, policyApplicableStatus = generalPolicyRulesViewModel.PolicyApplicableStatus });
            }
            return View("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/CreateEditPolicyRules.cshtml", generalPolicyRulesViewModel);
        }

        public virtual ActionResult DeleteRules(string generalPolicyRulesIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalPolicyRulesIds))
            {
                status = _generalPolicyAgent.DeletePolicyRules(generalPolicyRulesIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralPolicyMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralPolicyMasterController>(x => x.List(null));
        }

        [HttpGet]
        public virtual ActionResult EditDetails(short generalPolicyDetailId)
        {
            GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel = _generalPolicyAgent.GetPolicyDetails(generalPolicyDetailId);
            return ActionView("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/CreateEditPolicyRules.cshtml", generalPolicyDetailsViewModel);
        }

        [HttpPost]
        public virtual ActionResult EditDetails(GeneralPolicyDetailsViewModel generalPolicyDetailsViewModel)
        {
            if (ModelState.IsValid)
            {
                generalPolicyDetailsViewModel = _generalPolicyAgent.UpdatePolicyDetails(generalPolicyDetailsViewModel);
                SetNotificationMessage(generalPolicyDetailsViewModel.HasError
                ? GetErrorNotificationMessage(generalPolicyDetailsViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("EditDetails", new { generalPolicyDetailId = generalPolicyDetailsViewModel.GeneralPolicyDetailId });
            }
            return View("~/Views/GeneralMaster/GeneralPolicyMaster/GeneralPolicyRules/CreateEditPolicyRules.cshtml", generalPolicyDetailsViewModel);
        }

        #endregion
        #region Protected
        protected virtual void BindDropdown(GeneralPolicyViewModel generalPolicyViewModel)
        {
            List<SelectListItem> policyApplicableStatusList = new List<SelectListItem>();
            policyApplicableStatusList.Add(new SelectListItem { Text = AdminConstants.General, Value = AdminConstants.General });
            policyApplicableStatusList.Add(new SelectListItem { Text = AdminConstants.Centrewise, Value = AdminConstants.Centrewise });
            ViewData["PolicyApplicableStatus"] = policyApplicableStatusList;
        }
        #endregion
    }
}