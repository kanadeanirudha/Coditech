using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralUserModuleMasterController : BaseController
    {
        private readonly IGeneralUserModuleAgent _generalUserModuleAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralUserModule/CreateEdit.cshtml";

        public GeneralUserModuleMasterController(IGeneralUserModuleAgent generalUserModuleAgent)
        {
            _generalUserModuleAgent = generalUserModuleAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            UserModuleListViewModel list = _generalUserModuleAgent.GetUserModuleList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralUserModule/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralUserModule/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new UserModuleViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(UserModuleViewModel generalUserModuleViewModel)
        {
            if (ModelState.IsValid)
            {
                generalUserModuleViewModel = _generalUserModuleAgent.CreateUserModule(generalUserModuleViewModel);
                if (!generalUserModuleViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalUserModuleViewModel.ErrorMessage));
            return View(createEdit, generalUserModuleViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short userModuleMasterId)
        {
            UserModuleViewModel generalUserModuleViewModel = _generalUserModuleAgent.GetUserModule(userModuleMasterId);
            return ActionView(createEdit, generalUserModuleViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(UserModuleViewModel generalUserModuleViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalUserModuleAgent.UpdateUserModule(generalUserModuleViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { userModuleMasterId = generalUserModuleViewModel.UserModuleMasterId });
            }
            return View(createEdit, generalUserModuleViewModel);
        }

        public virtual ActionResult Delete(string userModuleMasterIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(userModuleMasterIds))
            {
                status = _generalUserModuleAgent.DeleteUserModule(userModuleMasterIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralUserModuleMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralUserModuleMasterController>(x => x.List(null));
        }
        #region Protected

        #endregion
    }
}