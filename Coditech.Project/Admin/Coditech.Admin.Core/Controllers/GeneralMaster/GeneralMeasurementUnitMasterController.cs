using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralMeasurementUnitMasterController : BaseController
    {
        private readonly IGeneralMeasurementUnitAgent _generalMeasurementUnitAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralMeasurementUnitMaster/CreateEdit.cshtml";

        public GeneralMeasurementUnitMasterController(IGeneralMeasurementUnitAgent generalMeasurementUnitAgent)
        {
            _generalMeasurementUnitAgent = generalMeasurementUnitAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralMeasurementUnitListViewModel list = _generalMeasurementUnitAgent.GetMeasurementUnitList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralMeasurementUnitMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralMeasurementUnitMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralMeasurementUnitViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel)
        {
            if (ModelState.IsValid)
            {
                generalMeasurementUnitViewModel = _generalMeasurementUnitAgent.CreateMeasurementUnit(generalMeasurementUnitViewModel);
                if (!generalMeasurementUnitViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    if (string.Equals(generalMeasurementUnitViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalMeasurementUnitId = generalMeasurementUnitViewModel.GeneralMeasurementUnitMasterId });
                    }
                    else if (string.Equals(generalMeasurementUnitViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction(AdminConstants.ActionRedirectToList);
                    }
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalMeasurementUnitViewModel.ErrorMessage));
            return View(createEdit, generalMeasurementUnitViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalMeasurementUnitId)
        {
            GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel = _generalMeasurementUnitAgent.GetMeasurementUnit(generalMeasurementUnitId);
            return ActionView(createEdit, generalMeasurementUnitViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalMeasurementUnitAgent.UpdateMeasurementUnit(generalMeasurementUnitViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (string.Equals(generalMeasurementUnitViewModel.ActionMode, AdminConstants.ActionModeSave, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToEdit, new { generalMeasurementUnitId = generalMeasurementUnitViewModel.GeneralMeasurementUnitMasterId });
                }
                else if (string.Equals(generalMeasurementUnitViewModel.ActionMode, AdminConstants.ActionModeSaveAndClose, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction(AdminConstants.ActionRedirectToList);
                }
            }
            return View(createEdit, generalMeasurementUnitViewModel);
        }

        public virtual ActionResult Delete(string MeasurementUnitIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(MeasurementUnitIds))
            {
                status = _generalMeasurementUnitAgent.DeleteMeasurementUnit(MeasurementUnitIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralMeasurementUnitMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralMeasurementUnitMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}