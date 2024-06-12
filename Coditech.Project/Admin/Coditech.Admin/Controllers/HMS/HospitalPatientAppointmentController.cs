﻿using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalPatientAppointmentController : BaseController
    {
        private readonly IHospitalPatientAppointmentAgent _hospitalPatientAppointmentAgent;
        private const string createEdit = "~/Views/HMS/HospitalPatientAppointment/CreateEdit.cshtml";

        public HospitalPatientAppointmentController(IHospitalPatientAppointmentAgent hospitalPatientAppointmentAgent)
        {
            _hospitalPatientAppointmentAgent = hospitalPatientAppointmentAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalPatientAppointmentListViewModel list = new HospitalPatientAppointmentListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalPatientAppointmentAgent.GetHospitalPatientAppointmentList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalPatientAppointment/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalPatientAppointment/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalPatientAppointmentViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel, string selectedCentreCode)
        {
            if (ModelState.IsValid)
            {
                hospitalPatientAppointmentViewModel = _hospitalPatientAppointmentAgent.CreateHospitalPatientAppointment(hospitalPatientAppointmentViewModel);
                if (!hospitalPatientAppointmentViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    //return RedirectToAction("List", CreateActionDataTable(hospitalPatientAppointmentViewModel.SelectedCentreCode, Convert.ToInt16(hospitalPatientAppointmentViewModel.SelectedDepartmentId)));
                    return RedirectToAction("List", new { selectedCentreCode });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalPatientAppointmentViewModel.ErrorMessage));
            return View(createEdit, hospitalPatientAppointmentViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalPatientAppointmentId)
        {
            HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel = _hospitalPatientAppointmentAgent.GetHospitalPatientAppointment(hospitalPatientAppointmentId);
            return ActionView(createEdit, hospitalPatientAppointmentViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalPatientAppointmentViewModel hospitalPatientAppointmentViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalPatientAppointmentAgent.UpdateHospitalPatientAppointment(hospitalPatientAppointmentViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalPatientAppointmentId = hospitalPatientAppointmentViewModel.HospitalPatientAppointmentId });
            }
            return View(createEdit, hospitalPatientAppointmentViewModel);
        }

        public virtual ActionResult Delete(string hospitalPatientAppointmentIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalPatientAppointmentIds))
            {
                status = _hospitalPatientAppointmentAgent.DeleteHospitalPatientAppointment(hospitalPatientAppointmentIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalPatientAppointmentController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalPatientAppointmentController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}