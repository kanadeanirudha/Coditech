using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class GeneralDistrictMasterController : BaseController
    {
        private readonly IGeneralDistrictAgent _generalDistrictAgent;
        private const string createEdit = "~/Views/GeneralMaster/GeneralDistrictMaster/CreateEdit.cshtml";

        public GeneralDistrictMasterController(IGeneralDistrictAgent generalDistrictAgent)
        {
            _generalDistrictAgent = generalDistrictAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            GeneralDistrictListViewModel list = _generalDistrictAgent.GetDistrictList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/GeneralDistrictMaster/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/GeneralDistrictMaster/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new GeneralDistrictViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(GeneralDistrictViewModel generalDistrictViewModel)
        {
            if (ModelState.IsValid)
            {
                generalDistrictViewModel = _generalDistrictAgent.CreateDistrict(generalDistrictViewModel);
                if (!generalDistrictViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(generalDistrictViewModel.ErrorMessage));
            return View(createEdit, generalDistrictViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short generalDistrictId)
        {
            GeneralDistrictViewModel generalDistrictViewModel = _generalDistrictAgent.GetDistrict(generalDistrictId);
            return ActionView(createEdit, generalDistrictViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(GeneralDistrictViewModel generalDistrictViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalDistrictAgent.UpdateDistrict(generalDistrictViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { generalDistrictId = generalDistrictViewModel.GeneralDistrictMasterId });
            }
            return View(createEdit, generalDistrictViewModel);
        }

        public virtual ActionResult Delete(string districtIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(districtIds))
            {
                status = _generalDistrictAgent.DeleteDistrict(districtIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<GeneralDistrictMasterController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<GeneralDistrictMasterController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}