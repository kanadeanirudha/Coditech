using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.Helper.Utilities;
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
            if (hospitalDoctorLeaveScheduleViewModel.IsFullDay)
            {
                ModelState.Remove("FromTime");
                ModelState.Remove("UptoTime");
            }
            if (ModelState.IsValid)
            {
                hospitalDoctorLeaveScheduleViewModel = _hospitalDoctorLeaveScheduleAgent.CreateHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleViewModel);
                if (!hospitalDoctorLeaveScheduleViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", CreateActionDataTable(hospitalDoctorLeaveScheduleViewModel.SelectedCentreCode, Convert.ToInt16(hospitalDoctorLeaveScheduleViewModel.SelectedDepartmentId)));
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
            if (hospitalDoctorLeaveScheduleViewModel.IsFullDay)
            {
                ModelState.Remove("FromTime");
                ModelState.Remove("UptoTime");
            }
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorLeaveScheduleAgent.UpdateHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalDoctorLeaveScheduleId = hospitalDoctorLeaveScheduleViewModel.HospitalDoctorLeaveScheduleId });
            }
            return View(createEdit, hospitalDoctorLeaveScheduleViewModel);
        }

        public virtual ActionResult Delete(string hospitalDoctorLeaveScheduleIds, string SelectedCentreCode, short SelectedDepartmentId)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalDoctorLeaveScheduleIds))
            {
                status = _hospitalDoctorLeaveScheduleAgent.DeleteHospitalDoctorLeaveSchedule(hospitalDoctorLeaveScheduleIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = Convert.ToInt16(SelectedDepartmentId) });
            }

            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = Convert.ToInt16(SelectedDepartmentId) });
        }

        public virtual ActionResult Cancel(string SelectedCentreCode, short SelectedDepartmentId)
        {
            DataTableViewModel dataTableViewModel = new DataTableViewModel() { SelectedCentreCode = SelectedCentreCode, SelectedDepartmentId = SelectedDepartmentId };
            return RedirectToAction("List", dataTableViewModel);
        }

        public virtual ActionResult GetHospitalDoctorsList(string selectedCentreCode, string selectedDepartmentId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.HospitalDoctorsList.ToString(),
                DropdownName = "HospitalDoctorId",
                Parameter = $"{selectedCentreCode}~{selectedDepartmentId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }
        #region Protected

        #endregion
    }
}