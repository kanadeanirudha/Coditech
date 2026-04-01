using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralSchoolMasterController : BaseController
    {
        private readonly IGeneralSchoolAgent _generalSchoolAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralSchoolMaster/CreateEdit.cshtml";

        public GeneralSchoolMasterController(IGeneralSchoolAgent generalSchoolAgent)
        {
            _generalSchoolAgent = generalSchoolAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralSchoolListViewModel list = _generalSchoolAgent.GetSchoolList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralSchoolMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralSchoolMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralSchoolViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralSchoolViewModel generalSchoolViewModel)
        {
            if (ModelState.IsValid)
            {
                generalSchoolViewModel = _generalSchoolAgent.CreateSchool(generalSchoolViewModel);
                if (!generalSchoolViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalSchoolViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalSchoolId = generalSchoolViewModel.GeneralSchoolMasterId });
                    }
                    else if (string.Equals(generalSchoolViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalSchoolViewModel.ErrorMessage));
            return View(createEdit, generalSchoolViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalSchoolId)
        {
            GeneralSchoolViewModel generalSchoolViewModel = _generalSchoolAgent.GetSchool(generalSchoolId);
            return ActionView(createEdit, generalSchoolViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralSchoolViewModel generalSchoolViewModel)
        {
            if (ModelState.IsValid)
            {
                generalSchoolViewModel = _generalSchoolAgent.UpdateSchool(generalSchoolViewModel);
                SetNotificationMessage(generalSchoolViewModel.HasError
                ? GetErrorNotificationMessage(generalSchoolViewModel.ErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalSchoolViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalSchoolId = generalSchoolViewModel.GeneralSchoolMasterId });
                }
                else if (string.Equals(generalSchoolViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalSchoolViewModel);
        }

        public virtual ActionResult Delete(string SchoolIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(SchoolIds))
            {
                status = _generalSchoolAgent.DeleteSchool(SchoolIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralSchoolMasterController>(x => x.List(null));
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralSchoolMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}