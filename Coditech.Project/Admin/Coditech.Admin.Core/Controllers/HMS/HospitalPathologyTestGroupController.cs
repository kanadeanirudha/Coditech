using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPathologyTestGroupController : BaseController
    {
        private readonly IHospitalPathologyTestGroupAgent _hospitalPathologyTestGroupAgent;
        private const string createEdit = "~/Views/HMS/HospitalPathologyTestGroup/CreateEdit.cshtml";

        public HospitalPathologyTestGroupController(IHospitalPathologyTestGroupAgent hospitalPathologyTestGroupAgent)
        {
            _hospitalPathologyTestGroupAgent = hospitalPathologyTestGroupAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalPathologyTestGroupListViewModel list = _hospitalPathologyTestGroupAgent.GetHospitalPathologyTestGroupList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPathologyTestGroup/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPathologyTestGroup/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPathologyTestGroupViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalPathologyTestGroupViewModel = _hospitalPathologyTestGroupAgent.CreateHospitalPathologyTestGroup(hospitalPathologyTestGroupViewModel);
                if (!hospitalPathologyTestGroupViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPathologyTestGroupViewModel.ErrorMessage));
            return View(createEdit, hospitalPathologyTestGroupViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int hospitalPathologyTestGroupId)
        {
            HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel = _hospitalPathologyTestGroupAgent.GetHospitalPathologyTestGroup(hospitalPathologyTestGroupId);
            return ActionView(createEdit, hospitalPathologyTestGroupViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPathologyTestGroupAgent.UpdateHospitalPathologyTestGroup(hospitalPathologyTestGroupViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPathologyTestGroupViewModel.HospitalPathologyTestGroupId });
            }
            return View(createEdit, hospitalPathologyTestGroupViewModel);
        }

        public virtual ActionResult Delete(string hospitalPathologyTestGroupIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPathologyTestGroupIds))
            {
                status = _hospitalPathologyTestGroupAgent.DeleteHospitalPathologyTestGroup(hospitalPathologyTestGroupIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalPathologyTestGroupController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPathologyTestGroupController>(x => x.List(null));
        }

    }
}
