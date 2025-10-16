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
        private const string createEditGeneralEnum = "~/Views/GeneralMaster/GeneralEnumaratorGroup/_CreateEditGeneralEnum.cshtml";

        public GeneralEnumaratorGroupController(IGeneralEnumaratorGroupAgent generalEnumaratorGroupAgent)
        {
            _generalEnumaratorGroupAgent = generalEnumaratorGroupAgent;
        }

        #region EnumaratorGroup
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
                    return RedirectToAction("Edit", new { generalEnumaratorGroupId = generalEnumaratorGroupViewModel.GeneralEnumaratorGroupId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalEnumaratorGroupViewModel.ErrorMessage));
            return View(createEdit, generalEnumaratorGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(string generalEnumaratorGroupId)
        {
            GeneralEnumaratorGroupViewModel generalEnumaratorGroupViewModel = _generalEnumaratorGroupAgent.GetEnumaratorGroup(Convert.ToInt32(generalEnumaratorGroupId));
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

        #endregion

        #region Enumarator
        [HttpGet]
        public virtual ActionResult CreateEditEnumarator(string generalEnumaratorGroupId, string generalEnumaratorId)
        {
            GeneralEnumaratorViewModel generalEnumaratorViewModel = new GeneralEnumaratorViewModel()
            {
                GeneralEnumaratorGroupId = Convert.ToInt32(generalEnumaratorGroupId)
            };
            if (Convert.ToInt32(generalEnumaratorId) > 0)
            {
                generalEnumaratorViewModel = _generalEnumaratorGroupAgent.GetEnumarator(Convert.ToInt32(generalEnumaratorId));
            }
            return ActionView(createEditGeneralEnum, generalEnumaratorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult CreateEditEnumarator(GeneralEnumaratorViewModel generalEnumaratorViewModel)
        {
            if (ModelState.IsValid)
            {
                generalEnumaratorViewModel = _generalEnumaratorGroupAgent.InsertUpdateEnumarator(generalEnumaratorViewModel);
                if (!generalEnumaratorViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("Edit", "GeneralEnumaratorGroup", new { generalEnumaratorGroupId = generalEnumaratorViewModel.GeneralEnumaratorGroupId });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalEnumaratorViewModel.ErrorMessage));
            return ActionView(createEditGeneralEnum, generalEnumaratorViewModel);
        }

        public virtual ActionResult DeleteEnumarator(string generalEnumaratorGroupId, string generalEnumaratorIds)
        {
            string message = string.Empty;

            if (!string.IsNullOrEmpty(generalEnumaratorIds))
            {
                bool status = false;
                status = _generalEnumaratorGroupAgent.DeleteEnumarator(generalEnumaratorIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
            }
            else
            {
                SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            }
            return RedirectToAction("Edit", "GeneralEnumaratorGroup", new { generalEnumaratorGroupId });

        }
        #endregion
        #region Protected

        #endregion
    }
}