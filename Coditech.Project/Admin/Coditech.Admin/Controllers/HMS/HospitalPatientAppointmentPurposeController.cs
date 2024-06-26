using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPatientAppointmentPurposeController : BaseController
    {
        private readonly IHospitalPatientAppointmentPurposeAgent _generalHospitalPatientAppointmentPurposeAgent;
        private const string createEdit = "~/Views/GeneralMaster/HospitalPatientAppointmentPurpose/CreateEdit.cshtml";

        public HospitalPatientAppointmentPurposeController(IHospitalPatientAppointmentPurposeAgent generalHospitalPatientAppointmentPurposeAgent)
        {
            _generalHospitalPatientAppointmentPurposeAgent = generalHospitalPatientAppointmentPurposeAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalPatientAppointmentPurposeListViewModel list = _generalHospitalPatientAppointmentPurposeAgent.GetHospitalPatientAppointmentPurposeList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/GeneralMaster/HospitalPatientAppointmentPurpose/_List.cshtml", list);
            }
            return View($"~/Views/GeneralMaster/HospitalPatientAppointmentPurpose/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPatientAppointmentPurposeViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPatientAppointmentPurposeViewModel HospitalPatientAppointmentPurposeViewModel)
        {
            if (ModelState.IsValid)
            {
                HospitalPatientAppointmentPurposeViewModel = _generalHospitalPatientAppointmentPurposeAgent.CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel);
                if (!HospitalPatientAppointmentPurposeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(HospitalPatientAppointmentPurposeViewModel.ErrorMessage));
            return View(createEdit, HospitalPatientAppointmentPurposeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short HospitalPatientAppointmentPurposeId)
        {
            HospitalPatientAppointmentPurposeViewModel HospitalPatientAppointmentPurposeViewModel = _generalHospitalPatientAppointmentPurposeAgent.GetHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeId);
            return ActionView(createEdit, HospitalPatientAppointmentPurposeId);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPatientAppointmentPurposeViewModel HospitalPatientAppointmentPurposeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_generalHospitalPatientAppointmentPurposeAgent.UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { HospitalPatientAppointmentPurposeId = HospitalPatientAppointmentPurposeViewModel.HospitalPatientAppointmentPurposeId });
            }
            return View(createEdit, HospitalPatientAppointmentPurposeViewModel);
        }

        public virtual ActionResult Delete(string appointmentIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(appointmentIds))
            {
                status = _generalHospitalPatientAppointmentPurposeAgent.DeleteHospitalPatientAppointmentPurpose(appointmentIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalPatientAppointmentPurposeController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPatientAppointmentPurposeController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}