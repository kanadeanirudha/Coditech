using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorVisitingChargesController : BaseController
    {
        private readonly IHospitalDoctorVisitingChargesAgent _hospitalDoctorVisitingChargesAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctorVisitingCharges/CreateEdit.cshtml";

        public HospitalDoctorVisitingChargesController(IHospitalDoctorVisitingChargesAgent hospitalDoctorVisitingChargesAgent)
        {
            _hospitalDoctorVisitingChargesAgent = hospitalDoctorVisitingChargesAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorVisitingChargesListViewModel list = new HospitalDoctorVisitingChargesListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingChargesList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, true, dataTableModel);
            }
            list.SelectedCentreCode= dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctorVisitingCharges/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctorVisitingCharges/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalDoctorVisitingChargesViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalDoctorVisitingChargesViewModel = _hospitalDoctorVisitingChargesAgent.CreateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel);
                if (!hospitalDoctorVisitingChargesViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorVisitingChargesViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short hospitalDoctorVisitingChargesId)
        {
            HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel = _hospitalDoctorVisitingChargesAgent.GetHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesId);
            return ActionView(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorVisitingChargesViewModel hospitalDoctorVisitingChargesViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorVisitingChargesAgent.UpdateHospitalDoctorVisitingCharges(hospitalDoctorVisitingChargesViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalDoctorVisitingChargesId = hospitalDoctorVisitingChargesViewModel.HospitalDoctorVisitingChargesId });
            }
            return View(createEdit, hospitalDoctorVisitingChargesViewModel);
        }

        public virtual ActionResult Delete(string hospitaldoctorvisitingchargesIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitaldoctorvisitingchargesIds))
            {
                status = _hospitalDoctorVisitingChargesAgent.DeleteHospitalDoctorVisitingCharges(hospitaldoctorvisitingchargesIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalDoctorVisitingChargesController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalDoctorVisitingChargesController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}