using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorLeaveScheduleController : BaseController
    {
        private readonly IHospitalDoctorLeaveScheduleAgent _hospitalDoctorLeaveScheduleAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctorLeaveSchedule/CreateEdit.cshtml";

        public HospitalDoctorLeaveScheduleController(IHospitalDoctorLeaveScheduleAgent hospitalDoctorLeaveScheduleAgent)
        {
            _hospitalDoctorLeaveScheduleAgent = hospitalDoctorLeaveScheduleAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorLeaveScheduleListViewModel list = new HospitalDoctorLeaveScheduleListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorLeaveScheduleAgent.GetHospitalDoctorLeaveScheduleList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctorLeaveSchedule/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctorLeaveSchedule/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View(createEdit, new HospitalDoctorLeaveScheduleViewModel());
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalDoctorLeaveScheduleViewModel = _hospitalDoctorLeaveScheduleAgent.CreateHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleViewModel);
                if (!hospitalDoctorLeaveScheduleViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable());
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorLeaveScheduleViewModel.ErrorMessage));
            return View(createEdit, hospitalDoctorLeaveScheduleViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(long hospitalDoctorLeaveScheduleId)
        {
            HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel = _hospitalDoctorLeaveScheduleAgent.GetHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleId);
            return ActionView(createEdit, hospitalDoctorLeaveScheduleViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorLeaveScheduleViewModel hospitalDoctorLeaveScheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorLeaveScheduleAgent.UpdateHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalDoctorLeaveScheduleId = hospitalDoctorLeaveScheduleViewModel.HospitalDoctorLeaveScheduleId });
            }
            return View(createEdit, hospitalDoctorLeaveScheduleViewModel);
        }

        public virtual ActionResult Delete(string hospitalDoctorLeaveScheduleIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalDoctorLeaveScheduleIds))
            {
                status = _hospitalDoctorLeaveScheduleAgent.DeleteHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction<HospitalDoctorLeaveScheduleController>(x => x.List(null));
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction<HospitalDoctorLeaveScheduleController>(x => x.List(null));
        }

        #region Protected

        #endregion
    }
}