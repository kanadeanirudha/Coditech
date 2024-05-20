using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
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
        public virtual ActionResult Edit(int hospitalDoctorId, int hospitalDoctorOPDScheduleId)
        {
            HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel = _hospitalDoctorOPDScheduleAgent.GetHospitalDoctorOPDSchedule(hospitalDoctorId, hospitalDoctorOPDScheduleId);
            return ActionView(createEdit, hospitalDoctorOPDScheduleViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorOPDScheduleViewModel hospitalDoctorOPDScheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorOPDScheduleAgent.UpdateHospitalDoctorOPDSchedule(hospitalDoctorOPDScheduleViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("List", CreateActionDataTable(hospitalDoctorOPDScheduleViewModel.HospitalDoctorId, Convert.ToInt16(hospitalDoctorOPDScheduleViewModel.HospitalDoctorId)));
            }
            return View(createEdit, hospitalDoctorOPDScheduleViewModel);
        }

        public virtual ActionResult GetHospitalDoctorOPDSchedule(string organisationCentrewiseBuildingMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuildingRooms.ToString(),
                DropdownName = "HospitalDoctorOPDScheduleId",
                Parameter = hospitalDoctorOPDScheduleId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
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