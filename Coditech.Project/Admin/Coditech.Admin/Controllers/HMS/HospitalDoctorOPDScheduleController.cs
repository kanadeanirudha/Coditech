using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorOPDScheduleController : BaseController
    {
        private readonly IHospitalDoctorOPDScheduleAgent _hospitalDoctorOPDScheduleAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctorOPDSchedule/CreateEdit.cshtml";

        public HospitalDoctorOPDScheduleController(IHospitalDoctorOPDScheduleAgent hospitalDoctorOPDScheduleAgent)
        {
            _hospitalDoctorOPDScheduleAgent = hospitalDoctorOPDScheduleAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorOPDScheduleListViewModel list = new HospitalDoctorOPDScheduleListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorOPDScheduleAgent.GetHospitalDoctorOPDScheduleList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctorOPDSchedule/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctorOPDSchedule/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Edit(int hospitalDoctorId, int weekDayEnumId = 0)
        {
            HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel = _hospitalDoctorOPDScheduleAgent.GetHospitalDoctorOPDSchedule(hospitalDoctorId, weekDayEnumId);
            return ActionView(createEdit, hospitalDoctorOPDScheduleViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                bool hasError = _hospitalDoctorOPDScheduleAgent.UpdateHospitalDoctorOPDSchedule(hospitalDoctorOPDScheduleViewModel).HasError;
                SetNotificationMessage(hasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                if (!hasError)
                    return RedirectToAction("Edit", new { hospitalDoctorId = hospitalDoctorOPDScheduleViewModel.HospitalDoctorId, weekDayEnumId = hospitalDoctorOPDScheduleViewModel.WeekDayEnumId });
            }
            return View(createEdit, hospitalDoctorOPDScheduleViewModel);
        }

        public virtual ActionResult Cancel(string SelectedCentreCode, short SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }

        #region Protected

        #endregion
    }
}