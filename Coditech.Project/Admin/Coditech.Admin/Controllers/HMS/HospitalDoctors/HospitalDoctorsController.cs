using Coditech.Admin.Agents;
using Coditech.Admin.Utilities;
using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;
using Coditech.Resources;

using Microsoft.AspNetCore.Mvc;

namespace Coditech.Admin.Controllers
{
    public class HospitalDoctorsController : BaseController
    {
        private readonly IHospitalDoctorsAgent _hospitalDoctorsAgent;
        private const string createEdit = "~/Views/HMS/HospitalDoctors/CreateEdit.cshtml";

        public HospitalDoctorsController(IHospitalDoctorsAgent hospitalDoctorsAgent)
        {
            _hospitalDoctorsAgent = hospitalDoctorsAgent;
        }

        public virtual ActionResult List(DataTableViewModel dataTableModel)
        {
            HospitalDoctorsListViewModel list = new HospitalDoctorsListViewModel();
            if (!string.IsNullOrEmpty(dataTableModel.SelectedCentreCode) && dataTableModel.SelectedDepartmentId > 0)
            {
                list = _hospitalDoctorsAgent.GetHospitalDoctorsList(dataTableModel.SelectedCentreCode, dataTableModel.SelectedDepartmentId, true, dataTableModel);
            }
            list.SelectedCentreCode = dataTableModel.SelectedCentreCode;
            list.SelectedDepartmentId = dataTableModel.SelectedDepartmentId;
            if (AjaxHelper.IsAjaxRequest)
            {
                return PartialView("~/Views/HMS/HospitalDoctors/_List.cshtml", list);
            }
            return View($"~/Views/HMS/HospitalDoctors/List.cshtml", list);
        }

        [HttpGet]
        public virtual ActionResult Create()
        {
            HospitalDoctorsViewModel hospitalDoctorsViewModel = new HospitalDoctorsViewModel();
            BindDropdown(hospitalDoctorsViewModel);
            return View(createEdit, hospitalDoctorsViewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            if (ModelState.IsValid)
            {
                hospitalDoctorsViewModel = _hospitalDoctorsAgent.CreateHospitalDoctors(hospitalDoctorsViewModel);
                if (!hospitalDoctorsViewModel.HasError)
                {
                    SetNotificationMessage(GetSuccessNotificationMessage(GeneralResources.RecordAddedSuccessMessage));
                    return RedirectToAction("List", new DataTableViewModel { SelectedCentreCode = hospitalDoctorsViewModel.SelectedCentreCode, SelectedDepartmentId = Convert.ToInt16(hospitalDoctorsViewModel.SelectedDepartmentId) });
                }
            }
            SetNotificationMessage(GetErrorNotificationMessage(hospitalDoctorsViewModel.ErrorMessage));
            BindDropdown(hospitalDoctorsViewModel);
            return View(createEdit, hospitalDoctorsViewModel);
        }

        [HttpGet]
        public virtual ActionResult Edit(int hospitalDoctorId)
        {
            HospitalDoctorsViewModel hospitalDoctorsViewModel = _hospitalDoctorsAgent.GetHospitalDoctors(hospitalDoctorId);
            BindDropdown(hospitalDoctorsViewModel);
            return ActionView(createEdit, hospitalDoctorsViewModel);
        }

        [HttpPost]
        public virtual ActionResult Edit(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            if (ModelState.IsValid)
            {
                SetNotificationMessage(_hospitalDoctorsAgent.UpdateHospitalDoctors(hospitalDoctorsViewModel).HasError
                ? GetErrorNotificationMessage(GeneralResources.UpdateErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.UpdateMessage));
                return RedirectToAction("Edit", new { hospitalDoctorId = hospitalDoctorsViewModel.HospitalDoctorId });
            }
            BindDropdown(hospitalDoctorsViewModel);
            return View(createEdit, hospitalDoctorsViewModel);
        }

        public virtual ActionResult Delete(string hospitalDoctorIds)
        {
            string message = string.Empty;
            bool status = false;
            if (!string.IsNullOrEmpty(hospitalDoctorIds))
            {
                status = _hospitalDoctorsAgent.DeleteHospitalDoctors(hospitalDoctorIds, out message);
                SetNotificationMessage(!status
                ? GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage)
                : GetSuccessNotificationMessage(GeneralResources.DeleteMessage));
                return RedirectToAction("List", CreateActionDataTable());
            }
            SetNotificationMessage(GetErrorNotificationMessage(GeneralResources.DeleteErrorMessage));
            return RedirectToAction("List", CreateActionDataTable());
        }

        public virtual ActionResult GetDepartmentsByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseDepartment.ToString(),
                DropdownName = "SelectedDepartmentId",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult GetOrganisationCentrewiseBuildingByCentreCode(string centreCode)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuilding.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingMasterId",
                Parameter = centreCode,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }


        public virtual ActionResult GetOrganisationCentrewiseRoomByBuildingId(string buildingMasterId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.CentrewiseBuildingRooms.ToString(),
                DropdownName = "OrganisationCentrewiseBuildingRoomId",
                Parameter = buildingMasterId,
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        public virtual ActionResult GetEmployeeList(string selectedCentreCode, string selectedDepartmentId)
        {
            DropdownViewModel departmentDropdown = new DropdownViewModel()
            {
                DropdownType = DropdownTypeEnum.UnAssociatedEmployeeList.ToString(),
                DropdownName = "EmployeeId",
                Parameter = $"{selectedCentreCode}~{selectedDepartmentId}",
            };
            return PartialView("~/Views/Shared/Control/_DropdownList.cshtml", departmentDropdown);
        }

        #region Protected
        protected virtual void BindDropdown(HospitalDoctorsViewModel hospitalDoctorsViewModel)
        {
            List<GeneralEnumaratorModel> weekDays = SessionHelper.GetDataFromSession<UserModel>(AdminConstants.UserDataSession)?.GeneralEnumaratorList?.Where(x => x.EnumGroupCode == DropdownTypeEnum.WeekDays.ToString())?.ToList();
            hospitalDoctorsViewModel.AllWeekDays = weekDays;
            if (hospitalDoctorsViewModel?.HospitalDoctorId > 0)
            {
                hospitalDoctorsViewModel.SelectedWeekDayEnumIds = new List<string>();
                foreach (string item in hospitalDoctorsViewModel.WeekDayEnumIds?.Split(','))
                {
                    hospitalDoctorsViewModel.SelectedWeekDayEnumIds.Add(item);
                }
            }
        }
        #endregion
    }
}
