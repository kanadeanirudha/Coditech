using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralEnumaratorGroupController : BaseController
    {
        private readonly IGeneralEnumaratorGroupAgent _generalEnumaratorGroupAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralEnumaratorGroup/CreateEdit.cshtml";

        public GeneralEnumaratorGroupController(IGeneralEnumaratorGroupAgent generalEnumaratorGroupAgent)
        {
            _generalEnumaratorGroupAgent = generalEnumaratorGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralEnumaratorGroupListViewModel list = _generalEnumaratorGroupAgent.GetEnumaratorGroupList(dataTableModel);
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
                generalEnumaratorGroupViewModel = _generalEnumaratorGroupAgent.CreateEnumaratorGroup(generalEnumaratorGroupViewModel);
                if (!generalEnumaratorGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction<GeneralEnumaratorGroupController>(x => x.List(null));
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalEnumaratorGroupViewModel.ErrorMessage));
            return View(createEdit, generalEnumaratorGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int generalEnumaratorGroupId)
        {
            GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel = _generalEnumaratorGroupAgent.GetEnumaratorGroup(generalEnumaratorGroupId);
            return ActionView(createEdit, generalEnumaratorGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalEnumaratorGroupAgent.UpdateEnumaratorGroup(generalEnumaratorGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalEnumaratorGroupId = generalEnumaratorGroupViewModel.GeneralEnumaratorGroupId });
            }
            return View(createEdit, generalEnumaratorGroupViewModel);
        }

        public virtual ActionResult Delete(string enumaratorGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(enumaratorGroupIds))
            {
                status = _generalEnumaratorGroupAgent.DeleteEnumaratorGroup(enumaratorGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralEnumaratorGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralEnumaratorGroupController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}