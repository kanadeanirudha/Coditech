using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralEnumaratorGroupMasterController : BaseController
    {
        private readonly IGeneralEnumaratorGroupAgent _generalEnumaratorGroupAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralEnumaratorGroup/CreateEdit.cshtml";

        public GeneralEnumaratorGroupMasterController(IGeneralEnumaratorGroupAgent generalEnumaratorGroupAgent)
        {
            _generalEnumaratorGroupAgent = generalEnumaratorGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralEnumaratorGroupListViewModel list = _generalEnumaratorGroupAgent.GetGeneralEnumaratorGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralEnumaratorGroup/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralEnumaratorGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralEnumaratorGroupViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                generalEnumaratorGroupViewModel = _generalEnumaratorGroupAgent.CreateGeneralEnumaratorGroup(generalEnumaratorGroupViewModel);
                if (!generalEnumaratorGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralEnumaratorGroupMasterController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalEnumaratorGroupViewModel.ErrorMessage));
            return View(createEdit, generalEnumaratorGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int generalEnumaratorGroupId)
        {
            GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel = _generalEnumaratorGroupAgent.GetGeneralEnumaratorGroup(generalEnumaratorGroupId);
            return ActionView(createEdit, generalEnumaratorGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalEnumaratorGroupAgent.UpdateGeneralEnumaratorGroup(generalEnumaratorGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalEnumaratorGroupId = generalEnumaratorGroupViewModel.GeneralEnumaratorGroupId });
            }
            return View(createEdit, generalEnumaratorGroupViewModel);
        }

        public virtual ActionResult Delete(string enumaratorIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(enumaratorIds))
            {
                status = _generalEnumaratorGroupAgent.DeleteGeneralEnumaratorGroup(enumaratorIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralEnumaratorGroupMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralEnumaratorGroupMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}