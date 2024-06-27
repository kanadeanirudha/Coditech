using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPatientAppointmentPurposeController : BaseController
    {
        private readonly IHospitalPatientAppointmentPurposeAgent _hospitalPatientAppointmentPurposeAgent;
        private const string createEdit = "~/Views/HMS/HospitalPatientAppointmentPurpose/CreateEdit.cshtml";

        public HospitalPatientAppointmentPurposeController(IHospitalPatientAppointmentPurposeAgent hospitalPatientAppointmentPurposeAgent)
        {
            _hospitalPatientAppointmentPurposeAgent = hospitalPatientAppointmentPurposeAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalPatientAppointmentPurposeListViewModel list = _hospitalPatientAppointmentPurposeAgent.GetHospitalPatientAppointmentPurposeList(dataTableModel);
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPatientAppointmentPurpose/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPatientAppointmentPurpose/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPatientAppointmentPurposeViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPatientAppointmentPurposeViewModel hospitalPatientAppointmentPurposeViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalPatientAppointmentPurposeViewModel = _hospitalPatientAppointmentPurposeAgent.CreateHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeViewModel);
                if (!hospitalPatientAppointmentPurposeViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPatientAppointmentPurposeViewModel.ErrorMessage));
            return View(createEdit, hospitalPatientAppointmentPurposeViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(short HospitalPatientAppointmentPurposeId)
        {
            HospitalPatientAppointmentPurposeViewModel HospitalPatientAppointmentPurposeViewModel = _hospitalPatientAppointmentPurposeAgent.GetHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeId);
            return ActionView(createEdit, HospitalPatientAppointmentPurposeId);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPatientAppointmentPurposeViewModel hospitalPatientAppointmentPurposeViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPatientAppointmentPurposeAgent.UpdateHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPatientAppointmentPurposeViewModel.HospitalPatientAppointmentPurposeId });
            }
            return View(createEdit, hospitalPatientAppointmentPurposeViewModel);
        }

        public virtual ActionResult Delete(string hospitalPatientAppointmentPurposeIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPatientAppointmentPurposeIds))
            {
                status = _hospitalPatientAppointmentPurposeAgent.DeleteHospitalPatientAppointmentPurpose(hospitalPatientAppointmentPurposeIds, out message);
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