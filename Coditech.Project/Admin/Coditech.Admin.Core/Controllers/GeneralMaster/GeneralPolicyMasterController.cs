using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

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
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalPolicyViewModel.ErrorMessage));
            return View(createEdit, generalPolicyViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalPolicyMasterId)
        {
            GeneralPolicyViewModel generalPolicyViewModel = _generalPolicyAgent.GetPolicy(generalPolicyMasterId);
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
                return RedirectToAction("Edit", new { generalPolicyMasterId = generalPolicyViewModel.GeneralPolicyMasterId });
            }
            return View(createEdit, generalPolicyViewModel);
        }

        public virtual ActionResult Delete(string generalPolicyMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(generalPolicyMasterIds))
            {
                status = _generalPolicyAgent.DeletePolicy(generalPolicyMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralPolicyMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralPolicyMasterController>(x => x.List(null));
        }       
        #region Protected

        #endregion
    }
}